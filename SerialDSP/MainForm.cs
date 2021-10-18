using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Filtering;
using MathNet.Filtering.FIR;
using MathNet.Numerics;

namespace SerialDSP
{
    public partial class MainForm : Form
    {

        private readonly Regex _dataPat = new Regex(@"(-?\d+),(-?\d+)");
        private readonly SerialPort _port = new SerialPort();
        // private readonly OnlineFilter lpf = OnlineFilter.CreateLowpass(ImpulseResponse.Finite, 250, 1);
        //delegate for other threads to invoke
        private readonly Action<string, string> _setPrintLbl;
        private readonly Action<List<float>, List<float>, List<float>, List<float>> _updateOutChart;
        private bool _hasBegin = false;
        //Chart scaling
        private int _horizonPoints;
        //Chart update batches
        private readonly List<float> _inIntegrationBatch = new List<float>();
        private readonly List<float> _outIntegrationBatch = new List<float>();
        private readonly List<float> _inLPFBatch = new List<float>();
        private readonly List<float> _outLPFBatch = new List<float>();
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
        public int UpdateBatchSize { get; set; }

        public MainForm()
        {
            InitializeComponent();
            //Serial port setup
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            LoadAvailablePorts(false);

            //manually-bind event handler
            integralChart.MouseWheel += OnChartMouseWheel;
            integralChart.ChartAreas[0].AxisX.Interval = 256;
            lpfChart.MouseWheel += OnChartMouseWheel;

            //import delegates
            _setPrintLbl = (s, t) => { printInPhaseLbl.Text = s; printOutPhaseLbl.Text = t; };
            _updateOutChart = (iIntgs, oIntgs, iLpfs, oLpfs) =>
            {
                for (int i = 0; i < iIntgs.Count; i++)
                {
                    //Update integral chart
                    UpdateChartOnePoint(iIntgs[i], oIntgs[i], integralChart);
                    //Update LPF chart
                    //UpdateChartOnePoint(iLpfs[i], oLpfs[i], lpfChart);
                }
            };

            //Load setup from UI
            WindowTrackScroll(null, null);    //fire windowTrackbar ValueChanged event manually
            HorizontalPoints = outChartHorizonTrack.Value;
            UpdateBatchSize = (int)updateBatchSizeBox.Value;
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
            while (true)
            {
                _port.ReadLine().InPlaceSingleSplit(',', _splitBuffer);
                //integration & average
                float inIntegral = int.Parse(_splitBuffer[0]);
                float outIntegral = int.Parse(_splitBuffer[1]);
                _integration.Roll(inIntegral, outIntegral);
                //Save new values to buffers
                _inIntegrationBatch.Add(_integration.AverageInPhase);
                _outIntegrationBatch.Add(_integration.AverageOutOfPhase);
                //DSP LPF
                // double inLpf = lpf.ProcessSample(inIntegral);
                // double outLpf = lpf.ProcessSample(outIntegral);
                // _inLPFBatch.Add((float)inLpf);
                // _outLPFBatch.Add((float)outLpf);
                //When it's enough for an update, do it and clear the buffer
                if (_inIntegrationBatch.Count >= UpdateBatchSize)
                {
                    SetPrintLblText($"{_inIntegrationBatch.Last():f4}", $"{_outIntegrationBatch.Last():f4}");
                    UpdateChart(_inIntegrationBatch, _outIntegrationBatch, _inLPFBatch, _outLPFBatch);
                    //Clear buffer after update to UI
                    _inIntegrationBatch.Clear();
                    _outIntegrationBatch.Clear();
                    //_inLPFBatch.Clear();
                    //_outLPFBatch.Clear();
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
        private void OutChartHorizonTrackerScroll(object sender, EventArgs e)
        {
            HorizontalPoints = outChartHorizonTrack.Value;
        }
        private void OnChartMouseWheel(object sender, MouseEventArgs e)
        {
            Chart chart = (Chart)sender;
            if (e.Delta < 0)
            {
                double original = chart.ChartAreas[0].AxisY.Minimum;
                chart.ChartAreas[0].AxisY.Minimum = Math.Round(original * 1.25, 1);
                chart.ChartAreas[0].AxisY.Maximum = -chart.ChartAreas[0].AxisY.Minimum;
            }
            else if (e.Delta > 0)
            {
                double original = chart.ChartAreas[0].AxisY.Minimum;
                chart.ChartAreas[0].AxisY.Minimum = Math.Round(original / 1.25, 1);
                chart.ChartAreas[0].AxisY.Maximum = -chart.ChartAreas[0].AxisY.Minimum;
            }
        }
        private void UpdateBatchSizeChanged(object sender, EventArgs e)
        {
            UpdateBatchSize = (int)updateBatchSizeBox.Value;
            _inIntegrationBatch.Clear();
            _outIntegrationBatch.Clear();
        }

        //Cross-thread methods
        private void SetPrintLblText(string s, string t)
        {
            if (printInPhaseLbl.InvokeRequired)
                Invoke(_setPrintLbl, new object[] { s, t });
            else
                _setPrintLbl(s, t);
        }
        private void UpdateChart(List<float> iIntg, List<float> oIntg, List<float> iLpf, List<float> oLpf)
        {
            if (integralChart.InvokeRequired)
                Invoke(_updateOutChart, new object[] { iIntg, oIntg, iLpf, oLpf });
            else
                _updateOutChart(iIntg, oIntg, iLpf, oLpf);
        }

        private void ClearOutChartBtn_Click(object sender, EventArgs e)
        {
            integralChart.ChartAreas[0].AxisX.Minimum = 0;
            foreach (var serie in integralChart.Series)
            {
                serie.Points.Clear();
            }
        }

        //Helpers
        private void UpdateChartOnePoint(float i, float o, Chart chart)
        {
            var iSerie = chart.Series["In-phase"];
            iSerie.Points.AddY(i);
            //rolling X axis
            if (iSerie.Points.Count >= _horizonPoints)
                chart.ChartAreas[0].AxisX.Minimum = chart.ChartAreas[0].AxisX.Maximum - _horizonPoints;
            chart.Series["Out-of-phase"].Points.AddY(o);
            chart.Series["Modulus"].Points.AddY(Math.Sqrt(i * i + o * o));
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
