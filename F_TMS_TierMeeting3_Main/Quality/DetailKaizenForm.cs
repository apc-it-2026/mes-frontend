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
    public partial class DetailKaizenForm : MaterialForm
    {
        private int type = 0;
        private string dept = "";
        private DateTime currentDate;
        private Dictionary<string, double> dic = new Dictionary<string, double>();
        public DetailKaizenForm(DateTime currentDate, string dept, int type)
        {
            this.currentDate = currentDate;
            var tempDate = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            this.currentDate = new DateTime(currentDate.Year, currentDate.Month, tempDate);
            this.type = type;
            this.dept = dept;
            InitializeComponent();
        }
        private void DetailKaizenForm_Load(object sender, EventArgs e)
        {
            InitCombobox();
            InitUI();
            GetData();
        }
        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cbxMonth.Size = new Size(173, 35);
            cbxMonth.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblMonth.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);

        }
        private void InitCombobox()
        {
            int month = DateTime.Now.AddDays(-1.0).Month;
            for (var i = 0; i < month; i++)
            {
                if (i < 9)
                {
                    cbxMonth.Items.Add("0"+(i + 1).ToString());
                }
                else
                {
                    cbxMonth.Items.Add((i + 1).ToString());
                }
                if (i + 1 == currentDate.Month)
                {
                    if (i < 9)
                    {
                        cbxMonth.Text = "0"+(i + 1).ToString();

                    }
                    else {
                        cbxMonth.Text = (i + 1).ToString();
                    }
                    
                }
            }
        }
        
        private void GetData()
        {
            GetKaizenData();
        }
        private void GetKaizenData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("date", currentDate.ToString(Parameters.dateFormat));
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenData",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                
                gridData.DataSource = Pivot(dtJson);
                gridData.Columns[0].Width = 300;
                ColorManage();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private DataTable Pivot(DataTable dtJson)
        {
            string[] uniqueUnits = dtJson.AsEnumerable().Select(x => x.Field<string>("DEPARTMENT")).Distinct().ToArray();

            DataTable dt1 = new DataTable();
            dt1.Columns.Add(lblDepartmentText.Text, typeof(string));
            foreach (string unit in uniqueUnits)
            {
                dt1.Columns.Add(unit, typeof(string));
            }
            dt1.Rows.Add();
            dt1.Rows.Add();
            dt1.Rows.Add();
            dt1.Rows[0][lblDepartmentText.Text] = lblReceivedText.Text;
            dt1.Rows[1][lblDepartmentText.Text] = lblAcceptedText.Text;
            dt1.Rows[2][lblDepartmentText.Text] = lblReceivedPersonText.Text;
            var groups = dtJson.AsEnumerable().GroupBy(x => x.Field<string>("DEPARTMENT"));
            foreach (var group in groups)
            {
                foreach (DataRow row in group)
                {
                    dt1.Rows[0][group.Key] = row.Field<string>("RECEIVED");
                    dt1.Rows[1][group.Key] = row.Field<string>("ACCEPTED");
                    dt1.Rows[2][group.Key] = row.Field<string>("PERPEOPLE");
                }
            }
            return dt1;
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
       
        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (type == (int)Parameters.QueryDeptType.Section) return;
            int index = this.gridData.CurrentRow.Index;
            string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
            DetailKaizenForm frm = new DetailKaizenForm(currentDate, this.gridData.Columns[e.ColumnIndex].HeaderText, type + 1);
            frm.Show();
        }

        private void cbxMonth_TextChanged(object sender, EventArgs e)
        {
            var tempDate = DateTime.DaysInMonth(currentDate.Year, cbxMonth.Text.ToInt());
            this.currentDate = new DateTime(currentDate.Year, cbxMonth.Text.ToInt(), tempDate);
        }
        private void ColorManage()
        {
            for (int i = 1; i < gridData.Columns.Count; i++) {
                var temp = gridData.Rows[2].Cells[i].Value.ToDouble() * 100;
                if (temp >= Parameters.GreenRate)
                {
                    gridData.Rows[2].Cells[i].Style.BackColor = Color.Green;
                }
                else if (temp >= Parameters.YellowRate)
                {
                    gridData.Rows[2].Cells[i].Style.BackColor = Color.Yellow;
                }
                else 
                {
                    gridData.Rows[2].Cells[i].Style.BackColor = Color.Red;
                }
            }
        }
    }
}
