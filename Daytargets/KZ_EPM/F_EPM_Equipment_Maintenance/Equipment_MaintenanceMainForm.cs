using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace F_EPM_Equipment_Maintenance
{
    public partial class Equipment_MaintenanceMainForm : MaterialForm
    {
        private List<string> orgIds = new List<string>();
        private List<string> deviceNos = new List<string>();
        private List<string> orderStatus = new List<string>();
        private List<string> deviceStatus = new List<string>();
        private List<string> addressCodes = new List<string>();

        public Equipment_MaintenanceMainForm()
        {
            InitializeComponent();
            comboBox5.SelectedIndex = 0;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Equipment_MaintenanceMainForm_Load(object sender, EventArgs e)
        {
            LoadOrg();
            GetOrderState();
            GetDeviceState();
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
                orgIds.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString());
                    orgIds.Add(dtJson.Rows[i]["org_code"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_name"].ToString()));
                }
                comboBox1.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetOrderState()
        {
            orderStatus.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("rule_no", "1012");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getalldevice_status", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                orderStatus.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orderStatus.Add(dtJson.Rows[i]["code_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code_no"].ToString(), dtJson.Rows[i]["code_name"].ToString() }, dtJson.Rows[i]["code_name"].ToString()));
                }
                comboBox4.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetDeviceState()
        {
            deviceStatus.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("rule_no", "1013");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getalldevice_status", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
               
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                deviceStatus.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceStatus.Add(dtJson.Rows[i]["code_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code_no"].ToString(), dtJson.Rows[i]["code_name"].ToString() }, dtJson.Rows[i]["code_name"].ToString()));
                }
                comboBox3.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
            GetAddressInfoByOrgId();
        }

        private void LoadDeviceInfo()
        {
            deviceNos.Clear();
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", orgIds[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "GetDeviceInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");
                deviceNos.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceNos.Add(dtJson.Rows[i]["device_no"].ToString());
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
                }
                comboBox2.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox2.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetAddressInfoByOrgId()
        {
            addressCodes.Clear();

            List<AutocompleteItem> autoAddress = new List<AutocompleteItem>();
            List<string> addressStr = new List<string>();
            AutoCompleteStringCollection autoCompleteAddress = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgIds[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
              // DataTable Bts = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                DataTable dtJson = (DataTable)JsonConvert.DeserializeObject(Data, (typeof(DataTable)));
                //dtJson.Rows.Add("全部","");

                autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                addressCodes.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    addressCodes.Add(dtJson.Rows[i]["address_code"].ToString());
                    addressStr.Add(dtJson.Rows[i]["address_name"].ToString());
                    autoAddress.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));
            
                }

                comboBox6.DataSource = autoAddress;
                autoCompleteAddress.AddRange(addressStr.ToArray());
                comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox6.AutoCompleteCustomSource = autoCompleteAddress;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["cl_cb"].Value = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean isExcute = false;
            DataTable dt = new DataTable();
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dt.Columns.Add(dataGridView1.Columns[i].HeaderText.ToString());
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    isExcute = true;
                    DataRow dr = dt.NewRow();
                    for (int j = 1; j < dataGridView1.Rows[i].Cells.Count; j++)
                    {
                        DataGridViewCell d = dataGridView1.Rows[i].Cells[j];
                        if (d == null || d.Value == null)
                        {
                            dr[j - 1] = "";
                        }
                        else
                        {
                            dr[j - 1] = d.Value.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (isExcute)
            {
                NewExportExcels.ExportExcels.Export("Equipment Maintenance History(Details)", dt);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryMaintenanceRecordList();
        }

        private void QueryMaintenanceRecordList()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgIds[comboBox1.SelectedIndex]);
            p.Add("device_no", deviceNos[comboBox2.SelectedIndex]);
            p.Add("order_status", comboBox4.SelectedIndex == 0 ? "" : comboBox4.SelectedItem.ToString());
            p.Add("device_status", deviceStatus[comboBox3.SelectedIndex]);
            p.Add("address", addressCodes[comboBox6.SelectedIndex]);
            p.Add("type", comboBox5.SelectedIndex);
            p.Add("start", Convert.ToDateTime(dateTimePicker1.Value).ToString("yyyy/MM/dd"));
            p.Add("end", Convert.ToDateTime(dateTimePicker2.Value).ToString("yyyy/MM/dd"));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryMaintenanceRecordList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);

                //header
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No Data！");
                    return;
                }

                Dictionary<string, JObject> data = mergeData(dtJson);
                int i = 0;
                foreach (string key in data.Keys)
                {
                    dataGridView1.Rows.Add(1);

                    JObject o = data[key];
                    string statusName = o.GetValue("status_name").ToString();
                    dataGridView1.Rows[i].Cells["order_state"].Value = statusName;
                    if (statusName == "To_be_Repaired")
                    {
                        dataGridView1.Rows[i].Cells["order_state"].Style.ForeColor = Color.Red;
                    }//1待维修//2维修中//3领料中//4待厂商支援//5厂商支援中//6维修完成
                    if (statusName == "In_Use" || statusName == "Material_Fetching" || statusName == "Waiting_for_supplier_Support"
                        || statusName == "Under_supplier_Support")
                    {
                        dataGridView1.Rows[i].Cells["order_state"].Style.ForeColor = Color.Yellow;
                    }
                    if (statusName == "Repaired")
                    {
                        dataGridView1.Rows[i].Cells["order_state"].Style.ForeColor = Color.Green;
                    }
                    dataGridView1.Rows[i].Cells["order_no"].Value = o.GetValue("work_order").ToString();
                    dataGridView1.Rows[i].Cells["bz_date"].Value = o.GetValue("bz_date").ToString();
                    dataGridView1.Rows[i].Cells["bz_context"].Value = o.GetValue("bz_context").ToString();
                    dataGridView1.Rows[i].Cells["org_name"].Value = o.GetValue("org_name").ToString();
                    dataGridView1.Rows[i].Cells["address"].Value = o.GetValue("address").ToString();
                    dataGridView1.Rows[i].Cells["device_name"].Value = o.GetValue("device_name").ToString();
                    dataGridView1.Rows[i].Cells["device_state"].Value = o.GetValue("device_state").ToString();
                    dataGridView1.Rows[i].Cells["snid"].Value = o.GetValue("snid").ToString();
                    dataGridView1.Rows[i].Cells["gz_name"].Value = o.GetValue("gz_name").ToString();
                    dataGridView1.Rows[i].Cells["diagnosis_name"].Value = o.GetValue("diagnosis_name").ToString();
                    dataGridView1.Rows[i].Cells["repair_name"].Value = o.GetValue("repair_name").ToString();
                    //dataGridView1.Rows[i].Cells["repair_proj"].Value = dtJson.Rows[i]["pj_name"].ToString();
                    dataGridView1.Rows[i].Cells["pj_name"].Value = o.GetValue("pj_name").ToString();
                    dataGridView1.Rows[i].Cells["count"].Value = o.GetValue("qty").ToString();
                    dataGridView1.Rows[i].Cells["unit"].Value = o.GetValue("unit").ToString();
                    dataGridView1.Rows[i].Cells["reason"].Value = o.GetValue("pause_cause").ToString();
                    dataGridView1.Rows[i].Cells["repair_user"].Value = o.GetValue("analyse_user").ToString();
                    dataGridView1.Rows[i].Cells["receive_date"].Value = o.GetValue("begin_date").ToString();
                    if (o.ContainsKey("pick_start_time"))
                    {
                        dataGridView1.Rows[i].Cells["begin_date"].Value = o.GetValue("pick_start_time").ToString();
                    }
                    if (o.ContainsKey("last_date"))
                    {
                        dataGridView1.Rows[i].Cells["end_date"].Value = o.GetValue("last_date").ToString();
                    }
                    if (o.ContainsKey("repair_date"))
                    {
                        string endDate = o.GetValue("repair_date").ToString();
                        dataGridView1.Rows[i].Cells["repair_date"].Value = Convert.ToDateTime(endDate).ToString("yyyy/MM/dd HH:mm:ss");
                        dataGridView1.Rows[i].Cells["close_date"].Value = Convert.ToDateTime(endDate).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    i++;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private Dictionary<string, JObject> mergeData(DataTable dtJson)
        {
            Dictionary<string, JObject> data = new Dictionary<string, JObject>();
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                JObject o;

                string workOrder = dtJson.Rows[i]["work_order"].ToString();
                string pickStartTime = dtJson.Rows[i]["pick_start_time"].ToString();
                string pickEndTime = dtJson.Rows[i]["last_date"].ToString();
                string pjName = dtJson.Rows[i]["pj_name"].ToString();
                string statusName = dtJson.Rows[i]["status_name"].ToString();
              
                if (data.ContainsKey(workOrder))
                {
                    o = data[workOrder];
                    string temp = "";
                    if (pickStartTime.Length != 0)
                    {
                        DateTime pickStartDate = Convert.ToDateTime(pickStartTime);

                        if (o.ContainsKey("pick_start_time"))
                        {
                            string date1 = o.GetValue("pick_start_time").ToString();
                            string date2 = pickStartDate.ToString("yyyy/MM/dd HH:mm:ss");
                            if (!date1.Contains(date2))
                            {
                                temp = o.GetValue("pick_start_time").ToString() + "," + pickStartDate.ToString("yyyy/MM/dd HH:mm:ss");
                                o.Remove("pick_start_time");
                                o.Add("pick_start_time", temp);
                            }
                        }
                        else
                        {
                            temp = pickStartDate.ToString("yyyy/MM/dd HH:mm:ss");
                            o.Add("pick_start_time", temp);
                        }
                    }

                    if (pickEndTime.Length != 0)
                    {
                        DateTime pickEndDate = Convert.ToDateTime(pickEndTime);

                        if (o.ContainsKey("last_date"))
                        {
                            string date1 = o.GetValue("last_date").ToString();
                            string date2 = pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                            if (!date1.Contains(date2))
                            {
                                temp = o.GetValue("last_date").ToString() + "," + pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                                o.Remove("last_date");
                                o.Add("last_date", temp);
                            }
                        }
                        else
                        {
                            temp = pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                            o.Add("last_date", temp);
                        }
                    }

                    temp = o.GetValue("pj_name").ToString();
                    if (temp.Contains(pjName))
                    {
                        continue;
                    }
                    o.Remove("pj_name");
                    o.Add("pj_name", temp + "," + pjName);
                }
                else
                {
                    o = new JObject();
                    data.Add(workOrder, o);

                    string endDate = dtJson.Rows[i]["END_DATE"].ToString();
                    string bzDate = dtJson.Rows[i]["bz_date"].ToString();
                    string snid = dtJson.Rows[i]["snid"].ToString();
                    DateTime pickStartDate = DateTime.Now;

                    o.Add("work_order", dtJson.Rows[i]["work_order"].ToString());
                    o.Add("bz_date", Convert.ToDateTime(bzDate).ToString("yyyy/MM/dd HH:mm:ss"));
                    o.Add("status_name", statusName);
                    o.Add("bz_context", dtJson.Rows[i]["bz_remark"].ToString());
                    o.Add("org_name", dtJson.Rows[i]["org_name"].ToString());
                    o.Add("address", dtJson.Rows[i]["address"].ToString());
                    o.Add("device_name", dtJson.Rows[i]["device_name"].ToString());
                    o.Add("device_state", dtJson.Rows[i]["status"].ToString());
                    o.Add("pj_name", pjName);
                    o.Add("snid", snid);
                    o.Add("gz_name", dtJson.Rows[i]["gz_name"].ToString());
                    o.Add("diagnosis_name", dtJson.Rows[i]["diagnosis_name"].ToString());
                    o.Add("qty", dtJson.Rows[i]["qty"].ToString());
                    o.Add("unit", dtJson.Rows[i]["unit"].ToString());
                    o.Add("pause_cause", dtJson.Rows[i]["pause_cause"].ToString());
                    o.Add("analyse_user", dtJson.Rows[i]["analyse_user"].ToString());
                    o.Add("begin_date", dtJson.Rows[i]["begin_date"].ToString());
                    o.Add("repair_name", dtJson.Rows[i]["repair_name"].ToString());

                    if (pickStartTime.Length != 0)
                    {
                        pickStartDate = Convert.ToDateTime(pickStartTime);
                        o.Add("pick_start_time", pickStartDate.ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    if (pickEndTime.Length != 0 )
                    {
                        o.Add("last_date", Convert.ToDateTime(pickEndTime).ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                    if (endDate.Length > 0)
                    {
                        o.Add("repair_date", Convert.ToDateTime(endDate).ToString("yyyy/MM/dd HH:mm:ss"));
                        o.Add("close_date", Convert.ToDateTime(endDate).ToString("yyyy/MM/dd HH:mm:ss"));
                    }

                }
            }

            return data;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["cl_cb"].Value = false;
            }
        }
    }
}
