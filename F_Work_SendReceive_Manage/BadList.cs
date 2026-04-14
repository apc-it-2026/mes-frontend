using F_Sample_SendReceive_Manage;
using F_Work_SendReceive_Manage;
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
    public partial class BadList : Form
    {
        string sample_gloal = "";
        public delegate void DataChangeHandlerItem(object sender, DataChangeEventArgsItem args);
        public event DataChangeHandlerItem DataChangeItem;
        public BadList()
        {
            InitializeComponent();
        }
        public BadList(string sample)
        {
            InitializeComponent();
            sample_gloal = sample;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("sample", sample);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.SCM_Work_ReceiveService", "QueryNGList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, string> dictionary2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(retData);
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dictionary2["body"]);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            OnDataChange(this,new  DataChangeEventArgsItem(dataGridView1.Rows[dataGridView1.CurrentRow.Index]));
            this.Close();
        }

        // 调用事件函数
        public void OnDataChange(object sender, DataChangeEventArgsItem args)
        {
            DataChangeItem?.Invoke(this, args);
        }

        public class DataChangeEventArgsItem : EventArgs
        {
            public DataGridViewRow rows { get; set; }
           // public string item_no { get; set; }
           // public string unit { get; set; }

            public DataChangeEventArgsItem(DataGridViewRow row)
            {
                rows = row;
                

            }
        }

    }
}
