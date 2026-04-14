namespace RPT_SFC_SuspendedWorkingHours
{
    partial class ArtQueryQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Query = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ART = new System.Windows.Forms.TextBox();
            this.dgv_art = new System.Windows.Forms.DataGridView();
            this.ColDeptNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_art)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Query);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ART);
            this.groupBox1.Location = new System.Drawing.Point(2, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(406, 78);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(290, 34);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(75, 23);
            this.Query.TabIndex = 9;
            this.Query.Text = "查询";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(67, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "ART：";
            // 
            // ART
            // 
            this.ART.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ART.Location = new System.Drawing.Point(115, 34);
            this.ART.Name = "ART";
            this.ART.Size = new System.Drawing.Size(159, 23);
            this.ART.TabIndex = 2;
            // 
            // dgv_art
            // 
            this.dgv_art.AllowUserToAddRows = false;
            this.dgv_art.AllowUserToDeleteRows = false;
            this.dgv_art.AllowUserToResizeColumns = false;
            this.dgv_art.AllowUserToResizeRows = false;
            this.dgv_art.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_art.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_art.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_art.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_art.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_art.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColDeptNo});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_art.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_art.Location = new System.Drawing.Point(2, 138);
            this.dgv_art.MultiSelect = false;
            this.dgv_art.Name = "dgv_art";
            this.dgv_art.ReadOnly = true;
            this.dgv_art.RowHeadersVisible = false;
            this.dgv_art.RowHeadersWidth = 51;
            this.dgv_art.RowTemplate.Height = 23;
            this.dgv_art.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_art.Size = new System.Drawing.Size(407, 475);
            this.dgv_art.TabIndex = 15;
            this.dgv_art.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ReturnArt);
//            this.dgv_art.Click += new System.EventHandler(this.ReturnArt);
            // 
            // ColDeptNo
            // 
            this.ColDeptNo.DataPropertyName = "prod_no";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColDeptNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColDeptNo.HeaderText = "ART";
            this.ColDeptNo.MinimumWidth = 6;
            this.ColDeptNo.Name = "ColDeptNo";
            this.ColDeptNo.ReadOnly = true;
            // 
            // ArtQueryQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 625);
            this.Controls.Add(this.dgv_art);
            this.Controls.Add(this.groupBox1);
            this.Name = "ArtQueryQuery";
            this.Text = "ArtSetQuery";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_art)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Query;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ART;
        private System.Windows.Forms.DataGridView dgv_art;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDeptNo;
    }
}