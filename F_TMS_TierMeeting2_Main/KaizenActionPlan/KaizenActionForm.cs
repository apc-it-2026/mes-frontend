using MaterialSkin.Controls;
using Newtonsoft.Json;
using ProductionEfficiencyForm;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    public partial class KaizenActionForm : MaterialForm
    {
        private int tempColIndex = 0;
        private int tempRowIndex = 0;
        private Rectangle rectangle;
        private string date = "";
        private string dept = "";
        private string userPlant = "";
        private string userSection = "";
        private int type = 0;
        public KaizenActionForm(string date, string dept, int type, string userPlant, string userSection)
        {
            //this.date = date;
            this.date = DateTime.Now.ToString(Parameters.dateFormat);
            this.dept = dept;
            this.type = type;
            this.userPlant = userPlant;
            this.userSection = userSection;
            InitializeComponent();
        }
        private void KaizenAction_Load(object sender, EventArgs e)
        {
            InitUI();
            GetData();
        }
        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            int width = base.Width;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 80f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.Columns["G_ITIME"].Width = Convert.ToInt32(width / 30);
            gridData.Columns["colCbx"].Width = Convert.ToInt32(width / 30);
            gridData.Columns["G_T1"].Width = Convert.ToInt32(width / 40);
            gridData.Columns["G_T2"].Width = Convert.ToInt32(width / 40);
            gridData.Columns["G_T3"].Width = Convert.ToInt32(width / 40);
            gridData.Columns["G_T4"].Width = Convert.ToInt32(width / 40); 
            gridData.Columns["G_DEPTCODE"].Width = Convert.ToInt32(width / 20); 
            gridData.Columns["G_GREATEDDATE"].Width = Convert.ToInt32(width / 13);
            gridData.Columns["G_FINDER"].Width = Convert.ToInt32(width / 15);
            gridData.Columns["G_PROBLEMPOINT"].Width = Convert.ToInt32(width / 7);
            gridData.Columns["G_MEASURE"].Width = Convert.ToInt32(width / 7);
            gridData.Columns["G_PRINCTIPAL"].Width = Convert.ToInt32(width / 15);
            gridData.Columns["G_PLANDATE"].Width = Convert.ToInt32(width / 13);
            gridData.Columns["G_FINISHDATE"].Width = Convert.ToInt32(width / 13);
            gridData.Columns["G_REMARK"].Width = Convert.ToInt32(width / 10);
            gridData.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            txtDepartment.Text = dept;
            txtDepartment.Size = new Size(100, (int)height / 60);
            txtDepartment.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblFrom.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblTo.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblDepartment.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            cbClosed.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpFrom.Size = new Size(150, (int)height / 60);
            dtpFrom.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = Parameters.dateFormat;
            string temp = date.ToDate().Year.ToString() + "/" + date.ToDate().Month.ToString() + "/" + "01"; ;
            dtpFrom.Text = temp;
            dtpTo.Size = new Size(150, (int)height / 60);
            dtpTo.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = Parameters.dateFormat;
            dtpTo.Text = date;
            dtp.Size = new Size(173, (int)height / 60);
            dtp.Font = new Font("宋体", (float)height / 75f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = Parameters.dateFormat;
            dtp.Visible = false;
            //dtp.TextChanged += new EventHandler(dtp_TextChange);
            dtp.CloseUp += new EventHandler(dtp_TextChange);
        }
        private void GetData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("from", dtpFrom.Text);
            p.Add("to", dtpTo.Text);
            p.Add("type", type);
            p.Add("dept", dept);
            p.Add("process", 2);
            p.Add("isClosed", cbClosed.Checked);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenAction",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                //DataTable dtJson = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                gridData.DataSource = dtJson;
                gridData.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    ReadOnlyCell(i, true);
                }
            }
            else
            {
                DataTable dt = (DataTable)gridData.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                gridData.Refresh();
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
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
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }


        private void dtp_TextChange(object sender, EventArgs e)
        {
            gridData.Rows[tempRowIndex].Cells[tempColIndex].Value = dtp.Text.ToString();
            dtp.Visible = false;
        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = this.gridData.CurrentRow.Index;
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            bool flag = index > -1 && this.gridData.Rows[index].ReadOnly == false;
            if (flag)
            {
                var colName = gridData.Columns[e.ColumnIndex].Name;
                    if (colName == "G_GREATEDDATE" //created date 
                        || colName == "G_PLANDATE") //plan date
                    {
                        rectangle = gridData.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true); //  
                        dtp.Size = new Size(rectangle.Width, rectangle.Height);
                        dtp.Location = new Point(rectangle.X + 5, rectangle.Y + 130);
                    if (gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) dtp.Text = gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        dtp.Visible = true;
                        tempColIndex = e.ColumnIndex;
                        tempRowIndex = e.RowIndex;
                    }
                    else
                    {
                        dtp.Visible = false;
                    }
            }
        }

        private void gridData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dtp.Visible = false;
        }

        private void gridData_Scroll(object sender, ScrollEventArgs e)
        {
            //dtp.Visible = false;
        }

        private void gridData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (CheckEditableRow()) return; //editing

            if (e.ColumnIndex == gridData.Columns["colCbx"].Index) {
                bool flag = gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToBool();
                foreach (DataGridViewRow r in gridData.Rows) {
                    r.Cells["colCbx"].Value = false;
                }
                gridData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !flag;
            }
        }
        private int GetSelectedRow() {
            int rowIndex = -1;
            foreach (DataGridViewRow r in gridData.Rows) {
                if (r.Cells["colCbx"].Value.ToBool()) {
                    rowIndex = r.Index;
                }
            }
            return rowIndex;
		}
        private bool CheckIsFinished(int rowIndex)
        {
            bool result = !gridData.Rows[rowIndex].Cells["G_FINISHDATE"].Value.IsEmpty();
            return result;
        }
        private void SaveAction(int rowIndex)
        {
            if (rowIndex < 0) {
                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00002", Program.client, "", Program.client.Language));
                return;
            }
            int id = gridData.Rows[rowIndex].Cells["G_ITIME"].Value.ToInt();
            string department = gridData.Rows[rowIndex].Cells["G_DEPTCODE"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_DEPTCODE"].Value.ToString().ToUpper();
            if (string.IsNullOrEmpty(department))
            {
                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00001", Program.client, "", Program.client.Language));
                return;
            }
            string finder = gridData.Rows[rowIndex].Cells["G_FINDER"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_FINDER"].Value.ToString();
            string problem = gridData.Rows[rowIndex].Cells["G_PROBLEMPOINT"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_PROBLEMPOINT"].Value.ToString();
            string measure = gridData.Rows[rowIndex].Cells["G_MEASURE"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_MEASURE"].Value.ToString();
            string principal = gridData.Rows[rowIndex].Cells["G_PRINCTIPAL"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_PRINCTIPAL"].Value.ToString();
            string remark = gridData.Rows[rowIndex].Cells["G_REMARK"].Value == null ? "" : gridData.Rows[rowIndex].Cells["G_REMARK"].Value.ToString();
            if (gridData.Rows[rowIndex].Cells["G_GREATEDDATE"].Value == null) {
                MessageHelper.ShowWarning(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00001", Program.client, "", Program.client.Language));
                return;
            }
            string createdDate = gridData.Rows[rowIndex].Cells["G_GREATEDDATE"].Value.ToString();
            if (gridData.Rows[rowIndex].Cells["G_PLANDATE"].Value == null) gridData.Rows[rowIndex].Cells["G_PLANDATE"].Value = "";
            string planDate = gridData.Rows[rowIndex].Cells["G_PLANDATE"].Value.ToString();
            if (gridData.Rows[rowIndex].Cells["G_FINISHDATE"].Value == null) gridData.Rows[rowIndex].Cells["G_FINISHDATE"].Value = "";
            string finishDate = gridData.Rows[rowIndex].Cells["G_FINISHDATE"].Value.ToString();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("id", id);
            p.Add("department", department);
            p.Add("finder", finder);
            p.Add("problem", problem);
            p.Add("measure", measure);
            p.Add("principal", principal);
            p.Add("remark", remark);
            p.Add("createdDate", createdDate);
            p.Add("planDate", planDate);
            p.Add("finishDate", finishDate);
            if (id != 0) //edit
            {
                string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "EditKaizenAction",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else
            { //create
                switch (type)
                {
                    case (int)Parameters.QueryDeptType.All:
                        p.Add("T1", "");
                        p.Add("T2", "");
                        p.Add("T3", "");
                        p.Add("T4", "P");
                        break;
                    case (int)Parameters.QueryDeptType.Plant:
                        p.Add("T1", "");
                        p.Add("T2", "");
                        p.Add("T3", "P");
                        p.Add("T4", "");
                        break;
                    case (int)Parameters.QueryDeptType.Section:
                        p.Add("T1", "");
                        p.Add("T2", "P");
                        p.Add("T3", "");
                        p.Add("T4", "");
                        break;
                    case (int)Parameters.QueryDeptType.Line:
                        p.Add("T1", "P");
                        p.Add("T2", "");
                        p.Add("T3", "");
                        p.Add("T4", "");
                        break;
                    default:
                        break;
                }
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "CreateKaizenAction",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
        private void UpdateStatus(int rowIndex, string action)
        {
            if (dtp.Visible) return;
            if (rowIndex < 0) {
                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00002", Program.client, "", Program.client.Language));
                return;
            }
            int id = gridData.Rows[rowIndex].Cells["G_ITIME"].Value.ToInt();
            if(id != 0) {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("id", id);
                if (action == "C")
                {
                    if (id == 0)
                    {
                        MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00003", Program.client, "", Program.client.Language));
                        return;
                    }
                    string T1 = gridData.Rows[rowIndex].Cells["G_T1"].Value.ToString();
                    string T2 = gridData.Rows[rowIndex].Cells["G_T2"].Value.ToString();
                    string T3 = gridData.Rows[rowIndex].Cells["G_T3"].Value.ToString();
                    string T4 = gridData.Rows[rowIndex].Cells["G_T4"].Value.ToString();
                    if (!string.IsNullOrEmpty(T3)) {
                        MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00007", Program.client, "", Program.client.Language));
                        return;
                    } 
                    if (T1 == "C" || T2 == "C" || T3 == "C" || T4 == "C")
                    {
                        //closed
                        MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00003", Program.client, "", Program.client.Language));
                        return;
                    }
                    T1 = T1 != "" ? "C" : "";
                    T2 = T2 != "" ? "C" : "";
                    T3 = T3 != "" ? "C" : "";
                    T4 = T4 != "" ? "C" : "";
                    p.Add("T1", T1);
                    p.Add("T2", T2);
                    p.Add("T3", T3);
                    p.Add("T4", T4);
                }
                else if (action == "N")
                {
                    if (id == 0)
                    {
                        MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00004", Program.client, "", Program.client.Language));
                        return;
                    }
                    string T1 = gridData.Rows[rowIndex].Cells["G_T1"].Value.ToString();
                    string T2 = gridData.Rows[rowIndex].Cells["G_T2"].Value.ToString();
                    string T3 = gridData.Rows[rowIndex].Cells["G_T3"].Value.ToString();
                    string T4 = gridData.Rows[rowIndex].Cells["G_T4"].Value.ToString();
                    if (T4 == "P")
                    {
                        //Tier 4
                        MessageHelper.ShowWarning(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00004", Program.client, "", Program.client.Language));
                        return;
                    }
                    if (T1 == "C" || T2 == "C" || T3 == "C" || T4 == "C")
                    {
                        //closed
                        MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00004", Program.client, "", Program.client.Language));
                        return;
                    }
                    else
                    {
                        if (T1 == "P" || T1 == "N")
                        {
                            T1 = "N";
                        }
                        else
                        {
                            T1 = "";
                        }
                        if (T2 == "P" || T2 == "N")
                        {
                            T2 = "N";
                        }
                        else
                        {
                            T2 = "";
                        }
                        if (T3 == "P" || T3 == "N")
                        {
                            T3 = "N";
                        }
                        else
                        {
                            T3 = "";
                        }
                        string[] arr = new string[4];
                        arr[0] = T1;
                        arr[1] = T2;
                        arr[2] = T3;
                        arr[3] = T4;
                        int i = 2;
                        do
                        {
                            if (arr[i] == "N")
                            {
                                arr[i + 1] = "P";
                                break;
                            }
                            i--;
                        }
                        while (i >= 0);
                        T1 = arr[0];
                        T2 = arr[1];
                        T3 = arr[2];
                        T4 = arr[3];
                        p.Add("T1", T1);
                        p.Add("T2", T2);
                        p.Add("T3", T3);
                        p.Add("T4", T4);
                    }
                }
                string ret =
                        SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.client.APIURL,
                            "TierMeeting",
                            "TierMeeting.Controllers.TierMeetingServer",
                                                "UpdateStatus",
                            Program.client.UserToken,
                            JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            
        }
        private void ReadOnlyCell(int rowIndex, bool flag)
        {
            gridData.Rows[rowIndex].ReadOnly = flag;
            if (!flag)
            {
                gridData.Rows[rowIndex].Cells["colCbx"].ReadOnly = true;
                gridData.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
            }
            else
            {
                gridData.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (dtp.Visible) return;
            GetData();
        }
        private void gridData_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["G_GREATEDDATE"].Value = DateTime.Now.ToString(Parameters.dateFormat);
            e.Row.Cells["G_DEPTCODE"].Value = dept;
        }

        private void gridData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
        }

        private void txtDepartment_DoubleClick(object sender, EventArgs e)
        {
            SelectDepartmentForm frm = new SelectDepartmentForm((int)Parameters.QueryDeptType.Line,userPlant,userSection);
            frm.DataChange += new SelectDepartmentForm.DataChangeHandler(DeptSelected);
            frm.ShowDialog();
        }
        private void DeptSelected(object sender, SelectDepartmentForm.DataChangeEventArgs args)
        {
            txtDepartment.Text = args.deptCode;
            dept = args.deptCode;
            type = args.deptType;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int rowIndex = GetSelectedRow();
            if (rowIndex > -1)
            {
				if (CheckIsFinished(rowIndex)) {
                    MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00006", Program.client, "", Program.client.Language));
                    return;
                } 
                ReadOnlyCell(rowIndex, false);
            }
            else
            {
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtp.Visible) return;
            SaveAction(GetSelectedRow());
        }

        private void btnUpgrade_Click(object sender, EventArgs e)
        {
            UpdateStatus(GetSelectedRow(), "N");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            UpdateStatus(GetSelectedRow(), "C");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckEditableRow()) {
                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00005", Program.client, "", Program.client.Language));
                return;
            }
            DataTable dt = (DataTable) gridData.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            else
            {
                gridData.Rows.Add();
            }
            ReadOnlyCell(gridData.Rows.Count - 1, false);
            foreach (DataGridViewRow r in gridData.Rows)
            {
                r.Cells["colCbx"].Value = false;
            }
            gridData.Rows[gridData.Rows.Count - 1].Cells["colCbx"].Value = true;
            gridData.Rows[gridData.Rows.Count - 1].Cells["G_GREATEDDATE"].Value = this.date.Replace("-", "/");
            gridData.Rows[gridData.Rows.Count - 1].Cells["G_DEPTCODE"].Value = dept;
        }
        private bool CheckEditableRow() {
            bool flag = false;
            foreach (DataGridViewRow r in gridData.Rows) {
                if (r.DefaultCellStyle.BackColor == Color.Aquamarine) flag = true;
            }
            return flag;
        }

        private void dtp_MouseEnter(object sender, EventArgs e)
        {
            gridData.CurrentCell.Value = dtp.Text.ToString();
        }
        #region Export - Import
        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel file|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = "'" + myDGV.Columns[i].HeaderText.ToString();
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();
            MessageBox.Show("保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            string a = "KaizenActionPlan.xlsx";
            ExportExcels(a, gridData);
        }
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "EXCEL|*.xlsx*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (gridData != null)
                {
                    data = new List<object[]>();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.GetExcelData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message);
                    }
                }
                DataTable dtTemp = (DataTable)gridData.DataSource;
                dtTemp.Rows.Clear();
                foreach (object obj in data)
                {
                    DataRow dr = dtTemp.NewRow();
                    for (var i = 1; i < dtTemp.Columns.Count; i++)
                    {
                        dr[i] = ((object[])obj)[i + 1];
                    }
                    dtTemp.Rows.Add(dr);
                }
                dtTemp.Rows[0].Delete();

                Dictionary<string, Object> d = new Dictionary<string, object>();
                d.Add("data", dtTemp);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "TierMeeting", "TierMeeting.Controllers.TierMeetingServer", "UpLoadKaizenAction",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入成功！", Program.client, "", Program.client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, msg);
                    GetData();
                }
                else
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("导入失败！", Program.client, "", Program.client.Language);
                    string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
                }
            }

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
        #endregion

        private void btnUnClose_Click(object sender, EventArgs e)
        {
            UpdateUnClose(GetSelectedRow());
        }
        private void UpdateUnClose(int rowIndex)
        {
            if (dtp.Visible) return;
            if (rowIndex < 0)
            {
                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00002", Program.client, "", Program.client.Language));
                return;
            }
            int id = gridData.Rows[rowIndex].Cells["G_ITIME"].Value.ToInt();
            if (id != 0)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("id", id);
                if (id == 0)
                {
                    MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00004", Program.client, "", Program.client.Language));
                    return;
                }
                string T1 = gridData.Rows[rowIndex].Cells["G_T1"].Value.ToString();
                string T2 = gridData.Rows[rowIndex].Cells["G_T2"].Value.ToString();
                string T3 = gridData.Rows[rowIndex].Cells["G_T3"].Value.ToString();
                string T4 = gridData.Rows[rowIndex].Cells["G_T4"].Value.ToString();
                if (!string.IsNullOrEmpty(T3))
                {
                    MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-kzap-err:00007", Program.client, "", Program.client.Language));
                    return;
                }
                //Tier 2
                T2 = "P";
                T1 = string.IsNullOrEmpty(T1) ? T1 : "N";
                //
                p.Add("T1", T1);
                p.Add("T2", T2);
                p.Add("T3", T3);
                p.Add("T4", T4);
                string ret =
                        SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                            Program.client.APIURL,
                            "TierMeeting",
                            "TierMeeting.Controllers.TierMeetingServer",
                                                "UpdateUnClose",
                            Program.client.UserToken,
                            JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {

                    GetData();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
    }
}
