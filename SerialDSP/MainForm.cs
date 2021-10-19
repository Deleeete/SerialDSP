using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialDSP
{
    public partial class MainForm : Form
    {
        //diagnostic data
        private readonly Stopwatch _sw = new Stopwatch();
        private volatile int _count = 0;
        private volatile bool _needClose = false;
        //pre-saved vars
        private readonly SerialPort _port = new SerialPort();
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly Axis _intgY, _intgX, _mpsX, _mpsY;
        private readonly Series _intgInSeries;
        private readonly Series _intgOutSeries;
        private readonly Series _intgModulusSeries;
        private readonly Series _mpsSeries;

        private bool _hasBegin = false;
        //Chart scaling
        private int _horizonPoints;
        //Chart update batches
        private readonly List<double> _inIntegrationBatch = new List<double>();
        private readonly List<double> _outIntegrationBatch = new List<double>();
        //Integration wrapper
        private readonly Integration _integration = new Integration();
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
            //Serial port setup
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            //_port.ReadTimeout = 3000;
            _port.DiscardNull = true;
            LoadAvailablePorts(false);

            //manually-bind event handler
            integralChart.MouseWheel += OnIntgChartMouseWheel;
            _intgX.Interval = 0;

            //Load setup from UI
            WindowTrackScroll(null, null);    //fire windowTrackbar ValueChanged event manually
            HorizontalPoints = chartHorizonTrack.Value;
            UpdateBatchSize = (int)updateBatchSizeBox.Value;

            //init charts with zeros
            InitCharts();
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
                BeginProcessAsync(null, null);
            }
            int value = (int)readBufferNumericBox.Value;
            _port.ReadBufferSize = value;
        }
        private void RefreshPortBtn_Click(object sender, EventArgs e)
        {
            LoadAvailablePorts(false);
        }
        private void PortChanged(object sender, EventArgs e)
        {
            if (_hasBegin)
            {
                BeginProcessAsync(null, null);
            }
            _port.PortName = portCombo.SelectedItem.ToString();
        }

        //General
        private async void BeginProcessAsync(object sender, EventArgs e)
        {
            if (!_hasBegin) //start
            {
                //style1: inter
                beginBtn.Enabled = serialGroupBox.Enabled = false;
                _hasBegin = true;
                //load setup
                _port.BaudRate = (int)baudNumericBox.Value;
                _port.ReadBufferSize = (int)readBufferNumericBox.Value;
                //open
                _port.Open();
                _port.DiscardInBuffer();
                //consume first line and do nothing, because the data should be broken if the port open at the middle of an message (very-likely)
                await _port.ReadLineAsync();
                _integration.Reset();
                //style2: end
                beginBtn.Text = "End";
                beginBtn.BackColor = Color.FromArgb(255, 109, 54);
                beginBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
                beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(215, 172, 106);
                beginBtn.Enabled = true;
                //async call
                while (!_needClose)
                {
                    string s = (await _port.ReadLineAsync());
                    s.InPlaceSingleSplit(',', _splitBuffer);
                    ProcessData(_splitBuffer);
                }
                //_needClose = true here. Close port and reset signal
                _port.Close();
                _needClose = false;
            }
            else  //stop
            {
                beginBtn.Enabled = false;
                serialGroupBox.Enabled = true;
                _hasBegin = false;
                mpsLbl.Text = "Closing port...";
                _needClose = true;
                beginBtn.Text = "Begin";
                beginBtn.BackColor = Color.FromArgb(27, 161, 226);
                beginBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
                beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(122, 193, 255);
                mpsLbl.Text = "Pending";
                beginBtn.Enabled = true;
            }
        }

        //DSP
        private void WindowTrackScroll(object sender, EventArgs e)
        {
            _integration.WindowSize = windowTrackbar.Value;
            windowLbl.Text = windowTrackbar.Value.ToString();
        }
        private void ProcessData(string[] data)
        {
            _sw.Start();
            _count++;
            if (_count == 200)
            {
                _sw.Stop();
                UpdateMps(200_000f / _sw.ElapsedMilliseconds);
                UpdatePrintLbl(_inIntegrationBatch.Last(), _integration.AverageOutOfPhase, _integration.AverageNorm, _integration.StandardDeviationNorm);
                _sw.Reset();
                _count = 0;
                _sw.Start();
            }
            //integration & average
            float inIntegral = int.Parse(data[0]);
            float outIntegral = int.Parse(data[1]);
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
            _inIntegrationBatch.Add(0);
            _outIntegrationBatch.Add(0);
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

        //Outputs
        private void UpdatePrintLbl(double i, double o, double m, double cv)
        {
            printInPhaseLbl.Text = i.ToString("f4");
            printOutPhaseLbl.Text = o.ToString("f4");
            printNormLbl.Text = m.ToString("f4");
            printCvLbl.Text = cv.ToString("f4");
        }
        private void UpdateMps(double mps)
        {
            _sb.AppendFormat("{0:f4} Mps", mps);
            mpsLbl.Text = _sb.ToString();
            _sb.Clear();
            _mpsSeries.Points.AddY(mps);
            _mpsSeries.Points.RemoveAt(0);
            //rolling X axis
            if (_mpsSeries.Points.Count >= _horizonPoints)
                _mpsX.Minimum = _mpsX.Maximum - _horizonPoints;
        }
        private void UpdateChart(List<double> iIntgs, List<double> oIntgs)
        {
            for (int i = 0; i < iIntgs.Count; i++)
            {
                double iIntg = iIntgs[i], oIntg = oIntgs[i];
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
        }

        //Other events
        private void OnClose(object sender, FormClosedEventArgs e)
        {
            if (_port.IsOpen)
                _port.Close();
            _port.Dispose();
            Environment.Exit(0);
        }
        private void ClearChartBtn_Click(object sender, EventArgs e)
        {
            _intgX.Minimum = 0;
            foreach (var series in integralChart.Series)
                series.Points.Clear();
            for (int i = 0; i < chartHorizonTrack.Maximum; i++)
            {
                foreach (var series in integralChart.Series)
                    series.Points.AddY(0);
            }
        }

        //Helpers
        private void InitCharts()
        {
            _mpsSeries.Points.Clear();
            foreach (var series in integralChart.Series)
                series.Points.Clear();
            for (int i = 0; i < chartHorizonTrack.Maximum; i++)
            {
                _mpsSeries.Points.AddY(0);
                foreach (var series in integralChart.Series)
                    series.Points.AddY(0);
            }
        }
    }
}
