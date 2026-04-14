
namespace WorkingHoursQuery
{
    partial class WorkingHoursQuery
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
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.labelWorkCenter = new System.Windows.Forms.Label();
            this.txtWorkCenter = new System.Windows.Forms.TextBox();
            this.labelFactoryRegion = new System.Windows.Forms.Label();
            this.txtFactoryRegion = new System.Windows.Forms.TextBox();
            this.labelEmpNO = new System.Windows.Forms.Label();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.labelDay = new System.Windows.Forms.Label();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.labelReportType = new System.Windows.Forms.Label();
            this.comboBoxReportType = new System.Windows.Forms.ComboBox();
            this.dataGridViewWorkingHours = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.员工编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.员工姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.厂区 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工作中心 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.班次代号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.打卡次数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.打卡记录 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工时 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewEmpNo = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期由 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期至 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.onlinetime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAutoSync = new System.Windows.Forms.Button();
            this.btnHandHourSync = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkingHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmpNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(42, 47);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(141, 47);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 25);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(242, 47);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 25);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // labelWorkCenter
            // 
            this.labelWorkCenter.AutoSize = true;
            this.labelWorkCenter.Location = new System.Drawing.Point(40, 101);
            this.labelWorkCenter.Name = "labelWorkCenter";
            this.labelWorkCenter.Size = new System.Drawing.Size(67, 13);
            this.labelWorkCenter.TabIndex = 3;
            this.labelWorkCenter.Text = "工作中心：";
            // 
            // txtWorkCenter
            // 
            this.txtWorkCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtWorkCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtWorkCenter.Location = new System.Drawing.Point(112, 92);
            this.txtWorkCenter.Name = "txtWorkCenter";
            this.txtWorkCenter.Size = new System.Drawing.Size(111, 20);
            this.txtWorkCenter.TabIndex = 4;
            // 
            // labelFactoryRegion
            // 
            this.labelFactoryRegion.AutoSize = true;
            this.labelFactoryRegion.Location = new System.Drawing.Point(242, 94);
            this.labelFactoryRegion.Name = "labelFactoryRegion";
            this.labelFactoryRegion.Size = new System.Drawing.Size(43, 13);
            this.labelFactoryRegion.TabIndex = 5;
            this.labelFactoryRegion.Text = "厂区：";
            // 
            // txtFactoryRegion
            // 
            this.txtFactoryRegion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFactoryRegion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFactoryRegion.Location = new System.Drawing.Point(289, 91);
            this.txtFactoryRegion.Name = "txtFactoryRegion";
            this.txtFactoryRegion.Size = new System.Drawing.Size(112, 20);
            this.txtFactoryRegion.TabIndex = 6;
            // 
            // labelEmpNO
            // 
            this.labelEmpNO.AutoSize = true;
            this.labelEmpNO.Location = new System.Drawing.Point(573, 94);
            this.labelEmpNO.Name = "labelEmpNO";
            this.labelEmpNO.Size = new System.Drawing.Size(67, 13);
            this.labelEmpNO.TabIndex = 9;
            this.labelEmpNO.Text = "员工条码：";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.Location = new System.Drawing.Point(644, 88);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(100, 20);
            this.txtEmpNo.TabIndex = 10;
            // 
            // labelDay
            // 
            this.labelDay.AutoSize = true;
            this.labelDay.Location = new System.Drawing.Point(52, 132);
            this.labelDay.Name = "labelDay";
            this.labelDay.Size = new System.Drawing.Size(55, 13);
            this.labelDay.TabIndex = 11;
            this.labelDay.Text = "日期由：";
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(111, 126);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(111, 20);
            this.dateTimePickerStartTime.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "至：";
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(289, 126);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(112, 20);
            this.dateTimePickerEndTime.TabIndex = 14;
            // 
            // labelReportType
            // 
            this.labelReportType.AutoSize = true;
            this.labelReportType.Location = new System.Drawing.Point(575, 132);
            this.labelReportType.Name = "labelReportType";
            this.labelReportType.Size = new System.Drawing.Size(67, 13);
            this.labelReportType.TabIndex = 15;
            this.labelReportType.Text = "报表类型：";
            // 
            // comboBoxReportType
            // 
            this.comboBoxReportType.FormattingEnabled = true;
            this.comboBoxReportType.Items.AddRange(new object[] {
            "List",
            "Aggregate by employee"});
            this.comboBoxReportType.Location = new System.Drawing.Point(644, 132);
            this.comboBoxReportType.Name = "comboBoxReportType";
            this.comboBoxReportType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxReportType.TabIndex = 16;
            // 
            // dataGridViewWorkingHours
            // 
            this.dataGridViewWorkingHours.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWorkingHours.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.员工编号,
            this.员工姓名,
            this.厂区,
            this.工作中心,
            this.日期,
            this.班次代号,
            this.打卡次数,
            this.打卡记录,
            this.工时});
            this.dataGridViewWorkingHours.Location = new System.Drawing.Point(42, 203);
            this.dataGridViewWorkingHours.Name = "dataGridViewWorkingHours";
            this.dataGridViewWorkingHours.RowTemplate.Height = 23;
            this.dataGridViewWorkingHours.Size = new System.Drawing.Size(1079, 476);
            this.dataGridViewWorkingHours.TabIndex = 17;
            // 
            // 序号
            // 
            this.序号.DataPropertyName = "No";
            this.序号.HeaderText = "序号";
            this.序号.Name = "序号";
            this.序号.Width = 80;
            // 
            // 员工编号
            // 
            this.员工编号.DataPropertyName = "EmpNo";
            this.员工编号.HeaderText = "员工编号";
            this.员工编号.Name = "员工编号";
            // 
            // 员工姓名
            // 
            this.员工姓名.DataPropertyName = "EmpName";
            this.员工姓名.HeaderText = "员工姓名";
            this.员工姓名.Name = "员工姓名";
            // 
            // 厂区
            // 
            this.厂区.DataPropertyName = "Org";
            this.厂区.HeaderText = "厂区";
            this.厂区.Name = "厂区";
            // 
            // 工作中心
            // 
            this.工作中心.DataPropertyName = "deptno";
            this.工作中心.HeaderText = "工作中心";
            this.工作中心.Name = "工作中心";
            // 
            // 日期
            // 
            this.日期.DataPropertyName = "Day";
            this.日期.HeaderText = "日期";
            this.日期.Name = "日期";
            // 
            // 班次代号
            // 
            this.班次代号.DataPropertyName = "shiftcode";
            this.班次代号.HeaderText = "班次代号";
            this.班次代号.Name = "班次代号";
            // 
            // 打卡次数
            // 
            this.打卡次数.DataPropertyName = "ClocksNumber";
            this.打卡次数.HeaderText = "打卡次数";
            this.打卡次数.Name = "打卡次数";
            // 
            // 打卡记录
            // 
            this.打卡记录.DataPropertyName = "PunchingCardRecord";
            this.打卡记录.HeaderText = "打卡记录";
            this.打卡记录.Name = "打卡记录";
            this.打卡记录.Width = 120;
            // 
            // 工时
            // 
            this.工时.DataPropertyName = "onlinetime";
            this.工时.HeaderText = "工时";
            this.工时.Name = "工时";
            // 
            // dataGridViewEmpNo
            // 
            this.dataGridViewEmpNo.AllowUserToAddRows = false;
            this.dataGridViewEmpNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmpNo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.EmpNo,
            this.EmpName,
            this.日期由,
            this.日期至,
            this.onlinetime});
            this.dataGridViewEmpNo.Location = new System.Drawing.Point(42, 203);
            this.dataGridViewEmpNo.Name = "dataGridViewEmpNo";
            this.dataGridViewEmpNo.ReadOnly = true;
            this.dataGridViewEmpNo.RowTemplate.Height = 23;
            this.dataGridViewEmpNo.Size = new System.Drawing.Size(1079, 476);
            this.dataGridViewEmpNo.TabIndex = 18;
            this.dataGridViewEmpNo.Visible = false;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "序号";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // EmpNo
            // 
            this.EmpNo.DataPropertyName = "EmpNo";
            this.EmpNo.HeaderText = "员工编号";
            this.EmpNo.Name = "EmpNo";
            this.EmpNo.ReadOnly = true;
            // 
            // EmpName
            // 
            this.EmpName.DataPropertyName = "EmpName";
            this.EmpName.HeaderText = "员工姓名";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            // 
            // 日期由
            // 
            this.日期由.DataPropertyName = "DateFrom";
            this.日期由.HeaderText = "日期由";
            this.日期由.Name = "日期由";
            this.日期由.ReadOnly = true;
            this.日期由.Width = 130;
            // 
            // 日期至
            // 
            this.日期至.DataPropertyName = "DateTo";
            this.日期至.HeaderText = "日期至";
            this.日期至.Name = "日期至";
            this.日期至.ReadOnly = true;
            this.日期至.Width = 130;
            // 
            // onlinetime
            // 
            this.onlinetime.DataPropertyName = "onlinetime";
            this.onlinetime.HeaderText = "工时";
            this.onlinetime.Name = "onlinetime";
            this.onlinetime.ReadOnly = true;
            // 
            // btnAutoSync
            // 
            this.btnAutoSync.Location = new System.Drawing.Point(323, 47);
            this.btnAutoSync.Name = "btnAutoSync";
            this.btnAutoSync.Size = new System.Drawing.Size(128, 25);
            this.btnAutoSync.TabIndex = 19;
            this.btnAutoSync.Text = "一键自动同步工时";
            this.btnAutoSync.UseVisualStyleBackColor = true;
            this.btnAutoSync.Click += new System.EventHandler(this.btnAutoSync_Click);
            // 
            // btnHandHourSync
            // 
            this.btnHandHourSync.Location = new System.Drawing.Point(478, 47);
            this.btnHandHourSync.Name = "btnHandHourSync";
            this.btnHandHourSync.Size = new System.Drawing.Size(145, 25);
            this.btnHandHourSync.TabIndex = 20;
            this.btnHandHourSync.Text = "一键手动打卡同步工时";
            this.btnHandHourSync.UseVisualStyleBackColor = true;
            this.btnHandHourSync.Click += new System.EventHandler(this.btnHandHourSync_Click);
            // 
            // WorkingHoursQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 914);
            this.Controls.Add(this.btnHandHourSync);
            this.Controls.Add(this.btnAutoSync);
            this.Controls.Add(this.dataGridViewEmpNo);
            this.Controls.Add(this.dataGridViewWorkingHours);
            this.Controls.Add(this.comboBoxReportType);
            this.Controls.Add(this.labelReportType);
            this.Controls.Add(this.dateTimePickerEndTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerStartTime);
            this.Controls.Add(this.labelDay);
            this.Controls.Add(this.txtEmpNo);
            this.Controls.Add(this.labelEmpNO);
            this.Controls.Add(this.txtFactoryRegion);
            this.Controls.Add(this.labelFactoryRegion);
            this.Controls.Add(this.txtWorkCenter);
            this.Controls.Add(this.labelWorkCenter);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnQuery);
            this.Name = "WorkingHoursQuery";
            this.Text = "工时查询";
            this.Load += new System.EventHandler(this.WorkingHoursQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWorkingHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmpNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label labelWorkCenter;
        private System.Windows.Forms.TextBox txtWorkCenter;
        private System.Windows.Forms.Label labelFactoryRegion;
        private System.Windows.Forms.TextBox txtFactoryRegion;
        private System.Windows.Forms.Label labelEmpNO;
        private System.Windows.Forms.TextBox txtEmpNo;
        private System.Windows.Forms.Label labelDay;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Label labelReportType;
        private System.Windows.Forms.ComboBox comboBoxReportType;
        private System.Windows.Forms.DataGridView dataGridViewWorkingHours;
        private System.Windows.Forms.DataGridView dataGridViewEmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 员工编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 员工姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 厂区;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工作中心;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 班次代号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 打卡次数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 打卡记录;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工时;
        private System.Windows.Forms.Button btnAutoSync;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期由;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期至;
        private System.Windows.Forms.DataGridViewTextBoxColumn onlinetime;
        private System.Windows.Forms.Button btnHandHourSync;
    }
}

