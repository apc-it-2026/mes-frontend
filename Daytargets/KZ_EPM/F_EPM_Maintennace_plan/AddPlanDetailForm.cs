using AutocompleteMenuNS;
using F_EPM_Maintennace_Plan.bean;
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
using static System.Windows.Forms.CheckedListBox;

namespace F_EPM_Maintennace_Plan
{
    public partial class AddPlanDetailForm : MaterialForm
    {
        AutoCompleteStringCollection autoComplete_factory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_device = new AutoCompleteStringCollection();

        private List<string> orgIds = new List<string>();
        private List<string> deviceNos = new List<string>();
        private List<int> selectWeek = new List<int>();
        private List<int> selectDay = new List<int>();
        private List<int> selectMonth = new List<int>();

        public List<MaintenanceType> maintenanceTypes=new List<MaintenanceType>();
        public List<BaseDeviceInfo> baseDeviceInfos = new List<BaseDeviceInfo>();
        private Dictionary<string, string> adrCodes = new Dictionary<string, string>();


        public AddPlanDetailForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
        }

        private void PlanDetailForm_Load(object sender, EventArgs e)
        {
            LoadOrg();
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
            deviceNos.Clear();
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
                items3.Add(new MulticolumnAutocompleteItem(new string[] { ""}, ""));
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    autoComplete1.Add(dtJson.Rows[i]["device_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_name"].ToString()));
                    deviceNos.Add(dtJson.Rows[i]["device_no"].ToString());
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

        private string FindOrgCode()
        {
            string result = comboBox1.Text;
            for (int i = 0; i < orgIds.Count; i++)
            {
                string[] orgs = orgIds[i].Split('|');
                if (orgs[1] == result)
                {
                    return orgs[0];
                }
            }
            return "";
        }


        private void textBox5_Click(object sender, EventArgs e)
        {
            checkedListBox1.Visible = !checkedListBox1.Visible;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            checkedListBox2.Visible = !checkedListBox2.Visible;
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            checkedListBox3.Visible = !checkedListBox3.Visible;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string currentTime =  DateTime.Now.ToString("yyyy/MM/dd");

            DateTime curDateTime = Convert.ToDateTime(currentTime);
            DateTime dateTime = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime dateTime2 = Convert.ToDateTime(dateTimePicker2.Value);
            if (DateTime.Compare(dateTime,curDateTime)<0) {
                MessageBox.Show($"Please select a date greater than or equal to {curDateTime}");
                dateTimePicker1.Value = curDateTime;
            }
            if (DateTime.Compare(dateTime2, dateTime) < 0) { 
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dateTime1 = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime dateTime2 = Convert.ToDateTime(dateTimePicker2.Value);
            if (DateTime.Compare(dateTime2, dateTime1) < 0) {
                MessageBox.Show($"Invalid Time! Start time is greater than end time！");
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex<=0) {
                MessageBox.Show("Please select maintenance equipment");
                return;
            }
            SelectMaintenanceProject  project=  new SelectMaintenanceProject();
            project.deviceNo = deviceNos[comboBox2.SelectedIndex-1];
            project.isAddType = true;
            project.maintenanceTypes = maintenanceTypes;
            project.mOnSelectedProjectResult += new onSelectedProjectResult(onSelectedProjectResult);
            project.ShowDialog();

        }

        public void onSelectedProjectResult(List<MaintenanceType> maintenanceTypes) {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < maintenanceTypes.Count; i++)
            {
                dataGridView1.Rows.Add(1);

                MaintenanceType maintenanceType = maintenanceTypes[i];
                dataGridView1.Rows[i].Cells["cl_cb"].Value = maintenanceType.isCheck;
                dataGridView1.Rows[i].Cells["by_no"].Value = maintenanceType.byNo;
                dataGridView1.Rows[i].Cells["part_body"].Value = maintenanceType.bodyPart;
                dataGridView1.Rows[i].Cells["item"].Value = maintenanceType.item;
                dataGridView1.Rows[i].Cells["standard"].Value = maintenanceType.standard;
                dataGridView1.Rows[i].Cells["level"].Value = maintenanceType.levelName;
            }

        }

        private void getMonthDay() {
           
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedItemCollection items = checkedListBox1.CheckedItems;
            textBox5.Text = "";
            selectWeek.Clear();
            foreach (string item in items)
            {
                textBox5.Text += item + " ";
                selectWeek.Add(checkedListBox1.Items.IndexOf(item)+1); 
            }
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedItemCollection items = checkedListBox2.CheckedItems;
            textBox6.Text = "";
            selectMonth.Clear();
            foreach (string item in items)
            {
                textBox6.Text += item + " ";
                selectMonth.Add(checkedListBox2.Items.IndexOf(item) + 1);
            }
        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedItemCollection items = checkedListBox3.CheckedItems;
            textBox7.Text = "";
            selectDay.Clear();
            foreach (int item in items)
            {
                textBox7.Text += item + " ";
                selectDay.Add(checkedListBox3.Items.IndexOf(item)+1);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox4.SelectedIndex;

            textBox7.Enabled = index == 2 || index == 3;
            textBox5.Enabled = index == 1;
            textBox6.Enabled =  index == 3;

            textBox7.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

            checkedListBox1.Visible = false;
            checkedListBox2.Visible = false;
            checkedListBox3.Visible = false;
            if (index == 2)
            {
                checkedListBox3.Items.Clear();
                DateTime startTime = DateTime.Parse(dateTimePicker1.Text);
                int days = DateTime.DaysInMonth(startTime.Year, startTime.Month);
                for (int i = 1; i <= days; i++)
                {
                    checkedListBox3.Items.Add(i);
                }
            }
            else {
                for (int i = 1; i <= 31; i++)
                {
                    checkedListBox3.Items.Add(i);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          List<int> indexs=  getSelectIndex(dataGridView1, "cl_cb");
            if (indexs.Count>0)
            {
                DialogResult dialogResult = MessageBox.Show("Please confirm whether to delete the selected maintenance item", "Prompt", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    for (int i = 0; i < indexs.Count; i++)
                    {
                        int index = indexs[i];
                        dataGridView1.Rows.RemoveAt(i == 0 ? index : index - i);
                        maintenanceTypes.RemoveAt(i == 0 ? index : index - i);
                    }
                    MessageBox.Show("Successfully Deleted");
                }
            }
        }

        private List<int> getSelectIndex(DataGridView dataGridView, string column)
        {
            List<int> ls = new List<int>();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView.Rows[i].Cells[column];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    ls.Add(i);
                }
            }
            return ls;
        }

        private bool hasSelectItem(DataGridView dataGridView, string column)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView.Rows[i].Cells[column];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            if (index <=0) {
                MessageBox.Show("Please select maintenance equipment！");
                return;
            }
            SelectMaintenanceDevice device=new SelectMaintenanceDevice();
            PlanInfo info= new PlanInfo();
            info.DeviceNo = deviceNos[index - 1];
            info.OrgId = FindOrgCode();
            device.info = info;
            device.isAddType = true;
            device.baseDeviceInfos = baseDeviceInfos;
            device.mOnSelectedDeviceResult += new onSelectedDeviceResult(OnSelectedDeviceResultListener);
            device.ShowDialog();
        }

        public void OnSelectedDeviceResultListener(List<BaseDeviceInfo> baseDeviceInfos)
        {
            dataGridView2.Rows.Clear();

            for (int i = 0; i < baseDeviceInfos.Count; i++)
            {
                dataGridView2.Rows.Add(1);

                BaseDeviceInfo baseDeviceInfo = baseDeviceInfos[i];

                dataGridView2.Rows[i].Cells["cl_cb1"].Value = baseDeviceInfo.isCheck;
                dataGridView2.Rows[i].Cells["product_no"].Value = baseDeviceInfo.snid;
                dataGridView2.Rows[i].Cells["product_type"].Value = baseDeviceInfo.type;
                dataGridView2.Rows[i].Cells["location"].Value = baseDeviceInfo.address;
                dataGridView2.Rows[i].Cells["brand"].Value = baseDeviceInfo.brand;
                dataGridView2.Rows[i].Cells["udf05"].Value = baseDeviceInfo.udf05;
                dataGridView2.Rows[i].Cells["status"].Value = baseDeviceInfo.status;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<int> indexs = getSelectIndex(dataGridView2, "cl_cb1");
            if (indexs.Count>0)
            {
                DialogResult dialogResult = MessageBox.Show("Please confirm whether to delete the selected maintenance equipment", "Prompt", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    for (int i = 0; i < indexs.Count; i++)
                    {
                        int index = indexs[i];
                        dataGridView2.Rows.RemoveAt(i == 0 ? index : index - i);
                        baseDeviceInfos.RemoveAt(i == 0 ? index : index - i);
                    }
                    MessageBox.Show("Successfully deleted");
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string planName = textBox2.Text;
            string orgId = FindOrgCode();
            int period = comboBox4.SelectedIndex + 1;
            string intervalStr = textBox3.Text; 
            string frequencyStr = textBox4.Text; 
            if (planName.Length == 0) {
                MessageBox.Show("Please enter a program name！");
                return;
            }
       
            if (comboBox4.SelectedIndex == -1) {
                MessageBox.Show("Please select a planning cycle！");
                return;
            }
            if (intervalStr == "") {
                MessageBox.Show("Please enter interval！");
                return;
            } 
            
            if (frequencyStr == "") {
                MessageBox.Show("Please enter frequency！");
                return;
            }
            if (comboBox2.SelectedIndex ==0)
            {
                MessageBox.Show("Please select a device name！");
                return;
            }


            DateTime startTime = DateTime.Parse(dateTimePicker1.Text);
            DateTime endTime = DateTime.Parse(dateTimePicker2.Text);
            int interval = Convert.ToInt32(intervalStr);
            int frequency = Convert.ToInt32(frequencyStr);
            if (period == 1) {
                //日
            } else if (period == 2) {
                if (textBox5.Text == "") {
                    MessageBox.Show("Week cannot be empty");
                    return;
                }
                if ((int)endTime.DayOfWeek!=0) {
                    MessageBox.Show("The Maintenance cycle is weekly, and the end time must be Sunday");
                    return;
                }
                string[] weeks = textBox5.Text.Trim().Split(' ');
                if (weeks.Length != frequency) {
                    MessageBox.Show("Days of week selection must be equal to frequency！");
                    return;
                }
            } else if (period == 3) {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Day cannot be empty");
                    return;
                }
                int days = DateTime.DaysInMonth(endTime.Year, endTime.Month);
                if (endTime.Day !=days)
                {
                    MessageBox.Show($"The maintenance cycle is monthly, and the end time must be the last day of the month！");
                    return;
                }
                string[] d = textBox7.Text.Trim().Split(' ');
                if (d.Length != frequency)
                {
                    MessageBox.Show("Day selection days must be equal to frequency！");
                    return;
                }
            } else if (period == 4) {
                if (textBox7.Text == "")
                {
                    MessageBox.Show("Day cannot be empty");
                    return;
                }
                if (textBox6.Text=="") {
                    MessageBox.Show("Month cannot be empty");
                    return;
                }
                DateTime dt = DateTime.Parse($"{endTime.Year}/12/31");
                if (DateTime.Compare(endTime,dt)!=0)
                {
                    MessageBox.Show($"The Maintenance cycle is a year, and the end time must be the last day of the year！");
                    return;
                }
                string[] d = textBox7.Text.Trim().Split(' ');
                if (d.Length != frequency)
                {
                    MessageBox.Show("Selected days must equal frequency！");
                    return;
                }
                d = textBox6.Text.Trim().Split(' ');
                if (d.Length != frequency)
                {
                    MessageBox.Show("The number of months selected must be equal to the frequency！");
                    return;
                }
            }

            if (maintenanceTypes.Count == 0)
            {
                MessageBox.Show("Please select the item to be maintained！");
                return;
            }
            if (baseDeviceInfos.Count == 0)
            {
                MessageBox.Show("Please select the equipment that needs maintenance！");
                return;
            }

            List<string> byNos = new List<string>();
            List<string> snids = new List<string>();
            foreach (MaintenanceType maintenanceType in maintenanceTypes) 
            {
                byNos.Add(maintenanceType.byNo);
            }
            foreach (BaseDeviceInfo baseDeviceInfo in baseDeviceInfos)
            {
                snids.Add(baseDeviceInfo.snid);
                adrCodes.Add(baseDeviceInfo.snid,baseDeviceInfo.addressCode);
            }

            Dictionary<string, object> p = new Dictionary<string, object>();

            p["plan_name"] = planName;
            p["device_no"] = deviceNos[comboBox2.SelectedIndex-1];
            p["org_id"] = orgId;
            p["plan_begindate"] = Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd");
            p["plan_enddate"] = Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy/MM/dd");
            //计算第一次计划的完成时间
            if (period == 1)
            {
                p["plan_finishdate"] = JArray.Parse(JsonConvert.SerializeObject(new string[] { p["plan_begindate"].ToString() }));
            } else if (period == 2)
            {
                p["plan_finishdate"] = JArray.Parse((JsonConvert.SerializeObject(transitionWeekTime())));
            } else if (period == 3) 
            {
                p["plan_finishdate"] = JArray.Parse((JsonConvert.SerializeObject(transitionMonthDay())));
            } else if (period ==4) 
            {
                p["plan_finishdate"] = JArray.Parse((JsonConvert.SerializeObject(transitionYear())));
            }
            p["frequency_info"] =JsonConvert.SerializeObject(p["plan_finishdate"]);
            p["snids"] = JArray.Parse((JsonConvert.SerializeObject(snids)));
            p["byNos"] = JArray.Parse((JsonConvert.SerializeObject(byNos)));
            p["addressCodes"] = JObject.Parse(JsonConvert.SerializeObject(adrCodes));
            p["period"] = period;
            p["interval"] = interval;
            p["frequency"] = frequency;

            buttonEnable(false);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceAddPlanServer", "InsertMaintenancePlan", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                DialogResult result = MessageBox.Show("Saved successfully");
                if (result == DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                buttonEnable(true);
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void buttonEnable(bool enable) {
            button1.Enabled = enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
            button4.Enabled = enable;
            button5.Enabled = enable;
            button6.Enabled = enable;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            workCountConfine();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            workCountConfine();
        }

        private void workCountConfine() {
            string countStr = textBox4.Text.Trim();
            string intervalStr = textBox3.Text.Trim();
            if (countStr.Length == 0 || intervalStr.Length == 0)
            {
                return;
            }
            int count = Convert.ToInt32(countStr);
            int interval = Convert.ToInt32(intervalStr);
            if (count > 1 && interval > 1)
            {
                MessageBox.Show("When the frequency >= 2, the interval can only be = 1, otherwise it cannot be saved!");
                textBox3.Text = "1";
            }
        }

        private List<string> transitionWeekTime() {
            List<string> days = new List<string>();
            DateTime startTime = DateTime.Parse(dateTimePicker1.Text);

            int startWeekDay = (int)startTime.DayOfWeek;
            for (int i =0;i<selectWeek.Count;i++) {
                int w = selectWeek[i];
                if (startWeekDay == w)
                {
                    days.Add(Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd"));
                }else if (startWeekDay>w) {
                    days.Add(startTime.AddDays((w + 7) - startWeekDay).ToString("yyyy/MM/dd"));
                }else if (startWeekDay<w) {
                    days.Add(startTime.AddDays(w - startWeekDay).ToString("yyyy/MM/dd"));
                }
            }
            return days;
        }

        private List<string> transitionMonthDay()
        {
            List<string> days = new List<string>();
            DateTime startTime = DateTime.Parse(dateTimePicker1.Text);
            int startDay = (int)startTime.Day;
            for (int i = 0; i < selectDay.Count; i++)
            {
                int d = selectDay[i];
                if (startDay == d)
                {
                    days.Add(Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd"));
                }
                else if (startDay > d)
                {
                    int day = DateTime.DaysInMonth(startTime.Year, startTime.Month);
                    days.Add(startTime.AddDays(day - startDay+ d).ToString("yyyy/MM/dd"));
                   
                }
                else if (startDay < d)
                {
                    days.Add(startTime.AddDays(d - startDay).ToString("yyyy/MM/dd"));
                }
            }
            return days;
        }

        private List<string> transitionYear()
        {
            List<string> days = new List<string>();
            DateTime startTime = DateTime.Parse(dateTimePicker1.Text);
            int year = (int)startTime.Year;
            int startMonth = (int)startTime.Month;
            int startDay = (int)startTime.Day;
            for (int i = 0; i < selectMonth.Count; i++)
            {
                int month = selectMonth[i];
                int day = selectDay[i];
                if ((month==4|| month == 6 || month == 9 || month ==11) && day>30) {
                    day = 30;
                }else if (month==2&&day>28) {
                    int offsetYear = startTime.Month > 2 ? 1 : 0;
                    if ((offsetYear+year) % 4 == 0)
                    {
                        day = 29;
                    }
                    else {
                        day = 28;
                    }
                }
                int startYear = year;
                if (month < startMonth || (month == startMonth && day < startDay)) {
                     startYear++;
                } 
                string date = $"{startYear}/{month}/{day}";
                days.Add(Convert.ToDateTime(date).ToString("yyyy/MM/dd"));
            }

            return days;
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            setTextBoxPureNumber(textBox4,e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            setTextBoxPureNumber(textBox3, e);
        }

        private void setTextBoxPureNumber(TextBox textBox, KeyPressEventArgs e) {

            //开头不允许出现0
            if (textBox.SelectionStart == 0)
            {
                if (e.KeyChar == '0')
                {
                    e.Handled = true;
                    return;
                }
            }

            //只能
            if (e.KeyChar == '0'
                || e.KeyChar == '1'
                || e.KeyChar == '2'
                || e.KeyChar == '3'
                || e.KeyChar == '4'
                || e.KeyChar == '5'
                || e.KeyChar == '6'
                || e.KeyChar == '7'
                || e.KeyChar == '8'
                || e.KeyChar == '9'
                || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }
    }
}
