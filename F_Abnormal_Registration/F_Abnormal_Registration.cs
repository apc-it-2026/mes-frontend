using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static F_Abnormal_Registration.ItemSelectForm;
using static F_Abnormal_Registration.SelectResponUnit;

namespace F_Abnormal_Registration
{
    public partial class F_Abnormal_Registration : MaterialForm
    {
        DataTable RcptList = new DataTable();
        DataTable respons_dt = new DataTable();
        DataTable CauseList = new DataTable();

        public F_Abnormal_Registration()
        {
            InitializeComponent();
            dataGridView2.AutoGenerateColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            GetStatusUI();
            //LoadCombbox();
            GetResponsList();
            getType();
            GetCauseList();
        }

        private void GetCauseList()
        {
            CauseList = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", txtSampleNo1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "GetCauseList", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                    CauseList = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        void getType()
        {
            List<ComboboxEntry> pp = new List<ComboboxEntry>();
            pp.Add(new ComboboxEntry() { Code = "Y", Name = "样品室" });
            pp.Add(new ComboboxEntry() { Code = "N", Name = "工艺部" });
            comboBox1.DataSource = pp;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Code";
        }

        private void GetResponsList()
        {
            respons_dt = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", txtSampleNo1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "GetResponUnit", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                    respons_dt = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            
        }

        public class ComboboxEntry
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public void GetStatusUI()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("enmu_type", "MESSampleReceive");

            List<ComboboxEntry> pp = new List<ComboboxEntry>();
            pp.Add(new ComboboxEntry { Code = "", Name = "" });
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_BillServer", "GetComboBoxUI", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    pp.Add(new ComboboxEntry { Code = dtJson.Rows[i]["ENUM_CODE"].ToString(), Name = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
            }
            cbStatus1.DataSource = pp;
            cbStatus1.DisplayMember = "Name";
            cbStatus1.ValueMember = "Code";
            Column22.DataSource = pp;
            Column22.DisplayMember = "Name";
            Column22.ValueMember = "Code";

            List<ComboboxEntry> WMSstatusEntries2 = new List<ComboboxEntry> { };
            pp.ForEach(i => WMSstatusEntries2.Add(i));
            this.cbStatus2.DataSource = WMSstatusEntries2;
            this.cbStatus2.DisplayMember = "Name";
            this.cbStatus2.ValueMember = "Code";


        }

        public void GetSelectAbnlno()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("Sample_NO", txtSampleNo1.Text);
            p.Add("ART_NO", txtArtNo1.Text);
            p.Add("ABNL_NO", txtAbnormalNo1.Text);
            p.Add("Create_user", txtCreateUser1.Text);
            p.Add("Create_Date", txtStartDate.Value.ToString("yyyy/MM/dd"));
            p.Add("Create_Date2", txtEndDate.Value.ToString("yyyy/MM/dd"));
            p.Add("status", cbStatus1.SelectedValue.ToString());

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SamplelistExceptionQuery", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson == null || dtJson.Rows.Count <= 0)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "查无此数据！");
                    return;
                }
                else
                {
                    dataGridView2.DataSource = dtJson;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)//查询
        {
            dataGridView2.DataSource = null;
            GetSelectAbnlno();


        }

        //protected void Clear(Control ctrl)
        //{
        //    //ctrl.Text = "";
        //    foreach (Control c in ctrl.Controls)
        //    {
        //        if (c is TextBox)
        //        {
        //               ((TextBox)(c)).Text = "";
        //        }
        //        c.Text = "";
        //        Clear(c);
        //    }
        //}

        private void btnClearContent_Click(object sender, EventArgs e)//重置搜索
        {
            //Application.Restart();
            this.dataGridView2.DataSource = null;
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

        private void btnSelectSampleOrder_Click(object sender, EventArgs e)//样品单查询
        {
           
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Name != "txtSampleOrder2")
                    {
                        item.Text = "";
                    }
                }
                if (item is RichTextBox)
                {
                    item.Text = "";
                }
            }
            cbStatus2.Text = "新单";
            while (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            txtRegistratDate2.Text = "";
            btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
            dataGridView1.ReadOnly = false;
            SelectAbnlInfoBySampleNo();

        }

        private void SelectAbnlInfoBySampleNo()
        {
            if (string.IsNullOrEmpty(txtSampleOrder2.Text))
            {
                MessageHelper.ShowErr(this, "样品单编号不能为空！");
                return;
            }
            //if (string.IsNullOrEmpty(txtAbnormalNo2.Text))
            //{
            //    dataGridView1.ReadOnly = false;
            //    btnInsertData.Enabled = btnDelDate.Enabled = true;
            //}
            //else
            //{
            //    dataGridView1.ReadOnly = true;
            //    btnInsertData.Enabled = btnDelDate.Enabled = false;
            //}
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", txtSampleOrder2.Text);
            p.Add("abnlNo", txtAbnormalNo2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectAbnlInfoBySampleNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                dataGridView1.AutoGenerateColumns = false;
                string json = JsonConvert.DeserializeObject<Dictionary<object, object>>(ret)["RetData"].ToString();
                List<DataTable> list = JsonConvert.DeserializeObject<List<DataTable>>(json);
                DataTable dt1 = list[0];
                if (dt1.Rows.Count > 0)
                {
                    txtArtNo2.Text = dt1.Rows[0]["art_no"].ToString();
                    txtArtName2.Text = dt1.Rows[0]["art_name"].ToString();
                    txtPurpose2.Text = dt1.Rows[0]["purpose"].ToString();
                    txtSeason2.Text = dt1.Rows[0]["season"].ToString();
                    txtMatchColor2.Text = dt1.Rows[0]["color_way"].ToString();
                    txtModelMater2.Text = dt1.Rows[0]["TYPE_LEADER"].ToString();//型体负责人
                    txtPatternMaster2.Text = dt1.Rows[0]["SAMPLE_LEADER"].ToString();//版型负责人
                    //txtSampleOrder2.Text = "";
                    cbStatus2.Text = "新单";
                    txtRegistratUnit2.Text = "";
                    rtxtReamrk2.Text = "";
                    txtRegisterUserNo2.Text = "";
                    txtRegisterDate2.Text = "";
                    txtModifyUserNo2.Text = "";
                    txtModifyDate2.Text = "";
                    txtRegisterUser2.Text = "";
                    txtModifyUser2.Text = "";
                    if (cbStatus2.Text == "")
                    {
                        btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
                    }
                    btnDelDate.Enabled = btnSelect3.Enabled =true;
                    
                }
                else
                {
                    MessageHelper.ShowErr(this, "未查询到该样品单编号的相关信息");
                    ClearAll();
                    return;
                }
                //DataTable dt2 = list[1];
                //if (dt2.Rows.Count > 0)
                //{
                //    //txtAbnormalNo2.Text = dt2.Rows[0]["ABNL_NO"].ToString();
                //    txtStatus2.Text = dt2.Rows[0]["status"].ToString();
                //    txtRegistratUnit2.Text = dt2.Rows[0]["REGISTRATION_DEPT"].ToString();
                //    //rtxtReamrk2.Text = dt2.Rows[0]["note"].ToString();
                //    txtRegisterUserNo2.Text = dt2.Rows[0]["CREATE_USER"].ToString();
                //    txtRegisterDate2.Text = dt2.Rows[0]["CREATE_DATE"].ToString();
                //    txtModifyUserNo2.Text = dt2.Rows[0]["LAST_USER"].ToString();
                //    txtModifyDate2.Text = dt2.Rows[0]["LAST_DATE"].ToString();
                //    txtRegistratDate2.Text = dt2.Rows[0]["REGISTER_DATE"].ToString();
                //    txtRegisterUser2.Text = dt2.Rows[0]["CreateUserName"].ToString();
                //    txtModifyUser2.Text = dt2.Rows[0]["LastUserName"].ToString();
                //    if (txtStatus2.Text == "已删除" || txtStatus2.Text == "已审核")
                //    {
                //        btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = false;
                //    }
                //    else
                //    {
                //        btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
                //    }
                //    rtxtReamrk2.ReadOnly = true;
                //}
                //else
                //{
                //    //txtSampleOrder2.Text = "";
                //    txtStatus2.Text = "新单";
                //    txtRegistratUnit2.Text = "";
                //    rtxtReamrk2.Text = "";
                //    txtRegisterUserNo2.Text = "";
                //    txtRegisterDate2.Text = "";
                //    txtModifyUserNo2.Text = "";
                //    txtModifyDate2.Text = "";
                //    txtRegisterUser2.Text = "";
                //    txtModifyUser2.Text = "";
                //    if (txtStatus2.Text == "")
                //    {
                //        btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
                //    }
                //}
                //DataTable dt3 = list[2];
                //if (dt3.Rows.Count > 0)
                //{
                //    //查询部件
                //    //if (dataGridView1.Columns.Contains("PART_NO"))
                //    //{
                //    //    dataGridView1.Columns.Remove("PART_NO");
                //    //}
                //    //DataGridViewComboBoxColumn colShow = new DataGridViewComboBoxColumn();
                //    //colShow.Name = "PART_NO";
                //    //colShow.HeaderText = "部件";
                //    //colShow.DataPropertyName = "PART_NO";
                //    //colShow.Width = 100;
                //    //Dictionary<string, string> pp = new Dictionary<string, string>();
                //    //pp.Add("sampleOrder", txtSampleOrder2.Text);
                //    //List<ComboboxEntry> WMPartEntries = new List<ComboboxEntry>();
                //    //string ret1 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectPartNo", Program.client.UserToken, JsonConvert.SerializeObject(pp));
                //    //if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                //    //{
                //    //    string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                //    //    DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                //    //    for (int i = 0; i < dtJson.Rows.Count; i++)
                //    //    {
                //    //        colShow.Items.Add(dtJson.Rows[i]["part_desc"].ToString());
                //    //    }
                //    //}
                //    //colShow.DisplayIndex = 2;
                //    //dataGridView1.Columns.Insert(2, colShow);

                //    //////工艺、厂商、码数
                //    ////if (dataGridView1.Columns.Contains("process_name"))
                //    ////{
                //    ////    dataGridView1.Columns.Remove("process_name");
                //    ////}
                //    ////DataGridViewComboBoxColumn colProcessName = new DataGridViewComboBoxColumn();
                //    ////colProcessName.Name = "process_name";
                //    ////colProcessName.HeaderText = "工艺";
                //    ////colProcessName.DataPropertyName = "process_name";
                //    ////colProcessName.Width = 100;

                //    ////if (dataGridView1.Columns.Contains("SUPPLIERS_CODE"))
                //    ////{
                //    ////    dataGridView1.Columns.Remove("SUPPLIERS_CODE");
                //    ////}
                //    ////DataGridViewComboBoxColumn colSUPPLIERS_CODE = new DataGridViewComboBoxColumn();
                //    ////colSUPPLIERS_CODE.Name = "SUPPLIERS_CODE";
                //    ////colSUPPLIERS_CODE.HeaderText = "厂商";
                //    ////colSUPPLIERS_CODE.DataPropertyName = "SUPPLIERS_CODE";
                //    ////colSUPPLIERS_CODE.Width = 100;

                //    ////if (dataGridView1.Columns.Contains("SIZE_NO"))
                //    ////{
                //    ////    dataGridView1.Columns.Remove("SIZE_NO");
                //    ////}
                //    ////DataGridViewComboBoxColumn colSIZE_NO = new DataGridViewComboBoxColumn();
                //    ////colSIZE_NO.Name = "SIZE_NO";
                //    ////colSIZE_NO.HeaderText = "码数";
                //    ////colSIZE_NO.DataPropertyName = "SIZE_NO";
                //    ////colSIZE_NO.Width = 100;

                //    //Dictionary<string, string> a = new Dictionary<string, string>();
                //    //a.Add("sampleOrder", txtSampleOrder2.Text);
                //    //string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectProcessName", Program.client.UserToken, JsonConvert.SerializeObject(a));
                //    ////if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                //    ////{
                //    ////    string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                //    ////    DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                //    ////    for (int i = 0; i < dtJson.Rows.Count; i++)
                //    ////    {
                //    ////        colProcessName.Items.Add(dtJson.Rows[i]["process_name"].ToString());
                //    ////        colSUPPLIERS_CODE.Items.Add(dtJson.Rows[i]["suppliers_name"].ToString());
                //    ////        colSIZE_NO.Items.Add(dtJson.Rows[i]["SIZE_NO"].ToString());
                //    ////    }
                //    ////}
                //    ////colProcessName.DisplayIndex = 3;
                //    ////dataGridView1.Columns.Insert(3, colProcessName);
                //    ////colSUPPLIERS_CODE.DisplayIndex = 4;
                //    ////dataGridView1.Columns.Insert(4, colSUPPLIERS_CODE);
                //    ////colSIZE_NO.DisplayIndex = 5;
                //    ////dataGridView1.Columns.Insert(5, colSIZE_NO);




                //    //dataGridView1.DataSource = dt3;
                //    //dataGridView1.ReadOnly = true;
                //    //    btnInsertData.Enabled = btnDelDate.Enabled = false;


                //}
                //else
                //{
                //    dataGridView1.DataSource = null;
                //}

                

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                ClearAll();
            }
        }

        private void LoadCombbox()
        {
            //责任单位GetResponUnit
            if (dataGridView1.Columns.Contains("RESPONSIBLE_UNIT"))
            {
                dataGridView1.Columns.Remove("RESPONSIBLE_UNIT");
            }
            DataGridViewComboBoxColumn colRESPONSIBLE_UNIT = new DataGridViewComboBoxColumn();
            colRESPONSIBLE_UNIT.Name = "RESPONSIBLE_UNIT";
            colRESPONSIBLE_UNIT.HeaderText = "责任单位";
            colRESPONSIBLE_UNIT.DataPropertyName = "RESPONSIBLE_UNIT";
            colRESPONSIBLE_UNIT.Width = 100;
            Dictionary<string, string> a = new Dictionary<string, string>();
            a.Add("sampleOrder", txtSampleOrder2.Text);
            string ret3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "GetResponUnit", Program.client.UserToken, JsonConvert.SerializeObject(a));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    colRESPONSIBLE_UNIT.Items.Add(dtJson.Rows[i]["STATION_NAME"].ToString());

                }
            }
            colRESPONSIBLE_UNIT.DisplayIndex = 10;
            dataGridView1.Columns.Insert(10, colRESPONSIBLE_UNIT);
        }

        private void ClearAll()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    if (item.Name != "txtSampleOrder2")
                    {
                        item.Text = "";
                    }
                }
            }
        }


        private void clearUI()
        {
            dataGridView1.DataSource = null;



        }

        private void btnDelete_Click(object sender, EventArgs e)//删除
        {
            if (string.IsNullOrEmpty(txtAbnormalNo2.Text.Trim()))
            {
                string msg = UIHelper.UImsg("异常记录编号为空，不能删除", Program.client, "", Program.client.Language);
                MessageBox.Show(msg);
                return;
            }
            else
            {
                DeleteByNo();
            }
        }

        private void DeleteByNo()
        {
            if (string.IsNullOrEmpty(txtAbnormalNo2.Text.Trim())) { return; }
            string ABNL_NO = txtAbnormalNo2.Text.Trim();
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("ABNL_NO", ABNL_NO);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "Delete", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    ClearExceptionInfo();
                    dataGridView1.Enabled = true;
                    panel1.Enabled = true;
                    btnExam.Enabled = true;
                    txtRegistratDate2.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    string msg = UIHelper.UImsg("删除成功！！", Program.client, "", Program.client.Language);
                    SetUI_New();
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK);
                    dataGridView1.Rows.Clear();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception exp)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, exp.Message);
            }
        }



        private void btnAdd_Click(object sender, EventArgs e)//新建
        {
            SetUI_New();
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox || item is RichTextBox)
                {
                    item.Text = "";
                }
            }
            cbStatus2.SelectedValue = "1";
            //while (dataGridView1.Rows.Count != 0)
            //{
            //    dataGridView1.Rows.RemoveAt(0);
            //}
            dataGridView1.Rows.Clear();
            txtRegistratDate2.Text = "";
            btnSave.Enabled = btnExam.Enabled = btnDelete.Enabled = true;
        }

        private void ClearExceptionInfo()
        {
            txtAbnormalNo2.Text = "";
            txtRegistratDate2.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            cbStatus2.Text = "";
            txtSampleOrder2.Text = "";
            txtPurpose2.Text = "";
            txtSeason2.Text = "";
            txtArtNo2.Text = "";
            txtArtName2.Text = "";
            txtMatchColor2.Text = "";
            txtRegistratUnit2.Text = "";
            txtModelMater2.Text = "";
            txtPatternMaster2.Text = "";
            rtxtReamrk2.Text = "";
            txtRegisterUserNo2.Text = "";
            txtRegisterUser2.Text = "";
            txtModifyUserNo2.Text = "";
            txtModifyUser2.Text = "";
            txtRegisterDate2.Text = "";
            txtModifyDate2.Text = "";
            dataGridView1.DataSource = null;
        }

        private void btnClose_Click(object sender, EventArgs e)//关闭
        {
            this.Close();
        }

        //private void btnUpdate_Click(object sender, EventArgs e)//修改
        //{
        //    if (txtStatus2.Text != "待审核")
        //    {
        //        MessageHelper.ShowErr(this, "状态不是待审核！无法【修改】！");
        //        return;
        //    }
        //    dataGridView1.ReadOnly = false;
        //    //btnInsertData.Enabled = btnDelDate.Enabled = true;
        //    rtxtReamrk2.ReadOnly = false;
        //}

        private void btnSave_Click(object sender, EventArgs e)//保存
        {
            if (string.IsNullOrEmpty(txtSampleOrder2.Text))
            {
                MessageHelper.ShowErr(this, "样品单编号不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtRegistratDate2.Text))
            {
                MessageHelper.ShowErr(this, "登记日期不能为空！");
                return;
            }
            if (dataGridView1.Rows.Count <= 0)
            {
                MessageHelper.ShowErr(this, "没有需要保存的异常记录！");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 1; j < dataGridView1.Columns.Count - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null || dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
                    {
                        int a = i + 1;
                        MessageHelper.ShowErr(this, dataGridView1.Columns[j].HeaderText + "：第" + a + "行第" + j + "列的值为空！");
                        return;
                    }
                }
            }

            //string[] str = new string[dataGridView1.Rows.Count];
            //string size_no = "";
            //for (int i = 0; i < str.Length; i++)
            //{
            //    size_no = dataGridView1.Rows[i].Cells["size_no"].Value.ToString();
            //    str[i] = size_no;
            //}
            //if (IsRepeat(str))
            //{
            //    MessageHelper.ShowErr(this, "表格中存在相同尺码！");
            //    return;
            //}

            DataTable dataTable = GetDgvToTable(dataGridView1);
            Dictionary<object, object> p = new Dictionary<object, object>();
            p.Add("ABNL_NO", txtAbnormalNo2.Text);
            p.Add("sampleOrder", txtSampleOrder2.Text);
            p.Add("REGISTER_DATE", txtRegistratDate2.Value.ToString("d"));
            p.Add("STATUS", cbStatus2.Text);
            p.Add("Purpose", txtPurpose2.Text);
            p.Add("SEASON", txtSeason2.Text);
            p.Add("ART_NO", txtArtNo2.Text);
            p.Add("ART_NAME", txtArtName2.Text);
            p.Add("COLOR_WAY", txtMatchColor2.Text);
            p.Add("MODEL_MASTER", txtModelMater2.Text);
            p.Add("PATTERN_MASTER", txtPatternMaster2.Text);
            p.Add("REMARK", rtxtReamrk2.Text);
            p.Add("dataTable", dataTable);
            p.Add("COL1", comboBox1.SelectedValue.ToString());
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SaveSampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show($"保存成功！\n 异常记录编号：{dt.Rows[0]["ABNL_NO"]}", "提醒", MessageBoxButtons.OK);
                    //btnSelectSampleOrder.PerformClick();
                    txtAbnormalNo2.Text = dt.Rows[0]["ABNL_NO"].ToString();
                    txtRegisterUserNo2.Text = dt.Rows[0]["create_user"].ToString();
                    txtRegisterUser2.Text = dt.Rows[0]["staff_name_create"].ToString();
                    txtRegisterDate2.Text = dt.Rows[0]["CREATE_DATE"].ToString();
                    txtModifyUserNo2.Text = dt.Rows[0]["LAST_USER"].ToString();
                    txtModifyUser2.Text = dt.Rows[0]["staff_name_last"].ToString();
                    txtModifyDate2.Text = dt.Rows[0]["LAST_DATE"].ToString();
                    cbStatus2.SelectedValue = dt.Rows[0]["status"].ToString();
                    txtRegistratUnit2.Text = dt.Rows[0]["REGISTRATION_DEPT"].ToString();
                    rtxtReamrk2.Text = dt.Rows[0]["note"].ToString();
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

        private DataTable GetDgvToTable(DataGridView dgv)
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

        /// <summary>
        /// 查询数组中是否存在相同的值
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns></returns>
        private static bool IsRepeat(string[] array)
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

        private void btnExam_Click(object sender, EventArgs e)//审核
        {
            if (cbStatus2.Text == "待审核")
            {
                if (string.IsNullOrEmpty(txtSampleOrder2.Text))
                {
                    MessageHelper.ShowErr(this, "样品单号不能为空！");
                    return;
                }
                if (string.IsNullOrEmpty(txtAbnormalNo2.Text))
                {
                    MessageHelper.ShowErr(this, "异常编号为空！审核失败！");
                    return;
                }
                DialogResult result = MessageBox.Show("你确定需要审核吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    Dictionary<object, object> p = new Dictionary<object, object>();
                    p.Add("AbnormalRecordNo", txtAbnormalNo2.Text);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "ExamSampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        MessageHelper.ShowSuccess(this, "审核成功！");
                        cbStatus2.SelectedValue = "7";
                        SetUI_Effect();

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

        private void SetUI_New()
        {
            txtRegistratDate2.Enabled = true;
            rtxtReamrk2.ReadOnly = false;
            dataGridView1.ReadOnly = false;
            btnSelect3.Enabled = true;
            btnDelDate.Enabled = true;

            //dataGridView1.CellDoubleClick +=  this.dataGridView1_CellDoubleClick;
        }

        private void SetUI_Effect()
        {
            txtRegistratDate2.Enabled = false;
            rtxtReamrk2.ReadOnly = true;
            dataGridView1.ReadOnly = true;
            btnSelect3.Enabled = false;
            btnDelDate.Enabled = false;
            dataGridView1.Columns["RESPONSIBLE_UNIT"].ReadOnly = dataGridView1.Columns["CAUSES"].ReadOnly= true;

           // dataGridView1.CellDoubleClick -= this.dataGridView1_CellDoubleClick;
        }

        private void btnExamMore_Click(object sender, EventArgs e)//批量审核
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "ABNL_NO", DataType = typeof(string) });
            if (dataGridView2.Rows.Count > 0 && dataGridView2 != null)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if ((bool)row.Cells[0].EditedFormattedValue)
                    {
                        if (dataGridView2.Rows[row.Index].Cells[dataGridView2.Columns["Column22"].Index].Value.ToString() == "2")
                        {
                            dt.Rows.Add(row.Cells["ABNL_NO"].Value.ToString());
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("你确定需要批量审核？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        Dictionary<object, object> p = new Dictionary<object, object>();
                        p.Add("Abnl_no_dt", dt);
                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "ExamMore", Program.client.UserToken, JsonConvert.SerializeObject(p));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            MessageHelper.ShowSuccess(this, "批量审核成功！");
                            LoadDatagridview2();
                            GetSelectAbnlno();



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
                    MessageHelper.ShowErr(this, "您还没有选择或你勾选的异常记录已审核！");
                }
            }
        }

        private void LoadDatagridview2()
        {
            dataGridView2.AutoGenerateColumns = false;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("Sample_NO", txtSampleNo1.Text);
            p.Add("ART_NO", txtArtNo1.Text);
            p.Add("ABNL_NO", txtAbnormalNo1.Text);
            p.Add("Create_user", txtCreateUser1.Text);
            p.Add("startDate", txtStartDate.Value.ToString("d"));
            p.Add("endDate", txtEndDate.Value.ToString("d"));
            p.Add("Status", cbStatus1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectBySampleOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                ((DataTable)dataGridView1.DataSource).Rows.Add();
                int rowindexvalue = dataGridView1.Rows.Count;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = rowindexvalue;
            }
            else
            {
                if (dataGridView1.Rows.Count <= 0)
                {
                    dataGridView1.DataSource = new DataTable();
                    ((DataTable)dataGridView1.DataSource).Rows.Add();
                    //dataGridView1.Rows.Add();
                    int rowindexvalue = dataGridView1.Rows.Count;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = rowindexvalue;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.AutoGenerateColumns = false;
                    //加载部件
                    if (dataGridView1.Columns.Contains("PART_NO"))
                    {
                        dataGridView1.Columns.Remove("PART_NO");
                    }
                    DataGridViewComboBoxColumn colShow = new DataGridViewComboBoxColumn();
                    colShow.Name = "PART_NO";
                    colShow.HeaderText = "部件";
                    colShow.DataPropertyName = "PART_NO";
                    colShow.Width = 100;
                    Dictionary<string, string> pp = new Dictionary<string, string>();
                    pp.Add("sampleOrder", txtSampleOrder2.Text);
                    string ret1 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectPartNo", Program.client.UserToken, JsonConvert.SerializeObject(pp));
                    List<ComboboxEntry> WMProcessEntries = new List<ComboboxEntry> { };
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                    {
                        string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                        DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            colShow.Items.Add(dtJson.Rows[i]["part_desc"].ToString());
                        }
                        colShow.DisplayIndex = 2;
                        dataGridView1.Columns.Insert(2, colShow);
                        //for (int i = 0; i < dtJson.Rows.Count; i++)
                        //{
                        //    WMProcessEntries.Add(new ComboboxEntry() { Code = dtJson.Rows[i]["part_no"].ToString(), Name = dtJson.Rows[i]["part_desc"].ToString() });
                        //}
                        //((DataGridViewComboBoxCell)dataGridView1.Rows[rowindexvalue - 1].Cells["part_no"]).DataSource = WMProcessEntries;
                        //((DataGridViewComboBoxCell)dataGridView1.Rows[rowindexvalue - 1].Cells["part_no"]).DisplayMember = "Name";
                        //((DataGridViewComboBoxCell)dataGridView1.Rows[rowindexvalue - 1].Cells["part_no"]).ValueMember = "Code";
                    }

                    // PART_NO.DataSource = WMProcessEntries;

                    //工艺、厂商、码数
                    if (dataGridView1.Columns.Contains("process_name"))
                    {
                        dataGridView1.Columns.Remove("process_name");
                    }
                    DataGridViewComboBoxColumn colProcessName = new DataGridViewComboBoxColumn();
                    colProcessName.Name = "process_name";
                    colProcessName.HeaderText = "工艺";
                    colProcessName.DataPropertyName = "process_name";
                    colProcessName.Width = 100;

                    if (dataGridView1.Columns.Contains("SUPPLIERS_CODE"))
                    {
                        dataGridView1.Columns.Remove("SUPPLIERS_CODE");
                    }
                    DataGridViewComboBoxColumn colSUPPLIERS_CODE = new DataGridViewComboBoxColumn();
                    colSUPPLIERS_CODE.Name = "SUPPLIERS_CODE";
                    colSUPPLIERS_CODE.HeaderText = "厂商";
                    colSUPPLIERS_CODE.DataPropertyName = "SUPPLIERS_CODE";
                    colSUPPLIERS_CODE.Width = 100;

                    if (dataGridView1.Columns.Contains("SIZE_NO"))
                    {
                        dataGridView1.Columns.Remove("SIZE_NO");
                    }
                    DataGridViewComboBoxColumn colSIZE_NO = new DataGridViewComboBoxColumn();
                    colSIZE_NO.Name = "SIZE_NO";
                    colSIZE_NO.HeaderText = "码数";
                    colSIZE_NO.DataPropertyName = "SIZE_NO";
                    colSIZE_NO.Width = 100;

                    Dictionary<string, string> a = new Dictionary<string, string>();
                    a.Add("sampleOrder", txtSampleOrder2.Text);
                    string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectProcessName", Program.client.UserToken, JsonConvert.SerializeObject(a));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                    {
                        string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                        DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);
                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            colProcessName.Items.Add(dtJson.Rows[i]["process_name"].ToString());
                            colSUPPLIERS_CODE.Items.Add(dtJson.Rows[i]["suppliers_name"].ToString());
                            colSIZE_NO.Items.Add(dtJson.Rows[i]["SIZE_NO"].ToString());
                        }
                    }
                    colProcessName.DisplayIndex = 3;
                    dataGridView1.Columns.Insert(3, colProcessName);
                    colSUPPLIERS_CODE.DisplayIndex = 4;
                    dataGridView1.Columns.Insert(4, colSUPPLIERS_CODE);
                    colSIZE_NO.DisplayIndex = 5;
                    dataGridView1.Columns.Insert(5, colSIZE_NO);


                    //责任单位GetResponUnit
                    if (dataGridView1.Columns.Contains("RESPONSIBLE_UNIT"))
                    {
                        dataGridView1.Columns.Remove("RESPONSIBLE_UNIT");
                    }
                    DataGridViewComboBoxColumn colRESPONSIBLE_UNIT = new DataGridViewComboBoxColumn();
                    colRESPONSIBLE_UNIT.Name = "RESPONSIBLE_UNIT";
                    colRESPONSIBLE_UNIT.HeaderText = "责任单位";
                    colRESPONSIBLE_UNIT.DataPropertyName = "RESPONSIBLE_UNIT";
                    colRESPONSIBLE_UNIT.Width = 100;
                    string ret3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "GetResponUnit", Program.client.UserToken, JsonConvert.SerializeObject(a));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
                    {
                        string json1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                        DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json1);


                        for (int i = 0; i < dtJson.Rows.Count; i++)
                        {
                            colRESPONSIBLE_UNIT.Items.Add(dtJson.Rows[i]["STATION_NAME"].ToString());
                        }
                    }
                    colRESPONSIBLE_UNIT.DisplayIndex = 11;
                    dataGridView1.Columns.Insert(11, colRESPONSIBLE_UNIT);
                    //dataGridView1.DataSource = new DataTable();
                }
                else
                {
                    ((DataTable)dataGridView1.DataSource).Rows.Add();
                    int rowindexvalue = dataGridView1.Rows.Count;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = rowindexvalue;
                }
            }
        }

        private void btnDelDate_Click(object sender, EventArgs e)
        {
            if ("".Equals(txtAbnormalNo2.Text))
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                        dataGridView1.Rows.RemoveAt(i);
                }
                
            }
            else
            {
                DataTable deleteDt = new DataTable();
                DataColumn dc1 = new DataColumn("PART_NO", Type.GetType("System.String"));
                DataColumn dc3 = new DataColumn("SUPPLIERS_CODE", Type.GetType("System.String"));
                DataColumn dc4 = new DataColumn("SIZE_NO", Type.GetType("System.String"));
                deleteDt.Columns.Add(dc1);
                deleteDt.Columns.Add(dc3);
                deleteDt.Columns.Add(dc4);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                    {
                        string part_no = dataGridView1.Rows[i].Cells["PART_NO"].Value.ToString();
                        string suppliers_code = dataGridView1.Rows[i].Cells["SUPPLIERS_CODE"].Value.ToString();
                        string size_no = dataGridView1.Rows[i].Cells["SIZE_NO"].Value.ToString();

                        DataRow dr = deleteDt.NewRow();
                        dr["PART_NO"] = part_no;
                        dr["SUPPLIERS_CODE"] = suppliers_code;
                        dr["SIZE_NO"] = size_no;
                        deleteDt.Rows.Add(dr);

                        //if (dataGridView1.Rows[i].Cells["registed_qty"].Value == null)
                        //{
                        //    dataGridView1.Rows.RemoveAt(i);
                        //    i--;
                        //}
                        //else
                        //{
                        //    if (dataGridView1.Rows[i].Cells["registed_qty"].Value.ToString() == "")
                        //    {
                        //        dataGridView1.Rows.RemoveAt(i);
                        //        i--;
                        //    }
                        //    else
                        //    {
                        //        string idCol = dataGridView1.Rows[i].Cells["registed_qty"].Value.ToString();
                        //        if (i == dataGridView1.Rows.Count - 1)
                        //        {
                        //            result += idCol;
                        //        }
                        //        else
                        //        {
                        //            result += idCol + ",";
                        //        }
                        //    }
                        //}
                    }
                }
                if (deleteDt.Rows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("您确认删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (DialogResult.OK == dialogResult)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("result", deleteDt);
                        dic.Add("abnlNo", txtAbnormalNo2.Text);
                        string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "DelAbnl", Program.client.UserToken, JsonConvert.SerializeObject(dic));
                        if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                        {
                            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                            {
                                if ((bool)dataGridView1.Rows[i].Cells[0].EditedFormattedValue)
                                    dataGridView1.Rows.RemoveAt(i);
                            }

                            MessageHelper.ShowSuccess(this, "删除成功！");
                            SelectAbnlInfoBySampleNo();
                        }
                        else
                        {
                            MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        }
                    }
                }
            }
            
        }
        public void BindCombobox(DataGridViewComboBoxCell comboBoxCell)
        {
            DataTable stepType = new DataTable();
            stepType.Columns.Add("Value");
            stepType.Columns.Add("Name");
            DataRow type;

            type = stepType.NewRow();
            type[0] = "亮雾";
            type[1] = "亮雾";
            stepType.Rows.Add(type);

            type = stepType.NewRow();
            type[0] = "打皱";
            type[1] = "打皱";
            stepType.Rows.Add(type);
            type = stepType.NewRow();
            type[0] = "痕迹";
            type[1] = "痕迹";
            stepType.Rows.Add(type);
            type = stepType.NewRow();
            type[0] = "色差";
            type[1] = "色差";
            stepType.Rows.Add(type);
            type = stepType.NewRow();
            type[0] = "脱落";
            type[1] = "脱落";
            stepType.Rows.Add(type);

            comboBoxCell.ValueMember = "Value";
            comboBoxCell.DisplayMember = "Name";
            comboBoxCell.DataSource = stepType;
        }

        private void btnSelect3_Click(object sender, EventArgs e)
        {
            GetRcptList(txtSampleOrder2.Text);

            ItemSelectForm frm = new ItemSelectForm(RcptList);
            frm.DataChange += new ItemSelectForm.DataChangeHandler(DataChanged_item);
            frm.Location = PointToScreen((sender as Control).Location);
            frm.ShowDialog();
        }

        private void DataChanged_item(object sender, ItemDataChangeEventArgs args)
        {
            dataGridView1.Columns["RESPONSIBLE_UNIT"].ReadOnly = dataGridView1.Columns["CAUSES"].ReadOnly = true;
            DataTable dt = args.value1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string procedure_no = dataGridView1.Rows[i].Cells["PROCEDURE_NO"].Value.ToString();
                string material_no = dataGridView1.Rows[i].Cells["MATERIAL_NO"].Value.ToString();
                string part_no = dataGridView1.Rows[i].Cells["PART_NO"].Value.ToString();
                string suppliers_code = dataGridView1.Rows[i].Cells["SUPPLIERS_CODE"].Value.ToString();
                string size_no = dataGridView1.Rows[i].Cells["SIZE_NO"].Value.ToString();
                string process_no = dataGridView1.Rows[i].Cells["Process_no"].Value.ToString();
                string process_name = dataGridView1.Rows[i].Cells["Process_name"].Value.ToString();
                
                string filter = "PROCEDURE_NO = '" + procedure_no + "' and MATERIAL_NO = '" + material_no + "' and PART_NO = '" + part_no + "' and SUPPLIERS_CODE = '" + suppliers_code +
                    "' and SIZE_NO = '" + size_no + "'and Process_no = '" + process_no + "'and Process_name = '" + process_name + "'";
                DataView dv = dt.DefaultView;
                dv.RowFilter = filter;
                if (dv.Count > 0)
                {
                    MessageBox.Show("记录已插入！");
                    return;
                }
            }
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                string procedure_no = dt.Rows[k]["PROCEDURE_NO"].ToString();
                string material_no = dt.Rows[k]["MATERIAL_NO"].ToString();
                string partNo = dt.Rows[k]["PART_NO"].ToString();
                string namet = dt.Rows[k]["NAME_T"].ToString();
                string process_no = dt.Rows[k]["Process_no"].ToString();
                string process_name = dt.Rows[k]["Process_name"].ToString();
                string suppliersCode = dt.Rows[k]["SUPPLIERS_CODE"].ToString();
                string suppliersName = dt.Rows[k]["SUPPLIES_NAME"].ToString();
                string sizeNo = dt.Rows[k]["SIZE_NO"].ToString();
                string quantity = dt.Rows[k]["QUANTITY"].ToString();
                string receivedQty = dt.Rows[k]["RECEIVED_QUANTITY"].ToString();
                string registedQty = dt.Rows[k]["registed_qty"].ToString();


                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.Cells[dataGridView1.Columns["PROCEDURE_NO"].Index].Value = procedure_no;
                dr.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value = material_no;
                dr.Cells[dataGridView1.Columns["PART_NO"].Index].Value = partNo;
                dr.Cells[dataGridView1.Columns["NAME_T"].Index].Value = namet;
                dr.Cells[dataGridView1.Columns["Process_no"].Index].Value = process_no;
                dr.Cells[dataGridView1.Columns["Process_name"].Index].Value = process_name;
                dr.Cells[dataGridView1.Columns["SUPPLIERS_CODE"].Index].Value = suppliersCode;
                dr.Cells[dataGridView1.Columns["SUPPLIES_NAME"].Index].Value = suppliersName;
                dr.Cells[dataGridView1.Columns["SIZE_NO"].Index].Value = sizeNo;
                dr.Cells[dataGridView1.Columns["QUANTITY"].Index].Value = quantity;
                dr.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value = receivedQty;
                dataGridView1.Rows.Add(dr);
                //((DataTable)dataGridView1.DataSource).Rows.Add(dr);
            }





        }

        public DataTable GetRcptList(string sampleNo)
        {
            RcptList = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("sampleOrder", sampleNo);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "GetRcptList", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                if (dtJson.Rows.Count > 0)
                    if (dtJson.Rows.Count > 0)
                    RcptList = dtJson;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return RcptList;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dataGridView1.Columns["QTY_L"].Index || e.ColumnIndex == dataGridView1.Columns["QTY_R"].Index))
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["QTY_L"] != null || dataGridView1.Rows[e.RowIndex].Cells["QTY_R"] != null)
                {
                    try
                    {
                        decimal qty = (decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value == null ? "0" : dataGridView1.Rows[e.RowIndex].Cells["QTY_L"].Value.ToString()) + decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value == null ? "0" : dataGridView1.Rows[e.RowIndex].Cells["QTY_R"].Value.ToString())) / 2;

                        //RECEIVED_QUANTITY
                        decimal received_qty = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["RECEIVED_QUANTITY"].Value == null ? "0" : dataGridView1.Rows[e.RowIndex].Cells["RECEIVED_QUANTITY"].Value.ToString());
                        //if (qty > received_qty)
                        //{
                        //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        //    MessageBox.Show("影响数量不可大于收料数量！");
                        //    return;
                        //}
                        dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value = qty;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value = "";
                    }
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["QTY"].Value = "";
                }

            }



        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex > -1)
            {
                //DgvbtnDelete  
                if (dataGridView2.Columns[e.ColumnIndex].Name == "btncheck" && !string.IsNullOrEmpty(dataGridView2.Rows[e.RowIndex].Cells["ABNL_NO"].Value.ToString()))
                {
                    //查询异常单明细
                    string abnlNo = dataGridView2.Rows[e.RowIndex].Cells["ABNL_NO"].Value.ToString();
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("ABNL_NO", abnlNo);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.F_Abnormal_RegistrationServer", "SelectAbnlByAblnNo", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                        List<DataTable> list = JsonConvert.DeserializeObject<List<DataTable>>(json);
                        DataTable dt1 = list[0];
                        DataTable dt2 = list[1];
                        if (dt1.Rows.Count > 0)
                        {
                            txtAbnormalNo2.Text = dt1.Rows[0]["ABNL_NO"].ToString();
                            txtRegistratDate2.Text = dt1.Rows[0]["Register_Date"].ToString();
                            cbStatus2.SelectedValue = dt1.Rows[0]["Status"].ToString();
                            txtSampleOrder2.Text = dt1.Rows[0]["Sample_NO"].ToString();
                            txtPurpose2.Text = dt1.Rows[0]["purpose"].ToString();
                            txtSeason2.Text = dt1.Rows[0]["Season"].ToString();
                            txtArtNo2.Text = dt1.Rows[0]["art_no"].ToString();
                            txtArtName2.Text = dt1.Rows[0]["name_t"].ToString();
                            txtMatchColor2.Text = dt1.Rows[0]["color_way"].ToString();
                            txtRegistratUnit2.Text = dt1.Rows[0]["registration_dept"].ToString();
                            txtModelMater2.Text = dt1.Rows[0]["type_leader"].ToString();
                            txtPatternMaster2.Text = dt1.Rows[0]["sample_leader"].ToString();
                            rtxtReamrk2.Text = dt1.Rows[0]["note"].ToString();
                            txtRegisterUserNo2.Text = dt1.Rows[0]["create_user"].ToString();
                            txtRegisterUser2.Text = dt1.Rows[0]["CreateUserName"].ToString();
                            txtRegisterDate2.Text = dt1.Rows[0]["create_date"].ToString();
                            txtModifyUserNo2.Text = dt1.Rows[0]["last_user"].ToString();
                            txtModifyUser2.Text = dt1.Rows[0]["LastUserName"].ToString();
                            txtModifyDate2.Text = dt1.Rows[0]["last_date"].ToString();
                            comboBox1.SelectedValue = dt1.Rows[0]["COL1"].ToString();


                            if ("7".Equals(dt1.Rows[0]["Status"].ToString()))
                                SetUI_Effect();
                            else
                                SetUI_New();


                            dataGridView1.Rows.Clear();
                            for (int k = 0; k < dt2.Rows.Count; k++)
                            {
                                string procedureNo = dt2.Rows[k]["PROCEDURE_NO"].ToString();
                                string materialNo = dt2.Rows[k]["MATERIAL_NO"].ToString();
                                string partNo = dt2.Rows[k]["PART_NO"].ToString();
                                string namet = dt2.Rows[k]["NAME_T"].ToString();
                                string processNo = dt2.Rows[k]["Process_no"].ToString();
                                string processName = dt2.Rows[k]["Process_name"].ToString();
                                string suppliersCode = dt2.Rows[k]["SUPPLIERS_CODE"].ToString();
                                string suppliersName = dt2.Rows[k]["SUPPLIES_NAME"].ToString();
                                string sizeNo = dt2.Rows[k]["SIZE_NO"].ToString();
                                string quantity = dt2.Rows[k]["QUANTITY"].ToString();
                                string receivedQty = dt2.Rows[k]["RECEIVED_QUANTITY"].ToString();
                                string qtyL = dt2.Rows[k]["QTY_L"].ToString();
                                string qtyR = dt2.Rows[k]["QTY_R"].ToString();
                                string registedQty = dt2.Rows[k]["qty"].ToString();
                                string responsibleUnit = dt2.Rows[k]["RESPONSIBLE_UNIT"].ToString();
                                string responsibleName = dt2.Rows[k]["RESPONSIBLE_NAME"].ToString();
                                string causes = dt2.Rows[k]["CAUSES"].ToString();


                                DataGridViewRow dr = new DataGridViewRow();
                                dr.CreateCells(dataGridView1);
                                dr.Cells[dataGridView1.Columns["PROCEDURE_NO"].Index].Value = procedureNo;
                                dr.Cells[dataGridView1.Columns["MATERIAL_NO"].Index].Value = materialNo;
                                dr.Cells[dataGridView1.Columns["PART_NO"].Index].Value = partNo;
                                dr.Cells[dataGridView1.Columns["NAME_T"].Index].Value = namet;
                                dr.Cells[dataGridView1.Columns["Process_no"].Index].Value = processNo;
                                dr.Cells[dataGridView1.Columns["Process_name"].Index].Value = processName;
                                dr.Cells[dataGridView1.Columns["SUPPLIERS_CODE"].Index].Value = suppliersCode;
                                dr.Cells[dataGridView1.Columns["SUPPLIES_NAME"].Index].Value = suppliersName;
                                dr.Cells[dataGridView1.Columns["SIZE_NO"].Index].Value = sizeNo;
                                dr.Cells[dataGridView1.Columns["QUANTITY"].Index].Value = quantity;
                                dr.Cells[dataGridView1.Columns["RECEIVED_QUANTITY"].Index].Value = receivedQty;
                                dr.Cells[dataGridView1.Columns["QTY_L"].Index].Value = qtyL;
                                dr.Cells[dataGridView1.Columns["QTY_R"].Index].Value = qtyR;
                                dr.Cells[dataGridView1.Columns["qty"].Index].Value = registedQty;
                                dr.Cells[dataGridView1.Columns["RESPONSIBLE_UNIT"].Index].Value = responsibleName;
                                dr.Cells[dataGridView1.Columns["CAUSES"].Index].Value = causes;
                                dr.Cells[dataGridView1.Columns["STATION_NO1"].Index].Value = responsibleUnit;
                                dataGridView1.Rows.Add(dr);
                                
                            }
                            

                            tabControl1.SelectedTab = tabPage1;
                        }
                     
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }

                }
            }
        }


        public void DataChanged_Cause(object sender, SelectCause.DataChangeEventArgs args)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Cells["CAUSES"].Value = args.value1;
        }

        public void DataChanged_Response(object sender, DataChangeEventArgs args)
        {
            int index = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[index].Cells["RESPONSIBLE_UNIT"].Value = args.value2;
            dataGridView1.Rows[index].Cells["STATION_NO1"].Value = args.value1;
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.ReadOnly==false)
            {
                int index = e.RowIndex;
                if (dataGridView1.Rows[index].Cells[1].Value != null && !"".Equals(dataGridView1.Rows[index].Cells[1].Value.ToString().Trim()))
                {
                    if (e.ColumnIndex == dataGridView1.Columns["RESPONSIBLE_UNIT"].Index)
                    {
                        SelectResponUnit frm = new SelectResponUnit(respons_dt.Copy(), "STATION_NO", "STATION_NAME");
                        frm.DataChange += new SelectResponUnit.DataChangeHandler(DataChanged_Response);
                        frm.ShowDialog();
                    }
                }

                if (dataGridView1.Rows[index].Cells[1].Value != null && !"".Equals(dataGridView1.Rows[index].Cells[1].Value.ToString().Trim()))
                {
                    if (e.ColumnIndex == dataGridView1.Columns["CAUSES"].Index)
                    {
                        SelectCause frm = new SelectCause(CauseList.Copy(), "code_name");
                        frm.DataChange += new SelectCause.DataChangeHandler(DataChanged_Cause);
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void F_Abnormal_Registration_Load(object sender, EventArgs e)
        {

        }
    }
}
