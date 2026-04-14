
namespace F_EPM_Equipment_Maintenance
{
    partial class Equipment_MaintenanceMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cl_cb = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.order_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bz_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bz_context = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.org_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.device_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.snid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gz_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosis_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_proj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pj_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receive_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.begin_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.end_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.close_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.labe7 = new System.Windows.Forms.Label();
            this.labe6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.labe5 = new System.Windows.Forms.Label();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.labe3 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.labe4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.labe2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labe1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(0, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1252, 47);
            this.panel1.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.ForeColor = System.Drawing.SystemColors.Window;
            this.button5.Location = new System.Drawing.Point(325, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 36);
            this.button5.TabIndex = 4;
            this.button5.Text = "取消全选";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ForeColor = System.Drawing.SystemColors.Window;
            this.button4.Location = new System.Drawing.Point(425, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 36);
            this.button4.TabIndex = 3;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.SystemColors.Window;
            this.button3.Location = new System.Drawing.Point(230, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 36);
            this.button3.TabIndex = 2;
            this.button3.Text = "全选";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.ForeColor = System.Drawing.SystemColors.Window;
            this.button2.Location = new System.Drawing.Point(139, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 36);
            this.button2.TabIndex = 1;
            this.button2.Text = "导出";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.SystemColors.Window;
            this.button1.Location = new System.Drawing.Point(40, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.comboBox6);
            this.panel2.Controls.Add(this.labe7);
            this.panel2.Controls.Add(this.labe6);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.labe5);
            this.panel2.Controls.Add(this.comboBox5);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.labe3);
            this.panel2.Controls.Add(this.comboBox4);
            this.panel2.Controls.Add(this.labe4);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.labe2);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.labe1);
            this.panel2.Location = new System.Drawing.Point(0, 125);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1252, 626);
            this.panel2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cl_cb,
            this.order_state,
            this.order_no,
            this.bz_date,
            this.bz_context,
            this.org_name,
            this.address,
            this.device_name,
            this.device_state,
            this.snid,
            this.gz_name,
            this.diagnosis_name,
            this.repair_name,
            this.repair_proj,
            this.pj_name,
            this.count,
            this.unit,
            this.reason,
            this.repair_user,
            this.receive_date,
            this.begin_date,
            this.end_date,
            this.repair_date,
            this.close_date});
            this.dataGridView1.Location = new System.Drawing.Point(0, 153);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1250, 468);
            this.dataGridView1.TabIndex = 19;
            // 
            // cl_cb
            // 
            this.cl_cb.HeaderText = "";
            this.cl_cb.MinimumWidth = 6;
            this.cl_cb.Name = "cl_cb";
            this.cl_cb.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cl_cb.Width = 50;
            // 
            // order_state
            // 
            this.order_state.HeaderText = "派工单状态";
            this.order_state.MinimumWidth = 6;
            this.order_state.Name = "order_state";
            this.order_state.ReadOnly = true;
            this.order_state.Width = 125;
            // 
            // order_no
            // 
            this.order_no.HeaderText = "派单编号";
            this.order_no.MinimumWidth = 6;
            this.order_no.Name = "order_no";
            this.order_no.ReadOnly = true;
            this.order_no.Width = 130;
            // 
            // bz_date
            // 
            this.bz_date.HeaderText = "报障日期";
            this.bz_date.MinimumWidth = 6;
            this.bz_date.Name = "bz_date";
            this.bz_date.ReadOnly = true;
            this.bz_date.Width = 150;
            // 
            // bz_context
            // 
            this.bz_context.HeaderText = "保障内容";
            this.bz_context.MinimumWidth = 6;
            this.bz_context.Name = "bz_context";
            this.bz_context.ReadOnly = true;
            this.bz_context.Width = 125;
            // 
            // org_name
            // 
            this.org_name.HeaderText = "工厂";
            this.org_name.MinimumWidth = 6;
            this.org_name.Name = "org_name";
            this.org_name.ReadOnly = true;
            this.org_name.Width = 125;
            // 
            // address
            // 
            this.address.HeaderText = "位置";
            this.address.MinimumWidth = 6;
            this.address.Name = "address";
            this.address.ReadOnly = true;
            this.address.Width = 125;
            // 
            // device_name
            // 
            this.device_name.HeaderText = "设备名称";
            this.device_name.MinimumWidth = 6;
            this.device_name.Name = "device_name";
            this.device_name.ReadOnly = true;
            this.device_name.Width = 125;
            // 
            // device_state
            // 
            this.device_state.HeaderText = "设备状态";
            this.device_state.MinimumWidth = 6;
            this.device_state.Name = "device_state";
            this.device_state.ReadOnly = true;
            this.device_state.Width = 125;
            // 
            // snid
            // 
            this.snid.HeaderText = "SN码";
            this.snid.MinimumWidth = 6;
            this.snid.Name = "snid";
            this.snid.ReadOnly = true;
            this.snid.Width = 125;
            // 
            // gz_name
            // 
            this.gz_name.HeaderText = "故障现象";
            this.gz_name.MinimumWidth = 6;
            this.gz_name.Name = "gz_name";
            this.gz_name.ReadOnly = true;
            this.gz_name.Width = 125;
            // 
            // diagnosis_name
            // 
            this.diagnosis_name.HeaderText = "诊断原因";
            this.diagnosis_name.MinimumWidth = 6;
            this.diagnosis_name.Name = "diagnosis_name";
            this.diagnosis_name.ReadOnly = true;
            this.diagnosis_name.Width = 125;
            // 
            // repair_name
            // 
            this.repair_name.HeaderText = "维修措施";
            this.repair_name.MinimumWidth = 6;
            this.repair_name.Name = "repair_name";
            this.repair_name.ReadOnly = true;
            this.repair_name.Width = 125;
            // 
            // repair_proj
            // 
            this.repair_proj.HeaderText = "维修项目";
            this.repair_proj.MinimumWidth = 6;
            this.repair_proj.Name = "repair_proj";
            this.repair_proj.ReadOnly = true;
            this.repair_proj.Width = 125;
            // 
            // pj_name
            // 
            this.pj_name.HeaderText = "配件名称";
            this.pj_name.MinimumWidth = 6;
            this.pj_name.Name = "pj_name";
            this.pj_name.ReadOnly = true;
            this.pj_name.Width = 125;
            // 
            // count
            // 
            this.count.HeaderText = "数量";
            this.count.MinimumWidth = 6;
            this.count.Name = "count";
            this.count.ReadOnly = true;
            this.count.Width = 125;
            // 
            // unit
            // 
            this.unit.HeaderText = "单位";
            this.unit.MinimumWidth = 6;
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 125;
            // 
            // reason
            // 
            this.reason.HeaderText = "拒绝原因";
            this.reason.MinimumWidth = 6;
            this.reason.Name = "reason";
            this.reason.ReadOnly = true;
            this.reason.Width = 125;
            // 
            // repair_user
            // 
            this.repair_user.HeaderText = "维修员";
            this.repair_user.MinimumWidth = 6;
            this.repair_user.Name = "repair_user";
            this.repair_user.ReadOnly = true;
            this.repair_user.Width = 125;
            // 
            // receive_date
            // 
            this.receive_date.HeaderText = "接收时间";
            this.receive_date.MinimumWidth = 6;
            this.receive_date.Name = "receive_date";
            this.receive_date.ReadOnly = true;
            this.receive_date.Width = 150;
            // 
            // begin_date
            // 
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.begin_date.DefaultCellStyle = dataGridViewCellStyle8;
            this.begin_date.HeaderText = "领料时间";
            this.begin_date.MinimumWidth = 6;
            this.begin_date.Name = "begin_date";
            this.begin_date.ReadOnly = true;
            this.begin_date.Width = 150;
            // 
            // end_date
            // 
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.end_date.DefaultCellStyle = dataGridViewCellStyle9;
            this.end_date.HeaderText = "领料结束时间";
            this.end_date.MinimumWidth = 6;
            this.end_date.Name = "end_date";
            this.end_date.ReadOnly = true;
            this.end_date.Width = 150;
            // 
            // repair_date
            // 
            this.repair_date.HeaderText = "维修完成时间";
            this.repair_date.MinimumWidth = 6;
            this.repair_date.Name = "repair_date";
            this.repair_date.ReadOnly = true;
            this.repair_date.Width = 150;
            // 
            // close_date
            // 
            this.close_date.HeaderText = "派工单结束时间";
            this.close_date.MinimumWidth = 6;
            this.close_date.Name = "close_date";
            this.close_date.ReadOnly = true;
            this.close_date.Width = 150;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(821, 78);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 18;
            this.comboBox6.Text = "全部";
            // 
            // labe7
            // 
            this.labe7.AutoSize = true;
            this.labe7.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe7.Location = new System.Drawing.Point(698, 85);
            this.labe7.Name = "labe7";
            this.labe7.Size = new System.Drawing.Size(42, 14);
            this.labe7.TabIndex = 17;
            this.labe7.Text = "位置:";
            // 
            // labe6
            // 
            this.labe6.AutoSize = true;
            this.labe6.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe6.Location = new System.Drawing.Point(422, 88);
            this.labe6.Name = "labe6";
            this.labe6.Size = new System.Drawing.Size(28, 14);
            this.labe6.TabIndex = 16;
            this.labe6.Text = "至:";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(488, 83);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker2.TabIndex = 15;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(205, 83);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 20);
            this.dateTimePicker1.TabIndex = 14;
            // 
            // labe5
            // 
            this.labe5.AutoSize = true;
            this.labe5.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe5.Location = new System.Drawing.Point(151, 88);
            this.labe5.Name = "labe5";
            this.labe5.Size = new System.Drawing.Size(28, 14);
            this.labe5.TabIndex = 13;
            this.labe5.Text = "由:";
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "Reporting_Date",
            "Received_Date",
            "Repair_Completion_Date"});
            this.comboBox5.Location = new System.Drawing.Point(11, 83);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 21);
            this.comboBox5.TabIndex = 12;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(1101, 29);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 11;
            this.comboBox3.Text = "全部";
            // 
            // labe3
            // 
            this.labe3.AutoSize = true;
            this.labe3.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe3.Location = new System.Drawing.Point(978, 32);
            this.labe3.Name = "labe3";
            this.labe3.Size = new System.Drawing.Size(70, 14);
            this.labe3.TabIndex = 10;
            this.labe3.Text = "设备状态:";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(821, 26);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 9;
            this.comboBox4.Text = "全部";
            // 
            // labe4
            // 
            this.labe4.AutoSize = true;
            this.labe4.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe4.Location = new System.Drawing.Point(656, 32);
            this.labe4.Name = "labe4";
            this.labe4.Size = new System.Drawing.Size(84, 14);
            this.labe4.TabIndex = 8;
            this.labe4.Text = "派工单状态:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(487, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.Text = "全部";
            // 
            // labe2
            // 
            this.labe2.AutoSize = true;
            this.labe2.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe2.Location = new System.Drawing.Point(380, 32);
            this.labe2.Name = "labe2";
            this.labe2.Size = new System.Drawing.Size(70, 14);
            this.labe2.TabIndex = 6;
            this.labe2.Text = "设备名称:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(205, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "全部";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labe1
            // 
            this.labe1.AutoSize = true;
            this.labe1.Font = new System.Drawing.Font("SimSun", 10F);
            this.labe1.Location = new System.Drawing.Point(136, 29);
            this.labe1.Name = "labe1";
            this.labe1.Size = new System.Drawing.Size(42, 14);
            this.labe1.TabIndex = 4;
            this.labe1.Text = "工厂:";
            // 
            // Equipment_MaintenanceMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 764);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Equipment_MaintenanceMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备维修履历";
            this.Load += new System.EventHandler(this.Equipment_MaintenanceMainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label labe3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label labe4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label labe2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labe1;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label labe5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label labe6;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.Label labe7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cl_cb;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn bz_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn bz_context;
        private System.Windows.Forms.DataGridViewTextBoxColumn org_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn device_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn snid;
        private System.Windows.Forms.DataGridViewTextBoxColumn gz_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosis_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_proj;
        private System.Windows.Forms.DataGridViewTextBoxColumn pj_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn count;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_user;
        private System.Windows.Forms.DataGridViewTextBoxColumn receive_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn begin_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn end_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn repair_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn close_date;
    }
}

