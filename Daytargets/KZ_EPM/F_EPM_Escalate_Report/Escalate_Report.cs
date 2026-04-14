using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using System.Net;

namespace F_EPM_Escalate_Report
{
    public partial class Escalate_Report : Form
    {
        AutoCompleteStringCollection Autodata;
        public Boolean isTitle = false;
        IList<object[]> data = null;
        DataTable dt;

        public string org, Brand, UDF05, Device_Status, Device_status_no;

        public Escalate_Report()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }
        private void Escalate_Report_Load(object sender, EventArgs e)
        {
            LoadModel();
            autocompleteMenu1.SetAutocompleteMenu(FA, autocompleteMenu1);
            // no smaller than design time size
            this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            // no larger than screen size
            this.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;           
        }

        private void Pressbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string Device_FA = FA.Text.ToUpper();
                dt = new DataTable();
                Dictionary<string, string> kk = new Dictionary<string, string>();
                kk.Add("MachineNO", Device_FA.ToString());
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "GetmaintenanceListBySNid",
                    Program.Client.UserToken, JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);
                    if (dt.Rows.Count < 0)
                    {
                        string msg = "No Data!";
                        MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    else
                    {
                        DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                        DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                        DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                        Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                        Faultreporttxt.Text = dt.Rows[0]["by_user"].ToString();
                        org = dt.Rows[0]["ORG_ID"].ToString();
                        Brand = dt.Rows[0]["BRAND"].ToString();
                        UDF05 = dt.Rows[0]["UDF05"].ToString();
                        Device_Status = dt.Rows[0]["device_status"].ToString();
                        Device_status_no = dt.Rows[0]["device_status_no"].ToString();
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Please Enter Correct FA number");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox1.SelectedItem.ToString() != "Select an option")
            {
                // Enable the submit button
                Submitbtn.Enabled = true;
            }
            else
            {
                // Disable the submit button
                Submitbtn.Enabled = false;
            }
        }
        public void FA_KeyDown(object sender, KeyEventArgs e)
       {
            try
            {

            
            if (e.KeyCode == Keys.Enter)
            {
                string Device_FA = FA.Text.ToUpper();
                dt = new DataTable();
                Dictionary<string, string> kk = new Dictionary<string, string>();
                kk.Add("MachineNO", Device_FA.ToString());
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "GetmaintenanceListBySNid",
                    Program.Client.UserToken, JsonConvert.SerializeObject(kk));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dt = JsonConvert.DeserializeObject<DataTable>(json);
                    if (dt.Rows.Count < 0)
                    {
                        string msg = "No Data!";
                        MessageHelper.ShowErr(this, msg);
                        return;
                    }
                    else
                    {
                        DeviceFAtxt.Text = dt.Rows[0]["MACHINE_NO"].ToString();
                        DeviceNametxt.Text = dt.Rows[0]["MACHINE_NAME"].ToString();
                        DeviceModeltxt.Text = dt.Rows[0]["TYPE"].ToString();
                        Storagepostxt.Text = dt.Rows[0]["ADDRESS"].ToString();
                        Faultreporttxt.Text = dt.Rows[0]["by_user"].ToString();
                        org = dt.Rows[0]["ORG_ID"].ToString();
                        Brand = dt.Rows[0]["BRAND"].ToString();
                        UDF05 = dt.Rows[0]["UDF05"].ToString();
                        Device_Status = dt.Rows[0]["device_status"].ToString();
                        Device_status_no = dt.Rows[0]["device_status_no"].ToString();
                    }
                }
                else
                {
                    //MessageBox.Show("Error");
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Please Enter Correct FA number");
            }

        }
        public void Submitbtn_Click(object sender, EventArgs e)
        {
            try
            {


                if (FA.Text == "")
                {
                    MessageHelper.ShowErr(this, "Please Select FA Number");
                    return;
                }
                if (DeviceFAtxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "FA Number Should not be Empty");
                    return;
                }
                if (DeviceNametxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "DeviceName Should not be Empty");
                    return;
                }
                if (Storagepostxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "Location Should not be Empty");
                    return;
                }
                if (Faultreporttxt.Text == "")
                {
                    MessageHelper.ShowErr(this, "Reporting Person Should not be Empty");
                    return;
                }
                if (comboBox1.SelectedItem == null)
                {
                    MessageHelper.ShowErr(this, "Please Select Fault report Content");
                    return;
                }
                else
                {
                    string Device_FA = DeviceFAtxt.Text.ToUpper();
                    string Devicename = DeviceNametxt.Text.ToUpper();
                    string Devicemodel = DeviceModeltxt.Text.ToUpper();
                    string Devicelocation = Storagepostxt.Text.ToUpper();
                    string Userreporter = Faultreporttxt.Text.ToUpper();
                    string Reportcontent = comboBox1.Text.ToUpper();
                    string Escalate = Escalationtext.Text.ToUpper();

                    Dictionary<string, object> SS = new Dictionary<string, object>();
                    // Assuming BZtable is a DataTable
                    DataTable BZtable = new DataTable();

                    // Adding columns to the DataTable if not already defined
                    BZtable.Columns.Add("ORG_ID", typeof(string));
                    BZtable.Columns.Add("BRAND", typeof(string));
                    BZtable.Columns.Add("UDF05", typeof(string));
                    BZtable.Columns.Add("device_status", typeof(string));
                    BZtable.Columns.Add("device_status_no", typeof(string));
                    BZtable.Columns.Add("MACHINE_NO", typeof(string));
                    BZtable.Columns.Add("Devicename", typeof(string));
                    BZtable.Columns.Add("Devicemodel", typeof(string));
                    BZtable.Columns.Add("Devicelocation", typeof(string));
                    BZtable.Columns.Add("Userreporter", typeof(string));
                    BZtable.Columns.Add("Escalationper", typeof(string));

                    BZtable.Columns.Add("TXT", typeof(string));

                    // Creating a new DataRow and adding values
                    DataRow row = BZtable.NewRow();
                    row["ORG_ID"] = org.ToString();
                    row["BRAND"] = Brand.ToString();
                    row["UDF05"] = UDF05.ToString();
                    row["device_status"] = Device_Status.ToString();
                    row["device_status_no"] = Device_status_no.ToString();
                    row["MACHINE_NO"] = Device_FA.ToString();
                    row["Devicename"] = Devicename.ToString();
                    row["Devicemodel"] = Devicemodel.ToString();
                    row["Devicelocation"] = Devicelocation.ToString();
                    row["Userreporter"] = Userreporter.ToString();
                    row["Escalationper"] = Escalate.ToString();
                    row["TXT"] = Reportcontent.ToString();



                    // Adding the DataRow to the DataTable
                    BZtable.Rows.Add(row);
                    SS.Add("BZtable", BZtable);
                    //string BZtable = JsonConvert.SerializeObject(SS);
                    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "InitiateReportTrouble",
                        Program.Client.UserToken, JsonConvert.SerializeObject(SS));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        // dt = JsonConvert.DeserializeObject<DataTable>(json);
                        if (json.Equals(""))
                        {
                            MessageHelper.ShowSuccess(this, "Dear:" + Userreporter + "Succesfully report a Issue");
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "Failed Reporting issue");
                            return;
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
                FA.Text = "";
                DeviceFAtxt.Text = "";
                DeviceNametxt.Text = "";
                DeviceModeltxt.Text = "";
                Storagepostxt.Text = "";
                Faultreporttxt.Text = "";
                DeviceFAtxt.Text = "";
                Escalationtext.Text = "";
                comboBox1.SelectedValue = null;
            }
            catch (WebException webEx)
            {
               
                Console.WriteLine("WebException: " + webEx.Message);
                MessageHelper.ShowErr(this, "A web error occurred. Please try again.");
               
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("An error occurred: " + ex.Message);
                MessageHelper.ShowErr(this, "An error occurred. Not Found this FA Number"); 
            }


        }
        public void LoadModel()
        {

            FA.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            FA.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Autodata = new AutoCompleteStringCollection();
            dt = new DataTable();
            Dictionary<string, string> kk = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_EPMAPI", "KZ_EPMAPI.Controllers.MachineUniappServer", "AutoMachineload",
                Program.Client.UserToken, JsonConvert.SerializeObject(kk));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("No data Found!");
                    return;
                }
                else
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["MACHINE_NO"].ToString() }, dt.Rows[i]["MACHINE_NO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }
    }
}
