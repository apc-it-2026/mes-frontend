using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static F_Bad_Registration.SampleLogoList;
using static F_Bad_Registration.BadReason;
using System.Collections;
using System.Text.RegularExpressions;

namespace F_Bad_Registration
{
    public partial class BadRegistration : MaterialForm
    {
        public BadRegistration()
        {
            InitializeComponent();
        }
        private void F_Bad_Registration_Load(object sender, EventArgs e)
        {
            SelectSampleNO();
            SelectStaffRole();
        }
        /// <summary>
        /// 查询样品单
        /// </summary>
        private void SelectSampleNO()
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectSampleNO", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    collection.Add(dtJson.Rows[i - 1]["sample_no"].ToString());
                }
                txtSampleOrder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleOrder.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleOrder.AutoCompleteCustomSource = collection;
                txtSampleOrder2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSampleOrder2.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSampleOrder2.AutoCompleteCustomSource = collection;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void SelectStaffRole()
        {
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectStaffRole", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (!string.IsNullOrEmpty(json))
                {
                    if (json=="sample")
                    {
                        cbSampleOrCraft.SelectedItem = "样品室";
                    }
                    if (json=="craft")
                    {
                        cbSampleOrCraft.SelectedItem = "工艺部";
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this,"没有查询到角色");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadDatagridview2();
        }
        private void LoadDatagridview2()
        {
            dataGridView2.AutoGenerateColumns = false;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", txtSampleOrder2.Text);
            p.Add("artNo", txtArtNo2.Text);
            p.Add("badOrder", txtBadOrder2.Text);
            p.Add("RegisterUser", txtRegisterUser12.Text);
            p.Add("startDate", txtStartDate.Value.ToString("d"));
            p.Add("endDate", txtEndDate.Value.ToString("d"));
            p.Add("status", cbStatus2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectBySampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                dataGridView2.DataSource = dt;
            }
            else
            {
                dataGridView2.DataSource = null;
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnExamMore_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "NG_NO", DataType = typeof(string) });
            if (dataGridView2.Rows.Count > 0 && dataGridView2 != null)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        if (dataGridView2.Rows[row.Index].Cells["Column22"].Value.ToString() == "待审核")
                        {
                            dt.Rows.Add(row.Cells["NG_NO"].Value.ToString());
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("你确定需要批量审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        Dictionary<object, object> p = new Dictionary<object, object>();
                        p.Add("ng_no_dt", dt);
                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "ExamMore", Program.client.UserToken, JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            MessageHelper.ShowSuccess(this, "批量审核成功！");
                            LoadDatagridview2();
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "您还没有选择或你勾选的不良记录已审核！");
                }
            }
        }

        private void btnSelectAgain_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is DateTimePicker)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "全选")
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.Cells[0].Value = true;
                }
                button.Text = "取消全选";
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    row.Cells[0].Value = false;
                }
                button.Text = "全选";
            }
        }

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.dataGridView2.Columns[e.ColumnIndex].HeaderText == "操作")
                {
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    e.PaintBackground(e.CellBounds, false);
                    string text = "查看";
                    e.Graphics.DrawString(text, dataGridView2.Font, Brushes.Black, e.CellBounds, sf);
                    e.Handled = true;
                }
            }
        }

        private void btnSelectSampleOrder_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Name != "txtSampleOrder")
                    {
                        item.Text = "";
                    }
                }
                if (item is RichTextBox)
                {
                    item.Text = "";
                }
                if (item is DateTimePicker)
                {
                    item.Text = "";
                }
            }
            txtStatus.Text = "新单";
            btnUpdate.Enabled = btnExam.Enabled = btnDelete.Enabled = btnSave.Enabled = true;
            btnAddNg.Enabled = btnDelNg.Enabled =cbSampleOrCraft.Enabled= true;
            cbSampleOrCraft.Text = "";
            SelectNgInfoBySampleNo();
        }
        /// <summary>
        /// 根据样品单查询
        /// </summary>
        private void SelectNgInfoBySampleNo()
        {
            if (string.IsNullOrEmpty(txtSampleOrder.Text))
            {
                MessageHelper.ShowErr(this, "样品单编号不能为空！");
                return;
            }
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", txtSampleOrder.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectNgInfoBySampleNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                dataGridView1.AutoGenerateColumns = false;
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    txtArtNo.Text = dt.Rows[0]["art_no"].ToString();
                    txtShoesType.Text = dt.Rows[0]["art_name"].ToString();
                    txtPurpose.Text = dt.Rows[0]["purpose"].ToString();
                    txtSeason.Text = dt.Rows[0]["season"].ToString();
                    txtMatchColor.Text = dt.Rows[0]["color_way"].ToString();
                    txtModelMater.Text = dt.Rows[0]["TYPE_LEADER"].ToString();//型体负责人
                    txtPatternMaster.Text = dt.Rows[0]["SAMPLE_LEADER"].ToString();//版型负责人
                }
                else
                {
                    MessageHelper.ShowErr(this, "未查询到该样品单编号的相关信息");
                    return;
                }
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private string GetStatus(string statusCode)
        {
            string status = "";
            switch (statusCode)
            {
                case "2":
                    status = "待审核";
                    break;
                case "7":
                    status = "已审核";
                    break;
                case "0":
                    status = "已删除";
                    break;
                case "1":
                    status = "新单";
                    break;
                case "5":
                    status = "修改中";
                    break;
            }
            return status;
        }
        private void btnAddNg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSampleOrder.Text))
            {
                MessageHelper.ShowErr(this, "样品单不能为空！");
                return;
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("sample_no", txtSampleOrder.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "GetSampleLogoListInfo", Program.client.UserToken, JsonConvert.SerializeObject(dic));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonHelper.GetDataTableByJson(json);
                SampleLogoList sampleLogoList = new SampleLogoList(dt);
                sampleLogoList.DataChange += new SampleLogoList.DataChangeHandler(DataChanged_SampleLogoList);
                sampleLogoList.ShowDialog();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public void DataChanged_SampleLogoList(object sender, SampleLogoList.DataChangeEventArgs args)
        {
            DataTable dt = args.dt;
            if (dataGridView1.Rows.Count <= 0)
            {
                SetDatagridview1Less(dt);
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string partName = dt.Rows[j]["part_name"].ToString() == "" ? "" : dt.Rows[j]["part_name"].ToString();
                    string processName = dt.Rows[j]["process_name"].ToString() == "" ? "" : dt.Rows[j]["process_name"].ToString();
                    string suppliersName = dt.Rows[j]["suppliers_name"].ToString() == "" ? "" : dt.Rows[j]["suppliers_name"].ToString();
                    string sizeNo = dt.Rows[j]["size_no"].ToString() == "" ? "" : dt.Rows[j]["size_no"].ToString();
                    string procedureNo=dt.Rows[j]["PROCEDURE_no"].ToString() == "" ? "" : dt.Rows[j]["PROCEDURE_no"].ToString();
                    string MATERIALNo= dt.Rows[j]["MATERIAL_NO"].ToString() == "" ? "" : dt.Rows[j]["MATERIAL_NO"].ToString();
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        string part_name = dataGridView1.Rows[i].Cells["part_name"].Value == null ? "" : dataGridView1.Rows[i].Cells["part_name"].Value.ToString();
                        string process_name = dataGridView1.Rows[i].Cells["process_name"].Value == null ? "" : dataGridView1.Rows[i].Cells["process_name"].Value.ToString();
                        string suppliers_name = dataGridView1.Rows[i].Cells["suppliers_name"].Value == null ? "" : dataGridView1.Rows[i].Cells["suppliers_name"].Value.ToString();
                        string size_no = dataGridView1.Rows[i].Cells["size_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["size_no"].Value.ToString();
                        string procedure_No = dataGridView1.Rows[i].Cells["PROCEDURE_no"].Value == null ? "" : dataGridView1.Rows[i].Cells["PROCEDURE_no"].Value.ToString();
                        string MATERIAL_NO = dataGridView1.Rows[i].Cells["MATERIAL_NO"].Value == null ? "" : dataGridView1.Rows[i].Cells["MATERIAL_NO"].Value.ToString();
                        if (partName == part_name && processName == process_name && suppliersName == suppliers_name && sizeNo == size_no&& procedureNo==procedure_No)
                        {
                            MessageHelper.ShowErr(this, $"所选择的第{i + 1}条记录已被插入");
                            return;
                        }
                    }
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    dr.Cells[dataGridView1.Columns["part_name"].Index].Value = dt.Rows[j]["part_name"].ToString();
                    dr.Cells[dataGridView1.Columns["process_name"].Index].Value = dt.Rows[j]["process_name"].ToString();
                    dr.Cells[dataGridView1.Columns["suppliers_name"].Index].Value = dt.Rows[j]["suppliers_name"].ToString();
                    dr.Cells[dataGridView1.Columns["size_no"].Index].Value = dt.Rows[j]["size_no"].ToString();
                    dr.Cells[dataGridView1.Columns["quantity"].Index].Value = dt.Rows[j]["quantity"].ToString();
                    dr.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value = dt.Rows[j]["RECEIVED_QUANTITY"].ToString();
                    dr.Cells[dataGridView1.Columns["part_no"].Index].Value = dt.Rows[j]["part_no"].ToString();
                    dr.Cells[dataGridView1.Columns["process_no"].Index].Value = dt.Rows[j]["process_no"].ToString();
                    dr.Cells[dataGridView1.Columns["suppliers_code"].Index].Value = dt.Rows[j]["suppliers_code"].ToString();
                    dr.Cells[dataGridView1.Columns["PROCEDURE_no"].Index].Value = dt.Rows[j]["PROCEDURE_no"].ToString();
                    dr.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value = dt.Rows[j]["MATERIAL_NO"].ToString();
                    dataGridView1.Rows.Add(dr);
                }
            }
            SetReadOnly();

        }

        private void btnDelNg_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            int count = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((bool)dataGridView1.Rows[i].Cells["dgSelect"].EditedFormattedValue)
                {
                    count++;
                    if (dataGridView1.Rows[i].Cells["id"].Value == null)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == "")
                        {
                            dataGridView1.Rows.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            string idCol = dataGridView1.Rows[i].Cells["id"].Value.ToString();
                            if (i == dataGridView1.Rows.Count - 1)
                            {
                                result += idCol;
                            }
                            else
                            {
                                result += idCol + ",";
                            }
                        }
                    }
                }
            }
            if (count == 0)
            {
                MessageHelper.ShowErr(this, "请选择需要删除的不良记录！");
            }
            if (!string.IsNullOrEmpty(result))
            {
                DialogResult dialogResult = MessageBox.Show("您确认删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (DialogResult.OK == dialogResult)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("result", result);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "DelNg", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        MessageHelper.ShowSuccess(this, "删除成功！");
                        SelectNgInfoBySampleNo();
                        if (dataGridView1.Rows.Count==0)
                        {
                            txtBadOrder.Text = "";
                            cbSampleOrCraft.Enabled = true;
                            cbSampleOrCraft.Text = "";
                            txtStatus.Text = "新单";
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
        }
        public void DataChanged_BadReason(object sender, BadReason.DataChangeEventArgs args)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CAUSES"].Value = args.result;
        }
        public void DataChanged_ResUnit(object sender, ResponsibleUnit.DataChangeEventArgs args)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["RESPONSIBLE_UNIT"].Value = args.result;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.ReadOnly == false)
            {
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "不良原因")
                {
                    BadReason badReason = new BadReason();
                    badReason.DataChange += new BadReason.DataChangeHandler(DataChanged_BadReason);
                    badReason.ShowDialog();
                }
                if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "责任单位")
                {
                    ResponsibleUnit responsibleUnit = new ResponsibleUnit();
                    responsibleUnit.DataChange += new ResponsibleUnit.DataChangeHandler(DataChanged_ResUnit);
                    responsibleUnit.ShowDialog();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is RichTextBox)
                {
                    item.Text = "";
                }
                if (item is DateTimePicker)
                {
                    item.Text = "";
                }
            }
            btnUpdate.Enabled = btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
            dataGridView1.Rows.Clear();
            cbSampleOrCraft.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSampleOrder.Text))
            {
                MessageHelper.ShowErr(this, "样品单编号不能为空!");
                return;
            }
            if (string.IsNullOrEmpty(txtStatus.Text))
            {
                MessageHelper.ShowErr(this, "未查询到该样品单的状态!");
                return;
            }
            if (txtStatus.Text == "待审核")
            {
                btnAddNg.Enabled = btnDelNg.Enabled = btnSave.Enabled = true;
                rtxtReamrk.ReadOnly = false;
                SetReadOnly();
            }
            else
            {
                MessageHelper.ShowErr(this, "状态不是待审核,无法修改！");
            }
        }

        private void SetReadOnly()
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns["part_name"].ReadOnly = dataGridView1.Columns["process_name"].ReadOnly = dataGridView1.Columns["SIZE_NO"].ReadOnly = dataGridView1.Columns["suppliers_name"].ReadOnly = dataGridView1.Columns["QUANTITY"].ReadOnly = dataGridView1.Columns["RECEIVED_QUANTITY"].ReadOnly = dataGridView1.Columns["RESPONSIBLE_UNIT"].ReadOnly = dataGridView1.Columns["CAUSES"].ReadOnly =dataGridView1.Columns["PROCEDURE_no"].ReadOnly= dataGridView1.Columns["suppliers_code"].ReadOnly = dataGridView1.Columns["process_no"].ReadOnly= dataGridView1.Columns["part_no"].ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSampleOrder.Text))
            {
                MessageHelper.ShowErr(this, "样品单编号不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtRegistratDate.Text))
            {
                MessageHelper.ShowErr(this, "登记日期不能为空！");
                return;
            }
            if (cbSampleOrCraft.Text=="")
            {
                MessageHelper.ShowErr(this, "样品室/工艺部不能为空！");
                return;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "没有需要保存的不良记录！");
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 11; j < dataGridView1.Columns.Count - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null || dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                    {
                        int a = i + 1;
                        MessageHelper.ShowErr(this, dataGridView1.Columns[j].HeaderText + "：第" + a + "行第" + j + "列的值为空！");
                        return;
                    }
                }
            }
            string sampleOrCraft = GetSampleOrCraft(cbSampleOrCraft.Text);
            string status = GetStatus(txtStatus.Text);
            DataTable dataTable = GetDgvToTable(dataGridView1);
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("ng_no", txtBadOrder.Text);
            p.Add("sampleOrder", txtSampleOrder.Text);
            p.Add("REGISTER_DATE", txtRegistratDate.Value.ToString("G"));
            p.Add("STATUS", status);
            p.Add("PURPOSE", txtPurpose.Text);
            p.Add("SEASON", txtSeason.Text);
            p.Add("ART_NO", txtArtNo.Text);
            p.Add("ART_NAME", txtShoesType.Text);
            p.Add("COLOR_WAY", txtMatchColor.Text);
            p.Add("MODEL_MASTER", txtModelMater.Text);
            p.Add("PATTERN_MASTER", txtPatternMaster.Text);
            p.Add("REMARK", rtxtReamrk.Text);
            p.Add("dataTable", dataTable);
            p.Add("sampleOrCraft", sampleOrCraft);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SaveSampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show($"保存成功！\n 不良记录编号：{dt.Rows[0]["ng_no"]}", "提醒", MessageBoxButtons.OK);
                    //btnSelectSampleOrder.PerformClick();
                    LoadSampleNg(dt.Rows[0]["ng_no"].ToString(), txtSampleOrder.Text);
                    txtBadOrder.Text = dt.Rows[0]["ng_no"].ToString();
                    txtRegisterUser1.Text = dt.Rows[0]["create_user"].ToString();
                    txtRegisterUser2.Text = dt.Rows[0]["staff_name_create"].ToString();
                    txtRegisterDate.Text = dt.Rows[0]["CREATE_DATE"].ToString();
                    txtModifyUser1.Text = dt.Rows[0]["LAST_USER"].ToString();
                    txtModifyUser2.Text = dt.Rows[0]["staff_name_last"].ToString();
                    txtModifyDate.Text = dt.Rows[0]["LAST_DATE"].ToString();
                    txtStatus.Text = GetStatus(dt.Rows[0]["status"].ToString());
                    txtRegistratUnit.Text = dt.Rows[0]["REGISTRATION_DEPT"].ToString();
                    rtxtReamrk.Text = dt.Rows[0]["remark"].ToString();
                    dataGridView1.ReadOnly = true;
                }
                else
                {
                    MessageHelper.ShowErr(this, "保存失败！");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private string GetSampleOrCraft(string sampleOrCraft) 
        {
            string result=string.Empty;
            if (sampleOrCraft=="样品室")
            {
                result = "Y";
            }
            if (sampleOrCraft=="工艺部")
            {
                result = "N";
            }
            return result;
        }
        /// <summary>
        /// 查询数组中是否存在相同的值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        public static bool IsRepeat(string[] array)
        {
            Hashtable ht = new Hashtable();
            for (int i = 0; i < array.Length; i++)
            {
                if (ht.Contains(array[i]))
                {
                    return true;
                }
                else
                {
                    ht.Add(array[i], array[i]);
                }
            }
            return false;
        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text != "待审核")
            {
                MessageHelper.ShowErr(this, "状态不是待审核，无法删除");
                return;
            }
            DialogResult result = MessageBox.Show("请确认是否删除此单据！", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (DialogResult.OK == result)
            {
                Dictionary<object, object> p = new Dictionary<object, object>();
                p.Add("badOrderNo", txtBadOrder.Text);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "DelSampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    MessageHelper.ShowSuccess(this, "删除成功！");
                    txtStatus.Text = "已删除";
                    btnUpdate.Enabled = btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = false;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void btnExam_Click(object sender, EventArgs e)
        {
            if (txtStatus.Text == "待审核")
            {
                if (string.IsNullOrEmpty(txtSampleOrder.Text))
                {
                    MessageHelper.ShowErr(this, "样品单号不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtBadOrder.Text))
                {
                    MessageHelper.ShowErr(this, "不良编号为空！审核失败！");
                    return;
                }
                DialogResult result = MessageBox.Show("你确定需要审核吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    Dictionary<object, object> p = new Dictionary<object, object>();
                    p.Add("badOrderNo", txtBadOrder.Text);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "ExamSampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        MessageHelper.ShowSuccess(this, "审核成功！");
                        //btnSelectSampleOrder.PerformClick();
                        LoadSampleNg(txtBadOrder.Text, txtSampleOrder.Text);
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "状态不是待审核,无法进行【审核】！");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string badOrder = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            string sampleOrder = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            LoadSampleNg(badOrder, sampleOrder);
            tabControl1.SelectedTab = tabPage1;
        }
        private void LoadSampleNg(string badOrder, string sampleOrder)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("badOrder", badOrder);
            p.Add("sampleOrder", sampleOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Bad_RegistrationServer", "SelectAllByBadOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                List<DataTable> list = JsonConvert.DeserializeObject<List<DataTable>>(json);
                ClearAll();
                DataTable dt = list[0];
                txtBadOrder.Text = dt.Rows[0]["NG_NO"].ToString();
                txtRegistratDate.Value = DateTime.Parse(dt.Rows[0]["REGISTER_DATE"].ToString());
                txtStatus.Text = GetStatus(dt.Rows[0]["STATUS"].ToString());
                cbSampleOrCraft.Text = GetCol(dt.Rows[0]["COL"].ToString());
                txtSeason.Text = dt.Rows[0]["SEASON"].ToString();
                txtPurpose.Text = dt.Rows[0]["PURPOSE"].ToString();
                txtSampleOrder.Text = dt.Rows[0]["SAMPLE_NO"].ToString();
                txtArtNo.Text = dt.Rows[0]["ART_NO"].ToString();
                txtShoesType.Text = dt.Rows[0]["ART_NAME"].ToString();
                txtMatchColor.Text = dt.Rows[0]["COLOR_WAY"].ToString();
                txtPatternMaster.Text = dt.Rows[0]["PATTERN_MASTER"].ToString();
                txtModelMater.Text = dt.Rows[0]["MODEL_MASTER"].ToString();
                txtRegistratUnit.Text = dt.Rows[0]["REGISTRATION_DEPT"].ToString();
                rtxtReamrk.Text = dt.Rows[0]["REMARK"].ToString();
                txtRegisterUser1.Text = dt.Rows[0]["CREATE_USER"].ToString();
                txtRegisterUser2.Text = dt.Rows[0]["create_userName"].ToString();
                txtRegisterDate.Text = dt.Rows[0]["CREATE_DATE"].ToString();
                txtModifyUser1.Text = dt.Rows[0]["LAST_USER"].ToString();
                txtModifyUser2.Text = dt.Rows[0]["last_userName"].ToString();
                txtModifyDate.Text = dt.Rows[0]["LAST_DATE"].ToString();
                if (txtStatus.Text == "已删除" || txtStatus.Text == "已审核")
                {
                    btnUpdate.Enabled = btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = btnAddNg.Enabled = btnDelNg.Enabled = false;
                    rtxtReamrk.ReadOnly = true;
                }
                else
                {
                    btnUpdate.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
                    btnAddNg.Enabled = btnDelNg.Enabled = btnSave.Enabled = false;
                }
                cbSampleOrCraft.Enabled = false;
                DataTable dt2 = list[1];
                if (dt2.Rows.Count > 0)
                {
                    //dataGridView1.DataSource = dt2;
                    dataGridView1.Rows.Clear();
                    dataGridView1.AutoGenerateColumns = false;
                    SetDatagridview1More(dt2);
                    dataGridView1.ReadOnly = true;
                }
                else
                {
                    dataGridView1.Rows.Clear();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private string GetCol(string YorN) 
        {
            string result = string.Empty;
            if (YorN=="N")
            {
                result = "工艺部";
            }
            if (YorN=="Y")
            {
                result = "样品室";
            }
            return result;
        }
        private void SetDatagridview1More(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.Cells[dataGridView1.Columns["part_name"].Index].Value = dt.Rows[j]["part_name"].ToString();
                dr.Cells[dataGridView1.Columns["process_name"].Index].Value = dt.Rows[j]["process_name"].ToString();
                dr.Cells[dataGridView1.Columns["suppliers_name"].Index].Value = dt.Rows[j]["suppliers_name"].ToString();
                dr.Cells[dataGridView1.Columns["size_no"].Index].Value = dt.Rows[j]["size_no"].ToString();
                dr.Cells[dataGridView1.Columns["quantity"].Index].Value = dt.Rows[j]["quantity"].ToString();
                dr.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value = dt.Rows[j]["RECEIVED_QUANTITY"].ToString();
                dr.Cells[dataGridView1.Columns["QTY_L"].Index].Value = dt.Rows[j]["QTY_L"].ToString();
                dr.Cells[dataGridView1.Columns["QTY_R"].Index].Value = dt.Rows[j]["QTY_R"].ToString();
                dr.Cells[dataGridView1.Columns["QTY"].Index].Value = dt.Rows[j]["QTY"].ToString();
                dr.Cells[dataGridView1.Columns["RESPONSIBLE_UNIT"].Index].Value = dt.Rows[j]["RESPONSIBLE_UNIT"].ToString();
                dr.Cells[dataGridView1.Columns["CAUSES"].Index].Value = dt.Rows[j]["CAUSES"].ToString();
                dr.Cells[dataGridView1.Columns["id"].Index].Value = dt.Rows[j]["id"].ToString();
                dr.Cells[dataGridView1.Columns["part_no"].Index].Value = dt.Rows[j]["part_no"].ToString();
                dr.Cells[dataGridView1.Columns["process_no"].Index].Value = dt.Rows[j]["process_no"].ToString();
                dr.Cells[dataGridView1.Columns["suppliers_code"].Index].Value = dt.Rows[j]["suppliers_code"].ToString();
                dr.Cells[dataGridView1.Columns["PROCEDURE_NO"].Index].Value = dt.Rows[j]["PROCEDURE_NO"].ToString();
                dr.Cells[dataGridView1.Columns["material_no"].Index].Value = dt.Rows[j]["material_no"].ToString();
                dataGridView1.Rows.Add(dr);
            }
        }
        private void SetDatagridview1Less(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.Cells[dataGridView1.Columns["part_name"].Index].Value = dt.Rows[j]["part_name"].ToString();
                dr.Cells[dataGridView1.Columns["process_name"].Index].Value = dt.Rows[j]["process_name"].ToString();
                dr.Cells[dataGridView1.Columns["suppliers_name"].Index].Value = dt.Rows[j]["suppliers_name"].ToString();
                dr.Cells[dataGridView1.Columns["size_no"].Index].Value = dt.Rows[j]["size_no"].ToString();
                dr.Cells[dataGridView1.Columns["quantity"].Index].Value = dt.Rows[j]["quantity"].ToString();
                dr.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value = dt.Rows[j]["RECEIVED_QUANTITY"].ToString();
                dr.Cells[dataGridView1.Columns["part_no"].Index].Value = dt.Rows[j]["part_no"].ToString();
                dr.Cells[dataGridView1.Columns["process_no"].Index].Value = dt.Rows[j]["process_no"].ToString();
                dr.Cells[dataGridView1.Columns["suppliers_code"].Index].Value = dt.Rows[j]["suppliers_code"].ToString();
                dr.Cells[dataGridView1.Columns["PROCEDURE_no"].Index].Value = dt.Rows[j]["PROCEDURE_no"].ToString();
                dr.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value = dt.Rows[j]["MATERIAL_NO"].ToString();
                dataGridView1.Rows.Add(dr);
            }
        }
        private void ClearAll()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Name != "txtSampleOrder")
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentCell.ColumnIndex == 7 || dataGridView1.CurrentCell.ColumnIndex == 8)
            //{
            //    double values_one = 0;
            //    double values_two = 0;
            //    if (dataGridView1.Rows[e.RowIndex].Cells[7].Value != null)
            //    {
            //        values_one = double.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "" ? "0" : dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            //    }
            //    else
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells[7].Value = "0";
            //    }
            //    if (dataGridView1.Rows[e.RowIndex].Cells[8].Value != null)
            //    {
            //        values_two = double.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString() == "" ? "0" : dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
            //    }
            //    else
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells[8].Value = "0";
            //    }
            //    string result = ((values_one + values_two) / 2).ToString();
            //    if (dataGridView1.Rows[e.RowIndex].Cells[9].Value != null)
            //    {
            //        if (dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString() != result)
            //        {
            //            dataGridView1.Rows[e.RowIndex].Cells[9].Value = result;
            //        }
            //    }
            //    else
            //    {
            //        dataGridView1.Rows[e.RowIndex].Cells[9].Value = result;
            //    }
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["QTY_L"].Index || e.ColumnIndex == dataGridView1.Columns["QTY_R"].Index))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["QTY_L"] != null || dataGridView1.Rows[e.RowIndex].Cells["QTY_R"] != null)
                {
                    double values_one = 0;
                    double values_two = 0;
                    if (dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value != null)
                    {
                        values_one = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value.ToString() == "" ? "0" : dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value.ToString());
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value = "0";
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value != null)
                    {
                        values_two = double.Parse(dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value.ToString() == "" ? "0" : dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value.ToString());
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value = "0";
                    }
                    string result = ((values_one + values_two) / 2).ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value = result;
                }
            }
        }
    }
}
