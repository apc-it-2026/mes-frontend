using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace F_TMS_TierMeeting2_Main
{
    class Waiting : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        public Waiting()
        {
            InitializeComponent();
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            ControlBox = true;
            BringToFront();
            TopMost = true;
            Focus();
        }

        private TableLayoutPanel tableLayoutPanel1;
        private ProgressBar pB;
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waiting));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new CusContorl.CusTableLayoutPanel();
            this.pB = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pB, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // pB
            // 
            resources.ApplyResources(this.pB, "pB");
            this.pB.Name = "pB";
            this.pB.Step = 1;
            // 
            // Waiting
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.tableLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.Name = "Waiting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.Waiting_Load);
            this.Shown += new System.EventHandler(this.Waiting_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.PictureBox pictureBox1;
        private bool isFinished = false;
        private delegate void GetDataCallback();
        private void progressBar()
        {
            if (this.InvokeRequired)
            {
                GetDataCallback d = new GetDataCallback(progressBar);
                this.Invoke(d);
            }
            else
            {
                loadProgressBar();
            }
        }
        private void loadProgressBar() {
            for (int i = 0; i < pB.Maximum; i++)
            {
                if (!isFinished)
                {
                    Thread.Sleep(2);
                    pB.Value = i;
                }
            }
        }

        private void Waiting_Load(object sender, EventArgs e)
        {
            Thread runProgressBar = new Thread(progressBar);
            runProgressBar.Start();
        }

        private void Waiting_Shown(object sender, EventArgs e)
        {

        }
    }

}
