
namespace F_CraftProductPutInto
{
    partial class SelectPartNo
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
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.part_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.part_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.putintostatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notputnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notputsize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.part_no,
            this.part_name,
            this.putintostatus,
            this.notputnum,
            this.notputsize});
            this.dataGridView1.Location = new System.Drawing.Point(5, 68);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(722, 369);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOK.Location = new System.Drawing.Point(645, 452);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 33);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnCheckAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckAll.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckAll.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCheckAll.Location = new System.Drawing.Point(557, 452);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(82, 33);
            this.btnCheckAll.TabIndex = 6;
            this.btnCheckAll.Text = "全选";
            this.btnCheckAll.UseVisualStyleBackColor = false;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 126.9036F;
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
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
            this.part_name.FillWeight = 116.4108F;
            this.part_name.HeaderText = "部件名称";
            this.part_name.Name = "part_name";
            this.part_name.Width = 158;
            // 
            // putintostatus
            // 
            this.putintostatus.DataPropertyName = "putintostatus";
            this.putintostatus.FillWeight = 85.5619F;
            this.putintostatus.HeaderText = "投入状态";
            this.putintostatus.Name = "putintostatus";
            this.putintostatus.Width = 117;
            // 
            // notputnum
            // 
            this.notputnum.DataPropertyName = "notputnum";
            this.notputnum.FillWeight = 85.5619F;
            this.notputnum.HeaderText = "未投入数量";
            this.notputnum.Name = "notputnum";
            this.notputnum.Width = 116;
            // 
            // notputsize
            // 
            this.notputsize.DataPropertyName = "notputsize";
            this.notputsize.FillWeight = 85.5619F;
            this.notputsize.HeaderText = "未投入尺码";
            this.notputsize.Name = "notputsize";
            this.notputsize.Width = 116;
            // 
            // SelectPartNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 497);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SelectPartNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部件查询";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn part_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn putintostatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn notputnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn notputsize;
    }
}