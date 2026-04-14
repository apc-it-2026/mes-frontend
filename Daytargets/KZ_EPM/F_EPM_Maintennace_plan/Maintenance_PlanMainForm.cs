using AutocompleteMenuNS;
using F_EPM_Maintennace_Plan.bean;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace F_EPM_Maintennace_Plan
{
    public partial class Maintenance_PlanMainForm : MaterialForm
    {

        AutoCompleteStringCollection autoComplete_factory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_device = new AutoCompleteStringCollection();

        private List<string> orgIds = new List<string>();
        private List<string> deviceNos = new List<string>();
        private List<string> finishDate = new List<string>();
        private List<string> frequencyInfo = new List<string>();

        public Maintenance_PlanMainForm()
        {
            InitializeComponent();
             //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryMaintenancePlanList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Maintenance_PlanMainForm_Load(object sender, EventArgs e)
        {
            LoadOrg();
            LoadDeviceInfo();
        }

        private void LoadOrg()
        {
            orgIds.Clear();

            //工厂
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                dtJson.Rows.Add("All", "");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString());
                    orgIds.Add(dtJson.Rows[i]["org_code"].ToString() + "|" + dtJson.Rows[i]["org_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_name"].ToString()));
                }
                comboBox1.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                autoComplete_factory.AddRange(stringList.ToArray());
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadDeviceInfo()
        {
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", FindOrgCode());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "GetDeviceInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
                }
                comboBox2.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                autoComplete_device.AddRange(stringList.ToArray());
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox2.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void QueryMaintenancePlanList()
        {
            deviceNos.Clear();
            finishDate.Clear();
            frequencyInfo.Clear();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", FindOrgCode());
            p.Add("deviceName", comboBox2.Text== "All" ? "":comboBox2.Text);
            p.Add("period", comboBox3.SelectedIndex);
            p.Add("startDate",checkBox1.Checked? Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd"):"");
            p.Add("endDate", checkBox2.Checked?Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy/MM/dd"):"");
            int status = comboBox4.SelectedIndex;
            if (status == 0)
            {
                p.Add("delete", "");
            }
            else if (status == 1)
            {
                p.Add("delete", "N");
            }
            else { 
                p.Add("delete", "Y");
            }

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryMaintenancePlanList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No Data！");
                }
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(1);

                    dataGridView1.Rows[i].Cells["factory_name"].Value = dtJson.Rows[i]["org_name"].ToString();
                    dataGridView1.Rows[i].Cells["device_name"].Value = dtJson.Rows[i]["device_name"].ToString();
                    dataGridView1.Rows[i].Cells["plan_no"].Value = dtJson.Rows[i]["plan_id"].ToString();
                    dataGridView1.Rows[i].Cells["plan_name"].Value = dtJson.Rows[i]["plan_name"].ToString();
                    dataGridView1.Rows[i].Cells["start_date"].Value = Convert.ToDateTime(dtJson.Rows[i]["plan_begindate"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["end_date"].Value = Convert.ToDateTime(dtJson.Rows[i]["plan_enddate"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["plan_period"].Value = getPeriod(dtJson.Rows[i]["period"].ToString());
                    dataGridView1.Rows[i].Cells["plan_interval"].Value = dtJson.Rows[i]["interval"].ToString();
                    dataGridView1.Rows[i].Cells["plan_frequency"].Value = dtJson.Rows[i]["frequency"].ToString();
                    dataGridView1.Rows[i].Cells["plan_status"].Value = dtJson.Rows[i]["is_delete"].ToString()=="N"? "Using" :"Deleted";//使用中":"已删除

                    deviceNos.Add(dtJson.Rows[i]["device_no"].ToString());
                    finishDate.Add(dtJson.Rows[i]["plan_enddate"].ToString());
                    frequencyInfo.Add(dtJson.Rows[i]["frequency_info"].ToString());
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
    }

        private string FindOrgCode()
        {
            string result = comboBox1.Text;
            if (result == "All") {
                return "";
            }
            for (int i = 0; i < orgIds.Count; i++) {
                string[] orgs = orgIds[i].Split('|');
                if (orgs[1] == result) {
                    return orgs[0];
                }
            }
            return "";
        }

        private string FindOrgCode(string orgName)
        {
            for (int i = 0; i < orgIds.Count; i++)
            {
                string[] orgs = orgIds[i].Split('|');
                if (orgs[1] == orgName)
                {
                    return orgs[0];
                }
            }
            return "";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isShow = false;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    DataGridViewRow rowCollection = dataGridView1.Rows[i];
                    isShow = true;

                    CheckPlanDetailForm c=new CheckPlanDetailForm();
                    c.updateWholeOrderListener += new UpdateWholeOrderListener(updateWholeOrderListener);
                    c.info = wrapPlanInfo(rowCollection,i);
                    c.ShowDialog();
                    return;
                }
            }
            if (!isShow) {
                MessageBox.Show("Please select the Maintenance plan you want to view！");
            }
        }

        public void updateWholeOrderListener() {
            QueryMaintenancePlanList();
        }

        private PlanInfo wrapPlanInfo(DataGridViewRow rowCollection,int index) {
            PlanInfo planInfo = new PlanInfo();
            planInfo.OrgName = rowCollection.Cells[1].Value.ToString();
            planInfo.OrgId = FindOrgCode(planInfo.OrgName);
            planInfo.DeviceNo = deviceNos[index];
            planInfo.DeviceName = rowCollection.Cells[2].Value.ToString();
            planInfo.PlanNo = rowCollection.Cells[3].Value.ToString();
            planInfo.PlanName = rowCollection.Cells[4].Value.ToString();
            planInfo.StartDate = rowCollection.Cells[5].Value.ToString();
            planInfo.EndDate = rowCollection.Cells[6].Value.ToString();
            planInfo.FinishDate = planInfo.EndDate;
            planInfo.Period = rowCollection.Cells[7].Value.ToString();
            planInfo.Interval = rowCollection.Cells[8].Value.ToString();
            planInfo.Frequency = rowCollection.Cells[9].Value.ToString();
            planInfo.FrequencyInfo = frequencyInfo[index];
            planInfo.Status = rowCollection.Cells[10].Value.ToString();
            return planInfo;
        }

        public static string getPeriod(string s) {
            if (s == "1") {
                return "Day";
            }
            if (s == "2")
            {
                return "week";
            }
            if (s == "3")
            {
                return "Month";
            }
            if (s == "4")
            {
                return "Year";
            }
            return "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateMaintenanceWholeOrderState();
        }

        private void UpdateMaintenanceWholeOrderState()
        {
            bool isPost = false;
            List<string> planIds = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                string planId = dataGridView1.Rows[i].Cells["plan_no"].Value.ToString();
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    isPost = true;
                    planIds.Add(planId);
                }
            }
            if (!isPost) {
                MessageBox.Show("Please select the maintenance plan to be deleted");
                return;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("planIds", JArray.Parse(JsonConvert.SerializeObject(planIds)));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "UpdateMaintenanceWholeOrderState", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("Successfully deleted");
                QueryMaintenancePlanList();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
           AddPlanDetailForm form =  new AddPlanDetailForm();
            form.ShowDialog();
        }
    }

}
