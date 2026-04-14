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

namespace F_EPM_Device_Standing_Book
{

    public delegate void OnUpdateListener();
    public partial class AdjustmentForm : MaterialForm
    {
        public JObject o;

        private string preAddress = "";

        private List<string> addressCodes = new List<string>();
        private List<string> deviceStatus = new List<string>();

        public event OnUpdateListener listener;

        public AdjustmentForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void AdjustmentForm_Load(object sender, EventArgs e)
        {
            GetAddressInfoByOrgId();
            GetDeviceState();
            if (o != null)
            {
                textBox1.Text = o.GetValue("name").ToString();
                textBox2.Text = o.GetValue("snid").ToString();
                textBox4.Text = o.GetValue("orgName").ToString();
                textBox3.Text = o.GetValue("type").ToString();
                textBox6.Text = o.GetValue("date").ToString();
            }
        }

        private void GetAddressInfoByOrgId()
        {
            addressCodes.Clear();

            List<AutocompleteItem> autoAddress = new List<AutocompleteItem>();
            List<string> addressStr = new List<string>();
            AutoCompleteStringCollection autoCompleteAddress = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", o.GetValue("orgCode").ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson =JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string addressCode = dtJson.Rows[i]["address_code"].ToString();
                    string addressName = dtJson.Rows[i]["address_name"].ToString();
                    addressCodes.Add(addressCode);
                    addressStr.Add(addressName);
                    autoAddress.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["address_code"].ToString(), dtJson.Rows[i]["address_name"].ToString() }, dtJson.Rows[i]["address_name"].ToString()));
                }
                comboBox2.DataSource = autoAddress;
                autoCompleteAddress.AddRange(addressStr.ToArray());
                comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox2.AutoCompleteCustomSource = autoCompleteAddress;

                comboBox2.Text = o.GetValue("address").ToString();
                int index = addressStr.IndexOf(comboBox2.Text);
                if (index > -1)
                {
                    preAddress = addressCodes[index];
                    comboBox2.SelectedIndex = index;
                }
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

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceStatus.Add(dtJson.Rows[i]["code_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["code_no"].ToString(), dtJson.Rows[i]["code_name"].ToString() }, dtJson.Rows[i]["code_name"].ToString()));
                }
                comboBox1.DataSource = items3;
                comboBox1.Text = o.GetValue("status").ToString();
                int index = comboBox1.Items.IndexOf(comboBox1.Text);
                if (index>-1) {
                    comboBox1.SelectedIndex = index;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("SN_id", textBox2.Text);
            p.Add("before_address", preAddress);
            p.Add("address_id",addressCodes[comboBox2.SelectedIndex]);
            p.Add("status", deviceStatus[comboBox1.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "updateDeviceInfo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("Saved successfully！");
                if (listener!=null) {
                    listener();
                }
                Close();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

    }
}
