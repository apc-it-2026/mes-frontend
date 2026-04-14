using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TierMeeting;

namespace TierMeeting
{
    public partial class TMS_AttendanceRecord : MaterialForm
    {
        private int id = 0;
        private string currentDept = "";
        public TMS_AttendanceRecord()
        {
            InitializeComponent();
        }
        private void TMS_AttendanceRecord_Load(object sender, EventArgs e)
        {
            InitUI();
            dtpRecord.CustomFormat = Parameters.dateFormat;
            dtpSummary.CustomFormat = Parameters.dateFormat;
            LoadDepartment();
            LoadRecord();
        }
        #region Init
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
                //Attendance
                cbbDepartmentAttendance.DataSource = dt;
                cbbDepartmentAttendance.ValueMember = "DEPARTMENT_CODE";
                GetDepartmentOfUser();
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
                            cbbDepartmentAttendance.SelectedIndex = cbbDepartmentAttendance.FindString(dt.Rows[0][i].ToString());
                            currentDept = dt.Rows[0][i].ToString();
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
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Name) {
                case "tabRecord":
                    LoadRecord();
                    break;
                case "tabSummary":
                    LoadSummary();
                    break;
                case "tabAttendance":
                    LoadAttendance();
                    break;
            }
        }
        #endregion
        #region Record
        private void LoadRecord() {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_DATE", dtpRecord.Value.ToString(Parameters.dateFormat));
            p.Add("G_DEPARTMENT", "");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "TierMeeting",
                "TierMeeting.Controllers.OtherFunctionServer",
                "GetAttendanceRecord", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                foreach (DataRow dr in dt.Rows) {
                    for (int i = 3; i < dt.Columns.Count; i++) {
                        if (i == 3) {
                            dr[i] = dr[i].ToDate().ToString(Parameters.dateFormat).Replace('-','/');
                        }
                        else {
                            if (!string.IsNullOrEmpty(dr[i].ToString()))
                            {
                                dr[i] = dr[i].ToInt() == 0 ? false : true;
                            }
                            else
                            {
                                dr[i] = false;
                            }
                        }
                    }
                }
                gridRecord.DataSource = dt;
                gridRecord.Update();
            }
            else
            {
                DataTable dt = (DataTable)gridRecord.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                gridRecord.Refresh();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnQueryRecord_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }
        private void gridRecord_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 4)
            {
                for (int i = 4; i < gridRecord.Columns.Count; i++) {
                    if (i != e.ColumnIndex) {
                        gridRecord.Rows[e.RowIndex].Cells[i].Value = false;
                    }
                }
                gridRecord.Update();
            }
            else {
                return;
            }
        }
        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("data", (DataTable)gridRecord.DataSource);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.OtherFunctionServer",
                                        "SaveAttendanceRecord",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageHelper.ShowSuccess(this, SJeMES_Framework.Common.UIHelper.UImsg("tms_suc_save:save success", Program.client, "", Program.client.Language));
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        #endregion
        #region Summary
        private void btnQuerySummary_Click(object sender, EventArgs e)
        {
            LoadSummary();
        }
        private void LoadSummary()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_DATE", dtpSummary.Value.ToString(Parameters.dateFormat));
            p.Add("G_DEPARTMENT", "");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "TierMeeting",
                "TierMeeting.Controllers.OtherFunctionServer",
                "GetAttendanceSummary", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                gridSummary.DataSource = dt;
                gridSummary.Update();
            }
            else
            {
                DataTable dt = (DataTable)gridSummary.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                gridSummary.Refresh();
                MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        #endregion
        #region Attendance
        private void LoadAttendance() {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("G_NAME", txtNameAttendance.Text);
            if (cbbDepartmentAttendance.SelectedIndex >= 0) {
                p.Add("G_DEPARTMENT", cbbDepartmentAttendance.SelectedValue.ToString());
            }
            else {
                p.Add("G_DEPARTMENT", string.Empty);
            }
            string temp = cbxIsActiveAttendance.Checked ? "1" : "0";
            p.Add("G_ISACTIVE", temp);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                Program.client.APIURL, "TierMeeting",
                "TierMeeting.Controllers.OtherFunctionServer",
                "GetAttendance", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                foreach (DataRow dr in dt.Rows) {
                    dr["G_ISACTIVE"] = dr["G_ISACTIVE"].ToBool();
                }
                gridAttendance.DataSource = dt;
                gridAttendance.Update();
            }
            else
            {
                DataTable dt = (DataTable)gridAttendance.DataSource;
                if (dt != null)
                {
                    dt.Clear();
                }
                gridAttendance.Refresh();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadAttendance();
        }
        #endregion

        private void gridAttendance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (CheckEditableRow()) return; //editing

            if (e.ColumnIndex.Equals(gridAttendance.Columns["colCbx"].Index))
            {
                foreach (DataGridViewRow r in gridAttendance.Rows)
                {
                    r.Cells["colCbx"].Value = false;
                }
                gridAttendance.CurrentCell.Value = !(gridAttendance.CurrentCell.Value.ToBool());
            }
        }
        private bool CheckEditableRow()
        {
            bool flag = false;
            foreach (DataGridViewRow r in gridAttendance.Rows)
            {
                if (r.DefaultCellStyle.BackColor.Equals(Color.Aquamarine)) flag = true;
            }
            return flag;
        }
        private void btnEditAttendance_Click(object sender, EventArgs e)
        {
            int rowIndex = GetSelectedRow();
            if (rowIndex > -1)
            {
                gridAttendance.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                cbbDepartmentAttendance.SelectedIndex = cbbDepartmentAttendance.FindString(gridAttendance.Rows[rowIndex].Cells["colDepartment"].Value.ToString());
                txtNameAttendance.Text = gridAttendance.Rows[rowIndex].Cells["colName"].Value.ToString();
                cbxIsActiveAttendance.Checked = gridAttendance.Rows[rowIndex].Cells["colIsActive"].Value.ToBool();
                id = gridAttendance.Rows[rowIndex].Cells["colID"].Value.ToInt();
                btnSaveAttendance.Visible = true;
            }
            else
            {
                return;
            }
        }
        private int GetSelectedRow()
        {
            int rowIndex = -1;
            foreach (DataGridViewRow r in gridAttendance.Rows)
            {
                if (r.Cells["colCbx"].Value.ToBool())
                {
                    rowIndex = r.Index;
                }
            }
            return rowIndex;
        }

        private void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            if (cbbDepartmentAttendance.SelectedIndex < 0) {

                MessageHelper.ShowErr(this, SJeMES_Framework.Common.UIHelper.UImsg("tms-attendance-err: department cannot be empty", Program.client, "", Program.client.Language));
                return;
            }
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("id", id);
            p.Add("G_NAME", txtNameAttendance.Text);
            p.Add("G_DEPARTMENT", cbbDepartmentAttendance.SelectedValue.ToString());
            int tmp = cbxIsActiveAttendance.Checked ? 1 : 0;
            p.Add("G_ISACTIVE", tmp);
            string ret =
                SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.client.APIURL,
                    "TierMeeting",
                    "TierMeeting.Controllers.OtherFunctionServer",
                                        "CreateOrUpdateAttendance",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                MessageHelper.ShowSuccess(this, SJeMES_Framework.Common.UIHelper.UImsg("tms_suc_save:save success", Program.client, "", Program.client.Language));
                foreach (DataGridViewRow dr in gridAttendance.Rows)
                {
                    dr.DefaultCellStyle.BackColor = Color.White;
                    dr.Cells["colCbx"].Value = false;
                }
                cbbDepartmentAttendance.SelectedIndex = cbbDepartmentAttendance.FindString(currentDept);
                txtNameAttendance.Text = "";
                cbxIsActiveAttendance.Checked = true;
                id = 0;
                btnSaveAttendance.Visible = false;
                LoadAttendance();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnAddAttendance_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in gridAttendance.Rows)
            {
                dr.DefaultCellStyle.BackColor = Color.White;
                dr.Cells["colCbx"].Value = false;
            }
            cbbDepartmentAttendance.SelectedIndex = cbbDepartmentAttendance.FindString(currentDept);
            txtNameAttendance.Text = "";
            cbxIsActiveAttendance.Checked = true;
            id = 0;
            btnSaveAttendance.Visible = true;
        }

        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("tms_confirm_delete: confirm to delete?",
                                     "tms_delete: delete!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                int rowIndex = GetSelectedRow();
                Dictionary<string, Object> p = new Dictionary<string, object>();
                p.Add("id", gridAttendance.Rows[rowIndex].Cells["colID"].Value.ToInt());
                string ret =
                    SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                        Program.client.APIURL,
                        "TierMeeting",
                        "TierMeeting.Controllers.OtherFunctionServer",
                                            "DeleteAttendance",
                        Program.client.UserToken,
                        JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, SJeMES_Framework.Common.UIHelper.UImsg("tms_suc_save:save success", Program.client, "", Program.client.Language));
                    //foreach (DataGridViewRow dr in gridAttendance.Rows)
                    //{
                    //    dr.DefaultCellStyle.BackColor = Color.White;
                    //    dr.Cells["colCbx"].Value = false;
                    //}
                    //cbbDepartmentAttendance.SelectedIndex = cbbDepartmentAttendance.FindString(currentDept);
                    //txtNameAttendance.Text = "";
                    //cbxIsActiveAttendance.Checked = true;
                    //id = 0;
                    //btnSaveAttendance.Visible = false;
                    LoadAttendance();
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
    }
}
