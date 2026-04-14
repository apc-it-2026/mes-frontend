using CommanClassLib.Util;
using MaterialSkin.Controls;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblyOutput_Domestic
{
    public partial class AssemblyOutput_Domestic : MaterialForm
    {
        delegate void SetTextCallBack(int hour);
        Bitmap smile = null;
        Bitmap cry = null;
        int dayFinishQty = 0;
        int seQty = 0;
        int seFinishQty = 0;
        int sizeUnfinishQty = 0;
        int hourQty = 0;
        Thread hourThread = null;
        DataTable workDayDt = null;
        DataTable workDaySizeDt = null;
        string d_dept = "";
        string d_dept_name = "";
        string orgId = "";
        string size = "";

        Button btnSize;
        bool isScan = true;
        string po_btn = "";


        private string strValue;
        public string StrValue
        {
            set
            {
                strValue = value;
            }
        }
        public AssemblyOutput_Domestic()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        private void ButSelectDept_Click(object sender, EventArgs e)
        {
            TrackOut_AssemblyOrder_DepSelect frm = new TrackOut_AssemblyOrder_DepSelect();
            frm.DataChange += new TrackOut_AssemblyOrder_DepSelect.DataChangeHandler(DataChanged);
            frm.ShowDialog();
        }
       


        public void Go()
        {
            while (true)
            {
                int hour = DateTime.Now.Hour;
                if (!hour.ToString().Equals(labelHour.Text))
                {
                    SetHour(hour);
                }
                Thread.Sleep(1000 * 1);
            }
        }

        private void SetHour(int hour)
        {
            if (this.labelHour.InvokeRequired)
            {
                SetTextCallBack stcb = new SetTextCallBack(SetHour);
                this.Invoke(stcb, new object[] { hour });
            }
            else
            {
                if (!hour.ToString().Equals(labelHour.Text))
                {
                    labelHour.Text = hour.ToString();
                }
            }
        }

        //load department
        private void LoadDept()
        {
            try
            {
                GetDept();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            labelDeptNo.Text = d_dept;
            labelDeptName.Text = d_dept_name;
        }

        //Load the total number of completions for the day
        private void LoadDayFinishQty()
        {
            try
            {
                GetDayFinishQty();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //Load PO list
        private void LoadDayPo()
        {
            this.PoPanel1.Controls.Clear();
            try
            {
                workDayDt = GetSeId_Po();
                for (int i = 0; i < workDayDt.Rows.Count; i++)
                {
                    RadioButton poRB = new RadioButton();
                    poRB = new RadioButton();
                    poRB.Text = workDayDt.Rows[i]["PO"].ToString();
                    poRB.AutoSize = true;
                    poRB.Font = new Font("微软雅黑", 32F);
                    poRB.Margin = new Padding(35, 10, 0, 0);
                    this.PoPanel1.Controls.Add(poRB);
                    poRB.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //Load current output
        private void LoadHourQty()
        {
            int hour = DateTime.Now.Hour;
            labelHour.Text = hour.ToString();
            try
            {
                GetHourQty();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            textHourQty.Text = hourQty.ToString();
        }

        //Load scan records of the day
        private void LoadScanLog()
        {
            dataGridView1.Rows.Clear();
            try
            {
                DataTable logDt = GetScanLog();
                for (int i = 0; i < logDt.Rows.Count; i++)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(dataGridView1);
                    dr.Cells[0].Value = logDt.Rows[i]["SE_ID"].ToString();
                    dr.Cells[1].Value = logDt.Rows[i]["PO_NO"].ToString();
                    dr.Cells[2].Value = logDt.Rows[i]["SIZE_NO"].ToString();
                    dr.Cells[3].Value = logDt.Rows[i]["ART_NO"].ToString();
                    dr.Cells[4].Value = Convert.ToDateTime(logDt.Rows[i]["SCAN_DATE"]).ToString();
                    dataGridView1.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //Specify the total number of scans for the order
        private void SetSeFinishQty(string seId)
        {
            try
            {
                GetSeFinishQty(seId);
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            textSeFinishQty.Text = seFinishQty.ToString();
        }

        //Specify the total quantity of order packaging and scheduling
        private void SetSeUnFinishQty(string seId)
        {
            try
            {
                GetSeQty(seId);
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            textSeUnFinishQty.Text = (seQty - seFinishQty).ToString();
        }

        private void SetDayFinishQty()
        {
            textDayFinishQty.Text = dayFinishQty.ToString();
        }

        //Get department code, name
        private void GetDept()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetDept", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                d_dept = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                d_dept_name = dtJson.Rows[0]["DEPARTMENT_NAME"].ToString();
                orgId = dtJson.Rows[0]["ORG_ID"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Get the number of scanned today
        private void GetDayFinishQty()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetPackingDayFinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dayFinishQty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //Get the task list of the group for the day
        private DataTable GetSeId_Po()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetSeId_Po", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void GetHourQty()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetHourQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                hourQty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private DataTable GetScanLog()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetScanLog", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void GetSeFinishQty(string seId)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vSeId", seId);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetSeFinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                seFinishQty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetSeQty(string seId)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            p.Add("vSeId", seId);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetSeQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                seQty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        //get art_name  can use same method for domestic also (comment written by Ashok)
        private string GetArtName(string artNo)
        {
            string artName = "";
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vArtNo", artNo);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetArtName", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                artName = json;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return artName;
        }

        //Get the task list of the group's SIZE for the day
        private DataTable GetWorkDaySize(string vPO)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vSeId", "");
            p.Add("vInOut", "OUT");
            p.Add("vPO", vPO);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetPackingWorkDaySize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    dt = dt.Rows.Cast<DataRow>().OrderBy(r => r["print_seq"].ToDecimal()).CopyToDataTable();//Sort by size
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }


        //same method is enough for Domestic also(Commented by Ashok)

        //Query related order information according to the inner box label
        private DataTable GetLabelDetail(string label, string vPO, string vSeId)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vLabel", label);
            p.Add("vOrgId", orgId);
            p.Add("vPO", vPO);
            p.Add("vSeId", vSeId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionOutputOrderServer", "GetLabelDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        //Query the outstanding quantity of the specified order size
        private int GetUnfinishQty(string se_id, string size)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", se_id);
            p.Add("vDDept", d_dept);
            p.Add("vSizeNo", size);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetUnfinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                qty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }

        //Query the specified order, the input quantity on the day of size and the total number of scans
        private DataTable GetOutDetail(string se_id, string size)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", se_id);
            p.Add("vDDept", d_dept);
            p.Add("vSizeNo", size);
            p.Add("vInOut", "OUT");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetOutDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        //Query the quantity of the specified order size_no
        private int GetSizeQty(string se_id, string size)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", se_id);
            p.Add("vSizeNo", size);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "GetSizeQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                decimal val = Decimal.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString());
                return Decimal.ToInt32(val);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return 0;
            }
        }

        /// <summary>
        /// Update the completed quantity
        /// </summary>
        /// <param name="seId"></param>
        /// <param name="SizeNo"></param>
        /// <param name="scan_ip"></param>
        /// <param name="label"></param>
        /// <param name="scanPZ">"A" scan entry, "A" hand point entry</param>
        /// <returns></returns>
        private bool updateOutFinshQty(string seId, string SizeNo, string ProdOrder, string MainProdOrder, string scan_ip, string label, string scanPZ)
        {
            bool isOK = false;
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vDDept", d_dept);
            p.Add("vSeId", seId);
            p.Add("vSizeNo", SizeNo);
            p.Add("vProdOrder", ProdOrder);
            p.Add("vMainProdOrder", MainProdOrder);
            p.Add("vIP", scan_ip);
            p.Add("vLabel", label);
            p.Add("vScanPZ", scanPZ);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server", "updateOutFinshQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }

        //switch po
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked)
            {
                return;
            }
            string po = rb.Text;
            //string se_id = "";

            for (int i = 0; i < workDayDt.Rows.Count; i++)
            {
                if (po.Equals(workDayDt.Rows[i]["PO"].ToString()))
                {
                    //se_id = workDayDt.Rows[i]["SE_ID"].ToString();

                    labelPo.Text = workDayDt.Rows[i]["PO"].ToString();
                    //labelArt.Text = workDayDt.Rows[i]["ART_NO"].ToString();
                    //labelSeId.Text = se_id;
                    //labelArtName.Text = GetArtName(workDayDt.Rows[i]["ART_NO"].ToString());
                    //labelSize.Text = "";
                    //labelSEQty.Text = "";
                    LoadSizeList(labelPo.Text);
                    break;
                }
            }
            //SetSeFinishQty(se_id);
            //SetSeUnFinishQty(se_id);
            //SetSizeQty(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        //load size list
        private void LoadSizeList(string vPO)
        {
            this.SizePanel.Controls.Clear();
            try
            {
                workDaySizeDt = GetWorkDaySize(vPO);
                for (int i = 0; i < workDaySizeDt.Rows.Count; i++)
                {
                    Button button = new Button();
                    button.Text = workDaySizeDt.Rows[i]["SIZE_NO"].ToString();
                    button.Font = new Font("微软雅黑", 30F);
                    button.Size = new Size(135, 135);
                    button.BackColor = System.Drawing.Color.Gray;
                    button.Click += new EventHandler(buttonSize_Click);
                    this.SizePanel.Controls.Add(button);
                }
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

       
        //The Size list is refreshed. If the list is empty, the corresponding PO will be removed.
        private void RefreshSize(string vPO)
        {
            LoadSizeList(vPO);
            if (SizePanel.Controls.Count == 0)
            {
                foreach (Control poControl in PoPanel1.Controls)
                {
                    RadioButton rb = (RadioButton)poControl;
                    if (rb.Checked)
                    {
                        PoPanel1.Controls.Remove(rb);
                        SetUINull();
                    }
                }
            }
        }

        //Manual data entry
        private void buttonSize_Click(object sender, EventArgs e)
        {
            btnSize = (Button)sender;
            size = btnSize.Text;
            DataRow dataRow = workDaySizeDt.Select("SIZE_NO = '" + size + "'")[0];
            string se_id = dataRow["SE_ID"].ToString();
            string prod_order = dataRow["PRODUCTION_ORDER"].ToString();
            string main_prod_order = dataRow["MAIN_PROD_ORDER"].ToString();
            string art = dataRow["ART_NO"].ToString();
            labelArt.Text = art;
            labelArtName.Text = GetArtName(art);
            labelSeId.Text = se_id;
            string po = labelPo.Text;
            string scan_ip = IPUtil.GetIpAddress();
            //Manually click, the inner box label defaults to 8888
            string label = "8888";
            //isScan = false;
            po_btn = po;
            SetSeFinishQty(se_id);
            //SetSeUnFinishQty(se_id);
            //SetSizeQty(string.Empty, string.Empty, string.Empty, string.Empty);
            SetButtonEnable();
            //string se_id = this.labelSeId.Text;
            //string art = labelArt.Text;
            try
            {
                sizeUnfinishQty = GetUnfinishQty(se_id, size);
                DataTable dt = GetOutDetail(se_id, size);
                if (updateOutFinshQty(se_id, size, prod_order, main_prod_order, scan_ip, label, "B"))
                {
                    ScanSucceed();
                    //Scan successfully PO:{0} ART:{1} SIZE:{2} data
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-10002", Program.Client, "", Program.Client.Language);
                    //labelDetail.Text = msg;
                    labelDetail.Text = string.Format(msg, po, art, size);
                    labelSize.Text = size;
                    labelSEQty.Text = GetSizeQty(se_id, size).ToString();
                    dayFinishQty += 1;
                    SetDayFinishQty();
                    seFinishQty += 1;
                    textSeFinishQty.Text = seFinishQty.ToString();
                    SetSeUnFinishQty(se_id);
                    hourQty += 1;
                    textHourQty.Text = hourQty.ToString();
                    //SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["WORK_QTY"].ToString()) + decimal.Parse(dt.Rows[0]["SUPPLEMENT_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString()) + 1).ToString());
                    SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["IN_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString()) + 1).ToString());
                    LoadSizeList(po);
                    AddScanLog(se_id, po, size, art);
                    RefreshSize(po);
                }
                else
                {
                    ScanFailed();
                    //Scan failed!
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00001", Program.Client, "", Program.Client.Language);
                    labelDetail.Text = msg;
                }
            }
            catch (Exception ex)
            {
                ScanFailed();
                //Scan failed!
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00001", Program.Client, "", Program.Client.Language);
                labelDetail.Text = msg;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        //Scan the inner box
        private void textLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string po = labelPo.Text;
                //if (string.IsNullOrEmpty(po) || !art.Equals(labelArt.Text))
                if (string.IsNullOrEmpty(po))
                {
                    //Scan failed! Please select correct PO
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00003", Program.Client, "", Program.Client.Language);
                    labelDetail.Text = msg;
                    ScanFailed();
                    CleanLabelText();
                    return;
                }
                string label = textLabel.Text;
                string se_id = workDaySizeDt.Rows[0]["SE_ID"].ToString();
                DataTable labelDt = null;
                if (string.IsNullOrEmpty(label))
                {
                    return;
                }
                isScan = true;
                SetButtonEnable();
                try
                {
                    labelDt = GetLabelDetail(label, po, se_id);
                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }

                if (labelDt.Rows.Count == 0)
                {
                    //Scan failed! The information of the box label in {0} was not found, please find the service update
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00002", Program.Client, "", Program.Client.Language);
                    //labelDetail.Text = msg;
                    labelDetail.Text = string.Format(msg, label);
                    ScanFailed();
                    CleanLabelText();
                }
                else
                {
                    size = labelDt.Rows[0]["SIZE_NO"].ToString();

                    SetSeFinishQty(se_id);

                    sizeUnfinishQty = GetUnfinishQty(se_id, size);

                    DataTable dt = GetOutDetail(se_id, size);

                    if (dt.Rows.Count <= 0)
                    {
                        //Scan failed! The SIZE of the {1} code of this PO:{0} is not input
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00004", Program.Client, "", Program.Client.Language);
                        //labelDetail.Text = msg;
                        labelDetail.Text = string.Format(msg, po, size);
                        SetSizeQty(se_id, size, 0.ToString(), 0.ToString());
                        LoadSizeList(po);
                        //RefreshSize(po);
                        ScanFailed();
                        CleanLabelText();
                    }
                    else if (dt.Rows.Count > 0 && sizeUnfinishQty <= 0)
                    {
                        //Scan failed! PO:{0}'s {1} code SIZE input quantity has been scanned
                        string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00005", Program.Client, "", Program.Client.Language);
                        labelDetail.Text = string.Format(msg, po, size);
                        //SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["WORK_QTY"].ToString()) + decimal.Parse(dt.Rows[0]["SUPPLEMENT_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString())).ToString());
                        SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["IN_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString())).ToString());
                        ScanFailed();
                        CleanLabelText();
                    }
                    else if (dt.Rows.Count > 0 && sizeUnfinishQty > 0)
                    {
                        DataRow dataRow = workDaySizeDt.Select("SIZE_NO = '" + size + "'")[0];
                        string art = dataRow["ART_NO"].ToString();
                        string prod_order = dataRow["PRODUCTION_ORDER"].ToString();
                        string main_prod_order = dataRow["MAIN_PROD_ORDER"].ToString();
                        po_btn = po;
                        string scan_ip = IPUtil.GetIpAddress();

                        labelArt.Text = art;
                        labelArtName.Text = GetArtName(art);
                        labelSeId.Text = se_id;

                        try
                        {

                            if (updateOutFinshQty(se_id, size, prod_order, main_prod_order, scan_ip, label, "A"))
                            {
                                ScanSucceed();
                                //Scan successfully! PO:{0} ART:{1} SIZE:{2} data                          
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-10002", Program.Client, "", Program.Client.Language);
                                //labelDetail.Text = msg;
                                labelDetail.Text = string.Format(msg, po, art, size);
                                labelSize.Text = size;
                                labelSEQty.Text = GetSizeQty(se_id, size).ToString();
                                dayFinishQty += 1;
                                SetDayFinishQty();
                                seFinishQty += 1;
                                textSeFinishQty.Text = seFinishQty.ToString();
                                SetSeUnFinishQty(se_id);
                                hourQty += 1;
                                textHourQty.Text = hourQty.ToString();
                                //SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["WORK_QTY"].ToString()) + decimal.Parse(dt.Rows[0]["SUPPLEMENT_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString()) + 1).ToString());
                                SetSizeQty(se_id, size, Convert.ToInt32(decimal.Parse(dt.Rows[0]["IN_QTY"].ToString())).ToString(), Convert.ToInt32(decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString()) + 1).ToString());
                                LoadSizeList(po);
                                AddScanLog(se_id, po, size, art);
                                RefreshSize(po);
                            }
                            else
                            {
                                ScanFailed();
                                //labelDetail.Text = "扫描失败";
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00001", Program.Client, "", Program.Client.Language);
                                labelDetail.Text = msg;
                                CleanLabelText();
                            }
                        }
                        catch (Exception ex)
                        {
                            ScanFailed();
                            //labelDetail.Text = "扫描失败";
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("msg-scan-00001", Program.Client, "", Program.Client.Language);
                            labelDetail.Text = msg;
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                            CleanLabelText();
                        }
                    }
                }
                CleanLabelText();
            }
        }

        //The current hourly change, reset the current hourly output to 0
        private void labelHour_TextChanged(object sender, EventArgs e)
        {
            hourQty = 0;
            textHourQty.Text = hourQty.ToString();
        }

        //Update the SIZE, input quantity and scanned quantity of the interface
        private void SetSizeQty(string se_id, string size, string inQty, string outFinishQty)
        {
            labelInQty.Text = inQty;
            labelOutQty.Text = outFinishQty;
            labelSize.Text = size;
            labelSEQty.Text = se_id == string.Empty ? "" : GetSizeQty(se_id, size).ToString();
        }

        //Clear the information on the left side of the interface (PO, ART, order number)
        private void SetUINull()
        {
            labelPo.Text = "";
            labelArt.Text = "";
            labelSeId.Text = "";
            labelArtName.Text = "";
            labelSize.Text = "";
            textSeUnFinishQty.Text = "";
            textSeFinishQty.Text = "";
            SetSizeQty(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        //Scan failed background image and color
        private void ScanFailed()
        {
            labelDetail.BackColor = Color.Red;
            btnImage.Visible = true;
            btnImage.BackgroundImage = cry;
            //labelSize.Text = "";
        }

        //Scan successfully background image and color
        private void ScanSucceed()
        {
            labelDetail.BackColor = Color.Green;
            btnImage.Visible = true;
            btnImage.BackgroundImage = smile;
        }

        //Scan details Add a new line
        private void AddScanLog(string se_id, string po, string size, string art)
        {
            DateTime insertDate = DateTime.Now;
            DataGridViewRow dr = new DataGridViewRow();
            dr.CreateCells(dataGridView1);
            dr.Cells[0].Value = se_id;
            dr.Cells[1].Value = po;
            dr.Cells[2].Value = size;
            dr.Cells[3].Value = art;
            dr.Cells[4].Value = insertDate.ToString();
            dataGridView1.Rows.Insert(0, dr);
        }

        private void ProductionOutputOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hourThread != null)
            {
                hourThread.Abort();
            }
        }

        //Control inner box label can only input 0-9
        private void textLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        //When the inner box label input box loses focus, pull the focus back
        private void textLabel_LostFocus(object sender, EventArgs e)
        {
            this.textLabel.Focus();
        }

        private void SetButtonEnable()
        {
            if (!isScan)
            {
                btnSize.BackColor = Color.Blue;
            }
            foreach (Button btn in SizePanel.Controls)
            {
                btn.Enabled = false;
            }
            textLabel.ReadOnly = true;
            timer1.Enabled = true;
        }

        public void DataChanged(object sender, DataChangeEventArgs args)
        {
            d_dept = args.name;
            labelDeptNo.Text = d_dept;
            labelDeptName.Text = args.pass;

            SetUINull();
            this.SizePanel.Controls.Clear();
            LoadDayPo();
            LoadDayFinishQty();
            SetDayFinishQty();
            LoadHourQty();
            LoadScanLog();

        }

        private void butSelectDept_Click(object sender, EventArgs e)
        {
            TrackOut_AssemblyOrder_DepSelect frm = new TrackOut_AssemblyOrder_DepSelect();
            frm.DataChange += new TrackOut_AssemblyOrder_DepSelect.DataChangeHandler(DataChanged);
            frm.ShowDialog();
        }
        private void CleanLabelText()
        {
            textLabel.Text = "";
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            LoadDayPo();
            this.SizePanel.Controls.Clear();
            SetUINull();
        }

        private void ButQuery_Click_1(object sender, EventArgs e)
        {
            Hide();
            TrackOut_AssemblyOrder_InOutDetail frm = new TrackOut_AssemblyOrder_InOutDetail(d_dept, orgId);
            frm.ShowDialog();
            System.Threading.Thread.Sleep(200);
            Show();
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Interval = 1200;
            if (!isScan)
            {
                LoadSizeList(po_btn);
                //RefreshSize(po_btn);
            }

            foreach (Button btn in SizePanel.Controls)
            {
                btn.Enabled = true;
            }
            textLabel.ReadOnly = false;
            //labelSize.Text = "";
            timer1.Stop();
        }

        private void Timer2_Tick_1(object sender, EventArgs e)
        {
            textTime.Text = DateTime.Now.ToString();
        }

        private void AssemblyOutput_Domestic_Load(object sender, EventArgs e)
        {
            LoadDept();
            LoadDayPo();
            LoadDayFinishQty();
            SetDayFinishQty();
            LoadHourQty();
            LoadScanLog();
            textLabel.LostFocus += new EventHandler(textLabel_LostFocus);
            hourThread = new Thread(new ThreadStart(Go));
            hourThread.Start();
        }
    }
}
