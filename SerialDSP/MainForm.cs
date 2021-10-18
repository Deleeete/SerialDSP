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
        private readonly OnlineFilter lpf = OnlineFilter.CreateLowpass(ImpulseResponse.Finite, 250, 25);
        //delegate for other threads to invoke
        private readonly Action<string, string> _setPrintLbl;
        private readonly Action<List<float>, List<float>> _updateOutChart;
        private bool _hasBegin = false;
        //Signals to control IO event handler
        private volatile bool _needClose = false;
        //Chart scaling
        private int _horizonPoints;
        //Chart update batches
        private readonly List<float> _inDataBatch = new List<float>();
        private readonly List<float> _outDataBatch = new List<float>();
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
            //manually-bind event handler
            outChart.MouseWheel += OutChartMouseWheel;
            LoadAvailablePorts(false);
            _setPrintLbl = (s, t) => { printInPhaseLbl.Text = s; printOutPhaseLbl.Text = t; };
            var axisx = outChart.ChartAreas[0].AxisX;
            axisx.MajorGrid.LineColor = Color.FromArgb(41, 111, 112);
            _updateOutChart = (ins, outs) =>
            {
                for (int i = 0; i < ins.Count; i++)
                {
                    float inValue = ins[i];
                    float outValue = outs[i];
                    var inps = outChart.Series["In-phase"];
                    inps.Points.AddY(inValue);
                    //rolling X axis
                    if (inps.Points.Count >= _horizonPoints)
                        outChart.ChartAreas[0].AxisX.Minimum = outChart.ChartAreas[0].AxisX.Maximum - _horizonPoints;
                    outChart.Series["Out-of-phase"].Points.AddY(outValue);
                    outChart.Series["Modulus"].Points.AddY(Math.Sqrt(inValue * inValue + outValue * outValue));
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
                float inPhase = int.Parse(_splitBuffer[0]);
                float outOfPhase = int.Parse(_splitBuffer[1]);
                _integration.Roll(inPhase, outOfPhase);
                //Save new values to buffers
                _inDataBatch.Add(_integration.AverageInPhase);
                _outDataBatch.Add(_integration.AverageOutOfPhase);
                //When it's enough for an update, do it and clear the buffer
                if (_inDataBatch.Count >= UpdateBatchSize)
                {
                    SetPrintLblText($"{_inDataBatch.Last():f4}", $"{_outDataBatch.Last():f4}");
                    UpdateChart(_inDataBatch, _outDataBatch);
                    //Clear buffer after update to UI
                    _inDataBatch.Clear();
                    _outDataBatch.Clear();
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
        private void OutChartMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                double original = outChart.ChartAreas[0].AxisY.Minimum;
                outChart.ChartAreas[0].AxisY.Minimum = Math.Round(original * 1.25, 1);
                outChart.ChartAreas[0].AxisY.Maximum = -outChart.ChartAreas[0].AxisY.Minimum;
            }
            else if (e.Delta > 0)
            {
                double original = outChart.ChartAreas[0].AxisY.Minimum;
                outChart.ChartAreas[0].AxisY.Minimum = Math.Round(original / 1.25, 1);
                outChart.ChartAreas[0].AxisY.Maximum = -outChart.ChartAreas[0].AxisY.Minimum;
            }
        }
        private void UpdateBatchSizeChanged(object sender, EventArgs e)
        {
            UpdateBatchSize = (int)updateBatchSizeBox.Value;
            _inDataBatch.Clear();
            _outDataBatch.Clear();
        }

        //Cross-thread methods
        private void SetPrintLblText(string s, string t)
        {
            if (printInPhaseLbl.InvokeRequired)
                Invoke(_setPrintLbl, new object[] { s, t });
            else
                _setPrintLbl(s, t);
        }
        private void UpdateChart(List<float> ip, List<float> op)
        {
            if (outChart.InvokeRequired)
                Invoke(_updateOutChart, new object[] { ip, op });
            else
                _updateOutChart(ip, op);
        }

        private void ClearOutChartBtn_Click(object sender, EventArgs e)
        {
            outChart.ChartAreas[0].AxisX.Minimum = 0;
            foreach (var serie in outChart.Series)
            {
                serie.Points.Clear();
            }
        }

        private bool ParseRegex(string s, out float inPhase, out float outOfPhase)
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
