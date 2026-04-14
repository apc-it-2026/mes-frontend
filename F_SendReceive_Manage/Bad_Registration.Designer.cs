
namespace F_SendReceive_Manage
{
    partial class Bad_Registration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bad_Registration));
            this.btnDelDate = new System.Windows.Forms.Button();
            this.btnInsertData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbBadRecordNo = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lbRegisterDate = new System.Windows.Forms.Label();
            this.lbSeason = new System.Windows.Forms.Label();
            this.lbPurpose = new System.Windows.Forms.Label();
            this.lbArtName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbArtNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbSampleNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suppliers_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suppliers_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsible_unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsible_unit_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.causes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDelDate
            // 
            this.btnDelDate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelDate.BackgroundImage")));
            this.btnDelDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelDate.Location = new System.Drawing.Point(57, 189);
            this.btnDelDate.Name = "btnDelDate";
            this.btnDelDate.Size = new System.Drawing.Size(33, 32);
            this.btnDelDate.TabIndex = 30;
            this.btnDelDate.UseVisualStyleBackColor = true;
            this.btnDelDate.Click += new System.EventHandler(this.btnDelDate_Click);
            // 
            // btnInsertData
            // 
            this.btnInsertData.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInsertData.BackgroundImage")));
            this.btnInsertData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInsertData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertData.Location = new System.Drawing.Point(17, 189);
            this.btnInsertData.Name = "btnInsertData";
            this.btnInsertData.Size = new System.Drawing.Size(33, 32);
            this.btnInsertData.TabIndex = 31;
            this.btnInsertData.UseVisualStyleBackColor = true;
            this.btnInsertData.Click += new System.EventHandler(this.btnInsertData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.part_no,
            this.part_name,
            this.Suppliers_Code,
            this.Suppliers_Name,
            this.Process_No,
            this.Process_Name,
            this.Size_No,
            this.qty,
            this.responsible_unit,
            this.responsible_unit_code,
            this.causes});
            this.dataGridView1.Location = new System.Drawing.Point(18, 228);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1200, 338);
            this.dataGridView1.TabIndex = 32;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.lbBadRecordNo);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lbRegisterDate);
            this.panel1.Controls.Add(this.lbSeason);
            this.panel1.Controls.Add(this.lbPurpose);
            this.panel1.Controls.Add(this.lbArtName);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.lbArtNo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lbSampleNo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1206, 113);
            this.panel1.TabIndex = 33;
            // 
            // lbBadRecordNo
            // 
            this.lbBadRecordNo.BackColor = System.Drawing.Color.White;
            this.lbBadRecordNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbBadRecordNo.Location = new System.Drawing.Point(148, 10);
            this.lbBadRecordNo.Multiline = true;
            this.lbBadRecordNo.Name = "lbBadRecordNo";
            this.lbBadRecordNo.ReadOnly = true;
            this.lbBadRecordNo.Size = new System.Drawing.Size(266, 21);
            this.lbBadRecordNo.TabIndex = 48;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(720, 71);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 33);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbRegisterDate
            // 
            this.lbRegisterDate.AutoSize = true;
            this.lbRegisterDate.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbRegisterDate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRegisterDate.Location = new System.Drawing.Point(601, 81);
            this.lbRegisterDate.Name = "lbRegisterDate";
            this.lbRegisterDate.Size = new System.Drawing.Size(55, 21);
            this.lbRegisterDate.TabIndex = 41;
            this.lbRegisterDate.Text = "label4";
            // 
            // lbSeason
            // 
            this.lbSeason.AutoSize = true;
            this.lbSeason.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbSeason.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSeason.Location = new System.Drawing.Point(368, 82);
            this.lbSeason.Name = "lbSeason";
            this.lbSeason.Size = new System.Drawing.Size(55, 21);
            this.lbSeason.TabIndex = 42;
            this.lbSeason.Text = "label4";
            // 
            // lbPurpose
            // 
            this.lbPurpose.AutoSize = true;
            this.lbPurpose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbPurpose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPurpose.Location = new System.Drawing.Point(144, 82);
            this.lbPurpose.Name = "lbPurpose";
            this.lbPurpose.Size = new System.Drawing.Size(55, 21);
            this.lbPurpose.TabIndex = 43;
            this.lbPurpose.Text = "label4";
            // 
            // lbArtName
            // 
            this.lbArtName.AutoSize = true;
            this.lbArtName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbArtName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbArtName.Location = new System.Drawing.Point(601, 43);
            this.lbArtName.Name = "lbArtName";
            this.lbArtName.Size = new System.Drawing.Size(55, 21);
            this.lbArtName.TabIndex = 44;
            this.lbArtName.Text = "label4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(504, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 25);
            this.label11.TabIndex = 34;
            this.label11.Text = "登记日期:";
            // 
            // lbArtNo
            // 
            this.lbArtNo.AutoSize = true;
            this.lbArtNo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbArtNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbArtNo.Location = new System.Drawing.Point(368, 46);
            this.lbArtNo.Name = "lbArtNo";
            this.lbArtNo.Size = new System.Drawing.Size(55, 21);
            this.lbArtNo.TabIndex = 45;
            this.lbArtNo.Text = "label4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(306, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 25);
            this.label7.TabIndex = 35;
            this.label7.Text = "季度:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(540, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 25);
            this.label10.TabIndex = 36;
            this.label10.Text = "鞋型:";
            // 
            // lbSampleNo
            // 
            this.lbSampleNo.AutoSize = true;
            this.lbSampleNo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbSampleNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSampleNo.Location = new System.Drawing.Point(144, 46);
            this.lbSampleNo.Name = "lbSampleNo";
            this.lbSampleNo.Size = new System.Drawing.Size(55, 21);
            this.lbSampleNo.TabIndex = 46;
            this.lbSampleNo.Text = "label4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(281, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 25);
            this.label6.TabIndex = 37;
            this.label6.Text = "Art_No:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(82, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 25);
            this.label3.TabIndex = 38;
            this.label3.Text = "用途:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(25, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 39;
            this.label2.Text = "样品单编号:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 25);
            this.label1.TabIndex = 40;
            this.label1.Text = "不良记录编号:";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // part_no
            // 
            this.part_no.HeaderText = "部件NO";
            this.part_no.Name = "part_no";
            this.part_no.ReadOnly = true;
            this.part_no.Visible = false;
            // 
            // part_name
            // 
            this.part_name.HeaderText = "部件";
            this.part_name.Name = "part_name";
            this.part_name.ReadOnly = true;
            // 
            // Suppliers_Code
            // 
            this.Suppliers_Code.HeaderText = "厂商NO";
            this.Suppliers_Code.Name = "Suppliers_Code";
            this.Suppliers_Code.ReadOnly = true;
            this.Suppliers_Code.Visible = false;
            // 
            // Suppliers_Name
            // 
            this.Suppliers_Name.HeaderText = "厂商";
            this.Suppliers_Name.Name = "Suppliers_Name";
            this.Suppliers_Name.ReadOnly = true;
            // 
            // Process_No
            // 
            this.Process_No.HeaderText = "工艺";
            this.Process_No.Name = "Process_No";
            this.Process_No.ReadOnly = true;
            // 
            // Process_Name
            // 
            this.Process_Name.HeaderText = "工艺NO";
            this.Process_Name.Name = "Process_Name";
            this.Process_Name.ReadOnly = true;
            this.Process_Name.Visible = false;
            // 
            // Size_No
            // 
            this.Size_No.HeaderText = "尺码";
            this.Size_No.Name = "Size_No";
            this.Size_No.ReadOnly = true;
            this.Size_No.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // qty
            // 
            this.qty.HeaderText = "不良数量";
            this.qty.Name = "qty";
            // 
            // responsible_unit
            // 
            this.responsible_unit.HeaderText = "责任单位号";
            this.responsible_unit.Name = "responsible_unit";
            this.responsible_unit.ReadOnly = true;
            this.responsible_unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.responsible_unit.Visible = false;
            // 
            // responsible_unit_code
            // 
            this.responsible_unit_code.HeaderText = "责任单位";
            this.responsible_unit_code.Name = "responsible_unit_code";
            this.responsible_unit_code.ReadOnly = true;
            // 
            // causes
            // 
            this.causes.HeaderText = "不良原因";
            this.causes.Name = "causes";
            this.causes.ReadOnly = true;
            this.causes.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.causes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Bad_Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 575);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDelDate);
            this.Controls.Add(this.btnInsertData);
            this.Name = "Bad_Registration";
            this.Text = "不良登记";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDelDate;
        private System.Windows.Forms.Button btnInsertData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbRegisterDate;
        private System.Windows.Forms.Label lbSeason;
        private System.Windows.Forms.Label lbPurpose;
        private System.Windows.Forms.Label lbArtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbArtNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbSampleNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox lbBadRecordNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Suppliers_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Suppliers_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsible_unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsible_unit_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn causes;
    }
}