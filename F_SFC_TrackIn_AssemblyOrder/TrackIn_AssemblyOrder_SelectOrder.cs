using F_SFC_TrackIn_AssemblyOrder;
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

namespace StickBottomInputByOrder
{
    public partial class TrackIn_AssemblyOrder_SelectOrder : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public TrackIn_AssemblyOrder_SelectOrder(DataTable dt)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string vProdOrder = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string vSubOrder = dataGridView1.Rows[index].Cells[1].Value.ToString();
                string vPO = dataGridView1.Rows[index].Cells[2].Value.ToString();
                string vSeQty = dataGridView1.Rows[index].Cells[3].ToString();
                string vSeDay = dataGridView1.Rows[index].Cells[4].Value.ToString();
                
                OnDataChange(this, new DataChangeEventArgs(vProdOrder, vSubOrder, vPO, vSeQty, vSeDay));

                this.Close();
            }

        }
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public string vProdOrder { get; set; }
            public string vSubOrder { get; set; }
            public string vPO { get; set; }
            public string vSeQty { get; set; }
            public string vSeDay { get; set; }


            public DataChangeEventArgs(string s1, string s2, string s3, string s4, string s5)
            {
                vProdOrder = s1;
                vSubOrder = s2;
                vPO = s3;
                vSeQty = s4;
                vSeDay = s5;

            }
        }
    }
    
    
}
