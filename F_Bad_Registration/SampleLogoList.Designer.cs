
namespace F_Bad_Registration
{
    partial class SampleLogoList
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROCEDURE_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATERIAL_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.process_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suppliers_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECEIVED_QUANTITY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.process_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.suppliers_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.rownum,
            this.PROCEDURE_no,
            this.MATERIAL_NO,
            this.part_no,
            this.part_name,
            this.process_name,
            this.suppliers_name,
            this.size_no,
            this.QUANTITY,
            this.RECEIVED_QUANTITY,
            this.process_no,
            this.suppliers_code});
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1126, 355);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOK.Location = new System.Drawing.Point(257, 66);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 33);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectAll.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSelectAll.Location = new System.Drawing.Point(12, 67);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(82, 33);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(100, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 32);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 60;
            // 
            // rownum
            // 
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "序号";
            this.rownum.Name = "rownum";
            this.rownum.Width = 60;
            // 
            // PROCEDURE_no
            // 
            this.PROCEDURE_no.DataPropertyName = "PROCEDURE_no";
            this.PROCEDURE_no.HeaderText = "活动编号";
            this.PROCEDURE_no.Name = "PROCEDURE_no";
            // 
            // MATERIAL_NO
            // 
            this.MATERIAL_NO.DataPropertyName = "MATERIAL_NO";
            this.MATERIAL_NO.HeaderText = "材料编号";
            this.MATERIAL_NO.Name = "MATERIAL_NO";
            // 
            // part_no
            // 
            this.part_no.DataPropertyName = "part_no";
            this.part_no.HeaderText = "部件代码";
            this.part_no.Name = "part_no";
            // 
            // part_name
            // 
            this.part_name.DataPropertyName = "part_name";
            this.part_name.HeaderText = "部件";
            this.part_name.Name = "part_name";
            // 
            // process_name
            // 
            this.process_name.DataPropertyName = "process_name";
            this.process_name.HeaderText = "工艺";
            this.process_name.Name = "process_name";
            // 
            // suppliers_name
            // 
            this.suppliers_name.DataPropertyName = "suppliers_name";
            this.suppliers_name.HeaderText = "厂商";
            this.suppliers_name.Name = "suppliers_name";
            // 
            // size_no
            // 
            this.size_no.DataPropertyName = "size_no";
            this.size_no.HeaderText = "码数";
            this.size_no.Name = "size_no";
            // 
            // QUANTITY
            // 
            this.QUANTITY.DataPropertyName = "QUANTITY";
            this.QUANTITY.HeaderText = "订单数量";
            this.QUANTITY.Name = "QUANTITY";
            // 
            // RECEIVED_QUANTITY
            // 
            this.RECEIVED_QUANTITY.DataPropertyName = "RECEIVED_QUANTITY";
            this.RECEIVED_QUANTITY.HeaderText = "收货数量";
            this.RECEIVED_QUANTITY.Name = "RECEIVED_QUANTITY";
            // 
            // process_no
            // 
            this.process_no.DataPropertyName = "process_no";
            this.process_no.HeaderText = "工艺代码";
            this.process_no.Name = "process_no";
            this.process_no.Visible = false;
            // 
            // suppliers_code
            // 
            this.suppliers_code.DataPropertyName = "suppliers_code";
            this.suppliers_code.HeaderText = "厂商代码";
            this.suppliers_code.Name = "suppliers_code";
            this.suppliers_code.Visible = false;
            // 
            // SampleLogoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 473);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SampleLogoList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择样品单";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCEDURE_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATERIAL_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn process_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn suppliers_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn size_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECEIVED_QUANTITY;
        private System.Windows.Forms.DataGridViewTextBoxColumn process_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn suppliers_code;
    }
}