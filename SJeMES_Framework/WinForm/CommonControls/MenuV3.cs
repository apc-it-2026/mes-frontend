using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SJeMES_Framework.WinForm.CommonControls
{
    public partial class MenuV3 : UserControl
    {
        public MenuItemV3 mi3;

        public bool IsClicked=false;

        public MenuV3(MenuItemV3 mi3)
        {
            this.mi3 = mi3;
            InitializeComponent();

            this.lab_Name.Text = mi3.MenuName;

            if (!string.IsNullOrEmpty(mi3.MenuImg))
            {
                pic_Head.Image = Image.FromFile(Application.StartupPath + @"\img\" + mi3.MenuImg.Replace(".png","") + @".png");
            }
        }


        private void mi_MouseEnter(object sender, EventArgs e)
        {

           ((Control)sender).BackColor = Color.FromArgb(83, 116, 149);
            lab_Name.ForeColor = Color.White;

        }

        private void mi_MuserLeave(object sender, EventArgs e)
        {

            ((Control)sender).BackColor = Color.White;
            lab_Name.ForeColor = Color.Gray;

        }

        public delegate void BtnClick(object sender, EventArgs e);
        //定义事件
        public event BtnClick BtnClicked;
        private void btn_Click(object sender, EventArgs e)
        {
            if (BtnClicked != null)
                BtnClicked(this, new EventArgs());//把按钮自身作为参数传递
        }

        private void MenuV3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Gray);
            p.Width = 1;
          
            g.DrawLine(p, 10, this.Height -5, this.Width -10, this.Height - 5);
        }
    }
}
