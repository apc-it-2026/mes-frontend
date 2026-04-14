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

namespace F_CraftProductOutPut
{
    public partial class OutPutSelectCraft : MaterialForm
    {
        public OutPutSelectCraft(DataTable dt)
        {
            InitializeComponent();
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
            public DataTable dtCarft;
            public DataChangeEventArgs(DataTable dt)
            {
                this.dtCarft = dt;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.DataSource != null && dataGridView1.Rows.Count > 0)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("process_no", typeof(string));
                dataTable.Columns.Add("process_name", typeof(string));
                DataRow dr = dataTable.NewRow();
                dr["process_no"] = dataGridView1.Rows[e.RowIndex].Cells["process_no"].Value.ToString();
                dr["process_name"] = dataGridView1.Rows[e.RowIndex].Cells["process_name"].Value.ToString();
                dataTable.Rows.Add(dr);
                OnDataChange(this, new DataChangeEventArgs(dataTable));
                Close();
            }
        }
    }
}
