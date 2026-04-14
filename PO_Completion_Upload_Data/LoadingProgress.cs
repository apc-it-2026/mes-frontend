using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PO_Completion_Upload_Data
{
    public partial class LoadingProgress : Form
    {
        Timer timer = new Timer();
        public LoadingProgress()
        {
            InitializeComponent();
            timer.Interval = 1;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value < 100)
            {
                progressBar1.Value++;
            }
            else
            {
                timer.Enabled = false;
            }


        }




    }
}
