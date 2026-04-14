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
        //Timer timer = new Timer();
        //public LoadingProgress()
        //{
        //    InitializeComponent();
        //    timer.Interval = 10;
        //    timer.Enabled = true;
        //    timer.Tick += Timer_Tick;
        //}

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    if(progressBar1.Value < 100)
        //    {
        //        progressBar1.Value++;
        //    }
        //    else
        //    {
        //        timer.Enabled = false;
        //    }


        //}

        private Timer timer;
        private int progressValue;

        public LoadingProgress()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 50; // Adjust interval as needed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressValue < 100)
            {
                progressValue++;
                progressBar1.Value = progressValue; // Update the progress bar value
            }
            else
            {
                timer.Stop(); // Stop the timer when progress reaches 100%
                Close(); // Close the form
            }
        }


    }
}
