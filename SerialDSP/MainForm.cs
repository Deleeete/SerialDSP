using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
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
        private readonly Action<float, float> _updateOutChart;
        private bool _hasBegin = false;
        private float _integrationIn = 0, _integrationOut = 0;
        private int _integrateWindow = 16;
        private int _horizonPoints, _verticalPoints;
        private readonly Queue<float> _bufferIn = new Queue<float>();
        private readonly Queue<float> _bufferOut = new Queue<float>();

        public int IntegrationWindow 
        {
            get => _integrateWindow;
            private set 
            {
                _integrateWindow = value;
                //add zeros if expansion required 
                while (_bufferIn.Count < _integrateWindow)
                {
                    _bufferIn.Enqueue(0);
                    _bufferOut.Enqueue(0);
                }
                //remove heads if shrinking required
                while (_bufferOut.Count < _integrateWindow)
                {
                    _bufferIn.Dequeue();
                    _bufferOut.Dequeue();
                }
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

        public MainForm()
        {
            //Generate.Sinusoidal(250, 44100, 350, 20);
            InitializeComponent();
            LoadAvailablePorts(false);
            _setPrintLbl = (s, t) => { printInPhaseLbl.Text = s; printOutPhaseLbl.Text = t; };
            var axisx = outChart.ChartAreas[0].AxisX;
            axisx.MajorGrid.LineColor = Color.FromArgb(41, 111, 112);
            _updateOutChart = (ip, op) => 
            {
                var inps = outChart.Series["In-phase"];
                inps.Points.AddY(ip);
                //outChart.ChartAreas[0].CursorX.IsUserEnabled = true; ;
                if (inps.Points.Count >= _horizonPoints)
                    outChart.ChartAreas[0].AxisX.Minimum = outChart.ChartAreas[0].AxisX.Maximum - _horizonPoints;
                outChart.Series["Out-of-phase"].Points.AddY(op);
                outChart.Series["Modulus"].Points.AddY(Math.Sqrt(ip*ip + op*op));
            };
            IntegrationWindow = windowTrackbar.Value;
            HorizontalPoints = outChartHorizonTrack.Value;
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
                    float inPhase = BitConverter.ToSingle(BitConverter.GetBytes(int.Parse(groups[1].Value)), 0);
                    float outPhase = BitConverter.ToSingle(BitConverter.GetBytes(int.Parse(groups[2].Value)), 0);
                    _integrationIn = _integrationIn + inPhase - _bufferIn.Dequeue();
                    _integrationOut = _integrationOut + outPhase - _bufferOut.Dequeue();
                    float meanIn = _integrationIn / IntegrationWindow;
                    float meanOut = _integrationOut / IntegrationWindow;
                    SetPrintLblText($"{meanIn:f4}", $"{meanOut:f4}");
                    UpdateChart(_integrationIn, _integrationOut);
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
            lock (_port)  //lock port from being used by IO thread
            {
                //reset integration vars
                _integrationIn = _integrationOut = 0;
                if (_hasBegin) //action of stop
                {
                    try
                    {
                        //clear buffer before end
                        _port.DiscardInBuffer();
                        _port.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to close port {_port.PortName}: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    beginBtn.Text = "Start";
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
                    _bufferIn.Clear();
                    _bufferOut.Clear();
                    beginBtn.Text = "End";
                    beginBtn.BackColor = Color.FromArgb(255, 109, 54);
                    beginBtn.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
                    beginBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(215, 172, 106);
                }
                serialGroupBox.Enabled = _hasBegin;
                _hasBegin = !_hasBegin;
            }
        }

        //DSP
        private void WindowTrackScroll(object sender, EventArgs e)
        {
            IntegrationWindow = windowTrackbar.Value;
            //printInPhaseLbl.Text = printOutPhaseLbl.Text = "Pending...";
        }

        //Graphic
        private void UpdateChart(float ip, float op)
        {
            if (outChart.InvokeRequired)
                Invoke(_updateOutChart, new object[] { ip, op });
            else
                _updateOutChart(ip, op);
        }

        //Cross-thread methods
        private void SetPrintLblText(string s, string t)
        {
            if (printInPhaseLbl.InvokeRequired)
                Invoke(_setPrintLbl, new object[] { s, t });
            else
                _setPrintLbl(s, t);
        }
        private void ClearOutChartBtn_Click(object sender, EventArgs e)
        {
            outChart.ChartAreas[0].AxisX.Minimum = 0;
            foreach (var serie in outChart.Series)
            {
                serie.Points.Clear();
            }
        }

        //Chart scale
        private void OutChartVerticalScroll(object sender, EventArgs e)
        {
            VerticalPoints = outChartVerticalTrack.Value;
        }
        private void OutChartHorizonTrackerScroll(object sender, EventArgs e)
        {
            HorizontalPoints = outChartHorizonTrack.Value;
        }

    }
}
