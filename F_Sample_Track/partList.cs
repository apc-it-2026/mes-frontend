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

namespace F_Sample_Track
{
    public partial class partList : MaterialForm
    {
        private DataTable dataTable;
        public delegate void sengPartList(string name, string values);
        public event sengPartList sengMessage;

        static string content1 = "part_no";
        static string content2 = "name_t";
        public partList()
        {
            InitializeComponent();
        }

        public partList(DataTable dt)
        {
            InitializeComponent();
            this.dataTable = dt;
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["part_no"].Value.ToString();
            string value = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["name_t"].Value.ToString();
            sengMessage?.Invoke(name,value);
            this.Close();
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string textInfo = textBox1.Text;
            string[] pp;

            string filter = content1 + " like '%" + textInfo + "%'";
            if (!"".Equals(content2))
            {
                if (textInfo.Contains("#"))
                {
                    int k = 0;
                    pp = textInfo.Split('#');
                    foreach (var item in pp)
                    {
                        if (k == 0)
                        {
                            filter += " or " + content2 + " like '" + item + "%'";
                        }
                        else
                        {
                            filter += " and " + content2 + " like '" + item + "%'";

                        }
                        k++;
                    }
                }
                else
                {
                    filter += " or " + content2 + " like '" + textInfo + "%'";

                }

            }
            DataView dv = dataTable.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
    }
}
