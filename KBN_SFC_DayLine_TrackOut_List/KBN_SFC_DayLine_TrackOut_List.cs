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

namespace KBN_SFC_DayLine_TrackOut_List
{
    public partial class KBN_SFC_DayLine_TrackOut_List : MaterialForm
    {
        public KBN_SFC_DayLine_TrackOut_List()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            DayLine_TrackOut_List_DashBoard frm = new DayLine_TrackOut_List_DashBoard();
            frm.ShowDialog();
            System.Threading.Thread.Sleep(200);
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            DayLine_TrackOut_List_halftime frm = new DayLine_TrackOut_List_halftime();
            frm.ShowDialog();
            System.Threading.Thread.Sleep(200);
            Show();
        }
    }
}
