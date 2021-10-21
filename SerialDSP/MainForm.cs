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
        private readonly Axis _intgY, _intgX, _stdvX, _stdvY;
        private readonly Series _intgInSeries;
        private readonly Series _intgOutSeries;
        private readonly Series _intgNormSeries;
        private readonly Series _stdvSeries;

        private bool _hasBegin = false;
        //Chart scaling
        private int _horizonPoints;
        //Chart update batches
        private readonly object _batchLocker = new object();
        private readonly List<double> _inBatch = new List<double>();
        private readonly List<double> _outBatch = new List<double>();
        private readonly List<double> _normBatch = new List<double>();
        private readonly List<double> _stdvBatch = new List<double>();
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
            _stdvX = stdvChart.ChartAreas[0].AxisX;
            _stdvY = stdvChart.ChartAreas[0].AxisY;
            _intgInSeries = integralChart.Series["In-phase"];
            _intgOutSeries = integralChart.Series["Out-of-phase"];
            _intgNormSeries = integralChart.Series["Norm"];
            _stdvSeries = stdvChart.Series["Standard deviation"];
            //Serial port setup
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.Parity = Parity.None;
            //_port.ReadTimeout = 3000;
            _port.DiscardNull = true;
            LoadAvailablePorts();

            //manually-bind event handler
            integralChart.MouseWheel += OnIntgChartMouseWheel;
            stdvChart.MouseWheel += OnIntgChartMouseWheel;
            //chart settings
            _intgX.Interval = 0;
            _stdvY.IsStartedFromZero = true;

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
        private void LoadAvailablePorts()
        {
            portCombo.Items.Clear();
            portCombo.Items.AddRange(SerialPort.GetPortNames());
            if (portCombo.Items.Count != 0)
                portCombo.SelectedIndex = 0;
            else
                MessageBox.Show("No available serial port detected. Plug in Arduino and refresh again. ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            LoadAvailablePorts();
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
                if (portCombo.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a serial port before continue. ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //style1: inter
                beginBtn.Enabled = serialGroupBox.Enabled = false;
                _hasBegin = true;
                //load setup
                _port.BaudRate = (int)baudNumericBox.Value;
                _port.ReadBufferSize = (int)readBufferNumericBox.Value;
                //open
                _port.Open();
                _port.DiscardInBuffer();
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
            if (_count == 197)
            {
                _sw.Stop();
                UpdateMps(197_000f / _sw.ElapsedMilliseconds);
                UpdatePrintLbl(_integration.AverageInPhase, _integration.AverageOutOfPhase, _integration.AverageNorm, _integration.StandardDeviationNorm);
                _sw.Reset();
                _count = 0;
                _sw.Start();
            }
            //integration & average
            //drop any broken data
            if (!int.TryParse(data[0], out int inIntegral_i))
                return;
            if (!int.TryParse(data[1], out int outIntegral_i))
                return;
            float inIntegral = inIntegral_i;
            float outIntegral = outIntegral_i;
            _integration.Roll(inIntegral, outIntegral);
            //Save new values to buffers
            _inBatch.Add(_integration.AverageInPhase);
            _outBatch.Add(_integration.AverageOutOfPhase);
            _normBatch.Add(_integration.AverageNorm);
            _stdvBatch.Add(_integration.StandardDeviationNorm);
            //When it's enough for an update, do it and clear the buffer
            if (_inBatch.Count >= UpdateBatchSize)
            {
                UpdateChart(_inBatch, _outBatch, _normBatch, _stdvBatch);
                //Clear buffer after update to UI
                _inBatch.Clear();
                _outBatch.Clear();
                _normBatch.Clear();
                _stdvBatch.Clear();
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
            lock (_batchLocker)
            {
                _inBatch.Clear();
                _outBatch.Clear();
                _normBatch.Clear();
                _inBatch.Add(0);
                _outBatch.Add(0);
                _normBatch.Add(0);
            }
        }
        private void AAChanged(object sender, EventArgs e)
        {
            if (aaBox.Checked)
            {
                integralChart.AntiAliasing = AntiAliasingStyles.All;
                stdvChart.AntiAliasing = AntiAliasingStyles.All;
            }
            else
            {
                integralChart.AntiAliasing = AntiAliasingStyles.Text;
                stdvChart.AntiAliasing = AntiAliasingStyles.Text;
            }
        }
        private void FastChanged(object sender, EventArgs e)
        {
            var type = fastBox.Checked ? SeriesChartType.FastLine : SeriesChartType.Line;
            foreach (var series in integralChart.Series)
            {
                series.ChartType = type;
            }
            foreach (var series in stdvChart.Series)
            {
                series.ChartType = type;
            }
        }

        //Outputs
        private void UpdatePrintLbl(double i, double o, double m, double stdv)
        {
            printInPhaseLbl.Text = i.ToString("f4");
            printOutPhaseLbl.Text = o.ToString("f4");
            printNormLbl.Text = m.ToString("f4");
            printStdvLbl.Text = stdv.ToString("f4");
        }
        private void UpdateMps(double mps)
        {
            _sb.AppendFormat("{0:f4} Mps", mps);
            mpsLbl.Text = _sb.ToString();
            _sb.Clear();
        }
        private void UpdateChart(List<double> iIntgs, List<double> oIntgs, List<double> nIntgs, List<double> stdvs)
        {
            lock (_batchLocker)
            {
                for (int i = 0; i < iIntgs.Count; i++)
                {
                    double iIntg = iIntgs[i], oIntg = oIntgs[i], nIntg = nIntgs[i], stdv = stdvs[i];
                    _intgInSeries.Points.AddY(iIntg);
                    _intgOutSeries.Points.AddY(oIntg);
                    _intgNormSeries.Points.AddY(nIntg);
                    _stdvSeries.Points.AddY(stdv);
                    _intgInSeries.Points.RemoveAt(0);
                    _intgOutSeries.Points.RemoveAt(0);
                    _intgNormSeries.Points.RemoveAt(0);
                    _stdvSeries.Points.RemoveAt(0);
                }
                //rolling X axis
                if (_intgInSeries.Points.Count > _horizonPoints)
                    _intgX.Minimum = _intgX.Maximum - _horizonPoints;
                if (_stdvSeries.Points.Count >= _horizonPoints)
                    _stdvX.Minimum = _stdvX.Maximum - _horizonPoints;
            }
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
            _stdvSeries.Points.Clear();
            foreach (var series in integralChart.Series)
                series.Points.Clear();
            for (int i = 0; i < chartHorizonTrack.Maximum; i++)
            {
                _stdvSeries.Points.AddY(0);
                foreach (var series in integralChart.Series)
                    series.Points.AddY(0);
            }
        }
    }
}
