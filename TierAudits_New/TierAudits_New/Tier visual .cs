using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
{
    public partial class Tier_visual : Form
    {
       
        public Tier_visual()
        {
            InitializeComponent();
            toolTip.AutoPopDelay = 5000; // The time, in milliseconds, that the ToolTip remains visible
            toolTip.InitialDelay = 1000; // The time, in milliseconds, that passes before the ToolTip appears
            toolTip.ReshowDelay = 500; // The time, in milliseconds, that passes before subsequent ToolTip windows appear as the pointer moves from one ToolTip region to another
                                       // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Set up the ToolTip text for the Buttons.
            toolTip.SetToolTip(this.IssuesBtnn, "click for Production Issues & solutions ");
            toolTip.SetToolTip(this.KaizaanBtn, "click for Kaizen ");
            toolTip.SetToolTip(this.workInjuryBtn, "click for Work Injuries ");
            toolTip.SetToolTip(this.SMEbutton, "click for SME ");
            toolTip.SetToolTip(this.button5, "click for Plant Kaizen ");
            toolTip.SetToolTip(this.button4, "click for Plant 6S");

        }

        private void button1_Click(object sender, EventArgs e) 
        {
            Form2 tier2 = new Form2();
            tier2.Show();
            //System.Windows.Forms.MessageBox.Show("TIER 2 in progress...", "Processing",
            //System.Windows.Forms.MessageBoxButtons.OK,
            //System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form4 tier4 = new Form4();
            tier4.Show();


            //System.Windows.Forms.MessageBox.Show("TIER 4 in progress...", "Processing",
            //System.Windows.Forms.MessageBoxButtons.OK,
            //System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Tier_4 tier4 = new Tier_4();
            //tier4.Show();

            System.Windows.Forms.MessageBox.Show("TIER 4 in progress...", "Processing",
            System.Windows.Forms.MessageBoxButtons.OK,
            System.Windows.Forms.MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 tier1 = new Form1();
            tier1.Show();
        }

        //private void button4_Click_1(object sender, EventArgs e)//issues & solutions
        //{

        //}

        //private void IssuesBtnn_Click(object sender, EventArgs e)
        //{
        //    issues isu = new issues();
        //    isu.Show();
        //}

        //private void KaizaanBtn_Click(object sender, EventArgs e)
        //{
        //    kaizen kzan = new kaizen();
        //    kzan.Show();
        //}

        // First, create a ToolTip instance at the class level
        private ToolTip toolTip = new ToolTip();

       

        private void IssuesBtnn_Click(object sender, EventArgs e)
        {
            issues isu = new issues();
            isu.Show();
        }

        private void KaizaanBtn_Click(object sender, EventArgs e)
        {
            kaizen kzann = new kaizen();
            kzann.Show();
        }

        private void workInjuryBtn_Click(object sender, EventArgs e)
        {
            WorkInjury workInjur = new WorkInjury();
            workInjur.Show();
        }

        private void SMEbutton_Click(object sender, EventArgs e)
        {
            LoginSME smelog = new LoginSME();
            smelog.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Plant_kaizen PlntKzn = new Plant_kaizen();
            PlntKzn.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            LoginSixS PlntSixS = new LoginSixS();
            PlntSixS.Show();
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    smeFrm smeorg = new smeFrm();
        //    smeorg.Show();
        //}
    }
}
