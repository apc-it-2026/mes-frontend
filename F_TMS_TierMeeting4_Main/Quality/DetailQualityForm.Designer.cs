namespace TierMeeting
{
    partial class DetailQualityForm
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
            this.btnSearch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.colPlant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colART = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBadPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRFT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shishi = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lblDate = new CusContorl.CusLabel();
            this.dtp = new System.Windows.Forms.DateTimePicker();
            this.lblHeader = new CusContorl.CusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Depth = 0;
            this.btnSearch.Icon = null;
            this.btnSearch.Location = new System.Drawing.Point(370, 28);
            this.btnSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Primary = true;
            this.btnSearch.Size = new System.Drawing.Size(60, 36);
            this.btnSearch.TabIndex = 54;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AllowUserToDeleteRows = false;
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPlant,
            this.colSection,
            this.colLine,
            this.colTime,
            this.colART,
            this.colPO,
            this.colTotal,
            this.colBad,
            this.colBadPercentage,
            this.colRFT});
            this.gridData.Location = new System.Drawing.Point(3, 103);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.RowHeadersWidth = 51;
            this.gridData.RowTemplate.Height = 40;
            this.gridData.Size = new System.Drawing.Size(1910, 887);
            this.gridData.TabIndex = 55;
            this.gridData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellDoubleClick);
            // 
            // colPlant
            // 
            this.colPlant.DataPropertyName = "PLANT";
            this.colPlant.HeaderText = "工厂";
            this.colPlant.MinimumWidth = 6;
            this.colPlant.Name = "colPlant";
            this.colPlant.ReadOnly = true;
            this.colPlant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSection
            // 
            this.colSection.DataPropertyName = "SECTION";
            this.colSection.HeaderText = "部门";
            this.colSection.MinimumWidth = 6;
            this.colSection.Name = "colSection";
            this.colSection.ReadOnly = true;
            this.colSection.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSection.Visible = false;
            // 
            // colLine
            // 
            this.colLine.DataPropertyName = "LINE";
            this.colLine.HeaderText = "线别";
            this.colLine.MinimumWidth = 6;
            this.colLine.Name = "colLine";
            this.colLine.ReadOnly = true;
            this.colLine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLine.Visible = false;
            // 
            // colTime
            // 
            this.colTime.DataPropertyName = "TIME";
            this.colTime.HeaderText = "Time";
            this.colTime.MinimumWidth = 6;
            this.colTime.Name = "colTime";
            this.colTime.ReadOnly = true;
            this.colTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTime.Visible = false;
            // 
            // colART
            // 
            this.colART.DataPropertyName = "ART";
            this.colART.HeaderText = "ART";
            this.colART.MinimumWidth = 6;
            this.colART.Name = "colART";
            this.colART.ReadOnly = true;
            this.colART.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colART.Visible = false;
            // 
            // colPO
            // 
            this.colPO.DataPropertyName = "PO";
            this.colPO.HeaderText = "PO";
            this.colPO.MinimumWidth = 6;
            this.colPO.Name = "colPO";
            this.colPO.ReadOnly = true;
            this.colPO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPO.Visible = false;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "TOTAL";
            this.colTotal.HeaderText = "总数";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            this.colTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colBad
            // 
            this.colBad.DataPropertyName = "BAD";
            this.colBad.HeaderText = "不良数";
            this.colBad.MinimumWidth = 6;
            this.colBad.Name = "colBad";
            this.colBad.ReadOnly = true;
            this.colBad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colBadPercentage
            // 
            this.colBadPercentage.DataPropertyName = "BADPERCENTAGE";
            this.colBadPercentage.HeaderText = "不良率(%)";
            this.colBadPercentage.MinimumWidth = 6;
            this.colBadPercentage.Name = "colBadPercentage";
            this.colBadPercentage.ReadOnly = true;
            this.colBadPercentage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colRFT
            // 
            this.colRFT.DataPropertyName = "RFT";
            this.colRFT.HeaderText = "RFT(%)";
            this.colRFT.MinimumWidth = 6;
            this.colRFT.Name = "colRFT";
            this.colRFT.ReadOnly = true;
            this.colRFT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridData, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1916, 1013);
            this.tableLayoutPanel1.TabIndex = 56;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.shishi);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.dtp);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1910, 94);
            this.panel1.TabIndex = 0;
            // 
            // shishi
            // 
            this.shishi.AutoSize = true;
            this.shishi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shishi.Depth = 0;
            this.shishi.Icon = null;
            this.shishi.Location = new System.Drawing.Point(489, 28);
            this.shishi.MouseState = MaterialSkin.MouseState.HOVER;
            this.shishi.Name = "shishi";
            this.shishi.Primary = true;
            this.shishi.Size = new System.Drawing.Size(60, 36);
            this.shishi.TabIndex = 58;
            this.shishi.Text = "实时";
            this.shishi.UseVisualStyleBackColor = true;
            this.shishi.Click += new System.EventHandler(this.shishi_Click);
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(41, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(37, 15);
            this.lblDate.TabIndex = 57;
            this.lblDate.Text = "日期";
            // 
            // dtp
            // 
            this.dtp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp.Location = new System.Drawing.Point(123, 28);
            this.dtp.Name = "dtp";
            this.dtp.Size = new System.Drawing.Size(200, 25);
            this.dtp.TabIndex = 56;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(718, 24);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(157, 39);
            this.lblHeader.TabIndex = 55;
            this.lblHeader.Text = "详细信息";
            // 
            // DetailQualityForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DetailQualityForm";
            this.Text = "Detail Quality";
            this.Load += new System.EventHandler(this.DetailKaizenForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gridData;
        private MaterialSkin.Controls.MaterialRaisedButton btnSearch;
        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private CusContorl.CusLabel lblHeader;
        private System.Windows.Forms.DateTimePicker dtp;
        private CusContorl.CusLabel lblDate;
        private MaterialSkin.Controls.MaterialRaisedButton shishi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPlant;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSection;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colART;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBadPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRFT;
    }
}