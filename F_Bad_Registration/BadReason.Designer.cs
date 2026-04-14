
namespace F_Bad_Registration
{
    partial class BadReason
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
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.code_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.code_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSure = new System.Windows.Forms.Button();
            this.txtBadReason = new System.Windows.Forms.TextBox();
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
            this.code_no,
            this.code_name});
            this.dataGridView1.Location = new System.Drawing.Point(2, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(275, 466);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 29.5203F;
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // code_no
            // 
            this.code_no.DataPropertyName = "code_no";
            this.code_no.HeaderText = "code_no";
            this.code_no.Name = "code_no";
            this.code_no.Visible = false;
            // 
            // code_name
            // 
            this.code_name.DataPropertyName = "code_name";
            this.code_name.FillWeight = 170.4797F;
            this.code_name.HeaderText = "不良原因";
            this.code_name.Name = "code_name";
            this.code_name.Width = 231;
            // 
            // btnSure
            // 
            this.btnSure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.btnSure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSure.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSure.Location = new System.Drawing.Point(192, 541);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(85, 35);
            this.btnSure.TabIndex = 4;
            this.btnSure.Text = "确认";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // txtBadReason
            // 
            this.txtBadReason.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBadReason.Location = new System.Drawing.Point(2, 541);
            this.txtBadReason.Multiline = true;
            this.txtBadReason.Name = "txtBadReason";
            this.txtBadReason.Size = new System.Drawing.Size(184, 35);
            this.txtBadReason.TabIndex = 5;
            this.txtBadReason.TextChanged += new System.EventHandler(this.txtBadReason_TextChanged);
            // 
            // BadReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 584);
            this.Controls.Add(this.txtBadReason);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BadReason";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "不良原因";
            this.Load += new System.EventHandler(this.BadReason_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn code_name;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.TextBox txtBadReason;
    }
}