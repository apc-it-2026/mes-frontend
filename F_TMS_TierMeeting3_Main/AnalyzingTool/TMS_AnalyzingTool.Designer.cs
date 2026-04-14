
namespace F_TMS_TierMeeting3_Main
{
    partial class TMS_AnalyzingTool
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.wb = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_PATH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_HEADER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_PROJECT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_FUNCTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_ART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_SHOE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_FILE_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_YEAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_MONTH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_DEPARTMENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_CREATEDDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_CREATEDBY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_UPDATEDDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colG_UPDATEDBY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDepartment = new MaterialSkin.Controls.MaterialLabel();
            this.cbbDepartment = new System.Windows.Forms.ComboBox();
            this.btnQuery = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblLocation = new MaterialSkin.Controls.MaterialLabel();
            this.cbbLocation = new System.Windows.Forms.ComboBox();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.lblHeader = new MaterialSkin.Controls.MaterialLabel();
            this.txtShoeType = new System.Windows.Forms.TextBox();
            this.lblShoeType = new MaterialSkin.Controls.MaterialLabel();
            this.txtART = new System.Windows.Forms.TextBox();
            this.lblART = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.wb, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 65);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1278, 653);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // wb
            // 
            this.wb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wb.Location = new System.Drawing.Point(3, 303);
            this.wb.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb.Name = "wb";
            this.wb.Size = new System.Drawing.Size(1272, 347);
            this.wb.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.grid, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1272, 294);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colG_PATH,
            this.colG_HEADER,
            this.colG_PROJECT,
            this.colG_FUNCTION,
            this.colG_ART,
            this.colG_SHOE_TYPE,
            this.colG_FILE_TYPE,
            this.colG_YEAR,
            this.colG_MONTH,
            this.colG_DEPARTMENT,
            this.colLocation,
            this.colG_CREATEDDATE,
            this.colG_CREATEDBY,
            this.colG_UPDATEDDATE,
            this.colG_UPDATEDBY});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 18F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.Location = new System.Drawing.Point(639, 3);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 51;
            this.grid.Size = new System.Drawing.Size(630, 288);
            this.grid.TabIndex = 12;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 6;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 125;
            // 
            // colG_PATH
            // 
            this.colG_PATH.DataPropertyName = "G_PATH";
            this.colG_PATH.HeaderText = "File name";
            this.colG_PATH.MinimumWidth = 6;
            this.colG_PATH.Name = "colG_PATH";
            this.colG_PATH.ReadOnly = true;
            this.colG_PATH.Width = 108;
            // 
            // colG_HEADER
            // 
            this.colG_HEADER.DataPropertyName = "G_HEADER";
            this.colG_HEADER.HeaderText = "Header";
            this.colG_HEADER.MinimumWidth = 6;
            this.colG_HEADER.Name = "colG_HEADER";
            this.colG_HEADER.ReadOnly = true;
            this.colG_HEADER.Width = 132;
            // 
            // colG_PROJECT
            // 
            this.colG_PROJECT.DataPropertyName = "G_PROJECT";
            this.colG_PROJECT.HeaderText = "Project";
            this.colG_PROJECT.MinimumWidth = 6;
            this.colG_PROJECT.Name = "colG_PROJECT";
            this.colG_PROJECT.ReadOnly = true;
            this.colG_PROJECT.Visible = false;
            this.colG_PROJECT.Width = 125;
            // 
            // colG_FUNCTION
            // 
            this.colG_FUNCTION.DataPropertyName = "G_FUNCTION";
            this.colG_FUNCTION.HeaderText = "Function";
            this.colG_FUNCTION.MinimumWidth = 6;
            this.colG_FUNCTION.Name = "colG_FUNCTION";
            this.colG_FUNCTION.ReadOnly = true;
            this.colG_FUNCTION.Visible = false;
            this.colG_FUNCTION.Width = 125;
            // 
            // colG_ART
            // 
            this.colG_ART.DataPropertyName = "G_ART";
            this.colG_ART.HeaderText = "ART";
            this.colG_ART.MinimumWidth = 6;
            this.colG_ART.Name = "colG_ART";
            this.colG_ART.ReadOnly = true;
            this.colG_ART.Width = 87;
            // 
            // colG_SHOE_TYPE
            // 
            this.colG_SHOE_TYPE.DataPropertyName = "G_SHOE_TYPE";
            this.colG_SHOE_TYPE.HeaderText = "Shoe type";
            this.colG_SHOE_TYPE.MinimumWidth = 6;
            this.colG_SHOE_TYPE.Name = "colG_SHOE_TYPE";
            this.colG_SHOE_TYPE.ReadOnly = true;
            this.colG_SHOE_TYPE.Width = 108;
            // 
            // colG_FILE_TYPE
            // 
            this.colG_FILE_TYPE.DataPropertyName = "G_FILE_TYPE";
            this.colG_FILE_TYPE.HeaderText = "File type";
            this.colG_FILE_TYPE.MinimumWidth = 6;
            this.colG_FILE_TYPE.Name = "colG_FILE_TYPE";
            this.colG_FILE_TYPE.ReadOnly = true;
            this.colG_FILE_TYPE.Visible = false;
            this.colG_FILE_TYPE.Width = 125;
            // 
            // colG_YEAR
            // 
            this.colG_YEAR.DataPropertyName = "G_YEAR";
            this.colG_YEAR.HeaderText = "Year";
            this.colG_YEAR.MinimumWidth = 6;
            this.colG_YEAR.Name = "colG_YEAR";
            this.colG_YEAR.ReadOnly = true;
            this.colG_YEAR.Visible = false;
            this.colG_YEAR.Width = 125;
            // 
            // colG_MONTH
            // 
            this.colG_MONTH.DataPropertyName = "G_MONTH";
            this.colG_MONTH.HeaderText = "Month";
            this.colG_MONTH.MinimumWidth = 6;
            this.colG_MONTH.Name = "colG_MONTH";
            this.colG_MONTH.ReadOnly = true;
            this.colG_MONTH.Visible = false;
            this.colG_MONTH.Width = 125;
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
            // colLocation
            // 
            this.colLocation.DataPropertyName = "G_LOCATION";
            this.colLocation.HeaderText = "Location";
            this.colLocation.MinimumWidth = 6;
            this.colLocation.Name = "colLocation";
            this.colLocation.ReadOnly = true;
            this.colLocation.Width = 162;
            // 
            // colG_CREATEDDATE
            // 
            this.colG_CREATEDDATE.DataPropertyName = "G_CREATEDDATE";
            this.colG_CREATEDDATE.HeaderText = "Created date";
            this.colG_CREATEDDATE.MinimumWidth = 6;
            this.colG_CREATEDDATE.Name = "colG_CREATEDDATE";
            this.colG_CREATEDDATE.ReadOnly = true;
            this.colG_CREATEDDATE.Width = 148;
            // 
            // colG_CREATEDBY
            // 
            this.colG_CREATEDBY.DataPropertyName = "G_CREATEDBY";
            this.colG_CREATEDBY.HeaderText = "Created by";
            this.colG_CREATEDBY.MinimumWidth = 6;
            this.colG_CREATEDBY.Name = "colG_CREATEDBY";
            this.colG_CREATEDBY.ReadOnly = true;
            this.colG_CREATEDBY.Width = 148;
            // 
            // colG_UPDATEDDATE
            // 
            this.colG_UPDATEDDATE.DataPropertyName = "G_UPDATEDDATE";
            this.colG_UPDATEDDATE.HeaderText = "Updated date";
            this.colG_UPDATEDDATE.MinimumWidth = 6;
            this.colG_UPDATEDDATE.Name = "colG_UPDATEDDATE";
            this.colG_UPDATEDDATE.ReadOnly = true;
            this.colG_UPDATEDDATE.Visible = false;
            this.colG_UPDATEDDATE.Width = 125;
            // 
            // colG_UPDATEDBY
            // 
            this.colG_UPDATEDBY.DataPropertyName = "G_UPDATEDBY";
            this.colG_UPDATEDBY.HeaderText = "Updated by";
            this.colG_UPDATEDBY.MinimumWidth = 6;
            this.colG_UPDATEDBY.Name = "colG_UPDATEDBY";
            this.colG_UPDATEDBY.ReadOnly = true;
            this.colG_UPDATEDBY.Visible = false;
            this.colG_UPDATEDBY.Width = 125;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.lblDepartment, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbbDepartment, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnQuery, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblLocation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbbLocation, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtHeader, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblHeader, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtShoeType, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblShoeType, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtART, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblART, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 288);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Depth = 0;
            this.lblDepartment.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblDepartment.Location = new System.Drawing.Point(3, 0);
            this.lblDepartment.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(120, 47);
            this.lblDepartment.TabIndex = 10;
            this.lblDepartment.Text = "部门";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbDepartment
            // 
            this.cbbDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbDepartment.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDepartment.FormattingEnabled = true;
            this.cbbDepartment.Location = new System.Drawing.Point(129, 3);
            this.cbbDepartment.Name = "cbbDepartment";
            this.cbbDepartment.Size = new System.Drawing.Size(246, 38);
            this.cbbDepartment.TabIndex = 16;
            // 
            // btnQuery
            // 
            this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuery.AutoSize = true;
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.Depth = 0;
            this.btnQuery.Icon = null;
            this.btnQuery.Location = new System.Drawing.Point(381, 3);
            this.btnQuery.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Primary = true;
            this.btnQuery.Size = new System.Drawing.Size(60, 41);
            this.btnQuery.TabIndex = 18;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLocation.AutoSize = true;
            this.lblLocation.Depth = 0;
            this.lblLocation.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblLocation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLocation.Location = new System.Drawing.Point(3, 47);
            this.lblLocation.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(120, 47);
            this.lblLocation.TabIndex = 11;
            this.lblLocation.Text = "楼面";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbbLocation
            // 
            this.cbbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbbLocation.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbLocation.FormattingEnabled = true;
            this.cbbLocation.Location = new System.Drawing.Point(129, 50);
            this.cbbLocation.Name = "cbbLocation";
            this.cbbLocation.Size = new System.Drawing.Size(246, 38);
            this.cbbLocation.TabIndex = 17;
            // 
            // txtHeader
            // 
            this.txtHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHeader.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHeader.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.Location = new System.Drawing.Point(129, 97);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(246, 42);
            this.txtHeader.TabIndex = 24;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.AutoSize = true;
            this.lblHeader.Depth = 0;
            this.lblHeader.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHeader.Location = new System.Drawing.Point(3, 94);
            this.lblHeader.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(120, 47);
            this.lblHeader.TabIndex = 23;
            this.lblHeader.Text = "标题";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtShoeType
            // 
            this.txtShoeType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShoeType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShoeType.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShoeType.Location = new System.Drawing.Point(129, 144);
            this.txtShoeType.Name = "txtShoeType";
            this.txtShoeType.Size = new System.Drawing.Size(246, 42);
            this.txtShoeType.TabIndex = 26;
            // 
            // lblShoeType
            // 
            this.lblShoeType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShoeType.AutoSize = true;
            this.lblShoeType.Depth = 0;
            this.lblShoeType.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblShoeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblShoeType.Location = new System.Drawing.Point(3, 141);
            this.lblShoeType.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblShoeType.Name = "lblShoeType";
            this.lblShoeType.Size = new System.Drawing.Size(120, 47);
            this.lblShoeType.TabIndex = 25;
            this.lblShoeType.Text = "鞋型";
            this.lblShoeType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtART
            // 
            this.txtART.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtART.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtART.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtART.Location = new System.Drawing.Point(129, 191);
            this.txtART.Name = "txtART";
            this.txtART.Size = new System.Drawing.Size(246, 42);
            this.txtART.TabIndex = 28;
            // 
            // lblART
            // 
            this.lblART.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblART.AutoSize = true;
            this.lblART.Depth = 0;
            this.lblART.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblART.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblART.Location = new System.Drawing.Point(3, 188);
            this.lblART.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblART.Name = "lblART";
            this.lblART.Size = new System.Drawing.Size(120, 47);
            this.lblART.TabIndex = 27;
            this.lblART.Text = "ART";
            this.lblART.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TMS_AnalyzingTool
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "TMS_AnalyzingTool";
            this.Text = "TMS_AnalyzingTool";
            this.Load += new System.EventHandler(this.TMS_AnalyzingTool_Load);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.WebBrowser wb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialLabel lblDepartment;
        private System.Windows.Forms.ComboBox cbbDepartment;
        private System.Windows.Forms.ComboBox cbbLocation;
        private MaterialSkin.Controls.MaterialLabel lblLocation;
        private MaterialSkin.Controls.MaterialRaisedButton btnQuery;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_PATH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_HEADER;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_PROJECT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_FUNCTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_ART;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_SHOE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_FILE_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_YEAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_MONTH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_DEPARTMENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_CREATEDDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_CREATEDBY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_UPDATEDDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colG_UPDATEDBY;
        private System.Windows.Forms.TextBox txtHeader;
        private MaterialSkin.Controls.MaterialLabel lblHeader;
        private System.Windows.Forms.TextBox txtShoeType;
        private MaterialSkin.Controls.MaterialLabel lblShoeType;
        private System.Windows.Forms.TextBox txtART;
        private MaterialSkin.Controls.MaterialLabel lblART;
    }
}