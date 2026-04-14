using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using AutocompleteMenuNS;
using NewExportExcels;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PO_Completion_Upload_Data
{
    public partial class PO_Completion_Upload_Data : MaterialForm
    {

        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;

        public PO_Completion_Upload_Data()
        {
            InitializeComponent();
            dateTimePicker5.ValueChanged += new EventHandler(dateTimePicker5_ValueChanged);
            Update_To_Date_Range(dateTimePicker1.Value);
        }

        private void Update_To_Date_Range(DateTime value)
        {
            DateTime SelectedDate = dateTimePicker5.Value;

            DateTime FirstDay_of_Month = new DateTime(SelectedDate.Year, SelectedDate.Month, 1);
            DateTime LastDay_of_Month = FirstDay_of_Month.AddMonths(1).AddDays(-1);

            dateTimePicker1.MinDate = FirstDay_of_Month;
            dateTimePicker1.MaxDate = LastDay_of_Month;

            if (dateTimePicker1.Value < FirstDay_of_Month || dateTimePicker1.Value > LastDay_of_Month)
            {
                dateTimePicker1.Value = FirstDay_of_Month;
            }
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            Update_To_Date_Range(dateTimePicker1.Value);
        }

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
                    data = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {

            isTitle = true;
            string errs = "";
            this.dgvShowData.AutoGenerateColumns = false;
            if (dgvShowData != null)
            {
                this.dgvShowData.Columns.Clear();
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xls*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dgvUploadPath != null)
                {
                    data = new List<object[]>();
                    this.dgvUploadPath.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dgvUploadPath.Rows.Add(filename);
                        dgvUploadPath.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                dgvShowData.AllowUserToAddRows = false;

                #region 



                if (data != null && data.Count > 0)
                {

                    /*------------  Header Text Start----------------- */

                    int colNum = data[0].Length;
                    for (int i = 0; i < colNum; i++)
                    {
                        DataGridViewTextBoxColumn acCode = new DataGridViewTextBoxColumn();
                        if (i > 0 && i < colNum)
                        {
                            try
                            {
                                //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToShortDateString();
                                //data[0][i] = Convert.ToDateTime(data[0][i].ToString()).ToString("yyyy/MM/dd");


                                data[0][i] = data[0][i].ToString();
                                acCode.Width = 68;
                            }
                            catch (Exception ex)
                            {
                                errs += "column:" + (i + 1) + "," + ex.Message + "\n";
                            }
                        }


                        acCode.Name = data[0][i].ToString();
                        acCode.HeaderText = data[0][i].ToString();
                        dgvShowData.Columns.Add(acCode);
                        //acCode.HeaderText = "Total";

                    }

                    /*------------  Header Text  End----------------- */



                    /*------------  Rows Text  Start----------------- */

                    dgvShowData.Rows.Add(data[1]);
                    for (int i = 2; i < data.Count; i++)                  // New Code 

                    //for (int i = 2; i < data.Count; i++)            // Old Code
                    {
                        dgvShowData.Rows.Add(data[i]);
                        dgvShowData.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        //try
                        //{
                        //    if (!ValiDept(data[i][0].ToString().Trim().ToUpper()))
                        //    {
                        //        errs += "row:" + i + ",error department" + "\n";
                        //    }
                        //    else
                        //    {
                        //        data[i][0] = data[i][0].ToString().Trim().ToUpper();
                        //    }
                        //}
                        //catch(Exception)
                        //{
                        //    errs += "row:" + i + ",error department" + "\n";
                        //}

                        for (int j = 1; j < colNum; j++)
                        {
                            if (data[i][j] != null && data[i][j].ToString().Trim() != "")
                            {
                                try
                                {
                                    //int qty = int.Parse(data[i][j].ToString().Trim());


                                    string line = (data[i][j].ToString().Trim());
                                }
                                catch (Exception ex)
                                {
                                    errs += "row:" + i + ",cloumn:" + (j + 1) + "," + ex.Message + "\n";
                                }
                            }
                            else
                            {
                                data[i][j] = "";
                            }
                        }
                    }


                    /*------------  Rows Text  End----------------- */

                }


                #endregion

            }
            if (errs != "")
            {
                MessageBox.Show(errs);
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            isTitle = true;

            btnSelectFile.Enabled = false;
            btnSave.Enabled = false;

            if (this.dgvShowData.Rows.Count >= 1)
            {
                try
                {
                    update_db();

                }
                catch (Exception ex)
                {
                    btnSelectFile.Enabled = true;
                    btnSave.Enabled = true;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                btnSelectFile.Enabled = true;
                btnSave.Enabled = true;
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00001", Program.Client, "", Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
            }

        }

        private void update_db()
        {
            string errs = "";
            DataTable tab = new DataTable();
            object[] cols = data[0];
            for (int i = 0; i < cols.Length; i++)
            {
                tab.Columns.Add(cols[i].ToString());
            }
            for (int i = 1; i < data.Count; i++)
            {
                DataRow dr = tab.NewRow();
                try
                {
                    if (!ValiDept(data[i][0].ToString().Trim().ToUpper()))
                    {
                        errs += "row:" + i + ",error department" + "\n";
                    }
                    else
                    {
                        dr[0] = data[i][0].ToString().Trim().ToUpper();
                    }
                }
                catch (Exception ex)
                {
                    errs += "row:" + i + ",error department" + "\n";
                }
                for (int j = 1; j < cols.Length; j++)
                {
                    if (data[i][j] != null && data[i][j].ToString().Trim() != "")
                    {
                        try
                        {
                            int qty = int.Parse(data[i][j].ToString().Trim());
                            dr[j] = data[i][j].ToString().Trim();
                        }
                        catch (Exception ex)
                        {
                            errs += "row:" + i + ",cloumn:" + (j + 1) + "," + ex.Message + "\n";
                        }
                    }
                    else
                    {
                        dr[j] = "";
                    }
                }
                tab.Rows.Add(dr);
            }
            if (errs == "")
            {
                try
                {
                    UpLoad(tab);
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                MessageBox.Show(errs);
            }
        }

        private bool ValiDept(string d_dept)
        {
            bool isOk = false;
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("vDDept", d_dept);
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "ValiDept",
            //                Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "ValiDept", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                isOk = bool.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOk;
        }

        private void UpLoad(DataTable tab)
        {
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("data", tab);
            d.Add("Date", dateTimePicker1.Text);
            d.Add("Date2", dateTimePicker5.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "UpLoad",
                            Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.Client, "", Program.Client.Language);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                cleardata();
            }
            else
            {

                var Error =  Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(ret)["ErrMsg"].ToString();
                MessageBox.Show(Error," Error !");
                btnSelectFile.Enabled = true;
                btnSave.Enabled = true;

            }
        }

        private void DownLoad_Excel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\PO Completion Excel.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "PO Completion Excel.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            cleardata();
        }

        private void cleardata()
        {
            btnSelectFile.Enabled = true;
            btnSave.Enabled = true;
            dgvShowData.Columns.Clear();
            dgvUploadPath.Rows.Clear();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageHelper.ShowErr(this, "PLease Enter Line !");
                return;
            }
            string date = dateTimePicker2.Text;
            string line = textBox1.Text;

            Cursor.Current = Cursors.WaitCursor;
            //LoadingProgress lp = new LoadingProgress();
            //lp.Show();
            Get_PO_Completion_Info(date, line);
            //lp.Hide();
            Cursor.Current = Cursors.Default;
        }

        private void Get_PO_Completion_Info(string date, string line)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("DATE", date);
            p.Add("LINE", line);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_PO_Completion_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            string Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(Json, (typeof(DataTable)));

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dtJson.Columns["LINE"].ReadOnly = true;
                }

                dgvUpdateInfo.DataSource = dtJson;
                dgvUploadPath.Refresh();

            }
        }

        private void Btn_updateClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            dgvUpdateInfo.Columns.Clear();
        }

        private void Btn_update_Click(object sender, EventArgs e)
        {

            if (dgvUpdateInfo.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "No Data Found to Update");
                return;
            }

            //int totalCount = 0;
            DialogResult res = MessageHelper.ShowOK(this, "Are you Sure you Want to Update!");
            if (res == DialogResult.OK)
            {

                //foreach (DataGridViewRow item in dgvUpdateInfo.Rows-1)
                for (int i = 0; i < dgvUpdateInfo.Rows.Count - 1; i++)
                {
                    string date = dateTimePicker2.Text;
                    string line = dgvUpdateInfo.Rows[i].Cells[0].Value.ToString();
                    string total_po = dgvUpdateInfo.Rows[i].Cells[1].Value.ToString();
                    string ontime_finish = dgvUpdateInfo.Rows[i].Cells[2].Value.ToString();
                    string late_finish = dgvUpdateInfo.Rows[i].Cells[3].Value.ToString();
                    string delay = dgvUpdateInfo.Rows[i].Cells[4].Value.ToString();
                    // Update_PO_Completion_Info(line,total_po,ontime_finish,late_finish,delay);

                    Dictionary<string, Object> d = new Dictionary<string, object>();
                    d.Add("line", line);
                    d.Add("Date", dateTimePicker2.Text);
                    d.Add("Date2", dateTimePicker6.Text);
                    d.Add("Total_PO", total_po);
                    d.Add("Ontime_Finish", ontime_finish);
                    d.Add("Late_Finish", late_finish);
                    d.Add("Delay", delay);

                    Cursor.Current = Cursors.WaitCursor;
                    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Update_PO_Completion_Info",
                                    Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
                    Cursor.Current = Cursors.Default;

                    //if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    //{
                    //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Update Success！", Program.Client, "", Program.Client.Language);
                    //    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    //    Get_PO_Completion_Info(date, line);
                    //}
                    //else
                    //{
                    //    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Failed to Update！", Program.Client, "", Program.Client.Language);
                    //    string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    //    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
                    //}

                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Modified Successfully！", Program.Client, "", Program.Client.Language);
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                        Get_PO_Completion_Info(date, line);
                    }
                    else
                    {
                        string tips = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.Client, "", Program.Client.Language);
                        string msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                        MessageBox.Show(msg, tips);
                    }


                }

                //if (totalCount > 0)
                //{
                //    MessageHelper.ShowSuccess(this, $"successfully saved {totalCount}Records");
                //    // rowIndexList.Clear();
                //    // BindData();
                //}




            }


        }

        public class ComboBoxData
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        private void PO_Completion_Upload_Data_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            checkBox1.Visible = false;
            btn_PDF.Visible = false;
            dateTimePicker1.MaxDate = DateTime.Now;
            dateTimePicker2.MaxDate = DateTime.Now;
            dt_from_date.MaxDate = DateTime.Now;
            dt_to_date.MaxDate = DateTime.Now;
            tabControl1.TabPages.Remove(tabPage4);
            tabControl1.TabPages.Remove(tabPage2);
            this.dateTimePicker5.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            this.dateTimePicker6.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            //this.dt_from_date.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);
            LoadOrgId();
            LoadPlant();
            LoadRoutNo();
        }
        private void LoadRoutNo()
        {
            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            cb_process.DataSource = items4;
            cb_process2.DataSource = items4;
        }
        private void LoadOrgId()
        {
            List<ComboBoxData> WMSorgEntries = new List<ComboBoxData> { };
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadOrgId", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                WMSorgEntries.Add(new ComboBoxData() { Code = "", Name = "" });
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    WMSorgEntries.Add(new ComboBoxData() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                }

                cb_org.DataSource = WMSorgEntries;
                cb_org.DisplayMember = "Name";
                cb_org.ValueMember = "Code";

                cb_org2.DataSource = WMSorgEntries;
                cb_org2.DisplayMember = "Name";
                cb_org2.ValueMember = "Code";

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadPlant()
        {
            var items1 = new List<AutocompleteItem>();

            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadPlant", Program.Client.UserToken, string.Empty);


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["CODE"].ToString() }, dtJson.Rows[i - 1]["CODE"].ToString()));
                }
            }
            cb_plant.DataSource = items1;
            cb_plant2.DataSource = items1;


        }

        private void Btn_query_Clear_Click(object sender, EventArgs e)
        {
            cb_org.SelectedItem = "";
            cb_plant.SelectedItem = "";
            cb_process.SelectedItem = "";
            textBox2.Text = null;
        }

        private void Btn_query_search_Click(object sender, EventArgs e)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("FromDate", dt_from_date.Text);
            p.Add("ToDate", dt_to_date.Text);
            p.Add("Org_Id", cb_org.SelectedValue.ToString());
            p.Add("Plant", string.IsNullOrWhiteSpace(cb_plant.Text) ? cb_plant.Text : cb_plant.Text.Split('|')[0]);
            p.Add("Process", string.IsNullOrWhiteSpace(cb_process.Text) ? cb_process.Text : cb_process.Text.Split('|')[0]);
            p.Add("Line", textBox2.Text);

            Cursor.Current = Cursors.WaitCursor;

            if (checkBox1.Checked == false)
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_PO_Completion_Report_DayWise", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                Cursor.Current = Cursors.Default;
                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                string Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(Json, (typeof(DataTable)));
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Refresh();
                }
                else
                {
                    if (dtJson == null || dtJson.Rows.Count <= 0)
                    {

                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                        dataGridView1.DataSource = null;

                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        dataGridView1.DataSource = null;
                    }

                }
            }
            else
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.PO_Completion_Server", "Get_PO_Completion_Report_Accumulated", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                Cursor.Current = Cursors.Default;
                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                string Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(Json, (typeof(DataTable)));
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {
                    dataGridView1.DataSource = dtJson;
                    dataGridView1.Refresh();
                }
                else
                {
                    if (dtJson == null || dtJson.Rows.Count <= 0)
                    {

                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                        dataGridView1.DataSource = null;

                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        dataGridView1.DataSource = null;
                    }

                }
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                #region Old Code
                //if (checkBox1.Checked == true)
                //{
                //    string a = "PO Completion Report ";
                //    ExportExcels.Export(a, dataGridView1);
                //}
                //else
                //{
                //    string a = "PO Completion Report ";
                //    ExportExcels.Export(a, dataGridView1);
                //
                #endregion



                DataTable dataTable = new DataTable();

                // Adding columns to DataTable
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    dataTable.Columns.Add(column.HeaderText, column.ValueType);
                }

                // Adding rows to DataTable
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;
                    DataRow dataRow = dataTable.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dataTable, "Sheet1");
                            workbook.SaveAs(saveFileDialog.FileName);
                            //MessageBox.Show("Exported successfully to Excel!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Export Excel Success");
                        }
                    }
                }


            }
        }

        private void btn_PDF_Click(object sender, EventArgs e)
        {

            #region Old Code
            DataTable dataTable = new DataTable();

            // Adding columns to DataTable
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                dataTable.Columns.Add(column.HeaderText, column.ValueType);
            }

            // Adding rows to DataTable
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                DataRow dataRow = dataTable.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dataRow[cell.ColumnIndex] = cell.Value ?? DBNull.Value;
                }
                dataTable.Rows.Add(dataRow);
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "PDF Files|*.pdf" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    document.Open();

                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);

                    // Define fonts
                    iTextSharp.text.Font headerFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES, 9, BaseColor.BLUE);
                    iTextSharp.text.Font cellFont = iTextSharp.text.FontFactory.GetFont(FontFactory.TIMES, 8, BaseColor.BLACK);

                    // Adding headers with custom font and alignment
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, headerFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            BackgroundColor = BaseColor.CYAN,
                            PaddingTop = 5f,
                            PaddingBottom = 5f
                        };
                        pdfTable.AddCell(cell);
                    }

                    // Adding DataRow with custom font and alignment
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (object cellData in row.ItemArray)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(cellData.ToString(), cellFont))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT, // Set alignment as needed
                                VerticalAlignment = Element.ALIGN_MIDDLE,

                            };
                            pdfTable.AddCell(cell);
                        }
                    }

                    document.Add(pdfTable);
                    document.Close();

                    MessageBox.Show("Exported successfully to PDF!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            #endregion


        }
    }
}
