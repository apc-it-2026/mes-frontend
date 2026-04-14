using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.WebAPI;
using SJeMES_Framework.Common;
using SJeMES_Control_Library;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;
using OfficeOpenXml.Style;
using NewExportExcels;
using Microsoft.Office.Interop.Excel;
using LicenseContext = OfficeOpenXml.LicenseContext;
using NPOI.SS.UserModel;
using System.Net.Sockets;
using System.Net;
using NPOI.SS.Formula.Functions;
using static OfficeOpenXml.ExcelErrorValue;
using Newtonsoft.Json;

namespace PlanningSchedule
{ 

    public partial class PlanningSchdule : Form
    {
        public class ComboboxEntry 
        { 
            public string Code { get; set; }
            public string Name { get; set; }
        }  

        private object originalValue = null; 
        private string CompanyCode = null; 
        private string PlantCode = null; 
        private string ProcessCode = null; 
        private string updEndDate = null; 
        private string updstatDate = null; 

        private string RCompanyCode = null;
        private string RPlantCode = null;
        private string RProcessCode = null;
        private string RLineCode = null;

        public PlanningSchdule() 
        {
            InitializeComponent();
            StyleComboBox(comboBox1);
            StyleComboBox(comboBox2);
            StyleComboBox(comboBox3);
            StyleComboBox(comboBox4); 

            // clear filter values 
            StyleRoundedButton(button7 , Color.FromArgb(149, 165, 166)); 
            // checking button 
            StyleRoundedButton(button6 , Color.FromArgb(241, 196, 15));
            //export excel button 
            StyleRoundedButton(button5 , Color.FromArgb(230, 126, 34) );
            // remove grid row button 
            StyleRoundedButton(button4, Color.FromArgb(155, 89, 182));
            // search button 
            StyleRoundedButton(button3 , Color.FromArgb(52, 152, 219));
            // save or update button 
            StyleRoundedButton(button2, Color.FromArgb(46, 204, 113)); 
            // delete button 
            StyleRoundedButton(button1, Color.FromArgb(231, 76, 60));
            // check target button 
            StyleRoundedButton(button13, Color.FromArgb(52, 73, 94));
            // Reschedule button 
            StyleRoundedButton(button10, Color.FromArgb(241, 196, 15)); 

            LoadOrg();
            LoadPlants(CompanyCode);
            LoadProcess();
            comboBox3.SelectedValue = "";
            comboBox4.SelectedValue = "";
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.UserAddedRow += dataGridView1_UserAddedRow;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing ; 
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter; 
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.Refresh();
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.CellValueChanged += dataGridView4_CellValueChanged;
            dataGridView2.CurrentCellDirtyStateChanged += dataGridView4_CurrentCellDirtyStateChanged;
            textBox2.Multiline = true;
            textBox2.AcceptsReturn = true;
            textBox2.AcceptsTab = false;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.WordWrap = false;
            textBox2.Font = new Font("Consolas", 11, FontStyle.Regular);
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox2.Padding = new Padding(5);
            textBox2.MinimumSize = new Size(300, 100);
            //premika
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox3.Multiline = true;
            textBox3.AcceptsReturn = true;
            textBox3.AcceptsTab = false;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.WordWrap = false;
            textBox3.Font = new Font("Consolas", 11, FontStyle.Regular);
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox3.Padding = new Padding(5);
            textBox3.MinimumSize = new Size(300, 50);
            DesignDataGridView(dataGridView1);
          //  PlanningScheduleTab_SelectedIndexChanged += PlanningScheduleTab_SelectedIndexChanged; 
        }
        private void StyleComboBox(ComboBox comboBox) 
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Prevent typing, select only
            comboBox.FlatStyle = FlatStyle.Flat;                 // Flat modern look
            comboBox.BackColor = Color.White;                    // Background color
            comboBox.ForeColor = Color.Black;                    // Text color
            comboBox.Font = new Font("Segoe UI", 10, FontStyle.Regular); // Font style
            comboBox.Margin = new Padding(2);
            comboBox.Cursor = Cursors.Hand;                      // Change cursor to hand
            comboBox.Width = 200;                                // Optional width

            // Optional: Add a border (simulate since WinForms ComboBox doesn’t support border color)
            comboBox.Region = new Region(comboBox.ClientRectangle);

            comboBox.DrawItem += (s, e) =>
            {
                e.DrawBackground();
                if (e.Index >= 0)
                {
                    string text = comboBox.Items[e.Index].ToString();
                    Brush brush = new SolidBrush(e.ForeColor);
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
                e.DrawFocusRectangle();
            }; 
        }
        private void StyleRoundedButton(Button btn , Color color ) 
        {
            btn.BackColor = color ;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Rounded corners using GraphicsPath
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 10;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            btn.Region = new Region(path);
        }

        private void LoadLines(string OrgId,String Plant,String Process)
        {
            if (string.IsNullOrEmpty(OrgId))
            {
                MessageHelper.ShowErr(this, "Please select Factory!");
            }
            else if (string.IsNullOrEmpty(Plant))
            {
                MessageHelper.ShowErr(this, "Please select Plant!");
            }
            else if (string.IsNullOrEmpty(Process))
            {
                MessageHelper.ShowErr(this, "Please select Process!");
            }
            else
            {
                try
                {
                    List<ComboboxEntry> Lines = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("OrgId", OrgId);
                    Data.Add("Plant", Plant);
                    Data.Add("Process", Process);
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "LoadLines", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                        Lines.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            Lines.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["department_code"].ToString(), Name = dtJson.Rows[i]["department_name"].ToString() });
                        }

                        comboBox2.DataSource = Lines;
                        comboBox2.DisplayMember = "Name";
                        comboBox2.ValueMember = "Code";
                        List<string> selectedCodes = new List<string>();
                        comboBox2.SelectedIndexChanged += (s, e) =>
                        {
                            if (comboBox2.SelectedValue != null)
                            {
                                string selectedCode = comboBox2.SelectedValue.ToString().Trim();

                                if (string.IsNullOrEmpty(selectedCode))
                                {
                                    selectedCodes.Clear();
                                    textBox3.Clear();
                                    return;
                                }

                                if (!selectedCodes.Contains(selectedCode))
                                {
                                    selectedCodes.Add(selectedCode);
                                }

                                textBox3.Text = string.Join(",", selectedCodes);
                            }
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, "Error: " + ex.Message);
                }
            }
        }

        private void LoadProcess()
        {
            List<ComboboxEntry> Process = new List<ComboboxEntry> { };

            Process.Add(new ComboboxEntry() { Code = "", Name = "" });
            Process.Add(new ComboboxEntry() { Code = "C", Name = "Cutting" });
            Process.Add(new ComboboxEntry() { Code = "S", Name = "Stitching" });
            Process.Add(new ComboboxEntry() { Code = "L", Name = "Assembly" });

            comboBox4.DataSource = Process;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "Code";
            comboBox4.SelectedIndexChanged += (s, e) =>
            {
                if (comboBox4.SelectedIndex >= 0 && comboBox4.SelectedValue != null)
                {
                    string selectedCode = comboBox4.SelectedValue.ToString();
                    string selectedName = comboBox4.Text;
                    ProcessCode = selectedCode;
                    LoadLines(CompanyCode,PlantCode,ProcessCode);

                }
            };
        }
        private void LoadOrg() 
        {
            try
            {
                List<ComboboxEntry> Companies = new List<ComboboxEntry> { };
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadOrg", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
          
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata); 
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"]) 
                   && result.ContainsKey("RetData") && result["RetData"] != "") 
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
               
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        Companies.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                    }
                    comboBox1.DataSource = Companies;
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "Code";
                   
                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                        string defaultOrgCode = comboBox1.SelectedValue.ToString();
                        CompanyCode = defaultOrgCode;

                       
                    }

                    comboBox1.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedValue != null)
                        {
                            string selectedCode = comboBox1.SelectedValue.ToString();
                            string selectedName = comboBox1.Text;
                            CompanyCode = selectedCode;
                            LoadPlants(CompanyCode);
                        }
                    };


                }

            }
            catch(Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }
        private void LoadPlants(string orgId) 
        {
            try
            {
                List<ComboboxEntry> Plants = new List<ComboboxEntry> { };
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("OrgId", orgId);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadPlant", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                   && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    Plants.Add(new ComboboxEntry() { Code = "", Name = "" });
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        Plants.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["CODE"].ToString(), Name = dtJson.Rows[i]["NAME"].ToString() });
                    }
                    comboBox3.DataSource = Plants;
                    comboBox3.DisplayMember = "Name";
                    comboBox3.ValueMember = "Code";

                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox3.SelectedIndex = 0;
                        string defaultPlantCode = comboBox3.SelectedValue.ToString();
                        PlantCode = defaultPlantCode;
                    }

                    comboBox3.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox3.SelectedIndex >= 0 && comboBox3.SelectedValue != null)
                        {
                            string selectedCode = comboBox3.SelectedValue.ToString();
                            string selectedName = comboBox3.Text;
                            PlantCode = selectedCode;
                            LoadLines(CompanyCode,PlantCode,ProcessCode);
                        }
                    };
                }
            }
            catch(Exception ex) {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        } 
        string orderbyvalue = "CRD";

        private void GetOrderByValue()
        {
            try
            {
                Form popup = new Form();
                popup.Text = "Select Order By";
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.Size = new Size(250, 150);
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;

                CheckBox chkCRD = new CheckBox() { Text = "CRD", Location = new Point(30, 20), Checked = true };
                CheckBox chkPSDD = new CheckBox() { Text = "PSDD", Location = new Point(30, 50) };

                Button btnOk = new Button() { Text = "OK", Location = new Point(70, 85), DialogResult = DialogResult.OK };

                chkCRD.CheckedChanged += (s, e) =>
                {
                    if (chkCRD.Checked)
                    {
                        chkPSDD.Checked = false;
                        orderbyvalue = "CRD";
                    }
                };

                chkPSDD.CheckedChanged += (s, e) =>
                {
                    if (chkPSDD.Checked)
                    {
                        chkCRD.Checked = false;
                        orderbyvalue = "PSDD";
                    }
                };

                popup.Controls.Add(chkCRD);
                popup.Controls.Add(chkPSDD);
                popup.Controls.Add(btnOk);
                popup.AcceptButton = btnOk;

                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void SearchFunction()
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    ["startdate"] = dateTimePicker5.Text,
                    ["enddate"] = dateTimePicker6.Text,
                    ["seid"] = textBox2.Text,
                    ["orgid"] = CompanyCode,
                    ["plant"] = PlantCode,  
                    ["process"] = ProcessCode
                }; 

                var input = textBox3.Text.Trim();
                var lines = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(part => part.Length > 4 ? part.Substring(4) : "")
                                 .Where(s => !string.IsNullOrEmpty(s))
                                 .ToArray();

                data["line"] = string.Join(",", lines);
                if (string.IsNullOrEmpty(data["plant"]?.ToString()) || string.IsNullOrEmpty(data["process"]?.ToString()))
                {
                    MessageHelper.ShowErr(this, "Please select Plant and Process first.");
                    return; // (optional) stop further execution
                }
                GetOrderByValue();
                data["orderbyvalue"] = orderbyvalue; 
                string response = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "PlanningTest",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (result == null || !result.ContainsKey("IsSuccess"))
                {
                    MessageHelper.ShowErr(this, "Invalid or empty response from server.");
                    return;
                } 

                bool isSuccess = Convert.ToBoolean(result["IsSuccess"]);


                if (isSuccess && result.ContainsKey("RetData") && !string.IsNullOrEmpty(result["RetData"]?.ToString()))
                { 
                    string json = result["RetData"].ToString();
                    DataTable dt = JsonHelper.GetDataTableByJson(json);
                    dataGridView1.AutoGenerateColumns = false;

                    if (dt.Columns.Contains("CRD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["CRD"]?.ToString(), out DateTime dateValue))
                            {
                                row["CRD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    if (dt.Columns.Contains("LPD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["LPD"]?.ToString(), out DateTime dateValue))
                            {
                                row["LPD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    if (dt.Columns.Contains("PSDD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["PSDD"]?.ToString(), out DateTime dateValue))
                            {
                                row["PSDD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    dataGridView1.DataSource = dt; 
                    // DesignDataGridView(dataGridView1);  
                   // dataGridView1.Columns["UPDSTATVAL"].ReadOnly = true; 
                   // dataGridView1.Columns["UNIQUE_ID"].ReadOnly = true; 
                    dataGridView1.AllowUserToAddRows = true; 
                }

                else if (result.ContainsKey("ErrMsg") && !string.IsNullOrEmpty(result["ErrMsg"]?.ToString()))
                {
                    string apiErrorMessage = result["ErrMsg"].ToString();
                    MessageHelper.ShowErr(this, $"API Error: {apiErrorMessage}\nPlease contact the IT department.");
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageHelper.ShowErr(this, "No data found matching the search criteria."); 
                }
            }
            catch (Exception ex)
            {

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                }
                MessageHelper.ShowErr(this, $"An error occurred. Please contact IT Department.\nDetails: {ex.Message}");
            } 
        }
   

        private void button3_Click(object sender, EventArgs e)
        { 
            dataGridView1.AllowUserToAddRows = false;
             
            bool hasStatus = false; 

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["UPDSTATVAL"].Value != null)
                {
                    string value = row.Cells["UPDSTATVAL"].Value.ToString().Trim();
                    if (value == "1" || value == "2")
                    {
                        hasStatus = true;
                        break;
                    }
                }
                if (row.Cells["UNIQUE_ID"].Value == "0000")
                {
                    string value = row.Cells["UNIQUE_ID"].Value.ToString().Trim();
                    if (value == "0000")
                    {
                        hasStatus = true;
                        break;
                    }
                } 
            }

            if (hasStatus)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to save and continue?",
                    "Confirmation",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.OK) 
                {

                    SaveUpdateUsemethod();
                    SearchFunction();
                     
                } 
                else
                {
                    SearchFunction();
                    
                }
            } 
            else
            {
                SearchFunction();
               
            } 
        }  
        private void SaveAndUpdateData(string ipaddress , string reason )  
        { 
            try  
            { 
                DataTable dt = new DataTable(); 
                 
                foreach (DataGridViewColumn col in dataGridView1.Columns)  
                {
                    dt.Columns.Add(col.Name); 
                } 
                 
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var statusValue = row.Cells["UPDSTATVAL"].Value?.ToString()?.Trim();
                        var idstatus = row.Cells["UNIQUE_ID"].Value?.ToString()?.Trim(); 
                         
                        if (string.IsNullOrEmpty(statusValue) && (statusValue != "1" && statusValue != "2" && idstatus != "0000") )    
                            continue;

                        DataRow dr = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dr[cell.OwningColumn.Name] = cell.Value ?? DBNull.Value;
                        } 

                        dt.Rows.Add(dr); 
                    } 
                } 
                string jsonData = JsonConvert.SerializeObject(dt); 

                Dictionary<string, object> data = new Dictionary<string, object> { { "planning", dt } }; 

                data.Add("ipaddress", ipaddress);
                /*  data.Add("reason", reason);
                  data.Add("OrgId", CompanyCode); 
                  data.Add("Plant", PlantCode); 
                  data.Add("Process", ProcessCode); 
                  data.Add("Lines", textBox3.Text); 
                  data.Add("startdate", dateTimePicker6.Text); 
                  data.Add("enddate", dateTimePicker5.Text); */
                data.Add("Process", ProcessCode);
                data.Add("assemblystatus" , checkBox1.Checked );   
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT", 
                     "KZ_CUTMNT.Controllers.PlanningController",
                     "SaveOrUpdatePlanningData", Program.client.UserToken, JsonConvert.SerializeObject(data)); 
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(result["IsSuccess"]))
                {
                    // comment by manhar 

                    /*// Convert RetData (which is a DataTable in JSON form) back to DataTable
                    DataTable dts = JsonConvert.DeserializeObject<DataTable>(result["RetData"].ToString());
                    var successOrders = new List<string>();
                    var failedOrders = new List<string>();
                    var GreaterOrders = new List<string>();
                    var LessOrders = new List<string>();
                    var FulfillOrders = new List<string>();
                    foreach (DataRow row in dts.Rows)
                    {
                        string soNo = row["SalesOrder"].ToString();
                        bool status = Convert.ToBoolean(row["Status"]);
                        string Message = row["Message"].ToString();
                        if (status==true && Message=="Success")
                        {
                            successOrders.Add(soNo);
                        }
                        else if(status==false && Message =="Greater")
                        {
                            GreaterOrders.Add(soNo);
                        }
                        else if(status==false && Message == "Less")
                        {
                            LessOrders.Add(soNo);
                        }
                        else if (status == false && Message == "Fulfill")
                        {
                            FulfillOrders.Add(soNo);
                        }

                        else
                        {
                            failedOrders.Add(soNo);
                        }
                            
                    }
                    // Show messages based on results
                    if (successOrders.Count > 0)
                    {
                        string SuccessList = string.Join(", ", successOrders);
                        MessageBox.Show($"data save/update successfully!.\n" +
                                        $"Success Sales Orders:\n{SuccessList}",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        //MessageBox.Show("All data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchFunction();
                    }
                    if (GreaterOrders.Count > 0)
                    {
                        string GreaterList = string.Join(", ", GreaterOrders);
                        MessageBox.Show($"failed to save/update.\n" +
                                        $"Schedule Qty Greater than Actual SO Qty.\n" +
                                        $"Greater Qty Sales Orders:\n{GreaterList}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    if (LessOrders.Count > 0)
                    {
                        string LessList = string.Join(", ", LessOrders);
                        MessageBox.Show($"failed to save/update.\n" +
                                        $"Schedule Qty Less than Actual SO Qty.\n" +
                                        $"Less Qty Sales Orders:\n{LessList}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    if (FulfillOrders.Count > 0)
                    {
                        string FullfillList = string.Join(", ", FulfillOrders);
                        MessageBox.Show($"failed to save/update.\n" +
                                        $"Schedule Qty is full filled for this process.\n" +
                                        $"Less Qty Sales Orders:\n{FullfillList}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    if (failedOrders.Count > 0)
                    {
                        string failedList = string.Join(", ", failedOrders);
                        MessageBox.Show($"failed to save/update.\n" +
                                        $"Failed Sales Orders:\n{failedList}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }*/

                    // by manohar 
                    DataTable dts = JsonConvert.DeserializeObject<DataTable>(result["RetData"].ToString());

                    if (dts != null && dts.Rows.Count > 0)
                    { 
                        List<string> successMessages = new List<string>();
                        List<string> warningMessages = new List<string>();
                        List<string> errorMessages = new List<string>(); 

                        foreach (DataRow row in dts.Rows)
                        {
                            string salesOrder = row["SalesOrder"]?.ToString() ?? "";
                            bool status = row["Status"] != DBNull.Value && Convert.ToBoolean(row["Status"]);
                            string message = row["Message"]?.ToString() ?? "";

                            // Handle based on message type
                            if (status)
                            {
                                /*  if (message.Contains("Inserted"))
                                  {
                                      MessageBox.Show($"✅ SO {salesOrder}: {message}", "Insert Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      SearchFunction();
                                  }
                                  else if (message.Contains("Updated")) 
                                  {
                                      MessageBox.Show($"✅ SO {salesOrder}: {message}", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      SearchFunction();
                                  }
                                  else
                                  {
                                      MessageBox.Show($"✅ SO {salesOrder}: {message}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                      SearchFunction(); 
                                  }*/
                                if (message.IndexOf("Inserted", StringComparison.OrdinalIgnoreCase) > 0)  
                                    successMessages.Add($"✅ SO {salesOrder}: {message}");
                                else if (message.IndexOf("Updated", StringComparison.OrdinalIgnoreCase) > 0)
                                    successMessages.Add($"✅ SO {salesOrder}: {message}");
                                else
                                    successMessages.Add($"✅ SO {salesOrder}: {message}"); 
                            }
                            else
                            {
                                /*  if (message.Contains("exceeds"))
                                  {
                                      MessageBox.Show($"⚠️ SO {salesOrder}: {message}", "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                  }
                                  else if (message.Contains("LESS THAN"))
                                  {
                                      MessageBox.Show($"⚠️ SO {salesOrder}: {message}", "Quantity Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                  }
                                  else if (message.Contains("Unique ID not found"))
                                  {
                                      MessageBox.Show($"❌ SO {salesOrder}: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                  }
                                  else
                                  {
                                      MessageBox.Show($"❌ SO {salesOrder}: {message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                  }*/
                                if (message.IndexOf("exceeds", StringComparison.OrdinalIgnoreCase) > 0)
                                    warningMessages.Add($"⚠️ SO {salesOrder}: {message}");
                                else if (message.IndexOf("LESS THAN", StringComparison.OrdinalIgnoreCase) > 0)
                                    warningMessages.Add($"⚠️ SO {salesOrder}: {message}");
                                else if (message.IndexOf("Unique ID not found", StringComparison.OrdinalIgnoreCase) > 0)
                                    errorMessages.Add($"❌ SO {salesOrder}: {message}");
                                else
                                    errorMessages.Add($"❌ SO {salesOrder}: {message}");
                            }
                        }
                        string summary = "";
                        if (successMessages.Count > 0)
                            summary += $"✅ SUCCESS ({successMessages.Count})\n{string.Join("\n", successMessages)}\n\n";
                        if (warningMessages.Count > 0)
                            summary += $"⚠️ WARNINGS ({warningMessages.Count})\n{string.Join("\n", warningMessages)}\n\n";
                        if (errorMessages.Count > 0)
                            summary += $"❌ ERRORS ({errorMessages.Count})\n{string.Join("\n", errorMessages)}\n";
                        if (!string.IsNullOrEmpty(summary))
                        {
                            MessageBox.Show(summary, "Bulk Save/Update Summary", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // ✅ Refresh only once at the end
                        SearchFunction(); 
                    }
                    else
                    {
                        MessageHelper.ShowErr(this ,  "No response received from server."); 
                    }



                }
                else
                {
                    MessageHelper.ShowErr(this ,  "Error occurred while saving data. Please try again!" ); 
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             
        }

        private string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork) // Only IPv4
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        public static string ShowInputDialog(string title, string promptText)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;

            textBox.Width = 250;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 50, 372, 20);
            buttonOk.SetBounds(228, 90, 75, 23);
            buttonCancel.SetBounds(309, 90, 75, 23);

            label.AutoSize = true;
            form.ClientSize = new Size(396, 130);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            return form.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {   
                SaveUpdateUsemethod();    
            }  
            else 
            { 
                MessageHelper.ShowErr(this ,  "No Data have for Update");   
            }    
        } 


        /*  private void SaveUpdateUsemethod() 
          {
              string ipAddress = GetLocalIPAddress();
              // string reason = "";
              DialogResult result = MessageBox.Show(
                      "Do you want to save and continue?",
                      "Confirmation",
                      MessageBoxButtons.OKCancel,
                      MessageBoxIcon.Warning
                  );

              if (result == DialogResult.OK)
              {
                  string reason = ShowInputDialog("Reason Required", "Please enter the reason:");

                  if (string.IsNullOrWhiteSpace(reason))
                  {
                      MessageHelper.ShowErr(this , "Save cancelled. Reason is required." ); 
                      return;
                  }

                  SaveAndUpdateData(ipAddress, reason);
              }
          }*/
        private void SaveUpdateUsemethod()
        {
            try
            {
                string ipAddress = GetLocalIPAddress();

                // 🟡 Step 1: Confirm user intent
                if (!ConfirmAction("Do you want to save and continue?", "⚠️ Confirmation"))
                    return;

                // 🟢 Step 2: Ask for reason
                string reason = PromptReason("Reason Required", "Please enter the reason for saving:");

                if (string.IsNullOrWhiteSpace(reason))
                {
                    MessageHelper.ShowErr(this, "❌ Save cancelled. Reason is required.");
                    return;
                } 

                // 🟢 Step 3: Perform save/update
                SaveAndUpdateData(ipAddress, reason);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, $"⚠️ Unexpected error: {ex.Message}");
            }
        }


        // ✅ Method 1: Confirmation Box
        private bool ConfirmAction(string message, string title)
        {
            DialogResult result = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            return result == DialogResult.OK;
        }

        // ✅ Method 2: Reason Input Box
        private string PromptReason(string title, string prompt)
        {
            using (Form inputForm = new Form())
            {
                inputForm.Text = title;
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.ClientSize = new Size(350, 150);
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                Label lblPrompt = new Label() { Text = prompt, Left = 15, Top = 15, Width = 320 };
                TextBox txtReason = new TextBox() { Left = 15, Top = 45, Width = 320 };
                Button btnOk = new Button() { Text = "OK", Left = 170, Width = 75, Top = 90, DialogResult = DialogResult.OK };
                Button btnCancel = new Button() { Text = "Cancel", Left = 260, Width = 75, Top = 90, DialogResult = DialogResult.Cancel };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtReason, btnOk, btnCancel });
                inputForm.AcceptButton = btnOk;
                inputForm.CancelButton = btnCancel;

                DialogResult dialogResult = inputForm.ShowDialog();

                return dialogResult == DialogResult.OK ? txtReason.Text.Trim() : string.Empty;
            }
        }

        private void DeleteScheduleData(string ipaddress ) 
        {
            DataTable selectedRowsTable = new DataTable();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                selectedRowsTable.Columns.Add(col.Name, typeof(string));
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataRow newRow = selectedRowsTable.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    newRow[cell.OwningColumn.Name] = cell.Value ?? DBNull.Value;
                }
                selectedRowsTable.Rows.Add(newRow);
            }
             bool hasStatus = selectedRowsTable.AsEnumerable()
            .Any(r => r["UPDSTATVAL"] != DBNull.Value &&
             (r["UPDSTATVAL"].ToString() == "1" || r["UPDSTATVAL"].ToString() == "2"));

            if (hasStatus) 
            {
                MessageHelper.ShowErr(this, "Some selected rows have needs update or Insert Please check those once ");
            } 
            else 
            { 
                string jsonData = JsonConvert.SerializeObject(selectedRowsTable);
                Dictionary<string, object> data = new Dictionary<string, object> { { "planning", selectedRowsTable } }; 
                data.Add("ipaddress", ipaddress);
                data.Add("Process", comboBox4.SelectedValue.ToString());
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                     "KZ_CUTMNT.Controllers.PlanningController",
                     "DeletePlanningData", Program.client.UserToken, JsonConvert.SerializeObject(data));
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (Convert.ToBoolean(result["IsSuccess"]))
                {
                    MessageHelper.ShowOK(this , $"{selectedRowsTable.Rows.Count} row Data Deleted successfully!" ); 
                    SearchFunction();
                }
                else
                {
                    MessageHelper.ShowErr(this, "Failed to delete data!"); 
                }
            }   
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                string ipAddress = GetLocalIPAddress();
                // string reason = "";
                DialogResult result = MessageBox.Show(
                        "Do you want to Delete and continue?",
                        "Confirmation",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning
                    );

                if (result == DialogResult.OK)
                { 
                    DeleteScheduleData(ipAddress); 
                }
                  
            } 
            else
            {
                MessageHelper.ShowErr(this, "Please select at least one row to delete.");
            }
        }   

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)  
        {
            int statusIndex = dataGridView1.Columns["UPDSTATVAL"].Index; 

            int newRowIndex = e.Row.Index - 1;

            if (newRowIndex >= 0)
            {
                DataGridViewRow newRow = dataGridView1.Rows[newRowIndex]; 
                newRow.Cells[statusIndex].Value = 2;
            } 
        } 

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        } 

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            originalValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        } 

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex < 0) return;
            if (!dataGridView1.Columns.Contains("UPDSTATVAL") || !dataGridView1.Columns.Contains("UNIQUE_ID") ) 
            {
                MessageHelper.ShowErr(this, "UpdateStatus column not found!");
                return;
            }
            int statusIndex = dataGridView1.Columns["UPDSTATVAL"].Index;
            int uniqueIdIndex = dataGridView1.Columns["UNIQUE_ID"].Index;

            if (e.ColumnIndex == statusIndex) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            if (row.IsNewRow) return;
            string uniqueId = row.Cells[uniqueIdIndex].Value?.ToString()?.Trim() ?? "";

            if (uniqueId != "0000")
            {
                object cellVal = row.Cells[statusIndex].Value;
                int status = (cellVal == null || cellVal == DBNull.Value) ? 0 : Convert.ToInt32(cellVal);

                if (status == 0)
                {
                    row.Cells[statusIndex].Value = 1; // mark as modified
                }
            }
        }  

        private void PlanningSchdule_Load(object sender, EventArgs e)
        {
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.KeyPreview = true;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int statusIndex = dataGridView1.Columns["UPDSTATVAL"].Index;
            int idstatusIndex = dataGridView1.Columns["UNIQUE_ID"].Index; 
            int status = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[statusIndex].Value);
            string idstatus = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[idstatusIndex].Value); 
            Color rowColor = Color.White;
            if (status == 1) rowColor = Color.LightYellow; 
            else if (status == 2 ) rowColor = Color.LightGreen;  
            else if(idstatus == "0000") rowColor= Color.LightBlue; 
            

            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = rowColor;  
        }  

        private void button4_Click(object sender, EventArgs e) 
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataTable selectedRowsTable = new DataTable();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    selectedRowsTable.Columns.Add(col.Name, typeof(string));
                }
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    DataRow newRow = selectedRowsTable.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        newRow[cell.OwningColumn.Name] = cell.Value ?? DBNull.Value;
                    }
                    selectedRowsTable.Rows.Add(newRow);
                }


                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
                MessageHelper.ShowOK(this, $"{selectedRowsTable.Rows.Count} row Data Removed successfully!"); 
                
                
            }
            else
            {
                MessageHelper.ShowErr(this, "Please select at least one row to delete.");
            }
        }

        private bool enterPressed = false; 
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Enter key is pressed
            if (e.KeyCode == Keys.Enter && !enterPressed ) 
            {
                e.Handled = true; // Prevent the default Enter key behavior
                e.SuppressKeyPress = true;
                enterPressed = true; 
                if (dataGridView1.CurrentRow != null)
                {
                    int selectedIndex = dataGridView1.CurrentRow.Index;

                    if (dataGridView1.DataSource is DataTable dt)
                    {
                        DataRow selectedDataRow = ((DataRowView)dataGridView1.Rows[selectedIndex].DataBoundItem).Row;
                        DataRow newRow = dt.NewRow();
                        newRow.ItemArray = (object[])selectedDataRow.ItemArray.Clone();
                        if (dt.Columns.Contains("UNIQUE_ID"))
                            newRow["UNIQUE_ID"] = "0000";  

                        dt.Rows.InsertAt(newRow, selectedIndex + 1);
                    }
                    else
                    {
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedIndex];
                        DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();

                        for (int i = 0; i < selectedRow.Cells.Count; i++)
                        {
                            newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                        }

                        dataGridView1.Rows.Insert(selectedIndex + 1, newRow);
                    }

                    // Create a new row with the same values
                   // DataGridViewRow selectedRow = dataGridView1.Rows[selectedIndex];
                   // DataGridViewRow newRow = (DataGridViewRow)selectedRow.Clone();

                   /* for (int i = 0; i < selectedRow.Cells.Count; i++)
                    {
                        newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                    }*/

                    // Insert the duplicated row below the selected one
                    // dataGridView1.Rows.Insert(selectedIndex + 1, newRow);

                    // Optionally, select the newly inserted row
                    dataGridView1.ClearSelection();
                    // dataGridView1.Rows[selectedIndex + 1].Selected = true; 
                    if (selectedIndex + 1 < dataGridView1.Rows.Count)
                        dataGridView1.Rows[selectedIndex + 1].Selected = true; 
                }
                 
                    
            } 
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        { 
            if (e.Control is TextBox tb)
            {
                // Remove existing event handler (to prevent multiple attachments)
                tb.KeyPress -= TextBox_KeyPressToUpper;
                tb.KeyPress += TextBox_KeyPressToUpper;
            }  
        }  

        private void TextBox_KeyPressToUpper(object sender, KeyPressEventArgs e)
        {
            // Convert character to uppercase
            e.KeyChar = char.ToUpper(e.KeyChar);
        } 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            { 
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.BeginEdit(true);
                 
                /*if (e.ColumnIndex == 3)
                { 
                    string newValue = dateTimePicker5.Value.ToString("yyyy/MM/dd") + "-" +
                                      dateTimePicker6.Value.ToString("yyyy/MM/dd");
                     
                    var currentValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                     
                    if (currentValue != newValue)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = newValue;
                    }
                    else
                    { 
                        Console.WriteLine("No change detected — value remains the same.");
                    }
                }*/ 
            }  
        } 
     
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // MessageHelper.ShowOK(this, "KeyPress"); 
        }

      /*  private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) 
            {
                e.Handled = true;
                int colIndex = dataGridView1.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                if(rowIndex 
                    != 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[colIndex, rowIndex - 1];
                    //  dataGridView1.Rows[rowIndex] = dataGridView1.Rows[rowIndex - 1 ];  
                    
                } 
                string cellValue = dataGridView1.CurrentCell.Value?.ToString() ?? string.Empty;
                if (dataGridView1.Columns[colIndex].Name == "SEIDVAL" && dataGridView1.CurrentCell != null)
                {
                    if (cellValue != "")
                    {
                         Dictionary<string, object> Data = new Dictionary<string, object>();
                        Data.Add("seid", cellValue);
                        string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                         "KZ_CUTMNT.Controllers.PlanningController",
                         "GettingValuesBySO", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                        ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);

                        var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                        if (result.ContainsKey("RetData")) 
                        {
                            var retDataJson = result["RetData"].ToString();
                            if(retDataJson != "")
                            {
                                var retDataArray = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDataJson);

                                if (retDataArray.Count > 0)
                                {
                                    var firstRowData = retDataArray[0];  

                                    // Bind each field to the DataGridView row
                                    foreach (var kvp in firstRowData)
                                    {
                                        if (dataGridView1.Columns.Contains(kvp.Key))
                                        {
                                            if (rowIndex != 0)
                                            {
                                                dataGridView1.Rows[rowIndex - 1].Cells[kvp.Key].Value = kvp.Value?.ToString();
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[rowIndex].Cells[kvp.Key].Value = kvp.Value?.ToString();
                                            }

                                        }
                                    }
                                }
                                else
                                {
                                    MessageHelper.ShowErr(this, "No data found for the entered SEID (or) SE_QTY is May be ZERO.Please Check Once!");
                                }
                            }
                            else
                            {
                                MessageHelper.ShowErr(this, "No data found for the entered SEID.");
                            }

                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "Invalid API response.");
                        } 

                    } 
                    else
                    {
                        MessageHelper.ShowErr(this, "Please Fill SO");  
                    }
                }

            }
        }   

       */

        private void ExportExcel() 
        {
            try
            {
                if (dataGridView1.Rows.Count == 1)
                {
                    MessageHelper.ShowErr(this, "No data available to export!");
                    return;
                } 

                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                         
                        using (ExcelPackage excel = new ExcelPackage())
                        { 
                            ExcelWorksheet ws = excel.Workbook.Worksheets.Add("Data");
                             
                            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                            {
                                ws.Cells[1, i + 1].Value = dataGridView1.Columns[i].HeaderText;
                                ws.Cells[1, i + 1].Style.Font.Bold = true;
                                ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                ws.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }
                            
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    ws.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                                }
                            }
                             
                            ws.Cells[ws.Dimension.Address].AutoFitColumns();

                            ExcelWorksheet ws2 = excel.Workbook.Worksheets.Add("Targets");

                            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                            {
                                ws2.Cells[1, i + 1].Value = dataGridView2.Columns[i].HeaderText;
                                ws2.Cells[1, i + 1].Style.Font.Bold = true;
                                ws2.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws2.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                ws2.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            }

                            for (int i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                                {
                                    ws2.Cells[i + 2, j + 1].Value = dataGridView2.Rows[i].Cells[j].Value?.ToString();
                                }
                            }

                            ws2.Cells[ws2.Dimension.Address].AutoFitColumns();

                            FileInfo fi = new FileInfo(sfd.FileName);
                            excel.SaveAs(fi);

                            MessageHelper.ShowOK(this, "Excel file saved successfully!");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }

        private void NewExportExelMethod()
        {
            try
            {
                if (dataGridView1.Rows.Count == 1)
                {
                    MessageHelper.ShowErr(this, "No data available to export!");
                    return;
                }
                 
            }

            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            } 
        }
        
        private void button5_Click(object sender, EventArgs e) 
        {
            ExportExcel(); 
        } 

       
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // string article = textBox4.Text.Trim(); 

               /* if (string.IsNullOrEmpty(article))
                {
                    MessageBox.Show("Please enter an article before pressing Enter!",
                                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.SuppressKeyPress = true;
                    return;
                }*/

                try
                { 
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                  //  Data.Add("article", article);
                     
                    string retdata = WebAPIHelper.Post(
                        Program.client.APIURL,
                        "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "GettingValuesBySO",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(Data)
                    );
                     
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                    if (result.ContainsKey("RetData"))
                    {
                        var retDataJson = result["RetData"].ToString();

                        if (!string.IsNullOrEmpty(retDataJson))
                        {
                            var retDataArray = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDataJson);

                            if (retDataArray.Count > 0)
                            {  
                                foreach (var row in retDataArray)
                                {
                                    int newRowIdx = dataGridView1.Rows.Add();

                                    foreach (var kvp in row)
                                    {
                                        if (dataGridView1.Columns.Contains(kvp.Key))
                                        {
                                            dataGridView1.Rows[newRowIdx].Cells[kvp.Key].Value = kvp.Value?.ToString();
                                        }
                                    }
                                     
                                    int statusIndex = dataGridView1.Columns["UPDSTATVAL"].Index;
                                    dataGridView1.Rows[newRowIdx].Cells[statusIndex].Value = 2;
                                }
                                 
                                dataGridView1.Refresh();
                                 
                            }
                            else
                            {
                                MessageHelper.ShowErr(this, "No data found for the entered SEID.");
                            }
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, "No data found for the entered SEID.");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "Invalid API response.");
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, "Error: " + ex.Message);
                }
                 
                e.SuppressKeyPress = true;
            } 
        } 
         
        private void BindDataToGrid2(List<Dictionary<string, object>> data) 
        {

            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            DateTime startDate = dateTimePicker6.Value;
            DateTime endDate = dateTimePicker5.Value;

            dataGridView2.Columns.Add("PROD_LINE", "PROD_LINE");

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Sunday)
                {
                    string colName = date.ToString("MM/dd");
                    dataGridView2.Columns.Add(colName, colName);
                }
            }

            dataGridView2.Columns.Add("TOTAL", "TOTAL");

            foreach (var row in data)
            {
                string prodLine = row["PROD_LINE"].ToString();
                decimal approxValue = Convert.ToDecimal(row["APROXIMATION_VALUE"]);

                List<object> values = new List<object>
                {
                   prodLine
                };

                decimal total = 0;

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        values.Add(approxValue);
                        total += approxValue;
                    }

                }

                values.Add(total);

                dataGridView2.Rows.Add(values.ToArray());
            }
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
         
        private void button9_Click(object sender, EventArgs e)
        { 
            try  
            {  
                Dictionary<string, object> Data = new Dictionary<string, object>
                {
                    { "startdate", dateTimePicker6.Value.ToString("yyyy-MM-dd") },
                    { "enddate", dateTimePicker5.Value.ToString("yyyy-MM-dd") },
                    { "line", textBox3.Text }
                 };
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                         "KZ_CUTMNT.Controllers.PlanningController",
                         "LineDayWiseTarget", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (response.ContainsKey("RetData"))
                {
                    var retDataJson = response["RetData"].ToString();
                    var retDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDataJson);

                    if (retDataList != null && retDataList.Count > 0)
                    {
                        BindDataToGrid(retDataList);
                    }
                    else
                    {
                        MessageHelper.ShowErr(this , "No data found for the selected dates." ); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this , "Error: " + ex.Message); 
            } 
             
        }

        private void BindDataToGrid(List<Dictionary<string, object>> data)
        { 

            dataGridView2.Columns.Clear();
            dataGridView2.Rows.Clear();

            DateTime startDate = dateTimePicker6.Value;
            DateTime endDate = dateTimePicker5.Value;

            dataGridView2.Columns.Add("PROD_LINE", "PROD_LINE");

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Sunday)
                {
                    string colName = date.ToString("MM/dd");
                    dataGridView2.Columns.Add(colName, colName);
                }
            } 

            dataGridView2.Columns.Add("TOTAL", "TOTAL");

            foreach (var row in data)
            {
                string prodLine = row["PROD_LINE"].ToString();
                decimal approxValue = Convert.ToDecimal(row["APROXIMATION_VALUE"]);

                List<object> values = new List<object>
                {
                   prodLine
                };

                decimal total = 0;

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        values.Add(approxValue);
                        total += approxValue;
                    }

                }

                values.Add(total);

                dataGridView2.Rows.Add(values.ToArray());
            }
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        } 
                
        private void button11_Click(object sender, EventArgs e)
        {
            GEtStiticDaywiseTargets(); 
        }  
        private void SaveTargetData() 
        {
            try
            {
                // Convert DataGridView to DataTable
                DataTable dt = new DataTable();

                // Create columns in DataTable from DataGridView
                foreach (DataGridViewColumn col in dataGridView2.Columns)
                {
                    dt.Columns.Add(col.Name, typeof(string)); // keep string, you can change type if needed
                }

                // Add rows
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow) // skip empty last row
                    {
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value?.ToString() ?? "";
                        }
                        dt.Rows.Add(dRow);
                    }
                }

                // Serialize DataTable to JSON
                string jsonData = JsonConvert.SerializeObject(dt);

                // Wrap into dictionary if needed

                Dictionary<string, object> Data = new Dictionary<string, object> 
                                                    {
                                                        { "GridData", jsonData } , {"stitching" , checkBox1.Checked } 
                                                    };    


                // Send to API  
                string retdata = WebAPIHelper.Post(Program.client.APIURL, 
                                "KZ_CUTMNT",
                                "KZ_CUTMNT.Controllers.PlanningController", 
                                "SaveAndUpdateTargetData",   
                                Program.client.UserToken,
                                JsonConvert.SerializeObject(Data));

                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);  
             //    MessageHelper.ShowOK(this, "Data sent successfully!");
                MessageBox.Show(this, "Data Sent Successfully"); 
            } 
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            } 
        } 

        private void button12_Click(object sender, EventArgs e)
        {
            SaveTargetData();
            GEtStiticDaywiseTargets(); 
        } 

        private void GEtStiticDaywiseTargets() 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    MessageHelper.ShowErr(this, "Please Enter Line");
                    return;
                }
                var input = textBox3.Text.Trim();

                var lines = input
                              .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(part => part.Length > 4 ? part.Substring(4) : "")
                              .Where(s => !string.IsNullOrEmpty(s))
                              .ToList(); 

             /*   if (checkBox1.Checked == true)
                {
                    var stitchingLines = lines
                        .Select(l => l.Replace("C", "S"))
                        .ToList();
                     
                    lines.AddRange(stitchingLines); 
                } */ 

                var Data = new Dictionary<string, object>
                {
                    { "line", lines.ToArray() }  
                }; 
              


                string retdata = WebAPIHelper.Post( 
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "StiticLineDayWiseTarget", 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                );

                var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata); 

                List<Dictionary<string, object>> retDataList = null;
                if (response != null && response.ContainsKey("RetData"))
                {
                    var retDataJson = response["RetData"].ToString();
                    retDataList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDataJson);
                }

                // If API returned nothing, create fallback row for the requested line so grid shows zeros.
                if (retDataList == null || retDataList.Count == 0)
                {
                    retDataList = new List<Dictionary<string, object>>
                    {
                        new Dictionary<string, object> { { "LINE", textBox3.Text } }
                    };
                }  

                BindStaticDataToGrid(retDataList);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        } 

        private void BindStaticDataToGrid(List<Dictionary<string, object>> data) 
        {  
            dataGridView2.Columns.Clear(); 
            dataGridView2.Rows.Clear(); 

            DateTime startDate = dateTimePicker5.Value.Date;
            DateTime endDate = dateTimePicker6.Value.Date;

            if (startDate > endDate)
            {
                MessageHelper.ShowErr(this, "Start date cannot be after end date.");
                return;
            }

            // Build list of dates (excluding Sundays)
            List<DateTime> dateList = new List<DateTime>();
            for (DateTime d = startDate; d <= endDate; d = d.AddDays(1))
            {
                if (d.DayOfWeek != DayOfWeek.Sunday)
                    dateList.Add(d);
            }
            var Data = new Dictionary<string, object>
                {
                    { "sdate", dateTimePicker5.Text } ,
                    {"edate" , dateTimePicker6.Text} ,  
                };
            string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "GetTargetHolidays" , 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                );

            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
            List<string> holidayStrings = new List<string>();
            if (response != null && response.ContainsKey("RetData"))
            { 
                // Assuming your API returns something like { "Result": ["2025/11/14", "2025/11/15"] }
                holidayStrings = JsonConvert.DeserializeObject<List<string>>(response["RetData"].ToString());  
            } 

            // Step 6: Convert holidays to DateTime
            List<DateTime> holidays = holidayStrings
                .Select(h => DateTime.ParseExact(h, "yyyy/MM/dd", null))
                .ToList();

            // Step 7: Remove holidays from dateList
            dateList = dateList.Where(d => !holidays.Contains(d)).ToList(); 
            // Prepare columns
            dataGridView2.Columns.Add("LINE", "LINE"); 
            foreach (var d in dateList)
            {
                string colName = d.ToString("MM/dd");
                dataGridView2.Columns.Add(colName, colName);
            }
            dataGridView2.Columns.Add("TOTAL", "TOTAL");

            var lookup = new Dictionary<string, Dictionary<DateTime, decimal>>(StringComparer.OrdinalIgnoreCase);

            foreach (var row in data)
            {
                if (!row.ContainsKey("LINE") || row["LINE"] == null) continue;
                var line = row["LINE"].ToString();

                if (!lookup.ContainsKey(line))
                    lookup[line] = new Dictionary<DateTime, decimal>();

                if (row.ContainsKey("TARGETDATE") && row["TARGETDATE"] != null)
                {
                    if (DateTime.TryParse(row["TARGETDATE"].ToString(), out DateTime dt))
                    {
                        decimal target = 0m;
                        if (row.ContainsKey("TARGET") && row["TARGET"] != null)
                            decimal.TryParse(row["TARGET"].ToString(), out target);

                        lookup[line][dt.Date] = target; // assign value for that date
                    }
                }
            }

            var uniqueLines = lookup.Keys.ToList();
            if (uniqueLines.Count == 0 && !string.IsNullOrWhiteSpace(textBox3.Text))
                uniqueLines.Add(textBox3.Text);

            foreach (var line in uniqueLines)
            {
                var values = new List<object> { line };
                decimal total = 0m;

                foreach (var d in dateList)
                {
                    decimal cellValue = 0m;
                    if (lookup.TryGetValue(line, out var dateDict))
                    {
                        if (dateDict.TryGetValue(d, out var v))
                            cellValue = v;
                    }
                    values.Add(cellValue);
                    total += cellValue;
                }

                values.Add(total);
                dataGridView2.Rows.Add(values.ToArray());
            } 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DesignDataGridView(dataGridView2); 
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return; // Skip header

                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                row.Cells[e.ColumnIndex].Style.BackColor = Color.LightGreen;
              //   row.Cells[e.RowIndex].Style.BackColor = Color.LightGreen;
                // Recalculate total by summing all cells except the last column
                int total = 0;
                for (int i = 0; i < dataGridView2.Columns.Count - 1; i++) // exclude last column
                {
                    var cellValue = row.Cells[i].Value;
                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int val))
                    {
                        total += val;
                    }
                }

                // Update last column (TOTAL)
                row.Cells[dataGridView2.Columns.Count - 1].Value = total;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error updating total: " + ex.Message);
            }
        } 

        private void dataGridView4_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView2.IsCurrentCellDirty)
            {
                dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form form1 = new TargetChecking(); 
            form1.ShowDialog();
        }

        private string buttonvalue = "CheckSOValidOrNot";
        
        private void button6_Click(object sender, EventArgs e) 
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageHelper.ShowErr(this, "Please Select Factory!");
                return;
            }
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageHelper.ShowErr(this, "Please Select Plant!");
                return;
            }
            if (string.IsNullOrEmpty(comboBox4.Text))
            {
                MessageHelper.ShowErr(this, "Please Select Process!");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageHelper.ShowErr(this, "Please Select at least one line!");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageHelper.ShowErr(this, "Please Select at least one SO!");
                return;
            }
            if (dateTimePicker6.Value.ToString("yyyy/MM/dd") == dateTimePicker5.Value.ToString("yyyy/MM/dd"))
            {
                MessageHelper.ShowErr(this, "Please Select the proper week");
                return;
            } 
            if(dataGridView2.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "Please Get Target First");
                return;
            } 
            if (buttonvalue == "CheckSOValidOrNot")  
            {
                CheckSOValidOrNot(); 
            }
            else if (buttonvalue == "CheckSchedule")
            {
                CheckSchedule();
            }
            else if (buttonvalue == "Upload")
            {
                GetSalesOrderDetails(); 
            } 
        } 
        private void CheckSOValidOrNot()
        {
            List<string> soList = GetSOList();
            if (soList == null || soList.Count == 0)
            {
                MessageHelper.ShowErr(this, "Please enter at least one Sales Order in TextBox2.");
                return;
            }

            try
            {
                Dictionary<string, object> data = new Dictionary<string, object>
                {
            { "SOList", soList }
                  } ;
                data.Add("org" , comboBox1.SelectedValue );   
                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "VerifySOOKNot",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null)
                {
                    List<string> missingSOList = JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString());
                    if (missingSOList.Count > 0)
                    {
                        string msg = "The following Sales Orders are Not Exist :\n\n" + string.Join("\n", missingSOList);
                        MessageBox.Show(msg, "Missing SOs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        List<string> remaining = soList.Except(missingSOList).ToList();
                        textBox2.Text = string.Join(Environment.NewLine, remaining);
                    }
                    else
                    {
                        MessageHelper.ShowOK(this, "All sales orders exist. ✅");
                        // ✅ Move to next stage
                        buttonvalue = "CheckSchedule";
                        button6.Text = "Check ";  
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "Error from server: " + ret.ErrMsg);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error while verifying SO: " + ex.Message);
            }
        }
        private void CheckSchedule()
        {
            List<string> soList = GetSOList();
            if (soList == null || soList.Count == 0)
            {
                MessageHelper.ShowErr(this, "Please enter at least one Sales Order in TextBox2.");
                return;
            }

            try
            {

                Dictionary<string, object> data = new Dictionary<string, object>
                {
                   { "SOList", soList }
                };
                data.Add("process", comboBox4.Text);  
                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "VerifyScheduleOKNot",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                if (ret.IsSuccess && ret.RetData != null)
                {
                    List<string> scheduledSOList = JsonConvert.DeserializeObject<List<string>>(ret.RetData.ToString());
                    if (scheduledSOList.Count > 0)
                    {
                        string msg = "The following Sales Orders are already Scheduled :\n\n" + string.Join("\n", scheduledSOList);
                        MessageBox.Show(msg, "Schedules SOs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        List<string> remaining = soList.Except(scheduledSOList).ToList();
                        textBox2.Text = string.Join(Environment.NewLine, remaining);
                    }
                    else
                    {
                        MessageHelper.ShowOK(this, "All sales orders need to be scheduled. ✅");
                        buttonvalue = "Upload";
                        button6.Text = "Upload"; 
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "Error from server: " + ret.ErrMsg); 
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error while verifying schedule: " + ex.Message); 
            }
        }  

        private List<string> GetSOList()
        {
            List<string> so = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                string[] soArray = textBox2.Text.Split(new[] { ',', ';', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in soArray)
                {
                    string trimmed = item.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                        so.Add(trimmed);
                }
            }

            return so;
        }
        public int GetTotalTarget()
        {
            int totalSum = 0;

            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.IsNewRow) continue; // Skip the empty new row at the end

                    if (row.Cells["TOTAL"].Value != null &&
                        int.TryParse(row.Cells["TOTAL"].Value.ToString(), out int value))
                    {
                        totalSum += value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while calculating total: " + ex.Message);
            }

            return totalSum;
        }

        private void GetSalesOrderDetails()  
        {
            string inputText = textBox2.Text.Trim(); 
            if (string.IsNullOrEmpty(inputText)) 
            {
                MessageHelper.ShowErr(this , "Please enter at least one SO!" );  
                return;
            }
            try
            {
                List<string> SOList = inputText
            .Split(new[] { ',', ';', '\n', '\r', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(a => a.Trim())
            .Distinct()
            .ToList();

                if (SOList.Count == 0)
                {
                    MessageHelper.ShowErr(this , "No valid SOS found!" );
                    return;
                }
                List<List<string>> SOvalues = new List<List<string>>();
                foreach (string so in SOList)
                {
                    SOvalues.Add(new List<string> { so });
                }
                Dictionary<string, object> Data = new Dictionary<string, object>
                       {
                            { "salesorders", SOvalues },
                            { "process", ProcessCode  } , 
                    {"org" , comboBox1.SelectedValue } 
                       };

                string retdata = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "GettingValuesBySO",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(Data)
                ); 

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (result.ContainsKey("RetData"))
                {
                    var retDataJson = result["RetData"].ToString();

                    if (!string.IsNullOrEmpty(retDataJson))
                    {
                        var retDataArray = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(retDataJson);

                        if (retDataArray.Count > 0)
                        {
                            int TotalTarget = GetTotalTarget();  
                            foreach (var row in retDataArray)
                            {
                                if (!row.ContainsKey("QTYVAL") || !int.TryParse(row["QTYVAL"]?.ToString(), out int orderQty))
                                    continue;
                                int targetQty = TotalTarget;

                                /* if (orderQty > targetQty) 
                                 {
                                     int remainingQty = orderQty - targetQty;
                                     int firstRowIdx = dataGridView1.Rows.Add();
                                     FillRowData(firstRowIdx, row, targetQty, dateTimePicker5.Value, dateTimePicker6.Value);
                                     int secondRowIdx = dataGridView1.Rows.Add();
                                     DateTime nextWeekStart = dateTimePicker5.Value.AddDays(7);
                                     DateTime nextWeekEnd = dateTimePicker6.Value.AddDays(7);
                                     FillRowData(secondRowIdx, row, remainingQty, nextWeekStart, nextWeekEnd);
                                 } 
                                 else
                                 {
                                     int newRowIdx = dataGridView1.Rows.Add();
                                     FillRowData(newRowIdx, row, orderQty, dateTimePicker5.Value, dateTimePicker6.Value);
                                 } */

                                int remainingQty = orderQty;
                                int weekCount = 0;
                                DateTime weekStart = dateTimePicker5.Value;
                                DateTime weekEnd = dateTimePicker6.Value;

                                while (remainingQty > 0)
                                {
                                    weekCount++;
                                    int allocatedQty = remainingQty > targetQty ? targetQty : remainingQty;
                                    int rowIndex = dataGridView1.Rows.Add();
                                    FillRowData(rowIndex, row, allocatedQty, weekStart, weekEnd);
                                    remainingQty -= allocatedQty;
                                    weekStart = weekStart.AddDays(7);
                                    weekEnd = weekEnd.AddDays(7);
                                }


                            }

                            dataGridView1.Refresh();
                            buttonvalue = "CheckSOValidOrNot";
                            button6.Text = "Check SO";
                        } 

                        else
                        {
                            MessageHelper.ShowErr(this, "No data found for this Process");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "No data found");
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "Invalid API response.");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }

        }
        private void FillRowData(int rowIndex, Dictionary<string, object> rowData, int orderQty, DateTime weekStart, DateTime weekEnd)
        {
            foreach (var kvp in rowData)
            {
                if (dataGridView1.Columns.Contains(kvp.Key))
                {
                    dataGridView1.Rows[rowIndex].Cells[kvp.Key].Value = kvp.Value?.ToString();
                }
            }

            // Update Order Quantity
            if (dataGridView1.Columns.Contains("QTYVAL"))
                dataGridView1.Rows[rowIndex].Cells["QTYVAL"].Value = orderQty;

            // Set status and static fields
            dataGridView1.Rows[rowIndex].Cells["UPDSTATVAL"].Value = 2;
            dataGridView1.Rows[rowIndex].Cells["ORGVAL"].Value = CompanyCode;
            dataGridView1.Rows[rowIndex].Cells["PLANTVAL"].Value = PlantCode;

            // Handle LINEVAL logic
            if (dataGridView1.Columns.Contains("LINEVAL"))
            {
                string input = textBox3.Text;
                string[] parts = input.Split(',');

                for (int i = 0; i < parts.Length; i++)
                {
                    if (parts[i].Length > 4)
                        parts[i] = parts[i].Substring(4);
                    else
                        parts[i] = "";
                }

                string Lresult = string.Join(",", parts);
                dataGridView1.Rows[rowIndex].Cells["LINEVAL"].Value = Lresult;
            }

            // Set week range
            dataGridView1.Rows[rowIndex].Cells["WEEKVAL"].Value =
                weekStart.ToString("yyyy/MM/dd") + "-" + weekEnd.ToString("yyyy/MM/dd");
        }


        private void UpdateEmptyWeekValues()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var weekVal = row.Cells["WEEKVAL"].Value?.ToString();
                var updStatVal = row.Cells["UPDSTATVAL"].Value?.ToString();

                // Fill only if WEEKVAL is empty and UPDSTATVAL == 2
                if (string.IsNullOrWhiteSpace(weekVal) && updStatVal == "2")
                {
                    row.Cells["WEEKVAL"].Value =
                        dateTimePicker5.Value.ToString("yyyy/MM/dd") + "-" +
                        dateTimePicker6.Value.ToString("yyyy/MM/dd");

                    
                }
            }
        } 

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Clear(); 
            textBox2.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;   
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker6.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            buttonvalue = "CheckSOValidOrNot";
            button6.Text = "Check SO"; 
        }  

        private void DesignDataGridView(DataGridView dgv)
        {
            

            // Attach event for column header click (only once)
/*            dgv.ColumnHeaderMouseClick -= Dgv_ColumnHeaderMouseClick;  
            dgv.ColumnHeaderMouseClick += Dgv_ColumnHeaderMouseClick */ 
            // General grid style
            // dgv.ReadOnly = true;
            /*   dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.MultiSelect = false;
               dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;*/
            //  dgv.AllowUserToAddRows = false;
            /*dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; 
            dgv.EnableHeadersVisualStyles = false;
            dgv.AllowUserToOrderColumns = true;*/
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.LightGray;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 35;

            // Row style
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternate row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);

            // Selection color
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(125, 50, 80);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Column header border and row height
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.RowTemplate.Height = 30;

            // Optional: auto-size for large datasets
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            // Optional: sort and alignment for numeric columns
           foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.ValueType == typeof(decimal) || col.ValueType == typeof(int))
                {
                    col.DefaultCellStyle.Format = "N0"; // comma separated format
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                enterPressed = false; // allow next Enter
            } 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PlanningScheduleTab_SelectedIndexChanged(object sender, EventArgs e)
        { 
             if(PlanningScheduleTab.SelectedIndex > 0 )   
            { 
                StyleComboBox(comboBox5); 
                StyleComboBox(comboBox6); 
                StyleComboBox(comboBox7); 
                StyleComboBox(comboBox8); 
                // Reaschedule Seach  
                StyleRoundedButton(button9, Color.FromArgb(52, 152, 219)); 
                // Reschedule export excel button 
                StyleRoundedButton(button8, Color.FromArgb(230, 126, 34)); 
                RLoadOrg(); 
                RLoadPlants(RCompanyCode); 
                RLoadProcess(); 
                comboBox5.SelectedValue = ""; 
                comboBox8.SelectedValue = ""; 
                DesignDataGridView(dataGridView4);   
            }   
        }   

        private void RLoadOrg() 
        {
            try
            {
                List<ComboboxEntry> Companies = new List<ComboboxEntry> { };
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadOrg", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                   && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);

                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        Companies.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                    }
                    comboBox6.DataSource = Companies;
                    comboBox6.DisplayMember = "Name";
                    comboBox6.ValueMember = "Code";
                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox6.SelectedIndex = 0;
                        string RdefaultOrgCode = comboBox6.SelectedValue.ToString();
                        RCompanyCode = RdefaultOrgCode;
                    }
                    comboBox6.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox6.SelectedIndex >= 0 && comboBox6.SelectedValue != null)
                        {
                            string selectedCode = comboBox6.SelectedValue.ToString();
                            string selectedName = comboBox6.Text;
                            RCompanyCode = selectedCode;
                            RLoadPlants(RCompanyCode); 
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }
        private void RLoadPlants(string orgId) 
        {
            try
            {
                List<ComboboxEntry> Plants = new List<ComboboxEntry> { };
                Dictionary<string, object> Data = new Dictionary<string, object>();
                Data.Add("OrgId", orgId);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController",
                    "LoadPlant", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                   && result.ContainsKey("RetData") && result["RetData"] != "")
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    Plants.Add(new ComboboxEntry() { Code = "", Name = "" });
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        Plants.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["CODE"].ToString(), Name = dtJson.Rows[i]["NAME"].ToString() });
                    }
                    comboBox5.DataSource = Plants;
                    comboBox5.DisplayMember = "Name";
                    comboBox5.ValueMember = "Code";

                    if (dtJson.Rows.Count > 0)
                    {
                        comboBox5.SelectedIndex = 0;
                        string RdefaultPlantCode = comboBox5.SelectedValue.ToString();
                        RPlantCode = RdefaultPlantCode;

                    }
                    comboBox5.SelectedIndexChanged += (s, e) =>
                    {
                        if (comboBox5.SelectedIndex >= 0 && comboBox5.SelectedValue != null)
                        {
                            string selectedCode = comboBox5.SelectedValue.ToString();
                            string selectedName = comboBox5.Text;
                            RPlantCode = selectedCode;
                            RLoadLines(RCompanyCode, RPlantCode, RProcessCode);
                        }
                    };

                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, "Error: " + ex.Message);
            }
        }
        private void RLoadLines(string OrgId, String Plant, String Process)
        {
            if (string.IsNullOrEmpty(OrgId))
            {
                MessageHelper.ShowErr(this, "Please select Factory!");
            }
            else if (string.IsNullOrEmpty(Plant))
            {
                MessageHelper.ShowErr(this, "Please select Plant!");
            }
            else if (string.IsNullOrEmpty(Process))
            {
                MessageHelper.ShowErr(this, "Please select Process!");
            }
            else
            {
                try
                {  
                    List<ComboboxEntry> Lines = new List<ComboboxEntry> { };
                    Dictionary<string, object> Data = new Dictionary<string, object>();
                    Data.Add("OrgId", OrgId);
                    Data.Add("Plant", Plant);
                    Data.Add("Process", Process);
                    string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_CUTMNT",
                        "KZ_CUTMNT.Controllers.PlanningController",
                        "LoadLines", Program.client.UserToken, JsonConvert.SerializeObject(Data));
                    ResultObject ret = JsonConvert.DeserializeObject<ResultObject>(retdata);
                    var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);
                    if (result != null && result.ContainsKey("IsSuccess") && Convert.ToBoolean(result["IsSuccess"])
                       && result.ContainsKey("RetData") && result["RetData"] != "")
                    {
                        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                        DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                        Lines.Add(new ComboboxEntry() { Code = "", Name = "" });
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            Lines.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["department_code"].ToString(), Name = dtJson.Rows[i]["department_name"].ToString() });
                        } 
                        comboBox7.DataSource = Lines;
                        comboBox7.DisplayMember = "Name";
                        comboBox7.ValueMember = "Code";

                        comboBox7.SelectedIndexChanged += (s, e) =>
                        {
                            if (comboBox7.SelectedIndex >= 0 && comboBox7.SelectedValue != null)
                            {
                                string selectedCode = comboBox7.SelectedValue.ToString();
                                string selectedName = comboBox7.Text;
                                RLineCode = selectedCode;
                            }
                        }; 
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, "Error: " + ex.Message);
                }
            }
        }
        private void RLoadProcess()
        {
            List<ComboboxEntry> Process = new List<ComboboxEntry> { };

            Process.Add(new ComboboxEntry() { Code = "", Name = "" });
            Process.Add(new ComboboxEntry() { Code = "C", Name = "Cutting" });
            Process.Add(new ComboboxEntry() { Code = "S", Name = "Stitching" });
            Process.Add(new ComboboxEntry() { Code = "L", Name = "Assembly" });
            comboBox8.DataSource = Process;
            comboBox8.DisplayMember = "Name";
            comboBox8.ValueMember = "Code";
            comboBox8.SelectedIndexChanged += (s, e) =>
            {
                if (comboBox8.SelectedIndex >= 0 && comboBox8.SelectedValue != null)
                {
                    string selectedCode = comboBox8.SelectedValue.ToString();
                    string selectedName = comboBox8.Text;
                    RProcessCode = selectedCode;
                    RLoadLines(RCompanyCode, RPlantCode, RProcessCode); 
                }
            };

        }

        private void SEarchFunction()
        {
            try
            {
                var data = new Dictionary<string, object>
                {
                    ["week"] = $"{dateTimePicker3.Text}-{dateTimePicker4.Text}" ,  
                    ["OrgId"] = RCompanyCode,
                    ["Plant"] = RPlantCode,
                    ["Process"] = RProcessCode , 
                    ["line"] = RLineCode 
                };

                if (string.IsNullOrEmpty(data["Plant"]?.ToString()) || string.IsNullOrEmpty(data["Process"]?.ToString()) || string.IsNullOrEmpty(data["line"]?.ToString())) 
                {
                    MessageHelper.ShowErr(this, "Please select Plant and Process first.");
                    return; 
                } 
                string response = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_CUTMNT",
                    "KZ_CUTMNT.Controllers.PlanningController", 
                    "GetReschedulePos", 
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(data)
                );

                var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (result == null || !result.ContainsKey("IsSuccess"))
                {
                    MessageHelper.ShowErr(this, "Invalid or empty response from server.");
                    return;
                }

                bool isSuccess = Convert.ToBoolean(result["IsSuccess"]);


                if (isSuccess && result.ContainsKey("RetData") && !string.IsNullOrEmpty(result["RetData"]?.ToString()))
                {
                    string json = result["RetData"].ToString();
                    DataTable dt = JsonHelper.GetDataTableByJson(json);

                    if (dt.Columns.Contains("CRD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["CRD"]?.ToString(), out DateTime dateValue))
                            {
                                row["CRD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    if (dt.Columns.Contains("LPD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["LPD"]?.ToString(), out DateTime dateValue))
                            {
                                row["LPD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    if (dt.Columns.Contains("PSDD"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (DateTime.TryParse(row["PSDD"]?.ToString(), out DateTime dateValue))
                            {
                                row["PSDD"] = dateValue.ToString("yyyy-MM-dd");
                            }
                        }
                    }

                    dataGridView4.DataSource = dt; 
                }

                else if (result.ContainsKey("ErrMsg") && !string.IsNullOrEmpty(result["ErrMsg"]?.ToString()))
                {
                    string apiErrorMessage = result["ErrMsg"].ToString();
                    MessageHelper.ShowErr(this, $"API Error: {apiErrorMessage}\nPlease contact the IT department.");
                } 
                else
                {
                    dataGridView4.DataSource = null;
                    MessageHelper.ShowErr(this, "No data found matching the search criteria.");
                } 
            }
            catch (Exception ex)
            {

                if (dataGridView4.Rows.Count > 0)
                {
                    dataGridView4.Rows.Clear(); 
                }
                MessageHelper.ShowErr(this, $"An error occurred. Please contact IT Department.\nDetails: {ex.Message}");
            }
        } 

        private void button9_Click_1(object sender, EventArgs e)
        {
            SEarchFunction();  
        } 

        private void button10_Click(object sender, EventArgs e)
        {
            string ipaddress = GetLocalIPAddress(); 
            // Reschedule form = new Reschedule(dataGridView4.DataSource , RCompanyCode , RPlantCode , RProcessCode , ipaddress );
            Reschedule form = new Reschedule(dataGridView4.DataSource, ipaddress); 
            form.ShowDialog(); 

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
