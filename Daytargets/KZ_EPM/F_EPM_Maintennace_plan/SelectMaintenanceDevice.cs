using AutocompleteMenuNS;
using F_EPM_Maintennace_Plan.bean;
using MaterialSkin.Controls;
using Newtonsoft.Json;
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

namespace F_EPM_Maintennace_Plan
{
    public delegate void onSelectedDeviceResult(List<BaseDeviceInfo> baseDeviceInfos);

    public partial class SelectMaintenanceDevice : MaterialForm
    {
        public event onSelectedDeviceResult mOnSelectedDeviceResult;

        private List<string> orgs = new List<string>();
        private List<string> deviceState = new List<string>();
        private List<string> addressCodes = new List<string>();
        public List<BaseDeviceInfo> baseDeviceInfos;
        public PlanInfo info;
        
        public bool isAddType;

        public SelectMaintenanceDevice()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void SelectMaintenanceDevice_Load(object sender, EventArgs e)
        {
            QuerySJOrgInfo();
            QueryDeviceAddress();
            QueryBaseDeviceList();
            GetDeviceState();
        }
        private void QueryBaseDeviceList()
        {
           QueryBaseDeviceInfoList();
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

                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                deviceState.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceState.Add(dtJson.Rows[i]["code_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code_no"].ToString(), dtJson.Rows[i]["code_name"].ToString() }, dtJson.Rows[i]["code_name"].ToString()));
                }
                comboBox1.DataSource = items3;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void QuerySJOrgInfo()
        {
            orgs.Clear();
            //工厂
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QuerySJOrgInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                dtJson.Rows.Add("All");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "" }, "All"));
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string org = dtJson.Rows[i]["org"].ToString();
                    stringList.Add(org);
                    orgs.Add(org);
                    items3.Add(new MulticolumnAutocompleteItem(new[] { org }, org));
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

        private void QueryDeviceAddress()
        {
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_id", info.OrgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryDeviceAddress", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string addressName = dtJson.Rows[i]["address_name"].ToString();
                    stringList.Add(addressName);
                }
                autoComplete1.AddRange(stringList.ToArray());
                textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox3.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void QueryBaseDeviceInfoList() {
            addressCodes.Clear();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("device_no", info.DeviceNo);
            p.Add("orgId", info.OrgId);
            p.Add("snid", textBox1.Text);
            p.Add("address", textBox3.Text);
            p.Add("org", comboBox2.SelectedIndex <= 0 ? "" : orgs[comboBox2.SelectedIndex - 1]);
            p.Add("status", getDeviceStatus()) ;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceAddPlanServer", "QueryBaseDeviceInfoList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView1.Rows.Clear();

                int count = dtJson.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("No Data！");
                }
                for (int i = 0; i < count; i++)
                {
                    dataGridView1.Rows.Add(1);

                    dataGridView1.Rows[i].Cells["product_no"].Value = dtJson.Rows[i]["machine_no"].ToString();
                    dataGridView1.Rows[i].Cells["product_type"].Value = dtJson.Rows[i]["type"].ToString();
                    dataGridView1.Rows[i].Cells["position"].Value = dtJson.Rows[i]["address"].ToString();
                    dataGridView1.Rows[i].Cells["brand"].Value = dtJson.Rows[i]["brand"].ToString();
                    dataGridView1.Rows[i].Cells["udf05"].Value = Convert.ToDateTime(dtJson.Rows[i]["udf05"].ToString()).ToString("yyyy/MM");
                    dataGridView1.Rows[i].Cells["status"].Value = dtJson.Rows[i]["status"].ToString();

                    addressCodes.Add(dtJson.Rows[i]["address_code"].ToString());
                    foreach (BaseDeviceInfo baseDeviceInfo in baseDeviceInfos)
                    {
                        if (baseDeviceInfo.snid == dtJson.Rows[i]["machine_no"].ToString())
                        {
                            dataGridView1.Rows[i].Cells["cl_cb"].Value = baseDeviceInfo.subIsCheck;
                        }
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private int getDeviceStatus() {
            int index = comboBox1.SelectedIndex;
            if (index <=0) {
                return -1;
            }
            return int.Parse(deviceState[comboBox1.SelectedIndex]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QueryBaseDeviceList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baseDeviceInfos.Clear();
            for (int i = 0;i<dataGridView1.Rows.Count;i++) {
                dataGridView1.Rows[i].Cells["cl_cb"].Value = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell val=  dataGridView1.Rows[i].Cells["cl_cb"];
                string snid=  dataGridView1.Rows[i].Cells["product_no"].Value.ToString();
                int index = hasDevieInfoInListBySnid(snid);
                if (val!=null && val.Value!=null) {
                    bool reuslt = bool.Parse(val.Value.ToString());
                    if (reuslt && index == -1) {
                        baseDeviceInfos.Add(wrapBaseDeviceInfo(dataGridView1.Rows[i].Cells,i));
                    } else if (!reuslt && index > -1 && baseDeviceInfos[index].enable != "Y") {
                        baseDeviceInfos.RemoveAt(index);
                    }
                }
            }
            if (mOnSelectedDeviceResult!=null) {
                mOnSelectedDeviceResult(baseDeviceInfos);
            }
            this.Close();

        }

        private int hasDevieInfoInListBySnid(string snid) {
            for (int i=0;i<baseDeviceInfos.Count;i++) {
                BaseDeviceInfo baseDeviceInfo = baseDeviceInfos[i];
                if (snid == baseDeviceInfo.snid) {
                    return i;
                }
            }
            return -1;
        }

        private BaseDeviceInfo wrapBaseDeviceInfo(DataGridViewCellCollection collection,int index) {
            BaseDeviceInfo deviceInfo = new BaseDeviceInfo();
            deviceInfo.snid = collection["product_no"].Value.ToString();
            deviceInfo.type = collection["product_type"].Value.ToString();
            deviceInfo.address = collection["position"].Value.ToString();
            deviceInfo.addressCode = addressCodes[index];
            deviceInfo.brand = collection["brand"].Value.ToString();
            deviceInfo.udf05 = collection["udf05"].Value.ToString();
            deviceInfo.status = collection["status"].Value.ToString();
            deviceInfo.subIsCheck = true;
            return deviceInfo;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           baseDeviceInfos.Clear();
           for (int i = 0; i < dataGridView1.Rows.Count; i++)
           {
               dataGridView1.Rows[i].Cells["cl_cb"].Value = false;
           }
        }
    }
}
