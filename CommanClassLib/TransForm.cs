using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Class;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;

namespace CommanClassLib
{
    public partial class TransForm : MaterialForm
    {
        private string department_code;
        private string department_name;
        private string work_day;
        private string rout_no;
        string APIURL;
        string UserToken;
        object Client;
        string Language;
        DataTable deptDatatable;

        public TransForm()
        {
            InitializeComponent();
        }

        public TransForm(string department_code, string department_name, string work_day, string rout_no, string sAPIURL, string sUserToken, object oClient, string sLanguage)
        {
            InitializeComponent();
            this.department_code = department_code;
            this.department_name = department_name;
            this.work_day = work_day;
            this.rout_no = rout_no;
            APIURL = sAPIURL;
            UserToken = sUserToken;
            Client = oClient;
            Language = sLanguage;
        }

        public void LoadMoveNo()
        {
            string ret = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "LoadMoveNo", UserToken, string.Empty);
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                txt_move_no.Text = "MV_" + department_code + "_" + JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"];
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        public void LoadSeDept()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 250 };
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", department_code);
            p.Add("vRoutNo", rout_no);
            string ret = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "LoadSeDept", UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                deptDatatable = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= deptDatatable.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { i + "", deptDatatable.Rows[i - 1]["DEPARTMENT_CODE"] + " " + deptDatatable.Rows[i - 1]["DEPARTMENT_NAME"] }, deptDatatable.Rows[i - 1]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i });
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ////没有输入组别
            //if (string.IsNullOrWhiteSpace(textBox1.Text))
            //{
            //    return;
            //}
            ////没有拨出时间
            //if (string.IsNullOrWhiteSpace(textBox4.Text))
            //{
            //    return;
            //}
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vMoveNo", txt_move_no.Text);
            p.Add("vMoveDate", txtMoveDate.Text);
            p.Add("vDDept", department_code);
            p.Add("vWrokDay", work_day);
            p.Add("vTransInTime", textBox5.Text);
            p.Add("vTransOutTime", textBox4.Text);
            string ret = WebAPIHelper.Post(APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "SaveTransTime", UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                Close();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TransForm_Load(object sender, EventArgs e)
        {
            UIHelper.UIUpdate(Name, this, (ClientClass)Client, "", Language);
            textBox2.Text = department_code;
            textBox3.Text = department_name;
            txt_work_day.Text = work_day;
            txtMoveDate.Text = DateTime.Now.ToShortDateString();
            LoadMoveNo();
            LoadSeDept();
        }
    }
}