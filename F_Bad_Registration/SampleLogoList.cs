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

namespace F_Bad_Registration
{
    public partial class SampleLogoList : MaterialForm
    {
        DataTable dataDt = null;
        public SampleLogoList(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
            dataDt = dt;
        }
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public DataTable dt;
            public DataChangeEventArgs(DataTable dt)
            {
                this.dt = dt;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "全选")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = true;
                }
                button.Text = "取消全选";
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = false;
                }
                button.Text = "全选";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1 != null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("part_name",typeof(string));
                dt.Columns.Add("process_name", typeof(string));
                dt.Columns.Add("suppliers_name", typeof(string));
                dt.Columns.Add("size_no", typeof(string));
                dt.Columns.Add("quantity", typeof(string));
                dt.Columns.Add("received_quantity", typeof(string));
                dt.Columns.Add("part_no", typeof(string));
                dt.Columns.Add("process_no", typeof(string));
                dt.Columns.Add("suppliers_code", typeof(string));
                dt.Columns.Add("PROCEDURE_no",typeof(string));
                dt.Columns.Add("MATERIAL_NO",typeof(string));
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        DataRow dr = dt.NewRow();
                        dr["part_name"] = row.Cells["part_name"].Value.ToString();
                        dr["process_name"] = row.Cells["process_name"].Value.ToString();
                        dr["suppliers_name"] = row.Cells["suppliers_name"].Value.ToString();
                        dr["size_no"] = row.Cells["size_no"].Value.ToString();
                        dr["quantity"] = row.Cells["quantity"].Value.ToString();
                        dr["received_quantity"] = row.Cells["received_quantity"].Value.ToString();
                        dr["part_no"] = row.Cells["part_no"].Value.ToString();
                        dr["process_no"] = row.Cells["process_no"].Value.ToString();
                        dr["suppliers_code"] = row.Cells["suppliers_code"].Value.ToString();
                        dr["PROCEDURE_no"] = row.Cells["PROCEDURE_no"].Value.ToString();
                        dr["MATERIAL_NO"] = row.Cells["MATERIAL_NO"].Value.ToString();
                        dt.Rows.Add(dr);
                    }
                }
                OnDataChange(this, new DataChangeEventArgs(dt));
                Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filter = "part_name like '%" + textBox1.Text.Trim() + "%'";
            DataView dv = dataDt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
    }
}
