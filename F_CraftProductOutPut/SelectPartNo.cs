using MaterialSkin.Controls;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_CraftProductOutPut
{
    public partial class SelectPartNo : MaterialForm
    {
        public SelectPartNo(DataTable dt)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public DataTable dtSizeNumPart;
            public DataChangeEventArgs(DataTable dtSizeNumPart)
            {
                this.dtSizeNumPart = dtSizeNumPart;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1 != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("partName", typeof(string));
                dataTable.Columns.Add("partNo", typeof(string));
                dataTable.Columns.Add("notPutSize", typeof(string));
                dataTable.Columns.Add("notPutNum", typeof(string));
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        if (row.Cells["putintostatus"].Value.ToString() == "已报完")
                        {
                            MessageHelper.ShowErr(this, "已报完的无法勾选！");
                            return;
                        }
                        DataRow dr = dataTable.NewRow();
                        dr["partName"] = row.Cells["part_name"].Value.ToString();
                        dr["notPutSize"] = row.Cells["notputsize"].Value.ToString();
                        dr["notPutNum"] = row.Cells["notputnum"].Value.ToString();
                        dr["partNo"] = row.Cells["part_no"].Value.ToString();
                        dataTable.Rows.Add(dr);
                    }
                }
                OnDataChange(this, new DataChangeEventArgs(dataTable));
                Close();
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "全选")
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = true;
                }
                button.Text = "全不选";
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

    }
}
