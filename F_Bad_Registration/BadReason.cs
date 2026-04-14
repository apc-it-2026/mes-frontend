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

namespace F_Bad_Registration
{
    public partial class BadReason : MaterialForm
    {
        public BadReason()
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
            public string result;
            public DataChangeEventArgs(string result)
            {
                this.result = result;
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1 != null)
            {
                string str = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                            str += row.Cells[2].Value.ToString() + ",";
                    }
                }
                str=str.Substring(0,str.LastIndexOf(','));
                OnDataChange(this, new DataChangeEventArgs(str));
                Close();
            }
        }

        private void BadReason_Load(object sender, EventArgs e)
        {
            LoadDatagridView();
        }
        DataTable dt = new DataTable();
        private void LoadDatagridView()
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectBadCause", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void txtBadReason_TextChanged(object sender, EventArgs e)
        {
            string filter = "code_name like '%" + txtBadReason.Text.Trim() + "%'";
            DataView dv = dt.DefaultView;
            dv.RowFilter = filter;
            dataGridView1.DataSource = dv;
        }
    }
}
