using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Base_List
{


    public partial class Main_Form : MaterialForm
    {
        public Main_Form()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            Dtgrid_devcetype.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView4.AutoGenerateColumns = false;
            dataGridView5.AutoGenerateColumns = false;
            datatimepick.Visible = false;
            this.dataGridView1.Controls.Add(datatimepick);
        }
        List<string> csList=null;
        DateTimePicker datatimepick = new DateTimePicker();
        private DataTable dtdept = null;
        AutoCompleteStringCollection autoComplete_dept = new AutoCompleteStringCollection();
        private void button7_Click(object sender, EventArgs e)
        {
            devicetypeForm df=new devicetypeForm();
            df.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            load_date();
            LoadDept();
            Is_administrators is_Administrators=new Is_administrators();
            bool isMangager = is_Administrators.IsMangager();
            if (isMangager)
            {
                button5.Enabled = true;
                button9.Visible = true;
            }
            else
            {
                button5.Enabled = false;
                button5.BackColor = SystemColors.Control;
                button9.Visible = false;
            }

        }
       
        private void LoadDept()
        {
            List<string> stringList = new List<string>();
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getalldevice_address", Program.Client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtdept = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtdept != null && dtdept.Rows.Count > 0)
                {
                    foreach (DataRow item in dtdept.Rows)
                    {
                        stringList.Add(item["ADDRESS_NAME"].ToString() + "|"+item["ADDRESS_CODE"].ToString());
                    }
                  
                }
                
                autoComplete_dept.AddRange(stringList.ToArray());           

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void load_date()
        {
            //Process
            List<AutocompleteItem> items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionDashBoardServer", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
               // dtJson.Rows.Add("全部","","","");
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));

                }
            }
            comboBox3.DataSource = items4;

            //factory
            List<AutocompleteItem> items3 = new List<AutocompleteItem>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_Miscellaneous_Server", "LoadOrg", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

                items3.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    items3.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["org_code"].ToString(), dtJson.Rows[i]["org_name"].ToString() }, dtJson.Rows[i]["org_code"].ToString() + "|" + dtJson.Rows[i]["org_name"].ToString()));


                }
                comboBox1.DataSource = items3;

            }

            //device status

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("rule_no", "1013");
            string ret3 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getalldevice_status", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);
                //dtJson.Rows.Add("全部","");

               
                this.code_name.DataSource = dtJson;
                this.code_name.DisplayMember = "code_name";
                this.code_name.ValueMember = "code_no";
                

            }
            //equipment manufacturer
            csList = new List<string>();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "Getdevice_manufacturer", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noparam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(Data);

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    csList.Add(dtJson.Rows[i]["suppliers_name"].ToString());

                }
                       
            }
        }

       
        private DataTable Getalldevicetype(string org_id,string process_no,string dev_no)
        {
            DataTable dataTable=null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("org_id", org_id);
            p.Add("rout_no", process_no);
            p.Add("dev_no", dev_no);
            p.Add("enable", "Y");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "GetallDeviceType", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Data);             
            }
            return dataTable;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {           
            DataTable dtJson = Getalldevicetype(comboBox1.Text.Split('|')[0], comboBox3.Text.Split('|')[0], comboBox2.Text.Split('|')[0]);
            Dtgrid_devcetype.DataSource = dtJson;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            textBox5.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
        }
  
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            DataTable dtJson = Getalldevicetype(comboBox1.Text.Split('|')[0], comboBox3.Text.Split('|')[0], comboBox2.Text.Split('|')[0]);
            List<AutocompleteItem> items1 = new List<AutocompleteItem>();
            items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));

            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["device_no"].ToString(), dtJson.Rows[i]["device_name"].ToString() }, dtJson.Rows[i]["device_no"].ToString() + "|" + dtJson.Rows[i]["device_name"].ToString()));


            }
            comboBox2.DataSource = items1;
        }

        

        private void Dtgrid_devcetype_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            textBox3.Text = Dtgrid_devcetype.Rows[index].Cells["Column3"].Value.ToString();//serial number
            textBox1.Text = Dtgrid_devcetype.Rows[index].Cells["Column2"].Value.ToString();//name
            textBox4.Text = Dtgrid_devcetype.Rows[index].Cells["Column26"].Value.ToString();//Section
            textBox5.Text = Dtgrid_devcetype.Rows[index].Cells["Column1"].Value.ToString();//factory
            fivetablecheck();


        }

        public void fivetablecheck()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("dev_no", textBox3.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "GetallDeviceListBydevice_no", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string Data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dataTables = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(Data);
                DataTable dt1 = dataTables["sndt"];
                DataTable dt2 = dataTables["bydt"];
                DataTable dt3 = dataTables["wxdt"];
                DataTable dt4 = dataTables["gzdt"];
                DataTable dt5 = dataTables["lpjdt"];

                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[i].Cells["ID"].Value = dt1.Rows[i]["ID"].ToString();
                    dataGridView1.Rows[i].Cells["MACHINE_NO"].Value = dt1.Rows[i]["MACHINE_NO"].ToString();
                    dataGridView1.Rows[i].Cells["TYPE"].Value = dt1.Rows[i]["TYPE"].ToString();
                    dataGridView1.Rows[i].Cells["code_name"].Value = dt1.Rows[i]["code_no"].ToString();
                    //dataGridView1.Rows[i].Cells["UDF05"].Value = DateTime.Parse(dt1.Rows[i]["UDF05"].ToString()).ToString("yyyy年MM月");
                    dataGridView1.Rows[i].Cells["UDF05"].Value = DateTime.Parse(dt1.Rows[i]["UDF05"].ToString()).ToString("yyyy/MM/dd");
                    dataGridView1.Rows[i].Cells["ADDRESS"].Value = dt1.Rows[i]["ADDRESS"].ToString();
                    dataGridView1.Rows[i].Cells["brand"].Value = dt1.Rows[i]["brand"].ToString();
                    ((DataGridViewComboBoxCell)dataGridView1.Rows[i].Cells["ENABLE"]).Value = (dt1.Rows[i]["ENABLE"].ToString());
                    dataGridView1.Rows[i].ReadOnly = true;
                }
                dataGridView2.Rows.Clear();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[i].Cells["BY_NO"].Value = dt2.Rows[i]["BY_NO"].ToString();
                    dataGridView2.Rows[i].Cells["BODY_PART"].Value = dt2.Rows[i]["BODY_PART"].ToString();
                    dataGridView2.Rows[i].Cells["ITEM"].Value = dt2.Rows[i]["ITEM"].ToString();
                    dataGridView2.Rows[i].Cells["STANDARD"].Value = dt2.Rows[i]["STANDARD"].ToString();
                    dataGridView2.Rows[i].Cells["LEVEL_NAME"].Value = dt2.Rows[i]["LEVEL_NAME"].ToString();
                    ((DataGridViewComboBoxCell)dataGridView2.Rows[i].Cells["ENABLE2"]).Value = (dt2.Rows[i]["ENABLE"].ToString());
                    dataGridView2.Rows[i].ReadOnly = true;
                }
                dataGridView3.Rows.Clear();
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    dataGridView3.Rows.Add(1);
                    dataGridView3.Rows[i].Cells["GZ_NO"].Value = dt4.Rows[i]["GZ_NO"].ToString();
                    dataGridView3.Rows[i].Cells["GZ_NAME"].Value = dt4.Rows[i]["GZ_NAME"].ToString();
                    ((DataGridViewComboBoxCell)dataGridView3.Rows[i].Cells["ENABLE3"]).Value = (dt4.Rows[i]["ENABLE"].ToString());
                    dataGridView3.Rows[i].ReadOnly = true;
                }
                dataGridView4.Rows.Clear();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    dataGridView4.Rows.Add(1);
                    dataGridView4.Rows[i].Cells["WX_NO"].Value = dt3.Rows[i]["WX_NO"].ToString();
                    dataGridView4.Rows[i].Cells["WX_NAME"].Value = dt3.Rows[i]["WX_NAME"].ToString();
                    ((DataGridViewComboBoxCell)dataGridView4.Rows[i].Cells["ENABLE4"]).Value = (dt3.Rows[i]["ENABLE"].ToString());
                    dataGridView4.Rows[i].ReadOnly = true;
                }
                dataGridView5.Rows.Clear();
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    dataGridView5.Rows.Add(1);
                    dataGridView5.Rows[i].Cells["PJ_NO"].Value = dt5.Rows[i]["PJ_NO"].ToString();
                    dataGridView5.Rows[i].Cells["PJ_NAME"].Value = dt5.Rows[i]["PJ_NAME"].ToString();
                    dataGridView5.Rows[i].Cells["UNIT"].Value = dt5.Rows[i]["UNIT"].ToString();
                    ((DataGridViewComboBoxCell)dataGridView5.Rows[i].Cells["ENABLE5"]).Value = (dt5.Rows[i]["ENABLE"].ToString());
                    dataGridView5.Rows[i].ReadOnly = true;
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please select device type");
                return;
            }
          
                eding(dataGridView1,0);
          
                eding(dataGridView2,0);
                dataGridView2.Columns[0].ReadOnly = true;
           
                eding(dataGridView3,0);
                dataGridView3.Columns[0].ReadOnly = true;
          
                eding(dataGridView4,0);
                dataGridView4.Columns[0].ReadOnly = true;
           
                eding(dataGridView5,0);
                dataGridView5.Columns[0].ReadOnly = true;
                SJeMES_Control_Library.MessageHelper.ShowOK(this, "Editable state is turned on。");
            
        }
        private void eding(DataGridView dg,int bjzt)
        {
            if (bjzt == 0)
            {
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    dg.Rows[i].ReadOnly = false;
                }
            }
            else
            {
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    dg.Rows[i].ReadOnly = true;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                dataGridView1.Rows.Insert(0,1);
                dataGridView1.Rows[0].ReadOnly = false;
                dataGridView1.Rows[0].Cells["ENABLE"].Value = "Y";
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                dataGridView2.Rows.Insert(0, 1);
                dataGridView2.Rows[0].ReadOnly = false;
                dataGridView2.Columns[0].ReadOnly = true;
                dataGridView2.Rows[0].Cells["ENABLE2"].Value = "Y";
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                dataGridView3.Rows.Insert(0, 1);
                dataGridView3.Rows[0].ReadOnly = false;
                dataGridView3.Columns[0].ReadOnly = true;
                dataGridView3.Rows[0].Cells["ENABLE3"].Value = "Y";
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                dataGridView4.Rows.Insert(0, 1);
                dataGridView4.Rows[0].ReadOnly = false;
                dataGridView4.Columns[0].ReadOnly = true;
                dataGridView4.Rows[0].Cells["ENABLE4"].Value = "Y";
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                dataGridView5.Rows.Insert(0, 1);
                dataGridView5.Rows[0].ReadOnly = false;
                dataGridView5.Columns[0].ReadOnly = true;
                dataGridView5.Rows[0].Cells["ENABLE5"].Value = "Y";
            }
        }
        private void baocun(DataTable dt, int index)
        {
            if (dt.Rows.Count > 0)
            {
                try
                {
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("datamodel", dt);
                    p.Add("index", index);
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "updateDeviceDatails", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Saved successfully！");
                        //dataGridView1.ReadOnly = true;                    
                        //textBox3.Text = string.Empty;
                    }
                    else
                    {
                        if (Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString() == "ORA-00001:Violation of the unique constraint (MES00.MES030M_A)")
                        {
                            SJeMES_Control_Library.MessageHelper.ShowWarning(this, "The inserted SN code is duplicated, please check whether the SN code is duplicated！");
                        }
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                   
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please select device type！");
                return;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("BY_NO", typeof(string));
                dt.Columns.Add("BODY_PART", typeof(string));
                dt.Columns.Add("ITEM", typeof(string));
                dt.Columns.Add("STANDARD", typeof(string));
                dt.Columns.Add("LEVEL_NO", typeof(string));
                dt.Columns.Add("LEVEL_NAME", typeof(string));
                dt.Columns.Add("ENABLE", typeof(string));
                dt.Columns.Add("DEVICE_NO", typeof(string));
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();
                    if (dataGridView2.Rows[i].Cells["BODY_PART"].Value == null)
                    {
                        MessageBox.Show("Maintenance Standard cannot be empty！");
                        return;
                    }
                    if (dataGridView2.Rows[i].Cells["ITEM"].Value == null)
                    {
                        MessageBox.Show("Maintenance items cannot be empty！");
                        return;
                    }
                    if (dataGridView2.Rows[i].Cells["STANDARD"].Value == null)
                    {
                        MessageBox.Show("Maintenance Standard cannot be empty！");
                        return;
                    }
                    if (dataGridView2.Rows[i].Cells["LEVEL_NAME"].Value == null)
                    {
                        MessageBox.Show("Maintenance level cannot be empty！");
                        return;
                    }
                    if (dataGridView2.Rows[i].Cells["ENABLE2"].Value == null)
                    {
                        MessageBox.Show("Please choose whether to enable！");
                        return;
                    }

                    dr["BY_NO"] = dataGridView2.Rows[i].Cells["BY_NO"].Value;
                    dr["BODY_PART"] = dataGridView2.Rows[i].Cells["BODY_PART"].Value.ToString().Replace("\"", "“");
                    dr["ITEM"] = dataGridView2.Rows[i].Cells["ITEM"].Value.ToString().Replace("\"", "“"); 
                    dr["STANDARD"] = dataGridView2.Rows[i].Cells["STANDARD"].Value.ToString().Replace("\"", "“"); 
                    dr["LEVEL_NO"] = dataGridView2.Rows[i].Cells["LEVEL_NAME"].Value.ToString()== "First_Level_Maintenance" ? "01":"02";
                    dr["LEVEL_NAME"] = dataGridView2.Rows[i].Cells["LEVEL_NAME"].Value;
                    dr["ENABLE"] = dataGridView2.Rows[i].Cells["ENABLE2"].Value;
                    dr["DEVICE_NO"] = textBox3.Text;
                    dt.Rows.Add(dr);
                }
                baocun(dt,1);
                eding(dataGridView2, 1);
            }
            else if (tabControl1.SelectedIndex == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("MACHINE_NO", typeof(string));
                dt.Columns.Add("TYPE", typeof(string));
                dt.Columns.Add("device_status", typeof(string));
                dt.Columns.Add("UDF05", typeof(string));
                dt.Columns.Add("ADDRESS", typeof(string));
                dt.Columns.Add("brand", typeof(string));
                dt.Columns.Add("ENABLE", typeof(string));
                dt.Columns.Add("DEVICE_NO", typeof(string));

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();
                    if (dataGridView1.Rows[i].Cells["MACHINE_NO"].Value == null)
                    {
                        MessageBox.Show("SN code cannot be empty！");
                        return;
                    }
                    if (dataGridView1.Rows[i].Cells["ADDRESS"].Value == null)
                    {
                        MessageBox.Show("Device location cannot be empty！");
                        return;
                    }
                    if (dataGridView1.Rows[i].Cells["code_name"].Value == null)
                    {
                        MessageBox.Show("Device status cannot be empty！");
                        return;
                    }
                    if (dataGridView1.Rows[i].Cells["ENABLE"].Value == null)
                    {
                        MessageBox.Show("Please choose whether to enable！");
                        return;
                    }

                    dr["ID"] = dataGridView1.Rows[i].Cells["ID"].Value;
                    dr["MACHINE_NO"] = dataGridView1.Rows[i].Cells["MACHINE_NO"].Value;
                    dr["TYPE"] = dataGridView1.Rows[i].Cells["TYPE"].Value;
                    dr["device_status"] = dataGridView1.Rows[i].Cells["CODE_NAME"].Value;
                    dr["UDF05"] = DateTime.Parse(dataGridView1.Rows[i].Cells["UDF05"].Value.ToString()).ToString("yyyy/MM/dd");
                    dr["ADDRESS"] = dataGridView1.Rows[i].Cells["ADDRESS"].Value.ToString().Split('|')[0];
                    dr["brand"] = dataGridView1.Rows[i].Cells["brand"].Value.ToString().Split('|')[0];
                    dr["ENABLE"] = dataGridView1.Rows[i].Cells["ENABLE"].Value;
                    dr["DEVICE_NO"] = textBox3.Text;

                    dt.Rows.Add(dr);
                }
                baocun(dt, 0);
                //eding(dataGridView1, 1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("GZ_NO", typeof(string));
                dt.Columns.Add("GZ_NAME", typeof(string));
                dt.Columns.Add("ENABLE", typeof(string));
                dt.Columns.Add("DEVICE_NO", typeof(string));

                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();
                    if (dataGridView3.Rows[i].Cells["GZ_NAME"].Value == null)
                    {
                        MessageBox.Show("Failure name cannot be empty！");
                        return;
                    }                   
                    if (dataGridView3.Rows[i].Cells["ENABLE3"].Value == null)
                    {
                        MessageBox.Show("Please choose whether to enable！");
                        return;
                    }

                    dr["GZ_NO"] = dataGridView3.Rows[i].Cells["GZ_NO"].Value;
                    dr["GZ_NAME"] = dataGridView3.Rows[i].Cells["GZ_NAME"].Value;
                    dr["ENABLE"] = dataGridView3.Rows[i].Cells["ENABLE3"].Value;
                    dr["DEVICE_NO"] = textBox3.Text;

                    dt.Rows.Add(dr);
                }
                baocun(dt, 2);
               // eding(dataGridView3, 1);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("WX_NO", typeof(string));
                dt.Columns.Add("WX_NAME", typeof(string));
                dt.Columns.Add("ENABLE", typeof(string));
                dt.Columns.Add("DEVICE_NO", typeof(string));

                for (int i = 0; i < dataGridView4.Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();
                    if (dataGridView4.Rows[i].Cells["WX_NAME"].Value == null)
                    {
                        MessageBox.Show("Maintenance name cannot be empty！");
                        return;
                    }
                    if (dataGridView4.Rows[i].Cells["ENABLE4"].Value == null)
                    {
                        MessageBox.Show("Please choose whether to enable！");
                        return;
                    }

                    dr["WX_NO"] = dataGridView4.Rows[i].Cells["WX_NO"].Value;
                    dr["WX_NAME"] = dataGridView4.Rows[i].Cells["WX_NAME"].Value;
                    dr["ENABLE"] = dataGridView4.Rows[i].Cells["ENABLE4"].Value;
                    dr["DEVICE_NO"] = textBox3.Text;

                    dt.Rows.Add(dr);
                }
                baocun(dt, 3);
               // eding(dataGridView4, 1);
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PJ_NO", typeof(string));
                dt.Columns.Add("PJ_NAME", typeof(string));
                dt.Columns.Add("UNIT", typeof(string));
                dt.Columns.Add("ENABLE", typeof(string));
                dt.Columns.Add("DEVICE_NO", typeof(string));

                for (int i = 0; i < dataGridView5.Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();
                    if (dataGridView5.Rows[i].Cells["PJ_NAME"].Value == null)
                    {
                        MessageBox.Show("Parts name cannot be empty！");
                        return;
                    }
                    if (dataGridView5.Rows[i].Cells["UNIT"].Value == null)
                    {
                        MessageBox.Show("Parts unit cannot be empty！");
                        return;
                    }
                    if (dataGridView5.Rows[i].Cells["ENABLE5"].Value == null)
                    {
                        MessageBox.Show("Please choose whether to enable！");
                        return;
                    }

                    dr["PJ_NO"] = dataGridView5.Rows[i].Cells["PJ_NO"].Value;
                    dr["PJ_NAME"] = dataGridView5.Rows[i].Cells["PJ_NAME"].Value;
                    dr["UNIT"] = dataGridView5.Rows[i].Cells["UNIT"].Value;
                    dr["ENABLE"] = dataGridView5.Rows[i].Cells["ENABLE5"].Value;
                    dr["DEVICE_NO"] = textBox3.Text;

                    dt.Rows.Add(dr);
                }
                baocun(dt, 4);
               // eding(dataGridView5, 1);
            }
            fivetablecheck();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                deletedetail("0", dataGridView1);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                deletedetail("1", dataGridView2);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                deletedetail("2", dataGridView3);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                deletedetail("3", dataGridView4);
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                deletedetail("4", dataGridView5);
            }
        }
        private void deletedetail(string num,DataGridView dg)
        {
            int index = dg.SelectedCells[0].RowIndex;
            if (MessageBox.Show("Are you sure to delete the except id number" + (dg.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            if (dg.Rows[index].Cells[0].Value == null)
            {
                dg.Rows.RemoveAt(index);
                return;
            }
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("uuid", dg.Rows[index].Cells[0].Value);
                p.Add("index", num);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineBasicdateServer", "deleteDeviceDetails", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully deleted！");
                    dg.Rows.RemoveAt(index);
                }
                else
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, json);
                    // MessageBox.Show(json);
                }
            }
            catch (Exception ex)
            {

                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var tbec = e.Control as DataGridViewTextBoxEditingControl;
            TextBox dk = e.Control as TextBox;
            if (tbec == null)
            {
                return;
            }
            if (dataGridView1.CurrentCell.OwningColumn.Name == "brand")
            {
                AutoCompleteStringCollection autoComplete1 = new AutoCompleteStringCollection();
                autoComplete1.AddRange(csList.ToArray());
                dk.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                dk.AutoCompleteSource = AutoCompleteSource.CustomSource;
                dk.AutoCompleteCustomSource = autoComplete1;
            }
            if (dataGridView1.CurrentCell.OwningColumn.Name == "ADDRESS")
            {

                dk.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                dk.AutoCompleteSource = AutoCompleteSource.CustomSource;
                dk.AutoCompleteCustomSource = autoComplete_dept;
            }
           
        }
        private void dtp_TextChange(object sender, EventArgs e)
        {
            //dataGridView1.CurrentCell.Value = datatimepick.Value.ToString("yyyy年MM月");
            dataGridView1.CurrentCell.Value = datatimepick.Value.ToString("yyyy/MM/dd");
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 0)
            {
                return;
            }
            int rownum = dataGridView1.SelectedCells[0].RowIndex;
            int cellnum = dataGridView1.SelectedCells[0].ColumnIndex;
            // string dtime = "";
            // datatimepick = new DateTimePicker();
            if (cellnum == 5 && dataGridView1.Rows[rownum].Cells["UDF05"].ReadOnly == false)
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
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int index = dataGridView1.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure to remove the id number" + (dataGridView1.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                dataGridView1.Rows.RemoveAt(index);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                int index = dataGridView2.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure to remove the id number" + (dataGridView2.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                dataGridView2.Rows.RemoveAt(index);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                int index = dataGridView3.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure to remove the id number" + (dataGridView3.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                dataGridView3.Rows.RemoveAt(index);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                int index = dataGridView4.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure to remove the id number" + (dataGridView4.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                dataGridView4.Rows.RemoveAt(index);
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                int index = dataGridView5.SelectedCells[0].RowIndex;
                if (MessageBox.Show("Are you sure to remove the id number" + (dataGridView5.Rows[index].Cells[0].Value), "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
                dataGridView5.Rows.RemoveAt(index);
            }
        }

        
    }
}
