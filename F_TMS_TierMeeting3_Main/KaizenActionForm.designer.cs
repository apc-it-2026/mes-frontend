namespace F_TMS_TierMeeting3_Main
{
    partial class KaizenActionForm
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
            this.gridData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbxLine = new System.Windows.Forms.ComboBox();
            this.btnQuery = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxPlant = new System.Windows.Forms.ComboBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.G_ITIME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_DEPTCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_GREATEDDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_FINDER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_PROBLEMPOINT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_MEASURE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_PRINCTIPAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_PLANDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_FINISHDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_REMARK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDIT = new System.Windows.Forms.DataGridViewButtonColumn();
            this.SAVE = new System.Windows.Forms.DataGridViewButtonColumn();
            this.UPGRADE2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CLOSE = new System.Windows.Forms.DataGridViewButtonColumn();
            this.G_T1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_T2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_T3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.G_T4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.ColumnHeadersHeight = 40;
            this.gridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.G_ITIME,
            this.G_DEPTCODE,
            this.G_GREATEDDATE,
            this.G_FINDER,
            this.G_PROBLEMPOINT,
            this.G_MEASURE,
            this.G_PRINCTIPAL,
            this.G_PLANDATE,
            this.G_FINISHDATE,
            this.G_REMARK,
            this.EDIT,
            this.SAVE,
            this.UPGRADE2,
            this.CLOSE,
            this.G_T1,
            this.G_T2,
            this.G_T3,
            this.G_T4,
            this.ID});
            this.gridData.Location = new System.Drawing.Point(3, 63);
            this.gridData.Name = "gridData";
            this.gridData.RowTemplate.Height = 40;
            this.gridData.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridData.Size = new System.Drawing.Size(1691, 554);
            this.gridData.TabIndex = 4;
            this.gridData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellClick);
            this.gridData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellContentClick);
            this.gridData.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridData_ColumnHeaderMouseClick);
            this.gridData.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.gridData_ColumnWidthChanged);
            this.gridData.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridData_DefaultValuesNeeded);
            this.gridData.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gridData_Scroll);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridData, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1697, 640);
            this.tableLayoutPanel1.TabIndex = 10009;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.cbxLine);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbxSection);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbxPlant);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1691, 54);
            this.panel1.TabIndex = 6;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(323, 16);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 10004;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Window;
            this.label7.Location = new System.Drawing.Point(1087, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 10014;
            this.label7.Text = "Line";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(49, 16);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 10003;
            // 
            // cbxLine
            // 
            this.cbxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLine.FormattingEnabled = true;
            this.cbxLine.Location = new System.Drawing.Point(1138, 16);
            this.cbxLine.Name = "cbxLine";
            this.cbxLine.Size = new System.Drawing.Size(74, 21);
            this.cbxLine.TabIndex = 10013;
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSize = true;
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.Depth = 0;
            this.btnQuery.Icon = null;
            this.btnQuery.Location = new System.Drawing.Point(1478, 10);
            this.btnQuery.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Primary = true;
            this.btnQuery.Size = new System.Drawing.Size(64, 36);
            this.btnQuery.TabIndex = 10005;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(817, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 10012;
            this.label5.Text = "Section";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 10006;
            this.label1.Text = "From";
            // 
            // cbxSection
            // 
            this.cbxSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSection.FormattingEnabled = true;
            this.cbxSection.Location = new System.Drawing.Point(872, 16);
            this.cbxSection.Name = "cbxSection";
            this.cbxSection.Size = new System.Drawing.Size(54, 21);
            this.cbxSection.TabIndex = 10011;
            this.cbxSection.TextChanged += new System.EventHandler(this.cbxSection_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 10007;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(580, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 10010;
            this.label4.Text = "Plant";
            this.label4.Visible = false;
            // 
            // cbxPlant
            // 
            this.cbxPlant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPlant.FormattingEnabled = true;
            this.cbxPlant.Location = new System.Drawing.Point(635, 16);
            this.cbxPlant.Name = "cbxPlant";
            this.cbxPlant.Size = new System.Drawing.Size(58, 21);
            this.cbxPlant.TabIndex = 10009;
            this.cbxPlant.Visible = false;
            this.cbxPlant.TextChanged += new System.EventHandler(this.cbxPlant_TextChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(269, 827);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 10010;
            // 
            // G_ITIME
            // 
            this.G_ITIME.DataPropertyName = "G_ITIME";
            this.G_ITIME.HeaderText = "Item(项次)*";
            this.G_ITIME.Name = "G_ITIME";
            this.G_ITIME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_ITIME.Width = 150;
            // 
            // G_DEPTCODE
            // 
            this.G_DEPTCODE.DataPropertyName = "G_DEPTCODE";
            this.G_DEPTCODE.HeaderText = "Department( 部门)*";
            this.G_DEPTCODE.Name = "G_DEPTCODE";
            this.G_DEPTCODE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_DEPTCODE.Width = 200;
            // 
            // G_GREATEDDATE
            // 
            this.G_GREATEDDATE.DataPropertyName = "G_GREATEDDATE";
            this.G_GREATEDDATE.HeaderText = "Created date(日期)";
            this.G_GREATEDDATE.Name = "G_GREATEDDATE";
            this.G_GREATEDDATE.ReadOnly = true;
            this.G_GREATEDDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_GREATEDDATE.Width = 200;
            // 
            // G_FINDER
            // 
            this.G_FINDER.DataPropertyName = "G_FINDER";
            this.G_FINDER.HeaderText = "Finder(发现人)";
            this.G_FINDER.Name = "G_FINDER";
            this.G_FINDER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_FINDER.Width = 150;
            // 
            // G_PROBLEMPOINT
            // 
            this.G_PROBLEMPOINT.DataPropertyName = "G_PROBLEMPOINT";
            this.G_PROBLEMPOINT.HeaderText = "Problem point(问题点 )";
            this.G_PROBLEMPOINT.Name = "G_PROBLEMPOINT";
            this.G_PROBLEMPOINT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_PROBLEMPOINT.Width = 400;
            // 
            // G_MEASURE
            // 
            this.G_MEASURE.DataPropertyName = "G_MEASURE";
            this.G_MEASURE.HeaderText = "Measure(改善对策 )";
            this.G_MEASURE.Name = "G_MEASURE";
            this.G_MEASURE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_MEASURE.Width = 400;
            // 
            // G_PRINCTIPAL
            // 
            this.G_PRINCTIPAL.DataPropertyName = "G_PRINCTIPAL";
            this.G_PRINCTIPAL.HeaderText = "Principal(负责人)";
            this.G_PRINCTIPAL.Name = "G_PRINCTIPAL";
            this.G_PRINCTIPAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_PRINCTIPAL.Width = 200;
            // 
            // G_PLANDATE
            // 
            this.G_PLANDATE.DataPropertyName = "G_PLANDATE";
            this.G_PLANDATE.HeaderText = "Plan date(计划完成日)";
            this.G_PLANDATE.Name = "G_PLANDATE";
            this.G_PLANDATE.ReadOnly = true;
            this.G_PLANDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_PLANDATE.Width = 200;
            // 
            // G_FINISHDATE
            // 
            this.G_FINISHDATE.DataPropertyName = "G_FINISHDATE";
            this.G_FINISHDATE.HeaderText = "Finish date(实际完成日)";
            this.G_FINISHDATE.Name = "G_FINISHDATE";
            this.G_FINISHDATE.ReadOnly = true;
            this.G_FINISHDATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_FINISHDATE.Width = 250;
            // 
            // G_REMARK
            // 
            this.G_REMARK.DataPropertyName = "G_REMARK";
            this.G_REMARK.HeaderText = "Remark(备注)";
            this.G_REMARK.Name = "G_REMARK";
            this.G_REMARK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_REMARK.Width = 300;
            // 
            // EDIT
            // 
            this.EDIT.HeaderText = "Edit";
            this.EDIT.Name = "EDIT";
            this.EDIT.Text = "Edit( 编辑)";
            this.EDIT.UseColumnTextForButtonValue = true;
            // 
            // SAVE
            // 
            this.SAVE.HeaderText = "Save";
            this.SAVE.Name = "SAVE";
            this.SAVE.Text = "Save( 保存)";
            this.SAVE.UseColumnTextForButtonValue = true;
            // 
            // UPGRADE2
            // 
            this.UPGRADE2.HeaderText = "Upgrade";
            this.UPGRADE2.Name = "UPGRADE2";
            this.UPGRADE2.Text = "Upgrade";
            this.UPGRADE2.UseColumnTextForButtonValue = true;
            this.UPGRADE2.Width = 120;
            // 
            // CLOSE
            // 
            this.CLOSE.HeaderText = "Close";
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Text = "Close";
            this.CLOSE.UseColumnTextForButtonValue = true;
            // 
            // G_T1
            // 
            this.G_T1.DataPropertyName = "G_T1";
            this.G_T1.HeaderText = "T1";
            this.G_T1.Name = "G_T1";
            this.G_T1.ReadOnly = true;
            this.G_T1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.G_T1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_T1.Width = 50;
            // 
            // G_T2
            // 
            this.G_T2.DataPropertyName = "G_T2";
            this.G_T2.HeaderText = "T2";
            this.G_T2.Name = "G_T2";
            this.G_T2.ReadOnly = true;
            this.G_T2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.G_T2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_T2.Width = 50;
            // 
            // G_T3
            // 
            this.G_T3.DataPropertyName = "G_T3";
            this.G_T3.HeaderText = "T3";
            this.G_T3.Name = "G_T3";
            this.G_T3.ReadOnly = true;
            this.G_T3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.G_T3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_T3.Width = 50;
            // 
            // G_T4
            // 
            this.G_T4.DataPropertyName = "G_T4";
            this.G_T4.HeaderText = "T4";
            this.G_T4.Name = "G_T4";
            this.G_T4.ReadOnly = true;
            this.G_T4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.G_T4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.G_T4.Width = 50;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Visible = false;
            // 
            // KaizenActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1701, 731);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "KaizenActionForm";
            this.Text = "Kaizen Action";
            this.Load += new System.EventHandler(this.KaizenAction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbxLine;
        private MaterialSkin.Controls.MaterialRaisedButton btnQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxPlant;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_ITIME;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_DEPTCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_GREATEDDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_FINDER;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_PROBLEMPOINT;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_MEASURE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_PRINCTIPAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_PLANDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_FINISHDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_REMARK;
        private System.Windows.Forms.DataGridViewButtonColumn EDIT;
        private System.Windows.Forms.DataGridViewButtonColumn SAVE;
        private System.Windows.Forms.DataGridViewButtonColumn UPGRADE2;
        private System.Windows.Forms.DataGridViewButtonColumn CLOSE;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_T1;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_T2;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_T3;
        private System.Windows.Forms.DataGridViewTextBoxColumn G_T4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    }
}