using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialDSP
{
    public partial class MainForm : Form
    {
        //diagnostic data
        private readonly Stopwatch _sw = new Stopwatch();
        private volatile int _count = 0;
        //pre-saved vars
        private readonly Regex _dataPat = new Regex(@"(-?\d+),(-?\d+)");
        private readonly SerialPort _port = new SerialPort();
        private readonly StringBuilder _sb = new StringBuilder();
#pragma warning disable IDE0052 
        private readonly Axis _intgY, _intgX, _mpsX, _mpsY;
#pragma warning restore IDE0052
        private readonly Series _intgInSeries;
        private readonly Series _intgOutSeries;
        private readonly Series _intgModulusSeries;
        private readonly Series _mpsSeries;

        //delegate for other threads to invoke and corresponding argument arrays that minimize heap alloc and hence GC
        private readonly Action<string, string> _setPrintLbl;
        private readonly object[] _setPrintLbl_Args = new object[2]; 
        private readonly Action<float> _updateMps;
        private readonly object[] _updateMps_Args = new object[1];
        private readonly Action<List<float>, List<float>> _updateIntegralChart;
        private readonly object[] _updateIntegralChart_Args = new object[2];
        private bool _hasBegin = false;
        //Chart scaling
        private int _horizonPoints;
        //Chart update batches
        private readonly List<float> _inIntegrationBatch = new List<float>();
        private readonly List<float> _outIntegrationBatch = new List<float>();
        //Integration wrapper
        private readonly Integration _integration = new Integration();
        //Data fetch&processing loop
        private Thread _dataLoopThread;
        //string[] for in-place split
        private readonly string[] _splitBuffer = new string[2];

        public int HorizontalPoints
        {
            get => _horizonPoints;
            set
            {
                _horizonPoints = value;
                horizonPointsLbl.Text = _horizonPoints.ToString();
            }
        }
        public int MaxDrawPoints { get; set; }
        public int UpdateBatchSize { get; set; }

        public MainForm()
        {
            InitializeComponent();
            //Init stuff
            MaxDrawPoints = chartHorizonTrack.Maximum;
            _intgX = integralChart.ChartAreas[0].AxisX;
            _intgY = integralChart.ChartAreas[0].AxisY;
            _mpsX = mpsChart.ChartAreas[0].AxisX;
            _mpsY = mpsChart.ChartAreas[0].AxisY;
            _intgInSeries = integralChart.Series["In-phase"];
            _intgOutSeries = integralChart.Series["Out-of-phase"];
            _intgModulusSeries = integralChart.Series["Modulus"];
            _mpsSeries = mpsChart.Series["Mps"];
            mpsLbl.Text = string.Empty;
            //Serial port setup
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            LoadAvailablePorts(false);

            //manually-bind event handler
            integralChart.MouseWheel += OnIntgChartMouseWheel;
            _intgX.Interval = 0;

            //import delegates
            _setPrintLbl = (s, t) => { printInPhaseLbl.Text = s; printOutPhaseLbl.Text = t; };
            _updateMps = mps => 
            {
                _sb.Append(mps);
                mpsLbl.Text = _sb.ToString() + " Mps";
                _sb.Clear();
                _mpsSeries.Points.AddY(mps);
                _mpsSeries.Points.RemoveAt(0);
                //rolling X axis
                if (_mpsSeries.Points.Count >= _horizonPoints)
                    _mpsX.Minimum = _mpsX.Maximum - _horizonPoints;
            };
            _updateIntegralChart = (iIntgs, oIntgs) =>
            {
                for (int i = 0; i < iIntgs.Count; i++)
                {
                    float iIntg = iIntgs[i], oIntg = oIntgs[i];
                    _intgInSeries.Points.AddY(iIntg);
                    _intgOutSeries.Points.AddY(oIntg);
                    _intgModulusSeries.Points.AddY(Math.Sqrt(iIntg * iIntg + oIntg * oIntg));
                    _intgInSeries.Points.RemoveAt(0);
                    _intgOutSeries.Points.RemoveAt(0);
                    _intgModulusSeries.Points.RemoveAt(0);
                }
                //rolling X axis
                if (_intgInSeries.Points.Count > _horizonPoints)
                    _intgX.Minimum = _intgX.Maximum - _horizonPoints;
            };

            //Load setup from UI
            WindowTrackScroll(null, null);    //fire windowTrackbar ValueChanged event manually
            HorizontalPoints = chartHorizonTrack.Value;
            UpdateBatchSize = (int)updateBatchSizeBox.Value;

            //init charts with zeros
            for (int i = 0; i < chartHorizonTrack.Maximum; i++)
            {
                _mpsSeries.Points.AddY(0);
                _intgInSeries.Points.AddY(0);
                _intgOutSeries.Points.AddY(0);
                _intgModulusSeries.Points.AddY(0);
            }
            _intgY.Minimum = -1024;
            _intgY.Maximum = 1024;
        }

        //Serial COM
        private void LoadAvailablePorts(bool isInit)
        {
            portCombo.Items.Clear();
            portCombo.Items.AddRange(SerialPort.GetPortNames());
            if (portCombo.Items.Count != 0)
                portCombo.SelectedIndex = 0;
            else if (!isInit)
                MessageBox.Show("No available serial port detected. Plug in Arduino and refresh. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void ReadBufferChanged(object sender, EventArgs e)
        {
            if (_hasBegin)
            {
                BeginProcess(null, null);
            }
            int value = (int)readBufferNumericBox.Value;
            _port.ReadBufferSize = value;
        }
        private void DataReadLineLoop() //Data process loop running on another thread
        {
            _sw.Start();
            while (true)
            {
                _port.ReadLine().InPlaceSingleSplit(',', _splitBuffer);
                _count++;
                if (_count == 100)
                {
                    _sw.Stop();
                    UpdateMps(100_000f / _sw.ElapsedMilliseconds);
                    SetPrintLblText($"{_inIntegrationBatch.Last():f4}", $"{_outIntegrationBatch.Last():f4}");
                    _sw.Reset();
                    _count = 0;
                    _sw.Start();
                }
                //integration & average
                float inIntegral = int.Parse(_splitBuffer[0]);
                float outIntegral = int.Parse(_splitBuffer[1]);
                _integration.Roll(inIntegral, outIntegral);
                //Save new values to buffers
                _inIntegrationBatch.Add(_integration.AverageInPhase);
                _outIntegrationBatch.Add(_integration.AverageOutOfPhase);
                //When it's enough for an update, do it and clear the buffer
                if (_inIntegrationBatch.Count >= UpdateBatchSize)
                {
                    UpdateChart(_inIntegrationBatch, _outIntegrationBatch);
                    //Clear buffer after update to UI
                    _inIntegrationBatch.Clear();
                    _outIntegrationBatch.Clear();
                }
            }
        }
        private void RefreshPortBtn_Click(object sender, EventArgs e)
        {
            LoadAvailablePorts(false);
        }
        private void PortChanged(object sender, EventArgs e)
        {
            if (_hasBegin)
            {
                BeginProcess(null, null);
            }
            _port.PortName = portCombo.SelectedItem.ToString();
        }

        //General
        private void BeginProcess(object sender, EventArgs e)
        {
            if (_hasBegin) //stop
            {
                if (_dataLoopThread != null && _dataLoopThread.IsAlive)
                    _dataLoopThread.Abort();
                _port.Close();
                beginBtn.Text = "Begin";
                beginBtn.BackColor = Color.FromArgb(27, 161, 226);
                beginBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
                beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(122, 193, 255);
                mpsLbl.Text = string.Empty;
            }
            else  //begin
            {
                _port.BaudRate = (int)baudNumericBox.Value;
                _port.ReadBufferSize = (int)readBufferNumericBox.Value;
                _port.Open();
                //consume first line because it will be broken if the port open at the middle of an message (very-likely)
                _port.ReadLine();
                _dataLoopThread = new Thread(DataReadLineLoop);
                _dataLoopThread.Start();
                _integration.Reset();
                beginBtn.Text = "End";
                beginBtn.BackColor = Color.FromArgb(255, 109, 54);
                beginBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
                beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(215, 172, 106);
            }
            serialGroupBox.Enabled = _hasBegin;
            _hasBegin = !_hasBegin;
        }

        //DSP
        private void WindowTrackScroll(object sender, EventArgs e)
        {
            _integration.WindowSize = windowTrackbar.Value;
            windowLbl.Text = windowTrackbar.Value.ToString();
        }

        //Chart
        private void ChartHorizonTrackerScroll(object sender, EventArgs e)
        {
            HorizontalPoints = chartHorizonTrack.Value;
        }
        private void OnIntgChartMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                Chart chart = (Chart)sender;
                var y = chart.ChartAreas[0].AxisY;
                double original = y.Maximum;
                if (e.Delta < 0 && original < 1048576)
                {
                    y.Maximum = Math.Round(original * 1.25, 2);
                    y.Minimum = -y.Maximum;
                }
                else if (e.Delta > 0 && original > 0.06)
                {
                    y.Maximum = Math.Round(original / 1.25, 2);
                    y.Minimum = -y.Maximum;
                }
            }
        }
        private void UpdateBatchSizeChanged(object sender, EventArgs e)
        {
            UpdateBatchSize = (int)updateBatchSizeBox.Value;
            _inIntegrationBatch.Clear();
            _outIntegrationBatch.Clear();
        }
        private void AAChanged(object sender, EventArgs e)
        {
            if (aaBox.Checked)
            {
                integralChart.AntiAliasing = AntiAliasingStyles.All;
                mpsChart.AntiAliasing = AntiAliasingStyles.All;
            }
            else
            {
                integralChart.AntiAliasing = AntiAliasingStyles.Text;
                mpsChart.AntiAliasing = AntiAliasingStyles.Text;
            }
        }
        private void FastChanged(object sender, EventArgs e)
        {
            var type = fastBox.Checked ? SeriesChartType.FastLine : SeriesChartType.Line;
            foreach (var series in integralChart.Series)
            {
                series.ChartType = type;
            }
            foreach (var series in mpsChart.Series)
            {
                series.ChartType = type;
            }
        }


        //Cross-thread methods
        private void SetPrintLblText(string s, string t)
        {
            if (printInPhaseLbl.InvokeRequired)
            {
                _setPrintLbl_Args[0] = s;
                _setPrintLbl_Args[1] = t;
                Invoke(_setPrintLbl, _setPrintLbl_Args);
            }
            else
                _setPrintLbl(s, t);
        }
        private void UpdateMps(float mps)
        {
            if (printInPhaseLbl.InvokeRequired)
            {
                _updateMps_Args[0] = mps;
                Invoke(_updateMps, _updateMps_Args);
            }
            else
                _updateMps(mps);
        }
        private void UpdateChart(List<float> iIntgs, List<float> oIntgs)
        {
            if (integralChart.InvokeRequired)
            {
                _updateIntegralChart_Args[0] = iIntgs;
                _updateIntegralChart_Args[1] = oIntgs;
                Invoke(_updateIntegralChart, _updateIntegralChart_Args);
            }
            else
                _updateIntegralChart(iIntgs, oIntgs);
        }

        private void ClearOutChartBtn_Click(object sender, EventArgs e)
        {
            _intgX.Minimum = 0;
            foreach (var serie in integralChart.Series)
            {
                serie.Points.Clear();
            }
        }

        [Obsolete]
#pragma warning disable IDE0051 
        private bool ParseRegex(string s, out float inPhase, out float outOfPhase)
#pragma warning restore IDE0051 // 删除未使用的私有成员
        {
            if (_dataPat.IsMatch(s))
            {
                var groups = _dataPat.Match(s).Groups;
                inPhase = int.Parse(groups[1].Value);
                outOfPhase = int.Parse(groups[2].Value);
                return true;
            }
            else
            {
                inPhase = 0;
                outOfPhase = 0;
                return false;
            }
        }

    }
}
