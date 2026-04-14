using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    public partial class DetailQualityForm : MaterialForm
    {
        private int type = 0;
        private string dept = "";
        private string RFTType = "";
        private DateTime currentDate = DateTime.Now;
        private Dictionary<string, double> dic = new Dictionary<string, double>();
        public DetailQualityForm(DateTime currentDate, string dept, int type, string RFTType)
        {
            this.currentDate = currentDate;
            this.type = type;
            this.dept = dept;
            this.RFTType = RFTType;
            InitializeComponent();
        }
        private void DetailKaizenForm_Load(object sender, EventArgs e)
        {
            InitUI();
            GetData();
        }
        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtp.CustomFormat = Parameters.dateFormat;
            dtp.Size = new Size(173, 35);
            dtp.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            dtp.Value = currentDate;
            lblDate.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Bold, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);

        }
        private void GetData()
        {
            GetQualityData();
        }
        private void GetQualityData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("dept", dept);
            p.Add("type", type);
            p.Add("RFTType", RFTType);
            p.Add("date", dtp.Value);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.TierMeetingServer",
                                        "GetQualityData",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
               
                switch (type)
                {
                    case (int)Parameters.QueryDeptType.Plant:
                        gridData.Columns["colLine"].Visible = false;
                        break;
                    default:
                        break;
                }
                //排序
                //DataTable dd = dtJson.Clone();
                dtJson = dtJson.Rows.Cast<DataRow>().OrderByDescending(r => r["BadPercentage"].ToDecimal()).CopyToDataTable();
                double sum1 = 0;
                double sum2 = 0;
                double avg1 = 0;
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL"].ToString()))
                    {
                        double TOTAL = string.IsNullOrEmpty(dtJson.Rows[i]["TOTAL"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["TOTAL"].ToString());
                        double BAD = string.IsNullOrEmpty(dtJson.Rows[i]["BAD"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["BAD"].ToString());
                        sum1 += TOTAL;
                        sum2 += BAD;
                    }           
                   
                }
                avg1 =sum1==0?0: Math.Round((double)sum2 / sum1, 4);
                DataRow row = dtJson.NewRow();
                if (sum1 == 0)
                {
                    row["TOTAL"]=string.Empty;
                }
                else
                {
                    row["TOTAL"]=sum1;
                }
                if (sum2 == 0)
                {
                    row["BAD"] = string.Empty;
                }
                else
                {
                    row["BAD"] = sum2;
                }
                if (avg1 == 0)
                {
                    row["BADPERCENTAGE"] = string.Empty;
                }
                else
                {
                    row["BADPERCENTAGE"] = avg1 * 100;
                }                
                dtJson.Rows.Add(row);
                dtJson.Columns.Add("RFT");

                foreach (DataRow dr in dtJson.Rows)
                {
                    if (dr["TOTAL"].ToDouble() != 0)
                    {
                        dr["RFT"] = 100 - dr["BadPercentage"].ToDouble();
                    }
                    else
                    {
                        dr["RFT"] = string.Empty;
                    }
                }

                gridData.DataSource = dtJson;                
                gridData.Refresh();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    if (RFTType == "L")
                    {
                        if (dtJson.Rows[i]["RFT"].ToDouble() >= 90)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Green;//颜色管理
                        }
                        else if (dtJson.Rows[i]["RFT"].ToDouble() >= 85.5)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if(dtJson.Rows[i]["RFT"].ToString()==string.Empty)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else                       
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    else if (RFTType == "S")
                    {
                        if (dtJson.Rows[i]["RFT"].ToDouble() >= 97)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Green;//颜色管理
                        }
                        else if (dtJson.Rows[i]["RFT"].ToDouble() >= 92.15)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dtJson.Rows[i]["RFT"].ToString() == string.Empty)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    else if (RFTType == "C")
                    {
                        if (dtJson.Rows[i]["RFT"].ToDouble() >= 98)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Green;//颜色管理
                        }
                        else if (dtJson.Rows[i]["RFT"].ToDouble() >= 93.1)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dtJson.Rows[i]["RFT"].ToString() == string.Empty)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    else if (RFTType == "T")
                    {
                        if (dtJson.Rows[i]["RFT"].ToDouble() >= 90)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Green;//颜色管理
                        }
                        else if (dtJson.Rows[i]["RFT"].ToDouble() >= 85.5)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (dtJson.Rows[i]["RFT"].ToString() == string.Empty)
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            gridData.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
       
        private void gridData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            bool flag = !string.IsNullOrEmpty(gridData.CurrentCell.Value.ToString());
            if (flag && type == (int)Parameters.QueryDeptType.Plant && e.ColumnIndex.Equals(gridData.Columns["colSection"].Index))
            {
                DetailQualityForm frm = new DetailQualityForm(dtp.Value, gridData.CurrentCell.Value.ToString(), (int)Parameters.QueryDeptType.Section, RFTType);
                frm.Show();
            }
            else if (flag && type == (int)Parameters.QueryDeptType.Section && e.ColumnIndex.Equals(gridData.Columns["colLine"].Index))
            {
                DetailQualityForm frm = new DetailQualityForm(dtp.Value, gridData.CurrentCell.Value.ToString(), (int)Parameters.QueryDeptType.Line, RFTType);
                frm.Show();
            }
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

        private void shishi_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Parameters.urlQuality);
        }
    }
}
