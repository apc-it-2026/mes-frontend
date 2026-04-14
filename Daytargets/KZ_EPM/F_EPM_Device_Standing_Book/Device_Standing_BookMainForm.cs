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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Device_Standing_Book
{
    public partial class Device_Standing_BookMainForm : MaterialForm
    {
        private List<string> orgCode = new List<string>();
        private List<string> listOrgCode = new List<string>();
        private List<string> deivceNos = new List<string>();
        private List<string> orgInfo = new List<string>();
        private List<string> storey = new List<string>();
        private List<string> addressCodes = new List<string>();
        private List<string> deviceState = new List<string>();
        private List<string> workShop = new List<string>();

        public Device_Standing_BookMainForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadOrg();
            GetSjqdmsOrgInfo();
            GetDeviceState();
            GetWorkshop();
        }
        private void LoadOrg()
        {
            orgCode.Clear();
            //factory
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                //stringList.Add("全部");
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

        private void GetSjqdmsOrgInfo() {
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

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                orgInfo.Add("");
                //stringList.Add("全部");
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    orgInfo.Add(dtJson.Rows[i]["code"].ToString());
                    stringList.Add(dtJson.Rows[i]["org"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code"].ToString(), dtJson.Rows[i]["org"].ToString() }, dtJson.Rows[i]["org"].ToString()));
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

        private void GetDeviceState()
        {
            deviceState.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("rule_no", "1013");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getalldevice_status", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                deviceState.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceState.Add(dtJson.Rows[i]["code_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code_no"].ToString(), dtJson.Rows[i]["code_name"].ToString() }, dtJson.Rows[i]["code_name"].ToString()));
                }
                comboBox5.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetWorkshop()
        {
            workShop.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetWorkshop", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                workShop.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    workShop.Add(dtJson.Rows[i]["ROUT_NO"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["ROUT_NO"].ToString(), dtJson.Rows[i]["ROUT_NAME_Z"].ToString() }, dtJson.Rows[i]["ROUT_NAME_Z"].ToString()));
                }
                comboBox8.DataSource = items3;
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
                dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0;i<dataGridView1.Rows.Count;i++) {
                dataGridView1.Rows[i].Cells[0].Value = false;
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
                    DataGridViewCell o = dataGridView1.Rows[i].Cells[0];
                    if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                    {
                        isExcute = true;
                        DataRow dr = dt.NewRow();
                        for (int j = 1; j < dataGridView1.Rows[i].Cells.Count; j++)
                        {
                            dr[j - 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (isExcute)
                {
                    //NewExportExcels.ExportExcels.Export("设备台账(明细)", dt);
                    NewExportExcels.ExportExcels.Export("Equipment Ledger(Details)", dt);
                }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            JObject jObject = null;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells[0];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    jObject = new JObject();
                    jObject.Add("orgName", row.Cells["orgName"].Value.ToString());
                    //jObject.Add("orgCode", row.Cells["orgName"].Value.ToString());
                    jObject.Add("status", row.Cells["status"].Value.ToString());
                    jObject.Add("name", row.Cells["name"].Value.ToString());
                    jObject.Add("snid", row.Cells["snid"].Value.ToString());
                    jObject.Add("type", row.Cells["type"].Value.ToString());
                    jObject.Add("frim", row.Cells["frim"].Value.ToString());
                    jObject.Add("date", row.Cells["date"].Value.ToString());
                    jObject.Add("address", row.Cells["address"].Value.ToString());
                    jObject.Add("orgCode", listOrgCode[i]);
                    break;
                }
            } 
            if (jObject == null) {
                return;
            }
            AdjustmentForm adjustment = new AdjustmentForm();
            adjustment.o = jObject;
            adjustment.listener += new OnUpdateListener(onUpdateListener);
            adjustment.ShowDialog();
        }

        private void onUpdateListener() {
            queryDeviceStandingBook();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAddressInfoByOrgId();
            LoadDeviceInfo();
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
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                //autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                //autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                storey.Add("");
                //storeyStr.Add("全部");
                storeyStr.Add("All");
                addressCodes.Add("");
                //addressStr.Add("全部");
                addressStr.Add("All");

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
                //autoComplete1Storey.AddRange(storeyStr.ToArray());
                //comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //comboBox3.AutoCompleteCustomSource = autoComplete1Storey;

                comboBox6.DataSource = autoAddress;
                //autoCompleteAddress.AddRange(addressStr.ToArray());
                //comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //comboBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //comboBox6.AutoCompleteCustomSource = autoCompleteAddress;
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

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                //stringList.Add("全部");
                stringList.Add("All");
                deivceNos.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
                    deivceNos.Add(dtJson.Rows[i]["device_no"].ToString());
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

        private void button1_Click(object sender, EventArgs e)
        {
            queryDeviceStandingBook();
        }

        private void queryDeviceStandingBook() {

            listOrgCode.Clear();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox1.SelectedIndex]);
            p.Add("device_no", deivceNos[comboBox2.SelectedIndex]);
            p.Add("code", orgInfo[comboBox4.SelectedIndex]);
            p.Add("storey_code", storey[comboBox3.SelectedIndex]);
            p.Add("snid", comboBox7.SelectedIndex==0?"":comboBox7.SelectedItem.ToString());
            p.Add("process_no", workShop[comboBox8.SelectedIndex]);
            p.Add("address_code", addressCodes[comboBox6.SelectedIndex]);
            p.Add("device_status", deviceState[comboBox5.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "queryDeviceStandingBook", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson =JsonConvert.DeserializeObject<DataTable>(Data);
                dataGridView1.Rows.Clear();
                int count = dtJson.Rows.Count;
                if (count ==0) {
                    MessageBox.Show("No data！");
                    return;
                }
                for (int i = 0;i<count;i++) {
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[i].Cells["orgName"].Value = dtJson.Rows[i]["org_name"].ToString();
                    dataGridView1.Rows[i].Cells["status"].Value = dtJson.Rows[i]["status"].ToString();
                    dataGridView1.Rows[i].Cells["name"].Value = dtJson.Rows[i]["device_name"].ToString();
                    dataGridView1.Rows[i].Cells["snid"].Value = dtJson.Rows[i]["MACHINE_NO"].ToString();
                    dataGridView1.Rows[i].Cells["type"].Value = dtJson.Rows[i]["type"].ToString();
                    dataGridView1.Rows[i].Cells["frim"].Value = dtJson.Rows[i]["brand"].ToString();
                    dataGridView1.Rows[i].Cells["address"].Value = dtJson.Rows[i]["address_name"].ToString();
                    dataGridView1.Rows[i].Cells["date"].Value = Convert.ToDateTime(dtJson.Rows[i]["UDF05"].ToString()).ToString("yyyy/MM/dd");

                    listOrgCode.Add(dtJson.Rows[i]["org_code"].ToString());
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySnidListByDeviceNo();
        }
        private void QuerySnidListByDeviceNo()
        {

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("device_no", deivceNos[comboBox2.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QuerySnidListByDeviceNo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    stringList.Add(dtJson.Rows[i]["machine_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["machine_no"].ToString(), dtJson.Rows[i]["machine_no"].ToString() }, dtJson.Rows[i]["machine_no"].ToString()));
                }
                comboBox7.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox7.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

    }
}
