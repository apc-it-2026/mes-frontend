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

namespace F_SendReceive_Manage
{
    public partial class SampleInfo : MaterialForm
    {
        public SampleInfo(DataTable dataTable,string sample,string art_no,string art_name,string purpose,string season)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = dataTable;
            label6.Text = sample;
            label7.Text = art_no;
            label8.Text = art_name;
            label9.Text = purpose;
            label10.Text = season;
        }
    }
}
