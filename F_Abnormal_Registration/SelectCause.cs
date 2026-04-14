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

namespace F_Abnormal_Registration
{
    public partial class SelectCause : MaterialForm
    {

        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        DataTable dataDt = null;
        string condition1;
        int checkTime = 0;

        public SelectCause(DataTable dt, string t1)
        {
            InitializeComponent();
            dataDt = dt;
            condition1 = t1;
            dataGridView1.DataSource = dataDt;
        }


        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }

        public class DataChangeEventArgs : EventArgs
        {
            public string value1 { get; set; }

            public DataChangeEventArgs(string t1)
            {
                value1 = t1;
            }
        }
        
        private void butOk1_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1)
            //{
            //    int index = dataGridView1.CurrentRow.Index;
            //    string value1 = dataGridView1.Rows[index].Cells[1].Value.ToString();
            //    OnDataChange(this, new DataChangeEventArgs(value1));
            //}

            string reqs = "";
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    string orderSep = row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString();
                    if (!"".Equals(orderSep))
                    {
                        if ("".Equals(reqs))
                            reqs += orderSep;
                        else
                            reqs += "," + orderSep;
                    }
                }
            }
            OnDataChange(this, new DataChangeEventArgs(reqs));
            this.Close();
        }

        private void selectCause1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string textInfo = selectCause1.Text;
            string filter = condition1 + " like '%" + textInfo + "%'";
            DataView dv = dataDt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }

        private void butCheckAll1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                checkTime++;
                if (checkTime % 2 == 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells[0].Value = false;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        row.Cells[0].Value = true;
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string value1 = dataGridView1.Rows[index].Cells[1].Value.ToString();
                OnDataChange(this, new DataChangeEventArgs(value1));
                this.Close();
                this.Dispose();
            }
        }

        private void ItemSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.selectCause1.Text = "";

        }

    }
}
