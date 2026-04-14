using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSafetyByDate = new CusContorl.CusLabel();
            this.lblAcceptPerPeople = new CusContorl.CusLabel();
            this.lblAccept = new CusContorl.CusLabel();
            this.txtSafetyByDate = new System.Windows.Forms.TextBox();
            this.txtAccept = new System.Windows.Forms.TextBox();
            this.txtAcceptPerPeople = new System.Windows.Forms.TextBox();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.lblRFT = new CusContorl.CusLabel();
            this.txtSDP = new System.Windows.Forms.TextBox();
            this.lblSDP = new CusContorl.CusLabel();
            this.txtTotalProduced = new System.Windows.Forms.TextBox();
            this.label9 = new CusContorl.CusLabel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label11 = new CusContorl.CusLabel();
            this.txtPPH = new System.Windows.Forms.TextBox();
            this.lblPPH = new CusContorl.CusLabel();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnKPI = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnKZAP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.lblSafetyUntilDate = new CusContorl.CusLabel();
            this.txtSafetyUntilDate = new System.Windows.Forms.TextBox();
            this.lblSafetyDays = new CusContorl.CusLabel();
            this.txtSafetyDays = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel2 = new CusContorl.CusTableLayoutPanel();
            this.lblSafetyHeader = new CusContorl.CusLabel();
            this.tableLayoutPanel3 = new CusContorl.CusTableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTier3Header = new CusContorl.CusLabel();
            this.tableLayoutPanel4 = new CusContorl.CusTableLayoutPanel();
            this.chartMainBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMainPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel5 = new CusContorl.CusTableLayoutPanel();
            this.chartQuality = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel6 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel14 = new CusContorl.CusTableLayoutPanel();
            this.lblQualityHeader = new CusContorl.CusLabel();
            this.cbbRFTType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel7 = new CusContorl.CusTableLayoutPanel();
            this.chartDelivery = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel8 = new CusContorl.CusTableLayoutPanel();
            this.txtPOCompletionRate = new System.Windows.Forms.TextBox();
            this.lblDeliveryHeader = new CusContorl.CusLabel();
            this.lblPOCompletionRate = new CusContorl.CusLabel();
            this.tableLayoutPanel9 = new CusContorl.CusTableLayoutPanel();
            this.chartEfficiency = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel10 = new CusContorl.CusTableLayoutPanel();
            this.lblEfficiencyHeader = new CusContorl.CusLabel();
            this.tableLayoutPanel11 = new CusContorl.CusTableLayoutPanel();
            this.chartKaizen = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel12 = new CusContorl.CusTableLayoutPanel();
            this.lblKaizenHeader = new CusContorl.CusLabel();
            this.tableLayoutPanel13 = new CusContorl.CusTableLayoutPanel();
            this.lblOtherHeader = new CusContorl.CusLabel();
            this.btnMA = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMainChartPieKZAPTitle = new CusContorl.CusLabel();
            this.lblOpenText = new CusContorl.CusLabel();
            this.lblMainChartBarTitle = new CusContorl.CusLabel();
            this.lblQualityChartTitle = new CusContorl.CusLabel();
            this.lblMainChartPiePOTitle = new CusContorl.CusLabel();
            this.lblClosedText = new CusContorl.CusLabel();
            this.lblDeliveryChartTitle = new CusContorl.CusLabel();
            this.lblKaizenChartTitle = new CusContorl.CusLabel();
            this.lblEfficiencyChartTitle = new CusContorl.CusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridKZAP = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrincipal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFinishDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainPie)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartQuality)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDelivery)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEfficiency)).BeginInit();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKaizen)).BeginInit();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKZAP)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSafetyByDate
            // 
            this.lblSafetyByDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSafetyByDate.AutoSize = true;
            this.lblSafetyByDate.BackColor = System.Drawing.SystemColors.Window;
            this.lblSafetyByDate.Location = new System.Drawing.Point(3, 164);
            this.lblSafetyByDate.Name = "lblSafetyByDate";
            this.lblSafetyByDate.Size = new System.Drawing.Size(374, 82);
            this.lblSafetyByDate.TabIndex = 4;
            this.lblSafetyByDate.Text = "Work injury on date";
            // 
            // lblAcceptPerPeople
            // 
            this.lblAcceptPerPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAcceptPerPeople.AutoSize = true;
            this.lblAcceptPerPeople.BackColor = System.Drawing.SystemColors.Window;
            this.lblAcceptPerPeople.Location = new System.Drawing.Point(3, 87);
            this.lblAcceptPerPeople.Name = "lblAcceptPerPeople";
            this.lblAcceptPerPeople.Size = new System.Drawing.Size(370, 44);
            this.lblAcceptPerPeople.TabIndex = 5;
            this.lblAcceptPerPeople.Text = "Avg Adopt per Person";
            // 
            // lblAccept
            // 
            this.lblAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccept.AutoSize = true;
            this.lblAccept.BackColor = System.Drawing.SystemColors.Window;
            this.lblAccept.Location = new System.Drawing.Point(3, 48);
            this.lblAccept.Name = "lblAccept";
            this.lblAccept.Size = new System.Drawing.Size(370, 39);
            this.lblAccept.TabIndex = 5;
            this.lblAccept.Text = "Adopted";
            // 
            // txtSafetyByDate
            // 
            this.txtSafetyByDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyByDate.BackColor = System.Drawing.Color.White;
            this.txtSafetyByDate.Location = new System.Drawing.Point(383, 167);
            this.txtSafetyByDate.Name = "txtSafetyByDate";
            this.txtSafetyByDate.ReadOnly = true;
            this.txtSafetyByDate.Size = new System.Drawing.Size(89, 20);
            this.txtSafetyByDate.TabIndex = 6;
            // 
            // txtAccept
            // 
            this.txtAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccept.BackColor = System.Drawing.Color.White;
            this.txtAccept.Location = new System.Drawing.Point(379, 51);
            this.txtAccept.Name = "txtAccept";
            this.txtAccept.ReadOnly = true;
            this.txtAccept.Size = new System.Drawing.Size(88, 20);
            this.txtAccept.TabIndex = 7;
            // 
            // txtAcceptPerPeople
            // 
            this.txtAcceptPerPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcceptPerPeople.BackColor = System.Drawing.Color.White;
            this.txtAcceptPerPeople.Location = new System.Drawing.Point(379, 90);
            this.txtAcceptPerPeople.Name = "txtAcceptPerPeople";
            this.txtAcceptPerPeople.ReadOnly = true;
            this.txtAcceptPerPeople.Size = new System.Drawing.Size(88, 20);
            this.txtAcceptPerPeople.TabIndex = 8;
            // 
            // txtQuality
            // 
            this.txtQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuality.BackColor = System.Drawing.Color.White;
            this.txtQuality.Location = new System.Drawing.Point(378, 3);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.ReadOnly = true;
            this.txtQuality.Size = new System.Drawing.Size(88, 20);
            this.txtQuality.TabIndex = 14;
            // 
            // lblRFT
            // 
            this.lblRFT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRFT.AutoSize = true;
            this.lblRFT.BackColor = System.Drawing.SystemColors.Window;
            this.lblRFT.Location = new System.Drawing.Point(3, 0);
            this.lblRFT.Name = "lblRFT";
            this.lblRFT.Size = new System.Drawing.Size(369, 70);
            this.lblRFT.TabIndex = 13;
            this.lblRFT.Text = "Average RFT";
            // 
            // txtSDP
            // 
            this.txtSDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDP.BackColor = System.Drawing.Color.White;
            this.txtSDP.Location = new System.Drawing.Point(378, 45);
            this.txtSDP.Name = "txtSDP";
            this.txtSDP.ReadOnly = true;
            this.txtSDP.Size = new System.Drawing.Size(88, 20);
            this.txtSDP.TabIndex = 18;
            this.txtSDP.Visible = false;
            // 
            // lblSDP
            // 
            this.lblSDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSDP.AutoSize = true;
            this.lblSDP.BackColor = System.Drawing.SystemColors.Window;
            this.lblSDP.Location = new System.Drawing.Point(3, 42);
            this.lblSDP.Name = "lblSDP";
            this.lblSDP.Size = new System.Drawing.Size(369, 34);
            this.lblSDP.TabIndex = 17;
            this.lblSDP.Text = "SDP";
            this.lblSDP.Visible = false;
            // 
            // txtTotalProduced
            // 
            this.txtTotalProduced.BackColor = System.Drawing.Color.White;
            this.txtTotalProduced.Location = new System.Drawing.Point(360, 33);
            this.txtTotalProduced.Name = "txtTotalProduced";
            this.txtTotalProduced.ReadOnly = true;
            this.txtTotalProduced.Size = new System.Drawing.Size(100, 20);
            this.txtTotalProduced.TabIndex = 22;
            this.txtTotalProduced.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(257, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Total Produced:";
            this.label9.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(347, 7);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(30, 20);
            this.textBox4.TabIndex = 24;
            this.textBox4.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(262, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Supporting:";
            this.label11.Visible = false;
            // 
            // txtPPH
            // 
            this.txtPPH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPPH.BackColor = System.Drawing.Color.White;
            this.txtPPH.Location = new System.Drawing.Point(379, 66);
            this.txtPPH.Name = "txtPPH";
            this.txtPPH.ReadOnly = true;
            this.txtPPH.Size = new System.Drawing.Size(88, 20);
            this.txtPPH.TabIndex = 26;
            this.txtPPH.TextChanged += new System.EventHandler(this.txtPPH_TextChanged);
            // 
            // lblPPH
            // 
            this.lblPPH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPPH.AutoSize = true;
            this.lblPPH.BackColor = System.Drawing.SystemColors.Window;
            this.lblPPH.Location = new System.Drawing.Point(3, 63);
            this.lblPPH.Name = "lblPPH";
            this.lblPPH.Size = new System.Drawing.Size(370, 63);
            this.lblPPH.TabIndex = 25;
            this.lblPPH.Text = "PPH:";
            // 
            // date
            // 
            this.date.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date.Location = new System.Drawing.Point(332, 53);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(122, 20);
            this.date.TabIndex = 27;
            this.date.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.date.ValueChanged += new System.EventHandler(this.date_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Depth = 0;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(672, 44);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Primary = true;
            this.btnSearch.Size = new System.Drawing.Size(64, 36);
            this.btnSearch.TabIndex = 34;
            this.btnSearch.Text = "query";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnKPI
            // 
            this.btnKPI.AutoSize = true;
            this.btnKPI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnKPI.Depth = 0;
            this.btnKPI.Icon = null;
            this.btnKPI.Location = new System.Drawing.Point(378, 3);
            this.btnKPI.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnKPI.Name = "btnKPI";
            this.btnKPI.Primary = true;
            this.btnKPI.Size = new System.Drawing.Size(42, 36);
            this.btnKPI.TabIndex = 35;
            this.btnKPI.Text = "KPI";
            this.btnKPI.UseVisualStyleBackColor = true;
            this.btnKPI.Visible = false;
            this.btnKPI.Click += new System.EventHandler(this.btnKPI_Click);
            // 
            // btnKZAP
            // 
            this.btnKZAP.AutoSize = true;
            this.btnKZAP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnKZAP.Depth = 0;
            this.btnKZAP.Icon = null;
            this.btnKZAP.Location = new System.Drawing.Point(3, 73);
            this.btnKZAP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnKZAP.Name = "btnKZAP";
            this.btnKZAP.Primary = true;
            this.btnKZAP.Size = new System.Drawing.Size(160, 36);
            this.btnKZAP.TabIndex = 46;
            this.btnKZAP.Text = "Kaizen Action Plan";
            this.btnKZAP.UseVisualStyleBackColor = true;
            this.btnKZAP.Click += new System.EventHandler(this.btnOther_Click);
            // 
            // txtDepartment
            // 
            this.txtDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDepartment.Location = new System.Drawing.Point(503, 53);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(122, 20);
            this.txtDepartment.TabIndex = 52;
            this.txtDepartment.DoubleClick += new System.EventHandler(this.txtDepartment_DoubleClick);
            // 
            // lblSafetyUntilDate
            // 
            this.lblSafetyUntilDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSafetyUntilDate.AutoSize = true;
            this.lblSafetyUntilDate.BackColor = System.Drawing.SystemColors.Window;
            this.lblSafetyUntilDate.Location = new System.Drawing.Point(3, 82);
            this.lblSafetyUntilDate.Name = "lblSafetyUntilDate";
            this.lblSafetyUntilDate.Size = new System.Drawing.Size(374, 82);
            this.lblSafetyUntilDate.TabIndex = 4;
            this.lblSafetyUntilDate.Text = "Work injury YTD";
            // 
            // txtSafetyUntilDate
            // 
            this.txtSafetyUntilDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyUntilDate.BackColor = System.Drawing.Color.White;
            this.txtSafetyUntilDate.Location = new System.Drawing.Point(383, 85);
            this.txtSafetyUntilDate.Name = "txtSafetyUntilDate";
            this.txtSafetyUntilDate.ReadOnly = true;
            this.txtSafetyUntilDate.Size = new System.Drawing.Size(89, 20);
            this.txtSafetyUntilDate.TabIndex = 6;
            // 
            // lblSafetyDays
            // 
            this.lblSafetyDays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSafetyDays.AutoSize = true;
            this.lblSafetyDays.BackColor = System.Drawing.SystemColors.Window;
            this.lblSafetyDays.Location = new System.Drawing.Point(3, 246);
            this.lblSafetyDays.Name = "lblSafetyDays";
            this.lblSafetyDays.Size = new System.Drawing.Size(374, 85);
            this.lblSafetyDays.TabIndex = 4;
            this.lblSafetyDays.Text = "Days without work injury";
            // 
            // txtSafetyDays
            // 
            this.txtSafetyDays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyDays.BackColor = System.Drawing.Color.White;
            this.txtSafetyDays.Location = new System.Drawing.Point(383, 249);
            this.txtSafetyDays.Name = "txtSafetyDays";
            this.txtSafetyDays.ReadOnly = true;
            this.txtSafetyDays.Size = new System.Drawing.Size(89, 20);
            this.txtSafetyDays.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel11, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel13, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 64);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1925, 1012);
            this.tableLayoutPanel1.TabIndex = 53;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.lblSafetyHeader, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSafetyUntilDate, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblSafetyByDate, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblSafetyDays, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtSafetyDays, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtSafetyByDate, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtSafetyUntilDate, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(475, 331);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // lblSafetyHeader
            // 
            this.lblSafetyHeader.AutoSize = true;
            this.lblSafetyHeader.Location = new System.Drawing.Point(3, 0);
            this.lblSafetyHeader.Name = "lblSafetyHeader";
            this.lblSafetyHeader.Size = new System.Drawing.Size(37, 13);
            this.lblSafetyHeader.TabIndex = 54;
            this.lblSafetyHeader.Text = "Safety";
            this.lblSafetyHeader.Click += new System.EventHandler(this.lblSafetyHeader_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(484, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(956, 331);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.lblTier3Header);
            this.panel2.Controls.Add(this.txtDepartment);
            this.panel2.Controls.Add(this.date);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(950, 93);
            this.panel2.TabIndex = 0;
            // 
            // lblTier3Header
            // 
            this.lblTier3Header.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTier3Header.AutoSize = true;
            this.lblTier3Header.Location = new System.Drawing.Point(295, -4);
            this.lblTier3Header.Name = "lblTier3Header";
            this.lblTier3Header.Size = new System.Drawing.Size(120, 13);
            this.lblTier3Header.TabIndex = 53;
            this.lblTier3Header.Text = "Tier 3 Visual Dashboard";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.chartMainBar, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chartMainPie, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 102);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(950, 226);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // chartMainBar
            // 
            this.chartMainBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartMainBar.ChartAreas.Add(chartArea1);
            this.chartMainBar.Location = new System.Drawing.Point(3, 3);
            this.chartMainBar.Name = "chartMainBar";
            this.chartMainBar.Size = new System.Drawing.Size(469, 220);
            this.chartMainBar.TabIndex = 57;
            this.chartMainBar.Text = "chart1";
            // 
            // chartMainPie
            // 
            this.chartMainPie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartMainPie.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chartMainPie.Legends.Add(legend1);
            this.chartMainPie.Location = new System.Drawing.Point(478, 3);
            this.chartMainPie.Name = "chartMainPie";
            this.chartMainPie.Size = new System.Drawing.Size(469, 220);
            this.chartMainPie.TabIndex = 57;
            this.chartMainPie.Text = "chart1";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.chartQuality, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel14, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 340);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(475, 381);
            this.tableLayoutPanel5.TabIndex = 58;
            // 
            // chartQuality
            // 
            this.chartQuality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chartQuality.ChartAreas.Add(chartArea3);
            this.chartQuality.Location = new System.Drawing.Point(3, 155);
            this.chartQuality.Name = "chartQuality";
            this.chartQuality.Size = new System.Drawing.Size(469, 182);
            this.chartQuality.TabIndex = 57;
            this.chartQuality.Text = "chart1";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Controls.Add(this.lblRFT, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtQuality, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 79);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(469, 70);
            this.tableLayoutPanel6.TabIndex = 58;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.Controls.Add(this.lblQualityHeader, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.cbbRFTType, 1, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(469, 70);
            this.tableLayoutPanel14.TabIndex = 59;
            // 
            // lblQualityHeader
            // 
            this.lblQualityHeader.AutoSize = true;
            this.lblQualityHeader.Location = new System.Drawing.Point(3, 0);
            this.lblQualityHeader.Name = "lblQualityHeader";
            this.lblQualityHeader.Size = new System.Drawing.Size(39, 13);
            this.lblQualityHeader.TabIndex = 59;
            this.lblQualityHeader.Text = "Quality";
            this.lblQualityHeader.Click += new System.EventHandler(this.lblQualityHeader_Click);
            // 
            // cbbRFTType
            // 
            this.cbbRFTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRFTType.FormattingEnabled = true;
            this.cbbRFTType.Items.AddRange(new object[] {
            "L",
            "S",
            "C",
            "T"});
            this.cbbRFTType.Location = new System.Drawing.Point(378, 3);
            this.cbbRFTType.Name = "cbbRFTType";
            this.cbbRFTType.Size = new System.Drawing.Size(88, 21);
            this.cbbRFTType.TabIndex = 58;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.chartDelivery, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 727);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(475, 282);
            this.tableLayoutPanel7.TabIndex = 60;
            // 
            // chartDelivery
            // 
            this.chartDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.chartDelivery.ChartAreas.Add(chartArea4);
            this.chartDelivery.Location = new System.Drawing.Point(3, 124);
            this.chartDelivery.Name = "chartDelivery";
            this.chartDelivery.Size = new System.Drawing.Size(469, 155);
            this.chartDelivery.TabIndex = 58;
            this.chartDelivery.Text = "chart1";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.Controls.Add(this.txtSDP, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.txtPOCompletionRate, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.lblDeliveryHeader, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblPOCompletionRate, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.btnKPI, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblSDP, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(469, 115);
            this.tableLayoutPanel8.TabIndex = 59;
            // 
            // txtPOCompletionRate
            // 
            this.txtPOCompletionRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOCompletionRate.BackColor = System.Drawing.Color.White;
            this.txtPOCompletionRate.Location = new System.Drawing.Point(378, 79);
            this.txtPOCompletionRate.Name = "txtPOCompletionRate";
            this.txtPOCompletionRate.ReadOnly = true;
            this.txtPOCompletionRate.Size = new System.Drawing.Size(88, 20);
            this.txtPOCompletionRate.TabIndex = 46;
            this.txtPOCompletionRate.Click += new System.EventHandler(this.txtPOCompletionRate_Click);
            this.txtPOCompletionRate.TextChanged += new System.EventHandler(this.txtPOCompletionRate_TextChanged);
            // 
            // lblDeliveryHeader
            // 
            this.lblDeliveryHeader.AutoSize = true;
            this.lblDeliveryHeader.Location = new System.Drawing.Point(3, 0);
            this.lblDeliveryHeader.Name = "lblDeliveryHeader";
            this.lblDeliveryHeader.Size = new System.Drawing.Size(45, 13);
            this.lblDeliveryHeader.TabIndex = 56;
            this.lblDeliveryHeader.Text = "Delivery";
            this.lblDeliveryHeader.Click += new System.EventHandler(this.lblDeliveryHeader_Click);
            // 
            // lblPOCompletionRate
            // 
            this.lblPOCompletionRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPOCompletionRate.AutoSize = true;
            this.lblPOCompletionRate.BackColor = System.Drawing.SystemColors.Window;
            this.lblPOCompletionRate.Location = new System.Drawing.Point(3, 76);
            this.lblPOCompletionRate.Name = "lblPOCompletionRate";
            this.lblPOCompletionRate.Size = new System.Drawing.Size(369, 39);
            this.lblPOCompletionRate.TabIndex = 45;
            this.lblPOCompletionRate.Text = "PO Completion Rate (100%)";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Controls.Add(this.chartEfficiency, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(1446, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(476, 331);
            this.tableLayoutPanel9.TabIndex = 61;
            // 
            // chartEfficiency
            // 
            this.chartEfficiency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.chartEfficiency.ChartAreas.Add(chartArea5);
            this.chartEfficiency.Location = new System.Drawing.Point(3, 135);
            this.chartEfficiency.Name = "chartEfficiency";
            this.chartEfficiency.Size = new System.Drawing.Size(470, 193);
            this.chartEfficiency.TabIndex = 57;
            this.chartEfficiency.Text = "chart1";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel10.Controls.Add(this.txtPPH, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.lblEfficiencyHeader, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblPPH, 0, 1);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(470, 126);
            this.tableLayoutPanel10.TabIndex = 58;
            // 
            // lblEfficiencyHeader
            // 
            this.lblEfficiencyHeader.AutoSize = true;
            this.lblEfficiencyHeader.Location = new System.Drawing.Point(3, 0);
            this.lblEfficiencyHeader.Name = "lblEfficiencyHeader";
            this.lblEfficiencyHeader.Size = new System.Drawing.Size(53, 13);
            this.lblEfficiencyHeader.TabIndex = 57;
            this.lblEfficiencyHeader.Text = "Efficiency";
            this.lblEfficiencyHeader.Click += new System.EventHandler(this.lblEfficiencyHeader_Click);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.chartKaizen, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel12, 0, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(1446, 340);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(476, 381);
            this.tableLayoutPanel11.TabIndex = 62;
            // 
            // chartKaizen
            // 
            this.chartKaizen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartKaizen.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea6.Name = "ChartArea1";
            this.chartKaizen.ChartAreas.Add(chartArea6);
            this.chartKaizen.Location = new System.Drawing.Point(3, 140);
            this.chartKaizen.Name = "chartKaizen";
            this.chartKaizen.Size = new System.Drawing.Size(470, 238);
            this.chartKaizen.TabIndex = 58;
            this.chartKaizen.Text = "chart1";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel12.Controls.Add(this.lblKaizenHeader, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.txtAcceptPerPeople, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.txtAccept, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.lblAccept, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.lblAcceptPerPeople, 0, 2);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 3;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(470, 131);
            this.tableLayoutPanel12.TabIndex = 59;
            // 
            // lblKaizenHeader
            // 
            this.lblKaizenHeader.AutoSize = true;
            this.lblKaizenHeader.Location = new System.Drawing.Point(3, 0);
            this.lblKaizenHeader.Name = "lblKaizenHeader";
            this.lblKaizenHeader.Size = new System.Drawing.Size(39, 13);
            this.lblKaizenHeader.TabIndex = 58;
            this.lblKaizenHeader.Text = "Kaizen";
            this.lblKaizenHeader.Click += new System.EventHandler(this.lblKaizenHeader_Click);
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel13.ColumnCount = 1;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel13.Controls.Add(this.lblOtherHeader, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnMA, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.btnKZAP, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(1446, 727);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 4;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(476, 282);
            this.tableLayoutPanel13.TabIndex = 63;
            // 
            // lblOtherHeader
            // 
            this.lblOtherHeader.AutoSize = true;
            this.lblOtherHeader.Location = new System.Drawing.Point(3, 0);
            this.lblOtherHeader.Name = "lblOtherHeader";
            this.lblOtherHeader.Size = new System.Drawing.Size(33, 13);
            this.lblOtherHeader.TabIndex = 59;
            this.lblOtherHeader.Text = "Other";
            // 
            // btnMA
            // 
            this.btnMA.AutoSize = true;
            this.btnMA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMA.Depth = 0;
            this.btnMA.Icon = null;
            this.btnMA.Location = new System.Drawing.Point(3, 143);
            this.btnMA.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMA.Name = "btnMA";
            this.btnMA.Primary = true;
            this.btnMA.Size = new System.Drawing.Size(180, 36);
            this.btnMA.TabIndex = 52;
            this.btnMA.Text = "Maturity Assessment";
            this.btnMA.UseVisualStyleBackColor = true;
            this.btnMA.Click += new System.EventHandler(this.btnMA_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblMainChartPieKZAPTitle);
            this.panel1.Controls.Add(this.lblOpenText);
            this.panel1.Controls.Add(this.lblMainChartBarTitle);
            this.panel1.Controls.Add(this.lblQualityChartTitle);
            this.panel1.Controls.Add(this.lblMainChartPiePOTitle);
            this.panel1.Controls.Add(this.txtTotalProduced);
            this.panel1.Controls.Add(this.lblClosedText);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblDeliveryChartTitle);
            this.panel1.Controls.Add(this.lblKaizenChartTitle);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblEfficiencyChartTitle);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Location = new System.Drawing.Point(3, 213);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 66);
            this.panel1.TabIndex = 60;
            // 
            // lblMainChartPieKZAPTitle
            // 
            this.lblMainChartPieKZAPTitle.AutoSize = true;
            this.lblMainChartPieKZAPTitle.Location = new System.Drawing.Point(278, 57);
            this.lblMainChartPieKZAPTitle.Name = "lblMainChartPieKZAPTitle";
            this.lblMainChartPieKZAPTitle.Size = new System.Drawing.Size(96, 13);
            this.lblMainChartPieKZAPTitle.TabIndex = 60;
            this.lblMainChartPieKZAPTitle.Text = "Kaizen Action Plan";
            this.lblMainChartPieKZAPTitle.Visible = false;
            // 
            // lblOpenText
            // 
            this.lblOpenText.AutoSize = true;
            this.lblOpenText.Location = new System.Drawing.Point(215, 37);
            this.lblOpenText.Name = "lblOpenText";
            this.lblOpenText.Size = new System.Drawing.Size(33, 13);
            this.lblOpenText.TabIndex = 63;
            this.lblOpenText.Text = "Open";
            this.lblOpenText.Visible = false;
            // 
            // lblMainChartBarTitle
            // 
            this.lblMainChartBarTitle.AutoSize = true;
            this.lblMainChartBarTitle.Location = new System.Drawing.Point(123, 57);
            this.lblMainChartBarTitle.Name = "lblMainChartBarTitle";
            this.lblMainChartBarTitle.Size = new System.Drawing.Size(123, 13);
            this.lblMainChartBarTitle.TabIndex = 59;
            this.lblMainChartBarTitle.Text = "Total Kaizen Action Plan";
            this.lblMainChartBarTitle.Visible = false;
            // 
            // lblQualityChartTitle
            // 
            this.lblQualityChartTitle.AutoSize = true;
            this.lblQualityChartTitle.Location = new System.Drawing.Point(9, 12);
            this.lblQualityChartTitle.Name = "lblQualityChartTitle";
            this.lblQualityChartTitle.Size = new System.Drawing.Size(89, 13);
            this.lblQualityChartTitle.TabIndex = 57;
            this.lblQualityChartTitle.Text = "Weekly Avg RFT";
            this.lblQualityChartTitle.Visible = false;
            // 
            // lblMainChartPiePOTitle
            // 
            this.lblMainChartPiePOTitle.AutoSize = true;
            this.lblMainChartPiePOTitle.Location = new System.Drawing.Point(11, 56);
            this.lblMainChartPiePOTitle.Name = "lblMainChartPiePOTitle";
            this.lblMainChartPiePOTitle.Size = new System.Drawing.Size(95, 13);
            this.lblMainChartPiePOTitle.TabIndex = 61;
            this.lblMainChartPiePOTitle.Text = "PO complete chart";
            this.lblMainChartPiePOTitle.Visible = false;
            // 
            // lblClosedText
            // 
            this.lblClosedText.AutoSize = true;
            this.lblClosedText.Location = new System.Drawing.Point(210, 10);
            this.lblClosedText.Name = "lblClosedText";
            this.lblClosedText.Size = new System.Drawing.Size(39, 13);
            this.lblClosedText.TabIndex = 62;
            this.lblClosedText.Text = "Closed";
            this.lblClosedText.Visible = false;
            // 
            // lblDeliveryChartTitle
            // 
            this.lblDeliveryChartTitle.AutoSize = true;
            this.lblDeliveryChartTitle.Location = new System.Drawing.Point(13, 37);
            this.lblDeliveryChartTitle.Name = "lblDeliveryChartTitle";
            this.lblDeliveryChartTitle.Size = new System.Drawing.Size(64, 13);
            this.lblDeliveryChartTitle.TabIndex = 58;
            this.lblDeliveryChartTitle.Text = "Weekly rate";
            this.lblDeliveryChartTitle.Visible = false;
            // 
            // lblKaizenChartTitle
            // 
            this.lblKaizenChartTitle.AutoSize = true;
            this.lblKaizenChartTitle.Location = new System.Drawing.Point(107, 38);
            this.lblKaizenChartTitle.Name = "lblKaizenChartTitle";
            this.lblKaizenChartTitle.Size = new System.Drawing.Size(86, 13);
            this.lblKaizenChartTitle.TabIndex = 58;
            this.lblKaizenChartTitle.Text = "Monthly adopted";
            this.lblKaizenChartTitle.Visible = false;
            // 
            // lblEfficiencyChartTitle
            // 
            this.lblEfficiencyChartTitle.AutoSize = true;
            this.lblEfficiencyChartTitle.Location = new System.Drawing.Point(103, 11);
            this.lblEfficiencyChartTitle.Name = "lblEfficiencyChartTitle";
            this.lblEfficiencyChartTitle.Size = new System.Drawing.Size(68, 13);
            this.lblEfficiencyChartTitle.TabIndex = 57;
            this.lblEfficiencyChartTitle.Text = "Weekly PPH";
            this.lblEfficiencyChartTitle.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.panel3.Controls.Add(this.gridKZAP);
            this.panel3.Location = new System.Drawing.Point(481, 337);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(962, 675);
            this.panel3.TabIndex = 64;
            // 
            // gridKZAP
            // 
            this.gridKZAP.AllowUserToAddRows = false;
            this.gridKZAP.AllowUserToDeleteRows = false;
            this.gridKZAP.AllowUserToResizeColumns = false;
            this.gridKZAP.AllowUserToResizeRows = false;
            this.gridKZAP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridKZAP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(25)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridKZAP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridKZAP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridKZAP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItem,
            this.colProblemPoint,
            this.colMeasure,
            this.colPrincipal,
            this.colDueDate,
            this.colPlant,
            this.colSection,
            this.colFinishDate});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridKZAP.DefaultCellStyle = dataGridViewCellStyle8;
            this.gridKZAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKZAP.Enabled = false;
            this.gridKZAP.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.gridKZAP.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gridKZAP.Location = new System.Drawing.Point(0, 0);
            this.gridKZAP.Name = "gridKZAP";
            this.gridKZAP.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridKZAP.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.gridKZAP.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Silver;
            this.gridKZAP.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.gridKZAP.Size = new System.Drawing.Size(962, 675);
            this.gridKZAP.TabIndex = 9;
            // 
            // colItem
            // 
            this.colItem.DataPropertyName = "ID";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Silver;
            this.colItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.colItem.HeaderText = "Item";
            this.colItem.MinimumWidth = 6;
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Visible = false;
            // 
            // colProblemPoint
            // 
            this.colProblemPoint.DataPropertyName = "G_PROBLEMPOINT";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Silver;
            this.colProblemPoint.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProblemPoint.HeaderText = "Problem Point";
            this.colProblemPoint.MinimumWidth = 6;
            this.colProblemPoint.Name = "colProblemPoint";
            this.colProblemPoint.ReadOnly = true;
            // 
            // colMeasure
            // 
            this.colMeasure.DataPropertyName = "G_MEASURE";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Silver;
            this.colMeasure.DefaultCellStyle = dataGridViewCellStyle4;
            this.colMeasure.HeaderText = "Measure";
            this.colMeasure.MinimumWidth = 6;
            this.colMeasure.Name = "colMeasure";
            this.colMeasure.ReadOnly = true;
            // 
            // colPrincipal
            // 
            this.colPrincipal.DataPropertyName = "G_PRINCTIPAL";
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Silver;
            this.colPrincipal.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPrincipal.HeaderText = "Principal";
            this.colPrincipal.MinimumWidth = 6;
            this.colPrincipal.Name = "colPrincipal";
            this.colPrincipal.ReadOnly = true;
            // 
            // colDueDate
            // 
            this.colDueDate.DataPropertyName = "G_PLANDATE";
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Silver;
            this.colDueDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.colDueDate.HeaderText = "Due Date";
            this.colDueDate.MinimumWidth = 6;
            this.colDueDate.Name = "colDueDate";
            this.colDueDate.ReadOnly = true;
            // 
            // colPlant
            // 
            this.colPlant.DataPropertyName = "UDF05";
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Silver;
            this.colPlant.DefaultCellStyle = dataGridViewCellStyle7;
            this.colPlant.HeaderText = "Plant";
            this.colPlant.MinimumWidth = 6;
            this.colPlant.Name = "colPlant";
            this.colPlant.ReadOnly = true;
            this.colPlant.Visible = false;
            // 
            // colSection
            // 
            this.colSection.DataPropertyName = "UDF07";
            this.colSection.HeaderText = "Section";
            this.colSection.MinimumWidth = 6;
            this.colSection.Name = "colSection";
            this.colSection.ReadOnly = true;
            this.colSection.Visible = false;
            // 
            // colFinishDate
            // 
            this.colFinishDate.DataPropertyName = "G_FINISHDATE";
            this.colFinishDate.HeaderText = "Finish Date";
            this.colFinishDate.MinimumWidth = 6;
            this.colFinishDate.Name = "colFinishDate";
            this.colFinishDate.ReadOnly = true;
            this.colFinishDate.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Tier Meeting Level 3";
            this.Load += new System.EventHandler(this.SafetyKaizen_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMainBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainPie)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartQuality)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDelivery)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEfficiency)).EndInit();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartKaizen)).EndInit();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKZAP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtSafetyByDate;
        private System.Windows.Forms.TextBox txtAccept;
        private System.Windows.Forms.TextBox txtAcceptPerPeople;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.TextBox txtSDP;
        private System.Windows.Forms.TextBox txtTotalProduced;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtPPH;
        private System.Windows.Forms.DateTimePicker date;
        private MaterialSkin.Controls.MaterialRaisedButton btnSearch;
        private MaterialSkin.Controls.MaterialRaisedButton btnKPI;
        private MaterialSkin.Controls.MaterialRaisedButton btnKZAP;
        private TextBox txtDepartment;
        private TextBox txtSafetyUntilDate;
        private TextBox txtSafetyDays;
        private MaterialSkin.Controls.MaterialRaisedButton btnMA;
        private TextBox txtPOCompletionRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDelivery;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEfficiency;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKaizen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMainBar;
        private Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartQuality;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMainPie;
        private Panel panel1;
        private DataGridView gridKZAP;
        private Panel panel3;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colProblemPoint;
        private DataGridViewTextBoxColumn colMeasure;
        private DataGridViewTextBoxColumn colPrincipal;
        private DataGridViewTextBoxColumn colDueDate;
        private DataGridViewTextBoxColumn colPlant;
        private DataGridViewTextBoxColumn colSection;
        private DataGridViewTextBoxColumn colFinishDate;
        private ComboBox cbbRFTType;
        private CusContorl.CusLabel lblSafetyByDate;
        private CusContorl.CusLabel lblAcceptPerPeople;
        private CusContorl.CusLabel lblAccept;
        private CusContorl.CusLabel lblRFT;
        private CusContorl.CusLabel lblSDP;
        private CusContorl.CusLabel label9;
        private CusContorl.CusLabel label11;
        private CusContorl.CusLabel lblPPH;
        private CusContorl.CusLabel lblSafetyUntilDate;
        private CusContorl.CusLabel lblSafetyDays;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private CusContorl.CusLabel lblTier3Header;
        private CusContorl.CusLabel lblPOCompletionRate;
        private CusContorl.CusLabel lblSafetyHeader;
        private CusContorl.CusLabel lblDeliveryHeader;
        private CusContorl.CusLabel lblEfficiencyHeader;
        private CusContorl.CusLabel lblKaizenHeader;
        private CusContorl.CusLabel lblOtherHeader;
        private CusContorl.CusLabel lblQualityChartTitle;
        private CusContorl.CusLabel lblDeliveryChartTitle;
        private CusContorl.CusLabel lblEfficiencyChartTitle;
        private CusContorl.CusLabel lblKaizenChartTitle;
        private CusContorl.CusLabel lblMainChartBarTitle;
        private CusContorl.CusLabel lblMainChartPieKZAPTitle;
        private CusContorl.CusLabel lblMainChartPiePOTitle;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel3;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel4;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel2;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel5;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel6;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel7;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel8;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel9;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel10;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel11;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel12;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel13;
        private CusContorl.CusLabel lblOpenText;
        private CusContorl.CusLabel lblClosedText;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel14;
        private CusContorl.CusLabel lblQualityHeader;
    }
}

