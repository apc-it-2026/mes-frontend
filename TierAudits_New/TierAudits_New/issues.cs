using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Microsoft.Office.Interop.Excel;
using SJeMES_Framework.WebAPI;
using Newtonsoft.Json;
using AutocompleteMenuNS;
using SJeMES_Framework.Common;
using SJeMES_Control_Library;

namespace TierAudits_New
{
    public partial class issues : Form
    {
        string Prodline = "";
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public issues()
        {
            InitializeComponent();
        }


        private void sample_excel_Click(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
        {

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vline", line.Text);
           // p.Add("vstart_date", start_date.Text);
            p.Add("vend_date", end_date.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.TierAuditServer", "GetProdIssuesData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            // string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.TierAuditServer", "GetDayQIPData", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            // string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetDayIE_Efficiency", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                // DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    upload_issueGrd.DataSource = dtJson;

                }
                else
                {
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        public void query()
        {
            System.Drawing.Font a = new System.Drawing.Font("宋体", 12);
            upload_issueGrd.Font = a;//font

            Dictionary<string, Object> p = new Dictionary<string, object>();

            //p.Add("vOrg_code", comboBox1.Text.Trim().ToString().ToUpper().Split('|')[0]);//.Trim().ToString().ToUpper().Split('|')[0]
            //p.Add("vPlant_code", comboBox2.Text.Trim().ToString().ToUpper().Split('|')[0]);
            //p.Add("vDept_code", textBox1.Text);
            p.Add("isuLine", line.Text);
            p.Add("ProdIssue", issue_box.Text.ToString().ToUpper().Split('|')[0].Trim());
          //  p.Add("isuBeginTime", start_date.Text);
            p.Add("isuEndTime", end_date.Text);


            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "QueryTMProdIssuesReport",
            Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                System.Data.DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {

                    upload_issueGrd.DataSource = dtJson;

                }
                else
                {
                    upload_issueGrd.Columns.Clear();
                    MessageBox.Show("No such data");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        //private void FileSelect_Click(object sender, EventArgs e)
        //{

        //    OpenFileDialog openFileDialog1 = new OpenFileDialog
        //    {
        //        InitialDirectory = @"D:\",
        //        Title = "Browse Text Files",

        //        CheckFileExists = true,
        //        CheckPathExists = true,

        //        DefaultExt = "txt",
        //        Filter = "txt files (*.txt)|*.txt",
        //        FilterIndex = 2,
        //        RestoreDirectory = true,

        //        ReadOnlyChecked = true,
        //        ShowReadOnly = true
        //    };

        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //      //  textBox1.Text = openFileDialog1.FileName;
        //    }
        //}
        //public string GetFile(OpenFileDialog pOFD)
        //{
        //    pOFD.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
        //    if (pOFD.ShowDialog() == DialogResult.OK)
        //    {
        //        return pOFD.FileName;
        //    }
        //    return "";
        //}
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            System.Drawing.Rectangle headerBounds = new System.Drawing.Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void GetExcelData(string fileName)
        {
            try
            {
                 this._currentExcelProcessor = new ExcelProcessor(fileName);
                IList<object[]> list = this._currentExcelProcessor.GetSheetData(0);
                if (data != null && data.Count > 0)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        data.Add(list[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string chk = "";
                        for (int j = 0; j < list[i].Length; j++)
                        {

                            object vl = list[i][j];

                            if (j == 0)
                                chk = list[i][j] == null ? "" : list[i][j].ToString();
                        }

                        //int chk = 0;
                        //foreach (string item in list[i])
                        //{

                        //}

                        if (chk != "")
                            data.Add(list[i]);


                    }

                    //data = list;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            isTitle = true;
            //btnFolder.Enabled = false;
            FileSelect.Enabled = false;
            //butImport.Enabled = false;
            if (this.upload_issueGrd.Rows.Count >= 2)
            {
                try
                {
                    update_db();
                }
                catch (Exception ex)
                {
                    FileSelect.Enabled = true;
                    //butImport.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                FileSelect.Enabled = true;
                //butImport.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.Client, "", Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            Cursor.Current = Cursors.Default;
        }


        private void UpLoad(System.Data.DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.IE_BonusEvalServer", "UpLoad",
                            Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.Client, "", Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.Client, "", Program.Client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }
        private void update_db()
        {

            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn col in upload_issueGrd.Columns)
            {
                dt.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in upload_issueGrd.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }

            try
            {

                UpLoad(StripEmptyRows(dt));
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }

        }

        private System.Data.DataTable StripEmptyRows(System.Data.DataTable dt)
        {
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value || dt.Rows[i][1].ToString() == "")
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        private void Issues_Load(object sender, EventArgs e)
        {
            LoadDept();
        }

        private void FileSelect_Click_1(object sender, EventArgs e)
        {
            isTitle = true;
            string errs = "";
            this.upload_issueGrd.AutoGenerateColumns = false;
            if (upload_issueGrd != null)
            {
                this.upload_issueGrd.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (upload_issueGrd != null)
                {
                    data = new List<object[]>();
                    this.upload_issueGrd.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.upload_issueGrd.Rows.Add(filename);
                        upload_issueGrd.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                upload_issueGrd.AllowUserToAddRows = false;
                if (data != null && data.Count > 0)
                {
                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        //if (i > 0 && i < colNum)
                        //{
                        //    try
                        //    {
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                        //        //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");
                        //        acCode.Width = 68;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                        //    }
                        //}
                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        upload_issueGrd.Columns.Add(acCode);
                    }
                    upload_issueGrd.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)
                    {
                        upload_issueGrd.Rows.Add(data[i]);
                        upload_issueGrd.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);


                        int len = data.Count;
                        for (int d = 1; d < len; d++)
                        {
                            if (data[d][2] != null && data[d][2].ToString().Trim() != "")
                            {
                                try
                                {
                                    decimal target = decimal.Parse(data[d][2].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + d + ",cloumn: 2," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[d][2] = "";
                            }
                        }


                    }
                }
            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SubmitPi_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetProdIssuesData", Program.Client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();

               
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                upload_issueGrd.DataSource = dt;

                
            }
            else
            {
                MessageBox.Show("NO SUCH DATA AVAILABLE");
            
            }
        }


        private void GetDept()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "GetDept", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Prodline = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                // d_dept_name = dtJson.Rows[0]["DEPARTMENT_NAME"].ToString();
                // orgId = dtJson.Rows[0]["ORG_ID"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadDept()
        {
            try
            {
                GetDept();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            line.Text = Prodline;
            //labelDeptName.Text = d_dept_name;
        }

        //issues form
        private void issueSubmit_Click(object sender, EventArgs e)
        {
            // Convert the line text to upper case
            string lineValue = line.Text.ToUpper();

            // Check the length of the line value
            if (lineValue.Length < 7)
            {
                // Display an error message and do not proceed with the operation
                MessageBox.Show("Line length should be 7 characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the selected item from the issue_box ComboBox
            if (issue_box.SelectedItem == null)
            {
                MessageBox.Show("Please select an item from the issue box.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string Issue_box = issue_box.SelectedItem.ToString();

            // Get the date from the end_date DateTimePicker and format it
            DateTime issueDate = end_date.Value;
            string Idate = issueDate.ToString("yyyy/MM/dd");

            // Prepare the data for the API call
            Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "Line", lineValue },
        { "Idate", Idate },
        { "issue_box", Issue_box }
    };

            // Make the API call
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "InsertIssues", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            // Process the API response
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    MessageBox.Show("Successfully Submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Submit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(retJson["ErrMsg"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void line_TextChanged(object sender, EventArgs e)

        //{
        //    AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
        //    autocompleteMenu2.Items = null;
        //    autocompleteMenu2.MaximumSize = new Size(200, 350);
        //    var columnWidth = new[] { 50, 350 };
        //    DataTable dt = GetAllDepts();
        //    int n = 1;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        //autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        n++;
        //    }
        //    autocompleteMenu2.SetAutocompleteMenu(line, autocompleteMenu2);
        //}


        //private DataTable GetAllDepts()
        //{
        //    DataTable dt = new DataTable();
        //    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_TIERAPI", "KZ_TIERAPI.Controllers.TierAuditServer", "autoLoadDept", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        dt = JsonHelper.GetDataTableByJson(json);
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }

        //    return dt;
        //}
    }
}

