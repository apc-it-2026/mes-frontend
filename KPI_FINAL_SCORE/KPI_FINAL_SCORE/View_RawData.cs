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

namespace KPI_FINAL_SCORE
{
    public partial class View_RawData :Form
    {
        DataTable DT = new DataTable();
        public View_RawData(DataTable dt)
        {
            InitializeComponent();
            DT = dt;
        }

        private void View_RawData_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DT;
        }
    }
}
