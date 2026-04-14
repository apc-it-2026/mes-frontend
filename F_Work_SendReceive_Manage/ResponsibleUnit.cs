using F_Sample_SendReceive_Manage;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Work_SendReceive_Manage
{
    public partial class ResponsibleUnit : MaterialForm
    {
        public ResponsibleUnit()
        {
            InitializeComponent();
        }
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public string result1;
            public string result2;
            public DataChangeEventArgs(string result1,string result2)
            {
                this.result1 = result1;
                this.result2 = result2;
            }
        }
        private void ResponsibleUnit_Load(object sender, EventArgs e)
        {
            SelectResponsibleUnit();
        }
        DataTable dtJson = new DataTable();
        private void SelectResponsibleUnit() 
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "GetResponUnit", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                dataGridView1.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0&&dataGridView1!=null)
            {
                string resUnit1 = dataGridView1.CurrentRow.Cells["STATION_NO"].Value.ToString();
                string resUnit2 = dataGridView1.CurrentRow.Cells["STATION_NAME"].Value.ToString();
                OnDataChange(this,new DataChangeEventArgs(resUnit1,resUnit2));
                Close();
            }
        }

        private void txtResUnit_TextChanged(object sender, EventArgs e)
        {
            string filter = "STATION_NAME like '%" + txtResUnit.Text.Trim() + "%'";
            DataView dv = dtJson.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
    }
}
