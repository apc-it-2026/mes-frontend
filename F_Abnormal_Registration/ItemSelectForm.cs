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
    public partial class ItemSelectForm : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, ItemDataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        DataTable dataDt = null;
        int checkTime = 0;

        public ItemSelectForm(DataTable dt)
        {
            InitializeComponent();
            dataDt = dt;
            this.dataGridView1.DataSource = dt;
        }

        // 调用事件函数
        public void OnDataChange(object sender, ItemDataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }

        public class ItemDataChangeEventArgs : EventArgs
        {
            public DataTable value1 { get; set; }

            public ItemDataChangeEventArgs(DataTable dt)
            {
                value1 = dt;
            }
        }

        //private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int index = e.RowIndex;
        //    if (index > -1)
        //    {
        //        string value1 = dataGridView1.Rows[index].Cells[0].Value.ToString();
        //        OnDataChange(this, new ItemDataChangeEventArgs(value1));
        //        this.Close();
        //        this.Dispose();
        //    }
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1)
            //{
            //    int index = dataGridView1.CurrentRow.Index;
            //    string value1 = dataGridView1.Rows[index].Cells[0].Value.ToString();
            //    OnDataChange(this, new ItemDataChangeEventArgs(value1));
            //}

            DataTable dt = dataDt.Clone();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue)
                {
                    string procedureNo = row.Cells[dataGridView1.Columns["PROCEDURE_NO"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["PROCEDURE_NO"].Index].Value.ToString();
                    string materialNo = row.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value.ToString();
                    string partNo = row.Cells[dataGridView1.Columns["PART_NO"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["PART_NO"].Index].Value.ToString();
                    string namet = row.Cells[dataGridView1.Columns["NAME_T"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["NAME_T"].Index].Value.ToString();
                    string supplierscode = row.Cells[dataGridView1.Columns["SUPPLIERS_CODE"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["SUPPLIERS_CODE"].Index].Value.ToString();
                    string suppliersname = row.Cells[dataGridView1.Columns["SUPPLIES_NAME"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["SUPPLIES_NAME"].Index].Value.ToString();
                    string sizeno = row.Cells[dataGridView1.Columns["SIZE_NO"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["SIZE_NO"].Index].Value.ToString();
                    string quantity = row.Cells[dataGridView1.Columns["QUANTITY"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["QUANTITY"].Index].Value.ToString();
                    string receivedquantity = row.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value.ToString();
                    string registedqty = row.Cells[dataGridView1.Columns["registed_qty"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["registed_qty"].Index].Value.ToString();
                    string processNo = row.Cells[dataGridView1.Columns["PROCESS_NO"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["PROCESS_NO"].Index].Value.ToString();
                    string processName = row.Cells[dataGridView1.Columns["PROCESS_NAME"].Index].Value == null ? "" : row.Cells[dataGridView1.Columns["PROCESS_NAME"].Index].Value.ToString();

                    DataRow r = dt.NewRow();
                    r["PROCEDURE_NO"] = procedureNo;
                    r["MATERIAL_NO"] = materialNo;
                    r["PART_NO"] = partNo;
                    r["NAME_T"] = namet;
                    r["SUPPLIERS_CODE"] = supplierscode;
                    r["SUPPLIES_NAME"] = suppliersname;
                    r["SIZE_NO"] = sizeno;
                    r["QUANTITY"] = quantity;
                    r["RECEIVED_QUANTITY"] = receivedquantity;
                    r["registed_qty"] = registedqty;
                    r["PROCESS_NO"] = processNo;
                    r["PROCESS_NAME"] = processName;

                    dt.Rows.Add(r);
                }
            }
            OnDataChange(this, new ItemDataChangeEventArgs(dt));

            this.Close();
        }

        private void textSelect_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            string textInfo = textSelect.Text;
            string filter = "NAME_T like '%" + textInfo + "%' or SUPPLIES_NAME like '%" + textInfo + "%' or PROCEDURE_NO like '%" + textInfo + "%' or MATERIAL_NO like '%" + textInfo + "%' or SIZE_NO like '%" + textInfo + "%' or Process_name like '%" + textInfo + "%'";
            DataView dv = dataDt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }

        private void ItemSelectForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
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

        private void ItemSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.textSelect.Text = "";

        }


       
    }
}
