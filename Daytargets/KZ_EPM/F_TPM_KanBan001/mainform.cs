using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TPM_KanBan001
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
            System.Windows.Forms.Help.ShowHelp(this, "http://10.2.1.46:8077/#/login");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, "http://10.2.1.46:8077/#/login");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //System.Windows.Forms.Help.ShowHelp(this, "http://10.2.1.46:8077/#/login");
        }
    }
}
