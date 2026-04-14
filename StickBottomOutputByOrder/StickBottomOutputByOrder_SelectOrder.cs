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

namespace StickBottomOutputByOrder
{
    public partial class StickBottomOutputByOrder_SelectOrder : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public StickBottomOutputByOrder_SelectOrder(DataTable dt)
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
                string VVworkorder = Convert.ToString(dataGridView1.Rows[index].Cells["main_order"].Value);
                string vSeDay = Convert.ToString(dataGridView1.Rows[index].Cells["se_day"].Value);
                string vPO = Convert.ToString(dataGridView1.Rows[index].Cells["po"].Value);
                string WorkOrder = Convert.ToString(dataGridView1.Rows[index].Cells["production_order"].Value);
                OnDataChange(this, new DataChangeEventArgs(WorkOrder, vSeDay, vPO, VVworkorder));

                this.Close();
            }

        }
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public string WorkOrder{ get; set; }
            public string vSeDay { get; set; }
            public string vPO { get; set; }
            public string VVworkorder { get; set; }
            
         



            public DataChangeEventArgs(string s1, string s2,string s3,string s4)
            {
                WorkOrder = s1;
                vSeDay = s2;
                vPO = s3;
                VVworkorder = s4;

            }
        }
    }
    
    
}
