namespace SJeMES_Framework.WinForm.CommonControls
{
    partial class MenuV1
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
            this.mi_Head = new System.Windows.Forms.Panel();
            this.pic_Head = new System.Windows.Forms.PictureBox();
            this.lab_Name = new System.Windows.Forms.Label();
            this.menu_v2 = new System.Windows.Forms.FlowLayoutPanel();
            this.mi_Head.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Head)).BeginInit();
            this.SuspendLayout();
            // 
            // mi_Head
            // 
            this.mi_Head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(116)))), ((int)(((byte)(149)))));
            this.mi_Head.Controls.Add(this.pic_Head);
            this.mi_Head.Controls.Add(this.lab_Name);
            this.mi_Head.Dock = System.Windows.Forms.DockStyle.Top;
            this.mi_Head.Location = new System.Drawing.Point(0, 0);
            this.mi_Head.Name = "mi_Head";
            this.mi_Head.Size = new System.Drawing.Size(180, 46);
            this.mi_Head.TabIndex = 0;
            this.mi_Head.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mi_Head_MouseClick);
            this.mi_Head.MouseEnter += new System.EventHandler(this.mi_MouseEnter);
            this.mi_Head.MouseLeave += new System.EventHandler(this.mi_MuserLeave);
            // 
            // pic_Head
            // 
            this.pic_Head.Location = new System.Drawing.Point(11, 11);
            this.pic_Head.Name = "pic_Head";
            this.pic_Head.Size = new System.Drawing.Size(24, 24);
            this.pic_Head.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Head.TabIndex = 0;
            this.pic_Head.TabStop = false;
            this.pic_Head.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mi_Head_MouseClick);
            this.pic_Head.MouseEnter += new System.EventHandler(this.mi_MouseEnter);
            this.pic_Head.MouseLeave += new System.EventHandler(this.mi_MuserLeave);
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Name.ForeColor = System.Drawing.Color.White;
            this.lab_Name.Location = new System.Drawing.Point(41, 12);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(90, 22);
            this.lab_Name.TabIndex = 1;
            this.lab_Name.Text = "lab_Name";
            this.lab_Name.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mi_Head_MouseClick);
            this.lab_Name.MouseEnter += new System.EventHandler(this.mi_MouseEnter);
            this.lab_Name.MouseLeave += new System.EventHandler(this.mi_MuserLeave);
            // 
            // menu_v2
            // 
            this.menu_v2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menu_v2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menu_v2.Location = new System.Drawing.Point(0, 46);
            this.menu_v2.Margin = new System.Windows.Forms.Padding(0);
            this.menu_v2.Name = "menu_v2";
            this.menu_v2.Size = new System.Drawing.Size(180, 303);
            this.menu_v2.TabIndex = 1;
            // 
            // MenuV1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(100)))), ((int)(((byte)(131)))));
            this.Controls.Add(this.menu_v2);
            this.Controls.Add(this.mi_Head);
            this.Name = "MenuV1";
            this.Size = new System.Drawing.Size(180, 349);
            this.mi_Head.ResumeLayout(false);
            this.mi_Head.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Head)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mi_Head;
        private System.Windows.Forms.Label lab_Name;
        private System.Windows.Forms.PictureBox pic_Head;
        private System.Windows.Forms.FlowLayoutPanel menu_v2;
    }
}
