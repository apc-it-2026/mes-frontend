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

namespace F_SFC_Holiday_Date_Report
{
    public partial class Holiday_DateForm : MaterialForm
    {
        public Holiday_DateForm()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            InitializeComponent();
        }
        private DataTable GetOrg()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOrg", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        private void Holiday_DateForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false; //禁止自动生成列
            DataTable dtJson = GetOrg();
            if (dtJson != null && dtJson != null && dtJson.Rows.Count > 0)
            {
                comboBoxOrgId.DisplayMember = "org_name";
                comboBoxOrgId.ValueMember = "org_id";
                comboBoxOrgId.DataSource = dtJson;
                comboBoxOrgId.SelectedIndex = 0; //默认为第一项
            }
            if (dtJson != null && dtJson != null && dtJson.Rows.Count > 0)
            {
                comboBoxOrgId2.DisplayMember = "org_name";
                comboBoxOrgId2.ValueMember = "org_id";
                comboBoxOrgId2.DataSource = dtJson;
                comboBoxOrgId2.SelectedIndex = 0; //默认为第一项
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(comboBoxOrgId.Text))
                {
                    MessageBox.Show("Information is incomplete！！");
                    return;
                }

                Dictionary<string, object> parm = new Dictionary<string, object>();
                parm.Add("org_id", comboBoxOrgId.SelectedValue);
                parm.Add("datee", dateTimePicker1.Value.ToShortDateString());
                parm.Add("date_name", textBox1.Text);
                parm.Add("datetype", comboBox1.Text.Split('|')[0]);

                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Add_HolidayDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));

                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {
                    string json = retJson["RetData"].ToString();
                    int dtJson = JsonConvert.DeserializeObject<Int16>(json);
                    if (dtJson > 0)
                    {
                        MessageHelper.ShowSuccess(this, "Added successfully！");
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "add failed！");
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("date_end", dateTimePicker3.Value.ToShortDateString());
            parm.Add("date_start", dateTimePicker2.Value.ToShortDateString());
            parm.Add("org_id", comboBoxOrgId2.SelectedValue);
            parm.Add("datetype", comboBox2.Text.Split('|')[0]);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Get_HolidayDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.DataSource = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataGridView1.SelectedCells[0].Value.ToString()== "delete")
            {
                try
                {
                    int index = dataGridView1.SelectedCells[0].RowIndex;
                    string org_id = dataGridView1.Rows[index].Cells["Column1"].Value.ToString();
                    string date = DateTime.Parse(dataGridView1.Rows[index].Cells["Column2"].Value.ToString()).ToShortDateString();
                    if (MessageBox.Show("Are you sure to delete？", "hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    Dictionary<string, object> parm = new Dictionary<string, object>();
                    parm.Add("org_id", org_id);
                    parm.Add("date", date);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Delete_HolidayDate", Program.client.UserToken, JsonConvert.SerializeObject(parm));

                    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                    if (Convert.ToBoolean(retJson["IsSuccess"]))
                    {
                        string json = retJson["RetData"].ToString();
                        bool dtJson = JsonConvert.DeserializeObject<bool>(json);

                        if (dtJson)
                        {
                            MessageHelper.ShowSuccess(this, "successfully deleted！");
                            dataGridView1.Rows.Remove(dataGridView1.Rows[index]);
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "failed to delete！");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }
    }
}
