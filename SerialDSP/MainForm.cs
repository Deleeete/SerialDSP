using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
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
        private int _integrateWindow = 16;
        private int _horizonPoints, _verticalPoints;
        private readonly Integration _integration = new Integration();
        private readonly List<float> _inBuffer = new List<float>();
        private readonly List<float> _outBuffer = new List<float>();

        public int IntegrationWindow 
        {
            get => _integrateWindow;
            private set 
            {
                _integrateWindow = value;
                _integration.WindowSize = value;
                windowLbl.Text = _integrateWindow.ToString();
            } 
        }
        public int HorizontalPoints
        {
            get => _horizonPoints;
            set 
            {
                _horizonPoints = value;
                horizonPointsLbl.Text = _horizonPoints.ToString();
            }
        }
        public int VerticalPoints
        {
            get => _verticalPoints;
            set
            {
                _verticalPoints = value;
                verticalPointsLbl.Text = _verticalPoints.ToString();
                int half = _verticalPoints / 2;
                outChart.ChartAreas[0].AxisY.Minimum = -half;
                outChart.ChartAreas[0].AxisY.Maximum = half;
            }
        }
        public int UpdateBatchSize { get; set; }

        public MainForm()
        {
            InitializeComponent();
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
                //Clear buffer after update to UI
                ins.Clear();
                outs.Clear();
            };
            //Load setup from UI
            IntegrationWindow = windowTrackbar.Value;
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
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string s = _port.ReadLine().Trim();
                if (_dataPat.IsMatch(s))
                {
                    var groups = _dataPat.Match(s).Groups;
                    float inPhase = int.Parse(groups[1].Value);
                    float outOfPhase = int.Parse(groups[2].Value);
                    _integration.Roll(inPhase, outOfPhase);
                    //Save new values to buffers
                    _inBuffer.Add(_integration.AverageInPhase);
                    _outBuffer.Add(_integration.AverageOutOfPhase);
                    //When it's enough for an update, do it and clear the buffer
                    if (_inBuffer.Count >= UpdateBatchSize)
                    {
                        SetPrintLblText($"{_inBuffer.Last():f4}", $"{_outBuffer.Last():f4}");
                        UpdateChart(_inBuffer, _outBuffer);
                    }
                    //Close port in handler and hence the IO thread so that it can be close before any other envents are fired
                    if (_needClose)
                    {
                        try
                        {
                            _port.Close();
                            _needClose = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Failed to close port {_port.PortName}: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is InvalidOperationException)    //Supress IOException and InvalidOperationException
                    return;
                else
                    MessageBox.Show($"Failed to read data from port {_port.PortName}: {ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (_hasBegin) //action of stop
            {
                _needClose = true;
                beginBtn.Text = "Begin";
                beginBtn.BackColor = Color.FromArgb(27, 161, 226);
                beginBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 122, 204);
                beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(122, 193, 255);
            }
            else
            {
                try
                {
                    if (_port.IsOpen)
                        _port.Close();
                    _port.BaudRate = (int)baudNumericBox.Value;
                    _port.ReadBufferSize = (int)readBufferNumericBox.Value;
                    _port.DataReceived += DataReceived;
                    _port.Open();
                    //clear buffer at each start
                    _port.DiscardInBuffer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fatal error: Failed to open port {_port.PortName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _port.Close();
                    return;
                }
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
        private void OutChartVerticalScroll(object sender, EventArgs e)
        {
            VerticalPoints = outChartVerticalTrack.Value;
        }
        private void OutChartHorizonTrackerScroll(object sender, EventArgs e)
        {
            HorizontalPoints = outChartHorizonTrack.Value;
        }
        private void UpdateBatchSizeChanged(object sender, EventArgs e)
        {
            UpdateBatchSize = (int)updateBatchSizeBox.Value;
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
    }
}
