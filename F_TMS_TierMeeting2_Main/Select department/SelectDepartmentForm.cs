using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    public partial class SelectDepartmentForm : MaterialForm
    {
        private string plant = "";
        private string section = "";
        public delegate void DataChangeHandler(object sender, DataChangeEventArgs args);
        public event DataChangeHandler DataChange;
        public SelectDepartmentForm(int type, string plant, string section)
        {
            this.plant = plant;
            this.section = section;
            InitializeComponent();
            switch (type) {
                case (int) Parameters.QueryDeptType.Plant: //plant
                    gridLine.Visible = false;
                    lblLine.Visible = false;
                    gridSection.Visible = false;
                    lblSection.Visible = false;
                    break;
                case (int)Parameters.QueryDeptType.Section: //section
                    gridLine.Visible = false;
                    lblLine.Visible = false;
                    break;
                case (int)Parameters.QueryDeptType.Line: //line
                    break;
                default: //line
                    break;
            }
        }

        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            int height = base.Height;
            gridPlant.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridPlant.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridPlant.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridPlant.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridPlant.ColumnHeadersVisible = false;
            gridPlant.AllowUserToAddRows = false;
            gridPlant.ReadOnly = true;
            gridSection.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridSection.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridSection.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridSection.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridSection.ColumnHeadersVisible = false;
            gridSection.AllowUserToAddRows = false;
            gridSection.ReadOnly = true;
            gridLine.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridLine.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridLine.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridLine.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridLine.ColumnHeadersVisible = false;
            gridLine.AllowUserToAddRows = false;
            gridLine.ReadOnly = true;
            lblPlant.Size = new Size(173, 35);
            lblPlant.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblSection.Size = new Size(173, 35);
            lblSection.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblLine.Size = new Size(173, 35);
            lblLine.Font = new Font("宋体", 18f, FontStyle.Regular, GraphicsUnit.Point, 134);
        }
        private void UpdateGridPlant()
        {
            if (plant == "")
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("plant", "");
                p.Add("section", "");
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "GetDeptList",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    gridPlant.DataSource = dtJson;
                    if (dtJson.Rows.Count > 0)
                        gridPlant.Columns[1].Visible = false;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else {
                DataTable dt = new DataTable();
                dt.Columns.Add("code");
                DataRow dr = dt.NewRow();
                dr["code"] = plant;
                dt.Rows.Add(dr);
                gridPlant.DataSource = dt;
                gridPlant.Enabled = false;
                UpdateGridSection("");
            }
            
        }
        private void UpdateGridSection(string plant)
        {
            if (section == "")
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("plant", plant);
                p.Add("section", "");
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.TierMeetingServer",
                                            "GetDeptList",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    gridSection.DataSource = dtJson;
                    if (dtJson.Columns.Count > 1) gridSection.Columns[1].Visible = false;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else {
                DataTable dt = new DataTable();
                dt.Columns.Add("code");
                DataRow dr = dt.NewRow();
                dr["code"] = section;
                dt.Rows.Add(dr);
                gridSection.DataSource = dt;
            }
            
        }
        private void UpdateGridLine(string section)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("plant", "");
            p.Add("section", section);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetDeptList",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                gridLine.DataSource = dtJson;
                if (dtJson.Columns.Count > 1) gridLine.Columns[1].Visible = false;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void gridSection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateGridLine(this.gridSection.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void gridPlant_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateGridSection(this.gridPlant.Rows[e.RowIndex].Cells[0].Value.ToString());
            DataTable dt = (DataTable)gridLine.DataSource;
            if(dt != null) dt.Clear();
            gridLine.Refresh();
        }

        private void gridLine_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string deptCode = gridLine.Rows[e.RowIndex].Cells[0].Value.ToString();
            OnDataChange(this, new DataChangeEventArgs(deptCode, (int)Parameters.QueryDeptType.Line));
            this.Close();
        }
        public void OnDataChange(object sender, DataChangeEventArgs args)
        {
            DataChange?.Invoke(this, args);
        }
        public class DataChangeEventArgs : EventArgs
        {
            public string deptCode { get; set; }
            public int deptType { get; set; }

            public DataChangeEventArgs(string code, int type)
            {
                deptCode = code;
                deptType = type;
            }
        }

        private void gridSection_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string deptCode = gridSection.Rows[e.RowIndex].Cells[0].Value.ToString();
            OnDataChange(this, new DataChangeEventArgs(deptCode, (int)Parameters.QueryDeptType.Section));
            this.Close();
        }

        private void gridPlant_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string deptCode = gridPlant.Rows[e.RowIndex].Cells[0].Value.ToString();
            OnDataChange(this, new DataChangeEventArgs(deptCode, (int)Parameters.QueryDeptType.Plant));
            this.Close();
        }

        private void SelectLineForm_Load(object sender, EventArgs e)
        {
            InitUI();
            UpdateGridPlant();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            OnDataChange(this, new DataChangeEventArgs("", (int)Parameters.QueryDeptType.All));
            this.Close();
        }
    }
}
