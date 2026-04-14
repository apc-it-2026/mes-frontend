
namespace TierMeeting
{
    partial class TMS_AttendanceRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new CusContorl.CusTabControl();
            
            this.tabRecord = new CusContorl.CusTabPage();
         
            this.tableLayoutPanel3 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel2 = new CusContorl.CusTableLayoutPanel();
            this.dtpRecord = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new MaterialSkin.Controls.MaterialLabel();
            this.btnQueryRecord = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSaveRecord = new MaterialSkin.Controls.MaterialRaisedButton();
            this.gridRecord = new System.Windows.Forms.DataGridView();
            this.colG_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_ATTENDANCE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_DEPARTMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_DATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_ATTENDANCE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colG_LATENESS = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colG_LEAVE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colG_ABSENCE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabSummary = new CusContorl.CusTabPage();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.tableLayoutPanel4 = new CusContorl.CusTableLayoutPanel();
            this.btnQuerySummary = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dtpSummary = new System.Windows.Forms.DateTimePicker();
            this.lblDateSummary = new MaterialSkin.Controls.MaterialLabel();
            this.gridSummary = new System.Windows.Forms.DataGridView();
            this.colG_NAMEsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_DEPARTMENTsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_ATTENDANCEsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_LATENESSsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_LEAVEsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_ABSENCEsummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAttendance = new CusContorl.CusTabPage();
            this.tableLayoutPanel5 = new CusContorl.CusTableLayoutPanel();
            this.gridAttendance = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCbx = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDepartment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel6 = new CusContorl.CusTableLayoutPanel();
            this.lblIsActiveAttendance = new MaterialSkin.Controls.MaterialLabel();
            this.lblNameAttendance = new MaterialSkin.Controls.MaterialLabel();
            this.txtNameAttendance = new System.Windows.Forms.TextBox();
            this.lblDepartmentAttendance = new MaterialSkin.Controls.MaterialLabel();
            this.cbbDepartmentAttendance = new System.Windows.Forms.ComboBox();
            this.cbxIsActiveAttendance = new System.Windows.Forms.CheckBox();
            this.btnQueryAttendance = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnSaveAttendance = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnEditAttendance = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnAddAttendance = new MaterialSkin.Controls.MaterialRaisedButton();
            this.btnDeleteAttendance = new MaterialSkin.Controls.MaterialRaisedButton();
            this.tabControl1.SuspendLayout();
            this.tabRecord.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecord)).BeginInit();
            this.tabSummary.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).BeginInit();
            this.tabAttendance.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabRecord);
            this.tabControl1.Controls.Add(this.tabSummary);
            this.tabControl1.Controls.Add(this.tabAttendance);
            this.tabControl1.Location = new System.Drawing.Point(1, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1919, 1008);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabRecord
            // 
            this.tabRecord.Controls.Add(this.tableLayoutPanel3);
            this.tabRecord.Location = new System.Drawing.Point(4, 25);
            this.tabRecord.Name = "tabRecord";
            this.tabRecord.Padding = new System.Windows.Forms.Padding(3);
            this.tabRecord.Size = new System.Drawing.Size(1911, 979);
            this.tabRecord.TabIndex = 0;
            this.tabRecord.Text = "Record";
            this.tabRecord.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.gridRecord, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1905, 977);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.dtpRecord, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDate, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnQueryRecord, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveRecord, 5, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1899, 44);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dtpRecord
            // 
            this.dtpRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRecord.Font = new System.Drawing.Font("宋体", 18F);
            this.dtpRecord.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecord.Location = new System.Drawing.Point(666, 3);
            this.dtpRecord.Name = "dtpRecord";
            this.dtpRecord.Size = new System.Drawing.Size(373, 42);
            this.dtpRecord.TabIndex = 19;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Depth = 0;
            this.lblDate.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDate.Location = new System.Drawing.Point(524, 0);
            this.lblDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(136, 44);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnQueryRecord
            // 
            this.btnQueryRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQueryRecord.AutoSize = true;
            this.btnQueryRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQueryRecord.Depth = 0;
            this.btnQueryRecord.Icon = null;
            this.btnQueryRecord.Location = new System.Drawing.Point(1045, 3);
            this.btnQueryRecord.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQueryRecord.Name = "btnQueryRecord";
            this.btnQueryRecord.Primary = true;
            this.btnQueryRecord.Size = new System.Drawing.Size(76, 38);
            this.btnQueryRecord.TabIndex = 18;
            this.btnQueryRecord.Text = "Query";
            this.btnQueryRecord.UseVisualStyleBackColor = true;
            this.btnQueryRecord.Click += new System.EventHandler(this.btnQueryRecord_Click);
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveRecord.AutoSize = true;
            this.btnSaveRecord.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveRecord.Depth = 0;
            this.btnSaveRecord.Icon = null;
            this.btnSaveRecord.Location = new System.Drawing.Point(1831, 3);
            this.btnSaveRecord.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Primary = true;
            this.btnSaveRecord.Size = new System.Drawing.Size(65, 38);
            this.btnSaveRecord.TabIndex = 20;
            this.btnSaveRecord.Text = "Save";
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // gridRecord
            // 
            this.gridRecord.AllowUserToAddRows = false;
            this.gridRecord.AllowUserToDeleteRows = false;
            this.gridRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRecord.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridRecord.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRecord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colG_NAME,
            this.colG_ATTENDANCE_ID,
            this.colG_DEPARTMENT,
            this.colG_DATE,
            this.colG_ATTENDANCE,
            this.colG_LATENESS,
            this.colG_LEAVE,
            this.colG_ABSENCE});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRecord.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridRecord.Location = new System.Drawing.Point(3, 53);
            this.gridRecord.Name = "gridRecord";
            this.gridRecord.RowHeadersWidth = 51;
            this.gridRecord.Size = new System.Drawing.Size(1899, 921);
            this.gridRecord.TabIndex = 2;
            this.gridRecord.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRecord_CellContentClick);
            // 
            // colG_NAME
            // 
            this.colG_NAME.DataPropertyName = "G_NAME";
            this.colG_NAME.HeaderText = "Name";
            this.colG_NAME.MinimumWidth = 6;
            this.colG_NAME.Name = "colG_NAME";
            this.colG_NAME.ReadOnly = true;
            this.colG_NAME.Width = 102;
            // 
            // colG_ATTENDANCE_ID
            // 
            this.colG_ATTENDANCE_ID.DataPropertyName = "G_ATTENDANCE_ID";
            this.colG_ATTENDANCE_ID.HeaderText = "ID";
            this.colG_ATTENDANCE_ID.MinimumWidth = 6;
            this.colG_ATTENDANCE_ID.Name = "colG_ATTENDANCE_ID";
            this.colG_ATTENDANCE_ID.ReadOnly = true;
            this.colG_ATTENDANCE_ID.Visible = false;
            this.colG_ATTENDANCE_ID.Width = 59;
            // 
            // colG_DEPARTMENT
            // 
            this.colG_DEPARTMENT.DataPropertyName = "G_DEPARTMENT";
            this.colG_DEPARTMENT.HeaderText = "Department";
            this.colG_DEPARTMENT.MinimumWidth = 6;
            this.colG_DEPARTMENT.Name = "colG_DEPARTMENT";
            this.colG_DEPARTMENT.ReadOnly = true;
            this.colG_DEPARTMENT.Width = 192;
            // 
            // colG_DATE
            // 
            this.colG_DATE.DataPropertyName = "G_DATE";
            this.colG_DATE.HeaderText = "Date";
            this.colG_DATE.MinimumWidth = 6;
            this.colG_DATE.Name = "colG_DATE";
            this.colG_DATE.ReadOnly = true;
            this.colG_DATE.Width = 102;
            // 
            // colG_ATTENDANCE
            // 
            this.colG_ATTENDANCE.DataPropertyName = "G_ATTENDANCE";
            this.colG_ATTENDANCE.HeaderText = "Attendance";
            this.colG_ATTENDANCE.MinimumWidth = 6;
            this.colG_ATTENDANCE.Name = "colG_ATTENDANCE";
            this.colG_ATTENDANCE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colG_ATTENDANCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colG_ATTENDANCE.Width = 192;
            // 
            // colG_LATENESS
            // 
            this.colG_LATENESS.DataPropertyName = "G_LATENESS";
            this.colG_LATENESS.HeaderText = "Lateness";
            this.colG_LATENESS.MinimumWidth = 6;
            this.colG_LATENESS.Name = "colG_LATENESS";
            this.colG_LATENESS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colG_LATENESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colG_LATENESS.Width = 162;
            // 
            // colG_LEAVE
            // 
            this.colG_LEAVE.DataPropertyName = "G_LEAVE";
            this.colG_LEAVE.HeaderText = "Leave";
            this.colG_LEAVE.MinimumWidth = 6;
            this.colG_LEAVE.Name = "colG_LEAVE";
            this.colG_LEAVE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colG_LEAVE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colG_LEAVE.Width = 117;
            // 
            // colG_ABSENCE
            // 
            this.colG_ABSENCE.DataPropertyName = "G_ABSENCE";
            this.colG_ABSENCE.HeaderText = "Absence";
            this.colG_ABSENCE.MinimumWidth = 6;
            this.colG_ABSENCE.Name = "colG_ABSENCE";
            this.colG_ABSENCE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colG_ABSENCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colG_ABSENCE.Width = 147;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.tableLayoutPanel1);
            this.tabSummary.Location = new System.Drawing.Point(4, 25);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(1911, 979);
            this.tabSummary.TabIndex = 1;
            this.tabSummary.Text = "Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridSummary, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1905, 973);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel4.Controls.Add(this.btnQuerySummary, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.dtpSummary, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblDateSummary, 2, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1899, 44);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnQuerySummary
            // 
            this.btnQuerySummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuerySummary.AutoSize = true;
            this.btnQuerySummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuerySummary.Depth = 0;
            this.btnQuerySummary.Icon = null;
            this.btnQuerySummary.Location = new System.Drawing.Point(1045, 3);
            this.btnQuerySummary.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQuerySummary.Name = "btnQuerySummary";
            this.btnQuerySummary.Primary = true;
            this.btnQuerySummary.Size = new System.Drawing.Size(76, 38);
            this.btnQuerySummary.TabIndex = 18;
            this.btnQuerySummary.Text = "Query";
            this.btnQuerySummary.UseVisualStyleBackColor = true;
            this.btnQuerySummary.Click += new System.EventHandler(this.btnQuerySummary_Click);
            // 
            // dtpSummary
            // 
            this.dtpSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpSummary.Font = new System.Drawing.Font("宋体", 18F);
            this.dtpSummary.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSummary.Location = new System.Drawing.Point(666, 3);
            this.dtpSummary.Name = "dtpSummary";
            this.dtpSummary.Size = new System.Drawing.Size(373, 42);
            this.dtpSummary.TabIndex = 20;
            // 
            // lblDateSummary
            // 
            this.lblDateSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateSummary.AutoSize = true;
            this.lblDateSummary.Depth = 0;
            this.lblDateSummary.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblDateSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDateSummary.Location = new System.Drawing.Point(524, 0);
            this.lblDateSummary.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDateSummary.Name = "lblDateSummary";
            this.lblDateSummary.Size = new System.Drawing.Size(136, 44);
            this.lblDateSummary.TabIndex = 21;
            this.lblDateSummary.Text = "Date";
            this.lblDateSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gridSummary
            // 
            this.gridSummary.AllowUserToAddRows = false;
            this.gridSummary.AllowUserToDeleteRows = false;
            this.gridSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridSummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridSummary.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSummary.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colG_NAMEsummary,
            this.colG_DEPARTMENTsummary,
            this.colG_ATTENDANCEsummary,
            this.colG_LATENESSsummary,
            this.colG_LEAVEsummary,
            this.colG_ABSENCEsummary});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSummary.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridSummary.Location = new System.Drawing.Point(3, 53);
            this.gridSummary.Name = "gridSummary";
            this.gridSummary.ReadOnly = true;
            this.gridSummary.RowHeadersWidth = 51;
            this.gridSummary.Size = new System.Drawing.Size(1899, 917);
            this.gridSummary.TabIndex = 2;
            // 
            // colG_NAMEsummary
            // 
            this.colG_NAMEsummary.DataPropertyName = "G_NAME";
            this.colG_NAMEsummary.HeaderText = "Name";
            this.colG_NAMEsummary.MinimumWidth = 6;
            this.colG_NAMEsummary.Name = "colG_NAMEsummary";
            this.colG_NAMEsummary.ReadOnly = true;
            this.colG_NAMEsummary.Width = 102;
            // 
            // colG_DEPARTMENTsummary
            // 
            this.colG_DEPARTMENTsummary.DataPropertyName = "G_DEPARTMENT";
            this.colG_DEPARTMENTsummary.HeaderText = "Department";
            this.colG_DEPARTMENTsummary.MinimumWidth = 6;
            this.colG_DEPARTMENTsummary.Name = "colG_DEPARTMENTsummary";
            this.colG_DEPARTMENTsummary.ReadOnly = true;
            this.colG_DEPARTMENTsummary.Width = 192;
            // 
            // colG_ATTENDANCEsummary
            // 
            this.colG_ATTENDANCEsummary.DataPropertyName = "G_ATTENDANCE";
            this.colG_ATTENDANCEsummary.HeaderText = "Attendance";
            this.colG_ATTENDANCEsummary.MinimumWidth = 6;
            this.colG_ATTENDANCEsummary.Name = "colG_ATTENDANCEsummary";
            this.colG_ATTENDANCEsummary.ReadOnly = true;
            this.colG_ATTENDANCEsummary.Width = 192;
            // 
            // colG_LATENESSsummary
            // 
            this.colG_LATENESSsummary.DataPropertyName = "G_LATENESS";
            this.colG_LATENESSsummary.HeaderText = "Lateness";
            this.colG_LATENESSsummary.MinimumWidth = 6;
            this.colG_LATENESSsummary.Name = "colG_LATENESSsummary";
            this.colG_LATENESSsummary.ReadOnly = true;
            this.colG_LATENESSsummary.Width = 162;
            // 
            // colG_LEAVEsummary
            // 
            this.colG_LEAVEsummary.DataPropertyName = "G_LEAVE";
            this.colG_LEAVEsummary.HeaderText = "Leave";
            this.colG_LEAVEsummary.MinimumWidth = 6;
            this.colG_LEAVEsummary.Name = "colG_LEAVEsummary";
            this.colG_LEAVEsummary.ReadOnly = true;
            this.colG_LEAVEsummary.Width = 117;
            // 
            // colG_ABSENCEsummary
            // 
            this.colG_ABSENCEsummary.DataPropertyName = "G_ABSENCE";
            this.colG_ABSENCEsummary.HeaderText = "Absence";
            this.colG_ABSENCEsummary.MinimumWidth = 6;
            this.colG_ABSENCEsummary.Name = "colG_ABSENCEsummary";
            this.colG_ABSENCEsummary.ReadOnly = true;
            this.colG_ABSENCEsummary.Width = 147;
            // 
            // tabAttendance
            // 
            this.tabAttendance.Controls.Add(this.tableLayoutPanel5);
            this.tabAttendance.Location = new System.Drawing.Point(4, 25);
            this.tabAttendance.Name = "tabAttendance";
            this.tabAttendance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttendance.Size = new System.Drawing.Size(1911, 979);
            this.tabAttendance.TabIndex = 2;
            this.tabAttendance.Text = "Attendance management";
            this.tabAttendance.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.gridAttendance, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1905, 973);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // gridAttendance
            // 
            this.gridAttendance.AllowUserToAddRows = false;
            this.gridAttendance.AllowUserToDeleteRows = false;
            this.gridAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridAttendance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gridAttendance.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAttendance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridAttendance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colCbx,
            this.colName,
            this.colDepartment,
            this.colIsActive});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridAttendance.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridAttendance.Location = new System.Drawing.Point(3, 343);
            this.gridAttendance.Name = "gridAttendance";
            this.gridAttendance.ReadOnly = true;
            this.gridAttendance.RowHeadersWidth = 51;
            this.gridAttendance.Size = new System.Drawing.Size(1899, 627);
            this.gridAttendance.TabIndex = 3;
            this.gridAttendance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAttendance_CellContentClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 6;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 59;
            // 
            // colCbx
            // 
            this.colCbx.HeaderText = "";
            this.colCbx.MinimumWidth = 6;
            this.colCbx.Name = "colCbx";
            this.colCbx.ReadOnly = true;
            this.colCbx.Width = 6;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "G_NAME";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 102;
            // 
            // colDepartment
            // 
            this.colDepartment.DataPropertyName = "G_DEPARTMENT";
            this.colDepartment.HeaderText = "Department";
            this.colDepartment.MinimumWidth = 6;
            this.colDepartment.Name = "colDepartment";
            this.colDepartment.ReadOnly = true;
            this.colDepartment.Width = 192;
            // 
            // colIsActive
            // 
            this.colIsActive.DataPropertyName = "G_ISACTIVE";
            this.colIsActive.HeaderText = "Is active";
            this.colIsActive.MinimumWidth = 6;
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.ReadOnly = true;
            this.colIsActive.Width = 154;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel6.Controls.Add(this.lblIsActiveAttendance, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.lblNameAttendance, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.txtNameAttendance, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblDepartmentAttendance, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.cbbDepartmentAttendance, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.cbxIsActiveAttendance, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.btnQueryAttendance, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnSaveAttendance, 2, 3);
            this.tableLayoutPanel6.Controls.Add(this.btnEditAttendance, 3, 4);
            this.tableLayoutPanel6.Controls.Add(this.btnAddAttendance, 2, 4);
            this.tableLayoutPanel6.Controls.Add(this.btnDeleteAttendance, 4, 4);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1899, 334);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // lblIsActiveAttendance
            // 
            this.lblIsActiveAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIsActiveAttendance.AutoSize = true;
            this.lblIsActiveAttendance.Depth = 0;
            this.lblIsActiveAttendance.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblIsActiveAttendance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblIsActiveAttendance.Location = new System.Drawing.Point(3, 198);
            this.lblIsActiveAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblIsActiveAttendance.Name = "lblIsActiveAttendance";
            this.lblIsActiveAttendance.Size = new System.Drawing.Size(278, 66);
            this.lblIsActiveAttendance.TabIndex = 9;
            this.lblIsActiveAttendance.Text = "Is active";
            this.lblIsActiveAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameAttendance
            // 
            this.lblNameAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNameAttendance.AutoSize = true;
            this.lblNameAttendance.Depth = 0;
            this.lblNameAttendance.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblNameAttendance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblNameAttendance.Location = new System.Drawing.Point(3, 66);
            this.lblNameAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblNameAttendance.Name = "lblNameAttendance";
            this.lblNameAttendance.Size = new System.Drawing.Size(278, 66);
            this.lblNameAttendance.TabIndex = 9;
            this.lblNameAttendance.Text = "Name";
            this.lblNameAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNameAttendance
            // 
            this.txtNameAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameAttendance.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNameAttendance.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameAttendance.Location = new System.Drawing.Point(287, 69);
            this.txtNameAttendance.Name = "txtNameAttendance";
            this.txtNameAttendance.Size = new System.Drawing.Size(563, 42);
            this.txtNameAttendance.TabIndex = 21;
            // 
            // lblDepartmentAttendance
            // 
            this.lblDepartmentAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepartmentAttendance.AutoSize = true;
            this.lblDepartmentAttendance.Depth = 0;
            this.lblDepartmentAttendance.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblDepartmentAttendance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDepartmentAttendance.Location = new System.Drawing.Point(3, 132);
            this.lblDepartmentAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDepartmentAttendance.Name = "lblDepartmentAttendance";
            this.lblDepartmentAttendance.Size = new System.Drawing.Size(278, 66);
            this.lblDepartmentAttendance.TabIndex = 22;
            this.lblDepartmentAttendance.Text = "Department";
            this.lblDepartmentAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbDepartmentAttendance
            // 
            this.cbbDepartmentAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbDepartmentAttendance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbDepartmentAttendance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbDepartmentAttendance.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDepartmentAttendance.FormattingEnabled = true;
            this.cbbDepartmentAttendance.Location = new System.Drawing.Point(287, 135);
            this.cbbDepartmentAttendance.Name = "cbbDepartmentAttendance";
            this.cbbDepartmentAttendance.Size = new System.Drawing.Size(563, 38);
            this.cbbDepartmentAttendance.TabIndex = 23;
            // 
            // cbxIsActiveAttendance
            // 
            this.cbxIsActiveAttendance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxIsActiveAttendance.AutoSize = true;
            this.cbxIsActiveAttendance.Checked = true;
            this.cbxIsActiveAttendance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxIsActiveAttendance.Font = new System.Drawing.Font("宋体", 18F);
            this.cbxIsActiveAttendance.Location = new System.Drawing.Point(287, 201);
            this.cbxIsActiveAttendance.Name = "cbxIsActiveAttendance";
            this.cbxIsActiveAttendance.Size = new System.Drawing.Size(563, 60);
            this.cbxIsActiveAttendance.TabIndex = 24;
            this.cbxIsActiveAttendance.UseVisualStyleBackColor = true;
            // 
            // btnQueryAttendance
            // 
            this.btnQueryAttendance.AutoSize = true;
            this.btnQueryAttendance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQueryAttendance.Depth = 0;
            this.btnQueryAttendance.Font = new System.Drawing.Font("宋体", 10F);
            this.btnQueryAttendance.Icon = null;
            this.btnQueryAttendance.Location = new System.Drawing.Point(856, 69);
            this.btnQueryAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQueryAttendance.Name = "btnQueryAttendance";
            this.btnQueryAttendance.Primary = true;
            this.btnQueryAttendance.Size = new System.Drawing.Size(76, 36);
            this.btnQueryAttendance.TabIndex = 25;
            this.btnQueryAttendance.Text = "Query";
            this.btnQueryAttendance.UseVisualStyleBackColor = true;
            this.btnQueryAttendance.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnSaveAttendance
            // 
            this.btnSaveAttendance.AutoSize = true;
            this.btnSaveAttendance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSaveAttendance.Depth = 0;
            this.btnSaveAttendance.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSaveAttendance.Icon = null;
            this.btnSaveAttendance.Location = new System.Drawing.Point(856, 201);
            this.btnSaveAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSaveAttendance.Name = "btnSaveAttendance";
            this.btnSaveAttendance.Primary = true;
            this.btnSaveAttendance.Size = new System.Drawing.Size(65, 36);
            this.btnSaveAttendance.TabIndex = 25;
            this.btnSaveAttendance.Text = "Save";
            this.btnSaveAttendance.UseVisualStyleBackColor = true;
            this.btnSaveAttendance.Visible = false;
            this.btnSaveAttendance.Click += new System.EventHandler(this.btnSaveAttendance_Click);
            // 
            // btnEditAttendance
            // 
            this.btnEditAttendance.AutoSize = true;
            this.btnEditAttendance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEditAttendance.Depth = 0;
            this.btnEditAttendance.Font = new System.Drawing.Font("宋体", 10F);
            this.btnEditAttendance.Icon = null;
            this.btnEditAttendance.Location = new System.Drawing.Point(1045, 267);
            this.btnEditAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnEditAttendance.Name = "btnEditAttendance";
            this.btnEditAttendance.Primary = true;
            this.btnEditAttendance.Size = new System.Drawing.Size(58, 36);
            this.btnEditAttendance.TabIndex = 25;
            this.btnEditAttendance.Text = "Edit";
            this.btnEditAttendance.UseVisualStyleBackColor = true;
            this.btnEditAttendance.Click += new System.EventHandler(this.btnEditAttendance_Click);
            // 
            // btnAddAttendance
            // 
            this.btnAddAttendance.AutoSize = true;
            this.btnAddAttendance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddAttendance.Depth = 0;
            this.btnAddAttendance.Font = new System.Drawing.Font("宋体", 10F);
            this.btnAddAttendance.Icon = null;
            this.btnAddAttendance.Location = new System.Drawing.Point(856, 267);
            this.btnAddAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAddAttendance.Name = "btnAddAttendance";
            this.btnAddAttendance.Primary = true;
            this.btnAddAttendance.Size = new System.Drawing.Size(59, 36);
            this.btnAddAttendance.TabIndex = 25;
            this.btnAddAttendance.Text = "New";
            this.btnAddAttendance.UseVisualStyleBackColor = true;
            this.btnAddAttendance.Click += new System.EventHandler(this.btnAddAttendance_Click);
            // 
            // btnDeleteAttendance
            // 
            this.btnDeleteAttendance.AutoSize = true;
            this.btnDeleteAttendance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDeleteAttendance.Depth = 0;
            this.btnDeleteAttendance.Font = new System.Drawing.Font("宋体", 10F);
            this.btnDeleteAttendance.Icon = null;
            this.btnDeleteAttendance.Location = new System.Drawing.Point(1234, 267);
            this.btnDeleteAttendance.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnDeleteAttendance.Name = "btnDeleteAttendance";
            this.btnDeleteAttendance.Primary = true;
            this.btnDeleteAttendance.Size = new System.Drawing.Size(82, 36);
            this.btnDeleteAttendance.TabIndex = 25;
            this.btnDeleteAttendance.Text = "Delete";
            this.btnDeleteAttendance.UseVisualStyleBackColor = true;
            this.btnDeleteAttendance.Click += new System.EventHandler(this.btnDeleteAttendance_Click);
            // 
            // TMS_AttendanceRecord
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tabControl1);
            this.Name = "TMS_AttendanceRecord";
            this.Text = "TMS_AttendanceRecord";
            this.Load += new System.EventHandler(this.TMS_AttendanceRecord_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabRecord.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecord)).EndInit();
            this.tabSummary.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSummary)).EndInit();
            this.tabAttendance.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAttendance)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CusContorl.CusTabControl tabControl1;
        private CusContorl.CusTabPage tabRecord;
        private CusContorl.CusTabPage tabSummary;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel4;
        private MaterialSkin.Controls.MaterialRaisedButton btnQuerySummary;
        private System.Windows.Forms.DataGridView gridSummary;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel3;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel2;
        private MaterialSkin.Controls.MaterialRaisedButton btnQueryRecord;
        private MaterialSkin.Controls.MaterialLabel lblDate;
        private System.Windows.Forms.DateTimePicker dtpRecord;
        private System.Windows.Forms.DataGridView gridRecord;
        private MaterialSkin.Controls.MaterialRaisedButton btnSaveRecord;
        private System.Windows.Forms.DateTimePicker dtpSummary;
        private MaterialSkin.Controls.MaterialLabel lblDateSummary;
        private CusContorl.CusTabPage tabAttendance;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel5;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel6;
        private MaterialSkin.Controls.MaterialLabel lblNameAttendance;
        private System.Windows.Forms.TextBox txtNameAttendance;
        private MaterialSkin.Controls.MaterialLabel lblDepartmentAttendance;
        private System.Windows.Forms.ComboBox cbbDepartmentAttendance;
        private MaterialSkin.Controls.MaterialLabel lblIsActiveAttendance;
        private System.Windows.Forms.CheckBox cbxIsActiveAttendance;
        private MaterialSkin.Controls.MaterialRaisedButton btnQueryAttendance;
        private System.Windows.Forms.DataGridView gridAttendance;
        private MaterialSkin.Controls.MaterialRaisedButton btnEditAttendance;
        private MaterialSkin.Controls.MaterialRaisedButton btnSaveAttendance;
        private MaterialSkin.Controls.MaterialRaisedButton btnAddAttendance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCbx;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDepartment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActive;
        private MaterialSkin.Controls.MaterialRaisedButton btnDeleteAttendance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_NAMEsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_DEPARTMENTsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_ATTENDANCEsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_LATENESSsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_LEAVEsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_ABSENCEsummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_ATTENDANCE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_DEPARTMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_DATE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colG_ATTENDANCE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colG_LATENESS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colG_LEAVE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colG_ABSENCE;
    }
}