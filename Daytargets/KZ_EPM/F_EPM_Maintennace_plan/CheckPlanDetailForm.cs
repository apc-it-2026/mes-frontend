using F_EPM_Maintennace_Plan.bean;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace F_EPM_Maintennace_Plan
{

    public delegate void UpdateWholeOrderListener();
    public partial class CheckPlanDetailForm : MaterialForm
    {

        public event UpdateWholeOrderListener updateWholeOrderListener;

        public PlanInfo info =null;
        private List<MaintenanceType> maintenanceTypes = new List<MaintenanceType>();
        private List<BaseDeviceInfo> baseDeviceInfos = new List<BaseDeviceInfo>();

        private Dictionary<string, string> updateMaintenanceTypes = new Dictionary<string, string>();
        private Dictionary<string, string> updateBaseDeviceInfos = new Dictionary<string, string>();
        private Dictionary<string, string> address = new Dictionary<string, string>();

        string[] weekdays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        private JArray jArray;

        private string deleteVal = "";
        private int typeDeleteCount;
        private int deviceDeleteCount;


        public CheckPlanDetailForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

        }

        private void PlanDetailForm_Load(object sender, EventArgs e)
        {
            initView();
        }

        private void initView() {
            if (info !=null) {
                textBox1.Text = info.PlanNo;
                textBox2.Text = info.PlanName;
                textBox8.Text = info.OrgName;
                textBox9.Text = info.DeviceName;
                textBox10.Text = info.Status;
                textBox12.Text = Convert.ToDateTime(info.StartDate).ToString("yyyy/MM/dd");
                textBox11.Text = Convert.ToDateTime(info.EndDate).ToString("yyyy/MM/dd");
                textBox3.Text = info.Interval;
                textBox4.Text = info.Frequency;
                textBox13.Text = info.Period;

                deleteVal = info.Status == "Using" ? "N" : "Y";
                if (isDelete()) {
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    button4.Visible = false;
                    button6.Visible = false;
                }


                string p = info.Period;
                jArray = (JArray)JsonConvert.DeserializeObject(info.FrequencyInfo);
                if (jArray!=null) {
                    if (p == "Week")
                    {
                        foreach (string sd in jArray)
                        {
                            DateTime date = DateTime.Parse(sd);
                            textBox5.Text += weekdays[Convert.ToInt32(date.DayOfWeek)] + " ";
                        }
                    }
                    else if (p == "Month")
                    {
                        foreach (string sd in jArray)
                        {
                            DateTime date = DateTime.Parse(sd);
                            textBox7.Text += date.Day.ToString() + " ";
                        }
                    }
                    else if (p == "Year")
                    {
                        foreach (string sd in jArray)
                        {
                            DateTime date = DateTime.Parse(sd);
                            textBox6.Text += date.Month.ToString();
                            textBox7.Text += date.Day.ToString();
                        }
                    }
                }
                getMaintennaceTypeList();
                QueryDeviceBaseInfo();
                QueryMaintenanceList();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QueryMaintenanceList()
        {
            if (deleteVal == "N") {
                return;
            }
            address.Clear();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("plan_id", info.PlanNo);
            p.Add("is_delete", deleteVal);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryMaintenanceList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string snid = dtJson.Rows[i]["snid"].ToString();
                    string nowAddRess = dtJson.Rows[i]["NOW_ADDRESS"].ToString();

                    if(address.ContainsKey(snid)){
                        address[snid] = nowAddRess;
                    }else { 
                        address.Add(snid,nowAddRess);
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void getMaintennaceTypeList()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("plan_id", info.PlanNo);
            p.Add("device_no", info.DeviceNo);
            p.Add("is_delete", deleteVal);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryMaintenanceTypeList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
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

                    string byNo = dtJson.Rows[i]["by_no"].ToString();
                    string bodyPart = dtJson.Rows[i]["body_part"].ToString();
                    string item = dtJson.Rows[i]["item"].ToString();
                    string standard = dtJson.Rows[i]["standard"].ToString();
                    string levelName = dtJson.Rows[i]["level_name"].ToString();

                    dataGridView1.Rows[i].Cells["by_no"].Value = byNo;
                    dataGridView1.Rows[i].Cells["part_body"].Value = bodyPart;
                    dataGridView1.Rows[i].Cells["item"].Value = item;
                    dataGridView1.Rows[i].Cells["standard"].Value = standard;
                    dataGridView1.Rows[i].Cells["level"].Value = levelName;

                    MaintenanceType maintenanceType = new MaintenanceType();
                    maintenanceType.byNo = byNo;
                    maintenanceType.bodyPart = bodyPart;
                    maintenanceType.item = item;
                    maintenanceType.standard = standard;
                    maintenanceType.levelName = levelName;
                    maintenanceType.enable = "Y";
                    maintenanceTypes.Add(maintenanceType);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteType();
        }

        private void DeleteType() {
            if (hasSelectItem(dataGridView1,"cl_cb")) {
                DialogResult dialogResult = MessageBox.Show("Please confirm whether to delete the selected maintenance item", "Prompt", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                  updateMaintenanceTypes.Clear();
                  string byNos= GetDeleteIds(dataGridView1, "cl_cb", "by_no");
                  string[] nos = byNos.Split(',');
                  typeDeleteCount = nos.Length;
                  foreach(string no in nos) {
                  updateMaintenanceTypes[no] = "N";
                  }
                    MessageBox.Show("Delete the mark successfully, click save to take effect");
                }
            }
        }

        private void QueryDeviceBaseInfo() {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("plan_id", info.PlanNo);
            p.Add("device_no", info.DeviceNo);
            p.Add("delete", deleteVal);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "QueryDeviceBaseInfo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView2.Rows.Clear();

                int count = dtJson.Rows.Count;
                if (count == 0)
                {
                    MessageBox.Show("No Data！");
                }
                for (int i = 0; i < count; i++)
                {
                    dataGridView2.Rows.Add(1);

                    string snid= dtJson.Rows[i]["machine_no"].ToString();
                    string type = dtJson.Rows[i]["type"].ToString();
                    string address = dtJson.Rows[i]["address"].ToString();
                    string brand = dtJson.Rows[i]["brand"].ToString();
                    string udf05 = Convert.ToDateTime(dtJson.Rows[i]["udf05"].ToString()).ToString("yyyy/MM");
                    string status = dtJson.Rows[i]["status"].ToString();

                    dataGridView2.Rows[i].Cells["product_no"].Value = snid;
                    dataGridView2.Rows[i].Cells["product_type"].Value = type;
                    dataGridView2.Rows[i].Cells["location"].Value = address;
                    dataGridView2.Rows[i].Cells["brand"].Value = brand;
                    dataGridView2.Rows[i].Cells["udf05"].Value = udf05;
                    dataGridView2.Rows[i].Cells["status"].Value = status;

                    BaseDeviceInfo baseDeviceInfo = new BaseDeviceInfo();
                    baseDeviceInfo.snid = snid;
                    baseDeviceInfo.type = type;
                    baseDeviceInfo.address = address;
                    baseDeviceInfo.brand = brand;
                    baseDeviceInfo.udf05 = udf05;
                    baseDeviceInfo.status = status;
                    baseDeviceInfo.enable = "Y";
                    baseDeviceInfos.Add(baseDeviceInfo);

                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private string GetDeleteIds(DataGridView dataGridView, string column, string columnForValue)
        {
            string result = "";
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView.Rows[i].Cells[column];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    if (result.Length > 0)
                    {
                        result += ",";
                    }
                    result += dataGridView.Rows[i].Cells[columnForValue].Value.ToString();
                }

            }
            return result;
        }

        private bool hasSelectItem(DataGridView dataGridView,string column) {
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

        private List<int> getSelectIndex(DataGridView dataGridView, string column) {
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

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                string byNo = dataGridView1.Rows[i].Cells["by_no"].Value.ToString(); ;
                if (o != null && o.Value != null)
                {
                    foreach (MaintenanceType maintenanceType in maintenanceTypes) {
                        if (byNo == maintenanceType.byNo) {
                             maintenanceType.isCheck = bool.Parse(o.Value.ToString());
                        }
                    }
                }
            }
            SelectMaintenanceProject selectMaintenanceProject = new SelectMaintenanceProject();
            selectMaintenanceProject.planId = info.PlanNo;
            selectMaintenanceProject.deviceNo = info.DeviceNo;
            selectMaintenanceProject.maintenanceTypes = maintenanceTypes;
            selectMaintenanceProject.mOnSelectedProjectResult += new onSelectedProjectResult(onSelectedProjectResult);
            selectMaintenanceProject.ShowDialog();
        }


        private void onSelectedProjectResult(List<MaintenanceType> maintenanceTypes) {

            dataGridView1.Rows.Clear();
            for (int i =0;i<maintenanceTypes.Count;i++) {
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



        private bool isDelete() {
            return deleteVal == "Y";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView2.Rows[i].Cells["cl_cb1"];
                string snid = dataGridView2.Rows[i].Cells["product_no"].Value.ToString(); ;
                if (o != null && o.Value != null)
                {
                    foreach (BaseDeviceInfo baseDeviceInfo in baseDeviceInfos)
                    {
                        if (snid == baseDeviceInfo.snid)
                        {
                            baseDeviceInfo.isCheck = bool.Parse(o.Value.ToString());
                        }
                    }
                }
            }
            SelectMaintenanceDevice device = new SelectMaintenanceDevice();
            device.info = info;
            device.baseDeviceInfos = baseDeviceInfos;
            device.mOnSelectedDeviceResult += new onSelectedDeviceResult(OnSelectedDeviceResultListener); 
            device.ShowDialog();
        }

        public void OnSelectedDeviceResultListener(List<BaseDeviceInfo> baseDeviceInfos) {
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
            if (hasSelectItem(dataGridView2, "cl_cb1"))
            {
                DialogResult dialogResult = MessageBox.Show("Please confirm whether to delete the selected maintenance equipment", "Prompt", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    updateBaseDeviceInfos.Clear();
                    string byNos = GetDeleteIds(dataGridView2, "cl_cb1", "product_no");
                    string[] nos = byNos.Split(',');
                    deviceDeleteCount = nos.Length;
                    foreach (string no in nos)
                    {
                        updateBaseDeviceInfos[no] = "N";
                    }
                    MessageBox.Show("Delete the mark successfully, click save to take effect");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool isWholeOrder = typeDeleteCount >= dataGridView1.Rows.Count && deviceDeleteCount >= dataGridView2.Rows.Count;
            foreach (MaintenanceType type in maintenanceTypes) {
                //type.enable != type.newEnable && 
                if (!updateMaintenanceTypes.ContainsKey(type.byNo)) {
                    updateMaintenanceTypes.Add(type.byNo,type.newEnable);
                }
            }
            foreach (BaseDeviceInfo deviceInfo in baseDeviceInfos) {
                //deviceInfo.enable != deviceInfo.newEnable && 
                if (!updateBaseDeviceInfos.ContainsKey(deviceInfo.snid))
                {
                    updateBaseDeviceInfos.Add(deviceInfo.snid, deviceInfo.newEnable);
                }
                //更新设备最新位置，在保养任务清单中获取的可能是旧地址
                if (!address.ContainsKey(deviceInfo.snid))
                {
                    address.Add(deviceInfo.snid, deviceInfo.addressCode);
                }
                else { 
                    address[deviceInfo.snid]= deviceInfo.addressCode;
                }
            }
            if (isWholeOrder)
            {
                //删除整单
                UpdateMaintenanceWholeOrderState();
            }
            else {
                //更新指定新增/删除
                UpdateTypeAndDeviceEnable();
            }
        }

        private void UpdateMaintenanceWholeOrderState() {
            Dictionary<string, object> p = new Dictionary<string, object>();
            List<string> infos = new List<string>();
            infos.Add(info.PlanNo);
            p.Add("planIds", JArray.Parse(JsonConvert.SerializeObject(infos)));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenancePlanServer", "UpdateMaintenanceWholeOrderState", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                info.Status = "Deleted";
                deleteVal = "Y";
                getMaintennaceTypeList();
                if (updateWholeOrderListener != null)
                {
                    updateWholeOrderListener();
                }
                DialogResult result=MessageBox.Show("Saved successfully");
                if (result == DialogResult.OK) {
                    this.Close();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void buttonEnable(bool enable)
        {
            button1.Enabled = enable;
            button2.Enabled = enable;
            button3.Enabled = enable;
            button4.Enabled = enable;
            button5.Enabled = enable;
            button6.Enabled = enable;
        }
        private int periodToInt() {
            string period = info.Period;
            if (period == "Day") {
                return 1;
            }
            if (period == "Week") {
                return 2;
            }
            if (period == "Month") {
                return 3;
            }
            if (period == "Year") {
                return 4;
            }
            return 1;
        }

        private void UpdateTypeAndDeviceEnable()
        {
            if (updateBaseDeviceInfos.Count == 0 && updateMaintenanceTypes.Count == 0)
            {
                return;
            }
            List<string> enableByNos = new List<string>();
             List<string> unableByNos = new List<string>();
             List<string> enableBySnids = new List<string>();
             List<string> unableBySnids = new List<string>();
            foreach (string key in updateMaintenanceTypes.Keys)
            {
                string enable = updateMaintenanceTypes[key];
                if (enable == "Y")
                {
                    enableByNos.Add(key);
                }
                else
                {
                    unableByNos.Add(key);
                }
            }

            foreach (string key in updateBaseDeviceInfos.Keys)
            {
                string enable = updateBaseDeviceInfos[key];
                if (enable == "Y")
                {
                    enableBySnids.Add(key);
                }
                else
                {
                    unableBySnids.Add(key);
                }
            }
            
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("plan_id",info.PlanNo);
            p.Add("plan_name", info.PlanName);
            p.Add("device_no", info.DeviceNo);
            p.Add("org_id", info.OrgId);
            p.Add("plan_begindate", Convert.ToDateTime(textBox12.Text).ToString("yyyy/MM/dd"));
            p.Add("plan_enddate", Convert.ToDateTime(textBox11.Text).ToString("yyyy/MM/dd"));
            p.Add("plan_finishdate", jArray);
            p.Add("frequency",textBox4.Text.ToInt());
            p.Add("interval", info.Interval);
            p.Add("period", periodToInt());
            p.Add("enableByNos", JArray.Parse(JsonConvert.SerializeObject(enableByNos)));
            p.Add("unableByNos", JArray.Parse(JsonConvert.SerializeObject(unableByNos)));
            p.Add("enableBySnids", JArray.Parse(JsonConvert.SerializeObject(enableBySnids)));
            p.Add("unableBySnids", JArray.Parse(JsonConvert.SerializeObject(unableBySnids)));
            p.Add("address", JObject.Parse(JsonConvert.SerializeObject(address)));
            buttonEnable(false);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MaintenanceAddPlanServer", "UpdateOrInsertMaintenanceList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                //List<int> indexs= getSelectIndex(dataGridView1,"cl_cb");
                // for (int i=0;i < indexs.Count;i++) {
                //     int index = indexs[i];
                //     dataGridView1.Rows.RemoveAt(i == 0?index:index-1);
                //     maintenanceTypes.RemoveAt(i == 0 ? index : index - 1);
                // }
                // indexs = getSelectIndex(dataGridView2,"cl_cb1");
                // for (int i = 0; i < indexs.Count; i++)
                // {
                //     int index = indexs[i];
                //     dataGridView2.Rows.RemoveAt(i == 0 ? index : index - 1);
                //     baseDeviceInfos.RemoveAt(i == 0 ? index : index - 1);
                // }
               DialogResult  result=  MessageBox.Show("Saved successfully");
                if (result==DialogResult.OK) {
                    this.Close();
                }
            }
            else
            {
                buttonEnable(true);
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
    }
}
