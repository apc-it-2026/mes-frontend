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
    public partial class Equipment_AllocationMainForm : MaterialForm
    {
        DateTimePicker datatimepick = new DateTimePicker();

        AutoCompleteStringCollection autoComplete_factory = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_device = new AutoCompleteStringCollection();

        private List<string> deviceInfo = new List<string>();
        private List<string> snids = new List<string>();
        private List<string> orgCode = new List<string>();
        private List<string> itemOrgCode = new List<string>();

        private List<MoveOrder> moveOrders = new List<MoveOrder>();


        public Equipment_AllocationMainForm()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            dataGridView1.AutoGenerateColumns = false;
            datatimepick.Visible = false;
            this.dataGridView1.Controls.Add(datatimepick);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 0)
            {
                return;
            }
            int rownum = dataGridView1.SelectedCells[0].RowIndex;
            int cellnum = dataGridView1.SelectedCells[0].ColumnIndex;
            if (cellnum == 9 && dataGridView1.Rows[rownum].Cells["date"].ReadOnly == false)
            {
                Rectangle rec = this.dataGridView1.GetCellDisplayRectangle(cellnum, rownum, true);
                datatimepick.Left = rec.Left;
                datatimepick.Top = rec.Top;
                datatimepick.Width = rec.Width;
                datatimepick.Height = rec.Height;
                datatimepick.Visible = true;
                // dtime = datatimepick.Value.ToShortDateString();
                datatimepick.CloseUp += new EventHandler(dtp_TextChange);

            }
            else
            {
                datatimepick.Visible = false;
            }

            if (cellnum == 8 && dataGridView1.Rows[rownum].Cells["after_location"].ReadOnly == false) {
                if (after_location.Items.Count ==0) {
                    MessageBox.Show("No data！");
                }
            }
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = datatimepick.Value.ToString("yyyy/MM/dd");
        }

        private void Equipment_AllocationMainForm_Load(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            LoadOrg();
            GetAddressInfoByOrgId("");
        }

        private void LoadOrg()
        {
            orgCode.Clear();
            itemOrgCode.Clear();
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

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                orgCode.Add("");
                //stringList.Add("全部");
                stringList.Add("All");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    stringList.Add(dtJson.Rows[i]["org_name"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_name"].ToString()));
                    orgCode.Add(dtJson.Rows[i]["org_code"].ToString());
                    itemOrgCode.Add(dtJson.Rows[i]["org_code"].ToString() + "|"+ dtJson.Rows[i]["org_name"].ToString());
                }
                comboBox1.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                autoComplete_factory.AddRange(stringList.ToArray());
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteCustomSource = autoComplete1;

                this.after_org.DataSource = dtJson;
                this.after_org.DisplayMember = "org_name";
                this.after_org.ValueMember = "org_code";

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDeviceInfo();
        }

        private void LoadDeviceInfo()
        {
            deviceInfo.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("orgId", orgCode.Count==0?"": orgCode[comboBox1.SelectedIndex]);
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
                deviceInfo.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    deviceInfo.Add(dtJson.Rows[i]["device_no"].ToString());
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySnidListByDeviceNo();
        }

        private void QuerySnidListByDeviceNo() {
            snids.Clear();

            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("device_no", deviceInfo.Count==0?"":deviceInfo[comboBox2.SelectedIndex]);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QuerySnidListByDeviceNo", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);

                //items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "全部"));
                items3.Add(new MulticolumnAutocompleteItem(new[] { "", "" }, "All"));
                //stringList.Add("全部");
                stringList.Add("All");
                snids.Add("");

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    snids.Add(dtJson.Rows[i]["machine_no"].ToString());
                    autoComplete1.Add(dtJson.Rows[i]["machine_no"].ToString());
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["machine_no"].ToString(), dtJson.Rows[i]["machine_no"].ToString() }, dtJson.Rows[i]["machine_no"].ToString()));
                }
                comboBox3.DataSource = items3;
                autoComplete1.AddRange(stringList.ToArray());
                autoComplete_device.AddRange(stringList.ToArray());
                comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox3.AutoCompleteCustomSource = autoComplete1;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QueryMoveRecordList();
        }

        private void QueryMoveRecordList() 
        {
            moveOrders.Clear();

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode.Count==0?"":orgCode[comboBox1.SelectedIndex]);
            p.Add("device_no", deviceInfo.Count == 0 ? "":deviceInfo[comboBox2.SelectedIndex]);
            p.Add("snid", snids.Count == 0 ? "":snids[comboBox3.SelectedIndex]);
            p.Add("start_date",Convert.ToDateTime(dateTimePicker1.Text).ToString("yyyy/MM/dd"));
            p.Add("end_date",Convert.ToDateTime(dateTimePicker2.Text).ToString("yyyy/MM/dd"));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryMoveRecordList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                //dtJson.Rows.Add("全部","");
                dataGridView1.Rows.Clear();
                if (dtJson.Rows.Count == 0)
                {
                    MessageBox.Show("No data！");
                }
                else {
                    for (int i = 0;i<dtJson.Rows.Count;i++) {
                        dataGridView1.Rows.Add(1);

                        dataGridView1.Rows[i].ReadOnly = true;
                        dataGridView1.Rows[i].Cells["order"].Value = dtJson.Rows[i]["uuid"];
                        dataGridView1.Rows[i].Cells["orgName"].Value = dtJson.Rows[i]["org_name"];
                        dataGridView1.Rows[i].Cells["device"].Value = dtJson.Rows[i]["device_name"];
                        dataGridView1.Rows[i].Cells["snid"].Value = dtJson.Rows[i]["snid"];
                        dataGridView1.Rows[i].Cells["device_type"].Value = dtJson.Rows[i]["type"];
                        dataGridView1.Rows[i].Cells["before_location"].Value = dtJson.Rows[i]["address_before"];
                        dataGridView1.Rows[i].Cells["after_org"].Value = dtJson.Rows[i]["org_code"];
                        dataGridView1.Rows[i].Cells["after_location"].Value = dtJson.Rows[i]["address_after"].ToString();
                        dataGridView1.Rows[i].Cells["status_before"].Value = dtJson.Rows[i]["status_before"].ToString();
                        dataGridView1.Rows[i].Cells["status_after"].Value = dtJson.Rows[i]["status_after"].ToString();
                        dataGridView1.Rows[i].Cells["user"].Value = dtJson.Rows[i]["insert_user"].ToString();
                        dataGridView1.Rows[i].Cells["date"].Value = DateTime.Parse(dtJson.Rows[i]["insert_date"].ToString()).ToString("yyyy/MM/dd");
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool isPost = false;
            List<int> indexs = new List<int>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCell o = dataGridView1.Rows[i].Cells["cl_cb"];
                if (o != null && o.Value != null && bool.Parse(o.Value.ToString()))
                {
                    isPost = true;
                    indexs.Add(i);
                }
            }
            if (!isPost)
            {
                MessageBox.Show("Please select the deployment order number to be deleted");
                return;
            }
            else {
               DialogResult dialogResult=  MessageBox.Show("Please confirm whether you really need to delete！", "Hint", MessageBoxButtons.OKCancel);
                if (dialogResult==DialogResult.OK) {
                    for (int i = 0; i < indexs.Count; i++)
                    {
                        int index = indexs[i];
                        moveOrders.RemoveAt(i == 0 ? index : index - i);
                    }
                    addOrderCallback(moveOrders);
                }
            }
           
            
        }

        private void todoCallback() 
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            //p.Add("orders", JArray.Parse(JsonConvert.SerializeObject(orders)));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "QueryMoveRecordList", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("Successfully deleted！");
                QueryMoveRecordList();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           AddEquipmentForm addEquipmentForm=  new AddEquipmentForm();
           addEquipmentForm.moveOrders = moveOrders;
           addEquipmentForm.onAddOrderCallback += new AddOrderCallback(addOrderCallback);
           addEquipmentForm.ShowDialog();
        }

        public void addOrderCallback(List<MoveOrder> moveOrders) {
            dataGridView1.Rows.Clear();
            this.moveOrders = moveOrders;
            for (int i = 0; i<moveOrders.Count;i++) {
                MoveOrder moveOrder = moveOrders[i];

                dataGridView1.Rows.Add(1);

                //dataGridView1.Rows[i].Cells["order"].Value = moveOrder.getWorkOrder(i+1);
                dataGridView1.Rows[i].Cells["orgName"].Value = moveOrder.OrgName;
                dataGridView1.Rows[i].Cells["device"].Value = moveOrder.DeviceName;
                dataGridView1.Rows[i].Cells["snid"].Value = moveOrder.Snid;
                dataGridView1.Rows[i].Cells["device_type"].Value = moveOrder.DeviceType;
                dataGridView1.Rows[i].Cells["before_location"].Value = moveOrder.CurPosition;
                dataGridView1.Rows[i].Cells["date"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                dataGridView1.Rows[i].Cells["after_org"].ReadOnly = false ;
                dataGridView1.Rows[i].Cells["after_location"].ReadOnly = false ;
                dataGridView1.Rows[i].Cells["date"].ReadOnly = true;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell!=null && dataGridView1.CurrentCell.Value !=null) { 
                int rownum = dataGridView1.SelectedCells[0].RowIndex;
                int cellnum = dataGridView1.SelectedCells[0].ColumnIndex;
                if (cellnum == 7 && !dataGridView1.Rows[rownum].Cells["after_org"].ReadOnly) {
                    dataGridView1.Rows[rownum].Cells["after_location"].Value = "";
                    GetAddressInfoByOrgId(dataGridView1.CurrentCell.Value.ToString());
                }
            }
        }

        private void GetAddressInfoByOrgId(string orgCode)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_code", orgCode);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "GetAddressInfoByOrgId", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(Data);
                if (dtJson.Rows.Count == 0)
                {
                    dtJson.Columns.Add("address_name", System.Type.GetType("System.String"));
                    dtJson.Columns.Add("address_code", System.Type.GetType("System.String"));
                }
               DataRow row = dtJson.NewRow();
                row["address_name"] = "";
                row["address_code"] = "";
                dtJson.Rows.Add(row);
                this.after_location.DataSource = dtJson;
                this.after_location.DisplayMember = "address_name";
                this.after_location.ValueMember = "address_code";
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            InsertMoveRecord();
        }

        private void InsertMoveRecord()
        {
            JArray jArray = new JArray();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                JObject jobject = new JObject();
                //string order = dataGridView1.Rows[i].Cells["order"].Value.ToString();
                string snid = dataGridView1.Rows[i].Cells["snid"].Value.ToString();
                
                if (dataGridView1.Rows[i].Cells["after_org"].Value ==null)
                {
                    MessageBox.Show($"Please fill in the {i+1} Item's deployment factory!");
                    return;
                }
                if (dataGridView1.Rows[i].Cells["after_location"].Value ==null)
                {
                    MessageBox.Show($"Please fill in the {i + 1} post-relocation position of the item!");
                    return;
                }
                if (dataGridView1.Rows[i].Cells["date"].Value == null)
                {
                    MessageBox.Show($"Please fill in the {i + 1} Item deployment time!");
                    return;
                }

                string afterLocation = dataGridView1.Rows[i].Cells["after_location"].Value.ToString();
                string afterOrg = dataGridView1.Rows[i].Cells["after_org"].Value.ToString();
                string date = dataGridView1.Rows[i].Cells["date"].Value.ToString();

                //jobject.Add("work_order", order);
                jobject.Add("snid", snid);
                jobject.Add("device_no", moveOrders[i].DeviceNo);
                jobject.Add("before_location", moveOrders[i].AddressCode);
                jobject.Add("org_code", afterOrg);
                jobject.Add("after_location", afterLocation);
                jobject.Add("move_date", date);
                jArray.Add(jobject);
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("data", jArray);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineManagerServer", "InsertMoveRecord", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageBox.Show("Saved successfully！");
                QueryMoveRecordList();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button6_Click(object sender, EventArgs e)
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
                NewExportExcels.ExportExcels.Export("Equipment allocation (details)", dt);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = false;
            }
        }
    }
}
