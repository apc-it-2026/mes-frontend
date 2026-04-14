using AutocompleteMenuNS;
using MaterialSkin.Controls;
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

namespace Employee_Swapping
{
    public partial class Employee_Swapping : MaterialForm
    {
        public Employee_Swapping()
        {
            InitializeComponent();
        }

        private void Employee_Swapping_Load(object sender, EventArgs e)
        {
            GetDept();
            GetEmployee(txtdept.Text);
            LoadProd_Line(txtdept.Text);
        }

        private void GetDept()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    txtdept.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    txtdept2.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                }
                else
                {
                    txtdept.Text = "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        public void LoadProd_Line(string Current_Dept)
        {
            txt_supdept.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_supdept.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("Current_Dept", Current_Dept);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                 "KZ_RTDMAPI.Controllers.GeneralServer", "GetMESDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };

                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
        }
        private void GetEmployee(string Dept)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Dept", Dept);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "GetEmployee", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtdept.Text == txt_supdept.Text)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Both Departments are same");
                return;
            }
            if (string.IsNullOrEmpty(txt_reason.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Swap reason");
                return;
            }
            if (!CheckSupportDept())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "In Correct Support Department");
                return;
            }
            DataTable dt1 = new DataTable();
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt1.Columns.Add(column.Name);


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool isSelected = Convert.ToBoolean(row.Cells["select"].Value);
                    if (isSelected)
                    {
                        DataRow dRow = dt1.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt1.Rows.Add(dRow);
                    }
                }
            }

            if(dt1.Rows.Count ==0 || string.IsNullOrEmpty(txt_supdept.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Both Employee List and Sup Dept");
                return;
            }

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Data", dt1);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "CheckSwapEmployeeScanned", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
            }
            else
            {
                Dictionary<string, object> p1 = new Dictionary<string, object>();
                p1.Add("Data", dt1);
                p1.Add("SupDept", txt_supdept.Text);
                p1.Add("SwapReason", txt_reason.Text);
                string retdata1 = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "SwapEmployee", Program.client.UserToken, JsonConvert.SerializeObject(p1));
                ResultObject ret1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata1);
                if (ret1.IsSuccess)
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, ret1.ErrMsg);
                    GetEmployee(txtdept.Text);
                    txt_supdept.Text = "";
                    txt_reason.Text = "";
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ret1.ErrMsg);
                    GetEmployee(txtdept.Text);
                    txt_supdept.Text = "";
                    txt_reason.Text = "";
                }
            }
            
            
           
           

        }

        public bool CheckSupportDept()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Sup_Dept", txt_supdept.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "CheckSupportDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            return ret.IsSuccess;
        }
        public bool CheckSwapEmployeeScanned(DataTable dt)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Data", dt);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "CheckSwapEmployeeScanned", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            return ret.IsSuccess;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            GetEmpSwapData(txtdept2.Text, dateTimePicker1.Text);
            if (dateTimePicker1.Value.Date == DateTime.Today)
            {
                dataGridView2.Columns["delete"].Visible = true;
            }
            else
            {
                dataGridView2.Columns["delete"].Visible = false;
            }

        }

        private void GetEmpSwapData(string Dept,string Swap_Date)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Dept", Dept);
            p.Add("Swap_Date", Swap_Date);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "GetEmpSwapData", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dtJson1;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
            }
            else
            {
                dataGridView2.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "delete")
                {
                    string Emp_No = dataGridView2.Rows[e.RowIndex].Cells["EMP_NO"].Value.ToString();
                    if (Check_Employee_Scan_Status(Emp_No))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Employee Already scanned"); 
                        return;
                    }
                    DialogResult result = MessageBox.Show($@"Are you sure you want to delete the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {

                        try
                        {
                            string SwapDate = dataGridView2.Rows[e.RowIndex].Cells["SWAP_DATE"].Value.ToString();
                            string EmpNo = dataGridView2.Rows[e.RowIndex].Cells["EMP_NO"].Value.ToString();
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SwapDate", SwapDate);
                            p.Add("EmpNo", EmpNo);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "DeleteSwapData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Deleted Successfully");
                                GetEmpSwapData(txtdept2.Text, dateTimePicker1.Text);
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                                GetEmpSwapData(txtdept2.Text, dateTimePicker1.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }
                    }
                }
               
            }
        }

        public bool Check_Employee_Scan_Status(string EmpNo)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("EmpNo", EmpNo);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI", "KZ_RTDMAPI.Controllers.GeneralServer", "Check_Employee_Scan_Status", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            return ret.IsSuccess;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            GetEmployee(txtdept.Text);
        }
    }
}
