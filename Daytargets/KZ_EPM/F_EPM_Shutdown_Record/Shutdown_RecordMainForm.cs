using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Shutdown_Record
{
    public partial class Shutdown_RecordMainForm : MaterialForm
    {
        private List<string> orgCode = new List<string>();
        private List<string> orgInfo = new List<string>();
        private List<string> storey = new List<string>();
        private List<string> addressCodes = new List<string>();
        private List<string> deivceNos = new List<string>();
        private Dictionary<string, int> deviceCount = new Dictionary<string, int>();
        private Dictionary<string, double> deviceRepairTime = new Dictionary<string, double>();
        int size = 0;

        public Shutdown_RecordMainForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Shutdown_RecordMainForm_Load(object sender, EventArgs e)
        {
            LoadOrg();
            LoadDeviceInfo();
            GetSjqdmsOrgInfo();
        }

        private void LoadOrg()
        {
            orgCode.Clear();
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

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");
                orgCode.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orgCode.Add(dtJson.Rows[i]["org_code"].ToString());
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString());
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

        private void LoadDeviceInfo()
        {
            deivceNos.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", orgCode[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "GetDeviceInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                stringList.Add("All");
                deivceNos.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
                    deivceNos.Add(dtJson.Rows[i]["device_no"].ToString());
                }
                comboBox4.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox4.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void GetSjqdmsOrgInfo()
        {
            orgInfo.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetSjqdmsOrgInfo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                orgInfo.Add("");
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orgInfo.Add(dtJson.Rows[i]["code"].ToString());
                    stringList.Add(dtJson.Rows[i]["org"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code"].ToString(), dtJson.Rows[i]["org"].ToString() }, dtJson.Rows[i]["org"].ToString()));
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
            storey.Clear();
            addressCodes.Clear();

            List<AutocompleteItem> autoAddress = new List<AutocompleteItem>();
            List<string> addressStr = new List<string>();
            AutoCompleteStringCollection autoCompleteAddress = new AutoCompleteStringCollection();

            List<AutocompleteItem> autoStorey = new List<AutocompleteItem>();
            List<string> storeyStr = new List<string>();
            AutoCompleteStringCollection autoComplete1Storey = new AutoCompleteStringCollection();
            List<string> distinct = new List<string>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                storey.Add("");
                addressCodes.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {

                    string storeyCode = dtJson.Rows[i]["storey_code"].ToString();
                    string storeyName = dtJson.Rows[i]["storey_name"].ToString();

                    if (!distinct.Contains(storeyCode + "|" + storeyName))
                    {
                        storey.Add(storeyCode);
                        storeyStr.Add(storeyName);
                        autoStorey.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["storey_code"].ToString(), dtJson.Rows[i]["storey_name"].ToString() }, dtJson.Rows[i]["storey_name"].ToString() + "|" + dtJson.Rows[i]["storey_code"].ToString()));
                        distinct.Add(storeyCode + "|" + storeyName);
                    }

                    string addressCode = dtJson.Rows[i]["address_code"].ToString();
                    string addressName = dtJson.Rows[i]["address_name"].ToString();
                    addressCodes.Add(addressCode);
                    addressStr.Add(addressName);
                    autoAddress.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));

                }

                comboBox3.DataSource = autoStorey;
                autoComplete1Storey.AddRange(storeyStr.ToArray());
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox3.AutoCompleteCustomSource = autoComplete1Storey;

                comboBox7.DataSource = autoAddress;
                autoCompleteAddress.AddRange(addressStr.ToArray());
                comboBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox7.AutoCompleteCustomSource = autoCompleteAddress;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }




        private void button1_Click(object sender, EventArgs e)
        {
            QueryShutDownRecordList();
        }

        private void QueryShutDownRecordList()
        {
            deviceCount.Clear();
            deviceRepairTime.Clear();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code",orgCode[comboBox1.SelectedIndex]);
            p.Add("device_no", deivceNos[comboBox4.SelectedIndex]);
            p.Add("storey", storey[comboBox3.SelectedIndex]);
            p.Add("address", addressCodes[comboBox7.SelectedIndex]);
            p.Add("region", orgInfo[comboBox2.SelectedIndex]);
            p.Add("start", Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy/MM/dd"));
            p.Add("end", Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd"));

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryShutDownRecordList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);

                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count==0)
                {
                    MessageBox.Show("No Data！");
                    return;
                }
                int i = 0;
                double sumMttr = 0.0F;
                Dictionary<string, JObject> data = mergeData(dtJson);
                size = data.Count;
                foreach (string key in data.Keys)
                {
                    dataGridView1.Rows.Add(1);

                    JObject o = data[key];

                    string snid = o.GetValue("snid").ToString();

                    dataGridView1.Rows[i].Cells["bz_date"].Value = o.GetValue("bz_date").ToString();
                    dataGridView1.Rows[i].Cells["shutDownStartDate"].Value = o.GetValue("bz_date").ToString();
                    dataGridView1.Rows[i].Cells["address"].Value = o.GetValue("address").ToString();
                    dataGridView1.Rows[i].Cells["deviceName"].Value = o.GetValue("device_name").ToString();
                    dataGridView1.Rows[i].Cells["pjName"].Value = o.GetValue("pj_name").ToString();
                    dataGridView1.Rows[i].Cells["snid"].Value = snid;
                    dataGridView1.Rows[i].Cells["gzName"].Value = o.GetValue("gz_name").ToString();
                    dataGridView1.Rows[i].Cells["diagnosisName"].Value = o.GetValue("diagnosis_name").ToString();
                    if (o.ContainsKey("pick_start_time"))
                    {
                        dataGridView1.Rows[i].Cells["pickStartDate"].Value = o.GetValue("pick_start_time").ToString();
                    }
                    if (o.ContainsKey("pick_total_date"))
                    {
                        dataGridView1.Rows[i].Cells["pickTotalDate"].Value = o.GetValue("pick_total_date").ToString();
                        dataGridView1.Rows[i].Cells["pickEndDate"].Value = o.GetValue("pick_end_date").ToString();
                    }

                    dataGridView1.Rows[i].Cells["step"].Value = o.GetValue("repair_name").ToString();
                    dataGridView1.Rows[i].Cells["count"].Value = o.GetValue("qty").ToString();

                    if (o.ContainsKey("mt_end_date"))
                    {
                        double repairTime = deviceRepairTime[snid];

                        dataGridView1.Rows[i].Cells["mtEndDate"].Value = o.GetValue("mt_end_date").ToString();
                        dataGridView1.Rows[i].Cells["mtTotalDate"].Value = o.GetValue("mt_total_date").ToString();
                        dataGridView1.Rows[i].Cells["stopDate"].Value = o.GetValue("stop_date").ToString();
                        dataGridView1.Rows[i].Cells["mttr"].Value = Math.Round(repairTime / deviceCount[snid], 3);

                        sumMttr += Math.Round(repairTime / deviceCount[snid], 3);
                    }
                    i++;
                }
                DataRow TotaldgvRow1 = dtJson.NewRow();
                dtJson.Rows.Add(TotaldgvRow1);
                dataGridView1.Rows.Add(1);
                dataGridView1.Rows[i].Cells["stopDate"].Value = "Average：";
                dataGridView1.Rows[i].Cells["mttr"].Value = Math.Round(sumMttr / size, 3);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private Dictionary<string, JObject> mergeData(DataTable dtJson) {
            Dictionary<string, JObject> data = new Dictionary<string, JObject>();
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                JObject o;

                string workOrder = dtJson.Rows[i]["work_order"].ToString();
                string pickStartTime = dtJson.Rows[i]["pick_start_time"].ToString();
                string pickEndTime = dtJson.Rows[i]["last_date"].ToString();
                string pjName = dtJson.Rows[i]["pj_name"].ToString();

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
                            if (!date1.Contains(date2)) {
                                temp = o.GetValue("pick_start_time").ToString() + "," + pickStartDate.ToString("yyyy/MM/dd HH:mm:ss");
                                o.Remove("pick_start_time");
                                o.Add("pick_start_time", temp);
                            }
                        }
                        else {
                            temp = pickStartDate.ToString("yyyy/MM/dd HH:mm:ss");
                            o.Add("pick_start_time", temp);
                        }
                    }

                    if (pickEndTime.Length != 0)
                    {
                        DateTime pickEndDate = Convert.ToDateTime(pickEndTime);

                        if (o.ContainsKey("pick_end_date"))
                        {
                            string date1 = o.GetValue("pick_end_date").ToString();
                            string date2 = pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                            if (!date1.Contains(date2))
                            {
                                temp = o.GetValue("pick_end_date").ToString() + "," + pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                                o.Remove("pick_end_date");
                                o.Add("pick_end_date", temp);
                            }
                        }
                        else
                        {
                            temp = pickEndDate.ToString("yyyy/MM/dd HH:mm:ss");
                            o.Add("pick_end_date", temp);
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
                else {
                    o = new JObject();
                    data.Add(workOrder, o);

                    string pickTime = dtJson.Rows[i]["picking_time"].ToString();
                    string endDate = dtJson.Rows[i]["END_DATE"].ToString();
                    string bzDate = dtJson.Rows[i]["bz_date"].ToString();
                    string snid = dtJson.Rows[i]["snid"].ToString();
                    DateTime pickStartDate = DateTime.Now;
                    double pickTotalDate = 0;

                    o.Add("bz_date", Convert.ToDateTime(bzDate).ToString("yyyy/MM/dd HH:mm:ss"));
                    o.Add("address", dtJson.Rows[i]["address"].ToString());
                    o.Add("device_name", dtJson.Rows[i]["device_name"].ToString());
                    o.Add("pj_name", pjName);
                    o.Add("snid", dtJson.Rows[i]["snid"].ToString());
                    o.Add("gz_name", dtJson.Rows[i]["gz_name"].ToString());
                    o.Add("diagnosis_name", dtJson.Rows[i]["diagnosis_name"].ToString());

                    if (deviceCount.ContainsKey(snid)) {
                        deviceCount[snid] += 1;
                    }
                    else {
                        deviceRepairTime.Add(snid,0.0F);
                        deviceCount.Add(snid,1);
                    }
                    if (pickStartTime.Length != 0)
                    {
                        pickStartDate = Convert.ToDateTime(pickStartTime);
                        o.Add("pick_start_time", pickStartDate.ToString("yyyy/MM/dd HH:mm:ss"));
                    }
            
                    if (pickTime.Length != 0 && double.Parse(pickTime) != 0)
                    {
                        pickTotalDate = double.Parse(pickTime) * 60;

                        o.Add("pick_total_date", string.Format("{0}:{1}", ((int)pickTotalDate / 60).ToString("00"), ((int)pickTotalDate % 60).ToString("00")));
                        o.Add("pick_end_date", Convert.ToDateTime(pickEndTime).ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                    o.Add("repair_name", dtJson.Rows[i]["repair_name"].ToString());
                    o.Add("qty", dtJson.Rows[i]["qty"].ToString());

                    if (endDate.Length > 0)
                    {
                        double waitTime = dtJson.Rows[i]["WAIT_TIME"].ToString().Length == 0 ? 0 : Double.Parse(dtJson.Rows[i]["WAIT_TIME"].ToString());
                        double repairTime = double.Parse(dtJson.Rows[i]["repair_time"].ToString())- waitTime- double.Parse(pickTime);
                        double repairTimeUnitminute = repairTime * 60;
                        //dataGridView1.Rows[i].Cells["mtTotalDate"].Value = string.Format("{0}:{1}", ((int)repairTimeUnitminute / 60).ToString("00"), ((int)repairTimeUnitminute % 60).ToString("00"));
                        double totalMinutes = (DateTime.Parse(endDate) - Convert.ToDateTime(bzDate)).TotalMinutes;
                        //dataGridView1.Rows[i].Cells["stopDate"].Value = string.Format("{0}:{1}", ((int)(totalMinutes / 60)).ToString("00"), ((int)(totalMinutes % 60)).ToString("00"));
                        //dataGridView1.Rows[i].Cells["mttr"].Value = Math.Round(repairTime / count, 3);
                        deviceRepairTime[snid] += repairTime;
                        o.Add("repair_time", repairTime);
                        //o.Add("mt_end_date", pickStartDate.AddMinutes(pickTotalDate+repairTimeUnitminute).ToString("yyyy/MM/dd HH:mm:ss"));
                        o.Add("mt_end_date", Convert.ToDateTime(endDate).ToString("yyyy/MM/dd HH:mm:ss"));
                        o.Add("mt_total_date", string.Format("{0}:{1}", ((int)repairTimeUnitminute / 60).ToString("00"), ((int)(repairTimeUnitminute % 60)).ToString("00")));
                        o.Add("stop_date", string.Format("{0}:{1}", ((int)(totalMinutes / 60)).ToString("00"), ((int)(totalMinutes % 60)).ToString("00")));
                    }
               
                 }
       

            }

            return data;
        } 


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
                NewExportExcels.ExportExcels.Export("Equipment_Shutdown_Maintenance_Records(Details)", dt);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAddressInfoByOrgId();
            LoadDeviceInfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i =0;i<dataGridView1.Rows.Count;i++) {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = false;
            }
        }
    }
}
