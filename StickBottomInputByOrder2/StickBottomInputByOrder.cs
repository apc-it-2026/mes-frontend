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
using CommanClassLib;
using StickBottomInputByOrder2.Bean;
using AutocompleteMenuNS;
using Mes.Util;
using System.Collections;
using static System.Math;
using StickBottomInputByOrder2.Properties;

namespace StickBottomInputByOrder2
{
    public partial class StickBottomInputByOrder2 : MaterialForm
    {
        Bitmap smile = null;
        Bitmap cry = null;
        int listSizeSelectIndex = -1;
        private string vPO;
        int dayFinishQty = 0;
        int daySizeFinishQty = 0;
        int unFinishQty = 0;
        int daySizeReturnQty = 0;
        int daySizeWorkQty = 0;
        string d_dept = "";
        string WorkOrder = "";
        string Process_No = "L";
        string qrcode_id = "NO";
        string warehouse = "";
        string VVworkson = "";
        string VVSeDay = "";
        string VVPO = "";
        string VVworkorder = "";

        IList<VwSjqdmsWorkDaySize> sjqdmsWorkDaySize = null;
        DataTable workDayDt = null;
        Button button_qty;
        bool ByClick = true;
        IDictionary<string,int> IsizeReturn = new Dictionary<string, int>();

        string msg01 = string.Empty;

        public StickBottomInputByOrder2()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);

            this.WindowState = FormWindowState.Maximized;
            btnImage.Visible = false;
            smile = new Bitmap(Resources.smile);
            cry = new Bitmap(Resources.cry);
        }

        private void StickBottomForm_Load(object sender, EventArgs e)
        {
            try
            {
                tabInvestment.Height = Screen.GetBounds(this).Height - 70;
                WorkHoursMaintain frmWorkHour = new WorkHoursMaintain(Program.Client.APIURL, Program.Client.UserToken, Program.Client, Program.Client.Language);
                frmWorkHour.TopLevel = false;
                frmWorkHour.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmWorkHour.Dock = DockStyle.Fill;
                tabPageWorkingHours.Controls.Add(frmWorkHour);

                frmWorkHour.Show();

                tabInvestment.SelectedIndex = frmWorkHour.AfterShow();

                d_dept = frmWorkHour.d_dept;
                textDept.Text = d_dept;
                textDeptName.Text = frmWorkHour.d_deptName;
                //GetUserInformation();
               // LoadProcess();
                LoadOrder();
                //LoadWarehouse();
                LoadLineWarehouse();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
            LoadDayFinishQty();
            SetDayFinishQty();
        }
        /// <summary>
        /// 根据工作中心加载仓库
        /// </summary>
        private void LoadLineWarehouse()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "LoadWIPWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt.Rows.Count == 1)
                {
                    textWareHourse.Text = Convert.ToString(dt.Rows[0]["warehouse_code"]);
                    textWareHouseName.Text = Convert.ToString(dt.Rows[0]["warehouse_name"]);

                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }


        }
        private void LoadProcess()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "LoadProcess", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Process_No = json;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }


        }

        private void LoadWarehouse() {


            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 350 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["warehouse_code"].ToString() + "|" + dt.Rows[i]["warehouse_name"].ToString() }, dt.Rows[i]["warehouse_code"].ToString() + "|" + dt.Rows[i]["warehouse_name"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });

                n++;
            }


        }
        private void LoadOrder()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new System.Drawing.Size(300, 300);
            var columnWidth = new int[] { 50, 300 };
            workDayDt = GetOrder();
            int n = 1;
            //for (int i = 0; i < workDayDt.Rows.Count; i++)
            //{
            //    autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
            //        new[] { n + "", workDayDt.Rows[i]["workorder"].ToString()}, workDayDt.Rows[i]["workorder"].ToString())
            //    { ColumnWidth = columnWidth, ImageIndex = n });
            //    n++;
            //}
            for (int i = 0; i < workDayDt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(
                    // new[] { n + "", dt.Rows[i]["production_order"].ToString() + " " + dt.Rows[i]["SE_ID"].ToString() + " " + dt.Rows[i]["MER_PO"].ToString() + " " + dt.Rows[i]["PROD_NO"].ToString() }, dt.Rows[i]["production_order"].ToString() + "|" + dt.Rows[i]["SE_ID"].ToString() + "|" + dt.Rows[i]["MER_PO"].ToString() + "|" + dt.Rows[i]["PROD_NO"].ToString())
                    new[] { n + "", workDayDt.Rows[i]["workorder"].ToString() + " " + workDayDt.Rows[i]["po"].ToString() }, workDayDt.Rows[i]["workorder"].ToString() + "|" + workDayDt.Rows[i]["po"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }

        }
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetWarehouse", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                // textId.Text = dt.Rows[0]["org_id"].ToString();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }
        ///获取组别当天的工单
        
        private DataTable GetOrder()
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        /// <summary>
        /// 加载当天完成派工的数量
        /// </summary>
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
        /// <summary>
        /// 工单版本GetDayFinishQty
        /// </summary>
        private void GetDayFinishQty()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetDayFinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dayFinishQty = int.Parse(json.ToString());
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void SetDayFinishQty()
        { 
            textDayFinishQty.Text = dayFinishQty.ToString();
        }

        private void btnReflash_Click(object sender, EventArgs e)
        {
            LoadOrder();
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
        private void DataChange(object sender, StickBottomInputByOrder_SelectOrder.DataChangeEventArgs args)
        {
            VVworkson =args.WorkOrder;
            VVSeDay = args.vSeDay;
            VVPO = args.vPO;
            VVworkorder = args.VVworkorder;
        }
        private void ReLoadWorkDaySize() {
            if (!string.IsNullOrEmpty(WorkOrder))
            {
                
                listSize.Items.Clear();
                vPO = "";
                sjqdmsWorkDaySize = null;
                sjqdmsWorkDaySize = GetOrderSize(WorkOrder);
                if (sjqdmsWorkDaySize.Count > 0)
                {
                    Label:
                    foreach (var o in sjqdmsWorkDaySize)
                    {
                        if (o.QTY <=o.FINISH_QTY) {
                            sjqdmsWorkDaySize.Remove(o);
                            listSize.Items.Clear();
                            goto Label;
                        }
                        this.listSize.Items.Add(o.SIZE_NO);
                    }
                    TextQuerySeID.Text = "";
                    //for (int i = 0; i < workDayDt.Rows.Count; i++)
                    //{
                    //    if (WorkOrder == workDayDt.Rows[i]["WorkOrder"].ToString())
                    //    {
                    //        vPO = workDayDt.Rows[i]["PO"].ToString();
                    //        break;
                    //    }
                    //}

                }
                
            }

        }
        private void ReturnWorkDaySize()
        {
            if (!string.IsNullOrEmpty(WorkOrder))
            {
                listSize.Items.Clear();
                IsizeReturn.Clear();
                vPO = "";
                sjqdmsWorkDaySize.Clear();
              // sjqdmsWorkDaySize = GetOrderSize(WorkOrder);
                //if (sjqdmsWorkDaySize.Count > 0)
                //{
                //    //foreach (var o in sjqdmsWorkDaySize)
                    //{
                    //   this.listSize.Items.Add(o.SIZE_NO);
                    //}
                    TextQuerySeID.Text = "";
                    DataTable dt = GetSeReturnSize(WorkOrder);
                    if (dt.Rows.Count > 0)
                    {
                        string size_list= "(''";
                        IsizeReturn.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            size_list += ",'" + row["size_no"].ToString() + "'";
                            this.listSize.Items.Add(row["size_no"].ToString());
                            IsizeReturn.Add(row["size_no"].ToString(), (int)decimal.Parse(row["returnqty"].ToString()));
                            
                        }
                        size_list += ')';
                         DataTable sizedt = GetOrderSizeDT(WorkOrder);
                         DataView dv = sizedt.DefaultView;
                         dv.RowFilter = string.Format("size_no in {0}", size_list);
                         DataTable dtSelect = dv.ToTable();
                         sjqdmsWorkDaySize = DataConvertUtil<VwSjqdmsWorkDaySize>.ConvertDataTableToList(dtSelect);

                }

            }

        }
        private void TextQuerySeID_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listSizeSelectIndex = -1;
                string txtQrCode = TextQuerySeID.Text.ToString().ToUpper();
                //B,200,A0A19040071,SM0A190415000049,485,1,9,10,48,EF9370,03996
                //B,1005,1000000262,20210929100044225,,4500000007-10,3.5,10,30201000873.5,GW1229,386,1000000262
                //B,1005,1000000262,20210929100044225,,4500000007-10,3.5,10,30201000873.5,GW1229,386,1000000262
                //B,1001,100005,20210929100044225,,4500000007-10,8.5,1,30201000873.5,KOJIMA,386,1000000262
                //类别,组织,制令,工票唯一码，工票号,订单序,size,数量,size序号,art,模号

                if (!string.IsNullOrWhiteSpace(txtQrCode) && !string.IsNullOrEmpty(txtQrCode) && txtQrCode.Contains(","))
                {
                    string[] str = txtQrCode.Split(',');
                    int length = str.Length;
                    if (ParseQrCode(str, length))
                    {
                       // string WorkOrder, string SizeNo, int qty, string scan_ip,string qrcode_id,string datetime,string vArtNo,string vSeDay, string vPO

                        listSize.Items.Clear();
                        setSizeButtonToDefault();
                       
                        string scan_ip = IPUtil.GetIpAddress();
                        string org_id = str[1].Trim().ToUpper();
                        string se_id = str[2].Trim().ToUpper();
                        int se_seq = 1;
                        
                        string size_no = str[6].Trim().ToUpper();
                        int qty = int.Parse(str[7]);
                       string VVqrcode_id = str[3].Trim().ToUpper();
                        string art_no = str[9];

                        //return WorkOrder,vSeDay,vPO
                       

                        try
                        {
                            Dictionary<string, Object> p = new Dictionary<string, object>();
                            p.Add("vDDept", d_dept);
                            p.Add("org_id", org_id);
                            p.Add("se_id", se_id);
                            p.Add("size_no", size_no);
                            

                            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetWorkOrderList", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                            {
                                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                                if (dtJson.Rows.Count == 1)
                                {
                                    VVworkson = dtJson.Rows[0][0].ToString();
                                    VVSeDay = dtJson.Rows[0][1].ToString();
                                    VVPO = dtJson.Rows[0][2].ToString();
                                    VVworkorder = dtJson.Rows[0][3].ToString();
                                    
                                } else if(dtJson.Rows.Count<1){
                                    SJeMES_Control_Library.MessageHelper.ShowWarning(this, "没有相关工单，请检查！");
                                    ScanFailed();
                                    return;
                                }
                                else {
                                    StickBottomInputByOrder_SelectOrder form = new StickBottomInputByOrder_SelectOrder(dtJson);
                                    form.DataChange += new StickBottomInputByOrder_SelectOrder.DataChangeHandler(DataChange);
                                    form.ShowDialog();
                                }
                            
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                            }

                            //这个？？
                            DataTable dt = GetSeSizeDetail(VVworkson);
                            if (dt.Rows.Count <= 0)
                            {
                                string msg02 = "查无此数据！";
                                MessageBox.Show(msg02, msg01);
                                TextQuerySeID.Text = "";
                                ScanFailed();
                                return;
                            }
                            VVPO = dt.Rows[0]["PO"].ToString();
                            textPo.Text = VVPO;
                            string se_day = dt.Rows[0]["SE_DAY"].ToString().Substring(0, 10);
                            int se_qty = (int)decimal.Parse(dt.Rows[0]["QTY"].ToString());
                            int finish_qty = (int)decimal.Parse(dt.Rows[0]["FINISH_QTY"].ToString());
                            if (se_qty <= finish_qty)
                            {
                                string msg02 = "扫描数量大于订单未投入数量！";
                                MessageBox.Show(msg02, msg01);
                                TextQuerySeID.Text = "";
                                ScanFailed();
                                return;
                            }
                            if (se_qty - finish_qty < qty)
                            {

                                string msg02 = "扫描数量大于订单未投入数量！";
                                MessageBox.Show(msg02, msg01);
                                TextQuerySeID.Text = "";
                                ScanFailed();
                                return;
                            }
                            //这个??
                            //updateInFinshQty(org_id, se_id, se_seq, size_no, size_seq, qty, scan_ip, vPO, art_no, se_day);
                            if (GetMaterial(VVworkson) == false)
                            {
                                DialogResult dialog = MessageBox.Show("此工单未查询到鞋面BOM资料是否继续投产？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dialog == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            if (updateFinshQty(VVworkson, size_no, qty, scan_ip, VVqrcode_id, DateTime.Now.ToString(), art_no, VVSeDay, VVPO))
                            {
                                textSizeFinishQty.Text = (finish_qty + qty).ToString();
                                dayFinishQty += qty;
                                SetDayFinishQty();
                                textSizeQty.Text = se_qty.ToString();
                                textSize.Text = size_no;
                                textQty.Text = qty.ToString();
                                btnImage.Visible = true;
                                btnImage.BackgroundImage = new Bitmap(Resources.smile);
                                this.btnImage.BackColor = System.Drawing.Color.Transparent;
                                this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                            }
                            else
                            {
                                ScanFailed();
                            } 
                        }
                        catch (Exception ex)
                        {
                            
                            ScanFailed();
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
                        }
                        TextQuerySeID.Text = "";
                    }
                }
                else
                {
                    if (!TextQuerySeID.Text.Contains('|')) { return; }
                    WorkOrder = TextQuerySeID.Text.Trim().ToUpper().Split('|')[0];
                    ReLoadWorkDaySize();
                    setSizeButtonToDefault();
                    //textPo.Text = vPO;
                    textSizeProductionOrder.Text = WorkOrder;
                    setProductInfoToDefault(0, 0);
                    setSizeButtonBackColorToDefault();
                }
            }
        }



        private void ScanFailed()
        {
            btnImage.Visible = true;
            btnImage.BackgroundImage = new Bitmap(Resources.cry);
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
                //string msg01 = SJeMES_Framework.Common.UIHelper.UImsg("Tips";
                string msg02 = "二维码长度有误，请联系系统管理员！";
                MessageBox.Show(msg02, msg01);
                return false;
            }
        }

        private DataTable GetSeSizeDetail(string WorkSon)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("WorkSon", WorkSon);
            p.Add("Process_No", Process_No);
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            
            
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetSeSizeDetail", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
        private DataTable GetOrderSizeDT(string WorkOrder) {
            DataTable dt = new DataTable();
           
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("WorkOrder", WorkOrder);
            p.Add("vDDept", d_dept);
            p.Add("Process_No", Process_No);
            p.Add("vInOut", "IN");

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetOrderSize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);


                // dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }




            return dt;
        }
        private IList<VwSjqdmsWorkDaySize> GetOrderSize(string WorkOrder)
        {
            
            return DataConvertUtil<VwSjqdmsWorkDaySize>.ConvertDataTableToList(GetOrderSizeDT(WorkOrder));
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
            label12.Text = "输入报工数量：";
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

        private bool updateFinshQty( string WorkOrder, string SizeNo, int qty, string scan_ip,string qrcode_id,string datetime,string vArtNo,string vSeDay, string vPO)
        {
          //  if (   string.IsNullOrEmpty(textWareHourse.Text)) { }
            bool isOK = false;
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("production_order", WorkOrder);
            p.Add("vSizeNo", SizeNo);
            p.Add("grt_dept", d_dept);
            p.Add("vQty", qty);
            p.Add("vDDept", d_dept);
            p.Add("vIP", scan_ip);
            p.Add("rout_no", Process_No);
            p.Add("qrcode_id", qrcode_id);
            p.Add("datetime", datetime);
            p.Add("vPO", vPO);
            p.Add("vSeDay", vSeDay);
            p.Add("vArtNo", vArtNo);
            p.Add("warehouse", textWareHourse.Text);
            p.Add("mainwork", textSizeProductionOrder.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "updateFinshQty2", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            isOK = Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]);
            if (!isOK)
            {
                string meg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
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
                    VwSjqdmsWorkDaySize vswdz = sjqdmsWorkDaySize[listSizeSelectIndex];

                     textPo.Text = vswdz.PO;
                    string workson = vswdz.PRODUCTION_ORDER;
                    textWorkSon.Text = workson;
                    //string size_no = vswdz.SIZE_NO;
                    //textSize.Text = size_no;
                    //textSizeFinishQty.Text = vswdz.FINISH_QTY.ToString();
                    //textSizeQty.Text = vswdz.QTY.ToString();


                    //工单size的排产数量
                    if (label12.BackColor == Color.LightCoral)
                    {
                        DataTable dt = GetSeSizeDetail(workson);
                        daySizeWorkQty = (int)decimal.Parse(dt.Rows[0]["QTY"].ToString());
                    }
                    else
                    {///
                        daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                    }
                    /////
                    ///
                     //所在部门工单size的完工数量
                    daySizeFinishQty = GeSizeFinishQty(workson);
                    //工单size的总未完工数量
                    int   totalUnFinishQty = GeTotalSizeFinishQty(workson);
                    //所在部门工单size的未完工数量
                    if (totalUnFinishQty > (daySizeWorkQty - daySizeFinishQty))
                    {
                        unFinishQty = daySizeWorkQty - daySizeFinishQty;
                    }
                    else
                    {
                        unFinishQty = totalUnFinishQty;
                    }
                    //工单size的完工数量
                  //  daySizeFinishQty = GeSizeFinishQty(workson);
                //    unFinishQty = daySizeWorkQty - daySizeFinishQty;

                    // 工单size的可回退数量
                    if (IsizeReturn.Count > 0 && label12.BackColor == Color.LightCoral)
                        textSizeReturnQty.Text = IsizeReturn[listSize.Items[listSizeSelectIndex].ToString()].ToString();
                    else
                        textSizeReturnQty.Text = "0";

                    if (label12.BackColor == Color.CornflowerBlue)
                        displayQtyButton(unFinishQty);
                    else if (label12.BackColor == Color.LightCoral)
                    {
                        if (int.TryParse(textSizeReturnQty.Text, out daySizeReturnQty))
                        {
                            displayQtyButton(daySizeReturnQty);
                        }
                    }

                    setProductInfoToDefault(daySizeWorkQty, daySizeFinishQty);
                    //setSizeButtonBackColorToDefault();
                    TextQuerySeID.Text = "";

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
        private int GeTotalSizeFinishQty(string workson)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
           
            p.Add("WorkOrder", workson);
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            p.Add("Process_No", Process_No);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetTotalSizeFinishQtyByOrder", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
        private int GetDaySizeWorkQty(int SelectIndex)
        {
            VwSjqdmsWorkDaySize vswdz = sjqdmsWorkDaySize[SelectIndex];
            int qty = decimal.ToInt32(vswdz.QTY);
            return qty;
        }
        private int GetReturnQty(int listSizeSelectIndex)
        {

            string size = listSize.Items[listSizeSelectIndex].ToString();
            return IsizeReturn[size];
            

        }

        private int GeSizeFinishQty(string WorkOrder)
        {
            int qty = 0;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("WorkOrder", WorkOrder);
            p.Add("vDDept", d_dept);
            p.Add("vInOut", "IN");
            p.Add("Process_No", Process_No);

            
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GeSizeFinishQty", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void btn_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textWareHourse.Text.Trim()))
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowWarning(this, "请选择仓库！");
            //    return ;

            //}

            finishQty(((Button)sender).Text, (Button)sender);
        
        }
       
        private bool GetMaterial(string prod)
        {
            bool IsHave=true;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("vProd", prod);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetMaterial", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (int.Parse(json) > 0)
                {
                    IsHave = true;
                }
                else
                {
                    IsHave = false;
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return IsHave;
        }

        /// <summary>
        /// 通过点击进行报工，相应的需要刷新size列表和button的数量和背景颜色
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="clickButton"></param>
        private void finishQty(string strqty, Button clickButton)
        {
            ByClick = true;
            button_qty = clickButton;
            SetButtonEnable(button_qty);
            VwSjqdmsWorkDaySize obj = sjqdmsWorkDaySize[listSizeSelectIndex];
            
            string size_no = "";
            //if (label12.BackColor == Color.CornflowerBlue)
                size_no = obj.SIZE_NO;
            //else
            //    size_no = listSize.Items[listSize.SelectedIndex].ToString();
            int qty = int.Parse(strqty);
            int oldunFinishQty = unFinishQty;
            string scan_ip = IPUtil.GetIpAddress();
            string workson = obj.PRODUCTION_ORDER;
            string vArtNo = obj.ART_NO;
            string vSeDay = obj.SE_DAY;
            string vPO = obj.PO;
            try
            {
                daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                if (label12.BackColor == Color.CornflowerBlue)
                    unFinishQty = daySizeWorkQty - daySizeFinishQty;
                else
                    unFinishQty =GetReturnQty(listSizeSelectIndex);
                if (unFinishQty <Abs(qty)){
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
                    string msg = "排程变更，请刷新界面后再进行操作!";
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                    return;
                }
                //if (qty < 0)
                //{
                //    if (!InsertReturnSize(Abs(qty), se_id, vPO, d_dept, size_no, org_id))
                //    {
                //        MessageBox.Show("回退记录出错！");
                //    }
                //}
                //查询鞋面BOM资料是否存在。
                if (GetMaterial(workson) ==false)
                {
                    DialogResult dialog = MessageBox.Show("此工单未查询到鞋面BOM资料是否继续投产？", "温馨提示", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                    if (dialog == DialogResult.No)
                    {
                        return;
                    }                             
                }
                if (updateFinshQty(workson, size_no, qty, scan_ip, qrcode_id, DateTime.Now.ToString(), vArtNo, vSeDay, vPO))
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
                else
                {
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
                ReLoadWorkDaySize();
                //sjqdmsWorkDaySize = GetOrderSize(WorkOrder);

                //if (sjqdmsWorkDaySize.Count > 0)
                //{
                //    this.listSize.Items.Clear();
                //    foreach (var o in sjqdmsWorkDaySize)
                //    {
                //        this.listSize.Items.Add(o.SIZE_NO);
                //    }
                //}
                displayQtyButton(0);
            }
            //setSizeButtonBackColorToDefault();
            //clickButton.BackColor = Color.Blue;
            //textPo.Text = vPO;
            textSize.Text = obj.SIZE_NO;
            textQty.Text = strqty;
            TextQuerySeID.Text = "";
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

        private void reflashListSize()
        {
            listSize.Items.Remove(listSize.SelectedItem);
            sjqdmsWorkDaySize.RemoveAt(listSizeSelectIndex);
            listSizeSelectIndex = -1;
            listSize.SelectedIndex = listSizeSelectIndex;
        }

        private void butConfirm_Click(object sender, EventArgs e)
        {
            

            if (listSizeSelectIndex < 0)
            {
                MessageBox.Show("请选择制令/PO和SIZE", "错误！");
                return;
            }

            if ("".Equals(textEnterQty.Text.ToString()) || decimal.Parse(textEnterQty.Text.ToString()) == 0)
            {
                MessageBox.Show("请输入大于0的转移数量", "错误！");
                return;
            }
            daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
            if (label12.BackColor == Color.CornflowerBlue)
                unFinishQty = daySizeWorkQty - daySizeFinishQty;
            else
                unFinishQty = GetReturnQty(listSizeSelectIndex);

            int enterQty = int.Parse(textEnterQty.Text.ToString());

            if ((Abs(enterQty) > Abs(unFinishQty)))
            {
                MessageBox.Show("输入的数量大于可转移的数量", "错误！");
                return;
            }
            //else if ((Abs(enterQty) > int.Parse(textSizeReturnQty.Text)) && (label12.BackColor == Color.LightCoral))
            //{
            //    MessageBox.Show("输入的数量不可大于可退回数量", "错误！");
            //    return;
            //}

            VwSjqdmsWorkDaySize obj = sjqdmsWorkDaySize[listSizeSelectIndex];
            
            string size_no = "";
            if (label12.BackColor == Color.CornflowerBlue)
                size_no = obj.SIZE_NO;
            else if (label12.BackColor == Color.LightCoral)
                size_no = listSize.Items[listSize.SelectedIndex].ToString();
            int oldunFinishQty = unFinishQty;
            string scan_ip = IPUtil.GetIpAddress();
            string workson = obj.PRODUCTION_ORDER;
            string vArtNo = obj.ART_NO;
            string vSeDay = obj.SE_DAY;
            string vPO = obj.PO;

            DialogResult dr = MessageBox.Show("确认提交数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                ByClick = false;
                SetButtonEnable((Button)sender);
                try
                {
                    

                    //daySizeWorkQty = GetDaySizeWorkQty(listSizeSelectIndex);
                    //unFinishQty = daySizeWorkQty - daySizeFinishQty;
                    if (unFinishQty < Abs(enterQty))
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
                        string msg = "排程变更，请刷新界面后再进行操作!";
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                        return;
                    }

                    //if (enterQty < 0)
                    //{
                    //    if (!InsertReturnSize(Abs(enterQty), se_id, vPO, d_dept, size_no, org_id))
                    //    {
                    //        MessageBox.Show("回退记录出错！");
                    //    }
                    //}
                    //查询鞋面BOM资料是否存在。
                    if (GetMaterial(workson) == false)
                    {
                        DialogResult dialog = MessageBox.Show("此工单未查询到鞋面BOM资料是否继续投产？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.No)
                        {
                            return;
                        }
                    }
                    if (updateFinshQty(workson, size_no, enterQty, scan_ip, qrcode_id, DateTime.Now.ToString(), vArtNo, vSeDay, vPO))
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
                    else
                    {
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

                    sjqdmsWorkDaySize = GetOrderSize(WorkOrder);

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (!ByClick)
            {
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                    button.BackColor = button1.BackColor;
                }
            }
            else
            {
                button_qty.BackColor = button1.BackColor;
                for (int i = 1; i < 10; i++)
                {
                    Button button = (Button)Controls.Find("btn_c" + i, true)[0];
                    button.Enabled = true;
                }
            }
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        
        {
            textTime.Text = DateTime.Now.ToString();
        }

        private void btnBackOff_Click(object sender, EventArgs e)
        {
           
            //displayQtyButton(0);
            //listSize.Items.Clear();
            textEnterQty.Clear();
          

            if (label12.BackColor == Color.CornflowerBlue)
            {
                ReturnWorkDaySize();
                setSizeButtonBackColorToBackOff();
               
            }
            else if (label12.BackColor == Color.LightCoral)
            {
               
                setSizeButtonBackColorToDefault();
                //sjqdmsWorkDaySize = null;
                ReLoadWorkDaySize();
                //if (sjqdmsWorkDaySize == null)

                //    sjqdmsWorkDaySize = GetOrderSize(WorkOrder);

                //if (sjqdmsWorkDaySize.Count > 0)
                //{
                //    foreach (var o in sjqdmsWorkDaySize)
                //    {
                //        this.listSize.Items.Add(o.SIZE_NO);
                //    }
                //}
            }

          listSize_SelectedIndexChanged(sender, e);
          displayQtyButton(0);
        }

        private DataTable GetSeReturnSize(string WorkOrder)
        {
            DataTable dt = new DataTable();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("WorkOrder", WorkOrder);
            p.Add("vDDept", d_dept);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionInputOrderServer", "GetSeReturnSize", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
            label12.Text = "输入回退数量：";
        }

        private void textEnterQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '\b')
            {
                e.Handled = false;
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

        private void textPickWareHourse_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
            if (e.KeyCode==Keys.Enter&& textPickWareHourse.Text.Contains('|')) {
                textWareHourse.Text = textPickWareHourse.Text.ToUpper().Split(new Char[] { '|' })[0].Trim();
              
                
                textWareHouseName.Text = textPickWareHourse.Text.ToUpper().Split(new Char[] { '|' })[1].Trim();
                textPickWareHourse.Clear();
            }
           


        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
