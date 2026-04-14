using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using AutocompleteMenuNS;

namespace F_SFC_TrackIn_AssemblyOrder
{
    public partial class TrackIn_AssemblyOrder_Trans : MaterialForm
    {
        private string department_code;
        private string department_name;
        private string work_day;
        private string rout_no;
        DataTable deptDatatable;

        public TrackIn_AssemblyOrder_Trans()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        public TrackIn_AssemblyOrder_Trans(string department_code, string department_name, string work_day, string rout_no)
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.department_code = department_code;
            this.department_name = department_name;
            this.work_day = work_day;
            this.rout_no = rout_no;
        }

        public void LoadMoveNo()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "LoadMoveNo", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                this.txt_move_no.Text = "MV_" + department_code + "_" + Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        public void LoadSeDept()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", department_code);
            p.Add("vRoutNo", rout_no);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "LoadSeDept", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                deptDatatable = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= deptDatatable.Rows.Count; i++)
                {
                    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { i + "", deptDatatable.Rows[i - 1]["DEPARTMENT_CODE"] + " " + deptDatatable.Rows[i - 1]["DEPARTMENT_NAME"] }, deptDatatable.Rows[i - 1]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = i });
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
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
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vMoveNo", txt_move_no.Text);
            p.Add("vMoveDate", txtMoveDate.Text);
            p.Add("vDDept", department_code);
            p.Add("vWrokDay", work_day);
            p.Add("vTransInTime", textBox5.Text);
            p.Add("vTransOutTime", textBox4.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.WorkingHoursServer", "SaveTransTime", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                this.Close();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TransForm_Load(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.textBox2.Text = this.department_code;
            this.textBox3.Text = this.department_name;
            this.txt_work_day.Text = work_day;
            this.txtMoveDate.Text = DateTime.Now.ToShortDateString();
            LoadMoveNo();
            LoadSeDept();
        }
    }
}
