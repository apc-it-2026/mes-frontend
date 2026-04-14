using F_SFC_Shift_Setting;
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

namespace F_SFC_TrackIn_List
{
    public partial class OrgSelectForm   : MaterialForm
    {
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;

        DataTable job_dt = null;
        public OrgSelectForm  ()
        {
       
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            this.dataGridView1.AutoGenerateColumns = false;
            LoadJobsList();
        }

        public OrgSelectForm  (DataTable jobDt)
        {

            InitializeComponent();
            job_dt = jobDt;
            dataGridView1.DataSource = jobDt;
        }

        private void LoadJobsList()
        {
            Font a = new Font("宋体", 15);
            dataGridView1.Font = a;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.SFC_Shift_SettingServer", "LoadOrg", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                job_dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Rows[0].Selected = false;
                }
                else
                {
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        // call event function
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index > -1)
            {
                string dep_no = dataGridView1.Rows[index].Cells[0].Value.ToString();
                string dep_name = dataGridView1.Rows[index].Cells[1].Value.ToString();

                OnDataChange(this, new DataChangeEventArgs(dep_no, dep_name));

                this.Close();
            }
        }

        private void textJobSelect_TextChanged(object sender, EventArgs e)
        {
            string job = textJobSelect.Text;

            DataView dv = job_dt.DefaultView;
            dv.RowFilter = "org_code like '%" + job + "%' or org_name like '%" + job + "%' ";//filter
            dataGridView1.DataSource = dv;
        }
    }
}
