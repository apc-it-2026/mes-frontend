using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    public partial class TMS_BasicInformation : MaterialForm
    {
        private string project = "F_TMS_TIERMEETING2_MAIN";
        private string functionOrg = "TMS_BASICINFORMATION_ORG";
        private string functionLayout = "TMS_BASICINFORMATION_LAYOUT";
        public enum tabType : int
        {
            Org = 0,
            Layout = 1
        }
        public TMS_BasicInformation()
        {
            InitializeComponent();
        }
        private void TMS_BasicInformation_Load(object sender, EventArgs e)
        {
            InitUI();
            LoadDepartment();
            LoadLocation();
        }
        private void InitUI()
        {
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
                //org
                cbbDepartmentOrg.DataSource = dt;
                cbbDepartmentOrg.ValueMember = "DEPARTMENT_CODE";
                //layout
                cbbDepartmentLayout.DataSource = dt;
                cbbDepartmentLayout.ValueMember = "DEPARTMENT_CODE";
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
                string lang = Program.client.Language == "hk" ? "yn" : Program.client.Language;
                //org
                cbbLocationOrg.DataSource = dt;
                cbbLocationOrg.ValueMember = "G_LOCATION_CODE";
                cbbLocationOrg.DisplayMember = lang;
                GetFirstLoadLocaion((int)tabType.Org);
                //layout
                cbbLocationLayout.DataSource = dt;
                cbbLocationLayout.ValueMember = "G_LOCATION_CODE";
                cbbLocationLayout.DisplayMember = lang;
                GetFirstLoadLocaion((int)tabType.Layout);
                
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
                    int i = 1;
                    while (i >= 0) {
                        if (!string.IsNullOrEmpty(dt.Rows[0][i].ToString()))
                        {
                            cbbDepartmentOrg.SelectedIndex = cbbDepartmentOrg.FindString(dt.Rows[0][i].ToString());
                            cbbDepartmentLayout.SelectedIndex = cbbDepartmentOrg.FindString(dt.Rows[0][i].ToString());
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
        private void GetFirstLoadLocaion(int type)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_PROJECT", project);
            switch (type) {
                case (int)tabType.Org:
                    p.Add("G_FUNCTION", functionOrg);
                    p.Add("G_DEPARTMENT", cbbDepartmentOrg.SelectedValue.ToString());
                    break;
                default:
                    p.Add("G_FUNCTION", functionLayout);
                    p.Add("G_DEPARTMENT", cbbDepartmentLayout.SelectedValue.ToString());
                    break;
            }
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "FileManagement",
                "FileManagement.Controllers.FileManagementServer",
                "GetFirstLoadLocaion", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count > 0) {
                    switch (type)
                    {
                        case (int)tabType.Org:
                            cbbLocationOrg.SelectedValue = dt.Rows[0]["G_LOCATION"];
                            break;
                        default:
                            cbbLocationLayout.SelectedValue = dt.Rows[0]["G_LOCATION"];
                            break;
                    }
                }
                GetFilesList(type);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void GetFilesList(int type)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_PATH", "");
            p.Add("G_HEADER", "");
            p.Add("G_PROJECT", project);
            switch (type)
            {
                case (int)tabType.Org:
                    p.Add("G_FUNCTION", functionOrg);
                    p.Add("G_DEPARTMENT", cbbDepartmentOrg.SelectedValue.ToString());
                    p.Add("G_LOCATION", cbbLocationOrg.SelectedValue.ToString());
                    break;
                default:
                    p.Add("G_FUNCTION", functionLayout);
                    p.Add("G_DEPARTMENT", cbbDepartmentLayout.SelectedValue.ToString());
                    p.Add("G_LOCATION", cbbLocationLayout.SelectedValue.ToString());
                    break;
            }
            p.Add("G_ART", "");
            p.Add("G_SHOE_TYPE", "");
            p.Add("G_FILE_TYPE", "PDF");
            p.Add("G_YEAR", DateTime.Now.Year);
            p.Add("G_MONTH", DateTime.Now.Month);
            p.Add("G_ISACTIVE", "1");
            p.Add("G_ISDELETED", "0");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "FileManagement", "FileManagement.Controllers.FileManagementServer", "GetFilesList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                switch (type)
                {
                    case (int)tabType.Org:
                        wbOrg.Navigate("about:blank");
                        break;
                    default:
                        wbLayout.Navigate("about:blank");
                        break;
                }
                if (dt.Rows.Count > 0)
                {
                    DownloadFile(dt.Rows[0]["G_PROJECT"].ToString(), dt.Rows[0]["G_FUNCTION"].ToString(), 
                        dt.Rows[0]["G_PATH"].ToString(), dt.Rows[0]["G_FILE_TYPE"].ToString(),type);
                }
                else {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "tms_err_file_not_found: file is not found!");
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void DownloadFile(string project, string function, string fileName, string fileType, int type)
        {
            if (fileType.ToUpper().Equals("PDF"))
            {
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("fileName", fileName);
                p.Add("project", project);
                switch (type)
                {
                    case (int)tabType.Org:
                        p.Add("function", functionOrg);
                        break;
                    default:
                        p.Add("function", functionLayout);
                        break;
                }
                //p.Add("function", function);
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
                        switch (type)
                        {
                            case (int)tabType.Org:
                                wbOrg.Navigate(path);
                                break;
                            default:
                                wbLayout.Navigate(path);
                                break;
                        }
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
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "tms_err_file_type: file type is not PDF!");
            }
        }

        private void btnQueryOrg_Click(object sender, EventArgs e)
        {
            GetFilesList((int)tabType.Org);
        }

        private void btnQueryLayout_Click(object sender, EventArgs e)
        {
            GetFilesList((int)tabType.Layout);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFilesList(tabControl1.SelectedIndex);
        }
    }
}
