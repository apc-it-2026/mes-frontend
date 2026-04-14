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
    public partial class MenuV1 : UserControl
    {
        public MenuItemV1 mi1;
        public bool GetChildren = false;

        public MenuV1(MenuItemV1 mi1)
        {
            this.mi1 = mi1;
            InitializeComponent();
            lab_Name.Text = mi1.MenuName;
            if (!string.IsNullOrEmpty(mi1.MenuImg))
            {
                pic_Head.Image = Image.FromFile(Application.StartupPath + @"\img\" + mi1.MenuImg.Replace(".png","") + @".png");
            }

            this.VScroll = true;

        }

        public void InitControl()
        {
            for(int i=1;i<=mi1.MenuItemV2s.Keys.Count;i++)
            {

                this.menu_v2.Controls.Add(mi1.MenuItemV2s[i].Control);
            }

            this.Height = 46;
        }

        private void mi_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                ((Panel)sender).BackColor = Color.FromArgb(36, 78, 120);
            }
            catch
            {
                ((Panel)((Control)sender).Parent).BackColor = Color.FromArgb(36, 78, 120);
            }
        }

        private void mi_MuserLeave(object sender, EventArgs e)
        {
            try
            {
                ((Panel)sender).BackColor = Color.FromArgb(83, 116, 149);
            }
            catch
           
            {
                ((Panel)((Control)sender).Parent).BackColor = Color.FromArgb(83, 116, 149);
            }
        
        }

        private void mi_Head_MouseClick(object sender, MouseEventArgs e)
        {
            if (BtnClicked != null)
                BtnClicked(this, new EventArgs());//把按钮自身作为参数传递

            if (this.Height == 46)
            {
                this.Height = 46 * (this.mi1.MenuItemV2s.Count + 1);
                this.Height += 5 * (this.mi1.MenuItemV2s.Count + 1);
            }
            else
            {
                this.Height = 46;
            }

           

        }

        public delegate void BtnClick(object sender, EventArgs e);
        //定义事件
        public event BtnClick BtnClicked;
       
    }
}
