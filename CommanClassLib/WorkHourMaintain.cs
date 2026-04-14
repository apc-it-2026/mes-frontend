using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace CommanClassLib
{
    public partial class WorkHoursMaintain : Form
    {
        // public delegate void FormLoadHandle(object sender, EventArgs e);
        //  public event FormLoadHandle changed;

        string APIURL;
        string UserToken;
        string Language;
        object Client;
        public string d_dept;
        public string d_deptName;

        public WorkHoursMaintain()
        {
            InitializeComponent();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public WorkHoursMaintain(string sAPIURL, string sUserToken, object oClient, string sLanguage)
        {
            APIURL = sAPIURL;
            UserToken = sUserToken;
            Client = oClient;
            Language = sLanguage;
            InitializeComponent();
        }


        public void AfterShow(string dept, string departmentName)
        {
            d_dept = dept;
            tbDept.Text = departmentName;
            GetCompany();
            GetRount();
            GetWorkingHoursData();
            //return GetWorkingHoursData() > 0 ? 0 : 1;
        }


        public int AfterShow()
        {
            try
            {
                GetDept();
                GetCompany();
                GetRount();

                return GetWorkingHoursData() > 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return 0;
            }
        }

        private void GetDept()
        {
            string ret = WebAPIHelper.Post(APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDept", UserToken, string.Empty);
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    d_dept = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    d_deptName = dtJson.Rows[0]["Department_Name"].ToString();
                    tbDept.Text = d_deptName;
                }
                else
                {
                    d_dept = "00";
                    d_deptName = "";
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                plMessage.Visible = true;
                plMessage.BackColor = Color.Firebrick;
                lbMessage.Text = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
            }
        }

        private void GetCompany()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            if (d_dept != null)
            {
                p.Add("vCode", d_dept.Substring(0, 2));
                string retCompany = WebAPIHelper.Post(APIURL, "KZ_SFCAPI",
                    "KZ_SFCAPI.Controllers.GeneralServer", "GetCompany", UserToken,
                    JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(retCompany)["IsSuccess"]))
                {
                    string json =
                        JsonConvert.DeserializeObject<Dictionary<string, object>>(retCompany)["RetData"]
                            .ToString();
                    DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count != 0)
                    {
                        tbOrg.Text = dtJson.Rows[0]["company"].ToString();
                        tbPlant.Text = d_dept.Substring(0, 2) + '|' + dtJson.Rows[0]["org"];
                    }
                    else
                    {
                        tbOrg.Text = d_dept.Substring(0, 2);
                        tbPlant.Text = "No site set";
                    }
                }
                else
                {
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retCompany)["ErrMsg"].ToString());
                    plMessage.Visible = true;
                    plMessage.BackColor = Color.Firebrick;
                    lbMessage.Text =
                        JsonConvert.DeserializeObject<Dictionary<string, object>>(retCompany)["ErrMsg"]
                            .ToString();
                }
            }
        }

        private void GetRount()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDeptCode", d_dept);
            string ret = WebAPIHelper.Post(APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.GeneralServer", "GetRount", UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);

                /*for (int i = 0; i < cbRout.Items.Count; i++)
                {
                    if (cbRout.Items.IndexOf(i).ToString().Substring(0, 1) == dtJson.ToString())
                    {
                        cbRout.SelectedIndex = i;
                    }
                }*/
                if (dtJson.Rows.Count != 0)
                {
                    tbRout.Text = dtJson.Rows[0]["rout_no"].ToString() + '|' + dtJson.Rows[0]["rout_name_z"];
                }
                else
                {
                    tbRout.Text = "Process not set";
                }
            }
            else
            {
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                plMessage.Visible = true;
                plMessage.BackColor = Color.Firebrick;
                lbMessage.Text = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
            }
        }

        private void tbOperator_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key = Convert.ToInt32(e.KeyChar);
            if (!(48 <= key && key <= 58 || key == 8)) //number、 Backspace
            {
                Text = key.ToString();
                e.Handled = true;
            }
            else Text = "0";
        }

        private void tbOperator_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string department_code = d_dept;
            string department_name = tbDept.Text;
            string work_day = dtpDate.Text;
            string rout_no = tbRout.Text == "Process not set" ? "" : tbRout.Text;
            TransForm frm = new TransForm(department_code, department_name, work_day, rout_no, APIURL, UserToken, Client, Language);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }

        private void btnConfirmWorkingHours_Click(object sender, EventArgs e)
        {
            string dd = dtpDate.Text;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Dept", d_dept);
            p.Add("Date", dtpDate.Text);
            p.Add("AmFrom", tsAmFrom.getHHmm);
            p.Add("AmTo", tsAmTo.getHHmm);
            p.Add("PmFrom", tsPmFrom.getHHmm);
            p.Add("PmTo", tsPmTo.getHHmm);
            p.Add("Operator", tbOperator.Text);
            p.Add("MultiSkill", tbMultiSkill.Text);
            p.Add("AllRounder", tbAllRounder.Text);
            p.Add("MobileWorker", tbMobileWorker.Text);


            string ret = WebAPIHelper.Post(APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionInputServer", "SaveWorkingHours", UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                // SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "工作历提交成功！");
                plMessage.Visible = true;
                plMessage.BackColor = Color.MediumSeaGreen;
                lbMessage.Text = "Work calendar submitted successfully";
                MessageBox.Show("Work calendar submitted successfully", "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                plMessage.Visible = true;
                plMessage.BackColor = Color.Firebrick;
                lbMessage.Text = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                MessageBox.Show(lbMessage.Text, "hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnQueryWorkingHours_Click(object sender, EventArgs e)
        {
            try
            {
                plMessage.Visible = false;
                GetWorkingHoursData();
            }
            catch (Exception ex)
            {
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                plMessage.Visible = true;
                plMessage.BackColor = Color.Firebrick;
                lbMessage.Text = ex.Message;
            }
        }

        private int GetWorkingHoursData()
        {
            //string vPlantNo = string.IsNullOrWhiteSpace(tbPlant.Text.ToString()) ? tbPlant.Text.ToString() : tbPlant.Text.ToString().Split('|')[0];
            //string vRoutNo = string.IsNullOrWhiteSpace(tbRout.Text.ToString()) ? tbRout.Text.ToString() : tbRout.Text.ToString().Split('|')[0];
            Dictionary<string, object> p = new Dictionary<string, object>();
            /*p.Add("vOrgNo", cbOrg.Text.ToString());
            p.Add("vPlantNo", vPlantNo);
            p.Add("vDeptNo", tbDept.Text.ToString());
            p.Add("vRoutNo", vRoutNo);*/
            p.Add("vDept", d_dept);
            p.Add("vWorkDate", dtpDate.Value.ToString("yyyy/MM/dd"));
            string ret = WebAPIHelper.Post(APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.ProductionInputServer", "QueryWorkingHours", UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dgvWorkingHours.DataSource = dtJson.DefaultView;
                dgvWorkingHours.Update();

                int iRowCount = dtJson.Rows.Count;
                if (iRowCount > 0)
                {
                    tsAmFrom.setHH = dtJson.Rows[0]["AmFromHour"].ToString().Split(':')[0];
                    tsAmFrom.setmm = dtJson.Rows[0]["AmFromHour"].ToString().Split(':')[1];
                    tsAmTo.setHH = dtJson.Rows[0]["AmToHour"].ToString().Split(':')[0];
                    tsAmTo.setmm = dtJson.Rows[0]["AmToHour"].ToString().Split(':')[1];
                    tsPmFrom.setHH = dtJson.Rows[0]["PmFromHour"].ToString().Split(':')[0];
                    tsPmFrom.setmm = dtJson.Rows[0]["PmFromHour"].ToString().Split(':')[1];
                    tsPmTo.setHH = dtJson.Rows[0]["PmToHour"].ToString().Split(':')[0];
                    tsPmTo.setmm = dtJson.Rows[0]["PmToHour"].ToString().Split(':')[1];

                    tbOperator.Text = dtJson.Rows[0]["jockey_qty"].ToString();
                    tbMultiSkill.Text = dtJson.Rows[0]["pluripotent_worker"].ToString();
                    tbAllRounder.Text = dtJson.Rows[0]["omnipotent_worker"].ToString();
                    tbMobileWorker.Text = dtJson.Rows[0]["udf01"].ToString();

                    return iRowCount;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                plMessage.Visible = true;
                plMessage.BackColor = Color.Firebrick;
                lbMessage.Text = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                return -1;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExcelExporter frm = new ExcelExporter(APIURL, UserToken);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
        }
    }
}