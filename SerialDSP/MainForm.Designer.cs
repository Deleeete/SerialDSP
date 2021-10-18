
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.SuspendLayout();
            // 
            // portCombo
            // 
            this.portCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portCombo.FormattingEnabled = true;
            this.portCombo.Location = new System.Drawing.Point(54, 31);
            this.portCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.portCombo.Name = "portCombo";
            this.portCombo.Size = new System.Drawing.Size(132, 25);
            this.portCombo.TabIndex = 0;
            this.portCombo.SelectedIndexChanged += new System.EventHandler(this.PortChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // refreshPortBtn
            // 
            this.refreshPortBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshPortBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.refreshPortBtn.ForeColor = System.Drawing.Color.Black;
            this.refreshPortBtn.Location = new System.Drawing.Point(192, 32);
            this.refreshPortBtn.Name = "refreshPortBtn";
            this.refreshPortBtn.Size = new System.Drawing.Size(61, 24);
            this.refreshPortBtn.TabIndex = 2;
            this.refreshPortBtn.Text = "Refresh";
            this.refreshPortBtn.UseVisualStyleBackColor = true;
            this.refreshPortBtn.Click += new System.EventHandler(this.RefreshPortBtn_Click);
            // 
            // baudNumericBox
            // 
            this.baudNumericBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baudNumericBox.Location = new System.Drawing.Point(409, 35);
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
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(339, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
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
            this.serialGroupBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serialGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.serialGroupBox.Location = new System.Drawing.Point(17, 12);
            this.serialGroupBox.Name = "serialGroupBox";
            this.serialGroupBox.Size = new System.Drawing.Size(544, 137);
            this.serialGroupBox.TabIndex = 7;
            this.serialGroupBox.TabStop = false;
            this.serialGroupBox.Text = "Serial Communication Setup";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(21, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Read-buffer Size";
            // 
            // readBufferNumericBox
            // 
            this.readBufferNumericBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readBufferNumericBox.Location = new System.Drawing.Point(134, 82);
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
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBox2.Location = new System.Drawing.Point(17, 176);
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
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Integration Window";
            // 
            // windowTrackbar
            // 
            this.windowTrackbar.Location = new System.Drawing.Point(15, 47);
            this.windowTrackbar.Maximum = 8192;
            this.windowTrackbar.Minimum = 16;
            this.windowTrackbar.Name = "windowTrackbar";
            this.windowTrackbar.Size = new System.Drawing.Size(238, 45);
            this.windowTrackbar.TabIndex = 7;
            this.windowTrackbar.TickFrequency = 128;
            this.windowTrackbar.Value = 1024;
            this.windowTrackbar.Scroll += new System.EventHandler(this.WindowTrackScroll);
            // 
            // beginBtn
            // 
            this.beginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.beginBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
            this.beginBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.beginBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.beginBtn.FlatAppearance.BorderSize = 3;
            this.beginBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.beginBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.beginBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beginBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.beginBtn.Location = new System.Drawing.Point(469, 484);
            this.beginBtn.Name = "beginBtn";
            this.beginBtn.Size = new System.Drawing.Size(92, 46);
            this.beginBtn.TabIndex = 9;
            this.beginBtn.Text = "Begin";
            this.beginBtn.UseVisualStyleBackColor = false;
            this.beginBtn.Click += new System.EventHandler(this.BeginProcess);
            // 
            // printInPhaseLbl
            // 
            this.printInPhaseLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printInPhaseLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printInPhaseLbl.Location = new System.Drawing.Point(167, 470);
            this.printInPhaseLbl.Name = "printInPhaseLbl";
            this.printInPhaseLbl.Size = new System.Drawing.Size(261, 22);
            this.printInPhaseLbl.TabIndex = 10;
            this.printInPhaseLbl.Text = "/";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 475);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "In-Phase Output";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 513);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Out-of-Phase Output";
            // 
            // printOutPhaseLbl
            // 
            this.printOutPhaseLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.printOutPhaseLbl.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printOutPhaseLbl.Location = new System.Drawing.Point(167, 509);
            this.printOutPhaseLbl.Name = "printOutPhaseLbl";
            this.printOutPhaseLbl.Size = new System.Drawing.Size(261, 22);
            this.printOutPhaseLbl.TabIndex = 12;
            this.printOutPhaseLbl.Text = "/";
            // 
            // integralChart
            // 
            this.integralChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.integralChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.integralChart.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
            chartArea3.AxisX.LineColor = System.Drawing.Color.DimGray;
            chartArea3.AxisY.LineColor = System.Drawing.Color.DimGray;
            chartArea3.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.Name = "ChartArea1";
            this.integralChart.ChartAreas.Add(chartArea3);
            this.integralChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.integralChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.integralChart.IsSoftShadows = false;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.Name = "Legend1";
            this.integralChart.Legends.Add(legend3);
            this.integralChart.Location = new System.Drawing.Point(3, 3);
            this.integralChart.Name = "integralChart";
            this.integralChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Color = System.Drawing.Color.Crimson;
            series5.LabelAngle = 3;
            series5.Legend = "Legend1";
            series5.MarkerColor = System.Drawing.Color.Blue;
            series5.Name = "In-phase";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Color = System.Drawing.Color.LightSeaGreen;
            series6.Legend = "Legend1";
            series6.MarkerColor = System.Drawing.Color.Red;
            series6.Name = "Out-of-phase";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.Color = System.Drawing.Color.DarkMagenta;
            series7.Legend = "Legend1";
            series7.Name = "Modulus";
            this.integralChart.Series.Add(series5);
            this.integralChart.Series.Add(series6);
            this.integralChart.Series.Add(series7);
            this.integralChart.Size = new System.Drawing.Size(575, 523);
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
            this.tabControl1.Location = new System.Drawing.Point(580, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(589, 559);
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
            this.tabPage1.Size = new System.Drawing.Size(581, 529);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Integral";
            // 
            // clearOutChartBtn
            // 
            this.clearOutChartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearOutChartBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearOutChartBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clearOutChartBtn.ForeColor = System.Drawing.Color.Black;
            this.clearOutChartBtn.Location = new System.Drawing.Point(513, 488);
            this.clearOutChartBtn.Name = "clearOutChartBtn";
            this.clearOutChartBtn.Size = new System.Drawing.Size(49, 24);
            this.clearOutChartBtn.TabIndex = 5;
            this.clearOutChartBtn.Text = "Clear";
            this.clearOutChartBtn.UseVisualStyleBackColor = true;
            this.clearOutChartBtn.Click += new System.EventHandler(this.ClearOutChartBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.mpsChart);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(581, 529);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mps";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // mpsChart
            // 
            this.mpsChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.mpsChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.mpsChart.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.BackwardDiagonal;
            chartArea4.AxisX.LineColor = System.Drawing.Color.DimGray;
            chartArea4.AxisY.LineColor = System.Drawing.Color.DimGray;
            chartArea4.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.DarkGray;
            chartArea4.Name = "ChartArea1";
            this.mpsChart.ChartAreas.Add(chartArea4);
            this.mpsChart.Cursor = System.Windows.Forms.Cursors.Cross;
            this.mpsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            this.mpsChart.Legends.Add(legend4);
            this.mpsChart.Location = new System.Drawing.Point(3, 3);
            this.mpsChart.Name = "mpsChart";
            this.mpsChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series8.Color = System.Drawing.Color.DodgerBlue;
            series8.Legend = "Legend1";
            series8.Name = "Mps";
            this.mpsChart.Series.Add(series8);
            this.mpsChart.Size = new System.Drawing.Size(575, 523);
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
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.groupBox3.Location = new System.Drawing.Point(17, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(544, 137);
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
            this.fastBox.Location = new System.Drawing.Point(409, 28);
            this.fastBox.Name = "fastBox";
            this.fastBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fastBox.Size = new System.Drawing.Size(84, 21);
            this.fastBox.TabIndex = 12;
            this.fastBox.Text = "Fast-draw";
            this.fastBox.UseVisualStyleBackColor = true;
            this.fastBox.CheckedChanged += new System.EventHandler(this.FastChanged);
            // 
            // aaBox
            // 
            this.aaBox.AutoSize = true;
            this.aaBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aaBox.Location = new System.Drawing.Point(278, 29);
            this.aaBox.Name = "aaBox";
            this.aaBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.aaBox.Size = new System.Drawing.Size(98, 21);
            this.aaBox.TabIndex = 11;
            this.aaBox.Text = "Anti-aliasing";
            this.aaBox.UseVisualStyleBackColor = true;
            this.aaBox.CheckedChanged += new System.EventHandler(this.AAChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(21, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Update Batch Size";
            // 
            // updateBatchSizeBox
            // 
            this.updateBatchSizeBox.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateBatchSizeBox.Location = new System.Drawing.Point(141, 100);
            this.updateBatchSizeBox.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.updateBatchSizeBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.updateBatchSizeBox.Name = "updateBatchSizeBox";
            this.updateBatchSizeBox.Size = new System.Drawing.Size(74, 24);
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
            this.horizonPointsLbl.Location = new System.Drawing.Point(150, 29);
            this.horizonPointsLbl.Name = "horizonPointsLbl";
            this.horizonPointsLbl.Size = new System.Drawing.Size(16, 17);
            this.horizonPointsLbl.TabIndex = 10;
            this.horizonPointsLbl.Text = "/";
            this.horizonPointsLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chartHorizonTrack
            // 
            this.chartHorizonTrack.Location = new System.Drawing.Point(19, 49);
            this.chartHorizonTrack.Maximum = 4096;
            this.chartHorizonTrack.Minimum = 16;
            this.chartHorizonTrack.Name = "chartHorizonTrack";
            this.chartHorizonTrack.Size = new System.Drawing.Size(147, 45);
            this.chartHorizonTrack.TabIndex = 7;
            this.chartHorizonTrack.TickFrequency = 128;
            this.chartHorizonTrack.Value = 1024;
            this.chartHorizonTrack.Scroll += new System.EventHandler(this.ChartHorizonTrackerScroll);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(20, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Horizontal Points";
            // 
            // mpsLbl
            // 
            this.mpsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mpsLbl.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mpsLbl.Location = new System.Drawing.Point(2, 553);
            this.mpsLbl.Name = "mpsLbl";
            this.mpsLbl.Size = new System.Drawing.Size(181, 22);
            this.mpsLbl.TabIndex = 16;
            this.mpsLbl.Text = "/";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1169, 577);
            this.Controls.Add(this.mpsLbl);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.printOutPhaseLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.printInPhaseLbl);
            this.Controls.Add(this.beginBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.serialGroupBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(596, 512);
            this.Name = "MainForm";
            this.Opacity = 0.97D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SerialDSP";
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
    }
}

