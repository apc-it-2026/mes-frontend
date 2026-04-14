using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;

namespace WorkingHoursQuery
{
    public partial class WorkingHoursQuery : Form
    {
        private DataTable dtJson = new DataTable();

        public WorkingHoursQuery()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

        }


        private void WorkingHoursQueryForm_Load(object sender, EventArgs e)
        {
            comboBoxReportType.SelectedIndex = 0;
            LoadDept();
            LoadOrgInfo();
            bool isMangager = IsMangager();
            if (isMangager)
            {
                btnAutoSync.Visible = true;
                btnHandHourSync.Visible = true;
            }
            else
            {
                btnAutoSync.Visible = false;
                btnHandHourSync.Visible = false;
            }
        }

        private bool IsMangager()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "IsMangager", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                bool isMangager = JsonConvert.DeserializeObject<bool>(json);
                return isMangager;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return false;
            }
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            txtWorkCenter.Text = "";
            txtFactoryRegion.Text = "";
            txtEmpNo.Text = "";
            dataGridViewWorkingHours.DataSource = null;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("workCenter", txtWorkCenter.Text);
            parm.Add("factoryRegion", txtFactoryRegion.Text);
            parm.Add("empNo", txtEmpNo.Text);
            parm.Add("startTime", dateTimePickerStartTime.Value.Date);
            parm.Add("endTime", dateTimePickerEndTime.Value.Date.AddDays(1).AddSeconds(-1));
            parm.Add("reportType", comboBoxReportType.SelectedIndex);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "WorkingHoursQuery", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                DataRow totalSumNewRow = dtJson.NewRow();
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    if (comboBoxReportType.SelectedIndex == 0)
                    {
                        totalSumNewRow["PunchingCardRecord"] = "Total";
                    }
                    else
                    {
                        totalSumNewRow["DateTo"] = "Total";
                    }

                    string strValue = $"{dtJson.Compute("sum(onlinetime)", ""):f2}"; //f2 retains two digits after the decimal point, and f4 retains the last four digits. The field in this field cannot be a string, but must be a number, so it must be ensured that the field read by the database is a numeric type.
                    totalSumNewRow["onlinetime"] = strValue;
                    dtJson.Rows.Add(totalSumNewRow);
                }

                if (comboBoxReportType.SelectedIndex == 0)
                {
                    dataGridViewWorkingHours.Visible = true;
                    dataGridViewEmpNo.Visible = false;
                    dataGridViewWorkingHours.AutoGenerateColumns = false;
                    dataGridViewWorkingHours.DataSource = dtJson;
                }
                else
                {
                    dataGridViewEmpNo.Visible = true;
                    dataGridViewWorkingHours.Visible = false;
                    dataGridViewEmpNo.AutoGenerateColumns = false;
                    dataGridViewEmpNo.DataSource = dtJson;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void LoadDept()
        {
            List<string> stringList = new List<string>();
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

            Dictionary<string, object> parm = new Dictionary<string, object>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDept", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    foreach (DataRow item in dtJson.Rows)
                    {
                        stringList.Add(item["STAFF_DEPARTMENT"].ToString());
                    }
                }

                autoComplete.AddRange(stringList.ToArray());
                txtWorkCenter.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtWorkCenter.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtWorkCenter.AutoCompleteCustomSource = autoComplete;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadOrgInfo()
        {
            List<string> stringactoryRegionList = new List<string>();
            AutoCompleteStringCollection autoComplete2 = new AutoCompleteStringCollection();

            Dictionary<string, object> parm2 = new Dictionary<string, object>();
            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOrgInfo", Program.client.UserToken, JsonConvert.SerializeObject(parm2));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                dtJson = null;
                dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    foreach (DataRow item in dtJson.Rows)
                    {
                        stringactoryRegionList.Add(item["org"].ToString());
                    }
                }

                autoComplete2.AddRange(stringactoryRegionList.ToArray());
                txtFactoryRegion.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtFactoryRegion.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtFactoryRegion.AutoCompleteCustomSource = autoComplete2;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (comboBoxReportType.SelectedIndex == 0)
            {
                dt = dataGridViewWorkingHours.DataSource as DataTable;
                if (dt != null)
                {
                    ExportDataToExcel(dt, "工时记录(明细表)");
                }
                else
                {
                    MessageHelper.ShowErr(this, "No query records，Please click query first！");
                }
            }
            else
            {
                dt = dataGridViewEmpNo.DataSource as DataTable;
                if (dt != null)
                {
                    ExportDataToExcel(dt, "工时记录(以员工汇总)");
                }
                else
                {
                    MessageHelper.ShowErr(this, "There is no query record, please click query first!");
                }
            }
        }

        public void ExportDataToExcel(DataTable TableName, string FileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //set file title
            saveFileDialog.Title = "导出Excel文件";
            //set file type
            saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls";
            //Set default file type display order  
            saveFileDialog.FilterIndex = 1;
            //Whether to automatically add extensions to filenames
            saveFileDialog.AddExtension = true;
            //Whether to remember the last opened directory
            saveFileDialog.RestoreDirectory = true;
            //set default filename
            saveFileDialog.FileName = FileName;
            //Press the button to confirm the selection  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //get file path 
                string localFilePath = saveFileDialog.FileName;

                //Data initialization
                int TotalCount; //total number of rows
                int RowRead = 0; //lines read
                int Percent = 0; //percentage

                TotalCount = TableName.Rows.Count;
                //NPOI export records
                IWorkbook workbook;
                string FileExt = Path.GetExtension(localFilePath).ToLower();
                if (FileExt == ".xlsx")
                {
                    workbook = new XSSFWorkbook();
                }
                else if (FileExt == ".xls")
                {
                    workbook = new HSSFWorkbook();
                }
                else
                {
                    workbook = null;
                }

                if (workbook == null)
                {
                    return;
                }

                ISheet sheet = string.IsNullOrEmpty(FileName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(FileName);

                //seconds
                Stopwatch timer = new Stopwatch();
                timer.Start();

                try
                {
                    string[] titles;
                    if (comboBoxReportType.SelectedIndex == 0)
                    {
                        titles = new[]
                        {
                            "序号",
                            "员工编号",
                            "员工姓名",
                            "厂区",
                            "工作中心",
                            "日期",
                            "班次代号",
                            "打卡次数",
                            "打卡记录",
                            "工时",
                        };
                    }
                    else
                    {
                        titles = new[]
                        {
                            "序号",
                            "员工编号",
                            "员工姓名",
                            "日期由",
                            "日期至",
                            "工时"
                        };
                    }

                    //read title  
                    IRow rowHeader = sheet.CreateRow(0);
                    //for (int i = 0; i < TableName.Columns.Count; i++)
                    //{
                    //    ICell cell = rowHeader.CreateCell(i);
                    //    cell.SetCellValue(TableName.Columns[i].ColumnName);
                    //}

                    for (int i = 0; i < titles.Length; i++)
                    {
                        ICell cell = rowHeader.CreateCell(i);
                        cell.SetCellValue(titles[i]);
                    }

                    //read data  
                    for (int i = 0; i < TableName.Rows.Count; i++)
                    {
                        IRow rowData = sheet.CreateRow(i + 1);
                        for (int j = 0; j < TableName.Columns.Count; j++)
                        {
                            ICell cell = rowData.CreateCell(j);
                            cell.SetCellValue(TableName.Rows[i][j].ToString());
                        }

                        //Status bar display
                        RowRead++;
                        Percent = 100 * RowRead / TotalCount;
                        Application.DoEvents();
                    }

                    Application.DoEvents();

                    //Convert to byte array  
                    MemoryStream stream = new MemoryStream();
                    workbook.Write(stream);
                    var buf = stream.ToArray();

                    //Save as Excel file  
                    if (localFilePath != null)
                    {
                        using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(buf, 0, buf.Length);
                            fs.Flush();
                            fs.Close();
                        }

                        Application.DoEvents();

                        //close seconds
                        timer.Reset();
                        timer.Stop();

                        //success tips
                        if (MessageBox.Show("The export was successful, do you want to open it now?", "hint", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Process.Start(localFilePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    //close seconds
                    timer.Reset();
                    timer.Stop();
                }
            }
        }


        /// <summary>
        ///     Get hours
        /// </summary>
        private DataTable GetWorkTime(string shift_code)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(shift_code))
            {
                parm.Add("shift_code", shift_code);
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetWorkTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                return dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return null;
        }

        private void btnAutoSync_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("status", 0); //online record。
            parm.Add("syncStartTime", dateTimePickerStartTime.Value.Date);
            parm.Add("syncEndTime", dateTimePickerEndTime.Value.Date.AddDays(1).AddSeconds(-1));

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOnlineAndOfflineDeptsBySyncTime", Program.client.UserToken, JsonConvert.SerializeObject(parm));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtDeptNos = JsonConvert.DeserializeObject<DataTable>(json);

                if (dtDeptNos != null && dtDeptNos.Rows.Count > 0)
                {
                    int totalCount = 0;
                    foreach (DataRow row in dtDeptNos.Rows) //As long as dtDeptNos is not empty, dtDeptNos.Rows.Count=0, foreach can also read in a loop without error.
                    {
                        DataTable workTimeDt = GetWorkTime(row["shiftCode"].ToString());
                        if (workTimeDt != null && workTimeDt.Rows.Count > 0)
                        {
                            Dictionary<string, object> parm2 = new Dictionary<string, object>();
                            parm2.Add("morningOnTime", workTimeDt.Rows[0]["am_from"].ToString());
                            parm2.Add("morningOffTime", workTimeDt.Rows[0]["am_to"].ToString());
                            parm2.Add("afternoonOnTime", workTimeDt.Rows[0]["pm_from"].ToString());
                            parm2.Add("afternoonEndTime", workTimeDt.Rows[0]["pm_to"].ToString());
                            parm2.Add("shiftCode", row["shiftCode"].ToString());
                            parm2.Add("startTime", row["startTime"].ToString());
                            parm2.Add("empNo", row["empNo"].ToString());
                            parm2.Add("onlinetime", row["onlinetime"].ToString());
                            parm2.Add("status", 0);
                            parm2.Add("deptNo", row["deptno"].ToString());

                            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "OverTimeCount", Program.client.UserToken, JsonConvert.SerializeObject(parm2));
                            var retJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2);
                            if (Convert.ToBoolean(retJson2["IsSuccess"]))
                            {
                                string jsonOverTimeCount = retJson2["RetData"].ToString();
                                totalCount += JsonConvert.DeserializeObject<int>(jsonOverTimeCount);
                            }
                            else
                            {
                                MessageHelper.ShowErr(this, retJson2["ErrMsg"].ToString());
                                return;
                            }
                        }
                    }

                    if (totalCount > 0)
                    {
                        MessageHelper.ShowSuccess(this, $"Sync succeeded，total{totalCount}Record");
                    }
                }
                else
                {
                    MessageBox.Show("No data to sync");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }
        }

        private void btnHandHourSync_Click(object sender, EventArgs e)
        {
            int totalCount = 0;
            int errorCount = 0;
            Dictionary<string, object> parm2 = new Dictionary<string, object>();
            parm2.Add("status", 0);//online record
            parm2.Add("syncStartTime", dateTimePickerStartTime.Value.Date);
            parm2.Add("syncEndTime", dateTimePickerEndTime.Value.Date.AddDays(1).AddSeconds(-1));
            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "UpdateOnlineAndOfflineCount", Program.client.UserToken, JsonConvert.SerializeObject(parm2));
            var retJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2);
            if (Convert.ToBoolean(retJson2["IsSuccess"]))
            {
                string jsonOverTimeCount = retJson2["RetData"].ToString();
                totalCount += JsonConvert.DeserializeObject<int>(jsonOverTimeCount);
            }
            else
            {
                errorCount += 1;
                MessageHelper.ShowErr(this, retJson2["ErrMsg"].ToString());
                return;
            }

            if (totalCount > 0)
            {
                MessageHelper.ShowSuccess(this, $"Sync succeeded，total{totalCount}Record");
            }
            else
            {
                MessageBox.Show("No data to sync");
            }

            if (errorCount > 0)
            {
                MessageBox.Show($"Failed to Synchronize Together{errorCount}Article data");
            }
        }
    }
}