using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace F_BarcodePrinting_WakeOrder
{
    public partial class F_MMS_SelectTurn : MaterialForm
    {
        public F_MMS_SelectTurn(DataTable dataTable)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            dataGridView1.DataSource = dataTable;

        }


        //Define the data type required by a delegate
        public class DataChangeEventArgs : EventArgs
        {
            public string turn { get; set; }

            public string production_line { get; set; }
            public DataChangeEventArgs(string turn,string production_line)
            {
                this.turn = turn;
                this.production_line = production_line;
            }
        }

        //define a delegate
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        //define a delegated event
        public event DataChangeHandler datachange;

        //If the delegated event is not null, the delegate is called
        public void onDataChange(object sender,DataChangeEventArgs args)
        {
            datachange?.Invoke(this, args);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string turn = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string production_line = dataGridView1.Rows[index].Cells[2].Value.ToString();
                onDataChange(this, new DataChangeEventArgs(turn,production_line));
                this.Close();
            }
        }
    }   
}
