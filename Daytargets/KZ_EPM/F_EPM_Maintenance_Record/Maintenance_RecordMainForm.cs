using AutocompleteMenuNS;
using F_EPM_Maintenance_Record;
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

namespace F_EPM_Maintenance_Record
{
    public partial class Maintenance_RecordMainForm : MaterialForm
    {

        AutoCompleteStringCollection autoComplete_factory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_device = new AutoCompleteStringCollection();

        private List<String> postionInfo = new List<string>();
        private List<String> deviceInfo = new List<string>();

        public Maintenance_RecordMainForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Equipment_Maintenance_Record_Form_Load(object sender, EventArgs e)
        {
            LoadDept();
            LoadOrg();
            DefaultComBoxSelect();
        }

        private void DefaultComBoxSelect() {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
        }

        private void LoadDept()
        {
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                items3.Add(new MulticolumnAutocompleteItem(new[] { "" }, "All"));
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtdept = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtdept != null && dtdept.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdept.Rows.Count; i++)
                    {
                        items3.Add(new MulticolumnAutocompleteItem(new[] { dtdept.Rows[i]["department_code"].ToString() }, dtdept.Rows[i]["department_code"].ToString()));
                    }
                }
                
                comboBox3.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadOrg() {
            //工厂
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "","" }, "All"));
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString() + "|" + dtJson.Rows[i]["org_code"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_name"].ToString() + "|" + dtJson.Rows[i]["org_code"].ToString()));
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

        private void button1_Click(object sender, EventArgs e)
        {
            QueryMaintenanceRecord();
        }

        private void QueryMaintenanceRecord() {
            string factoryName = comboBox1.Text == "All" ? "" : comboBox1.Text.Split('|')[0].Trim();
            string deviceName = comboBox2.Text == "All" ? "" : comboBox2.Text;
            string departmentName = comboBox3.Text == "All" ? "" : comboBox3.Text;
            string maintainStatusText = comboBox4.Text;
            string filterDateType = comboBox5.Text;
            string startDate = Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy/MM/dd");
            string endDate = Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd");
            string level = comboBox7.Text == "All" ? "" : comboBox7.Text;
            string checkResult = comboBox8.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p["factory_name"] = factoryName;
            p["device_name"] = deviceName;
            p["department_name"] = departmentName;
            if (maintainStatusText == "All")
            {
                p["maintain_status"] = -1;
            }
            else if (maintainStatusText == "Unmaintained")
            {
                p["maintain_status"] = 0;
            }
            else { //已保养
                p["maintain_status"] = 1;
            }
            //计划保养日期
            // 实际保养日期
            // 稽查日期
            if (filterDateType == "Planned_Maintenance_Date") {
                p["filter_date_type"] = 0;
            } else if (filterDateType == "Actual_Maintenance_Date") {
                p["filter_date_type"] = 1;
            } else if (filterDateType == "Audit_Date") {
                p["filter_date_type"] = 2;
            }
            p["start_date"] = startDate;
            p["end_date"] = endDate;
            p["level"] = level;
            if (checkResult == "All")
            {
                p["check_result"] = -1;
            }
            else if (checkResult == "Not_Audited")
            {
                p["check_result"] = 0;
            }
            else if (checkResult == "OK") {
                p["check_result"] = 1;
            }else if(checkResult == "NG")
            { 
                p["check_result"] = 2;
            }

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "QueryMaintenanceRecord", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                //dtJson.Rows.Add("全部","");
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count ==0) { 
                      MessageBox.Show("No Data！");
                }
                for (int i = 0; i < dtJson.Rows.Count; i++) {
                    dataGridView1.Rows.Add(1);

                    dataGridView1.Rows[i].Cells["by_status"].Value = dtJson.Rows[i]["by_status"].ToString()=="0"? "Unmaintained" : "Maintained";
                    int jcResult = Convert.ToInt32(dtJson.Rows[i]["jc_result"]);

                    if (jcResult == 0) {
                        dataGridView1.Rows[i].Cells["jc_result"].Value = "Not_Audited";
                    } else if (jcResult == 1) {
                        dataGridView1.Rows[i].Cells["jc_result"].Value = "OK";
                        dataGridView1.Rows[i].Cells["jc_result"].Style.ForeColor = Color.Green;
                        dataGridView1.Rows[i].Cells["by_status"].Style.ForeColor = Color.Green;
                        dataGridView1.Rows[i].Cells["jc_description"].Style.ForeColor = Color.Green;
                    }
                    else if (jcResult == 2) { 
                        dataGridView1.Rows[i].Cells["jc_result"].Value = "NG";
                        dataGridView1.Rows[i].Cells["jc_result"].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[i].Cells["by_status"].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[i].Cells["jc_description"].Style.ForeColor = Color.Red;
                    }
                    dataGridView1.Rows[i].Cells["jc_description"].Value = dtJson.Rows[i]["jc_description"].ToString();
                    dataGridView1.Rows[i].Cells["org_name"].Value = dtJson.Rows[i]["org_name"].ToString();
                    dataGridView1.Rows[i].Cells["department_name"].Value = dtJson.Rows[i]["address_name"].ToString();
                    dataGridView1.Rows[i].Cells["device_name"].Value = dtJson.Rows[i]["device_name"].ToString();
                    dataGridView1.Rows[i].Cells["snid"].Value = dtJson.Rows[i]["snid"].ToString();
                    dataGridView1.Rows[i].Cells["type"].Value = dtJson.Rows[i]["type"].ToString();
                    dataGridView1.Rows[i].Cells["body_part"].Value = dtJson.Rows[i]["body_part"].ToString();
                    dataGridView1.Rows[i].Cells["item"].Value = dtJson.Rows[i]["item"].ToString();
                    dataGridView1.Rows[i].Cells["level_name"].Value = dtJson.Rows[i]["level_name"].ToString();
                    dataGridView1.Rows[i].Cells["frequency"].Value = dtJson.Rows[i]["frequency"].ToString();
                    dataGridView1.Rows[i].Cells["plan_finishdate"].Value = Convert.ToDateTime(dtJson.Rows[i]["plan_finishdate"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["by_date"].Value = dtJson.Rows[i]["by_date"].ToString();
                    dataGridView1.Rows[i].Cells["by_user"].Value = dtJson.Rows[i]["by_user"].ToString();
                    dataGridView1.Rows[i].Cells["jc_date"].Value = dtJson.Rows[i]["jc_date"].ToString();
                    dataGridView1.Rows[i].Cells["jc_user"].Value = dtJson.Rows[i]["jc_user"].ToString();
                    dataGridView1.Rows[i].Cells["plan_begindate"].Value = Convert.ToDateTime(dtJson.Rows[i]["plan_begindate"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["plan_enddate"].Value = Convert.ToDateTime(dtJson.Rows[i]["plan_enddate"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["plan_id"].Value = dtJson.Rows[i]["plan_id"].ToString();
                    dataGridView1.Rows[i].Cells["plan_name"].Value = dtJson.Rows[i]["plan_name"].ToString();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
            GetPositionInfoByOrgId();
        }

        private void LoadDeviceInfo() {
            deviceInfo.Clear();

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
                    deviceInfo.Add(dtJson.Rows[i]["device_no"].ToString() + "|" + dtJson.Rows[i]["device_name"].ToString());
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString() ));
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

        private void GetPositionInfoByOrgId()
        {
            postionInfo.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
           
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", FindOrgCode());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "GetPositionInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));


                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    postionInfo.Add(dtJson.Rows[i]["address_code"].ToString() + "|" + dtJson.Rows[i]["address_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));
                }
                comboBox3.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        /*工厂下拉框选择后获取org_code*/
        private string FindOrgCode()
        {
            string text = comboBox1.Text;
            if (text.Equals("All"))
            {
                return "";
            }
            string orgId = text.Split('|')[1];
            return orgId;
        }

        private string GetCodeFromList(List<string> list,string text)
        {
            if (text.Equals("All"))
            {
                return "";
            }
            for (int i = 0; i < list.Count; i++)
            {
                string data = list[i];
                string[] st = data.Split('|');
                if (st[1].Equals(text))
                {
                    return st[0];
                }
            }
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean isExcute = false;
            DataTable dt = new DataTable();
            for (int i =1;i<dataGridView1.Columns.Count;i++) {
                dt.Columns.Add(dataGridView1.Columns[i].HeaderText.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++) {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["column_cb"];
                if (o!=null && o.Value!=null && bool.Parse(o.Value.ToString())) {
                    isExcute = true;
                    DataRow dr = dt.NewRow();
                    for (int j=1;j< dataGridView1.Rows[i].Cells.Count;j++) {
                        dr[j-1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (isExcute) { 
               NewExportExcels.ExportExcels.Export("Maintenance History (details)", dt);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["column_cb"].Value = true;
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["column_cb"].Value = false;
            }
        }
    }
}
