
namespace SerialDSP
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.portCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.refreshPortBtn = new System.Windows.Forms.Button();
            this.baudNumericBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.serialGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.readBufferNumericBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.windowLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.windowTrackbar = new System.Windows.Forms.TrackBar();
            this.beginBtn = new System.Windows.Forms.Button();
            this.printInPhaseLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.printOutPhaseLbl = new System.Windows.Forms.Label();
            this.integralChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.clearOutChartBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.mpsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fastBox = new System.Windows.Forms.CheckBox();
            this.aaBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.updateBatchSizeBox = new System.Windows.Forms.NumericUpDown();
            this.horizonPointsLbl = new System.Windows.Forms.Label();
            this.chartHorizonTrack = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.mpsLbl = new System.Windows.Forms.Label();
            this.printNormLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.printCvLbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.baudNumericBox)).BeginInit();
            this.serialGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readBufferNumericBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integralChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpsChart)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateBatchSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHorizonTrack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // portCombo
            // 
            this.portCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portCombo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portCombo.FormattingEnabled = true;
            this.portCombo.Location = new System.Drawing.Point(54, 31);
            this.portCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.portCombo.Name = "portCombo";
            this.portCombo.Size = new System.Drawing.Size(132, 27);
            this.portCombo.TabIndex = 0;
            this.portCombo.SelectedIndexChanged += new System.EventHandler(this.PortChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // refreshPortBtn
            // 
            this.refreshPortBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshPortBtn.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPortBtn.ForeColor = System.Drawing.Color.Black;
            this.refreshPortBtn.Location = new System.Drawing.Point(192, 32);
            this.refreshPortBtn.Name = "refreshPortBtn";
            this.refreshPortBtn.Size = new System.Drawing.Size(57, 27);
            this.refreshPortBtn.TabIndex = 2;
            this.refreshPortBtn.Text = "Refresh";
            this.refreshPortBtn.UseVisualStyleBackColor = true;
            this.refreshPortBtn.Click += new System.EventHandler(this.RefreshPortBtn_Click);
            // 
            // baudNumericBox
            // 
            this.baudNumericBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baudNumericBox.Location = new System.Drawing.Point(421, 34);
            this.baudNumericBox.Maximum = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            this.baudNumericBox.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.baudNumericBox.Name = "baudNumericBox";
            this.baudNumericBox.Size = new System.Drawing.Size(74, 24);
            this.baudNumericBox.TabIndex = 3;
            this.baudNumericBox.Value = new decimal(new int[] {
            38400,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(343, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "BaudRate";
            // 
            // serialGroupBox
            // 
            this.serialGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.serialGroupBox.Controls.Add(this.label3);
            this.serialGroupBox.Controls.Add(this.readBufferNumericBox);
            this.serialGroupBox.Controls.Add(this.refreshPortBtn);
            this.serialGroupBox.Controls.Add(this.portCombo);
            this.serialGroupBox.Controls.Add(this.label1);
            this.serialGroupBox.Controls.Add(this.label2);
            this.serialGroupBox.Controls.Add(this.baudNumericBox);
            this.serialGroupBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.serialGroupBox.Location = new System.Drawing.Point(17, 21);
            this.serialGroupBox.Name = "serialGroupBox";
            this.serialGroupBox.Size = new System.Drawing.Size(544, 137);
            this.serialGroupBox.TabIndex = 7;
            this.serialGroupBox.TabStop = false;
            this.serialGroupBox.Text = "Serial Communication Setup";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Read-buffer Size";
            // 
            // readBufferNumericBox
            // 
            this.readBufferNumericBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readBufferNumericBox.Location = new System.Drawing.Point(141, 84);
            this.readBufferNumericBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.readBufferNumericBox.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.readBufferNumericBox.Name = "readBufferNumericBox";
            this.readBufferNumericBox.Size = new System.Drawing.Size(74, 24);
            this.readBufferNumericBox.TabIndex = 5;
            this.readBufferNumericBox.Value = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.readBufferNumericBox.ValueChanged += new System.EventHandler(this.ReadBufferChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.groupBox2.Controls.Add(this.windowLbl);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.windowTrackbar);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBox2.Location = new System.Drawing.Point(17, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(544, 107);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DSP Setup";
            // 
            // windowLbl
            // 
            this.windowLbl.AutoSize = true;
            this.windowLbl.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowLbl.ForeColor = System.Drawing.Color.Black;
            this.windowLbl.Location = new System.Drawing.Point(214, 25);
            this.windowLbl.Name = "windowLbl";
            this.windowLbl.Size = new System.Drawing.Size(18, 19);
            this.windowLbl.TabIndex = 9;
            this.windowLbl.Text = "/";
            this.windowLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Integration Window Size";
            // 
            // windowTrackbar
            // 
            this.windowTrackbar.Location = new System.Drawing.Point(15, 49);
            this.windowTrackbar.Maximum = 8192;
            this.windowTrackbar.Minimum = 16;
            this.windowTrackbar.Name = "windowTrackbar";
            this.windowTrackbar.Size = new System.Drawing.Size(234, 45);
            this.windowTrackbar.TabIndex = 7;
            this.windowTrackbar.TickFrequency = 128;
            this.windowTrackbar.Value = 2048;
            this.windowTrackbar.Scroll += new System.EventHandler(this.WindowTrackScroll);
            // 
            // beginBtn
            // 
            this.beginBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
            this.beginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.beginBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.beginBtn.FlatAppearance.BorderSize = 3;
            this.beginBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.beginBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.beginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beginBtn.ForeColor = System.Drawing.Color.White;
            this.beginBtn.Location = new System.Drawing.Point(455, 100);
            this.beginBtn.Name = "beginBtn";
            this.beginBtn.Size = new System.Drawing.Size(71, 34);
            this.beginBtn.TabIndex = 0;
            this.beginBtn.Text = "Begin";
            this.beginBtn.UseVisualStyleBackColor = false;
            this.beginBtn.Click += new System.EventHandler(this.BeginProcessAsync);
            // 
            // printInPhaseLbl
            // 
            this.printInPhaseLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printInPhaseLbl.ForeColor = System.Drawing.Color.Black;
            this.printInPhaseLbl.Location = new System.Drawing.Point(148, 35);
            this.printInPhaseLbl.Name = "printInPhaseLbl";
            this.printInPhaseLbl.Size = new System.Drawing.Size(143, 22);
            this.printInPhaseLbl.TabIndex = 10;
            this.printInPhaseLbl.Text = "Pending";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "In-Phase Output";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 19);
            this.label6.TabIndex = 13;
            this.label6.Text = "Out-of-Phase Output";
            // 
            // printOutPhaseLbl
            // 
            this.printOutPhaseLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printOutPhaseLbl.ForeColor = System.Drawing.Color.Black;
            this.printOutPhaseLbl.Location = new System.Drawing.Point(148, 74);
            this.printOutPhaseLbl.Name = "printOutPhaseLbl";
            this.printOutPhaseLbl.Size = new System.Drawing.Size(143, 22);
            this.printOutPhaseLbl.TabIndex = 12;
            this.printOutPhaseLbl.Text = "Pending";
            // 
            // integralChart
            // 
            this.integralChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.integralChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.integralChart.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
            chartArea5.AxisX.LineColor = System.Drawing.Color.DimGray;
            chartArea5.AxisY.LineColor = System.Drawing.Color.DimGray;
            chartArea5.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DarkGray;
            chartArea5.Name = "ChartArea1";
            this.integralChart.ChartAreas.Add(chartArea5);
            this.integralChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.integralChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.integralChart.IsSoftShadows = false;
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend5.Name = "Legend1";
            this.integralChart.Legends.Add(legend5);
            this.integralChart.Location = new System.Drawing.Point(3, 3);
            this.integralChart.Name = "integralChart";
            this.integralChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series9.BorderWidth = 2;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series9.Color = System.Drawing.Color.Crimson;
            series9.LabelAngle = 3;
            series9.Legend = "Legend1";
            series9.MarkerColor = System.Drawing.Color.Blue;
            series9.Name = "In-phase";
            series9.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series10.BorderWidth = 2;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series10.Color = System.Drawing.Color.LightSeaGreen;
            series10.Legend = "Legend1";
            series10.MarkerColor = System.Drawing.Color.Red;
            series10.Name = "Out-of-phase";
            series10.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series11.Color = System.Drawing.Color.DarkMagenta;
            series11.Legend = "Legend1";
            series11.Name = "Modulus";
            this.integralChart.Series.Add(series9);
            this.integralChart.Series.Add(series10);
            this.integralChart.Series.Add(series11);
            this.integralChart.Size = new System.Drawing.Size(575, 627);
            this.integralChart.SuppressExceptions = true;
            this.integralChart.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(579, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 663);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.tabPage1.Controls.Add(this.clearOutChartBtn);
            this.tabPage1.Controls.Add(this.integralChart);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(581, 633);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Integral";
            // 
            // clearOutChartBtn
            // 
            this.clearOutChartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearOutChartBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearOutChartBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clearOutChartBtn.ForeColor = System.Drawing.Color.Black;
            this.clearOutChartBtn.Location = new System.Drawing.Point(513, 592);
            this.clearOutChartBtn.Name = "clearOutChartBtn";
            this.clearOutChartBtn.Size = new System.Drawing.Size(49, 24);
            this.clearOutChartBtn.TabIndex = 5;
            this.clearOutChartBtn.Text = "Clear";
            this.clearOutChartBtn.UseVisualStyleBackColor = true;
            this.clearOutChartBtn.Click += new System.EventHandler(this.ClearChartBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.mpsChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(581, 633);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // mpsChart
            // 
            this.mpsChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.mpsChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.mpsChart.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
            chartArea6.AxisX.LineColor = System.Drawing.Color.DimGray;
            chartArea6.AxisY.LineColor = System.Drawing.Color.DimGray;
            chartArea6.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DarkGray;
            chartArea6.Name = "ChartArea1";
            this.mpsChart.ChartAreas.Add(chartArea6);
            this.mpsChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mpsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend6.Name = "Legend1";
            this.mpsChart.Legends.Add(legend6);
            this.mpsChart.Location = new System.Drawing.Point(3, 3);
            this.mpsChart.Name = "mpsChart";
            this.mpsChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series12.Color = System.Drawing.Color.DodgerBlue;
            series12.Legend = "Legend1";
            series12.Name = "Mps";
            this.mpsChart.Series.Add(series12);
            this.mpsChart.Size = new System.Drawing.Size(575, 627);
            this.mpsChart.SuppressExceptions = true;
            this.mpsChart.TabIndex = 15;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.groupBox3.Controls.Add(this.fastBox);
            this.groupBox3.Controls.Add(this.aaBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.updateBatchSizeBox);
            this.groupBox3.Controls.Add(this.horizonPointsLbl);
            this.groupBox3.Controls.Add(this.chartHorizonTrack);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.beginBtn);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBox3.Location = new System.Drawing.Point(17, 318);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(544, 150);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Graph Setup";
            // 
            // fastBox
            // 
            this.fastBox.AutoSize = true;
            this.fastBox.Checked = true;
            this.fastBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fastBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fastBox.Location = new System.Drawing.Point(305, 90);
            this.fastBox.Name = "fastBox";
            this.fastBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fastBox.Size = new System.Drawing.Size(92, 23);
            this.fastBox.TabIndex = 12;
            this.fastBox.Text = "Fast-draw";
            this.fastBox.UseVisualStyleBackColor = true;
            this.fastBox.CheckedChanged += new System.EventHandler(this.FastChanged);
            // 
            // aaBox
            // 
            this.aaBox.AutoSize = true;
            this.aaBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aaBox.Location = new System.Drawing.Point(305, 49);
            this.aaBox.Name = "aaBox";
            this.aaBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.aaBox.Size = new System.Drawing.Size(110, 23);
            this.aaBox.TabIndex = 11;
            this.aaBox.Text = "Anti-aliasing";
            this.aaBox.UseVisualStyleBackColor = true;
            this.aaBox.CheckedChanged += new System.EventHandler(this.AAChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(20, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 19);
            this.label8.TabIndex = 8;
            this.label8.Text = "Update Batch Size";
            // 
            // updateBatchSizeBox
            // 
            this.updateBatchSizeBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBatchSizeBox.Location = new System.Drawing.Point(152, 100);
            this.updateBatchSizeBox.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.updateBatchSizeBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.updateBatchSizeBox.Name = "updateBatchSizeBox";
            this.updateBatchSizeBox.Size = new System.Drawing.Size(63, 24);
            this.updateBatchSizeBox.TabIndex = 7;
            this.updateBatchSizeBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.updateBatchSizeBox.ValueChanged += new System.EventHandler(this.UpdateBatchSizeChanged);
            // 
            // horizonPointsLbl
            // 
            this.horizonPointsLbl.AutoSize = true;
            this.horizonPointsLbl.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horizonPointsLbl.ForeColor = System.Drawing.Color.Black;
            this.horizonPointsLbl.Location = new System.Drawing.Point(215, 33);
            this.horizonPointsLbl.Name = "horizonPointsLbl";
            this.horizonPointsLbl.Size = new System.Drawing.Size(16, 17);
            this.horizonPointsLbl.TabIndex = 10;
            this.horizonPointsLbl.Text = "/";
            this.horizonPointsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chartHorizonTrack
            // 
            this.chartHorizonTrack.Location = new System.Drawing.Point(15, 49);
            this.chartHorizonTrack.Maximum = 4096;
            this.chartHorizonTrack.Minimum = 16;
            this.chartHorizonTrack.Name = "chartHorizonTrack";
            this.chartHorizonTrack.Size = new System.Drawing.Size(234, 45);
            this.chartHorizonTrack.TabIndex = 7;
            this.chartHorizonTrack.TickFrequency = 128;
            this.chartHorizonTrack.Value = 2048;
            this.chartHorizonTrack.Scroll += new System.EventHandler(this.ChartHorizonTrackerScroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(20, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 19);
            this.label9.TabIndex = 6;
            this.label9.Text = "Horizontal Points";
            // 
            // mpsLbl
            // 
            this.mpsLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpsLbl.ForeColor = System.Drawing.Color.Black;
            this.mpsLbl.Location = new System.Drawing.Point(148, 111);
            this.mpsLbl.Name = "mpsLbl";
            this.mpsLbl.Size = new System.Drawing.Size(143, 22);
            this.mpsLbl.TabIndex = 16;
            this.mpsLbl.Text = "Pending";
            // 
            // printNormLbl
            // 
            this.printNormLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printNormLbl.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printNormLbl.Location = new System.Drawing.Point(348, 528);
            this.printNormLbl.Name = "printNormLbl";
            this.printNormLbl.Size = new System.Drawing.Size(150, 39);
            this.printNormLbl.TabIndex = 17;
            this.printNormLbl.Text = "Pending";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.label10.Location = new System.Drawing.Point(351, 504);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 19);
            this.label10.TabIndex = 18;
            this.label10.Text = "Output";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 19);
            this.label7.TabIndex = 19;
            this.label7.Text = "Processing Speed";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.label11.Location = new System.Drawing.Point(351, 602);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "Standard Deviation";
            // 
            // printCvLbl
            // 
            this.printCvLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printCvLbl.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printCvLbl.Location = new System.Drawing.Point(351, 627);
            this.printCvLbl.Name = "printCvLbl";
            this.printCvLbl.Size = new System.Drawing.Size(97, 30);
            this.printCvLbl.TabIndex = 20;
            this.printCvLbl.Text = "Pending";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.groupBox1.Controls.Add(this.printInPhaseLbl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.printOutPhaseLbl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.mpsLbl);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBox1.Location = new System.Drawing.Point(17, 493);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 169);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1169, 681);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.printCvLbl);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.printNormLbl);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.serialGroupBox);
            this.Font = new System.Drawing.Font("Calibri", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(596, 700);
            this.Name = "MainForm";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialDSP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClose);
            ((System.ComponentModel.ISupportInitialize)(this.baudNumericBox)).EndInit();
            this.serialGroupBox.ResumeLayout(false);
            this.serialGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readBufferNumericBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integralChart)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mpsChart)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateBatchSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHorizonTrack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox portCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshPortBtn;
        private System.Windows.Forms.NumericUpDown baudNumericBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox serialGroupBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button beginBtn;
        private System.Windows.Forms.Label printInPhaseLbl;
        private System.Windows.Forms.TrackBar windowTrackbar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label windowLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label printOutPhaseLbl;
        private System.Windows.Forms.DataVisualization.Charting.Chart integralChart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button clearOutChartBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar chartHorizonTrack;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label horizonPointsLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown readBufferNumericBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown updateBatchSizeBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart mpsChart;
        private System.Windows.Forms.CheckBox aaBox;
        private System.Windows.Forms.Label mpsLbl;
        private System.Windows.Forms.CheckBox fastBox;
        private System.Windows.Forms.Label printNormLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label printCvLbl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

