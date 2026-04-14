using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using CommanClassLib;
using CommanClassLib.Util;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using StitchingOutput2.Properties;
using StitchingOutput2.ViewModel;

namespace StitchingOutput2
{
    public partial class StitchingOutput2 : MaterialForm
    {
        int listSizeSelectIndex = -1;

        Bitmap smile;
        Bitmap cry;
        Button button_qty;
        IList<WorkDaySizeViewModel> workDaySizeViewModelList;
        DataTable workDayDt;

        bool isFinishQtyButton;
        decimal dayFinishQty;
        int daySizeFinishQty;
        int allsizeWorkQty;
        int unFinishQty;
        private string vPO;

        /// <summary>
        ///     Work Center (Sewing Department)
        /// </summary>
        string dept = "";

        string msg01;
        private string mainProductionOrder = ""; //Master work order number
        string batchNo = "";
        WorkHoursMaintain frmWorkHour;
        private bool isInit;

        public StitchingOutput2()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
            smile = new Bitmap(Resources.smile);
            cry = new Bitmap(Resources.cry);
            msg01 = UIHelper.UImsg("Tips", Program.client, string.Empty, Program.client.Language);
        }

        private void StitchingOutput2_Load(object sender, EventArgs e)
        {
            try
            {

                tcStichingOutput.Height = Screen.GetWorkingArea(this).Height - 30;
                panel5.Height = tpOutput.Height - tableLayoutPanel4.Height - 30; //Set the spacing between the text box and size of the receiving warehouse, the typesetting must have

                autocompleteMenu1.SetAutocompleteMenu(txtMainProductionOrderId, autocompleteMenu1);
                autocompleteMenu2.SetAutocompleteMenu(textQueryDept, autocompleteMenu2);
                autocompleteMenu3.SetAutocompleteMenu(txtReceivingWarehouse, autocompleteMenu3);

                frmWorkHour = new WorkHoursMaintain(Program.client.APIURL, Program.client.UserToken, Program.client, Program.client.Language);
                frmWorkHour.Height = tcStichingOutput.Height;
                frmWorkHour.Width = tpWorkingHours.Width;
                frmWorkHour.TopLevel = false;
                frmWorkHour.FormBorderStyle = FormBorderStyle.None;
                frmWorkHour.Dock = DockStyle.Fill;
                tpWorkingHours.Controls.Add(frmWorkHour);

                frmWorkHour.Show();
                tcStichingOutput.SelectedIndex = frmWorkHour.AfterShow();

                dept = textDept.Text; //work center code

                LoadDept();
                txtMainProductionOrderId.Focus();
                txtWarehouseCode.Text = ""; //Receiving warehouse
                txtReceivingDeptNo.Text = "";
                isInit = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
               
                throw ex;
            }
        }

        private void LoadProductionOrder()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 300 };
            workDayDt = GetStitchingOutputMainOrder();
            int n = 1;
            for (int i = 0; i < workDayDt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                        new[] { n + "", workDayDt.Rows[i]["udf01"].ToString() }, workDayDt.Rows[i]["udf01"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        /// <summary>
        ///     Get order list
        /// </summary>
        /// <returns></returns>
        private DataTable GetStitchingOutputMainOrder()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> parm = new Dictionary<string, string>();
            parm.Add("sticking_dept", textDept.Text); //work center code
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetStitchingOutputMainOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }

        /// <summary>
        ///     Load department list
        /// </summary>
        private void LoadDept()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 350 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        /// <summary>
        ///     load warehouse
        /// </summary>
        private void LoadReceivingWarehouse()
        {
            autocompleteMenu3.Items = null;
            autocompleteMenu3.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 350 };

            DataTable dt = GetWarehouses();
            int n = 1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu3.AddItem(new MulticolumnAutocompleteItem(
                        new[] { n + "", dt.Rows[i]["org_id"] + " " + dt.Rows[i]["warehouse_code"] + " " + dt.Rows[i]["warehouse_name"] }, dt.Rows[i]["org_id"] + "|" + dt.Rows[i]["warehouse_code"] + "|" + dt.Rows[i]["warehouse_name"])
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        /// <summary>
        ///     Get department list
        /// </summary>
        /// <returns></returns>
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }

        /// <summary>
        ///     Get the factory list corresponding to the work center
        /// </summary>
        /// <returns></returns>
        private string GetOrgId()
        {

            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("departmentCode", dept);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOrgIdByDepartmentCode", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                return JsonConvert.DeserializeObject<string>(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return "";
        }


        /// <summary>
        ///     Get warehouse list
        /// </summary>
        /// <returns></returns>
        private DataTable GetWarehouses()
        {
            string orgId = GetOrgId();
            DataTable dt = new DataTable();
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("orgId", orgId);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetWarehouses", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["org_id"].ToString() == orgId && dr["warehouse_code"].ToString() == "3001")
                    {
                        txtWarehouseCode.Text = dr["warehouse_code"].ToString();
                        txtReceivingDeptNo.Text = dr["warehouse_name"].ToString();//Load the default 3001 bin
                    }
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }


        /// <summary>
        ///     Number of workers dispatched today
        /// </summary>
        private void SetDayFinishQty()
        {
            textFinishQty.Text = dayFinishQty.ToString(CultureInfo.InvariantCulture); //Number of workers dispatched today
        }

        /// <summary>
        ///     Get the order for the group's task of the day
        /// </summary>
        /// <returns></returns>
        private DataTable GetSeId()
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", dept);
            p.Add("vInOut", "OUT");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetSeId_Po", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }

        /// <summary>
        ///     Get the task list of the group's SIZE for the day
        /// </summary>
        /// <param name="main_production_order">Master work order number</param>
        /// <returns></returns>
        private IList<WorkDaySizeViewModel> GetWorkDaySize(string main_production_order)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("main_production_order", main_production_order); //Master work order number
            p.Add("sticking_dept", textDept.Text); //work center code
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetWorkDaySize", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    dt = dt.Rows.Cast<DataRow>().OrderBy(r => r["print_seq"].ToDecimal()).CopyToDataTable();
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return DataConvertUtil<WorkDaySizeViewModel>.DataTableToList(dt);
        }

        /// <summary>
        ///     Get the total number of completions for the day
        /// </summary>
        /// <param name="d_dept"></param>
        /// <returns></returns>
        private int GetDayFinishQty(string d_dept)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "OUT");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.StitchingInOutServer", "GetDayFinishQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }

        /// <summary>
        ///     Get the production scheduling quantity of SIZE on the day
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="SizeNo"></param>
        /// <returns></returns>
        private int GetDaySizeWorkQtyByProductOrder(string productOrder, string SizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", productOrder);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", textDept.Text); //work center code
            p.Add("vInOut", "OUT");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDaySizeWorkQtyByProductOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }

        /// <summary>
        ///     Get all input quantities of SIZE
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="SizeNo"></param>
        /// <returns></returns>
        private int GetSizeWorkQtyByProductOrder(string productOrder, string SizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", productOrder);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", textDept.Text); //work center code
            p.Add("vInOut", "IN");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetSizeWorkQtyByProductOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }


        /// <summary>
        ///     Get SIZE's scheduled production quantity
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="sizeNo"></param>
        /// <returns></returns>
        private int GetSizeWorkQty(string productOrder, string sizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", productOrder);
            p.Add("vSizeNo", sizeNo);
            p.Add("vDDept", dept);
            p.Add("vInOut", "IN");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetSizeWorkQtyNew", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }

        /// <summary>
        ///     Get all quantities completed
        /// </summary>
        /// <param name="productionOrder"></param>
        /// <param name="size_no"></param>
        /// <returns></returns>
        private int GetFinishQty(string productionOrder, string size_no)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", dept);
            p.Add("productionOrder", productionOrder);
            p.Add("size_no", size_no);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetFinishQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }

        /// <summary>
        ///     Get the completed quantity of SIZE on the day
        /// </summary>
        /// <param name="vProductOrder"></param>
        /// <param name="SizeNo"></param>
        /// <returns></returns>
        private int GetDaySizeFinishQtyByProductOrder(string vProductOrder, string SizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", vProductOrder);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", dept);
            p.Add("vInOut", "OUT");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetDaySizeFinishQtyByProductOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return qty;
        }

        /// <summary>
        ///     Get po according to the order number
        /// </summary>
        /// <param name="seId"></param>
        /// <returns></returns>
        private string GetPoBySeid(string seId)
        {
            string po = "";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vSeId", seId);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetPoBySeid", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                po = json;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return po;
        }

        private bool UpdateOutFinshQtyByQRCodeId(string orgId, string seId, string seSeq, string SizeNo, int qty, string scan_ip, string stocNo, string vDpetNo, string vProductionOrder, string vItemNo, string vTransType, string vBatchNo, string vShelfNo, string vOperateType, string qrCodeId, string vMainProdOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", seId);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", dept);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vStocNo", stocNo);
            p.Add("vDpetNo", vDpetNo);
            p.Add("vProductionOrder", vProductionOrder);
            p.Add("vItemNo", vItemNo);
            p.Add("vTransType", vTransType);
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", vShelfNo);
            p.Add("vOperateType", vOperateType);
            p.Add("qty", qty);
            p.Add("vQRCodeId", qrCodeId);
            p.Add("vMainProdOrder", vMainProdOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "UpdateOutFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        private bool UpdateOutFinshQty(string orgId, string seId, string seSeq, string SizeNo, int qty, string scan_ip, string stocNo, string vDpetNo, string vProductionOrder, string vItemNo, string vTransType, string vBatchNo, string vShelfNo, string vOperateType, string vMainProdOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", seId);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", textDept.Text);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vStocNo", stocNo);
            p.Add("vDpetNo", vDpetNo);
            p.Add("vProductionOrder", vProductionOrder);
            p.Add("vItemNo", vItemNo);
            p.Add("vTransType", vTransType);
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", vShelfNo);
            p.Add("vOperateType", vOperateType);
            p.Add("qty", qty);
            p.Add("vMainProdOrder", vMainProdOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "UpdateOutFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        private void listSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSize.SelectedIndex > -1)
            {
                try
                {
                    listSizeSelectIndex = listSize.SelectedIndex;
                    WorkDaySizeViewModel vswdz = workDaySizeViewModelList[listSizeSelectIndex];
                    string se_id = vswdz.SE_ID;
                    string productOrder = vswdz.PRODUCTION_ORDER;
                    int se_seq = vswdz.SE_SEQ;
                    string size_no = vswdz.SIZE_NO;
                    string orgId = vswdz.ORG_ID;
                    //The number of production schedules on the day of the work order size
                    int daySizeWorkQty = GetDaySizeWorkQtyByProductOrder(productOrder, size_no); //Get the production scheduling quantity of SIZE on the day
                    //The completed quantity of the work order size
                    daySizeFinishQty = GetDaySizeFinishQtyByProductOrder(productOrder, size_no); //Get the completed quantity of SIZE on the day

                    int sizeFinishQty = GetFinishQty(productOrder, size_no); //The completed quantity of the work order size
                    int sizeWorkQty = GetSizeWorkQty(productOrder, size_no); //Work order SIZE input quantity

                    unFinishQty = daySizeWorkQty - daySizeFinishQty;
                    DisplayQtyButton(unFinishQty);
                    SetProductInfoToDefault(sizeWorkQty, sizeFinishQty);
                    txtMainProductionOrderId.Text = "";
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                SetProductInfoToDefault(0, 0);
            }
        }

        /// <summary>
        ///     Report work by clicking, correspondingly need to refresh the size list and the number of buttons and background color
        /// </summary>
        /// <param name="strqty"></param>
        /// <param name="clickButton"></param>
        //private void FinishQty(string strqty, Button clickButton)
        //{
        //    string stocNo = txtWarehouseCode.Text; //Receiving warehouse
        //    if (string.IsNullOrEmpty(stocNo))
        //    {
        //        MessageHelper.ShowErr(this, "Please enter the receiving warehouse！");
        //        return;
        //    }

        //    isFinishQtyButton = true;
        //    button_qty = clickButton;
        //    SetButtonEnable(button_qty);
        //    WorkDaySizeViewModel obj = workDaySizeViewModelList[listSizeSelectIndex];
        //    if (obj != null)
        //    {
        //        string org_id = obj.ORG_ID;
        //        string se_id = obj.SE_ID;
        //        string productionOrder = obj.PRODUCTION_ORDER;
        //        int se_seq = obj.SE_SEQ;
        //        string size_no = obj.SIZE_NO;
        //        int qty = int.Parse(strqty);
        //        string scan_ip = IPUtil.GetIpAddress();
        //        if (!string.IsNullOrWhiteSpace(productionOrder))
        //        {
        //            int sizeFinishQty = GetFinishQty(productionOrder, size_no);
        //            allsizeWorkQty = GetSizeWorkQtyByProductOrder(productionOrder, size_no);
        //            try
        //            {
        //                string vDpetNo = dept;
        //                string vProductionOrder = obj.PRODUCTION_ORDER;
        //                string vItemNo = obj.MATERIAL_NO; //Part No
        //                string vTransType = "101"; //default
        //                string vBatchNo = batchNo; //batch = sales order
        //                string vShelfNo = "ALL"; //default
        //                string vOperateType = "C"; //default

        //                int daysizeWorkQty = GetDaySizeWorkQtyByProductOrder(productionOrder, size_no);
        //                unFinishQty = daysizeWorkQty - daySizeFinishQty;
        //                if (qty > unFinishQty)
        //                {
        //                    btnImage.Visible = true;
        //                    btnImage.BackgroundImage = cry; //Failed to report to work, crying face
        //                    btnImage.BackColor = Color.Transparent;
        //                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                    DisplayQtyButton(unFinishQty);
        //                    if (unFinishQty <= 0)
        //                    {
        //                        RefreshListSize();
        //                    }

        //                    string msg02 = UIHelper.UImsg("输入的数量大于未完成的数量", Program.client, "", Program.client.Language);
        //                    MessageBox.Show(msg02, msg01);
        //                    return;
        //                }

        //                if (UpdateOutFinshQty(org_id, se_id, se_seq.ToString(), size_no, qty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, mainProductionOrder))
        //                {
        //                    if (qty == unFinishQty)
        //                    {
        //                        RefreshListSize();
        //                    }

        //                    btnImage.Visible = true;
        //                    btnImage.BackgroundImage = smile;
        //                    btnImage.BackColor = Color.Transparent;
        //                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;

        //                    unFinishQty = unFinishQty - qty;
        //                    daySizeFinishQty += qty;
        //                    sizeFinishQty += qty;
        //                    dayFinishQty += qty;
        //                    SetDayFinishQty();
        //                }
        //                else
        //                {
        //                    btnImage.Visible = true;
        //                    btnImage.BackgroundImage = cry;
        //                    btnImage.BackColor = Color.Transparent;
        //                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ScanFailed();
        //                MessageHelper.ShowErr(this, ex.Message);
        //            }

        //            textSizeQty.Text = allsizeWorkQty.ToString(); //Number of sub-work orders
        //            textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
        //        }

        //        textSize.Text = obj.SIZE_NO; //size
        //    }

        //    DisplayQtyButton(unFinishQty);
        //    textQty.Text = strqty; //quantity
        //    txtMainProductionOrderId.Text = "";
        //    textEnterQty.Text = "";
        //}

        private void FinishQty(string strqty, Button clickButton) // Modify by venkat 2024/05/11
        {
            if (judgeTime())
            {
                MessageBox.Show("Maintenace Period Cannot Input!! ");
                return;
            }
            else
            {
                string stocNo = txtWarehouseCode.Text; //Receiving warehouse
                if (string.IsNullOrEmpty(stocNo))
                {
                    MessageHelper.ShowErr(this, "Please enter the receiving warehouse！");
                    return;
                }

                isFinishQtyButton = true;
                button_qty = clickButton;
                SetButtonEnable(button_qty);
                WorkDaySizeViewModel obj = workDaySizeViewModelList[listSizeSelectIndex];
                if (obj != null)
                {
                    string org_id = obj.ORG_ID;
                    string se_id = obj.SE_ID;
                    string productionOrder = obj.PRODUCTION_ORDER;
                    int se_seq = obj.SE_SEQ;
                    string size_no = obj.SIZE_NO;
                    int qty = int.Parse(strqty);
                    string scan_ip = IPUtil.GetIpAddress();
                    if (!string.IsNullOrWhiteSpace(productionOrder))
                    {
                        int sizeFinishQty = GetFinishQty(productionOrder, size_no);
                        allsizeWorkQty = GetSizeWorkQtyByProductOrder(productionOrder, size_no);
                        try
                        {
                            string vDpetNo = dept;
                            string vProductionOrder = obj.PRODUCTION_ORDER;
                            string vItemNo = obj.MATERIAL_NO; //Part No
                            string vTransType = "101"; //default
                            string vBatchNo = batchNo; //batch = sales order
                            string vShelfNo = "ALL"; //default
                            string vOperateType = "C"; //default

                            int daysizeWorkQty = GetDaySizeWorkQtyByProductOrder(productionOrder, size_no);
                            unFinishQty = daysizeWorkQty - daySizeFinishQty;
                            if (qty > unFinishQty)
                            {
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = cry; //Failed to report to work, crying face
                                btnImage.BackColor = Color.Transparent;
                                btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                DisplayQtyButton(unFinishQty);
                                if (unFinishQty <= 0)
                                {
                                    RefreshListSize();
                                }

                                string msg02 = UIHelper.UImsg("输入的数量大于未完成的数量", Program.client, "", Program.client.Language);
                                MessageBox.Show(msg02, msg01);
                                return;
                            }

                            if (UpdateOutFinshQty(org_id, se_id, se_seq.ToString(), size_no, qty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, mainProductionOrder))
                            {
                                if (qty == unFinishQty)
                                {
                                    RefreshListSize();
                                }

                                btnImage.Visible = true;
                                btnImage.BackgroundImage = smile;
                                btnImage.BackColor = Color.Transparent;
                                btnImage.BackgroundImageLayout = ImageLayout.Stretch;

                                unFinishQty = unFinishQty - qty;
                                daySizeFinishQty += qty;
                                sizeFinishQty += qty;
                                dayFinishQty += qty;
                                SetDayFinishQty();
                            }
                            else
                            {
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = cry;
                                btnImage.BackColor = Color.Transparent;
                                btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                        }
                        catch (Exception ex)
                        {
                            ScanFailed();
                            MessageHelper.ShowErr(this, ex.Message);
                        }

                        textSizeQty.Text = allsizeWorkQty.ToString(); //Number of sub-work orders
                        textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
                    }

                    textSize.Text = obj.SIZE_NO; //size
                }

                DisplayQtyButton(unFinishQty);
                textQty.Text = strqty; //quantity
                txtMainProductionOrderId.Text = "";
                textEnterQty.Text = "";
            }


        }

        /// <summary>
        ///     Enter master work order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtQuerySeID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        listSizeSelectIndex = -1;
        //        string txtQrCode = txtMainProductionOrderId.Text.ToUpper();
        //        //B,200,A0A19040071,SM0A190415000049,485,1,9,10,48,EF9370,03996 (11 numbers before)
        //        //category, organization, order, unique code of the ticket, ticket number, order sequence, size, quantity, size serial number, art, model number
        //        //(1)B,(2)1001,(3)100005,(4)20210809100007878,(5)100012,(6)-,(7)8.5,(8)50,(9)30101T00158.5,( 10) KOJIMA, (11) 325816, (12) 100005 (12 numbers)
        //        if (!string.IsNullOrWhiteSpace(txtQrCode) && txtQrCode.Contains(","))
        //        {
        //            string[] str = txtQrCode.Split(',');
        //            int length = str.Length;
        //            if (string.IsNullOrWhiteSpace(textDept.Text)) //work center code
        //            {
        //                string msg = UIHelper.UImsg("Please enter input group！", Program.client, "", Program.client.Language);
        //                MessageHelper.ShowErr(this, msg);
        //                txtMainProductionOrderId.Text = "";
        //                return;
        //            }

        //            if (IsQrCode(length))
        //            {
        //                //string org_id = str[1];//organize
        //                if (str != null)
        //                {
        //                    string se_id = str[2]; //sales order
        //                    string productionOrder = str[4]; //Sub work order
        //                    int se_seq = 1; //Sales order serial number
        //                    string art_no = str[9];

        //                    listSize.Items.Clear();
        //                    SetSizeButtonToDefault();
        //                    string scan_ip = IPUtil.GetIpAddress();

        //                    if (!string.IsNullOrWhiteSpace(productionOrder))
        //                    {
        //                        DataTable dtMES010M = GetMainProductionOrder(productionOrder); //Master work order number

        //                        string mainProductionOrder = "";
        //                        //The factory that takes the work order directly
        //                        string orgId = "";

        //                        if (dtMES010M != null && dtMES010M.Rows.Count > 0)
        //                        {
        //                            mainProductionOrder = dtMES010M.Rows[0]["udf01"].ToString();
        //                            orgId = dtMES010M.Rows[0]["org"].ToString();
        //                        }

        //                        if (!string.IsNullOrWhiteSpace(mainProductionOrder))
        //                        {
        //                            workDaySizeViewModelList = GetWorkDaySize(mainProductionOrder);
        //                            string size_no = str[6]; //size
        //                            int qty = int.Parse(str[7]); //quantity
        //                            string qrCodeId = str[3]; //QR code ID
        //                            bool isExistQRCode = IsExistQRCode(qrCodeId);
        //                            if (isExistQRCode)
        //                            {
        //                                MessageHelper.ShowErr(this, "The same QR code exists, please check and re-enter!");
        //                                txtMainProductionOrderId.Text = "";
        //                                return;
        //                            }

        //                            if (string.IsNullOrWhiteSpace(txtWarehouseCode.Text)) //Receiving warehouse
        //                            {
        //                                MessageHelper.ShowErr(this, "Receiving warehouse cannot be empty");
        //                                return;
        //                            }

        //                            WorkDaySizeViewModel orderSize = null;
        //                            foreach (var item in workDaySizeViewModelList)
        //                            {
        //                                if (item.PRODUCTION_ORDER == productionOrder)
        //                                {
        //                                    orderSize = item;
        //                                    break;
        //                                }
        //                            }

        //                            string size_seq = "";
        //                            foreach (WorkDaySizeViewModel item in workDaySizeViewModelList)
        //                            {
        //                                if (!string.IsNullOrWhiteSpace(size_no) && item.SIZE_NO == size_no)
        //                                {
        //                                    size_seq = item.SIZE_SEQ;
        //                                    se_seq = item.SE_SEQ;
        //                                    break;
        //                                }
        //                            }

        //                            try
        //                            {
        //                                string vMainProdOrder = mainProductionOrder; //Master work order
        //                                allsizeWorkQty = GetSizeWorkQtyByProductOrder(productionOrder, size_no);
        //                                if (allsizeWorkQty <= 0)
        //                                {
        //                                    string msg02 = UIHelper.UImsg($"The sub work order number:{productionOrder}of{size_no}Code not entered or fully scanned", Program.client, "", Program.client.Language);

        //                                    MessageBox.Show(msg02, msg01);
        //                                    txtMainProductionOrderId.Text = "";
        //                                    ScanFailed();
        //                                    return;
        //                                }

        //                                textSizeQty.Text = allsizeWorkQty.ToString(); //Number of sub-work orders

        //                                int daysizeWorkQty = GetDaySizeWorkQtyByProductOrder(productionOrder, size_no);
        //                                //The completed quantity of the work order size
        //                                daySizeFinishQty = GetDaySizeFinishQtyByProductOrder(productionOrder, size_no); //Get the completed quantity of SIZE on the day
        //                                unFinishQty = daysizeWorkQty - daySizeFinishQty;
        //                                if (qty > unFinishQty)
        //                                {
        //                                    string msg02 = UIHelper.UImsg("扫描数量大于剩余未扫描数量！", Program.client, "", Program.client.Language);
        //                                    MessageBox.Show(msg02, msg01);
        //                                    txtMainProductionOrderId.Text = "";
        //                                    ScanFailed();
        //                                    return;
        //                                }

        //                                if (!string.IsNullOrWhiteSpace(se_id))
        //                                {
        //                                    vPO = GetPoBySeid(se_id);
        //                                }

        //                                textPo.Text = vPO;

        //                                string stocNo = txtWarehouseCode.Text; //Receiving warehouse
        //                                string vDpetNo = dept;
        //                                string vProductionOrder = productionOrder;
        //                                string vItemNo = "";
        //                                if (orderSize != null)
        //                                {
        //                                    vItemNo = orderSize.MATERIAL_NO; //Part No
        //                                }

        //                                string vTransType = "101"; //default
        //                                string vBatchNo = se_id; //batch = sales order
        //                                string vShelfNo = "ALL"; //default
        //                                string vOperateType = "C"; //default

        //                                if (UpdateOutFinshQtyByQRCodeId(orgId, se_id, se_seq.ToString(), size_no, qty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, qrCodeId, vMainProdOrder))
        //                                {
        //                                    int sizeFinishQty = GetFinishQty(productionOrder, size_no);
        //                                    textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
        //                                    dayFinishQty += qty;
        //                                    SetDayFinishQty();

        //                                    textSize.Text = size_no; //size
        //                                    textQty.Text = qty.ToString(); //quantity
        //                                    btnImage.Visible = true;
        //                                    btnImage.BackgroundImage = smile;
        //                                    btnImage.BackColor = Color.Transparent;
        //                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                                    daySizeFinishQty += qty;
        //                                }
        //                                else
        //                                {
        //                                    ScanFailed();
        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                ScanFailed();
        //                                MessageHelper.ShowErr(this, ex.Message);
        //                                return;
        //                            }
        //                        }
        //                    }
        //                }

        //                txtMainProductionOrderId.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrWhiteSpace(txtMainProductionOrderId.Text))
        //            {
        //                string[] numbers = txtMainProductionOrderId.Text.Trim().ToUpper().Split('|');
        //                mainProductionOrder = numbers[0];
        //            }

        //            if (!string.IsNullOrWhiteSpace(mainProductionOrder))
        //            {
        //                listSize.Items.Clear();
        //                vPO = "";
        //                workDaySizeViewModelList = GetWorkDaySize(mainProductionOrder);
        //                if (workDaySizeViewModelList != null && workDaySizeViewModelList.Count > 0)
        //                {
        //                    foreach (var o in workDaySizeViewModelList)
        //                    {
        //                        listSize.Items.Add(o.SIZE_NO);
        //                    }

        //                    foreach (var item in workDaySizeViewModelList)
        //                    {
        //                        if (mainProductionOrder == item.Udf01)
        //                        {
        //                            vPO = item.PO;
        //                            batchNo = item.SALES_ORDER; //sales order
        //                            break;
        //                        }
        //                    }

        //                    txtMainProductionOrderId.Text = "";
        //                }

        //                SetSizeButtonToDefault();
        //                textPo.Text = vPO;
        //            }

        //            SetProductInfoToDefault(0, 0);
        //        }
        //    }
        //}

        private void txtQuerySeID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) // Modify By venkat 2024/05/11
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (judgeTime())
                {
                    MessageBox.Show("Maintenace Period Cannot Input!! ");
                    return;
                }
                else
                {
                    listSizeSelectIndex = -1;
                    string txtQrCode = txtMainProductionOrderId.Text.ToUpper();
                    //B,200,A0A19040071,SM0A190415000049,485,1,9,10,48,EF9370,03996 (11 numbers before)
                    //category, organization, order, unique code of the ticket, ticket number, order sequence, size, quantity, size serial number, art, model number
                    //(1)B,(2)1001,(3)100005,(4)20210809100007878,(5)100012,(6)-,(7)8.5,(8)50,(9)30101T00158.5,( 10) KOJIMA, (11) 325816, (12) 100005 (12 numbers)
                    if (!string.IsNullOrWhiteSpace(txtQrCode) && txtQrCode.Contains(","))
                    {
                        string[] str = txtQrCode.Split(',');
                        int length = str.Length;
                        if (string.IsNullOrWhiteSpace(textDept.Text)) //work center code
                        {
                            string msg = UIHelper.UImsg("Please enter input group！", Program.client, "", Program.client.Language);
                            MessageHelper.ShowErr(this, msg);
                            txtMainProductionOrderId.Text = "";
                            return;
                        }

                        if (IsQrCode(length))
                        {
                            //string org_id = str[1];//organize
                            if (str != null)
                            {
                                string se_id = str[2]; //sales order
                                string productionOrder = str[4]; //Sub work order
                                int se_seq = 1; //Sales order serial number
                                string art_no = str[9];

                                listSize.Items.Clear();
                                SetSizeButtonToDefault();
                                string scan_ip = IPUtil.GetIpAddress();

                                if (!string.IsNullOrWhiteSpace(productionOrder))
                                {
                                    DataTable dtMES010M = GetMainProductionOrder(productionOrder); //Master work order number

                                    string mainProductionOrder = "";
                                    //The factory that takes the work order directly
                                    string orgId = "";

                                    if (dtMES010M != null && dtMES010M.Rows.Count > 0)
                                    {
                                        mainProductionOrder = dtMES010M.Rows[0]["udf01"].ToString();
                                        orgId = dtMES010M.Rows[0]["org"].ToString();
                                    }

                                    if (!string.IsNullOrWhiteSpace(mainProductionOrder))
                                    {
                                        workDaySizeViewModelList = GetWorkDaySize(mainProductionOrder);
                                        string size_no = str[6]; //size
                                        int qty = int.Parse(str[7]); //quantity
                                        string qrCodeId = str[3]; //QR code ID
                                        bool isExistQRCode = IsExistQRCode(qrCodeId);
                                        if (isExistQRCode)
                                        {
                                            MessageHelper.ShowErr(this, "The same QR code exists, please check and re-enter!");
                                            txtMainProductionOrderId.Text = "";
                                            return;
                                        }

                                        if (string.IsNullOrWhiteSpace(txtWarehouseCode.Text)) //Receiving warehouse
                                        {
                                            MessageHelper.ShowErr(this, "Receiving warehouse cannot be empty");
                                            return;
                                        }

                                        WorkDaySizeViewModel orderSize = null;
                                        foreach (var item in workDaySizeViewModelList)
                                        {
                                            if (item.PRODUCTION_ORDER == productionOrder)
                                            {
                                                orderSize = item;
                                                break;
                                            }
                                        }

                                        string size_seq = "";
                                        foreach (WorkDaySizeViewModel item in workDaySizeViewModelList)
                                        {
                                            if (!string.IsNullOrWhiteSpace(size_no) && item.SIZE_NO == size_no)
                                            {
                                                size_seq = item.SIZE_SEQ;
                                                se_seq = item.SE_SEQ;
                                                break;
                                            }
                                        }

                                        try
                                        {
                                            string vMainProdOrder = mainProductionOrder; //Master work order
                                            allsizeWorkQty = GetSizeWorkQtyByProductOrder(productionOrder, size_no);
                                            if (allsizeWorkQty <= 0)
                                            {
                                                string msg02 = UIHelper.UImsg($"The sub work order number:{productionOrder}of{size_no}Code not entered or fully scanned", Program.client, "", Program.client.Language);

                                                MessageBox.Show(msg02, msg01);
                                                txtMainProductionOrderId.Text = "";
                                                ScanFailed();
                                                return;
                                            }

                                            textSizeQty.Text = allsizeWorkQty.ToString(); //Number of sub-work orders

                                            int daysizeWorkQty = GetDaySizeWorkQtyByProductOrder(productionOrder, size_no);
                                            //The completed quantity of the work order size
                                            daySizeFinishQty = GetDaySizeFinishQtyByProductOrder(productionOrder, size_no); //Get the completed quantity of SIZE on the day
                                            unFinishQty = daysizeWorkQty - daySizeFinishQty;
                                            if (qty > unFinishQty)
                                            {
                                                string msg02 = UIHelper.UImsg("扫描数量大于剩余未扫描数量！", Program.client, "", Program.client.Language);
                                                MessageBox.Show(msg02, msg01);
                                                txtMainProductionOrderId.Text = "";
                                                ScanFailed();
                                                return;
                                            }

                                            if (!string.IsNullOrWhiteSpace(se_id))
                                            {
                                                vPO = GetPoBySeid(se_id);
                                            }

                                            textPo.Text = vPO;

                                            string stocNo = txtWarehouseCode.Text; //Receiving warehouse
                                            string vDpetNo = dept;
                                            string vProductionOrder = productionOrder;
                                            string vItemNo = "";
                                            if (orderSize != null)
                                            {
                                                vItemNo = orderSize.MATERIAL_NO; //Part No
                                            }

                                            string vTransType = "101"; //default
                                            string vBatchNo = se_id; //batch = sales order
                                            string vShelfNo = "ALL"; //default
                                            string vOperateType = "C"; //default

                                            if (UpdateOutFinshQtyByQRCodeId(orgId, se_id, se_seq.ToString(), size_no, qty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, qrCodeId, vMainProdOrder))
                                            {
                                                int sizeFinishQty = GetFinishQty(productionOrder, size_no);
                                                textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
                                                dayFinishQty += qty;
                                                SetDayFinishQty();

                                                textSize.Text = size_no; //size
                                                textQty.Text = qty.ToString(); //quantity
                                                btnImage.Visible = true;
                                                btnImage.BackgroundImage = smile;
                                                btnImage.BackColor = Color.Transparent;
                                                btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                                daySizeFinishQty += qty;
                                            }
                                            else
                                            {
                                                ScanFailed();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ScanFailed();
                                            MessageHelper.ShowErr(this, ex.Message);
                                            return;
                                        }
                                    }
                                }
                            }

                            txtMainProductionOrderId.Text = "";
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(txtMainProductionOrderId.Text))
                        {
                            string[] numbers = txtMainProductionOrderId.Text.Trim().ToUpper().Split('|');
                            mainProductionOrder = numbers[0];
                        }

                        if (!string.IsNullOrWhiteSpace(mainProductionOrder))
                        {
                            listSize.Items.Clear();
                            vPO = "";
                            workDaySizeViewModelList = GetWorkDaySize(mainProductionOrder);
                            if (workDaySizeViewModelList != null && workDaySizeViewModelList.Count > 0)
                            {
                                foreach (var o in workDaySizeViewModelList)
                                {
                                    listSize.Items.Add(o.SIZE_NO);
                                }

                                foreach (var item in workDaySizeViewModelList)
                                {
                                    if (mainProductionOrder == item.Udf01)
                                    {
                                        vPO = item.PO;
                                        batchNo = item.SALES_ORDER; //sales order
                                        break;
                                    }
                                }

                                txtMainProductionOrderId.Text = "";
                            }

                            SetSizeButtonToDefault();
                            textPo.Text = vPO;
                        }

                        SetProductInfoToDefault(0, 0);
                    }
                }


            }
        }

        /// <summary>
        ///     Is there a QR code
        /// </summary>
        /// <param name="qrcodeId"></param>
        /// <returns></returns>
        private bool IsExistQRCode(string qrcodeId)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("qrcodeId", qrcodeId);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "TrackOutIsExistQRCode", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                bool isExisted = JsonConvert.DeserializeObject<bool>(json);
                return isExisted;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                return false;
            }
        }

        private DataTable GetMainProductionOrder(string productionOrder)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("production_order", productionOrder); //Sub work order number
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetMainProductionOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                    return dt;
                }
                else
                {
                    MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                }
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }

            return null;
        }

        /// <summary>
        ///     Clear the button corresponding to the selected size
        /// </summary>
        /// <param name="uFinishQty">unfinished quantity</param>
        private void DisplayQtyButton(decimal uFinishQty)
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (uFinishQty >= decimal.Parse(button.Text))
                {
                    button.Visible = true;
                }
                else
                {
                    button.Visible = false;
                }
            }
        }

        /// <summary>
        /// Set the information in the right column, size input quantity, work order report quantity
        /// </summary>
        /// <param name="daySizeWorkQty">size input quantity</param>
        /// <param name="sizeFinishQty">Number of work order reports</param>
        private void SetProductInfoToDefault(int daySizeWorkQty, int sizeFinishQty)
        {
            if (listSizeSelectIndex == -1)
            {
                textSizeQty.Text = 0.ToString(); //Number of sub-work orders
                textSizeFinishQty.Text = 0.ToString(); //Number of work orders dispatched
            }
            else
            {
                textSizeQty.Text = daySizeWorkQty.ToString(); //Number of sub-work orders
                textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
            }

            textSize.Text = ""; //size
            textQty.Text = ""; //quantity
        }

        /// <summary>
        ///     Refresh the size list to remove the completed size
        /// </summary>
        private void RefreshListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            workDaySizeViewModelList.RemoveAt(listSizeSelectIndex);
            listSizeSelectIndex = -1;
            listSize.SelectedIndex = listSizeSelectIndex;
        }

        //Settings button is not visible
        private void SetSizeButtonToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                button.Visible = false;
            }
        }

        private void btnRefreshMesWorkDay_Click(object sender, EventArgs e)
        {
            LoadProductionOrder();
            listSize.Items.Clear();
            SetSizeButtonToDefault();
        }

        /// <summary>
        ///     Judging the correct QR code by the length of the QR code
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private bool IsQrCode(int length)
        {
            if (length == 12)
            {
                return true;
            }
            else
            {
                string msg02 = UIHelper.UImsg("二维码长度有误，请联系系统管理员！", Program.client, "", Program.client.Language);
                MessageBox.Show(msg02, msg01);
                return false;
            }
        }

        private void ScanFailed()
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = cry;
            btnImage.BackColor = Color.Transparent;
            btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void SetButtonEnable(Button clickButton)
        {
            if (!isFinishQtyButton)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = false;
                    button.BackColor = Color.Gray;
                }
            }
            else
            {
                clickButton.BackColor = Color.Gray;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = false;
                }
            }

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (!isFinishQtyButton)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    button.BackColor = Color.CornflowerBlue;
                }
            }
            else
            {
                button_qty.BackColor = Color.CornflowerBlue;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }

            timer1.Stop();
        }

        /// <summary>
        ///     ol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_c1_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c2_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c3_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c4_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c5_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c6_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c7_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c8_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void btn_c9_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        //private void butConfirm_Click(object sender, EventArgs e)
        //{
        //    if (listSizeSelectIndex < 0)
        //    {
        //        string msg02 = UIHelper.UImsg("SIZE", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(textEnterQty.Text))
        //    {
        //        string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    int enterQty = int.Parse(textEnterQty.Text);
        //    if (enterQty <= 0)
        //    {
        //        string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    if (enterQty > unFinishQty)
        //    {
        //        string msg02 = UIHelper.UImsg("输入的数量大于可转移的数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(txtWarehouseCode.Text))
        //    {
        //        MessageHelper.ShowErr(this, "Please enter the receiving warehouse!");
        //        return;
        //    }

        //    if (workDaySizeViewModelList != null)
        //    {
        //        WorkDaySizeViewModel obj = workDaySizeViewModelList[listSizeSelectIndex];
        //        if (obj != null)
        //        {
        //            string productOrder = obj.PRODUCTION_ORDER;
        //            string org_id = obj.ORG_ID;
        //            string se_id = obj.SE_ID;
        //            string se_seq = obj.SE_SEQ.ToString();
        //            string size_no = obj.SIZE_NO;
        //            string size_seq = obj.SIZE_SEQ;
        //            string scan_ip = IPUtil.GetIpAddress();
        //            string stocNo = txtWarehouseCode.Text; //Receiving warehouse
        //            string vDpetNo = dept;
        //            string vProductionOrder = obj.PRODUCTION_ORDER; //Sub work order number
        //            string vItemNo = obj.MATERIAL_NO; //Part No
        //            string vTransType = "101"; //default
        //            string vBatchNo = batchNo; //batch = sales order
        //            string vShelfNo = "ALL"; //default
        //            string vOperateType = "C"; //default

        //            string msg12 = UIHelper.UImsg("确认提交数据？", Program.client, "", Program.client.Language);
        //            DialogResult dr = MessageBox.Show(msg12, msg01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (dr == DialogResult.Yes)
        //            {
        //                isFinishQtyButton = false;
        //                SetButtonEnable((Button)sender);
        //                try
        //                {
        //                    if (UpdateOutFinshQty(org_id, se_id, se_seq, size_no, enterQty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, mainProductionOrder))
        //                    {
        //                        if (enterQty == unFinishQty)
        //                        {
        //                            RefreshListSize();
        //                        }

        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = smile; //Reported to work successfully
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;

        //                        unFinishQty = unFinishQty - enterQty;
        //                        daySizeFinishQty += enterQty;
        //                        dayFinishQty += enterQty;
        //                    }
        //                    else
        //                    {
        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = cry; //Failed to report to work
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    btnImage.Visible = true;
        //                    btnImage.BackgroundImage = cry;
        //                    btnImage.BackColor = Color.Transparent;
        //                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                    MessageHelper.ShowErr(this, ex.Message);
        //                }

        //                DisplayQtyButton(unFinishQty);
        //                SetDayFinishQty();

        //                int sizeFinishQty = GetFinishQty(productOrder, size_no);
        //                textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
        //                textSize.Text = obj.SIZE_NO; //size
        //                textQty.Text = enterQty.ToString(); //quantity
        //                txtMainProductionOrderId.Text = "";
        //                textEnterQty.Text = "";
        //            }
        //        }
        //    }
        //}

        private void butConfirm_Click(object sender, EventArgs e) // Modify by Venkat 2024/05/11
        {
            if (judgeTime())
            {
                MessageBox.Show("Maintenace Period Cannot Input!! ");
                return;
            }
            else
            {
                if (listSizeSelectIndex < 0)
                {
                    string msg02 = UIHelper.UImsg("SIZE", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textEnterQty.Text))
                {
                    string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                int enterQty = int.Parse(textEnterQty.Text);
                if (enterQty <= 0)
                {
                    string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                if (enterQty > unFinishQty)
                {
                    string msg02 = UIHelper.UImsg("输入的数量大于可转移的数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtWarehouseCode.Text))
                {
                    MessageHelper.ShowErr(this, "Please enter the receiving warehouse!");
                    return;
                }

                if (workDaySizeViewModelList != null)
                {
                    WorkDaySizeViewModel obj = workDaySizeViewModelList[listSizeSelectIndex];
                    if (obj != null)
                    {
                        string productOrder = obj.PRODUCTION_ORDER;
                        string org_id = obj.ORG_ID;
                        string se_id = obj.SE_ID;
                        string se_seq = obj.SE_SEQ.ToString();
                        string size_no = obj.SIZE_NO;
                        string size_seq = obj.SIZE_SEQ;
                        string scan_ip = IPUtil.GetIpAddress();
                        string stocNo = txtWarehouseCode.Text; //Receiving warehouse
                        string vDpetNo = dept;
                        string vProductionOrder = obj.PRODUCTION_ORDER; //Sub work order number
                        string vItemNo = obj.MATERIAL_NO; //Part No
                        string vTransType = "101"; //default
                        string vBatchNo = batchNo; //batch = sales order
                        string vShelfNo = "ALL"; //default
                        string vOperateType = "C"; //default

                        string msg12 = UIHelper.UImsg("确认提交数据？", Program.client, "", Program.client.Language);
                        DialogResult dr = MessageBox.Show(msg12, msg01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            isFinishQtyButton = false;
                            SetButtonEnable((Button)sender);
                            try
                            {
                                if (UpdateOutFinshQty(org_id, se_id, se_seq, size_no, enterQty, scan_ip, stocNo, vDpetNo, vProductionOrder, vItemNo, vTransType, vBatchNo, vShelfNo, vOperateType, mainProductionOrder))
                                {
                                    if (enterQty == unFinishQty)
                                    {
                                        RefreshListSize();
                                    }

                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = smile; //Reported to work successfully
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;

                                    unFinishQty = unFinishQty - enterQty;
                                    daySizeFinishQty += enterQty;
                                    dayFinishQty += enterQty;
                                }
                                else
                                {
                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = cry; //Failed to report to work
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                }
                            }
                            catch (Exception ex)
                            {
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = cry;
                                btnImage.BackColor = Color.Transparent;
                                btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                MessageHelper.ShowErr(this, ex.Message);
                            }

                            DisplayQtyButton(unFinishQty);
                            SetDayFinishQty();

                            int sizeFinishQty = GetFinishQty(productOrder, size_no);
                            textSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
                            textSize.Text = obj.SIZE_NO; //size
                            textQty.Text = enterQty.ToString(); //quantity
                            txtMainProductionOrderId.Text = "";
                            textEnterQty.Text = "";
                        }
                    }
                }
            }

        }

        private void textQueryDept_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!textQueryDept.Text.Contains('|'))
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                dept = textQueryDept.Text.Trim().ToUpper().Split('|')[0];
                try
                {
                    if (ValisStitchingDept(dept, "S"))
                    {
                        textDept.Text = dept; //work center code
                        textDepartName.Text = textQueryDept.Text.Trim().ToUpper().Split('|')[1]; //work center name
                        frmWorkHour.AfterShow(dept, textDepartName.Text); //work center name
                        listSize.Items.Clear();
                        textSizeQty.Text = ""; //Number of sub-work orders
                        textSizeFinishQty.Text = ""; //Number of work orders dispatched
                        textPo.Text = ""; //PO number
                        textSize.Text = ""; //size
                        textQty.Text = ""; //quantity
                        dayFinishQty = GetDayFinishQty(textDept.Text); //Work center code, load the number of dispatched workers on the day
                        SetDayFinishQty();
                        textQueryDept.Text = "";
                        LoadProductionOrder();
                    }
                    else
                    {
                        string msg = UIHelper.UImsg("请输入正确的针车组别！", Program.client, "", Program.client.Language);
                        MessageHelper.ShowErr(this, msg);
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }

                LoadReceivingWarehouse();

            }
        }


        /// <summary>
        ///     Verify that the process of the group is sewing (S)
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="routNo"></param>
        /// <returns></returns>
        private bool ValisStitchingDept(string dept, string routNo)
        {
            bool isStitchingDept = false;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", dept);
            p.Add("vRoutNo", routNo);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.StitchingInOutServer", "ValisStitchingDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                isStitchingDept = Convert.ToBoolean(json);
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isStitchingDept;
        }

        private void btnWarehouseQuery_Click(object sender, EventArgs e)
        {
            if (isInit)
            {
                StitchingOutput2_SelectIn form = new StitchingOutput2_SelectIn();
                form.deptNo = textDept.Text; //work center code
                form.Show();
            }
        }

        private void txtReceivingWarehouse_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Triggered when the OK button is pressed
            if (e.KeyChar == '\r')
            {
                if (!string.IsNullOrWhiteSpace(txtReceivingWarehouse.Text))
                {
                    string[] receivingWarehouse = txtReceivingWarehouse.Text.Split('|');
                    if (receivingWarehouse != null && receivingWarehouse.Length >= 2)
                    {
                        txtWarehouseCode.Text = receivingWarehouse[1]; //Receiving warehouse
                        txtReceivingDeptNo.Text = receivingWarehouse[2];
                    }
                }

                txtReceivingWarehouse.Text = "";
            }
        }


        //Add by Venkat 2024.05.11
        private bool judgeTime()
        {
            string sql = string.Empty;
            sql = @"SELECT getdate() as Cur_Date,START_TIME_HHHMM as st,END_TIME_HHMM as et from TIME_CONFIG where TYPE = 't_daily_prod_rolling'";
            DataTable dt_check = GDSJ_Framework.Common.WebServiceHelper.GetDataTable(Program.client.WebServiceUrl, sql, new Dictionary<string, string>());

            string[] st = dt_check.Rows[0]["st"].ToString().Split(':');
            string[] et = dt_check.Rows[0]["et"].ToString().Split(':');
            TimeSpan start = new TimeSpan(Convert.ToInt16(st[0]), Convert.ToInt16(st[1]), 0);
            TimeSpan end = new TimeSpan(Convert.ToInt16(et[0]), Convert.ToInt16(et[1]), 0);
            //TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan now = Convert.ToDateTime(dt_check.Rows[0]["Cur_Date"]).TimeOfDay;

            if ((now > start) && (now < end))
            {
                return true;
            }
            return false;
        }


    }
}