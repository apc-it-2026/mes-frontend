namespace F_Abnormal_Registration
{
    partial class SelectCause
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
            this.butCheckAll1 = new System.Windows.Forms.Button();
            this.butOk1 = new System.Windows.Forms.Button();
            this.selectCause1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // butCheckAll1
            // 
            this.butCheckAll1.Location = new System.Drawing.Point(4, 67);
            this.butCheckAll1.Name = "butCheckAll1";
            this.butCheckAll1.Size = new System.Drawing.Size(62, 25);
            this.butCheckAll1.TabIndex = 0;
            this.butCheckAll1.Text = "全选";
            this.butCheckAll1.UseVisualStyleBackColor = true;
            this.butCheckAll1.Click += new System.EventHandler(this.butCheckAll1_Click);
            // 
            // butOk1
            // 
            this.butOk1.Location = new System.Drawing.Point(255, 67);
            this.butOk1.Name = "butOk1";
            this.butOk1.Size = new System.Drawing.Size(62, 25);
            this.butOk1.TabIndex = 1;
            this.butOk1.Text = "确定";
            this.butOk1.UseVisualStyleBackColor = true;
            this.butOk1.Click += new System.EventHandler(this.butOk1_Click);
            // 
            // selectCause1
            // 
            this.selectCause1.Location = new System.Drawing.Point(72, 69);
            this.selectCause1.Name = "selectCause1";
            this.selectCause1.Size = new System.Drawing.Size(175, 21);
            this.selectCause1.TabIndex = 2;
            this.selectCause1.TextChanged += new System.EventHandler(this.selectCause1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(4, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(313, 351);
            this.panel1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(313, 351);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "code_name";
            this.Column2.HeaderText = "原因";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // SelectCause
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectCause1);
            this.Controls.Add(this.butOk1);
            this.Controls.Add(this.butCheckAll1);
            this.Name = "SelectCause";
            this.Text = "异常原因";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butCheckAll1;
        private System.Windows.Forms.Button butOk1;
        private System.Windows.Forms.TextBox selectCause1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}