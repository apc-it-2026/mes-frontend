
namespace CommanClassLib
{
    partial class ExcelExporter
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvFullWorkingHours = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOrg = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPlant = new System.Windows.Forms.ComboBox();
            this.cbRout = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDept = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.Org = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rout = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.work_day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmFromHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmToHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PmFromHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PmToHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.work_hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jockey_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pluripotent_worker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.omnipotent_worker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udf01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANIN_HOURS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANOUT_HOURS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllWorkTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFullWorkingHours)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgvFullWorkingHours, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.68727F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.31273F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1603, 809);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvFullWorkingHours
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvFullWorkingHours.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFullWorkingHours.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFullWorkingHours.ColumnHeadersHeight = 50;
            this.dgvFullWorkingHours.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Org,
            this.Plant,
            this.DeptName,
            this.Rout,
            this.work_day,
            this.AmFromHour,
            this.AmToHour,
            this.PmFromHour,
            this.PmToHour,
            this.work_hours,
            this.jockey_qty,
            this.pluripotent_worker,
            this.omnipotent_worker,
            this.udf01,
            this.TRANIN_HOURS,
            this.TRANOUT_HOURS,
            this.AllWorkTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFullWorkingHours.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFullWorkingHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFullWorkingHours.Location = new System.Drawing.Point(4, 139);
            this.dgvFullWorkingHours.Margin = new System.Windows.Forms.Padding(4);
            this.dgvFullWorkingHours.Name = "dgvFullWorkingHours";
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFullWorkingHours.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFullWorkingHours.RowHeadersVisible = false;
            this.dgvFullWorkingHours.RowHeadersWidth = 200;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvFullWorkingHours.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvFullWorkingHours.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvFullWorkingHours.RowTemplate.Height = 50;
            this.dgvFullWorkingHours.Size = new System.Drawing.Size(1595, 666);
            this.dgvFullWorkingHours.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbOrg, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPlant, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbRout, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.tbDept, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnQuery, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExcel, 8, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFrom, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.dtpEnd, 5, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1597, 129);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 28);
            this.label1.TabIndex = 33;
            this.label1.Text = "起始时间：";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(59, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 28);
            this.label3.TabIndex = 25;
            this.label3.Text = "组织：";
            // 
            // cbOrg
            // 
            this.cbOrg.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbOrg.FormattingEnabled = true;
            this.cbOrg.Location = new System.Drawing.Point(161, 3);
            this.cbOrg.Name = "cbOrg";
            this.cbOrg.Size = new System.Drawing.Size(173, 35);
            this.cbOrg.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(418, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 28);
            this.label4.TabIndex = 29;
            this.label4.Text = "厂别：";
            // 
            // cbPlant
            // 
            this.cbPlant.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbPlant.FormattingEnabled = true;
            this.cbPlant.Location = new System.Drawing.Point(520, 3);
            this.cbPlant.Name = "cbPlant";
            this.cbPlant.Size = new System.Drawing.Size(173, 35);
            this.cbPlant.TabIndex = 30;
            // 
            // cbRout
            // 
            this.cbRout.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRout.FormattingEnabled = true;
            this.cbRout.Location = new System.Drawing.Point(857, 3);
            this.cbRout.Name = "cbRout";
            this.cbRout.Size = new System.Drawing.Size(200, 35);
            this.cbRout.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(1104, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 28);
            this.label5.TabIndex = 37;
            this.label5.Text = "线别:";
            // 
            // tbDept
            // 
            this.tbDept.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDept.Location = new System.Drawing.Point(1192, 3);
            this.tbDept.Name = "tbDept";
            this.tbDept.Size = new System.Drawing.Size(173, 38);
            this.tbDept.TabIndex = 38;
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(1371, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(197, 55);
            this.btnQuery.TabIndex = 39;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExcel.Location = new System.Drawing.Point(1371, 67);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(197, 55);
            this.btnExcel.TabIndex = 40;
            this.btnExcel.Text = "导出Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(699, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 28);
            this.label2.TabIndex = 35;
            this.label2.Text = "结束时间：";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(755, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 28);
            this.label6.TabIndex = 31;
            this.label6.Text = "制程：";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpFrom.Location = new System.Drawing.Point(161, 67);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(251, 38);
            this.dtpFrom.TabIndex = 34;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpEnd.Location = new System.Drawing.Point(857, 67);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(241, 38);
            this.dtpEnd.TabIndex = 36;
            // 
            // Org
            // 
            this.Org.DataPropertyName = "Org";
            this.Org.Frozen = true;
            this.Org.HeaderText = "组织";
            this.Org.MinimumWidth = 6;
            this.Org.Name = "Org";
            this.Org.Width = 125;
            // 
            // Plant
            // 
            this.Plant.DataPropertyName = "Plant";
            this.Plant.Frozen = true;
            this.Plant.HeaderText = "厂别";
            this.Plant.MinimumWidth = 6;
            this.Plant.Name = "Plant";
            this.Plant.Width = 125;
            // 
            // DeptName
            // 
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.Frozen = true;
            this.DeptName.HeaderText = "线别";
            this.DeptName.MinimumWidth = 6;
            this.DeptName.Name = "DeptName";
            this.DeptName.Width = 125;
            // 
            // Rout
            // 
            this.Rout.DataPropertyName = "Rout";
            this.Rout.Frozen = true;
            this.Rout.HeaderText = "制程";
            this.Rout.MinimumWidth = 6;
            this.Rout.Name = "Rout";
            this.Rout.Width = 125;
            // 
            // work_day
            // 
            this.work_day.DataPropertyName = "work_day";
            this.work_day.Frozen = true;
            this.work_day.HeaderText = "日期";
            this.work_day.MinimumWidth = 6;
            this.work_day.Name = "work_day";
            this.work_day.ReadOnly = true;
            this.work_day.Width = 125;
            // 
            // AmFromHour
            // 
            this.AmFromHour.DataPropertyName = "AmFromHour";
            this.AmFromHour.HeaderText = "早上开始时间";
            this.AmFromHour.MinimumWidth = 6;
            this.AmFromHour.Name = "AmFromHour";
            this.AmFromHour.ReadOnly = true;
            this.AmFromHour.Width = 125;
            // 
            // AmToHour
            // 
            this.AmToHour.DataPropertyName = "AmToHour";
            this.AmToHour.HeaderText = "早上结束时间";
            this.AmToHour.MinimumWidth = 6;
            this.AmToHour.Name = "AmToHour";
            this.AmToHour.ReadOnly = true;
            this.AmToHour.Width = 125;
            // 
            // PmFromHour
            // 
            this.PmFromHour.DataPropertyName = "PmFromHour";
            this.PmFromHour.HeaderText = "下午开始时间";
            this.PmFromHour.MinimumWidth = 6;
            this.PmFromHour.Name = "PmFromHour";
            this.PmFromHour.ReadOnly = true;
            this.PmFromHour.Width = 125;
            // 
            // PmToHour
            // 
            this.PmToHour.DataPropertyName = "PmToHour";
            this.PmToHour.HeaderText = "下午结束时间";
            this.PmToHour.MinimumWidth = 6;
            this.PmToHour.Name = "PmToHour";
            this.PmToHour.ReadOnly = true;
            this.PmToHour.Width = 125;
            // 
            // work_hours
            // 
            this.work_hours.DataPropertyName = "work_hours";
            this.work_hours.HeaderText = "工作时间";
            this.work_hours.MinimumWidth = 6;
            this.work_hours.Name = "work_hours";
            this.work_hours.ReadOnly = true;
            this.work_hours.Width = 125;
            // 
            // jockey_qty
            // 
            this.jockey_qty.DataPropertyName = "jockey_qty";
            this.jockey_qty.HeaderText = "操作工";
            this.jockey_qty.MinimumWidth = 6;
            this.jockey_qty.Name = "jockey_qty";
            this.jockey_qty.ReadOnly = true;
            this.jockey_qty.Width = 125;
            // 
            // pluripotent_worker
            // 
            this.pluripotent_worker.DataPropertyName = "pluripotent_worker";
            this.pluripotent_worker.HeaderText = "多序工";
            this.pluripotent_worker.MinimumWidth = 6;
            this.pluripotent_worker.Name = "pluripotent_worker";
            this.pluripotent_worker.ReadOnly = true;
            this.pluripotent_worker.Width = 125;
            // 
            // omnipotent_worker
            // 
            this.omnipotent_worker.DataPropertyName = "omnipotent_worker";
            this.omnipotent_worker.HeaderText = "全能工";
            this.omnipotent_worker.MinimumWidth = 6;
            this.omnipotent_worker.Name = "omnipotent_worker";
            this.omnipotent_worker.ReadOnly = true;
            this.omnipotent_worker.Width = 125;
            // 
            // udf01
            // 
            this.udf01.DataPropertyName = "udf01";
            this.udf01.HeaderText = "水蜘蛛";
            this.udf01.MinimumWidth = 6;
            this.udf01.Name = "udf01";
            this.udf01.ReadOnly = true;
            this.udf01.Width = 125;
            // 
            // TRANIN_HOURS
            // 
            this.TRANIN_HOURS.DataPropertyName = "TRANIN_HOURS";
            this.TRANIN_HOURS.HeaderText = "调入工时";
            this.TRANIN_HOURS.MinimumWidth = 6;
            this.TRANIN_HOURS.Name = "TRANIN_HOURS";
            this.TRANIN_HOURS.Width = 125;
            // 
            // TRANOUT_HOURS
            // 
            this.TRANOUT_HOURS.DataPropertyName = "TRANOUT_HOURS";
            this.TRANOUT_HOURS.HeaderText = "调出工时";
            this.TRANOUT_HOURS.MinimumWidth = 6;
            this.TRANOUT_HOURS.Name = "TRANOUT_HOURS";
            this.TRANOUT_HOURS.Width = 125;
            // 
            // AllWorkTime
            // 
            this.AllWorkTime.DataPropertyName = "AllWorkTime";
            this.AllWorkTime.HeaderText = "总工时";
            this.AllWorkTime.MinimumWidth = 6;
            this.AllWorkTime.Name = "AllWorkTime";
            this.AllWorkTime.Width = 125;
            // 
            // ExcelExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1603, 876);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExcelExporter";
            this.Text = "导出Excel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ExcelExporter_Load);
            this.Resize += new System.EventHandler(this.ExcelExporter_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFullWorkingHours)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvFullWorkingHours;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbOrg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPlant;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbRout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDept;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Org;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plant;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rout;
        private System.Windows.Forms.DataGridViewTextBoxColumn work_day;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmFromHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmToHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmFromHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn PmToHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn work_hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn jockey_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn pluripotent_worker;
        private System.Windows.Forms.DataGridViewTextBoxColumn omnipotent_worker;
        private System.Windows.Forms.DataGridViewTextBoxColumn udf01;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANIN_HOURS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANOUT_HOURS;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllWorkTime;
    }
}