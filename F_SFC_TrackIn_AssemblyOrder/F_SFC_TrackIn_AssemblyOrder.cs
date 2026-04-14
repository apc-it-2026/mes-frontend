using AutocompleteMenuNS;
using CommanClassLib;
using MaterialSkin.Controls;
using Mes.Util;
using Newtonsoft.Json;
using ProductionInput.Bean;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using StickBottomInputByOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Math;

namespace F_SFC_TrackIn_AssemblyOrder
{
    public partial class F_SFC_TrackIn_AssemblyOrder : MaterialForm
    {
        Bitmap smile = null;
        Bitmap cry = null;
        int listSizeSelectIndex = -1;
        private string vPO;

        int dayFinishQty = 0;
        int daySizeFinishQty = 0;
        int unFinishQty = 0;
        int totalUnFinishQty = 0;
        int daySizeReturnQty = 0;
        int daySizeWorkQty = 0;
        string d_dept = "";
        string orgId = "";
        string vvWorkOrder = "";
        string vvSubWorkOrder = "";
        string vvPO = "";
        int vvSeQty = 0;
        string vvSeDay = "";
        //string userCode = "";
        string stock = "";
        string stock_orgId = "";
        string main_prod_order = "";


        IList <VwSjqdmsWorkDaySize> sjqdmsWorkDaySize = null;
        DataTable workDayDt = null;
        DataTable wipWarehouseDt = null;
        Button button_qty;
        bool ByClick = true;
        IDictionary<string, int> IsizeReturn = new Dictionary<string, int>();

        string msg01 = string.Empty;

        public F_SFC_TrackIn_AssemblyOrder()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
            smile = new Bitmap(Properties.Resources.smile);
            cry = new Bitmap(Properties.Resources.cry);
        }

        private void F_SFC_TrackIn_AssemblyOrder_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            try
            {
                tabInvestment.Height = Screen.GetBounds(this).Height - 70;
                //int pnlx = (panel8.Width - panel9.Width) / 2;
                //int pnly = (panel8.Location.Y - panel9.Location.Y) / 4;
                //panel9.Location = new Point(pnlx, pnly);
                //dtpDate.Value = DateTime.Now;


                //GetDefault();

                //LoadQueryItem();

                WorkHoursMaintain frmWorkHour = new WorkHoursMaintain(Program.Client.APIURL, Program.Client.UserToken, Program.Client, Program.Client.Language);
                //frmWorkHour.Height = tabInvestment.Height;
                //frmWorkHour.Width = tabPageWorkingHours.Width;
                frmWorkHour.TopLevel = false;
                frmWorkHour.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmWorkHour.Dock = DockStyle.Fill;
                // frmWorkHour.WindowState = FormWindowState.Maximized;
                tabPageWorkingHours.Controls.Add(frmWorkHour);

                frmWorkHour.Show();

                tabInvestment.SelectedIndex = frmWorkHour.AfterShow();

                d_dept = frmWorkHour.d_dept;               

                GetUserInformation();
                autocompleteMenu1.SetAutocompleteMenu(TextQuerySeID, autocompleteMenu1);
                autocompleteMenu2.SetAutocompleteMenu(textWIPWarehouse, autocompleteMenu2);
                //Limit investment to processing departments only
                string sql = $@"select*from base005m where DEPARTMENT_CODE='" + d_dept + "' and UDF01='L' and UDF02='Y'";
                DataTable d = Program.Client.GetDT(sql);
                if (d.Rows.Count > 0)
                {
                    LoadSeId();
                    LoadWIPWarehouse();
                }
                else
                {

                    MessageBox.Show("Please use the Assembly account to input data");
                }

            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            //LoadDayFinishQty();
            //SetDayFinishQty();
            //SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        private void GetUserInformation()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetUserInformation", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                string s_user = "Barcode：" + dtJson.Rows[0]["staff_no"].ToString() + "         Username：" + dtJson.Rows[0]["staff_name"].ToString();
                lbInformation.Text = s_user;                           
                textWorkCenterCode.Text = dtJson.Rows[0]["staff_department"].ToString();
                textWorkCenterName.Text = dtJson.Rows[0]["department_name"].ToString();
                orgId = dtJson.Rows[0]["ORG_ID"].ToString();
                d_dept= dtJson.Rows[0]["staff_department"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        public bool HaveworkingHour(string work_day, string dept_no)
        {
            bool is_true = false;
            try
            {
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("Vdate", work_day);
                p.Add("Vdeptno", dept_no);
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI", "KZ_SFCAPI.Controllers.IE_AchievementServer", "HaveworkingHour", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    is_true = true;
                }
                else
                {
                    TextQuerySeID.Text = string.Empty;
                    //MessageBox.Show(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageHelper.ShowWarning(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowErr(this, ex.Message.ToString());
            }
            return is_true;
        }
        private void LoadSeId() //加载主工单数据
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(300, 300);  //设置弹出框大小
            var columnWidth = new int[] { 50, 300 };
            workDayDt = GetSeId();  //获取数据
            //if(workDayDt.Rows.Count > 0)
            //{ 
            //    orgId = workDayDt.Rows[0]["ORG_ID"].ToString();
            //}
            
            int n = 1;
            for (int i = 0; i < workDayDt.Rows.Count; i++)
            {
                //弹出框插入数据
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                    new[] { n + "", workDayDt.Rows[i]["MAIN_PROD_ORDER"].ToString() + " " + workDayDt.Rows[i]["SE_ID"].ToString() + " " + workDayDt.Rows[i]["PO"].ToString() }, workDayDt.Rows[i]["MAIN_PROD_ORDER"].ToString() + "|" + workDayDt.Rows[i]["SE_ID"].ToString() + "|" + workDayDt.Rows[i]["PO"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private void LoadWIPWarehouse() //加载领料仓库数据
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new System.Drawing.Size(300, 300);  //设置弹出框大小
            var columnWidth = new int[] { 50, 300 };
            wipWarehouseDt = GetWIPWarehouse();  //获取数据
            int n = 1;
            for (int i = 0; i < wipWarehouseDt.Rows.Count; i++)
            {
                //弹出框插入数据
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(
                    new[] { n + "", wipWarehouseDt.Rows[i]["WAREHOUSE_CODE"].ToString() + " " + wipWarehouseDt.Rows[i]["WAREHOUSE_NAME"].ToString() + " " + wipWarehouseDt.Rows[i]["ORG_ID"].ToString() }, wipWarehouseDt.Rows[i]["WAREHOUSE_CODE"].ToString() + "|" + wipWarehouseDt.Rows[i]["WAREHOUSE_NAME"].ToString() + "|" + wipWarehouseDt.Rows[i]["ORG_ID"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        ///// <summary>
        ///// 加载领料仓库数据
        ///// </summary>
        //private void LoadWIPWarehouse() 
        //{
        //    List<string> stringList = new List<string>();
        //    AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();
        //    wipWarehouseDt = GetWIPWarehouse();  //获取数据
        //    foreach (DataRow item in wipWarehouseDt.Rows)
        //    {
        //        string row = item["WAREHOUSE_CODE"].ToString() + "|" + item["WAREHOUSE_NAME"].ToString() + "|" +item["ORG_ID"].ToString();
        //        stringList.Add(row);
        //    }

        //    autoComplete.AddRange(stringList.ToArray());

        //    textWIPWarehouse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    textWIPWarehouse.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    textWIPWarehouse.AutoCompleteCustomSource = autoComplete;
        //}

        private void LoadDayFinishQty() //加载当天完成派工的数量
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

        private void SetDayFinishQty()  //赋值【今日派工数量】文本
        {
            textDayFinishQty.Text = dayFinishQty.ToString();
        }

        /// <summary>
        /// 获取当天完成派工的数量
        /// </summary>
        private void GetDayFinishQty()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetDayFinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dayFinishQty = int.Parse(json.ToString());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        /// <summary>
        /// 获取组别当天的制令
        /// </summary>
        /// <returns></returns>
        private DataTable GetSeId()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("DeptNo", textWorkCenterCode.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetSeOrderByDeptNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json); //查询结果存在空值时无法提取数据
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        /// <summary>
        /// 获取线边仓数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetWIPWarehouse()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetWIPWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                
                textWIPCode.Text = dt.Rows[0]["WAREHOUSE_CODE"].ToString();
                textWIPName.Text = dt.Rows[0]["WAREHOUSE_NAME"].ToString();
                stock = dt.Rows[0]["WAREHOUSE_CODE"].ToString();
                stock_orgId = dt.Rows[0]["ORG_ID"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        /// <summary>
        /// 根据主工单获取所有size的数据，用一个IList存起来，提供其它功能引用
        /// </summary>
        /// <param name="vMainProdOrder"></param>
        /// <returns></returns>
        private IList<VwSjqdmsWorkDaySize> GetSeSize(string vMainProdOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("DeptNo", textWorkCenterCode.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetSeSizeByOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return DataConvertUtil<VwSjqdmsWorkDaySize>.ConvertDataTableToList(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vMainProdOrder"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        private DataTable GetSeReturnSize(string vMainProdOrder, string orgId)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vDDept", d_dept);
            p.Add("vOrgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetSeReturnSize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private int GetDaySizeWorkQty(int SelectIndex)
        {
            VwSjqdmsWorkDaySize vswdz = sjqdmsWorkDaySize[SelectIndex];
            int qty = decimal.ToInt32(vswdz.SE_QTY);
            return qty;
        }


        private int GeSizeFinishQty(string vSeId, string size_no)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vDept", d_dept);
            p.Add("vSeId", vSeId);
            p.Add("vSizeNo", size_no);
            p.Add("main_prod_order",main_prod_order); 
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetSizeFinishQtyByOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }

        private int GeTotalSizeFinishQty(string vSeId, string size_no)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", vSeId);
            p.Add("vSizeNo", size_no);
            p.Add("main_prod_order", main_prod_order);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetTotalSizeFinishQtyByOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }

        private bool updateFinshQty
            (
                string orgId, string se_Id, string seSeq, string SizeNo, int qty, string scan_ip, string qrcode_id, string ArtNo, string SeDay, string PO, string ProdOrder, string MainProdOrder,
                string vStock_orgId, string vStocNo, string vItemNo, string vBatchNo
            )
        {
            bool isOK = false;
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", se_Id);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", d_dept);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vQrcodeId", qrcode_id);
            p.Add("vArtNo", ArtNo);
            p.Add("vSeDay", SeDay);
            p.Add("vPO", PO);
            p.Add("vProdOrder", ProdOrder);
            p.Add("vMainProdOrder", MainProdOrder);
            p.Add("vStock_orgId", vStock_orgId);
            p.Add("vStocNo", vStocNo);
            p.Add("vItemNo", vItemNo);
            p.Add("vTransType", "261");
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", "ALL");
            p.Add("vOperateType", "C");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "updateFinshQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }

        private bool InsertReturnSize(decimal returnsizeqty, string se_id, string po, string d_dept, string size_no, string orgId)
        {
            bool isOK = false;
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("returnsizeqty", returnsizeqty);
            p.Add("se_id", se_id);
            p.Add("po", po);
            p.Add("d_dept", d_dept);
            p.Add("size_no", size_no);
            p.Add("orgId", orgId);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "InsertReturnSize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }

        private void listSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDayFinishQty();
            SetDayFinishQty();
            if (listSize.SelectedIndex > -1)
            {
                try
                {
                    listSizeSelectIndex = listSize.SelectedIndex;
                    VwSjqdmsWorkDaySize vswdz = sjqdmsWorkDaySize[listSizeSelectIndex];

                    string se_id = vswdz.SE_ID;
                    int se_seq = vswdz.SE_SEQ;
                    string size_no = vswdz.SIZE_NO;

                    //工单size的排产数量
                    DataTable dt = GetSeSizeDetailByOrder(main_prod_order, listSize.Items[listSizeSelectIndex].ToString(), d_dept);
                    daySizeWorkQty = (int)decimal.Parse(dt.Rows[0]["SE_QTY"].ToString());
                    textOrderNo.Text = dt.Rows[0]["MAIN_PROD_ORDER"].ToString();
                    textSubOrder.Text = dt.Rows[0]["SUB_PROD_ORDER"].ToString();
                    //if (label12.BackColor == Color.LightCoral)
                    //{
                    //    //DataTable dt = GetSeSizeDetail(seId, se_seq.ToString(), listSize.Items[listSizeSelectIndex].ToString());
                    //    DataTable dt = GetSeSizeDetailByOrder(seId, listSize.Items[listSizeSelectIndex].ToString());
                    //    daySizeWorkQty = (int)decimal.Parse(dt.Rows[0]["SE_QTY"].ToString());
                    //}
                    //else
                    //{
                    //daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                    //}

                    //所在部门工单size的完工数量
                    daySizeFinishQty = GeSizeFinishQty(se_id, listSize.Items[listSizeSelectIndex].ToString());
                    //工单size的总未完工数量
                    totalUnFinishQty = GeTotalSizeFinishQty(se_id, listSize.Items[listSizeSelectIndex].ToString());
                    //所在部门工单size的未完工数量
                    if (totalUnFinishQty > (daySizeWorkQty - daySizeFinishQty))
                    {
                        unFinishQty = daySizeWorkQty - daySizeFinishQty;
                    }
                    else
                    {
                        unFinishQty = totalUnFinishQty;
                    }
                    //unFinishQty = daySizeWorkQty - daySizeFinishQty;

                    // 工单size的可回退数量
                    if (IsizeReturn.Count > 0 && label12.BackColor == Color.LightCoral)
                        textReturnQty.Text = IsizeReturn[listSize.Items[listSizeSelectIndex].ToString()].ToString();
                    else
                        textReturnQty.Text = "0";

                    if (label12.BackColor == Color.CornflowerBlue)
                        displayQtyButton(unFinishQty);
                    else if (label12.BackColor == Color.LightCoral)
                    {
                        if (int.TryParse(textReturnQty.Text, out daySizeReturnQty))
                        {
                            displayQtyButton(daySizeReturnQty);
                        }
                    }

                    setProductInfoToDefault(daySizeWorkQty, daySizeFinishQty);
                    //setSizeButtonBackColorToDefault();
                    TextQuerySeID.Text = "";
                    textSize.Text = size_no;

                }
                catch (Exception ex)
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
            }
            else
            {
                setProductInfoToDefault(0, 0);
            }
        }
        //判断是否已经启动排程，如果已经启动则不允许投产
        private bool GetPaicheng()
        {
            bool isOK = false;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionOutputOrderServer", "IsTruebyProduction_order", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            return isOK;
        }
        /// <summary>
        /// 通过点击进行报工，相应的需要刷新size列表和button的数量和背景颜色
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="clickButton"></param>
        private void finishQty(string strqty, Button clickButton)
        {
            //判断是否已经启动过排程
            if (DateTime.Now.Hour >= 21)
            {
                if (GetPaicheng() == false)
                {
                    ScanFailed();
                    SJeMES_Framework.Common.UIHelper.UImsg("时间超时，今日已不允许投产！！！", Program.Client, "", Program.Client.Language);
                    return;
                }
            }
            ByClick = true;
            button_qty = clickButton;
            SetButtonEnable(button_qty);
            VwSjqdmsWorkDaySize obj = sjqdmsWorkDaySize[listSizeSelectIndex];
            string org_id = obj.ORG_ID.ToString();
            string se_id = obj.SE_ID;
            string se_seq = obj.SE_SEQ.ToString();
            string ArtNo = obj.ART_NO;
            string MtNo = obj.MATERIAL_NO;
            string SeDay = obj.SE_DAY;
            string PO = obj.PO;
            string ProdOrder = obj.PRODUCTION_ORDER;
            string MainProdOrder = obj.MAIN_PROD_ORDER;
            string BatchNo = obj.BATCH_NO;
            string size_no = "";
            if (label12.BackColor == Color.CornflowerBlue)
                size_no = obj.SIZE_NO;
            else
                size_no = listSize.Items[listSize.SelectedIndex].ToString();
            int qty = int.Parse(strqty);
            int oldunFinishQty = unFinishQty;
            string scan_ip = IPUtil.GetIpAddress();
            string qrcode_id = "";
            string txtWarehouseCode = textWIPCode.Text.ToString().ToUpper();
            //string itemNo = GetItemNo(ProdOrder);
            string itemNo = "";
            //if (itemNo == "")
            //{
            //    return;
            //}
            try
            {
                if (!string.IsNullOrWhiteSpace(txtWarehouseCode) && !string.IsNullOrEmpty(txtWarehouseCode))
                {
                    //if (qty <= GetStockRemainingQty(stock_orgId, stock, itemNo)) //报工数量大于库存数量时，不允许报工
                    //if (GetStockRemainingQty(stock_orgId, stock, itemNo, BatchNo, qty) == 1) //报工数量大于库存数量时，不允许报工
                    //{
                        daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                        //工单size的总未完工数量
                        totalUnFinishQty = GeTotalSizeFinishQty(se_id, size_no);
                        //所在部门工单size的未完工数量
                        if (totalUnFinishQty > (daySizeWorkQty - daySizeFinishQty))
                        {
                            unFinishQty = daySizeWorkQty - daySizeFinishQty;
                        }
                        else
                        {
                            unFinishQty = totalUnFinishQty;
                        }
                        //unFinishQty = daySizeWorkQty - daySizeFinishQty;
                        if (unFinishQty < qty)
                        {
                            btnImage.Visible = true;
                            btnImage.BackgroundImage = cry;
                            this.btnImage.BackColor = System.Drawing.Color.Transparent;
                            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            textSizeQty.Text = daySizeWorkQty.ToString();
                            displayQtyButton(unFinishQty);
                            if (unFinishQty <= 0)
                            {
                                reflashListSize();
                            }
                            string msg = SJeMES_Framework.Common.UIHelper.UImsg("排程变更，请刷新界面后再进行操作!", Program.Client, "", Program.Client.Language);
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                            return;
                        }
                        if (qty < 0)
                        {
                            //报工退回和库存退回共用同一个接口、同一个事务，确保其中一个失败时同时回滚。
                            if (!InsertReturnSize(Abs(qty), se_id, vPO, d_dept, size_no, org_id))
                            {
                                MessageBox.Show("回退记录出错！");
                            }
                        }
                        //生成报工信息和修改库存信息共用同一个接口、同一个事务，确保其中一个失败时同时回滚。
                        if (updateFinshQty(org_id, se_id, se_seq, size_no, qty, scan_ip, qrcode_id, ArtNo, SeDay, PO, ProdOrder, MainProdOrder, stock_orgId, stock, itemNo, BatchNo))
                        {


                            if (qty == unFinishQty)
                            {
                                reflashListSize();
                            }
                            btnImage.Visible = true;
                            btnImage.BackgroundImage = smile;
                            this.btnImage.BackColor = System.Drawing.Color.Transparent;
                            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                            unFinishQty -= qty;
                            daySizeFinishQty += qty;
                            dayFinishQty += qty;
                            SetDayFinishQty();
                        }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("库存数量不足！");
                    //    btnImage.Visible = true;
                    //    btnImage.BackgroundImage = cry;
                    //    this.btnImage.BackColor = System.Drawing.Color.Transparent;
                    //    this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    //}
                }
                else
                {
                    MessageBox.Show("请选择领料仓库！");
                    //unFinishQty = oldunFinishQty;
                    btnImage.Visible = true;
                    btnImage.BackgroundImage = cry;
                    this.btnImage.BackColor = System.Drawing.Color.Transparent;
                    this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                //unFinishQty = oldunFinishQty;
                btnImage.Visible = true;
                btnImage.BackgroundImage = cry;
                this.btnImage.BackColor = System.Drawing.Color.Transparent;
                this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }


            textSizeQty.Text = daySizeWorkQty.ToString();
            textSizeFinishQty.Text = daySizeFinishQty.ToString();
            if (label12.BackColor == Color.CornflowerBlue)
            {
                displayQtyButton(unFinishQty);
            }
            else if (label12.BackColor == Color.LightCoral)
            {
                setSizeButtonBackColorToDefault();

                sjqdmsWorkDaySize = GetSeSize(main_prod_order);

                if (sjqdmsWorkDaySize.Count > 0)
                {
                    this.listSize.Items.Clear();
                    foreach (var o in sjqdmsWorkDaySize)
                    {
                        this.listSize.Items.Add(o.SIZE_NO);
                    }
                }
                displayQtyButton(0);
            }
            //setSizeButtonBackColorToDefault();
            //clickButton.BackColor = Color.Blue;
            //textPo.Text = vPO;
            textSize.Text = obj.SIZE_NO;
            textQty.Text = strqty;
            TextQuerySeID.Text = "";
        }

        /// <summary>
        /// 设置size的按钮为蓝色
        /// </summary>
        private void setSizeButtonBackColorToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (button.Text.Contains("-"))
                {
                    button.BackColor = System.Drawing.Color.CornflowerBlue;
                    button.Text = button.Text.Replace("-", "");
                    button.Enabled = true;
                }
            }
            label12.BackColor = System.Drawing.Color.CornflowerBlue;
            label12.Text = "Enter Qty to report：";
        }

        /// <summary>
        /// 清空根据选中size对应的button
        /// </summary>
        /// <param name="uFinishQty">未完工的数量</param>
        private void displayQtyButton(int uFinishQty)
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

        private void reflashListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            sjqdmsWorkDaySize.RemoveAt(listSizeSelectIndex);
            listSizeSelectIndex = -1;
            listSize.SelectedIndex = listSizeSelectIndex;
        }

        private void btnReflash_Click(object sender, EventArgs e)
        {
            LoadSeId();
            listSize.Items.Clear();
            setSizeButtonToDefault();
        }

        private void setSizeButtonToDefault()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                //button.BackColor = Color.Green;
                button.Visible = false;
            }
        }

        private void TextQuerySeID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                listSizeSelectIndex = -1;
                string txtQrCode = TextQuerySeID.Text.ToString().ToUpper();
                //类别,工厂代号,销售订单号,二维码ID,子工单号,调拨单/采购单-行号,Size,数量,料号,ART,模号,批次
                //B,1001,100005,20210809100007878,100012,-,8.5,50,30101T00158.5,KOJIMA,325816,100005

                if (!string.IsNullOrWhiteSpace(txtQrCode) && !string.IsNullOrEmpty(txtQrCode) && txtQrCode.Contains(","))
                {
                    string[] str = txtQrCode.Split(',');
                    int length = str.Length;
                    if (ParseQrCode(str, length))
                    {
                        listSize.Items.Clear();
                        setSizeButtonToDefault();
                        string scan_ip = IPUtil.GetIpAddress();
                        //string org_id = str[1]; //标签上的工厂代号有问题，直接取工单上的
                        string se_id = str[2];
                        string qrcode_id = str[3];
                        string se_seq = "1";
                        string size_no = str[6];
                        int qty = int.Parse(str[7]);
                        string size_seq = str[8];
                        string art_no = str[9];
                        string materialNo = str[8];
                        string batch_No = str[11];
                        string dept = d_dept;
                        string PO = "";
                        string se_day = "";
                        string prod_order = "";
                        //string main_prod_order = "";
                        int se_qty = 0;
                        int finish_qty = 0;
                        string txtWarehouseCode = textWIPCode.Text.ToString().ToUpper();
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(txtWarehouseCode) && !string.IsNullOrEmpty(txtWarehouseCode))
                            {
                                //if (qty <= GetStockRemainingQty(stock_orgId, stock, materialNo)) //报工数量大于库存数量时，不允许报工
                                //if (GetStockRemainingQty(stock_orgId, stock, materialNo, batch_No, qty) == 1) //报工数量大于库存数量时，不允许报工
                                //{
                                    //检查二维码ID是否已存在
                                    if(GetQrcodeId(qrcode_id, se_id) > 0)
                                    {
                                        string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("二维码重复扫描！", Program.Client, "", Program.Client.Language);
                                        MessageBox.Show(msg02, msg01);
                                        TextQuerySeID.Text = "";
                                        ScanFailed();
                                        return;
                                    }

                                    //根据工作中心、销售订单、size获取工单信息，判断是否可以报工
                                    DataTable dt = GetProductionOrder(orgId, dept, se_id, size_no);
                                    if (dt.Rows.Count <= 0)
                                    {
                                        string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("查无此数据！", Program.Client, "", Program.Client.Language);
                                        MessageBox.Show(msg02, msg01);
                                        TextQuerySeID.Text = "";
                                        ScanFailed();
                                        return;
                                    }
                                    if (dt.Rows.Count == 1)
                                    {
                                        PO = dt.Rows[0]["PO"].ToString();
                                        se_day = dt.Rows[0]["SE_DAY"].ToString();
                                        prod_order = dt.Rows[0]["SUB_ORDER"].ToString();
                                        main_prod_order = dt.Rows[0]["PROD_ORDER"].ToString();
                                        se_qty = (int)decimal.Parse(dt.Rows[0]["SE_QTY"].ToString());
                                        finish_qty = GeSizeFinishQty(se_id, size_no);
                                        totalUnFinishQty = GeTotalSizeFinishQty(se_id, size_no);
                                        textPo.Text = PO;
                                        textSeid.Text = se_id;
                                        textSubOrder.Text = prod_order;
                                        textOrderNo.Text = main_prod_order;
                                    }
                                    if (dt.Rows.Count > 1)
                                    {
                                        TrackIn_AssemblyOrder_SelectOrder frmSelect = new TrackIn_AssemblyOrder_SelectOrder(dt);
                                        frmSelect.DataChange += new TrackIn_AssemblyOrder_SelectOrder.DataChangeHandler(DataChange);
                                        frmSelect.ShowDialog();
                                       
                                        PO = vvPO;
                                        se_day = vvSeDay;
                                        prod_order = vvSubWorkOrder;
                                        main_prod_order = vvWorkOrder;
                                        se_qty = vvSeQty;
                                        finish_qty = GeSizeFinishQty(se_id, size_no);
                                        totalUnFinishQty = GeTotalSizeFinishQty(se_id, size_no);
                                        textPo.Text = PO;
                                        textSubOrder.Text = prod_order;
                                        textOrderNo.Text = main_prod_order;
                                    }
                                    if (se_qty <= finish_qty)
                                    {
                                        string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("扫描数量大于订单未投入数量！", Program.Client, "", Program.Client.Language);
                                        MessageBox.Show(msg02, msg01);
                                        TextQuerySeID.Text = "";
                                        ScanFailed();
                                        return;
                                    }
                                    if (se_qty - finish_qty < qty)
                                    {
                                        string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("扫描数量大于订单未投入数量！", Program.Client, "", Program.Client.Language);
                                        MessageBox.Show(msg02, msg01);
                                        TextQuerySeID.Text = "";
                                        ScanFailed();
                                        return;
                                    }
                                    if (se_qty - finish_qty > totalUnFinishQty) //当前部门的剩余未投入数量不能大于工单的总未投入数量
                                    {
                                        string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("扫描数量大于订单未投入数量！", Program.Client, "", Program.Client.Language);
                                        MessageBox.Show(msg02, msg01);
                                        TextQuerySeID.Text = "";
                                        ScanFailed();
                                        return;
                                    }
                                //判断是否已经启动过排程
                                if (DateTime.Now.Hour >= 21)
                                {
                                    if (GetPaicheng() == false)
                                    {
                                        ScanFailed();
                                        SJeMES_Framework.Common.UIHelper.UImsg("时间超时，今日已不允许投产！！！", Program.Client, "", Program.Client.Language);
                                        return;
                                    }
                                }
                                //生成报工信息和修改库存信息共用同一个接口、同一个事务，确保其中一个失败时同时回滚。
                                if (updateFinshQty(orgId, se_id, se_seq, size_no, qty, scan_ip, qrcode_id, art_no, se_day, PO, prod_order, main_prod_order, stock_orgId, stock, materialNo, batch_No))
                                    {
                                        textSizeFinishQty.Text = (finish_qty + qty).ToString();
                                        dayFinishQty += qty;
                                        SetDayFinishQty();
                                        textSizeQty.Text = se_qty.ToString();
                                        textSize.Text = size_no;
                                        textQty.Text = qty.ToString();
                                        btnImage.Visible = true;
                                        btnImage.BackgroundImage = new Bitmap(Properties.Resources.smile);
                                        this.btnImage.BackColor = System.Drawing.Color.Transparent;
                                        this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                                    }
                                //}
                                //else
                                //{
                                //    MessageBox.Show("库存数量不足！");
                                //    btnImage.Visible = true;
                                //    btnImage.BackgroundImage = cry;
                                //    this.btnImage.BackColor = System.Drawing.Color.Transparent;
                                //    this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                                //}
                            }
                            else
                            {
                                MessageBox.Show("Please select a picking warehouse！");
                                ScanFailed();
                            }
                        }
                        catch (Exception ex)
                        {
                            //scanToDefault("", "", "");
                            ScanFailed();
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }

                        main_prod_order = TextQuerySeID.Text;
                        TextQuerySeID.Text = "";
                    }
                }
                else
                {
                    if (!TextQuerySeID.Text.Contains("|"))
                    {
                        return;
                    }
                    main_prod_order = TextQuerySeID.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0];
                    if (!string.IsNullOrEmpty(main_prod_order))
                    {
                        listSize.Items.Clear();
                        vPO = "";
                        sjqdmsWorkDaySize = GetSeSize(main_prod_order);
                        if (sjqdmsWorkDaySize.Count > 0)
                        {
                            foreach (var o in sjqdmsWorkDaySize)
                            {
                                this.listSize.Items.Add(o.SIZE_NO);
                            }
                            for (int i = 0; i < workDayDt.Rows.Count; i++)
                            {
                                if (main_prod_order == workDayDt.Rows[i]["MAIN_PROD_ORDER"].ToString())
                                {
                                    vPO = workDayDt.Rows[i]["PO"].ToString();
                                    textSeid.Text = workDayDt.Rows[i]["se_id"].ToString();
                                    break;
                                }
                            }
                            TextQuerySeID.Text = "";
                        }
                        
                        setSizeButtonToDefault();
                        textOrderNo.Text = main_prod_order;
                        textPo.Text = vPO;
                       // textSeid.Text = se_id;
                        textSubOrder.Text = "";
                    }
                    setProductInfoToDefault(0, 0);
                    setSizeButtonBackColorToDefault();
                }
            }
        }

        private void DataChange(object sender, TrackIn_AssemblyOrder_SelectOrder.DataChangeEventArgs args)
        {
            vvWorkOrder = args.vProdOrder;
            vvSubWorkOrder = args.vSubOrder;
            vvPO = args.vPO;
            vvSeQty = (int)decimal.Parse(args.vSeQty);
            vvSeDay = args.vSeDay;
        }

        private void TextWIPWarehouse_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)  //仓库文本框回车
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!textWIPWarehouse.Text.Contains("|"))
                {
                    return;
                }

                string txtWIPCode = textWIPWarehouse.Text.ToString().ToUpper();

                if (!string.IsNullOrWhiteSpace(txtWIPCode) && !string.IsNullOrEmpty(txtWIPCode))
                {
                    string[] str = textWIPWarehouse.Text.Trim().ToString().ToUpper().Split('|');
                    textWIPCode.Text = str[0];
                    textWIPName.Text = str[1];
                    textWIPWarehouse.Text = "";
                    stock = str[0];
                    stock_orgId = str[2];
                }
            }
        }

        private bool updateInFinshQty(string orgId, string se_Id, string seSeq, string SizeNo, string size_seq, int qty, string scan_ip, string po, string artNo, string seDay)
        {
            bool isOK = false;
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vSeId", se_Id);
            p.Add("vSeSeq", seSeq);
            p.Add("vSizeNo", SizeNo);
            p.Add("vDDept", d_dept);
            p.Add("vSizeSeq", size_seq);
            p.Add("vQty", qty);
            p.Add("vIP", scan_ip);
            p.Add("vPO", po);
            p.Add("vArtNo", artNo);
            p.Add("vSeDay", seDay);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "updateInFinshQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return isOK;
        }
        private DataTable GetSeSizeDetail(string se_Id, string se_seq, string size_no)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vSeId", se_Id);
            p.Add("vSeSeq", se_seq);
            p.Add("vSizeNo", size_no);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetSeSizeDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private DataTable GetSeSizeDetailByOrder(string vMainProdOrder, string size_no, string vDept)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", orgId);
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vSizeNo", size_no);
            p.Add("vDept", vDept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetSeSizeDetailByOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private DataTable GetProductionOrder(string vOrgId, string DeptNo, string SalesOrder, string SizeNo)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", vOrgId);
            p.Add("DeptNo", DeptNo);
            p.Add("SalesOrder", SalesOrder);
            p.Add("SizeNo", SizeNo);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetProductionOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void ScanFailed()
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = new Bitmap(Properties.Resources.cry);
            this.btnImage.BackColor = System.Drawing.Color.Transparent;
            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        }

        private Boolean ParseQrCode(string[] str, int length)
        {
            if (length == 12)
            {
                return true;
            }
            else
            {
                //string msg01 = SJeMES_Framework.Common.UIHelper.UImsg("Tips", Program.client, "", Program.client.Language);
                string msg02 = SJeMES_Framework.Common.UIHelper.UImsg("二维码长度有误，请联系系统管理员！", Program.Client, "", Program.Client.Language);
                MessageBox.Show(msg02, msg01);
                return false;
            }
        }


        private void setProductInfoToDefault(int qty, int daySizeFinishQty)
        {
            if (listSizeSelectIndex == -1)
            {
                textSizeQty.Text = 0.ToString();
                textSizeFinishQty.Text = 0.ToString();
            }
            else
            {
                textSizeQty.Text = qty.ToString();
                textSizeFinishQty.Text = daySizeFinishQty.ToString();
            }
            textSize.Text = "";
            textQty.Text = "";
        }

        private void SetButtonEnable(Button clickButton)
        {
            if (!ByClick)
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
            if (!ByClick)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    button.BackColor = label12.BackColor;
                }
            }
            else
            {
                button_qty.BackColor = label12.BackColor;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }
            timer1.Stop();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            finishQty(((Button)sender).Text, (Button)sender);
            //LoadSeId();
        }

        private void butQuery_Click(object sender, EventArgs e)
        {
            Hide();
            TrackIn_AssemblyOrder_InOutDetail frm = new TrackIn_AssemblyOrder_InOutDetail(d_dept);
            frm.ShowDialog();
            System.Threading.Thread.Sleep(200);
            Show();
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            if (listSizeSelectIndex < 0)
            {
                MessageBox.Show("Please select order/PO and SIZE", "Error!");
                return;
            }

            if ("".Equals(textEnterQty.Text.ToString()) || decimal.Parse(textEnterQty.Text.ToString()) == 0)
            {
                MessageBox.Show("Please enter a transfer quantity greater than 0", "Error!");
                return;
            }


            int enterQty = int.Parse(textEnterQty.Text.ToString());

            if ((enterQty > unFinishQty) && (label12.BackColor == Color.CornflowerBlue))
            {
                MessageBox.Show("The quantity entered is greater than the quantity that can be transferred", "Error!");
                return;
            }
            else if ((Abs(enterQty) > int.Parse(textReturnQty.Text)) && (label12.BackColor == Color.LightCoral))
            {
                MessageBox.Show("The entered quantity cannot be greater than the returnable quantity", "Error!");
                return;
            }

            VwSjqdmsWorkDaySize obj = sjqdmsWorkDaySize[listSizeSelectIndex];
            string org_id = obj.ORG_ID.ToString();
            string se_id = obj.SE_ID;
            string se_seq = obj.SE_SEQ.ToString();
            string ArtNo = obj.ART_NO;
            string MtNo = obj.MATERIAL_NO;
            string SeDay = obj.SE_DAY;
            string PO = obj.PO;
            string ProdOrder = obj.PRODUCTION_ORDER;
            string MainProdOrder = obj.MAIN_PROD_ORDER;
            string BatchNo = obj.BATCH_NO;
            string size_no = "";
            if (label12.BackColor == Color.CornflowerBlue)
                size_no = obj.SIZE_NO;
            else if (label12.BackColor == Color.LightCoral)
                size_no = listSize.Items[listSize.SelectedIndex].ToString();
            int oldunFinishQty = unFinishQty;
            string scan_ip = IPUtil.GetIpAddress();
            string qrcode_id = "";
            string txtWarehouseCode = textWIPCode.Text.ToString().ToUpper();
            //string itemNo = GetItemNo(ProdOrder);
            string itemNo = "";

            DialogResult dr = MessageBox.Show("确认提交数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ByClick = false;
                SetButtonEnable((Button)sender);
                try
                {
                    if (!string.IsNullOrWhiteSpace(txtWarehouseCode) && !string.IsNullOrEmpty(txtWarehouseCode))
                    {
                        //if (enterQty <= GetStockRemainingQty(stock_orgId, stock, itemNo)) //报工数量大于库存数量时，不允许报工
                        //if (GetStockRemainingQty(stock_orgId, stock, itemNo, BatchNo, enterQty) == 1) //报工数量大于库存数量时，不允许报工
                        //{
                            daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                            //工单size的总未完工数量
                            totalUnFinishQty = GeTotalSizeFinishQty(se_id, size_no);
                            //所在部门工单size的未完工数量
                            if (totalUnFinishQty > (daySizeWorkQty - daySizeFinishQty))
                            {
                                unFinishQty = daySizeWorkQty - daySizeFinishQty;
                            }
                            else
                            {
                                unFinishQty = totalUnFinishQty;
                            }
                            //unFinishQty = daySizeWorkQty - daySizeFinishQty;
                            if (unFinishQty < enterQty)
                            {
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = cry;
                                this.btnImage.BackColor = System.Drawing.Color.Transparent;
                                this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                                textSizeQty.Text = daySizeWorkQty.ToString();
                                displayQtyButton(unFinishQty);
                                if (unFinishQty <= 0)
                                {
                                    reflashListSize();
                                }
                                string msg = SJeMES_Framework.Common.UIHelper.UImsg("排程变更，请刷新界面后再进行操作!", Program.Client, "", Program.Client.Language);
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                                return;
                            }

                            if (enterQty < 0)
                            {
                                //报工退回和库存退回共用同一个接口、同一个事务，确保其中一个失败时同时回滚。
                                if (!InsertReturnSize(Abs(enterQty), se_id, vPO, d_dept, size_no, org_id))
                                {
                                    MessageBox.Show("回退记录出错！");
                                }
                            }
                            //生成报工信息和修改库存信息共用同一个接口、同一个事务，确保其中一个失败时同时回滚。
                            if (updateFinshQty(org_id, se_id, se_seq, size_no, enterQty, scan_ip, qrcode_id, ArtNo, SeDay, PO, ProdOrder, MainProdOrder, stock_orgId, stock, itemNo, BatchNo))
                            {
                                if (enterQty == unFinishQty)
                                {
                                    reflashListSize();
                                }
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = smile;
                                this.btnImage.BackColor = System.Drawing.Color.Transparent;
                                this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

                                unFinishQty = unFinishQty - enterQty;
                                daySizeFinishQty += enterQty;
                                dayFinishQty += enterQty;
                                SetDayFinishQty();
                            }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("库存数量不足！");
                        //    btnImage.Visible = true;
                        //    btnImage.BackgroundImage = cry;
                        //    this.btnImage.BackColor = System.Drawing.Color.Transparent;
                        //    this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        //}
                    }
                    else
                    {
                        MessageBox.Show("请选择领料仓库！");
                        //unFinishQty = oldunFinishQty;
                        btnImage.Visible = true;
                        btnImage.BackgroundImage = cry;
                        this.btnImage.BackColor = System.Drawing.Color.Transparent;
                        this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    }
                }
                catch (Exception ex)
                {
                    //unFinishQty = oldunFinishQty;
                    btnImage.Visible = true;
                    btnImage.BackgroundImage = cry;
                    this.btnImage.BackColor = System.Drawing.Color.Transparent;
                    this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                }
                textSizeQty.Text = daySizeWorkQty.ToString();
                textSizeFinishQty.Text = daySizeFinishQty.ToString();
                if (label12.BackColor == Color.CornflowerBlue)
                {
                    displayQtyButton(unFinishQty);
                }
                else if (label12.BackColor == Color.LightCoral)
                {
                    setSizeButtonBackColorToDefault();

                    sjqdmsWorkDaySize = GetSeSize(main_prod_order);

                    if (sjqdmsWorkDaySize.Count > 0)
                    {
                        this.listSize.Items.Clear();
                        foreach (var o in sjqdmsWorkDaySize)
                        {
                            this.listSize.Items.Add(o.SIZE_NO);
                        }
                    }
                    displayQtyButton(0);
                }
                //setSizeButtonBackColorToDefault();
                //clickButton.BackColor = Color.Blue;
                //textPo.Text = vPO;
                textSize.Text = obj.SIZE_NO;
                textQty.Text = enterQty.ToString();
                TextQuerySeID.Text = "";
                textEnterQty.Text = "";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            textTime.Text = DateTime.Now.ToString();
        }

        private void btn_Supplementary_Click(object sender, EventArgs e)
        {
            Hide();
            TrackIn_AssemblyOrder_Supplement frm = new TrackIn_AssemblyOrder_Supplement(d_dept);
            frm.ShowDialog();
            System.Threading.Thread.Sleep(200);
            Show();
        }



        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("department_no");
            dt.Columns.Add("department_name");
            // 列强制转换
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (dgv.Rows[count].Cells["isCheck"].Value.ToString().Equals("Y"))
                {
                    dr[0] = dgv.Rows[count].Cells[1].Value.ToString();
                    dr[1] = dgv.Rows[count].Cells[2].Value.ToString();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        private void btnBackOff_Click(object sender, EventArgs e)
        {
            displayQtyButton(0);
            listSize.Items.Clear();
            textEnterQty.Clear();
            if (label12.BackColor == Color.CornflowerBlue)
            {
                setSizeButtonBackColorToBackOff();
                DataTable dt = GetSeReturnSize(main_prod_order, orgId);
                if (dt.Rows.Count > 0)
                {
                    IsizeReturn.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        this.listSize.Items.Add(row["size_no"].ToString());
                        IsizeReturn.Add(row["size_no"].ToString(), (int)decimal.Parse(row["returnqty"].ToString()));
                    }
                }
                //else
                //{
                //    label12.BackColor = Color.CornflowerBlue;
                //    setSizeButtonBackColorToDefault();
                //}
            }
            else if (label12.BackColor == Color.LightCoral)
            {
                setSizeButtonBackColorToDefault();
                if (sjqdmsWorkDaySize == null)
                    sjqdmsWorkDaySize = GetSeSize(main_prod_order);

                if (sjqdmsWorkDaySize.Count > 0)
                {
                    foreach (var o in sjqdmsWorkDaySize)
                    {
                        this.listSize.Items.Add(o.SIZE_NO);
                    }
                }
            }

            listSize_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 设置size的按钮为红色
        /// </summary>
        private void setSizeButtonBackColorToBackOff()
        {
            for (int i = 1; i < 10; i++)
            {
                Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                if (!button.Text.Contains("-"))
                {
                    button.Text = "-" + button.Text;
                    button.BackColor = System.Drawing.Color.LightCoral;
                    button.Enabled = true;
                }
            }
            label12.BackColor = System.Drawing.Color.LightCoral;
            label12.Text = "Enter rollback qty：";
        }

        private void textEnterQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
                {
                    e.Handled = true;
                }
            }

        }

        private void textEnterQty_KeyUp(object sender, KeyEventArgs e)
        {
            if (label12.BackColor == Color.LightCoral)
            {
                if (!string.IsNullOrEmpty(textEnterQty.Text) && !string.IsNullOrWhiteSpace(textEnterQty.Text))
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

        private void button2_Click(object sender, EventArgs e)
        {
            //string txtWarehouseCode = textWIPCode.Text.ToString().ToUpper();
            //if (!string.IsNullOrWhiteSpace(txtWarehouseCode) && !string.IsNullOrEmpty(txtWarehouseCode))
            //{
            //    string[] str =
            //    string itemNo = GetItemNo(ArtNo);
            //    UpdateInventory(orgId, stock, d_dept, se_id, itemNo, "261", batch_No, "ALL", qty, "C"); //修改库存
            //}
            //else
            //{
            //    MessageBox.Show("领料仓库必填");
            //}
        }

        private string GetItemNo(string vProdOrder)
        {
            string itemNo = "";
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vProdOrder", vProdOrder);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetItemNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                itemNo = json;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return itemNo;
        }

        private DataTable UpdateInventory(string vOrgId, string vStocNo, string vDeptNo, string vProductionOrder, string vItemNo, string vTransType,
            string vBatchNo, string vShelfNo, decimal vQty, string vOperateType)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vOrgId", vOrgId);
            p.Add("vStocNo", vStocNo);
            p.Add("vDeptNo", vDeptNo);
            p.Add("vProductionOrder", vProductionOrder);
            p.Add("vItemNo", vItemNo);
            p.Add("vTransType", vTransType);
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", vShelfNo);
            p.Add("vQty", vQty);
            p.Add("vOperateType", vOperateType);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "UpdateInventory", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        //查询剩余库存数量
        private int GetStockRemainingQty(string vStock_orgId, string vStock, string vItemNo, string vBatchNo, decimal vQty)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vStock_orgId", vStock_orgId);
            p.Add("vStock", vStock);
            p.Add("vItemNo", vItemNo);
            p.Add("vBatchNo", vBatchNo);
            p.Add("vShelfNo", "ALL");
            p.Add("vQty", vQty);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetStockRemainingQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }

        //查询二维码ID
        private int GetQrcodeId(string vQrcodeId,string se_id)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vQrcodeId", vQrcodeId);
            p.Add("vseId", se_id);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "GetQrcodeId", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                qty = int.Parse(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return qty;
        }
        //if (string.IsNullOrEmpty(textPo.Text))
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textPo.Text))
            {
                PoRemind();
            }
        }
        private string PoRemind()
        {
            string itemNo = "";
            //DataTable dt = new DataTable();
            //Dictionary<string, Object> p = new Dictionary<string, object>();
            //p.Add("PO", textPo.Text);
            //p.Add("DEPT", textOrderNo.Text);
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderTwoServer", "PoRemind", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            //{
            //    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            //    //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            //    itemNo = json;
            //    button88.Text = itemNo;
            //    if (!string.IsNullOrEmpty(itemNo))
            //    {
            //        groupBox1.Visible = true;
            //        button88.BackColor = Color.Red;
            //        button88.Left -= 66;
            //        if (button88.Right < 0)
            //        {
            //            button88.Left = this.Width;
            //        }
            //    }
                
            //}
            //else
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            //}
            return itemNo;
        }

        private void textPo_TextChanged(object sender, EventArgs e)
        {
            PoRemind();
        }

        private void button88_SizeChanged(object sender, EventArgs e)
        {
            this.button88.SizeChanged += button88_SizeChanged;
            //button88.Font = new Font(button88.Font.FontFamily, button88.Height * 0.6f);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabInvestment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabInvestment.SelectedTab == tabPageInvest)
            //{
            //    if (!HaveworkingHour(DateTime.Now.ToShortDateString(), d_dept))
            //    {
            //        tabInvestment.SelectedTab = tabPageWorkingHours;
            //    }

            //}
        }
    }
}