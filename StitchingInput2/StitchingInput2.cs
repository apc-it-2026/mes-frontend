using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
using StitchingInput2.Properties;
using StitchingInput2.ViewModel;
using static System.Math;

namespace StitchingInput2
{
    public partial class StitchingInput2 : MaterialForm
    {
        int listSizeSelectIndex = -1;

        /// <summary>
        ///     unfinished quantity
        /// </summary>
        int unFinishQty;

        /// <summary>
        ///     Number of completions per day
        /// </summary>
        int dayFinishQty;

        Bitmap smile;
        Bitmap cry;
        Button button_qty;

        /// <summary>
        ///     Whether to report work by clicking
        /// </summary>
        bool isFinishQtyButton;

        string msg01;
        WorkHoursMaintain frmWorkHour;
        IList<VwAssemblyByOrderSize> workDaySizeList;
        private string vPO;
        IDictionary<string, int> sizeReturnDic = new Dictionary<string, int>();

        /// <summary>
        ///     Sub work order number
        /// </summary>
        string productionOrder = "";

        /// <summary>
        ///     Master work order number
        /// </summary>
        string mainProductionOrder = "";

        /// <summary>
        ///     department
        /// </summary>
        string d_dept = "";

        public StitchingInput2()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
            smile = new Bitmap(Resources.smile);
            cry = new Bitmap(Resources.cry);
        }

        private void StitchingInputForm2_Load(object sender, EventArgs e)
        {
            tabControl1.Height = Screen.GetWorkingArea(this).Height - 30;
            autocompleteMenu1.SetAutocompleteMenu(txtMainProductionOrderId, autocompleteMenu1);
            autocompleteMenu2.SetAutocompleteMenu(txtDept, autocompleteMenu2);

            frmWorkHour = new WorkHoursMaintain(Program.client.APIURL, Program.client.UserToken, Program.client, Program.client.Language);
            frmWorkHour.Height = tabControl1.Height;
            frmWorkHour.Width = tabPage2.Width;
            frmWorkHour.TopLevel = false;
            frmWorkHour.FormBorderStyle = FormBorderStyle.None;
            frmWorkHour.Dock = DockStyle.Fill;
            tabPage2.Controls.Add(frmWorkHour);

            frmWorkHour.Show();
            tabControl1.SelectedIndex = frmWorkHour.AfterShow();

            txtCuttingDept.Text = frmWorkHour.d_dept; //Cutting unit code
            txtCuttingDeptName.Text = frmWorkHour.d_deptName; //Crop unit name
            LoadDept();

            msg01 = UIHelper.UImsg("Tips", Program.client, string.Empty, Program.client.Language);
            //labelMainProductionOrder.Text = "master production work order";
        }

        /// <summary>
        ///     Load list of master ticket numbers
        /// </summary>
        private void LoadMainProductionOrder()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 250 };
            DataTable dt = GetMainOrder();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                        new[] { n + "", dt.Rows[i]["udf01"].ToString() }, dt.Rows[i]["udf01"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        //Load department list
        private void LoadDept()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 250 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }


        /// <summary>
        ///     Get order list
        /// </summary>
        /// <returns></returns>
        private DataTable GetMainOrder()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> parm = new Dictionary<string, string>();
            parm.Add("cutting_dept", txtCuttingDept.Text); //Cutting department code
            parm.Add("sticking_dept", textDept.Text); //Work Center (Code name of sewing machine department)
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetMainOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                int count = dt.Rows.Count;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }

        /// <summary>
        ///     Get order list based on sub-ticket number
        /// </summary>
        /// <returns></returns>
        private DataTable GetMainOrderByProductOrder(string productOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> parm = new Dictionary<string, string>();
            parm.Add("cutting_dept", txtCuttingDept.Text); //Cutting department code
            parm.Add("sticking_dept", textDept.Text); //Work Center (Code name of sewing machine department)
            parm.Add("productOrder", productOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetMainOrder", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                int count = dt.Rows.Count;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return dt;
        }


        /// <summary>
        ///     Get the total number of reports for the input group
        /// </summary>
        /// <param name="d_dept"></param>
        /// <returns></returns>
        private int GetDayFinishQty(string d_dept)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetDayFinishQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        ///     Get department list
        /// </summary>
        /// <returns></returns>
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
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

        private IList<VwAssemblyByOrderSize> GetWorkDaySize(string main_production_order)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("main_production_order", main_production_order); //Master work order number
            p.Add("cutting_dept", txtCuttingDept.Text); //Cutting department code
            p.Add("sticking_dept", textDept.Text); //Work Center (Code name of sewing machine department)
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetWorkDaySize", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count > 0)
                {
                    dt = dt.Rows.Cast<DataRow>().OrderBy(r => r["print_seq"].ToDecimal()).CopyToDataTable();//Sort by size
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return DataConvertUtil<VwAssemblyByOrderSize>.DataTableToList(dt);
        }

        /// <summary>
        ///     Is there a QR code
        /// </summary>
        /// <param name="qrcodeId"></param>
        /// <returns></returns>
        private bool IsExistQRCode(string qrcodeId, string se_id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("qrcodeId", qrcodeId);
            p.Add("sales_order", se_id);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "TrackInIsExistQRCode", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private DataTable GetSeSizeDetail(string mainProductionOrder, string se_seq, string size_no)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vSeSeq", se_seq);
            p.Add("vSizeNo", size_no);
            p.Add("mainProductionOrder", mainProductionOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetSeSizeDetail", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        ///     Verify whether the process of the group is sewing (S stands for sewing)
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
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "ValisStitchingDept", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private bool UpdateInFinshQtyByQRCodeId(string orgId, string seId, string seSeq, string SizeNo, string size_seq, int qty, string scan_ip, string po, string artNo, string seDay, string productionOrder, string qrCodeId, string mainProdOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", seId);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", textDept.Text); //Work center (code of sewing department)
            p.Add("vSizeSeq", size_seq);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vPO", po);
            p.Add("vArtNo", artNo);
            p.Add("vSeDay", seDay);
            p.Add("vCuttingDept", txtCuttingDept.Text); //Cutting department code
            p.Add("vProductionOrder", productionOrder);
            p.Add("vMainProdOrder", mainProdOrder);
            p.Add("vQRCodeId", qrCodeId);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "UpdateInFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        private bool UpdateInFinshQty(string orgId, string seId, string seSeq, string SizeNo, string size_seq, int qty, string scan_ip, string po, string artNo, string seDay, string productionOrder, string mainProdOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", seId);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", textDept.Text); //Work center (code of sewing department)d
            p.Add("vSizeSeq", size_seq);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vPO", po);
            p.Add("vArtNo", artNo);
            p.Add("vSeDay", seDay);
            p.Add("vCuttingDept", txtCuttingDept.Text); //Cutting department code
            p.Add("vProductionOrder", productionOrder);
            p.Add("vMainProdOrder", mainProdOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "UpdateInFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        private bool InsertReturnSize(decimal returnsizeqty, string se_id, string po, string d_dept, string size_no, string orgId)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("returnsizeqty", returnsizeqty);
            p.Add("se_id", se_id);
            p.Add("po", po);
            p.Add("d_dept", d_dept);
            p.Add("size_no", size_no);
            p.Add("orgId", orgId);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "InsertReturnSize", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        /// <summary>
        ///     Get the total number of completions of the size of the specified order
        /// </summary>
        /// <param name="org_id"></param>
        /// <param name="se_id"></param>
        /// <param name="se_seq"></param>
        /// <param name="size_no"></param>
        /// <param name="production_order"></param>
        /// <returns></returns>
        private int GeSizeFinishQty(string org_id, string se_id, string se_seq, string size_no, string production_order)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", org_id);
            p.Add("vSeId", se_id);
            p.Add("vSeSeq", se_seq);
            p.Add("vSizeNo", size_no);
            p.Add("production_order", production_order);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GeSizeFinishQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        ///     Select the size corresponding to the sub-work order number, and then update the number of report buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSize.SelectedIndex > -1)
            {
                listSizeSelectIndex = listSize.SelectedIndex;
                VwAssemblyByOrderSize model = workDaySizeList[listSizeSelectIndex];
                string se_id = model.SE_ID;
                string org_id = model.ORG_ID;
                string se_seq = model.SE_SEQ;
                string size_no = model.SIZE_NO;

                productionOrder = model.PRODUCTION_ORDER;
                if (productionOrder != null)
                {
                    DataTable dtMES010M = GetMainProductionOrder(productionOrder); //Obtained by sub-ticket number

                    int qty = 0;

                    //labelInputWorkReport is the title box of the number of input reports in the interface
                    if (labelInputWorkReport.BackColor == Color.LightCoral)
                    {
                        if (dtMES010M != null && !string.IsNullOrEmpty(dtMES010M.Rows[0]["QTY"].ToString()))
                        {
                            qty = (int)decimal.Parse(dtMES010M.Rows[0]["QTY"].ToString());
                        }
                    }
                    else
                    {
                        qty = Convert.ToInt32(workDaySizeList[listSizeSelectIndex].QTY);
                    }

                    int sizeFinishQty = GeSizeFinishQty(org_id, se_id, se_seq, size_no, productionOrder);

                    unFinishQty = qty - sizeFinishQty; //Unfinished Quantity = Work Order Quantity - Completed Quantity of Specified Order Size
                    // The number of rollbacks for the ticket size
                    if (sizeReturnDic.Count > 0 && labelInputWorkReport.BackColor == Color.LightCoral)
                    {
                        txtSizeReturnQty.Text = sizeReturnDic[listSize.Items[listSizeSelectIndex].ToString()].ToString();
                    }
                    else
                    {
                        txtSizeReturnQty.Text = "0"; //The number of rollbacks for the ticket size
                    }

                    if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                    {
                        DisplayQtyButton(unFinishQty);
                    }
                    else if (labelInputWorkReport.BackColor == Color.LightCoral)
                    {
                        if (int.TryParse(txtSizeReturnQty.Text, out int daySizeReturnQty))
                        {
                            DisplayQtyButton(daySizeReturnQty);
                        }
                    }

                    SetProductInfoToDefault(qty, sizeFinishQty);
                }

                txtMainProductionOrderId.Text = "";
                txtDept.Text = "";
            }
            else
            {
                SetProductInfoToDefault(0, 0);
            }
        }

        /// <summary>
        ///     Clear the button corresponding to the selected size
        /// </summary>
        /// <param name="uFinishQty">unfinished quantity</param>
        private void DisplayQtyButton(int uFinishQty)
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (uFinishQty >= Abs(int.Parse(button.Text)))
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
        ///     completed
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

        /// <summary>
        ///     Report work by clicking, correspondingly need to refresh the size list and the number of buttons and background color
        /// </summary>
        /// <param name="strqty"></param>
        /// <param name="clickButton"></param>
        //private void FinishQty(string strqty, Button clickButton)
        //{

        //    isFinishQtyButton = true;
        //    if (!string.IsNullOrWhiteSpace(d_dept))
        //    {
        //        button_qty = clickButton;
        //        SetButtonEnable(button_qty);
        //        VwAssemblyByOrderSize obj = workDaySizeList[listSizeSelectIndex];
        //        int numberQty = 0;
        //        if (!string.IsNullOrWhiteSpace(strqty))
        //        {
        //            numberQty = int.Parse(strqty);
        //        }

        //        if (numberQty > unFinishQty)
        //        {
        //            string msg02 = UIHelper.UImsg("The entered quantity is greater than the outstanding quantity", Program.client, "", Program.client.Language);
        //            MessageBox.Show(msg02, msg01);
        //            return;
        //        }

        //        if (obj != null)
        //        {
        //            string org_id = obj.ORG_ID;
        //            string se_id = obj.SE_ID;
        //            string se_seq = obj.SE_SEQ;

        //            string size_no = "";
        //            if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //            {
        //                size_no = obj.SIZE_NO;
        //            }
        //            else
        //            {
        //                size_no = listSize.Items[listSize.SelectedIndex].ToString();
        //            }


        //            string scan_ip = IPUtil.GetIpAddress();
        //            string size_seq = obj.SIZE_SEQ;
        //            string art_no = obj.ART_NO;
        //            string se_day = "";
        //            if (!string.IsNullOrWhiteSpace(obj.SE_DAY))
        //            {
        //                se_day = obj.SE_DAY.Substring(0, 10);
        //            }

        //            string productionOrder = obj.PRODUCTION_ORDER;
        //            try
        //            {
        //                if (numberQty < 0)
        //                {
        //                    if (!InsertReturnSize(Abs(numberQty), se_id, vPO, d_dept, size_no, org_id))
        //                    {
        //                        MessageBox.Show("Error in rollback record！");
        //                    }
        //                }

        //                if (UpdateInFinshQty(org_id, se_id, se_seq, size_no, size_seq, numberQty, scan_ip, vPO, art_no, se_day, productionOrder, mainProductionOrder))
        //                {
        //                    if (numberQty == unFinishQty)
        //                    {
        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = smile;
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                        ReflashListSize();
        //                    }
        //                    else if (numberQty > unFinishQty && labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //                    {
        //                        SetScanFailedStatus();
        //                        MessageBox.Show("wrong");
        //                    }
        //                    else
        //                    {
        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = smile;
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                    }

        //                    unFinishQty = unFinishQty - numberQty;
        //                    dayFinishQty += numberQty;
        //                }
        //                else
        //                {
        //                    SetScanFailedStatus();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                SetScanFailedStatus();
        //                MessageHelper.ShowErr(this, ex.Message);
        //            }

        //            int sizeFinishQty = GeSizeFinishQty(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
        //            txtSizeFinishQty.Text = sizeFinishQty.ToString();

        //            if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //            {
        //                DisplayQtyButton(unFinishQty);
        //            }
        //            else if (labelInputWorkReport.BackColor == Color.LightCoral)
        //            {
        //                SetSizeButtonBackColorToDefault();

        //                workDaySizeList = GetWorkDaySize(mainProductionOrder);

        //                if (workDaySizeList.Count > 0)
        //                {
        //                    listSize.Items.Clear();
        //                    foreach (var o in workDaySizeList)
        //                    {
        //                        listSize.Items.Add(o.SIZE_NO);
        //                    }
        //                }

        //                DisplayQtyButton(0);
        //            }

        //            txtSize.Text = obj.SIZE_NO;
        //        }

        //        txtPo.Text = vPO; //PO number
        //        txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
        //        txtQty.Text = strqty; //quantity
        //        txtMainProductionOrderId.Text = "";
        //        txtDept.Text = "";
        //    }
        //    else
        //    {
        //        string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
        //        MessageHelper.ShowErr(this, msg);
        //    }
        //}


        private void FinishQty(string strqty, Button clickButton) // Modify by Venkat 2024/05/11 add rolling time control logic
        {
            if (judgeTime())
            {
                MessageBox.Show("Maintenace Period Cannot Input!! ");
                return;
            }
            else
            {
                isFinishQtyButton = true;
                if (!string.IsNullOrWhiteSpace(d_dept))
                {
                    button_qty = clickButton;
                    SetButtonEnable(button_qty);
                    VwAssemblyByOrderSize obj = workDaySizeList[listSizeSelectIndex];
                    int numberQty = 0;
                    if (!string.IsNullOrWhiteSpace(strqty))
                    {
                        numberQty = int.Parse(strqty);
                    }

                    if (numberQty > unFinishQty)
                    {
                        string msg02 = UIHelper.UImsg("The entered quantity is greater than the outstanding quantity", Program.client, "", Program.client.Language);
                        MessageBox.Show(msg02, msg01);
                        return;
                    }

                    if (obj != null)
                    {
                        string org_id = obj.ORG_ID;
                        string se_id = obj.SE_ID;
                        string se_seq = obj.SE_SEQ;

                        string size_no = "";
                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            size_no = obj.SIZE_NO;
                        }
                        else
                        {
                            size_no = listSize.Items[listSize.SelectedIndex].ToString();
                        }


                        string scan_ip = IPUtil.GetIpAddress();
                        string size_seq = obj.SIZE_SEQ;
                        string art_no = obj.ART_NO;
                        string se_day = "";
                        if (!string.IsNullOrWhiteSpace(obj.SE_DAY))
                        {
                            se_day = obj.SE_DAY.Substring(0, 10);
                        }

                        string productionOrder = obj.PRODUCTION_ORDER;
                        try
                        {
                            if (numberQty < 0)
                            {
                                if (!InsertReturnSize(Abs(numberQty), se_id, vPO, d_dept, size_no, org_id))
                                {
                                    MessageBox.Show("Error in rollback record！");
                                }
                            }

                            if (UpdateInFinshQty(org_id, se_id, se_seq, size_no, size_seq, numberQty, scan_ip, vPO, art_no, se_day, productionOrder, mainProductionOrder))
                            {
                                if (numberQty == unFinishQty)
                                {
                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = smile;
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                    ReflashListSize();
                                }
                                else if (numberQty > unFinishQty && labelInputWorkReport.BackColor == Color.CornflowerBlue)
                                {
                                    SetScanFailedStatus();
                                    MessageBox.Show("wrong");
                                }
                                else
                                {
                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = smile;
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                }

                                unFinishQty = unFinishQty - numberQty;
                                dayFinishQty += numberQty;
                            }
                            else
                            {
                                SetScanFailedStatus();
                            }
                        }
                        catch (Exception ex)
                        {
                            SetScanFailedStatus();
                            MessageHelper.ShowErr(this, ex.Message);
                        }

                        int sizeFinishQty = GeSizeFinishQty(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
                        txtSizeFinishQty.Text = sizeFinishQty.ToString();

                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            DisplayQtyButton(unFinishQty);
                        }
                        else if (labelInputWorkReport.BackColor == Color.LightCoral)
                        {
                            SetSizeButtonBackColorToDefault();

                            workDaySizeList = GetWorkDaySize(mainProductionOrder);

                            if (workDaySizeList.Count > 0)
                            {
                                listSize.Items.Clear();
                                foreach (var o in workDaySizeList)
                                {
                                    listSize.Items.Add(o.SIZE_NO);
                                }
                            }

                            DisplayQtyButton(0);
                        }

                        txtSize.Text = obj.SIZE_NO;
                    }

                    txtPo.Text = vPO; //PO number
                    txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
                    txtQty.Text = strqty; //quantity
                    txtMainProductionOrderId.Text = "";
                    txtDept.Text = "";
                }
                else
                {
                    string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
                    MessageHelper.ShowErr(this, msg);
                }
            }

        }

        /// <summary>
        ///     The default production information, that is, the information in the far right column is empty
        /// </summary>
        private void SetProductInfoToDefault(int qty, int sizeFinish)
        {
            if (listSizeSelectIndex == -1)
            {
                txtSizeQty.Text = 0.ToString(); //Number of sub-work orders
                txtSizeFinishQty.Text = 0.ToString(); //Number of work orders dispatched
            }
            else
            {
                txtSizeQty.Text = qty.ToString(); //Number of sub-work orders
                txtSizeFinishQty.Text = sizeFinish.ToString(); //Number of work orders dispatched
            }

            txtSize.Text = ""; //size
            txtQty.Text = ""; //quantity
        }

        /// <summary>
        ///     refresh the size list
        /// </summary>
        private void ReflashListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            workDaySizeList.RemoveAt(listSizeSelectIndex);
            listSizeSelectIndex = -1;
            listSize.SelectedIndex = listSizeSelectIndex;
        }

        /// <summary>
        ///     Enter the master production work order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void txtMainProductionOrderId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
        //            if (IsQrCode(length))
        //            {
        //                if (string.IsNullOrWhiteSpace(textDept.Text)) //Work Center (Code name of sewing machine department)
        //                {
        //                    string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
        //                    MessageHelper.ShowErr(this, msg);
        //                    txtMainProductionOrderId.Text = "";
        //                    return;
        //                }

        //                listSize.Items.Clear();
        //                SetScanSizeButtonToDefault();
        //                string scan_ip = IPUtil.GetIpAddress();
        //                //string ord_id = str[1];//Factory Organization This QR code factory is wrong.
        //                if (str != null)
        //                {
        //                    string se_id = str[2]; //Sales order: This is the sub work order number
        //                    string se_seq = "1"; //Sales order serial number
        //                    productionOrder = str[4]; //Sub work order

        //                    if (!string.IsNullOrWhiteSpace(productionOrder))
        //                    {
        //                        DataTable dtMES010M = GetMainProductionOrder(productionOrder); //Obtained by sub-ticket number
        //                        string mainProductionOrder = ""; //Master order number
        //                        string orgId = ""; //Factory number
        //                        if (dtMES010M != null && dtMES010M.Rows.Count > 0)
        //                        {
        //                            if (dtMES010M.Rows != null)
        //                            {
        //                                mainProductionOrder = dtMES010M.Rows[0]["udf01"].ToString(); //Master work order number
        //                                orgId = dtMES010M.Rows[0]["org"].ToString();
        //                            }
        //                        }

        //                        if (!string.IsNullOrWhiteSpace(mainProductionOrder))
        //                        {
        //                            workDaySizeList = GetWorkDaySize(mainProductionOrder);
        //                            string size_no = str[6]; //size
        //                            int qty = int.Parse(str[7]); //quantity
        //                            string size_seq = "";
        //                            if (!string.IsNullOrWhiteSpace(size_no))
        //                            {
        //                                if (workDaySizeList != null && workDaySizeList.Count > 0)
        //                                {
        //                                    foreach (VwAssemblyByOrderSize item in workDaySizeList)
        //                                    {
        //                                        if (item.SIZE_NO == size_no)
        //                                        {
        //                                            size_seq = item.SIZE_SEQ;
        //                                            se_seq = item.SE_SEQ;
        //                                            break;
        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            string art_no = str[9]; //art
        //                            string qrCodeId = str[3]; //QR code ID

        //                            bool isExistQRCode = qrCodeId != null && IsExistQRCode(qrCodeId, se_id);
        //                            if (isExistQRCode)
        //                            {
        //                                MessageHelper.ShowErr(this, "The same QR code exists, please check and re-enter！");
        //                                txtMainProductionOrderId.Text = "";
        //                                return;
        //                            }

        //                            try
        //                            {
        //                                DataTable dt = GetSeSizeDetail(mainProductionOrder, se_seq, size_no); //Get it according to the main work order number
        //                                if (dt != null && dt.Rows.Count <= 0)
        //                                {
        //                                    string msg02 = UIHelper.UImsg("查无此数据！", Program.client, "", Program.client.Language);
        //                                    MessageBox.Show(msg02, msg01);
        //                                    txtMainProductionOrderId.Text = "";
        //                                    SetScanFailedStatus();
        //                                    return;
        //                                }

        //                                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PO"].ToString()))
        //                                {
        //                                    vPO = dt.Rows[0]["PO"].ToString();
        //                                }

        //                                txtPo.Text = vPO;
        //                                string se_day = "";
        //                                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SE_DAY"].ToString()))
        //                                {
        //                                    se_day = dt.Rows[0]["SE_DAY"].ToString().Substring(0, 10);
        //                                }

        //                                int subOrderQty = 0;
        //                                if (!string.IsNullOrWhiteSpace(dt.Rows[0]["QTY"].ToString()))
        //                                {
        //                                    subOrderQty = (int)decimal.Parse(dt.Rows[0]["QTY"].ToString());
        //                                }

        //                                int allSizeFinishQty = GetSizeFinishQtyByProductOrder(productionOrder, size_no); //input quantity
        //                                unFinishQty = subOrderQty - allSizeFinishQty; //Uninvested Quantity = Sub-Order Quantity - Cumulative Quantity of Yards Invested

        //                                if (qty > unFinishQty)
        //                                {
        //                                    string msg02 = UIHelper.UImsg("扫描数量大于剩余未扫描数量！", Program.client, "", Program.client.Language);
        //                                    MessageBox.Show(msg02, msg01);
        //                                    txtMainProductionOrderId.Text = "";
        //                                    SetScanFailedStatus();
        //                                    return;
        //                                }

        //                                if (!string.IsNullOrWhiteSpace(mainProductionOrder))
        //                                {
        //                                    if (UpdateInFinshQtyByQRCodeId(orgId, se_id, se_seq, size_no, size_seq, qty, scan_ip, vPO, art_no, se_day, productionOrder, qrCodeId, mainProductionOrder))
        //                                    {
        //                                        int sizeFinishQty = GeSizeFinishQty(orgId, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
        //                                        txtSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
        //                                        dayFinishQty += qty;
        //                                        txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
        //                                        txtSizeQty.Text = subOrderQty.ToString(); //Number of sub-work orders
        //                                        txtSize.Text = size_no; //size
        //                                        txtQty.Text = qty.ToString(); //Number of work orders
        //                                        btnImage.Visible = true;
        //                                        btnImage.BackgroundImage = smile;
        //                                        btnImage.BackColor = Color.Transparent;
        //                                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                                    }
        //                                    else
        //                                    {
        //                                        SetScanFailedStatus();
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    MessageBox.Show("The main ticket number is empty error！", msg01);
        //                                    return;
        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                SetScanFailedStatus();
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
        //            //The main order number will jump here。
        //            if (string.IsNullOrWhiteSpace(textDept.Text)) //Work Center (Code name of sewing machine department)
        //            {
        //                string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
        //                MessageHelper.ShowErr(this, msg);
        //                txtMainProductionOrderId.Text = "";
        //                return;
        //            }

        //            if (!string.IsNullOrWhiteSpace(txtMainProductionOrderId.Text))
        //            {
        //                mainProductionOrder = txtMainProductionOrderId.Text.Trim().ToUpper().Split('|')[0];
        //                if (!string.IsNullOrWhiteSpace(mainProductionOrder))
        //                {
        //                    listSize.Items.Clear();
        //                    vPO = "";
        //                    workDaySizeList = GetWorkDaySize(mainProductionOrder);

        //                    if (workDaySizeList != null && workDaySizeList.Count > 0)
        //                    {
        //                        foreach (var o in workDaySizeList)
        //                        {
        //                            listSize.Items.Add(o.SIZE_NO);
        //                        }

        //                        vPO = workDaySizeList[0].PO;
        //                        txtMainProductionOrderId.Text = "";
        //                    }

        //                    SetScanSizeButtonToDefault();
        //                    txtPo.Text = vPO; //The number of POs on the far right
        //                }
        //            }

        //            SetProductInfoToDefault(0, 0);
        //        }
        //    }
        //}

        /************** Modify by Venkat 2024/05/11*************/
        private void txtMainProductionOrderId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                        if (IsQrCode(length))
                        {
                            if (string.IsNullOrWhiteSpace(textDept.Text)) //Work Center (Code name of sewing machine department)
                            {
                                string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
                                MessageHelper.ShowErr(this, msg);
                                txtMainProductionOrderId.Text = "";
                                return;
                            }

                            listSize.Items.Clear();
                            SetScanSizeButtonToDefault();
                            string scan_ip = IPUtil.GetIpAddress();
                            //string ord_id = str[1];//Factory Organization This QR code factory is wrong.
                            if (str != null)
                            {
                                string se_id = str[2]; //Sales order: This is the sub work order number
                                string se_seq = "1"; //Sales order serial number
                                productionOrder = str[4]; //Sub work order

                                if (!string.IsNullOrWhiteSpace(productionOrder))
                                {
                                    DataTable dtMES010M = GetMainProductionOrder(productionOrder); //Obtained by sub-ticket number
                                    string mainProductionOrder = ""; //Master order number
                                    string orgId = ""; //Factory number
                                    if (dtMES010M != null && dtMES010M.Rows.Count > 0)
                                    {
                                        if (dtMES010M.Rows != null)
                                        {
                                            mainProductionOrder = dtMES010M.Rows[0]["udf01"].ToString(); //Master work order number
                                            orgId = dtMES010M.Rows[0]["org"].ToString();
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(mainProductionOrder))
                                    {
                                        workDaySizeList = GetWorkDaySize(mainProductionOrder);
                                        string size_no = str[6]; //size
                                        int qty = int.Parse(str[7]); //quantity
                                        string size_seq = "";
                                        if (!string.IsNullOrWhiteSpace(size_no))
                                        {
                                            if (workDaySizeList != null && workDaySizeList.Count > 0)
                                            {
                                                foreach (VwAssemblyByOrderSize item in workDaySizeList)
                                                {
                                                    if (item.SIZE_NO == size_no)
                                                    {
                                                        size_seq = item.SIZE_SEQ;
                                                        se_seq = item.SE_SEQ;
                                                        break;
                                                    }
                                                }
                                            }
                                        }

                                        string art_no = str[9]; //art
                                        string qrCodeId = str[3]; //QR code ID

                                        bool isExistQRCode = qrCodeId != null && IsExistQRCode(qrCodeId, se_id);
                                        if (isExistQRCode)
                                        {
                                            MessageHelper.ShowErr(this, "The same QR code exists, please check and re-enter！");
                                            txtMainProductionOrderId.Text = "";
                                            return;
                                        }

                                        try
                                        {
                                            DataTable dt = GetSeSizeDetail(mainProductionOrder, se_seq, size_no); //Get it according to the main work order number
                                            if (dt != null && dt.Rows.Count <= 0)
                                            {
                                                string msg02 = UIHelper.UImsg("查无此数据！", Program.client, "", Program.client.Language);
                                                MessageBox.Show(msg02, msg01);
                                                txtMainProductionOrderId.Text = "";
                                                SetScanFailedStatus();
                                                return;
                                            }

                                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PO"].ToString()))
                                            {
                                                vPO = dt.Rows[0]["PO"].ToString();
                                            }

                                            txtPo.Text = vPO;
                                            string se_day = "";
                                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["SE_DAY"].ToString()))
                                            {
                                                se_day = dt.Rows[0]["SE_DAY"].ToString().Substring(0, 10);
                                            }

                                            int subOrderQty = 0;
                                            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["QTY"].ToString()))
                                            {
                                                subOrderQty = (int)decimal.Parse(dt.Rows[0]["QTY"].ToString());
                                            }

                                            int allSizeFinishQty = GetSizeFinishQtyByProductOrder(productionOrder, size_no); //input quantity
                                            unFinishQty = subOrderQty - allSizeFinishQty; //Uninvested Quantity = Sub-Order Quantity - Cumulative Quantity of Yards Invested

                                            if (qty > unFinishQty)
                                            {
                                                string msg02 = UIHelper.UImsg("扫描数量大于剩余未扫描数量！", Program.client, "", Program.client.Language);
                                                MessageBox.Show(msg02, msg01);
                                                txtMainProductionOrderId.Text = "";
                                                SetScanFailedStatus();
                                                return;
                                            }

                                            if (!string.IsNullOrWhiteSpace(mainProductionOrder))
                                            {
                                                if (UpdateInFinshQtyByQRCodeId(orgId, se_id, se_seq, size_no, size_seq, qty, scan_ip, vPO, art_no, se_day, productionOrder, qrCodeId, mainProductionOrder))
                                                {
                                                    int sizeFinishQty = GeSizeFinishQty(orgId, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
                                                    txtSizeFinishQty.Text = sizeFinishQty.ToString(); //Number of work orders dispatched
                                                    dayFinishQty += qty;
                                                    txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
                                                    txtSizeQty.Text = subOrderQty.ToString(); //Number of sub-work orders
                                                    txtSize.Text = size_no; //size
                                                    txtQty.Text = qty.ToString(); //Number of work orders
                                                    btnImage.Visible = true;
                                                    btnImage.BackgroundImage = smile;
                                                    btnImage.BackColor = Color.Transparent;
                                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                                }
                                                else
                                                {
                                                    SetScanFailedStatus();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("The main ticket number is empty error！", msg01);
                                                return;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            SetScanFailedStatus();
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
                        //The main order number will jump here。
                        if (string.IsNullOrWhiteSpace(textDept.Text)) //Work Center (Code name of sewing machine department)
                        {
                            string msg = UIHelper.UImsg("请输入投入组别！", Program.client, "", Program.client.Language);
                            MessageHelper.ShowErr(this, msg);
                            txtMainProductionOrderId.Text = "";
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(txtMainProductionOrderId.Text))
                        {
                            mainProductionOrder = txtMainProductionOrderId.Text.Trim().ToUpper().Split('|')[0];
                            if (!string.IsNullOrWhiteSpace(mainProductionOrder))
                            {
                                listSize.Items.Clear();
                                vPO = "";
                                workDaySizeList = GetWorkDaySize(mainProductionOrder);

                                if (workDaySizeList != null && workDaySizeList.Count > 0)
                                {
                                    foreach (var o in workDaySizeList)
                                    {
                                        listSize.Items.Add(o.SIZE_NO);
                                    }

                                    vPO = workDaySizeList[0].PO;
                                    txtMainProductionOrderId.Text = "";
                                }

                                SetScanSizeButtonToDefault();
                                txtPo.Text = vPO; //The number of POs on the far right
                            }
                        }

                        SetProductInfoToDefault(0, 0);
                    }

                }



            }
        }

        /// <summary>
        ///     Get all SIZE scheduling quantities
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="SizeNo"></param>
        /// <returns></returns>
        private int GetSizeFinishQtyByProductOrder(string productOrder, string SizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", productOrder);
            p.Add("vSizeNo", SizeNo);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetSizeFinishQtyByProductOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
            p.Add("vDDept", textDept.Text); //work center code
            p.Add("vInOut", "IN");
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


        private DataTable GetMainProductionOrder(string productionOrder)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("production_order", productionOrder); //Sub work order number
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetMainProductionOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
            }

            return null;
        }

        /// <summary>
        ///     Enter department
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDept_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtDept.Text.Contains('|'))
                {
                    return;
                }

                string dept = txtDept.Text.Trim().ToUpper().Split('|')[0];
                try
                {
                    if (ValisStitchingDept(dept, "S"))
                    {
                        d_dept = dept;
                        txtDepartName.Text = txtDept.Text.Trim().ToUpper().Split('|')[1]; //work center name
                        listSize.Items.Clear();
                        txtSizeQty.Text = ""; //Number of sub-work orders
                        txtSizeFinishQty.Text = ""; //Number of work orders dispatched
                        txtPo.Text = ""; //PO
                        txtSize.Text = ""; //Size
                        txtQty.Text = ""; //quantity
                        textDept.Text = d_dept; //Work Center (Code name of sewing machine department)
                        txtDept.Text = "";
                        dayFinishQty = GetDayFinishQty(textDept.Text); //Obtained according to the work center (code of sewing machine department), load the quantity completed on the day
                        LoadMainProductionOrder();
                        //Number of workers dispatched today
                        txtFinishQty.Text = dayFinishQty.ToString();
                        SetScanSizeButtonToDefault();
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
            }
        }

        /// <summary>
        ///     Judging whether it is a legal QR code by the length
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

        /// <summary>
        ///     Set scan size default
        /// </summary>
        private void SetScanSizeButtonToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                button.Visible = false;
            }
        }

        /// <summary>
        ///     Refresh daily schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReflashMesWorkDay_Click(object sender, EventArgs e)
        {
            LoadMainProductionOrder();
            listSize.Items.Clear();
            SetScanSizeButtonToDefault();
        }

        /// <summary>
        ///     Set scan failure status
        /// </summary>
        private void SetScanFailedStatus()
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
            timer1.Interval = 1000;//Note: When there is return data, update the number list on the interface.
            if (!isFinishQtyButton)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    button.BackColor = labelInputWorkReport.BackColor;
                }
            }
            else
            {
                button_qty.BackColor = labelInputWorkReport.BackColor;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }

            timer1.Stop();
        }

        private void textEnterQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
        }

        //private void butConfirm_Click(object sender, EventArgs e)
        //{
        //    if (d_dept == "")
        //    {
        //        string msg02 = UIHelper.UImsg("请输入投入组别", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    if (listSizeSelectIndex < 0)
        //    {
        //        string msg02 = UIHelper.UImsg("请选择SIZE", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    if ("".Equals(textEnterQty.Text) || decimal.Parse(textEnterQty.Text) == 0)
        //    {
        //        string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    int enterQty = int.Parse(textEnterQty.Text);

        //    if (enterQty > unFinishQty && labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //    {
        //        string msg02 = UIHelper.UImsg("输入的数量大于未完工的数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }
        //    else if (Abs(enterQty) > int.Parse(txtSizeReturnQty.Text) && labelInputWorkReport.BackColor == Color.LightCoral)
        //    {
        //        string msg02 = UIHelper.UImsg("输入的数量大于可退回的数量", Program.client, "", Program.client.Language);
        //        MessageBox.Show(msg02, msg01);
        //        return;
        //    }

        //    VwAssemblyByOrderSize obj = workDaySizeList[listSizeSelectIndex];
        //    string org_id = obj.ORG_ID;
        //    string se_id = obj.SE_ID;
        //    string se_seq = obj.SE_SEQ;
        //    string size_no = "";
        //    if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //    {
        //        size_no = obj.SIZE_NO;
        //    }
        //    else if (labelInputWorkReport.BackColor == Color.LightCoral)
        //    {
        //        if (listSize != null)
        //        {
        //            size_no = listSize.Items[listSize.SelectedIndex].ToString();
        //        }
        //    }

        //    string size_seq = obj.SIZE_SEQ;
        //    string art_no = obj.ART_NO;
        //    if (obj.SE_DAY != null)
        //    {
        //        string se_day = obj.SE_DAY.Substring(0, 10);
        //        string scan_ip = IPUtil.GetIpAddress();
        //        string productionOrder = obj.PRODUCTION_ORDER;
        //        string msg12 = UIHelper.UImsg("确认提交数据？", Program.client, "", Program.client.Language);
        //        DialogResult dr = MessageBox.Show(msg12, msg01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (dr == DialogResult.Yes)
        //        {
        //            isFinishQtyButton = false;
        //            SetButtonEnable((Button)sender);
        //            try
        //            {
        //                if (enterQty < 0)
        //                {
        //                    if (!InsertReturnSize(Abs(enterQty), se_id, vPO, d_dept, size_no, org_id))
        //                    {
        //                        MessageBox.Show("Error in rollback record！");
        //                    }
        //                }

        //                if (UpdateInFinshQty(org_id, se_id, se_seq, size_no, size_seq, enterQty, scan_ip, vPO, art_no, se_day, productionOrder, mainProductionOrder))
        //                {
        //                    if (enterQty == unFinishQty)
        //                    {
        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = smile;
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                        ReflashListSize();
        //                    }
        //                    else
        //                    {
        //                        btnImage.Visible = true;
        //                        btnImage.BackgroundImage = smile;
        //                        btnImage.BackColor = Color.Transparent;
        //                        btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                    }

        //                    unFinishQty = unFinishQty - enterQty;
        //                    dayFinishQty += enterQty;
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
        //                btnImage.Visible = true;
        //                btnImage.BackgroundImage = cry;
        //                btnImage.BackColor = Color.Transparent;
        //                btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        //                MessageHelper.ShowErr(this, ex.Message);
        //            }

        //            int sizeFinishQty = GeSizeFinishQty(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
        //            txtSizeFinishQty.Text = sizeFinishQty.ToString();

        //            if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
        //            {
        //                DisplayQtyButton(unFinishQty);
        //            }
        //            else if (labelInputWorkReport.BackColor == Color.LightCoral)
        //            {
        //                SetSizeButtonBackColorToDefault();

        //                workDaySizeList = GetWorkDaySize(mainProductionOrder);

        //                if (workDaySizeList.Count > 0)
        //                {
        //                    listSize.Items.Clear();
        //                    foreach (var o in workDaySizeList)
        //                    {
        //                        listSize.Items.Add(o.SIZE_NO);
        //                    }
        //                }

        //                DisplayQtyButton(0);
        //            }

        //            txtPo.Text = vPO;
        //            txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
        //            txtSize.Text = obj.SIZE_NO;
        //            txtQty.Text = enterQty.ToString(); //quantity
        //            txtMainProductionOrderId.Text = "";
        //            textEnterQty.Text = "";
        //        }
        //    }
        //}


        private void butConfirm_Click(object sender, EventArgs e) // add by Venkat 2024.05.11 add maintenance logic
        {

            if (judgeTime())
            {
                MessageBox.Show("Maintenace Period Cannot Input!! ");
                return;
            }
            else
            {
                if (d_dept == "")
                {
                    string msg02 = UIHelper.UImsg("请输入投入组别", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                if (listSizeSelectIndex < 0)
                {
                    string msg02 = UIHelper.UImsg("请选择SIZE", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                if ("".Equals(textEnterQty.Text) || decimal.Parse(textEnterQty.Text) == 0)
                {
                    string msg02 = UIHelper.UImsg("请输入大于0的转移数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                int enterQty = int.Parse(textEnterQty.Text);

                if (enterQty > unFinishQty && labelInputWorkReport.BackColor == Color.CornflowerBlue)
                {
                    string msg02 = UIHelper.UImsg("输入的数量大于未完工的数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }
                else if (Abs(enterQty) > int.Parse(txtSizeReturnQty.Text) && labelInputWorkReport.BackColor == Color.LightCoral)
                {
                    string msg02 = UIHelper.UImsg("输入的数量大于可退回的数量", Program.client, "", Program.client.Language);
                    MessageBox.Show(msg02, msg01);
                    return;
                }

                VwAssemblyByOrderSize obj = workDaySizeList[listSizeSelectIndex];
                string org_id = obj.ORG_ID;
                string se_id = obj.SE_ID;
                string se_seq = obj.SE_SEQ;
                string size_no = "";
                if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                {
                    size_no = obj.SIZE_NO;
                }
                else if (labelInputWorkReport.BackColor == Color.LightCoral)
                {
                    if (listSize != null)
                    {
                        size_no = listSize.Items[listSize.SelectedIndex].ToString();
                    }
                }

                string size_seq = obj.SIZE_SEQ;
                string art_no = obj.ART_NO;
                if (obj.SE_DAY != null)
                {
                    string se_day = obj.SE_DAY.Substring(0, 10);
                    string scan_ip = IPUtil.GetIpAddress();
                    string productionOrder = obj.PRODUCTION_ORDER;
                    string msg12 = UIHelper.UImsg("确认提交数据？", Program.client, "", Program.client.Language);
                    DialogResult dr = MessageBox.Show(msg12, msg01, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        isFinishQtyButton = false;
                        SetButtonEnable((Button)sender);
                        try
                        {
                            if (enterQty < 0)
                            {
                                if (!InsertReturnSize(Abs(enterQty), se_id, vPO, d_dept, size_no, org_id))
                                {
                                    MessageBox.Show("Error in rollback record！");
                                }
                            }

                            if (UpdateInFinshQty(org_id, se_id, se_seq, size_no, size_seq, enterQty, scan_ip, vPO, art_no, se_day, productionOrder, mainProductionOrder))
                            {
                                if (enterQty == unFinishQty)
                                {
                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = smile;
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                    ReflashListSize();
                                }
                                else
                                {
                                    btnImage.Visible = true;
                                    btnImage.BackgroundImage = smile;
                                    btnImage.BackColor = Color.Transparent;
                                    btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                                }

                                unFinishQty = unFinishQty - enterQty;
                                dayFinishQty += enterQty;
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
                            btnImage.Visible = true;
                            btnImage.BackgroundImage = cry;
                            btnImage.BackColor = Color.Transparent;
                            btnImage.BackgroundImageLayout = ImageLayout.Stretch;
                            MessageHelper.ShowErr(this, ex.Message);
                        }

                        int sizeFinishQty = GeSizeFinishQty(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
                        txtSizeFinishQty.Text = sizeFinishQty.ToString();

                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            DisplayQtyButton(unFinishQty);
                        }
                        else if (labelInputWorkReport.BackColor == Color.LightCoral)
                        {
                            SetSizeButtonBackColorToDefault();

                            workDaySizeList = GetWorkDaySize(mainProductionOrder);

                            if (workDaySizeList.Count > 0)
                            {
                                listSize.Items.Clear();
                                foreach (var o in workDaySizeList)
                                {
                                    listSize.Items.Add(o.SIZE_NO);
                                }
                            }

                            DisplayQtyButton(0);
                        }

                        txtPo.Text = vPO;
                        txtFinishQty.Text = dayFinishQty.ToString(); //Number of workers dispatched today
                        txtSize.Text = obj.SIZE_NO;
                        txtQty.Text = enterQty.ToString(); //quantity
                        txtMainProductionOrderId.Text = "";
                        textEnterQty.Text = "";
                    }
                }
            }
        }

        /// <summary>
        ///     The rollback operation is canceled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackOff_Click(object sender, EventArgs e)
        {
            //displayQtyButton(0);
            listSize.Items.Clear();
            textEnterQty.Clear();
            if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
            {
                SetSizeButtonBackColorToBackOff();
                if (mainProductionOrder != null)
                {
                    DataTable dt = GetSeReturnSize(mainProductionOrder);
                    if (dt.Rows.Count > 0)
                    {
                        sizeReturnDic.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            listSize.Items.Add(row["size_no"].ToString());
                            sizeReturnDic.Add(row["size_no"].ToString(), (int)decimal.Parse(row["returnqty"].ToString()));
                        }
                    }
                }
            }
            else if (labelInputWorkReport.BackColor == Color.LightCoral)
            {
                SetSizeButtonBackColorToDefault();
                if (workDaySizeList == null && mainProductionOrder != null)
                    workDaySizeList = GetWorkDaySize(mainProductionOrder);

                if (workDaySizeList.Count > 0)
                {
                    foreach (var o in workDaySizeList)
                    {
                        listSize.Items.Add(o.SIZE_NO);
                    }
                }
            }

            listSize_SelectedIndexChanged(sender, e);
            DisplayQtyButton(0);
        }

        /// <summary>
        ///     Set the size button to blue
        /// </summary>
        private void SetSizeButtonBackColorToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (button.Text.Contains("-"))
                {
                    button.BackColor = Color.CornflowerBlue;
                    button.Text = button.Text.Replace("-", "");
                    button.Enabled = true;
                }
            }

            labelInputWorkReport.BackColor = Color.CornflowerBlue;
            labelInputWorkReport.Text = "Enter the no.of reports：";
        }

        /// <summary>
        ///     Set the size button to red
        /// </summary>
        private void SetSizeButtonBackColorToBackOff()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (!button.Text.Contains("-"))
                {
                    button.Text = "-" + button.Text;
                    button.BackColor = Color.LightCoral;
                    button.Enabled = true;
                }
            }

            labelInputWorkReport.BackColor = Color.LightCoral;
            labelInputWorkReport.Text = "Enter the rollback amount：";
        }


        private DataTable GetSeReturnSize(string seId)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vSeId", seId);
            p.Add("vDDept", d_dept);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.StitchingInOutServer", "GetSeReturnSize", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void textEnterQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (labelInputWorkReport.BackColor == Color.LightCoral)
            {
                if (!string.IsNullOrWhiteSpace(textEnterQty.Text))
                {
                    if (textEnterQty.Text.Contains("-"))
                    {
                        textEnterQty.Text = textEnterQty.Text.Replace("-", "");
                    }

                    textEnterQty.Text = "-" + textEnterQty.Text;
                    textEnterQty.SelectionStart = textEnterQty.Text.Length;
                }
            }
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

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

        private void TxtMainProductionOrderId_ParentChanged(object sender, EventArgs e)
        {

        }
    }
}