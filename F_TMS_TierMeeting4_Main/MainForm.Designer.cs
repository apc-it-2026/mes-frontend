using System.Windows.Forms;
using CusContorl;

namespace TierMeeting
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tableLayoutPanel13 = new CusContorl.CusTableLayoutPanel();
            this.lblOtherHeader = new CusContorl.CusLabel();
            this.btnMA = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnKZAP = new MaterialSkin.Controls.MaterialRaisedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtTotalProduced = new System.Windows.Forms.TextBox();
            this.label9 = new CusContorl.CusLabel();
            this.lblMainChartPieKZAPTitle = new CusContorl.CusLabel();
            this.label11 = new CusContorl.CusLabel();
            this.lblMainChartBarTitle = new CusContorl.CusLabel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.lblMainChartPiePOTitle = new CusContorl.CusLabel();
            this.lblOpenText = new CusContorl.CusLabel();
            this.lblClosedText = new CusContorl.CusLabel();
            this.lblKaizenChartTitle = new CusContorl.CusLabel();
            this.lblEfficiencyChartTitle = new CusContorl.CusLabel();
            this.lblDeliveryChartTitle = new CusContorl.CusLabel();
            this.lblQualityChartTitle = new CusContorl.CusLabel();
            this.tableLayoutPanel11 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel12 = new CusContorl.CusTableLayoutPanel();
            this.lblKaizenHeader = new CusContorl.CusLabel();
            this.txtAcceptPerPeople = new System.Windows.Forms.TextBox();
            this.txtAccept = new System.Windows.Forms.TextBox();
            this.lblAccept = new CusContorl.CusLabel();
            this.lblAcceptPerPeople = new CusContorl.CusLabel();
            this.chartKaizen = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel9 = new CusContorl.CusTableLayoutPanel();
            this.chartEfficiency = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel10 = new CusContorl.CusTableLayoutPanel();
            this.txtPPH = new System.Windows.Forms.TextBox();
            this.lblEfficiencyHeader = new CusContorl.CusLabel();
            this.lblPPH = new CusContorl.CusLabel();
            this.tableLayoutPanel7 = new CusContorl.CusTableLayoutPanel();
            this.chartDelivery = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel8 = new CusContorl.CusTableLayoutPanel();
            this.txtSDP = new System.Windows.Forms.TextBox();
            this.txtPOCompletionRate = new System.Windows.Forms.TextBox();
            this.lblDeliveryHeader = new CusContorl.CusLabel();
            this.lblPOCompletionRate = new CusContorl.CusLabel();
            this.btnKPI = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblSDP = new CusContorl.CusLabel();
            this.tableLayoutPanel5 = new CusContorl.CusTableLayoutPanel();
            this.chartQuality = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel6 = new CusContorl.CusTableLayoutPanel();
            this.lblRFT = new CusContorl.CusLabel();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel14 = new CusContorl.CusTableLayoutPanel();
            this.cbbRFTType = new System.Windows.Forms.ComboBox();
            this.lblQualityHeader = new CusContorl.CusLabel();
            this.gridKZAP = new System.Windows.Forms.DataGridView();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProblemPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeasure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrincipal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPlant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFinishDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new CusContorl.CusTableLayoutPanel();
            this.lblSafetyHeader = new CusContorl.CusLabel();
            this.lblSafetyUntilDate = new CusContorl.CusLabel();
            this.lblSafetyByDate = new CusContorl.CusLabel();
            this.lblSafetyDays = new CusContorl.CusLabel();
            this.txtSafetyDays = new System.Windows.Forms.TextBox();
            this.txtSafetyByDate = new System.Windows.Forms.TextBox();
            this.txtSafetyUntilDate = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel3 = new CusContorl.CusTableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblTier4Header = new CusContorl.CusLabel();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.date = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel4 = new CusContorl.CusTableLayoutPanel();
            this.chartMainBar = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartMainPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel13.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKaizen)).BeginInit();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEfficiency)).BeginInit();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDelivery)).BeginInit();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartQuality)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKZAP)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainPie)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
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
            this.tableLayoutPanel13.Location = new System.Drawing.Point(1225, 677);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 4;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(402, 332);
            this.tableLayoutPanel13.TabIndex = 63;
            // 
            // lblOtherHeader
            // 
            this.lblOtherHeader.AutoSize = true;
            this.lblOtherHeader.Location = new System.Drawing.Point(3, 0);
            this.lblOtherHeader.Name = "lblOtherHeader";
            this.lblOtherHeader.Size = new System.Drawing.Size(47, 15);
            this.lblOtherHeader.TabIndex = 59;
            this.lblOtherHeader.Text = "Other";
            // 
            // btnMA
            // 
            this.btnMA.AutoSize = true;
            this.btnMA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMA.Depth = 0;
            this.btnMA.Icon = null;
            this.btnMA.Location = new System.Drawing.Point(3, 169);
            this.btnMA.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnMA.Name = "btnMA";
            this.btnMA.Primary = true;
            this.btnMA.Size = new System.Drawing.Size(221, 36);
            this.btnMA.TabIndex = 52;
            this.btnMA.Text = "Maturity Assessment";
            this.btnMA.UseVisualStyleBackColor = true;
            this.btnMA.Click += new System.EventHandler(this.btnMA_Click);
            // 
            // btnKZAP
            // 
            this.btnKZAP.AutoSize = true;
            this.btnKZAP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnKZAP.Depth = 0;
            this.btnKZAP.Icon = null;
            this.btnKZAP.Location = new System.Drawing.Point(3, 86);
            this.btnKZAP.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnKZAP.Name = "btnKZAP";
            this.btnKZAP.Primary = true;
            this.btnKZAP.Size = new System.Drawing.Size(196, 36);
            this.btnKZAP.TabIndex = 46;
            this.btnKZAP.Text = "Kaizen Action Plan";
            this.btnKZAP.UseVisualStyleBackColor = true;
            this.btnKZAP.Click += new System.EventHandler(this.btnOther_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTotalProduced);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblMainChartPieKZAPTitle);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lblMainChartBarTitle);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.lblMainChartPiePOTitle);
            this.panel1.Controls.Add(this.lblOpenText);
            this.panel1.Controls.Add(this.lblClosedText);
            this.panel1.Controls.Add(this.lblKaizenChartTitle);
            this.panel1.Controls.Add(this.lblEfficiencyChartTitle);
            this.panel1.Controls.Add(this.lblDeliveryChartTitle);
            this.panel1.Controls.Add(this.lblQualityChartTitle);
            this.panel1.Location = new System.Drawing.Point(3, 252);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 77);
            this.panel1.TabIndex = 60;
            // 
            // txtTotalProduced
            // 
            this.txtTotalProduced.BackColor = System.Drawing.Color.White;
            this.txtTotalProduced.Location = new System.Drawing.Point(363, 51);
            this.txtTotalProduced.Name = "txtTotalProduced";
            this.txtTotalProduced.ReadOnly = true;
            this.txtTotalProduced.Size = new System.Drawing.Size(100, 25);
            this.txtTotalProduced.TabIndex = 22;
            this.txtTotalProduced.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Window;
            this.label9.Location = new System.Drawing.Point(262, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 15);
            this.label9.TabIndex = 21;
            this.label9.Text = "Total Produced:";
            this.label9.Visible = false;
            // 
            // lblMainChartPieKZAPTitle
            // 
            this.lblMainChartPieKZAPTitle.AutoSize = true;
            this.lblMainChartPieKZAPTitle.Location = new System.Drawing.Point(188, 9);
            this.lblMainChartPieKZAPTitle.Name = "lblMainChartPieKZAPTitle";
            this.lblMainChartPieKZAPTitle.Size = new System.Drawing.Size(151, 15);
            this.lblMainChartPieKZAPTitle.TabIndex = 67;
            this.lblMainChartPieKZAPTitle.Text = "Kaizen Action Plan";
            this.lblMainChartPieKZAPTitle.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(350, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 15);
            this.label11.TabIndex = 23;
            this.label11.Text = "Supporting:";
            this.label11.Visible = false;
            // 
            // lblMainChartBarTitle
            // 
            this.lblMainChartBarTitle.AutoSize = true;
            this.lblMainChartBarTitle.Location = new System.Drawing.Point(182, 33);
            this.lblMainChartBarTitle.Name = "lblMainChartBarTitle";
            this.lblMainChartBarTitle.Size = new System.Drawing.Size(199, 15);
            this.lblMainChartBarTitle.TabIndex = 66;
            this.lblMainChartBarTitle.Text = "Total Kaizen Action Plan";
            this.lblMainChartBarTitle.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(432, 25);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(30, 25);
            this.textBox4.TabIndex = 24;
            this.textBox4.Visible = false;
            // 
            // lblMainChartPiePOTitle
            // 
            this.lblMainChartPiePOTitle.AutoSize = true;
            this.lblMainChartPiePOTitle.Location = new System.Drawing.Point(129, 53);
            this.lblMainChartPiePOTitle.Name = "lblMainChartPiePOTitle";
            this.lblMainChartPiePOTitle.Size = new System.Drawing.Size(143, 15);
            this.lblMainChartPiePOTitle.TabIndex = 68;
            this.lblMainChartPiePOTitle.Text = "PO complete chart";
            this.lblMainChartPiePOTitle.Visible = false;
            // 
            // lblOpenText
            // 
            this.lblOpenText.AutoSize = true;
            this.lblOpenText.Location = new System.Drawing.Point(129, 35);
            this.lblOpenText.Name = "lblOpenText";
            this.lblOpenText.Size = new System.Drawing.Size(39, 15);
            this.lblOpenText.TabIndex = 65;
            this.lblOpenText.Text = "Open";
            this.lblOpenText.Visible = false;
            // 
            // lblClosedText
            // 
            this.lblClosedText.AutoSize = true;
            this.lblClosedText.Location = new System.Drawing.Point(129, 12);
            this.lblClosedText.Name = "lblClosedText";
            this.lblClosedText.Size = new System.Drawing.Size(55, 15);
            this.lblClosedText.TabIndex = 64;
            this.lblClosedText.Text = "Closed";
            this.lblClosedText.Visible = false;
            // 
            // lblKaizenChartTitle
            // 
            this.lblKaizenChartTitle.AutoSize = true;
            this.lblKaizenChartTitle.Location = new System.Drawing.Point(19, 51);
            this.lblKaizenChartTitle.Name = "lblKaizenChartTitle";
            this.lblKaizenChartTitle.Size = new System.Drawing.Size(127, 15);
            this.lblKaizenChartTitle.TabIndex = 61;
            this.lblKaizenChartTitle.Text = "Monthly adopted";
            this.lblKaizenChartTitle.Visible = false;
            // 
            // lblEfficiencyChartTitle
            // 
            this.lblEfficiencyChartTitle.AutoSize = true;
            this.lblEfficiencyChartTitle.Location = new System.Drawing.Point(19, 38);
            this.lblEfficiencyChartTitle.Name = "lblEfficiencyChartTitle";
            this.lblEfficiencyChartTitle.Size = new System.Drawing.Size(87, 15);
            this.lblEfficiencyChartTitle.TabIndex = 59;
            this.lblEfficiencyChartTitle.Text = "Weekly PPH";
            this.lblEfficiencyChartTitle.Visible = false;
            // 
            // lblDeliveryChartTitle
            // 
            this.lblDeliveryChartTitle.AutoSize = true;
            this.lblDeliveryChartTitle.Location = new System.Drawing.Point(19, 25);
            this.lblDeliveryChartTitle.Name = "lblDeliveryChartTitle";
            this.lblDeliveryChartTitle.Size = new System.Drawing.Size(95, 15);
            this.lblDeliveryChartTitle.TabIndex = 62;
            this.lblDeliveryChartTitle.Text = "Weekly rate";
            this.lblDeliveryChartTitle.Visible = false;
            // 
            // lblQualityChartTitle
            // 
            this.lblQualityChartTitle.AutoSize = true;
            this.lblQualityChartTitle.Location = new System.Drawing.Point(19, 12);
            this.lblQualityChartTitle.Name = "lblQualityChartTitle";
            this.lblQualityChartTitle.Size = new System.Drawing.Size(119, 15);
            this.lblQualityChartTitle.TabIndex = 60;
            this.lblQualityChartTitle.Text = "Weekly Avg RFT";
            this.lblQualityChartTitle.Visible = false;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel12, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.chartKaizen, 0, 1);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(1225, 340);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 2;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(402, 331);
            this.tableLayoutPanel11.TabIndex = 62;
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
            this.tableLayoutPanel12.Size = new System.Drawing.Size(396, 139);
            this.tableLayoutPanel12.TabIndex = 59;
            // 
            // lblKaizenHeader
            // 
            this.lblKaizenHeader.AutoSize = true;
            this.lblKaizenHeader.Location = new System.Drawing.Point(3, 0);
            this.lblKaizenHeader.Name = "lblKaizenHeader";
            this.lblKaizenHeader.Size = new System.Drawing.Size(55, 15);
            this.lblKaizenHeader.TabIndex = 58;
            this.lblKaizenHeader.Text = "Kaizen";
            this.lblKaizenHeader.Click += new System.EventHandler(this.lblKaizenHeader_Click);
            // 
            // txtAcceptPerPeople
            // 
            this.txtAcceptPerPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcceptPerPeople.BackColor = System.Drawing.Color.White;
            this.txtAcceptPerPeople.Location = new System.Drawing.Point(319, 95);
            this.txtAcceptPerPeople.Name = "txtAcceptPerPeople";
            this.txtAcceptPerPeople.ReadOnly = true;
            this.txtAcceptPerPeople.Size = new System.Drawing.Size(74, 25);
            this.txtAcceptPerPeople.TabIndex = 8;
            // 
            // txtAccept
            // 
            this.txtAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccept.BackColor = System.Drawing.Color.White;
            this.txtAccept.Location = new System.Drawing.Point(319, 54);
            this.txtAccept.Name = "txtAccept";
            this.txtAccept.ReadOnly = true;
            this.txtAccept.Size = new System.Drawing.Size(74, 25);
            this.txtAccept.TabIndex = 7;
            // 
            // lblAccept
            // 
            this.lblAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccept.AutoSize = true;
            this.lblAccept.BackColor = System.Drawing.SystemColors.Window;
            this.lblAccept.Location = new System.Drawing.Point(3, 51);
            this.lblAccept.Name = "lblAccept";
            this.lblAccept.Size = new System.Drawing.Size(310, 41);
            this.lblAccept.TabIndex = 5;
            this.lblAccept.Text = "Adopted";
            // 
            // lblAcceptPerPeople
            // 
            this.lblAcceptPerPeople.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAcceptPerPeople.AutoSize = true;
            this.lblAcceptPerPeople.BackColor = System.Drawing.SystemColors.Window;
            this.lblAcceptPerPeople.Location = new System.Drawing.Point(3, 92);
            this.lblAcceptPerPeople.Name = "lblAcceptPerPeople";
            this.lblAcceptPerPeople.Size = new System.Drawing.Size(310, 47);
            this.lblAcceptPerPeople.TabIndex = 5;
            this.lblAcceptPerPeople.Text = "Avg Adopt per Person";
            // 
            // chartKaizen
            // 
            this.chartKaizen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartKaizen.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.Name = "ChartArea1";
            this.chartKaizen.ChartAreas.Add(chartArea1);
            this.chartKaizen.Location = new System.Drawing.Point(3, 148);
            this.chartKaizen.Name = "chartKaizen";
            this.chartKaizen.Size = new System.Drawing.Size(396, 180);
            this.chartKaizen.TabIndex = 58;
            this.chartKaizen.Text = "chart1";
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
            this.tableLayoutPanel9.Location = new System.Drawing.Point(1225, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(402, 331);
            this.tableLayoutPanel9.TabIndex = 61;
            // 
            // chartEfficiency
            // 
            this.chartEfficiency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chartEfficiency.ChartAreas.Add(chartArea2);
            this.chartEfficiency.Location = new System.Drawing.Point(3, 135);
            this.chartEfficiency.Name = "chartEfficiency";
            this.chartEfficiency.Size = new System.Drawing.Size(396, 193);
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
            this.tableLayoutPanel10.Size = new System.Drawing.Size(396, 126);
            this.tableLayoutPanel10.TabIndex = 58;
            // 
            // txtPPH
            // 
            this.txtPPH.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPPH.BackColor = System.Drawing.Color.White;
            this.txtPPH.Location = new System.Drawing.Point(319, 66);
            this.txtPPH.Name = "txtPPH";
            this.txtPPH.ReadOnly = true;
            this.txtPPH.Size = new System.Drawing.Size(74, 25);
            this.txtPPH.TabIndex = 26;
            // 
            // lblEfficiencyHeader
            // 
            this.lblEfficiencyHeader.AutoSize = true;
            this.lblEfficiencyHeader.Location = new System.Drawing.Point(3, 0);
            this.lblEfficiencyHeader.Name = "lblEfficiencyHeader";
            this.lblEfficiencyHeader.Size = new System.Drawing.Size(87, 15);
            this.lblEfficiencyHeader.TabIndex = 57;
            this.lblEfficiencyHeader.Text = "Efficiency";
            this.lblEfficiencyHeader.Click += new System.EventHandler(this.lblEfficiencyHeader_Click);
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
            this.lblPPH.Size = new System.Drawing.Size(310, 63);
            this.lblPPH.TabIndex = 25;
            this.lblPPH.Text = "PPH:";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.chartDelivery, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 677);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(401, 332);
            this.tableLayoutPanel7.TabIndex = 60;
            // 
            // chartDelivery
            // 
            this.chartDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chartDelivery.ChartAreas.Add(chartArea3);
            this.chartDelivery.Location = new System.Drawing.Point(3, 145);
            this.chartDelivery.Name = "chartDelivery";
            this.chartDelivery.Size = new System.Drawing.Size(395, 182);
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
            this.tableLayoutPanel8.Size = new System.Drawing.Size(395, 136);
            this.tableLayoutPanel8.TabIndex = 59;
            // 
            // txtSDP
            // 
            this.txtSDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSDP.BackColor = System.Drawing.Color.White;
            this.txtSDP.Location = new System.Drawing.Point(319, 53);
            this.txtSDP.Name = "txtSDP";
            this.txtSDP.ReadOnly = true;
            this.txtSDP.Size = new System.Drawing.Size(73, 25);
            this.txtSDP.TabIndex = 18;
            this.txtSDP.Visible = false;
            // 
            // txtPOCompletionRate
            // 
            this.txtPOCompletionRate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPOCompletionRate.BackColor = System.Drawing.Color.White;
            this.txtPOCompletionRate.Location = new System.Drawing.Point(319, 93);
            this.txtPOCompletionRate.Name = "txtPOCompletionRate";
            this.txtPOCompletionRate.ReadOnly = true;
            this.txtPOCompletionRate.Size = new System.Drawing.Size(73, 25);
            this.txtPOCompletionRate.TabIndex = 46;
            this.txtPOCompletionRate.Click += new System.EventHandler(this.txtPOCompletionRate_Click);
            this.txtPOCompletionRate.TextChanged += new System.EventHandler(this.txtPOCompletionRate_TextChanged);
            // 
            // lblDeliveryHeader
            // 
            this.lblDeliveryHeader.AutoSize = true;
            this.lblDeliveryHeader.Location = new System.Drawing.Point(3, 0);
            this.lblDeliveryHeader.Name = "lblDeliveryHeader";
            this.lblDeliveryHeader.Size = new System.Drawing.Size(71, 15);
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
            this.lblPOCompletionRate.Location = new System.Drawing.Point(3, 90);
            this.lblPOCompletionRate.Name = "lblPOCompletionRate";
            this.lblPOCompletionRate.Size = new System.Drawing.Size(310, 46);
            this.lblPOCompletionRate.TabIndex = 45;
            this.lblPOCompletionRate.Text = "PO Completion Rate (100%)";
            this.lblPOCompletionRate.Click += new System.EventHandler(this.lblPOCompletionRate_Click);
            // 
            // btnKPI
            // 
            this.btnKPI.AutoSize = true;
            this.btnKPI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnKPI.Depth = 0;
            this.btnKPI.Icon = null;
            this.btnKPI.Location = new System.Drawing.Point(319, 3);
            this.btnKPI.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnKPI.Name = "btnKPI";
            this.btnKPI.Primary = true;
            this.btnKPI.Size = new System.Drawing.Size(49, 36);
            this.btnKPI.TabIndex = 35;
            this.btnKPI.Text = "KPI";
            this.btnKPI.UseVisualStyleBackColor = true;
            this.btnKPI.Click += new System.EventHandler(this.btnKPI_Click);
            // 
            // lblSDP
            // 
            this.lblSDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSDP.AutoSize = true;
            this.lblSDP.BackColor = System.Drawing.SystemColors.Window;
            this.lblSDP.Location = new System.Drawing.Point(3, 50);
            this.lblSDP.Name = "lblSDP";
            this.lblSDP.Size = new System.Drawing.Size(310, 40);
            this.lblSDP.TabIndex = 17;
            this.lblSDP.Text = "SDP";
            this.lblSDP.Visible = false;
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
            this.tableLayoutPanel5.Size = new System.Drawing.Size(401, 331);
            this.tableLayoutPanel5.TabIndex = 58;
            // 
            // chartQuality
            // 
            this.chartQuality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea4.Name = "ChartArea1";
            this.chartQuality.ChartAreas.Add(chartArea4);
            this.chartQuality.Location = new System.Drawing.Point(3, 135);
            this.chartQuality.Name = "chartQuality";
            this.chartQuality.Size = new System.Drawing.Size(395, 182);
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
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 69);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(395, 60);
            this.tableLayoutPanel6.TabIndex = 58;
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
            this.lblRFT.Size = new System.Drawing.Size(310, 60);
            this.lblRFT.TabIndex = 13;
            this.lblRFT.Text = "Average RFT";
            // 
            // txtQuality
            // 
            this.txtQuality.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuality.BackColor = System.Drawing.Color.White;
            this.txtQuality.Location = new System.Drawing.Point(319, 3);
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.ReadOnly = true;
            this.txtQuality.Size = new System.Drawing.Size(73, 25);
            this.txtQuality.TabIndex = 14;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel14.Controls.Add(this.cbbRFTType, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.lblQualityHeader, 0, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(395, 60);
            this.tableLayoutPanel14.TabIndex = 59;
            // 
            // cbbRFTType
            // 
            this.cbbRFTType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbRFTType.FormattingEnabled = true;
            this.cbbRFTType.Items.AddRange(new object[] {
            "",
            "L",
            "S",
            "C",
            "T"});
            this.cbbRFTType.Location = new System.Drawing.Point(319, 3);
            this.cbbRFTType.Name = "cbbRFTType";
            this.cbbRFTType.Size = new System.Drawing.Size(73, 23);
            this.cbbRFTType.TabIndex = 59;
            // 
            // lblQualityHeader
            // 
            this.lblQualityHeader.AutoSize = true;
            this.lblQualityHeader.Location = new System.Drawing.Point(3, 0);
            this.lblQualityHeader.Name = "lblQualityHeader";
            this.lblQualityHeader.Size = new System.Drawing.Size(63, 15);
            this.lblQualityHeader.TabIndex = 56;
            this.lblQualityHeader.Text = "Quality";
            this.lblQualityHeader.Click += new System.EventHandler(this.lblQualityHeader_Click);
            // 
            // gridKZAP
            // 
            this.gridKZAP.AllowUserToAddRows = false;
            this.gridKZAP.AllowUserToDeleteRows = false;
            this.gridKZAP.AllowUserToResizeColumns = false;
            this.gridKZAP.AllowUserToResizeRows = false;
            this.gridKZAP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridKZAP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
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
            this.gridKZAP.Size = new System.Drawing.Size(815, 675);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(401, 331);
            this.tableLayoutPanel2.TabIndex = 57;
            // 
            // lblSafetyHeader
            // 
            this.lblSafetyHeader.AutoSize = true;
            this.lblSafetyHeader.Location = new System.Drawing.Point(3, 0);
            this.lblSafetyHeader.Name = "lblSafetyHeader";
            this.lblSafetyHeader.Size = new System.Drawing.Size(55, 15);
            this.lblSafetyHeader.TabIndex = 54;
            this.lblSafetyHeader.Text = "Safety";
            this.lblSafetyHeader.Click += new System.EventHandler(this.lblSafetyHeader_Click);
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
            this.lblSafetyUntilDate.Size = new System.Drawing.Size(314, 82);
            this.lblSafetyUntilDate.TabIndex = 4;
            this.lblSafetyUntilDate.Text = "Work injury YTD";
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
            this.lblSafetyByDate.Size = new System.Drawing.Size(314, 82);
            this.lblSafetyByDate.TabIndex = 4;
            this.lblSafetyByDate.Text = "Work injury on date";
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
            this.lblSafetyDays.Size = new System.Drawing.Size(314, 85);
            this.lblSafetyDays.TabIndex = 4;
            this.lblSafetyDays.Text = "Days without work injury";
            // 
            // txtSafetyDays
            // 
            this.txtSafetyDays.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyDays.BackColor = System.Drawing.Color.White;
            this.txtSafetyDays.Location = new System.Drawing.Point(323, 249);
            this.txtSafetyDays.Name = "txtSafetyDays";
            this.txtSafetyDays.ReadOnly = true;
            this.txtSafetyDays.Size = new System.Drawing.Size(75, 25);
            this.txtSafetyDays.TabIndex = 6;
            // 
            // txtSafetyByDate
            // 
            this.txtSafetyByDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyByDate.BackColor = System.Drawing.Color.White;
            this.txtSafetyByDate.Location = new System.Drawing.Point(323, 167);
            this.txtSafetyByDate.Name = "txtSafetyByDate";
            this.txtSafetyByDate.ReadOnly = true;
            this.txtSafetyByDate.Size = new System.Drawing.Size(75, 25);
            this.txtSafetyByDate.TabIndex = 6;
            // 
            // txtSafetyUntilDate
            // 
            this.txtSafetyUntilDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSafetyUntilDate.BackColor = System.Drawing.Color.White;
            this.txtSafetyUntilDate.Location = new System.Drawing.Point(323, 85);
            this.txtSafetyUntilDate.Name = "txtSafetyUntilDate";
            this.txtSafetyUntilDate.ReadOnly = true;
            this.txtSafetyUntilDate.Size = new System.Drawing.Size(75, 25);
            this.txtSafetyUntilDate.TabIndex = 6;
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-4, 69);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1630, 1012);
            this.tableLayoutPanel1.TabIndex = 53;
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(410, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(809, 331);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.lblTier4Header);
            this.panel2.Controls.Add(this.txtDepartment);
            this.panel2.Controls.Add(this.date);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 96);
            this.panel2.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Depth = 0;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(598, 48);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Primary = true;
            this.btnSearch.Size = new System.Drawing.Size(76, 36);
            this.btnSearch.TabIndex = 34;
            this.btnSearch.Text = "query";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblTier4Header
            // 
            this.lblTier4Header.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTier4Header.AutoSize = true;
            this.lblTier4Header.Location = new System.Drawing.Point(221, 6);
            this.lblTier4Header.Name = "lblTier4Header";
            this.lblTier4Header.Size = new System.Drawing.Size(191, 15);
            this.lblTier4Header.TabIndex = 53;
            this.lblTier4Header.Text = "Tier 4 Visual Dashboard";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDepartment.Location = new System.Drawing.Point(429, 56);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(122, 25);
            this.txtDepartment.TabIndex = 52;
            this.txtDepartment.DoubleClick += new System.EventHandler(this.txtDepartment_DoubleClick);
            // 
            // date
            // 
            this.date.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.date.Location = new System.Drawing.Point(258, 56);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(122, 25);
            this.date.TabIndex = 27;
            this.date.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            this.date.ValueChanged += new System.EventHandler(this.date_ValueChanged);
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(803, 226);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // chartMainBar
            // 
            this.chartMainBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            this.chartMainBar.ChartAreas.Add(chartArea5);
            this.chartMainBar.Location = new System.Drawing.Point(3, 3);
            this.chartMainBar.Name = "chartMainBar";
            this.chartMainBar.Size = new System.Drawing.Size(395, 220);
            this.chartMainBar.TabIndex = 57;
            this.chartMainBar.Text = "chart1";
            // 
            // chartMainPie
            // 
            this.chartMainPie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea6.Name = "ChartArea1";
            this.chartMainPie.ChartAreas.Add(chartArea6);
            legend1.Name = "Legend1";
            this.chartMainPie.Legends.Add(legend1);
            this.chartMainPie.Location = new System.Drawing.Point(404, 3);
            this.chartMainPie.Name = "chartMainPie";
            this.chartMainPie.Size = new System.Drawing.Size(396, 220);
            this.chartMainPie.TabIndex = 57;
            this.chartMainPie.Text = "chart1";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.panel3.Controls.Add(this.gridKZAP);
            this.panel3.Location = new System.Drawing.Point(407, 337);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 2);
            this.panel3.Size = new System.Drawing.Size(815, 675);
            this.panel3.TabIndex = 64;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1622, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Tier Meeting Level 4";
            this.Load += new System.EventHandler(this.SafetyKaizen_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartKaizen)).EndInit();
            this.tableLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEfficiency)).EndInit();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDelivery)).EndInit();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartQuality)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKZAP)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMainBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMainPie)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialRaisedButton btnMA;
        private MaterialSkin.Controls.MaterialRaisedButton btnKZAP;
        private TextBox txtAcceptPerPeople;
        private TextBox txtAccept;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartKaizen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEfficiency;
        private TextBox txtPPH;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDelivery;
        private TextBox txtSDP;
        private TextBox txtPOCompletionRate;
        private MaterialSkin.Controls.MaterialRaisedButton btnKPI;
        private TextBox txtTotalProduced;
        private TextBox textBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartQuality;
        private TextBox txtQuality;
        private DataGridView gridKZAP;
        private TextBox txtSafetyDays;
        private TextBox txtSafetyByDate;
        private TextBox txtSafetyUntilDate;
        private Panel panel2;
        private MaterialSkin.Controls.MaterialRaisedButton btnSearch;
        private TextBox txtDepartment;
        private DateTimePicker date;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMainBar;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMainPie;
        private Panel panel1;
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
        private CusTableLayoutPanel tableLayoutPanel13;
        private CusLabel lblOtherHeader;
        private CusTableLayoutPanel tableLayoutPanel11;
        private CusTableLayoutPanel tableLayoutPanel12;
        private CusLabel lblKaizenHeader;
        private CusLabel lblAccept;
        private CusLabel lblAcceptPerPeople;
        private CusTableLayoutPanel tableLayoutPanel9;
        private CusTableLayoutPanel tableLayoutPanel10;
        private CusLabel lblEfficiencyHeader;
        private CusLabel lblPPH;
        private CusTableLayoutPanel tableLayoutPanel7;
        private CusTableLayoutPanel tableLayoutPanel8;
        private CusLabel lblDeliveryHeader;
        private CusLabel lblPOCompletionRate;
        private CusLabel lblSDP;
        private CusLabel label9;
        private CusLabel label11;
        private CusTableLayoutPanel tableLayoutPanel5;
        private CusLabel lblQualityHeader;
        private CusTableLayoutPanel tableLayoutPanel6;
        private CusLabel lblRFT;
        private CusTableLayoutPanel tableLayoutPanel2;
        private CusLabel lblSafetyHeader;
        private CusLabel lblSafetyUntilDate;
        private CusLabel lblSafetyByDate;
        private CusLabel lblSafetyDays;
        private CusTableLayoutPanel tableLayoutPanel1;
        private CusTableLayoutPanel tableLayoutPanel3;
        private CusLabel lblTier4Header;
        private CusTableLayoutPanel tableLayoutPanel4;
        private CusLabel lblMainChartPieKZAPTitle;
        private CusLabel lblMainChartBarTitle;
        private CusLabel lblMainChartPiePOTitle;
        private CusLabel lblOpenText;
        private CusLabel lblClosedText;
        private CusLabel lblKaizenChartTitle;
        private CusLabel lblEfficiencyChartTitle;
        private CusLabel lblDeliveryChartTitle;
        private CusLabel lblQualityChartTitle;
        private CusTableLayoutPanel tableLayoutPanel14;
    }
}

