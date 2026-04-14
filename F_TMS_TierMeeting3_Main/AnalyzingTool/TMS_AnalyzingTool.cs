using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace F_TMS_TierMeeting3_Main
{
    public partial class TMS_AnalyzingTool : MaterialForm
    {
        private string project = "F_TMS_TIERMEETING3_MAIN";
        private string function = "TMS_ANALYZINGTOOL";
        private DataTable dtLocation = new DataTable();
        public TMS_AnalyzingTool()
        {
            InitializeComponent();
        }
        private void TMS_AnalyzingTool_Load(object sender, EventArgs e)
        {
            InitUI();
            LoadDepartment();
            LoadLocation();
        }
        private void InitUI() {
            this.WindowState = FormWindowState.Maximized;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }
        private void LoadDepartment()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "FileManagement",
                "FileManagement.Controllers.FileManagementServer",
                "GetDepartmentsList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                cbbDepartment.DataSource = dt;
                cbbDepartment.ValueMember = "DEPARTMENT_CODE";
                GetDepartmentOfUser();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadLocation()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "FileManagement",
                "FileManagement.Controllers.FileManagementServer",
                "GetLocationsList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dtLocation = dt;
                cbbLocation.DataSource = dt;
                cbbLocation.ValueMember = "G_LOCATION_CODE";
                string lang = Program.client.Language == "hk" ? "yn" : Program.client.Language;
                cbbLocation.DisplayMember = lang;
                GetFirstLoadLocaion();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetDepartmentOfUser()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "FileManagement",
                "FileManagement.Controllers.FileManagementServer",
                "GetDepartmentOfUser", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0) {
                    int i = 2;
                    while (i >= 0) {
                        if (!string.IsNullOrEmpty(dt.Rows[0][i].ToString()))
                        {
                            cbbDepartment.SelectedIndex = cbbDepartment.FindString(dt.Rows[0][i].ToString());
                            break;
                        }
                        i--;
                    }
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetFirstLoadLocaion()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_PROJECT", project);
            p.Add("G_FUNCTION", function);
            p.Add("G_DEPARTMENT", cbbDepartment.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "FileManagement",
                "FileManagement.Controllers.FileManagementServer",
                "GetFirstLoadLocaion", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0) {
                    cbbLocation.SelectedValue = dt.Rows[0]["G_LOCATION"];
                }
                GetFilesList();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetFilesList()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_PATH", "");
            p.Add("G_HEADER", txtHeader.Text.ToUpper());
            p.Add("G_PROJECT", project);
            p.Add("G_FUNCTION", function);
            p.Add("G_ART", txtART.Text.ToUpper());
            p.Add("G_SHOE_TYPE", txtShoeType.Text.ToUpper());
            p.Add("G_FILE_TYPE", "");
            p.Add("G_YEAR", DateTime.Now.Year);
            p.Add("G_MONTH", DateTime.Now.Month);
            p.Add("G_DEPARTMENT", cbbDepartment.SelectedValue.ToString());
            p.Add("G_LOCATION", cbbLocation.SelectedValue.ToString()) ;
            p.Add("G_ISACTIVE", "1");
            p.Add("G_ISDELETED", "0");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "FileManagement", "FileManagement.Controllers.FileManagementServer", "GetFilesList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                foreach (DataRow dr in dt.Rows) {
                    var result = dtLocation.Rows.Cast<DataRow>().FirstOrDefault(x => x.Field<string>("G_LOCATION_CODE").Equals(dr["G_LOCATION"].ToString()));
                    string lang = Program.client.Language == "hk" ? "yn" : Program.client.Language;
                    dr["G_LOCATION"] = result[lang];
                    dr["G_CREATEDDATE"] = Convert.ToDateTime(dr["G_CREATEDDATE"]).ToString("yyyy/MM/dd");
                    if (!string.IsNullOrEmpty(dr["G_UPDATEDDATE"].ToString()))
                        dr["G_UPDATEDDATE"] = Convert.ToDateTime(dr["G_UPDATEDDATE"]).ToString("yyyy/MM/dd");
                }
                grid.DataSource = dt;
                grid.Update();
            }
            else
            {
                DataTable dt = (DataTable)grid.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                grid.Refresh();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void DownloadFile(string project, string function, string fileName, string fileType)
        {
            string[] strArr = { "JPG", "JPEG", "PNG", "PDF" };
            if (Array.IndexOf(strArr, fileType.ToUpper()) >= 0)
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("fileName", fileName);
                p.Add("project", project);
                p.Add("function", function);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                                        Program.client.APIURL, "FileManagement",
                                        "FileManagement.Controllers.FileManagementServer",
                                        "DownloadFile", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    byte[] bytes = Convert.FromBase64String(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString());
                    var path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\files\\" + project+"\\"+ function;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + "\\" + fileName;
                    try {
                        System.IO.File.WriteAllBytes(path, bytes);
                        wb.Navigate(path);
                    }
                    catch (Exception) {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "tms_err_file_is_using: file is using at another process!");
                    }
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            else {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "tms_err_file_type: file type is not correct!");
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetFilesList();
        }

        private void grid_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            int index = this.grid.CurrentRow.Index;
            bool flag = index > -1 && this.grid.Rows[index].Cells[0].Value != null;
            if (flag)
            {
                DownloadFile(grid.Rows[index].Cells["colG_PROJECT"].Value.ToString(), grid.Rows[index].Cells["colG_FUNCTION"].Value.ToString(),
                    grid.Rows[index].Cells["colG_PATH"].Value.ToString(), grid.Rows[index].Cells["colG_FILE_TYPE"].Value.ToString());
            }
        }

    }
}
