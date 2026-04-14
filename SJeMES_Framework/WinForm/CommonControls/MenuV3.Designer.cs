namespace SJeMES_Framework.WinForm.CommonControls
{
    partial class MenuV3
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
            this.pic_Head = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Head)).BeginInit();
            this.SuspendLayout();
            // 
            // lab_Name
            // 
            this.lab_Name.BackColor = System.Drawing.Color.White;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.Color.Gray;
            this.lab_Name.Location = new System.Drawing.Point(43, 3);
            this.lab_Name.Margin = new System.Windows.Forms.Padding(0);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(137, 31);
            this.lab_Name.TabIndex = 0;
            this.lab_Name.Text = "lab_Name";
            this.lab_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lab_Name.Click += new System.EventHandler(this.btn_Click);
            this.lab_Name.MouseEnter += new System.EventHandler(this.mi_MouseEnter);
            this.lab_Name.MouseLeave += new System.EventHandler(this.mi_MuserLeave);
            // 
            // pic_Head
            // 
            this.pic_Head.Location = new System.Drawing.Point(16, 6);
            this.pic_Head.Name = "pic_Head";
            this.pic_Head.Size = new System.Drawing.Size(24, 24);
            this.pic_Head.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Head.TabIndex = 1;
            this.pic_Head.TabStop = false;
            // 
            // MenuV3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pic_Head);
            this.Controls.Add(this.lab_Name);
            this.Name = "MenuV3";
            this.Size = new System.Drawing.Size(180, 46);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MenuV3_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Head)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lab_Name;
        private System.Windows.Forms.PictureBox pic_Head;
    }
}
