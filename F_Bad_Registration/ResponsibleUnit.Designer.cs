
namespace F_Bad_Registration
{
    partial class ResponsibleUnit
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
            this.STATION_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATION_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtResUnit = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATION_NO,
            this.STATION_NAME});
            this.dataGridView1.Location = new System.Drawing.Point(2, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(287, 503);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // STATION_NO
            // 
            this.STATION_NO.DataPropertyName = "STATION_NO";
            this.STATION_NO.HeaderText = "责任单位代号";
            this.STATION_NO.Name = "STATION_NO";
            // 
            // STATION_NAME
            // 
            this.STATION_NAME.DataPropertyName = "STATION_NAME";
            this.STATION_NAME.HeaderText = "责任单位名称";
            this.STATION_NAME.Name = "STATION_NAME";
            // 
            // txtResUnit
            // 
            this.txtResUnit.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtResUnit.Location = new System.Drawing.Point(2, 574);
            this.txtResUnit.Multiline = true;
            this.txtResUnit.Name = "txtResUnit";
            this.txtResUnit.Size = new System.Drawing.Size(287, 35);
            this.txtResUnit.TabIndex = 7;
            this.txtResUnit.TextChanged += new System.EventHandler(this.txtResUnit_TextChanged);
            // 
            // ResponsibleUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 617);
            this.Controls.Add(this.txtResUnit);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResponsibleUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "责任单位";
            this.Load += new System.EventHandler(this.ResponsibleUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATION_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn STATION_NAME;
        private System.Windows.Forms.TextBox txtResUnit;
    }
}