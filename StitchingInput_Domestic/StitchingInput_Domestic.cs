using AutocompleteMenuNS;
using CommanClassLib;
using CommanClassLib.Util;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
using StitchingInput_Domestic.Properties;
using StitchingInput2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace StitchingInput_Domestic
{
    public partial class StitchingInput_Domestic : MaterialForm
    {
        int listSizeSelectIndex = -1;

        private DataTable User_Org;
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
        public StitchingInput_Domestic()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
            smile = new Bitmap(Resources.smile);
            cry = new Bitmap(Resources.cry);
        }
        private void StitchingInput_Domestic_Load(object sender, EventArgs e)
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
            Get_User_Org();
            msg01 = UIHelper.UImsg("Tips", Program.client, string.Empty, Program.client.Language);
            //labelMainProductionOrder.Text = "master production work order";
        }

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

        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
           // string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetAllDepts", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
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
        private void TxtDept_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                        txtDepartName.Text = txtDept.Text.Trim().ToUpper().Split('|')[1]; 
                        listSize.Items.Clear();
                        txtSizeQty.Text = ""; 
                        txtSizeFinishQty.Text = ""; //Number of work orders dispatched
                        txtPo.Text = ""; //PO
                        txtSize.Text = ""; //Size
                        txtQty.Text = ""; //quantity
                        textDept.Text = d_dept; //Work Center (Code name of sewing machine department)
                        txtDept.Text = "";
                        dayFinishQty = GetDomesticDayFinishQty(textDept.Text); //Obtained according to the work center (code of sewing machine department), load the quantity completed on the day
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

        private int GetDomesticDayFinishQty(string d_dept)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetDomesticDayFinishQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void LoadMainProductionOrder()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new Size(350, 350);
            var columnWidth = new[] { 50, 250 };
            DataTable dt = GetMainOrder_Domestic();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                        new[] { n + "", dt.Rows[i]["udf01"].ToString() }, dt.Rows[i]["udf01"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetMainOrder_Domestic()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> parm = new Dictionary<string, string>();
            parm.Add("cutting_dept", txtCuttingDept.Text); //Cutting department code
            parm.Add("sticking_dept", textDept.Text); //Work Center (Code name of sewing machine department)
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetMainOrder_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(parm));
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

        private void TxtMainProductionOrderId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                                    DataTable dtMES010M = GetMainProductionOrder(productionOrder);
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
                                        workDaySizeList = GetWorkDaySize_Domestic(mainProductionOrder);
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

                                        bool isExistQRCode = qrCodeId != null && IsExistQRCode_Domestic(qrCodeId, se_id);
                                        if (isExistQRCode)
                                        {
                                            MessageHelper.ShowErr(this, "The same QR code exists, please check and re-enter！");
                                            txtMainProductionOrderId.Text = "";
                                            return;
                                        }

                                        try
                                        {
                                            DataTable dt = GetSeSizeDetail_Domestic(mainProductionOrder, se_seq, size_no); //Get it according to the main work order number
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

                                            int allSizeFinishQty = GetSizeFinishQtyByProductOrder_Domestic(productionOrder, size_no); //input quantity
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
                                                    int sizeFinishQty = GeSizeFinishQty_Domestic(orgId, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
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
                                workDaySizeList = GetWorkDaySize_Domestic(mainProductionOrder);

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

        private DataTable GetMainProductionOrderQty(string productionOrder,string Cut_Dept, string Stitch_Dept)  
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("production_order", productionOrder); //Sub work order number
            p.Add("Cut_Dept", Cut_Dept); //Sub work order number
            p.Add("Stitch_Dept", Stitch_Dept); //Sub work order number
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetMainProductionOrder", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private IList<VwAssemblyByOrderSize> GetWorkDaySize_Domestic(string main_production_order)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("main_production_order", main_production_order); //Master work order number
            p.Add("cutting_dept", txtCuttingDept.Text); //Cutting department code
            p.Add("sticking_dept", textDept.Text); //Work Center (Code name of sewing machine department)
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetWorkDaySize_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private bool IsExistQRCode_Domestic(string qrcodeId, string se_id)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("qrcodeId", qrcodeId);
            p.Add("sales_order", se_id);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "TrackInIsExistQRCode_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private DataTable GetSeSizeDetail_Domestic(string mainProductionOrder, string se_seq, string size_no)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vSeSeq", se_seq);
            p.Add("vSizeNo", size_no);
            p.Add("mainProductionOrder", mainProductionOrder);
            string ret = WebAPIHelper.Post(Program.client.APIURL,"KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetSeSizeDetail_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void SetScanFailedStatus()
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = cry;
            btnImage.BackColor = Color.Transparent;
            btnImage.BackgroundImageLayout = ImageLayout.Stretch;
        }

        /// <summary>
        ///     Get all SIZE scheduling quantities
        /// </summary>
        /// <param name="productOrder"></param>
        /// <param name="SizeNo"></param>
        /// <returns></returns>
        private int GetSizeFinishQtyByProductOrder_Domestic(string productOrder, string SizeNo)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vProductOrder", productOrder);
            p.Add("vSizeNo", SizeNo);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GetSizeFinishQtyByProductOrder_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "UpdateInFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "UpdateInFinshQty", Program.client.UserToken, JsonConvert.SerializeObject(p));
            bool isOK = Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            return isOK;
        }

        private int GeSizeFinishQty_Domestic(string org_id, string se_id, string se_seq, string size_no, string production_order)
        {
            int qty = 0;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("vOrgId", org_id);
            p.Add("vSeId", se_id);
            p.Add("vSeSeq", se_seq);
            p.Add("vSizeNo", size_no);
            p.Add("production_order", production_order);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Input_Server", "GeSizeFinishQty_Domestic", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        private void ListSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listSize.SelectedIndex > -1)
            {
                listSizeSelectIndex = listSize.SelectedIndex;
                VwAssemblyByOrderSize model = workDaySizeList[listSizeSelectIndex];
                string se_id = model.SE_ID;
                //string org_id = model.ORG_ID;
               // string org_id = "5001";
                string org_id = "";
                if (User_Org != null && User_Org.Rows.Count > 0)
                {
                    org_id = User_Org.Rows[0]["ORG"].ToString();  // 👈 Extract string value
                }
                else
                {
                    MessageBox.Show("User organization not loaded!");
                    return;
                }
                string se_seq = model.SE_SEQ;
                string size_no = model.SIZE_NO;

                productionOrder = model.PRODUCTION_ORDER;
                if (productionOrder != null)
                {
                    DataTable dtMES010M = GetMainProductionOrderQty(productionOrder,txtCuttingDept.Text, textDept.Text);
                   // DataTable dtMES010M = GetMainProductionOrder(productionOrder);

                    int qty = 0;

                    //labelInputWorkReport is the title box of the number of input reports in the interface(Commented by Ashok)
                    //if (labelInputWorkReport.BackColor == Color.LightCoral)
                    //{
                        if (dtMES010M != null && !string.IsNullOrEmpty(dtMES010M.Rows[0]["QTY"].ToString()))
                        {
                            qty = (int)decimal.Parse(dtMES010M.Rows[0]["QTY"].ToString());
                        }
                    //}
                    //else
                    //{
                    //    qty = Convert.ToInt32(workDaySizeList[listSizeSelectIndex].QTY);
                    //}

                    //if (dtMES010M != null && !string.IsNullOrEmpty(dtMES010M.Rows[0]["QTY"].ToString()))
                    //{
                    //    qty = (int)decimal.Parse(dtMES010M.Rows[0]["QTY"].ToString());
                    //}


                    int sizeFinishQty = GeSizeFinishQty_Domestic(org_id, se_id, se_seq, size_no, productionOrder);

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

        private void Btn_c1_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c2_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c3_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c4_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c5_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c6_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c7_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c8_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
        }

        private void Btn_c9_Click(object sender, EventArgs e)
        {
            FinishQty(((Button)sender).Text, (Button)sender);
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

        public bool CheckUnfinishQty(string org_id,string se_id,string se_seq,string size_no,string ProductionOrder,int numberQty)
        {
            DataTable dtMES010M = GetMainProductionOrderQty(ProductionOrder, txtCuttingDept.Text, textDept.Text);
            int qty = 0;
            if (dtMES010M != null && !string.IsNullOrEmpty(dtMES010M.Rows[0]["QTY"].ToString()))
            {
                qty = (int)decimal.Parse(dtMES010M.Rows[0]["QTY"].ToString());
            }
            int sizeFinishQty = GeSizeFinishQty_Domestic(org_id, se_id, se_seq, size_no, ProductionOrder); //Number of work orders dispatched
            unFinishQty = qty - sizeFinishQty;
            txtSizeQty.Text = qty.ToString();
            txtSizeFinishQty.Text = sizeFinishQty.ToString();
            return unFinishQty >= numberQty;
        }
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

                    if (obj != null)
                    {
                        //string org_id = obj.ORG_ID;
                        // string org_id = "5001";  //Added by Ashok to bind APC ORG Code only.
                        string org_id = "";
                        if (User_Org != null && User_Org.Rows.Count > 0)
                        {
                            org_id = User_Org.Rows[0]["ORG"].ToString();  // 👈 Extract string value
                        }
                        else
                        {
                            MessageBox.Show("User organization not loaded!");
                            return;
                        }
                        string se_id = obj.SE_ID;
                        string se_seq = obj.SE_SEQ;
                        string productionOrder = obj.PRODUCTION_ORDER;
                        string size_no = "";
                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            size_no = obj.SIZE_NO;
                        }
                        else
                        {
                            size_no = listSize.Items[listSize.SelectedIndex].ToString();
                        }
                        if (!CheckUnfinishQty(org_id, se_id, se_seq, size_no, productionOrder, numberQty))
                        {
                            string msg02 = UIHelper.UImsg("The entered quantity is greater than the outstanding quantity", Program.client, "", Program.client.Language);
                            MessageBox.Show(msg02, msg01);
                            DisplayQtyButton(unFinishQty);
                            return;
                        }
                        if (numberQty > unFinishQty)
                        {
                            string msg02 = UIHelper.UImsg("The entered quantity is greater than the outstanding quantity", Program.client, "", Program.client.Language);
                            MessageBox.Show(msg02, msg01);
                            return;
                        }


                        string scan_ip = IPUtil.GetIpAddress();
                        string size_seq = obj.SIZE_SEQ;
                        string art_no = obj.ART_NO;
                        string se_day = "";
                        if (!string.IsNullOrWhiteSpace(obj.SE_DAY))
                        {
                            se_day = obj.SE_DAY.Substring(0, 10);
                        }

                        
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
                        int sizeFinishQty = GeSizeFinishQty_Domestic(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
                        txtSizeFinishQty.Text = sizeFinishQty.ToString();
                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            DisplayQtyButton(unFinishQty);
                        }
                        else if (labelInputWorkReport.BackColor == Color.LightCoral)
                        {
                            SetSizeButtonBackColorToDefault();

                            workDaySizeList = GetWorkDaySize_Domestic(mainProductionOrder);

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

        private void ReflashListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            workDaySizeList.RemoveAt(listSizeSelectIndex);
            listSizeSelectIndex = -1;
            listSize.SelectedIndex = listSizeSelectIndex;
        }
        private void SetSizeButtonBackColorToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (button.Text.Contains("-"))
                {
                    button.BackColor = Color.Bisque;
                    button.Text = button.Text.Replace("-", "");
                    button.Enabled = true;
                }
            }

            labelInputWorkReport.BackColor = Color.CornflowerBlue;
            labelInputWorkReport.Text = "Enter the no.of reports：";
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


        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Interval = 1000;//Note: When there is return data, update the number list on the interface.
            if (!isFinishQtyButton)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    //button.BackColor = labelInputWorkReport.BackColor;
                    button.BackColor = Color.Bisque;
                }
            }
            else
            {
                //button_qty.BackColor = labelInputWorkReport.BackColor;
                button_qty.BackColor = Color.Bisque;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }

            timer1.Stop();
        }

        private void BtnReflashMesWorkDay_Click(object sender, EventArgs e)
        {
            LoadMainProductionOrder();
            listSize.Items.Clear();
            SetScanSizeButtonToDefault();
        }
        public void Get_User_Org()
        {
            //Dictionary<string, object> p = new Dictionary<string, object>();
            //p.Add("production_order", productionOrder); //Sub work order number
            string ret = WebAPIHelper.Post(Program.client.APIURL,
                "KZ_FAST_REPORT_API",
                "KZ_FAST_REPORT_API.Controllers.Domestic_Output_Server",
                "Get_User_Org",
                Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();

                if (!string.IsNullOrEmpty(json))
                {
                    User_Org = JsonConvert.DeserializeObject<DataTable>(json);
                    //return User_Org;
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
        }
       private void ButConfirm_Click(object sender, EventArgs e)
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
                //string org_id = obj.ORG_ID;
                // string org_id = "5001";
                string org_id = "";
                if (User_Org != null && User_Org.Rows.Count > 0)
                {
                    org_id = User_Org.Rows[0]["ORG"].ToString();  // 👈 Extract string value
                }
                else
                {
                    MessageBox.Show("User organization not loaded!");
                    return;
                }
                string se_id = obj.SE_ID;
                string se_seq = obj.SE_SEQ;
                string size_no = "";
                string productionOrder = obj.PRODUCTION_ORDER;
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
                    if (!CheckUnfinishQty(org_id, se_id, se_seq, size_no, productionOrder, enterQty))
                    {
                        string msg02 = UIHelper.UImsg("The entered quantity is greater than the outstanding quantity", Program.client, "", Program.client.Language);
                        MessageBox.Show(msg02, msg01);
                        DisplayQtyButton(unFinishQty);
                        return;
                    }

                    string se_day = obj.SE_DAY.Substring(0, 10);
                    string scan_ip = IPUtil.GetIpAddress();
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

                               // unFinishQty = unFinishQty - enterQty;
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

                        int qty = 0;
                        DataTable dtMES010M = GetMainProductionOrderQty(productionOrder, txtCuttingDept.Text, textDept.Text);
                        if (dtMES010M != null && !string.IsNullOrEmpty(dtMES010M.Rows[0]["QTY"].ToString()))
                        {
                            qty = (int)decimal.Parse(dtMES010M.Rows[0]["QTY"].ToString());
                        }

                        int sizeFinishQty = GeSizeFinishQty_Domestic(org_id, se_id, se_seq, size_no, productionOrder); //Number of work orders dispatched
                        txtSizeFinishQty.Text = sizeFinishQty.ToString();
                        unFinishQty = qty - sizeFinishQty;
                        if (labelInputWorkReport.BackColor == Color.CornflowerBlue)
                        {
                            DisplayQtyButton(unFinishQty);
                        }
                        else if (labelInputWorkReport.BackColor == Color.LightCoral)
                        {
                            SetSizeButtonBackColorToDefault();

                            workDaySizeList = GetWorkDaySize_Domestic(mainProductionOrder);

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
    }
}
