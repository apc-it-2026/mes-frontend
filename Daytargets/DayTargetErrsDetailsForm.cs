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

namespace DayTargets
{
    public partial class ErrsDetailsForm : MaterialForm
    {
        DataTable dtErrs;

        public ErrsDetailsForm()
        {
            InitializeComponent();
        }

        public ErrsDetailsForm(DataTable datatable)
        {
            InitializeComponent();
            dtErrs = datatable;
        }

        private void ErrsDetailsForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = dtErrs.DefaultView;
            this.dataGridView1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
