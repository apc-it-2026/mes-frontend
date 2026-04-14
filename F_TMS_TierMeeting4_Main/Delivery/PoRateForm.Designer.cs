namespace TierMeeting.Delivery
{
    partial class PoRateForm
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
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new CusContorl.CusLabel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnQuery = new MaterialSkin.Controls.MaterialRaisedButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.plant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.department_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shoe_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.model_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wk_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plan_finish_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finish_situation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packing_owe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actual_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.under_reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 74);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1444, 622);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1438, 59);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 18F);
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "生产时间";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(138, 11);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(173, 35);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // btnQuery
            // 
            this.btnQuery.AutoSize = true;
            this.btnQuery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnQuery.Depth = 0;
            this.btnQuery.Icon = null;
            this.btnQuery.Location = new System.Drawing.Point(346, 11);
            this.btnQuery.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Primary = true;
            this.btnQuery.Size = new System.Drawing.Size(64, 36);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 16F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.plant,
            this.department_code,
            this.shoe_name,
            this.model_number,
            this.wk_id,
            this.art_no,
            this.qty,
            this.plan_finish_date,
            this.finish_situation,
            this.packing_owe,
            this.actual_date,
            this.under_reason});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 18F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 50;
            this.dataGridView1.Size = new System.Drawing.Size(1438, 551);
            this.dataGridView1.TabIndex = 1;
            // 
            // plant
            // 
            this.plant.DataPropertyName = "plant";
            this.plant.FillWeight = 50F;
            this.plant.HeaderText = "厂区";
            this.plant.Name = "plant";
            // 
            // department_code
            // 
            this.department_code.DataPropertyName = "department_code";
            this.department_code.HeaderText = "组别";
            this.department_code.Name = "department_code";
            // 
            // shoe_name
            // 
            this.shoe_name.DataPropertyName = "shoe_name";
            this.shoe_name.FillWeight = 150F;
            this.shoe_name.HeaderText = "鞋 型";
            this.shoe_name.Name = "shoe_name";
            // 
            // model_number
            // 
            this.model_number.DataPropertyName = "model_number";
            this.model_number.FillWeight = 80F;
            this.model_number.HeaderText = "模 号";
            this.model_number.Name = "model_number";
            // 
            // wk_id
            // 
            this.wk_id.DataPropertyName = "wk_id";
            this.wk_id.HeaderText = "制 令";
            this.wk_id.Name = "wk_id";
            // 
            // art_no
            // 
            this.art_no.DataPropertyName = "art_no";
            this.art_no.HeaderText = "Art No.";
            this.art_no.Name = "art_no";
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.FillWeight = 60F;
            this.qty.HeaderText = "数量";
            this.qty.Name = "qty";
            // 
            // plan_finish_date
            // 
            this.plan_finish_date.DataPropertyName = "plan_finish_date";
            this.plan_finish_date.HeaderText = "加工计划完成时间";
            this.plan_finish_date.Name = "plan_finish_date";
            // 
            // finish_situation
            // 
            this.finish_situation.DataPropertyName = "finish_situation";
            this.finish_situation.FillWeight = 80F;
            this.finish_situation.HeaderText = "完成状况";
            this.finish_situation.Name = "finish_situation";
            // 
            // packing_owe
            // 
            this.packing_owe.DataPropertyName = "packing_owe";
            this.packing_owe.FillWeight = 80F;
            this.packing_owe.HeaderText = "包装欠数";
            this.packing_owe.Name = "packing_owe";
            // 
            // actual_date
            // 
            this.actual_date.DataPropertyName = "actual_date";
            this.actual_date.HeaderText = "实际完成日期";
            this.actual_date.Name = "actual_date";
            // 
            // under_reason
            // 
            this.under_reason.DataPropertyName = "under_reason";
            this.under_reason.HeaderText = "未满箱原因";
            this.under_reason.Name = "under_reason";
            // 
            // PoRateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1449, 698);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PoRateForm";
            this.Text = "PoRateForm";
            this.Load += new System.EventHandler(this.PoRateForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CusContorl.CusTableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialRaisedButton btnQuery;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private CusContorl.CusLabel label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn plant;
        private System.Windows.Forms.DataGridViewTextBoxColumn department_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn shoe_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn model_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn wk_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn plan_finish_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn finish_situation;
        private System.Windows.Forms.DataGridViewTextBoxColumn packing_owe;
        private System.Windows.Forms.DataGridViewTextBoxColumn actual_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn under_reason;
    }
}