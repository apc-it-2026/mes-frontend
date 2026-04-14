namespace SJeMES_Framework.WinForm.CommonControls
{
    partial class MenuV2
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lab_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lab_Name
            // 
            this.lab_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.Color.White;
            this.lab_Name.Location = new System.Drawing.Point(0, 0);
            this.lab_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(180, 46);
            this.lab_Name.TabIndex = 0;
            this.lab_Name.Text = "lab_Name";
            this.lab_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lab_Name.Click += new System.EventHandler(this.btn_Click);
            this.lab_Name.MouseEnter += new System.EventHandler(this.mi_MouseEnter);
            this.lab_Name.MouseLeave += new System.EventHandler(this.mi_MuserLeave);
            // 
            // MenuV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(100)))), ((int)(((byte)(131)))));
            this.Controls.Add(this.lab_Name);
            this.Name = "MenuV2";
            this.Size = new System.Drawing.Size(180, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lab_Name;
    }
}
