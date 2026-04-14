using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    public partial class DetailSafetyForm : MaterialForm
    {
        private int days;
        private string dept = "";
        private int type = 0;
        private string date= "";
        public DetailSafetyForm(string date, string dept, int type)
        {
            this.date = date;
            this.dept = dept;
            this.type = type;
            InitializeComponent();
        }

        private void DetailSafety_Load(object sender, EventArgs e)
        {
            InitUI();
            InitCombobox();
        }
        private void InitUI() {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cbxMonth.Size = new Size(50, 35);
            cbxMonth.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblMonth.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblHeader.Font = new Font("宋体", (float)height / 30f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblDept.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblDept.Text = dept;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
        }
        private void GetData() {
            GetTableData();
        }
        private void GetTableData() { 
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
            DateTime tempDate = new DateTime(date.ToDate().Year,tempMonth.ToInt(),1);
            p.Add("firstDateOfMonth", tempDate.ToString(Parameters.dateFormat));
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetSafetyTable",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                UpdateDataTable(dtJson);
                GetChartData();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void InitTable() {
            int year = DateTime.Now.Year;
            int month = int.Parse(cbxMonth.Text);
            days = DateTime.DaysInMonth(year, month);
            gridData.Columns.Add(lblDepartmentText.Text, lblDepartmentText.Text);
            gridData.Columns.Add(lblAmountText.Text, lblAmountText.Text);
            int width = base.Width;
            gridData.Columns[0].Width = (int)width / 10;
            gridData.Columns[1].Width = (int)width / 16;
            for (int day = 1; day <= days; day++)
            {
                DateTime temp = new DateTime(year, month, day);
                gridData.Columns.Add(day.ToString(), day.ToString());
                gridData.Columns[day + 1].Width = (int)width / 40;
            }
        }
        private DataTable Pivot(DataTable dtJson) {
            string[] uniqueUnits = dtJson.AsEnumerable().Select(x => x.Field<string>("CODE")).Distinct().ToArray();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("HAPPENEDDATE", typeof(string));
            foreach (string unit in uniqueUnits)
            {
                dt1.Columns.Add(unit, typeof(int));
            }
            var groups = dtJson.AsEnumerable().GroupBy(x => x.Field<string>("HAPPENEDDATE"));

            foreach (var group in groups)
            {
                DataRow newRow = dt1.Rows.Add();
                foreach (DataRow row in group)
                {
                    newRow["HAPPENEDDATE"] = group.Key;
                    newRow[row.Field<string>("CODE")] = row.Field<string>("COUNT");
                }
            }
            return dt1;
        }
        private void UpdateDataTable(DataTable dtJson) {
            ClearTable();
            InitTable();
            DataTable dtTemp = Pivot(dtJson);
            for (var i = 1; i < dtTemp.Columns.Count; i++) {
                gridData.Rows.Add();
                gridData.Rows[i - 1].Cells[0].Value = dtTemp.Columns[i].ToString();
                gridData.Rows[i - 1].Cells[1].Value = lblAmountText.Text;
                for (var j = 0; j < dtTemp.Rows.Count; j++)
                {
                    for (int day = 1; day <= days; day++)
                    {
                        DateTime dateTemp = dtTemp.Rows[j][0].ToDate();
                        if (dateTemp.Day.ToString() == day.ToString())
                        {
                            gridData.Rows[i-1].Cells[day + 1].Value = (dtTemp.Rows[j][i].ToInt()).ToString();
                            if(dtTemp.Rows[j][i].ToInt() != 0) gridData.Rows[i - 1].Cells[day + 1].Style.BackColor = Color.Red;
                            else gridData.Rows[i - 1].Cells[day + 1].Style.BackColor = Color.Green;

                        }
                        else
                        {
                            if (gridData.Rows[i - 1].Cells[day + 1].Value == null)
                            {
                                gridData.Rows[i - 1].Cells[day + 1].Value = "0";
                                gridData.Rows[i - 1].Cells[day + 1].Style.BackColor = Color.Green;
                            }
                        }
                        bool flag = false;
                        DateTime tempYesterday = DateTime.Now.AddDays(-1);
                        flag = int.Parse(cbxMonth.Text) == tempYesterday.Month;
                        if (flag)
                        {
                            flag = day > tempYesterday.Day;
                        }
                        if (flag)
                        {
                            gridData.Rows[i - 1].Cells[day + 1].Value = null;
                            gridData.Rows[i - 1].Cells[day + 1].Style.BackColor = Color.White;
                        }
                    }
                }
            }
        }
        private void InitCombobox() {
            int month = DateTime.Now.AddDays(-1.0).Month;
            for (var i = 0; i < month; i++) {
                cbxMonth.Items.Add(i+1);
                if (i + 1 == date.ToDate().Month) {
                    cbxMonth.Text = (i+1).ToString();
                }
            }
            GetData();
        }
        private void ClearTable() {
            gridData.Rows.Clear();
            gridData.Columns.Clear();
        }
        private void GetChartData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
            DateTime tempDate = new DateTime(date.ToDate().Year, tempMonth.ToInt(), 1);
            p.Add("firstDateOfMonth", tempDate.ToString(Parameters.dateFormat));
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetSafetyChart",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                InitChart(dtJson);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void InitChart(DataTable dtJson)
        {
            //clear chart
            chartData.Series["Number of accidents"].Points.Clear();
            chartData.Titles.Clear();
            chartData.Series["Number of accidents"].IsValueShownAsLabel = false;
            //chartData.Series["Number of accidents"].Legend[0].;
            chartData.Legends.Clear();
            //init chart
            chartData.Titles.Add(lblChartTitleText.Text);
            int year = DateTime.Now.Year;
            int month = int.Parse(cbxMonth.Text);
            for (int day = 1; day <= days; day++)
            {
                bool flag = false;
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    DateTime dateTemp = dtJson.Rows[i][0].ToDate();
                    if (day.ToString() == dateTemp.Day.ToString())
                    {
                        chartData.Series["Number of accidents"].Points.AddXY(day.ToString(), dtJson.Rows[i][1].ToInt());
                        flag = true;
                    }
                }
                if(!flag) chartData.Series["Number of accidents"].Points.AddXY(day.ToString(), 0);
            }
            var chartSeries = chartData.Series["Number of accidents"];
            var chartArea = chartData.ChartAreas[chartSeries.ChartArea];
            chartArea.CursorX.AutoScroll = true;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.Interval = 1;
            chartArea.AxisX.Title = lblDeptText.Text;
            chartArea.AxisY.Title = lblAmountText.Text;
        }
   
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (type==(int)Parameters.QueryDeptType.Line) return;
            int index = this.gridData.CurrentRow.Index;
            bool flag = index > -1 && this.gridData.Rows[index].Cells[0].Value != null;
            if (flag)
            {
                string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
                DateTime tempDate = new DateTime(date.ToDate().Year, tempMonth.ToInt(), 1);
                DetailSafetyForm frm = new DetailSafetyForm(tempDate.ToString(Parameters.dateFormat), this.gridData.Rows[index].Cells[0].Value.ToString(), type+1);
                frm.Show();
            }
        }
    }
}
