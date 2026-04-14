using MaterialSkin.Controls;
using SJeMES_Control_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_CraftProductPutInto
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
                int count = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        count++;
                    }
                }
                string[] sizeArray = new string[count];
                string[] partArray = new string[count];
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        if (row.Cells["putintostatus"].Value.ToString() == "已投完")
                        {
                            MessageHelper.ShowErr(this, "已投完的无法勾选！");
                            return;
                        }
                        string size_no = row.Cells["notputsize"].Value.ToString();

                        DataRow dr = dataTable.NewRow();
                        dr["partName"] = row.Cells["part_name"].Value.ToString();
                        dr["notPutSize"] = row.Cells["notputsize"].Value.ToString();
                        dr["notPutNum"] = row.Cells["notputnum"].Value.ToString();
                        dr["partNo"] = row.Cells["part_no"].Value.ToString();
                        dataTable.Rows.Add(dr);
                    }
                }
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                //    {
                //        if (dataGridView1.Rows[i].Cells["putintostatus"].Value.ToString() == "已投完")
                //        {
                //            MessageHelper.ShowErr(this, "已投完的无法勾选！");
                //            return;
                //        }
                //        string size_no = dataGridView1.Rows[i].Cells["notputsize"].Value.ToString();
                //        string part_no = dataGridView1.Rows[i].Cells["part_name"].Value.ToString();
                //        partArray[i] = part_no;
                //        sizeArray[i] = size_no;
                //        DataRow dr = dataTable.NewRow();
                //        dr["partName"] = dataGridView1.Rows[i].Cells["part_name"].Value.ToString();
                //        dr["notPutSize"] = dataGridView1.Rows[i].Cells["notputsize"].Value.ToString();
                //        dr["notPutNum"] = dataGridView1.Rows[i].Cells["notputnum"].Value.ToString();
                //        dr["partNo"] = dataGridView1.Rows[i].Cells["part_no"].Value.ToString();
                //        dataTable.Rows.Add(dr);
                //    }
                //}
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
        /// <summary>
        /// 查询数组中是否存在相同的值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static bool IsRepeat(string[] array)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < array.Length; i++)
            {
                if (ht.Contains(array[i]))
                {
                    return true;
                }
                else
                {
                    ht.Add(array[i], array[i]);
                }
            }
            return false;
        }
    }
}
