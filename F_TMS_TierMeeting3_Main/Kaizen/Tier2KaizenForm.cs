using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    public partial class Tier2KaizenForm : MaterialForm
    {
        private string txtDepartment = "Department";
        private string txtReceived = "Received";
        private string txtAccepted = "Accepted";
        private string txtAcceptedPerPeople = "Accepted/People";
        private string txtAll = "All";
        private int type = 0;
        private string dept = "";
        private string date = "";
        private string plant = "";
        private string section = "";
        private Dictionary<string, double> dic = new Dictionary<string, double>();
        public Tier2KaizenForm(string date, string plant, string section)
        {
            this.date = date;
            this.plant = plant;
            this.section = section;
            InitializeComponent();
        }
        private void DetailKaizenForm_Load(object sender, EventArgs e)
        {
            InitCombobox();
            InitUI();
        }
        private void InitUI()
        {
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cbxMonth.Size = new Size(173, 35);
            cbxLine.Size = new Size(173, 35);
            cbxPlant.Size = new Size(173, 35);
            cbxSection.Size = new Size(173, 35);
            int height = base.Height;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.WindowState = FormWindowState.Maximized;
        }
        private void InitCombobox()
        {
            int month = DateTime.Now.AddDays(-1.0).Month;
            for (var i = 0; i < month; i++)
            {
                cbxMonth.Items.Add(i + 1);
                if (i + 1 == date.ToDate().Month)
                {
                    cbxMonth.Text = (i + 1).ToString();
                }
            }
            InitPlantCombobox();
        }
        private void InitPlantCombobox()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "is null");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxPlant.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxPlant.Items.Add(dtJson.Rows[i][0]);
                }
                
                EnableSectionCombobox(false);
                EnableLineCombobox(false);
                cbxPlant.Text = plant;
                cbxSection.Text = section;
                GetData();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void EnableSectionCombobox(bool temp)
        {
            cbxSection.Enabled = temp;
        }
        private void EnableLineCombobox(bool temp)
        {
            cbxLine.Enabled = temp;
        }
        private void FilterSection(string plant)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "= '" + plant + "'");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxSection.Items.Clear();
                cbxSection.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxSection.Items.Add(dtJson.Rows[i][0]);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void FilterLine(string section)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("parent", "= '" + section + "'");
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbxLine.Items.Clear();
                cbxLine.Items.Add(txtAll);
                for (var i = 0; i < dtJson.Rows.Count; i++)
                {
                    cbxLine.Items.Add(dtJson.Rows[i][0]);
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetData()
        {
            GetDepartmentKaizen();
        }
        private void GetDepartmentKaizen()
        {
            GetTypeAndDept();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDepartmentKaizen",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                InitTable(dtJson);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetKaizenReceived()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string strDate = "";
            if (cbxMonth.Text == date.ToDate().Month.ToString())
            {
                DateTime tempDate = new DateTime(date.ToDate().Year, date.ToDate().Month, date.ToDate().Day);
                strDate = tempDate.ToString("yyyy-MM-dd");
            }
            else
            {
                string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
                int tempDate = DateTime.DaysInMonth(date.ToDate().Year, int.Parse(tempMonth));
                strDate = date.ToDate().Year.ToString() + "-" + tempMonth + "-" + tempDate;
            }
            string firstDateOfYear = date.ToDate().Year.ToString() + "-01-01";
            p.Add("date", strDate);
            p.Add("firstDateOfYear", firstDateOfYear);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenReceived",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                gridData.Rows.Add();
                gridData.Rows[0].Cells[0].Value = txtReceived;
                if (dtJson.Rows.Count > 0)
                {
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        for (int j = 1; j < gridData.Columns.Count; j++)
                        {
                            if (dtJson.Rows[i][0].ToString() == gridData.Columns[j].Name)
                            {
                                gridData.Rows[0].Cells[j].Value = dtJson.Rows[i][1].ToInt();
                            }
                            else
                            {
                                if (gridData.Rows[0].Cells[j].Value == null) gridData.Rows[0].Cells[j].Value = 0;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < gridData.Columns.Count; i++)
                    {
                        gridData.Rows[0].Cells[i].Value = 0;
                    }
                }
                GetKaizenAccepted();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetKaizenAccepted()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string strDate = "";
            if (cbxMonth.Text == date.ToDate().Month.ToString())
            {
                DateTime tempDate = new DateTime(date.ToDate().Year, date.ToDate().Month, date.ToDate().Day);
                strDate = tempDate.ToString("yyyy-MM-dd");
            }
            else
            {
                string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
                int tempDate = DateTime.DaysInMonth(date.ToDate().Year, int.Parse(tempMonth));
                strDate = date.ToDate().Year.ToString() + "-" + tempMonth + "-" + tempDate;
            }
            string firstDateOfYear = date.ToDate().Year.ToString() + "-01-01";
            p.Add("date", strDate);
            p.Add("firstDateOfYear", firstDateOfYear);
            p.Add("dept", dept);
            p.Add("type", type);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetKaizenAccepted",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                gridData.Rows.Add();
                gridData.Rows.Add();
                gridData.Rows[1].Cells[0].Value = txtAccepted;
                gridData.Rows[2].Cells[0].Value = txtAcceptedPerPeople;
                gridData.Columns[0].Width = 300;
                if (dtJson.Rows.Count > 0)
                {
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        for (int j = 1; j < gridData.Columns.Count; j++)
                        {
                            if (dtJson.Rows[i][0].ToString() == gridData.Columns[j].Name)
                            {
                                gridData.Rows[1].Cells[j].Value = dtJson.Rows[i][1].ToInt();
                                var tmp = dic.FirstOrDefault(t => t.Key == gridData.Columns[j].Name);
                                gridData.Rows[2].Cells[j].Value = Math.Round((dtJson.Rows[i][1].ToDouble() * 100) / (tmp.Value), 2) + "%";
                            }
                            else
                            {
                                if (gridData.Rows[1].Cells[j].Value == null) gridData.Rows[1].Cells[j].Value = 0;
                                if (gridData.Rows[2].Cells[j].Value == null) gridData.Rows[2].Cells[j].Value = 0 + "%";
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < gridData.Columns.Count; i++)
                    {
                        gridData.Rows[1].Cells[i].Value = 0;
                        gridData.Rows[2].Cells[i].Value = "0%";
                    }
                }
                foreach (DataGridViewColumn column in gridData.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void InitTable(DataTable dtJson)
        {
            gridData.Columns.Clear();
            gridData.Rows.Clear();
            dic.Clear();
            gridData.Columns.Add(txtDepartment, txtDepartment);
            for (int i = 0; i < dtJson.Rows.Count; i++)
            {
                gridData.Columns.Add(dtJson.Rows[i][0].ToString(), dtJson.Rows[i][0].ToString());
                dic.Add(dtJson.Rows[i][0].ToString(), dtJson.Rows[i][1].ToDouble());
            }
            GetKaizenReceived();
        }
        private void cbxPlant_TextChanged(object sender, EventArgs e)
        {
            if (cbxPlant.Text == txtAll)
            {
                EnableSectionCombobox(false);
                EnableLineCombobox(false);
                cbxSection.Text = txtAll;
                cbxLine.Text = txtAll;
            }
            else
            {
                FilterSection(cbxPlant.Text);
                EnableSectionCombobox(true);
                cbxSection.Text = txtAll;
            }
        }

        private void cbxSection_TextChanged(object sender, EventArgs e)
        {
            if (cbxSection.Text == txtAll)
            {
                EnableLineCombobox(false);
                cbxLine.Text = txtAll;
            }
            else
            {
                FilterLine(cbxSection.Text);
                EnableLineCombobox(true);
                cbxLine.Text = txtAll;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetTypeAndDept()
        {
            type = 0;
            if (cbxPlant.Text != txtAll)
            {
                if (cbxSection.Text != txtAll)
                {
                    if (cbxLine.Text != txtAll)
                    {
                        type = 3; //get by line
                        dept = cbxLine.Text;
                    }
                    else
                    {
                        type = 2; //get by section
                        dept = cbxSection.Text;
                    }
                }
                else
                {
                    type = 1; //get by plant
                    dept = cbxPlant.Text;
                }
            }
        }

        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void cbxMonth_TextChanged(object sender, EventArgs e)
        {
            string tempMonth = (cbxMonth.Text.Length == 1) ? "0" + cbxMonth.Text : cbxMonth.Text;
            int tempDate = DateTime.DaysInMonth(date.ToDate().Year, int.Parse(cbxMonth.Text));
            date = date.ToDate().Year.ToString() + "-" + tempMonth + "-" + tempDate;
        }
    }
}
