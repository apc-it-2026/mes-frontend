using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace BI_Reports_OnePage
{
    public partial class CustomMessageBox : Form
    {

        
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
        }
       
        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            Random ran = new Random();
            int col1 = ran.Next(0, 255);
            int col2 = ran.Next(0, 245);
            int col3 = ran.Next(0, 235);
            int col4 = ran.Next(0, 205);

            //label1.ForeColor = Color.FromArgb(col1, col2, col3, col4);
            label2.ForeColor = Color.FromArgb(col1, col2, col3, col4);
            label3.BackColor = Color.FromArgb(col1, col2, col3, col4);
            label1.Visible = !label1.Visible;
        }
    }
}
