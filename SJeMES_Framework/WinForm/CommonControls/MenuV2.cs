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
    public partial class MenuV2 : UserControl
    {
        public MenuItemV2 mi2;

        public bool IsClicked=false;

        public bool GetChildren = false;

        public MenuV2(MenuItemV2 mi2)
        {
            this.mi2 = mi2;
            InitializeComponent();

            this.lab_Name.Text = mi2.MenuName;
        }


        public void SetBackColor(Color c)
        {
            this.lab_Name.BackColor = c;
        }

        public void SetFontColor(Color c)
        {
            this.lab_Name.ForeColor = c;
        }

        private void mi_MouseEnter(object sender, EventArgs e)
        {
            
                ((Control)sender).BackColor = Color.FromArgb(36, 78, 120);

        }

        private void mi_MuserLeave(object sender, EventArgs e)
        {
            
                ((Control)sender).BackColor = Color.FromArgb(67, 100, 131);
            
        }

        public delegate void BtnClick(object sender, EventArgs e);
        //定义事件
        public event BtnClick BtnClicked;
        private void btn_Click(object sender, EventArgs e)
        {
            if (BtnClicked != null)
                BtnClicked(this, new EventArgs());//把按钮自身作为参数传递
        }
    }
}
