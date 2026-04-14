using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Kanban
{
    public partial class TierMettingMaintain : MaterialForm
    {
        public TierMettingMaintain()
        {
            InitializeComponent();
            //label49.Text = DateTime.Now.ToShortDateString();
            //label62.Text =DateTime.Now.ToShortDateString();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public TierMettingMaintain(string dateTime)
        {
            InitializeComponent();
            label49.Text = dateTime;
            label62.Text = dateTime;
        }

        private void TierMettingMaintain_Load(object sender, EventArgs e)
        {
            Win32.AnimateWindow(this.Handle, 2000, Win32.AW_BLEND);
        }

        private void TierMettingMaintain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32.AnimateWindow(this.Handle, 2000, Win32.AW_SLIDE | Win32.AW_HIDE | Win32.AW_BLEND);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
