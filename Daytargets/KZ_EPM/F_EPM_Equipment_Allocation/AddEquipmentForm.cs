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

namespace F_EPM_Equipment_Allocation
{

    public delegate void AddOrderCallback(List<MoveOrder> moveOrders);
    public partial class AddEquipmentForm : MaterialForm
    {

        public event AddOrderCallback onAddOrderCallback;
        public List<MoveOrder> moveOrders;

        private List<string> orgCode = new List<string>();
        private List<string> orgInfo = new List<string>();
        private List<string> deviceInfo = new List<string>();
        private List<string> address = new List<string>();
        private List<string> storey = new List<string>();
        private List<string> udf05 = new List<string>();
        private List<string> workShop = new List<string>();
        private List<string> deviceState = new List<string>();
        private List<string> addressCode = new List<string>();
        private List<string> listDeviceNos = new List<string>();

        public AddEquipmentForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }



          
        private void AddEquipmentForm_Load(object sender, EventArgs e)
        {
            LoadOrg();
            GetWorkshop();
            GetDeviceState();
            GetSjqdmsOrgInfo();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void LoadOrg()
        {
            orgCode.Clear();
            //工厂
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>( );
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
                comboBox3.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox3.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadDeviceInfo()
        {
            deviceInfo.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", orgCode[comboBox3.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceRecordService", "GetDeviceInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                //stringList.Add("全部");
                stringList.Add("All");
                deviceInfo.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceInfo.Add(dtJson.Rows[i]["device_no"].ToString());
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    stringList.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
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
                comboBox7.DataSource = items3;
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
                comboBox6.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox6.AutoCompleteCustomSource = autoComplete1;
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
                comboBox8.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void GetAddressInfoByOrgId()
        {
            address.Clear();
            storey.Clear();
            udf05.Clear();

            List<AutocompleteItem> autoAddress = new List<AutocompleteItem>();
            List<string> addressStr = new List<string>();
            AutoCompleteStringCollection autoCompleteAddress = new AutoCompleteStringCollection(); 
            
            List<AutocompleteItem> autoStorey = new List<AutocompleteItem>();
            List<string> storeyStr = new List<string>();
            AutoCompleteStringCollection autoComplete1Storey = new AutoCompleteStringCollection();
            List<string> distinct = new List<string>();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox3.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);

                //dtJson.Rows.Add("全部","");

                //autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                //autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                autoAddress.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                autoStorey.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                address.Add("");
                storey.Add("");
                udf05.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    address.Add(dtJson.Rows[i]["address_code"].ToString());
                    addressStr.Add(dtJson.Rows[i]["address_name"].ToString());
                    autoAddress.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));
                    udf05.Add(dtJson.Rows[i]["udf05"].ToString());

                    string storeyCode = dtJson.Rows[i]["storey_code"].ToString();
                    string storeyName = dtJson.Rows[i]["storey_name"].ToString();
                    if (!distinct.Contains(storeyCode + "|" + storeyName)) { 
                         storey.Add(storeyCode);
                         storeyStr.Add(storeyName);
                         autoStorey.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["storey_code"].ToString(), dtJson.Rows[i]["storey_name"].ToString() }, dtJson.Rows[i]["storey_name"].ToString() + "|" + dtJson.Rows[i]["storey_code"].ToString()));
                         distinct.Add(storeyCode+"|"+ storeyName);
                    }
                }

                comboBox5.DataSource = autoAddress;
                autoCompleteAddress.AddRange(addressStr.ToArray());
                comboBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox5.AutoCompleteCustomSource = autoCompleteAddress;

                comboBox4.DataSource = autoStorey;
                autoComplete1Storey.AddRange(storeyStr.ToArray());
                comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox4.AutoCompleteCustomSource = autoComplete1Storey;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
            GetAddressInfoByOrgId();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void Query() {
            addressCode.Clear();
            listDeviceNos.Clear();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode[comboBox3.SelectedIndex]);
            p.Add("device_no", deviceInfo[comboBox1.SelectedIndex]);
            p.Add("snid", comboBox2.Text=="All"?"":comboBox2.Text);
            p.Add("storey", storey[comboBox4.SelectedIndex]);
            p.Add("state", deviceState[comboBox8.SelectedIndex]);
            p.Add("work", workShop[comboBox7.SelectedIndex]);
            p.Add("code", orgInfo[comboBox6.SelectedIndex]);
            p.Add("address", address[comboBox5.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryBaseDeviceInfo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson =JsonConvert.DeserializeObject<DataTable>(Data);
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No data！");
                }
                else {
                    for (int i =0;i<dtJson.Rows.Count;i++) {
                        dataGridView1.Rows.Add(1);

                        string orgName = dtJson.Rows[i]["org_name"].ToString();
                        string deviceName = dtJson.Rows[i]["device_name"].ToString();
                        string snid = dtJson.Rows[i]["machine_no"].ToString();
                        string type = dtJson.Rows[i]["type"].ToString();
                        string address = dtJson.Rows[i]["address"].ToString();
                        dataGridView1.Rows[i].Cells["orgName"].Value = orgName;
                        dataGridView1.Rows[i].Cells["deviceName"].Value = deviceName;
                        dataGridView1.Rows[i].Cells["snid"].Value = snid;
                        dataGridView1.Rows[i].Cells["type"].Value = type;
                        dataGridView1.Rows[i].Cells["district"].Value = dtJson.Rows[i]["org"].ToString();
                        dataGridView1.Rows[i].Cells["floor"].Value = dtJson.Rows[i]["storey_name"].ToString();
                        dataGridView1.Rows[i].Cells["curPosition"].Value = address;
                        dataGridView1.Rows[i].Cells["state"].Value = dtJson.Rows[i]["status"].ToString();
                        dataGridView1.Rows[i].Cells["work"].Value = dtJson.Rows[i]["rout_name_z"].ToString();

                        foreach (MoveOrder move in moveOrders) {
                            if (move.DeviceName == deviceName && snid==move.Snid && move.IsCheck) { 
                                dataGridView1.Rows[i].Cells["cl_cb"].Value = true;
                            }
                        }

                        addressCode.Add(dtJson.Rows[i]["address_code"].ToString());
                        listDeviceNos.Add(dtJson.Rows[i]["device_no"].ToString());
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private string getDeviceState() {
            string item = comboBox8.Text;
            if (item == "All") {
                return "";
             }
            if (item == "Using")
            {
                return "7";
            }
            if (item == "Lend")
            {
                return "1";
            }
            if (item == "Scrapped")
            {
                return "9";
            }
            if (item == "To_Be_Repaired")
            {
                return "5";
            }
            if (item == "In_Maintenance")
            {
                return "6";
            }
            if (item == "Idle")
            {
                return "0";
            }
            return "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySnidListByDeviceNo();
        }

        private void QuerySnidListByDeviceNo()
        {

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("device_no", deviceInfo[comboBox1.SelectedIndex]);
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
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                string deviceName = dataGridView1.Rows[i].Cells["deviceName"].Value.ToString();
                string snid = dataGridView1.Rows[i].Cells["snid"].Value.ToString();
                int index = hasMoveOrderInListBySnid(deviceName,snid);
                if (o != null && o.Value != null)
                {
                    bool isCheck = bool.Parse(o.Value.ToString());
                    if (isCheck && index == -1)
                    {

                        MoveOrder move = new MoveOrder();
                        move.IsCheck = true;
                        move.DeviceName = deviceName;
                        move.Snid = snid;
                        move.OrgName = dataGridView1.Rows[i].Cells["orgName"].Value.ToString();
                        move.DeviceType = dataGridView1.Rows[i].Cells["type"].Value.ToString();
                        move.CurPosition = dataGridView1.Rows[i].Cells["curPosition"].Value.ToString();
                        move.AddressCode = addressCode[i];
                        move.DeviceNo = listDeviceNos[i];

                        moveOrders.Add(move);
                    }
                    else if(!isCheck && index!=-1){
                        moveOrders.RemoveAt(index);
                    }
                }
            }
            if (onAddOrderCallback!=null) {
                onAddOrderCallback(moveOrders);
            }
            this.Close();
        }

        private int hasMoveOrderInListBySnid(string deviceName,string snid)
        {
            for (int i = 0; i < moveOrders.Count; i++)
            {
                MoveOrder moveOrder  = moveOrders[i];
                if (snid == moveOrder.Snid && moveOrder.DeviceName==deviceName)
                {
                    return i;
                }
            }
            return -1;
        }

     
    }
}
