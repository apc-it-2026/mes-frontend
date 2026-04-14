using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutocompleteMenuNS;
using NewExportExcels;
using MaterialSkin.Controls;
using SJeMES_Framework.Common;

namespace Ready_To_Load
{
    public partial class Treatment_Process : Form
    // public partial class Treatment_Process : MaterialForm
    {
        string scan_ip = GetIpAddress();
        DataTable sortfilterJson;
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        public Treatment_Process()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.Client, "", Program.Client.Language);
            this.WindowState = FormWindowState.Maximized;
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.MouseEnter += Button_MouseEnter;
                    button.MouseLeave += Button_MouseLeave;
                }
            }


            //autocompleteMenu1.Items = null;
            //autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);

            //  this.WindowState = FormWindowState.Maximized;

            // tabControl1.TabPages.Remove(tabPage4);

            //label3.Visible = false;
            //txt_component.Visible = false;

            //lbl_status.Visible = false;
            //txt_status_report.Visible = false;

        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.ForeColor = SystemColors.ControlText; 
            button.BackColor = SystemColors.Control; 
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;

            button.ForeColor = Color.Red; 
            button.BackColor = Color.Yellow;
        }

        private DataTable GetQR_Code_Data(string qr_Code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", qr_Code.ToString().ToUpper());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetQR_Code_Data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        private DataTable Get_Out_QR_Code_Data(string qr_Code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", qr_Code.ToString().ToUpper());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Get_Out_QR_Code_Data", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }


        private Boolean ParseQrCode(int length)
        {
            if (length == 12)
            {
                return true;
            }
            else
            {
                MessageBox.Show("The Length of the QR code is incorrect, Please Check Once", "Error!");
                return false;
            }
        }

        private void Text_qr_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable CheckCuttingLine = IsCuttingLIneorNot(lbl_text_line.Text.Trim());
                if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
                {
                    if (CheckCuttingLine.Rows[0]["PROCESS"].ToString() == "C")
                    {
                        InsertRTLInward(text_qr_code.Text.Trim());
                        text_qr_code.Text = "";
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Only Cutting Lines Allowed to Scan QR Code !");
                        text_qr_code.Text = "";
                        txt_out_qr_code.Text = "";
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "You are not Allowed to Scan !");
                }

            }
        }

        private DataTable IsCuttingLIneorNot(string Line)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("LINE", Line);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "CheckingCuttingOrNot", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }
        }

        private void InsertRTLInward(string qr_code)
        {

            //string qr_code = text_qr_code.Text;

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", qr_code);
            p.Add("IP_ADDRESS", scan_ip);

            DataTable get_qr_code = GetQR_Code_Data(qr_code);
            if (get_qr_code != null && get_qr_code.Rows.Count > 0)
            {
                string salesorder = get_qr_code.Rows[0]["SALES_ORDER"].ToString();
                //string qrcode = get_qr_code.Rows[0]["QR_CODE"].ToString();
                string order_no = get_qr_code.Rows[0]["ORDER_NO"].ToString();
                string line = get_qr_code.Rows[0]["LINE"].ToString();
                string master_po = get_qr_code.Rows[0]["MASTER_PO"].ToString();
                string article = get_qr_code.Rows[0]["ART"].ToString();
                string part_no = get_qr_code.Rows[0]["PART_NO"].ToString();
                string part_name = get_qr_code.Rows[0]["PART_NAME"].ToString();
                string size_no = get_qr_code.Rows[0]["SIZE_NO"].ToString();
                string l_qty = get_qr_code.Rows[0]["LEFT_QTY"].ToString();
                string r_qty = get_qr_code.Rows[0]["RIGHT_QTY"].ToString();
                string process = get_qr_code.Rows[0]["PROSS_NAME"].ToString();
                string vend_no = get_qr_code.Rows[0]["VEND_NO"].ToString();
                string vend_name = get_qr_code.Rows[0]["VEND_NAME"].ToString();
                string pur_order = get_qr_code.Rows[0]["PURCHASE_ORDER_NUMBER"].ToString();
                string shoe_name = get_qr_code.Rows[0]["SHOE_NAME"].ToString();
                //string org_id = get_qr_code.Rows[0]["RECEIVING_ORGID"].ToString() == "" ?"" : get_qr_code.Rows[0][" RECEIVING_ORGID"].ToString();
                string org_id = get_qr_code.Rows[0]["RECEIVING_ORGID"].ToString();

                //dtJson.Rows[i]["Target"].ToString() == "" ? 0 : decimal.Parse(dtJson.Rows[i]["Target"].ToString());

                p.Add("SALES_ORDER", salesorder);
                p.Add("ORDER_NO", order_no);
                p.Add("LINE", line);
                p.Add("MASTER_PO", master_po);
                p.Add("ART", article);
                p.Add("PART_NO", part_no);
                p.Add("PART_NAME", part_name);
                p.Add("SIZE_NO", size_no);
                p.Add("LEFT_QTY", l_qty);
                p.Add("RIGHT_QTY", r_qty);
                p.Add("PROSS_NAME", process);
                p.Add("VEND_NO", vend_no);
                p.Add("VEND_NAME", vend_name);
                p.Add("PURCHASE_ORDER_NUMBER", pur_order);
                p.Add("SHOE_NAME", shoe_name);
                p.Add("RECEIVING_ORGID", org_id);
            }
            else
            {
                MessageHelper.ShowErr(this, "No Data Found！");
                return;
            }

            bool isExistsQrcode = IsExist_QR_Code(qr_code); // Check the data already Exists or not

            if (isExistsQrcode == false)
            {
                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {

                    int count = int.Parse(retJson["RetData"].ToString());
                    if (count > 0)
                    {
                        GetRTL_Inward_Data(p, false, isExistsQrcode);
                        MessageHelper.ShowSuccess(this, "Success");
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
            else
            {
                MessageHelper.ShowErr(this, "Data Already Exsts!!");
            }


        }

        private void GetRTL_Inward_Data(Dictionary<string, object> Dictionary, bool data_load, bool isExistsQrcode = false)
        {

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetRTL_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(Dictionary));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    decimal? total = 0;
                    //decimal? total2 = 0;

                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        total += string.IsNullOrEmpty(dtJson.Rows[i]["LEFT_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["LEFT_QTY"].ToString());
                    }

                    dataGridView1.DataSource = dtJson;
                    lbl_Inward_Total_Qty.Text = Convert.ToString(total);

                }
            }
        }



        private void GetRTL_Outward_Data(Dictionary<string, object> Dictionary, bool data_load, bool isExistsQrcode = false)
        {

            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetRTL_Outward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(Dictionary));

            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                //DataTable dtJson2 = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(dtJson2);


                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                // DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    //var size = "Total";

                    decimal? total = 0;
                    //decimal? total2 = 0;

                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        total += string.IsNullOrEmpty(dtJson.Rows[i]["LEFT_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["LEFT_QTY"].ToString());
                        //total += dataGridView1.Rows[i].Cells[8].Value.ToString() == "" ? 0 : decimal.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                        //total2 += dataGridView1.Rows[i].Cells[9].Value.ToString() == "" ? 0 : decimal.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());
                    }


                    label6.Text = Convert.ToString(total);

                    //dtJson2.DefaultView.Sort = "starttime ASC";
                    dtJson = dtJson.DefaultView.ToTable();
                    dataGridView2.AutoGenerateColumns = false;
                    dataGridView2.DataSource = dtJson;



                }

            }


        }

        private bool IsExist_QR_Code(string Qr_Code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", Qr_Code);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "IsExist_QR_Code", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                bool isExists = JsonConvert.DeserializeObject<bool>(retJson["RetData"].ToString());
                return isExists;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }

        private void Treatment_Process_Load(object sender, EventArgs e)
        {

            Dictionary<string, object> parm = new Dictionary<string, object>();
            GetRTL_Inward_Data(parm, true);
            GetRTL_Outward_Data(parm, true);
            getLine_and_User();
            LoadOrgId();
            GetPlant();
            LoadStitchingDept();
            text_qr_code.Select();
            txt_status_report.SelectedItem = "IN";
        }


        public class ComboBoxData
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        private void LoadOrgId()
        {
            List<ComboBoxData> WMSorgEntries = new List<ComboBoxData> { };
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadOrgId", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                WMSorgEntries.Add(new ComboBoxData() { Code = "", Name = "" });
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    WMSorgEntries.Add(new ComboBoxData() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
                }

                cb_org.DataSource = WMSorgEntries;
                cb_org.DisplayMember = "Name";
                cb_org.ValueMember = "Code";


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadStitchingDept()
        {
            var items1 = new List<AutocompleteItem>();
            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "LoadStitchingDept", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                    for (int i = 1; i <= dtJson.Rows.Count; i++)
                    {
                        items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i - 1]["DEPARTMENT_CODE"].ToString()));
                    }
                }
                Stitching_Line_Cb.DataSource = items1;


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }


        private void GetPlant()
        {

            var items1 = new List<AutocompleteItem>();

            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadPlant", Program.Client.UserToken, string.Empty);


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["CODE"].ToString() }, dtJson.Rows[i - 1]["CODE"].ToString()));
                }
            }
            comboBox1.DataSource = items1;


        }

        private void getLine_and_User()
        {

            string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "QueryUserAndDept_Details", Program.Client.UserToken, string.Empty);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
            {

                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();

                //DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));

                if (dtJson.Rows.Count > 0)
                {
                    lbl_text_line.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    lbl_user.Text = dtJson.Rows[0]["STAFF_NO"].ToString();
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }

        private void Txt_out_qr_code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((string.IsNullOrEmpty(Stitching_Line_Cb.Text)) && (string.IsNullOrEmpty(Stitching_Line_Cb.Text)))
                {
                    MessageBox.Show("Please Select Delivery Stitching Line !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_out_qr_code.Text = "";
                    return;
                }

                DataTable CheckCuttingLine = IsCuttingLIneorNot(lbl_text_line.Text.Trim());
                if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
                {
                    if (CheckCuttingLine.Rows[0]["PROCESS"].ToString() == "C")
                    {
                        Outward_RTL_Data(txt_out_qr_code.Text.Trim());
                        txt_out_qr_code.Text = "";
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "Only Cutting Lines Allowed to Scan QR Code !");
                        text_qr_code.Text = "";
                        txt_out_qr_code.Text = "";
                        //text_qr_code.Enabled = false;
                        // txt_out_qr_code.Enabled = false;
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "You are not Allowed to Scan !");
                }



            }
        }

        private DataTable GetRecieptLineInfo(string master_po, string part_no, string size_no, string article, string salesorder, string dept_code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("MASTER_PO", master_po);
            p.Add("SIZE_NO", size_no);
            p.Add("PART_NO", part_no);
            p.Add("ARTICLE", article);
            p.Add("SALESORDER", salesorder);
            p.Add("DEPT_CODE", dept_code);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetRecieptLineInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }

        }



        private void Outward_RTL_Data(string qr_code)
        {

            DataTable lineinfo;

            //getLine_and_User();
            string dept_code = lbl_text_line.Text;
            string outWARD = "OUT";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", qr_code);
            p.Add("IP_ADDRESS", scan_ip);
            p.Add("OUT", outWARD);
            p.Add("DEPT_CODE", dept_code);
            p.Add("DELIVERY_STITCHING_LINE", Stitching_Line_Cb.Text);
            DataTable get_qr_code = Get_Out_QR_Code_Data(qr_code);
            // DataTable get_qr_code = GetQR_Code_Data(qr_code);
            if (get_qr_code != null && get_qr_code.Rows.Count > 0)
            {
                string salesorder = get_qr_code.Rows[0]["SALES_ORDER"].ToString();
                //string qrcode = get_qr_code.Rows[0]["QR_CODE"].ToString();
                string order_no = get_qr_code.Rows[0]["ORDER_NO"].ToString();
                string line = get_qr_code.Rows[0]["LINE"].ToString();
                string master_po = get_qr_code.Rows[0]["MASTER_PO"].ToString();
                string article = get_qr_code.Rows[0]["ART"].ToString();
                string part_no = get_qr_code.Rows[0]["PART_NO"].ToString();
                string part_name = get_qr_code.Rows[0]["PART_NAME"].ToString();
                string size_no = get_qr_code.Rows[0]["SIZE_NO"].ToString();
                string l_qty = get_qr_code.Rows[0]["LEFT_QTY"].ToString();
                string r_qty = get_qr_code.Rows[0]["RIGHT_QTY"].ToString();
                string process = get_qr_code.Rows[0]["PROSS_NAME"].ToString();
                string vend_no = get_qr_code.Rows[0]["VEND_NO"].ToString();
                string vend_name = get_qr_code.Rows[0]["VEND_NAME"].ToString();
                string pur_order = get_qr_code.Rows[0]["PURCHASE_ORDER_NUMBER"].ToString();
                string shoe_name = get_qr_code.Rows[0]["SHOE_NAME"].ToString();
                //string org_id = get_qr_code.Rows[0]["RECEIVING_ORGID"].ToString() == "" ?"" : get_qr_code.Rows[0][" RECEIVING_ORGID"].ToString();
                string org_id = get_qr_code.Rows[0]["RECEIVING_ORGID"].ToString();


                p.Add("SALES_ORDER", salesorder);
                p.Add("ORDER_NO", order_no);
                p.Add("LINE", line);
                p.Add("MASTER_PO", master_po);
                p.Add("ART", article);
                p.Add("PART_NO", part_no);
                p.Add("PART_NAME", part_name);
                p.Add("SIZE_NO", size_no);
                p.Add("LEFT_QTY", l_qty);
                p.Add("RIGHT_QTY", r_qty);
                p.Add("PROSS_NAME", process);
                p.Add("VEND_NO", vend_no);
                p.Add("VEND_NAME", vend_name);
                p.Add("PURCHASE_ORDER_NUMBER", pur_order);
                p.Add("SHOE_NAME", shoe_name);
                p.Add("RECEIVING_ORGID", org_id);

                lineinfo = GetRecieptLineInfo(master_po, part_no, size_no, article, salesorder, dept_code);


            }
            else
            {
                MessageHelper.ShowErr(this, "No Data Found！");
                return;
            }

            bool isExistsQrcode = IsExist_Out_QR_Code(qr_code); // Check the data already Exists or not

            if (isExistsQrcode == false)
            {
                if (!checkBox1.Checked)
                {
                    if (lineinfo == null)
                    {
                        MessageHelper.ShowErr(this, "No Receipt Scanning Record Found");
                    }

                    else if (lineinfo.Rows.Count > 0)
                    {
                        string recieptline = lineinfo.Rows[0]["LINE"].ToString();

                        if (dept_code == recieptline)
                        {

                            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Outward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                            if (Convert.ToBoolean(retJson["IsSuccess"]))
                            {

                                int count = int.Parse(retJson["RetData"].ToString());
                                if (count > 0)
                                {
                                    GetRTL_Outward_Data(p, false, isExistsQrcode);
                                    MessageHelper.ShowSuccess(this, "Success");
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
                        else
                        {
                            SJeMES_Control_Library.MessageHelper.ShowErr(this, "Your not Allowed to Scan using This Line");
                        }
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, "No Data Found");
                    }

                }
                else
                {
                    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Outward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                    if (Convert.ToBoolean(retJson["IsSuccess"]))
                    {

                        int count = int.Parse(retJson["RetData"].ToString());
                        if (count > 0)
                        {
                            GetRTL_Outward_Data(p, false, isExistsQrcode);
                            MessageHelper.ShowSuccess(this, "Success");
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

                //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", " ", Program.Client.UserToken, JsonConvert.SerializeObject(p));



            }
            else
            {
                MessageHelper.ShowErr(this, "Data Already Exsts!!");
            }



        }


        private bool IsExist_Out_QR_Code(string Qr_Code)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", Qr_Code);
            string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "IsExist_Out_QR_Code", Program.Client.UserToken, JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                bool isExists = JsonConvert.DeserializeObject<bool>(retJson["RetData"].ToString());
                return isExists;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return false;
            }
        }

        private DataTable Get_Inventory_Info(string out_qrcode)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("QR_CODE", out_qrcode.ToString().ToUpper());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Get_Inventory_Info", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                return dt;
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                return null;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string INVENTORYstatus = "";
            dataGridView3.DataSource = null;
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("ORG_ID", string.IsNullOrWhiteSpace(cb_org.Text) ? cb_org.Text : cb_org.SelectedValue.ToString());
            p.Add("PLANT", string.IsNullOrWhiteSpace(comboBox1.Text) ? comboBox1.Text :  comboBox1.SelectedValue.ToString());
            p.Add("ART", txt_article_reports.Text);
            p.Add("SALESORDER", txt_so_reports.Text);
            p.Add("LINE", textBox1.Text.ToUpper().Trim());
            p.Add("PO", txt_cpo_reports.Text);
            p.Add("FROM_DATE", From_Date.Text);
            p.Add("TO_DATE", To_Date.Text);
            if (txt_status_report.Text == "INVENTORY")
            {
                p.Add("STATUS", INVENTORYstatus);
            }
            else
            {
                p.Add("STATUS", txt_status_report.Text);
            }

            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Get_Rtl_In_Out_Reports", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            string Json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(Json, (typeof(DataTable)));
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {


                decimal total_size_qty = 0;
                decimal inqty = 0;
                decimal outqty = 0;
                decimal balance = 0;
                Cursor.Current = Cursors.Default;
               
                dataGridView3.DataSource = dtJson;
               
                //dataGridView3.DefaultCellStyle.BackColor = Color.Azure;

                if (dtJson != null && dtJson.Rows.Count > 0)
                {

                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        //total_size_qty += string.IsNullOrEmpty(dtJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["SIZE_QTY"].ToString());

                        //if(dtJson.Rows[i]["STATUS"].ToString() == "IN" )
                        if (txt_status_report.Text == "IN")
                        {
                            total_size_qty += string.IsNullOrEmpty(dtJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["SIZE_QTY"].ToString());
                            inqty += string.IsNullOrEmpty(dtJson.Rows[i]["IN_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["IN_QTY"].ToString());
                        }
                        else if (txt_status_report.Text == "OUT")
                        {
                            total_size_qty += string.IsNullOrEmpty(dtJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["SIZE_QTY"].ToString());
                            outqty += string.IsNullOrEmpty(dtJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["OUT_QTY"].ToString());
                        }
                        else
                        {
                            total_size_qty += string.IsNullOrEmpty(dtJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["SIZE_QTY"].ToString());
                            inqty += string.IsNullOrEmpty(dtJson.Rows[i]["IN_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["IN_QTY"].ToString());
                            outqty += string.IsNullOrEmpty(dtJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["OUT_QTY"].ToString());
                            balance += string.IsNullOrEmpty(dtJson.Rows[i]["BALANCE"].ToString()) ? 0 : decimal.Parse(dtJson.Rows[i]["BALANCE"].ToString());


                            int in_check_qty = string.IsNullOrEmpty(dtJson.Rows[i]["IN_QTY"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["IN_QTY"].ToString());
                            int out_check_qty = string.IsNullOrEmpty(dtJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : int.Parse(dtJson.Rows[i]["OUT_QTY"].ToString());

                            if (in_check_qty > out_check_qty)
                            {
                                dataGridView3.Rows[i].Cells["BALANCE"].Style.BackColor = Color.ForestGreen;
                                dataGridView3.Rows[i].Cells["BALANCE"].Style.ForeColor = Color.White;

                            }
                            else
                            {
                                dataGridView3.Rows[i].Cells["BALANCE"].Style.BackColor = Color.LightPink;
                            }



                        }
                    }

                    DataRow row = dtJson.NewRow();

                    if (txt_status_report.Text == "IN")
                    {
                        row["SIZE_QTY"] = total_size_qty;
                        row["IN_QTY"] = inqty;
                        dataGridView3.Columns[0].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";
                    }

                    else if (txt_status_report.Text == "OUT")
                    {
                        row["SIZE_QTY"] = total_size_qty;
                        row["OUT_QTY"] = outqty;
                        dataGridView3.Columns[0].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";
                    }

                    else
                    {
                        row["SIZE_QTY"] = total_size_qty;
                        row["IN_QTY"] = inqty;
                        row["OUT_QTY"] = outqty;
                        row["BALANCE"] = balance;
                    }
                    dtJson.Rows.Add(row);




                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                    //SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    dataGridView3.DataSource = null;
                }
            }
            else
            {
                if (dtJson == null || dtJson.Rows.Count <= 0)
                {

                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                    dataGridView3.DataSource = null;

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    dataGridView3.DataSource = null;
                }

            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            cb_org.Text = "";
            txt_article_reports.Text = "";
            txt_so_reports.Text = "";


            //txt_status_report.ResetText();
            //txt_status_report.Text = "";
            txt_status_report.SelectedIndex = -1;

            comboBox1.Text = "";
            textBox1.Text = "";
            txt_cpo_reports.Text = "";
            From_Date.Text = "";
            To_Date.Text = "";
            dataGridView3.DataSource = null;
        }

        private void TxtFromDate_ValueChanged(object sender, EventArgs e)
        {
            From_Date.Text = TxtFromDate.Value.ToString("yyyy/MM/dd");
        }

        private void Txt_To_Date_ValueChanged(object sender, EventArgs e)
        {
            To_Date.Text = Txt_To_Date.Value.ToString("yyyy/MM/dd");
        }

  
        private void Txt_ExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "SuperMarket Information";
                ExportExcels.Export(a, dataGridView3);
            }
        }

        private void Btn_art_search_Click(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;

            string Article = txt_article.Text.Trim();
            string PO = txt_po.Text;
            if ((string.IsNullOrEmpty(Article)) && (string.IsNullOrEmpty(PO)))
            {
                MessageBox.Show("Please Enter Article or PO !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (string.IsNullOrEmpty(PO))
            //{
            //    MessageBox.Show("Please Enter Article or PO !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("ARTICLE", txt_article.Text);
            d.Add("PO", PO);
            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetArtWise_Component_Reports", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(ret)["RetData"].ToString();
                DataTable dtJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                //  DataTable dtJson = (DataTable)SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Cursor.Current = Cursors.Default;
                if (dtJson == null || dtJson.Rows.Count <= 0)
                {

                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                    dataGridView4.DataSource = null;

                }
                else
                {

                    dataGridView4.DataSource = dtJson;
                    //dataGridView4.ColumnHeadersBorderStyle = 
                    if ((string.IsNullOrEmpty(txt_article.Text) && !string.IsNullOrEmpty(txt_po.Text)) || (!string.IsNullOrEmpty(txt_article.Text) && !string.IsNullOrEmpty(txt_po.Text)))
                    {
                        SetDgvView1();
                    }
                    else
                    {
                        SetDgvView();
                    }


                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView4.DataSource = null;
            }


        }

        private void SetDgvView()
        {
            int col_count = dataGridView4.Columns.Count;
            for (int k = 0; k < dataGridView4.Rows.Count; k++)
            {
                string row_type = dataGridView4.Rows[k].Cells["ROW_TYPE"].Value == null ? "" : dataGridView4.Rows[k].Cells["ROW_TYPE"].Value.ToString().Trim();
                string art_no = dataGridView4.Rows[k].Cells["ARTICLE"].Value == null ? "" : dataGridView4.Rows[k].Cells["ARTICLE"].Value.ToString().Trim();

                if ("Z".Equals(row_type))
                {
                    dataGridView4.Rows[k].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                }

                decimal qty = 0;
                for (int i = 5; i < col_count - 1; i++)
                {
                    string cell_qty = dataGridView4.Rows[k].Cells[i].Value == null ? "" : dataGridView4.Rows[k].Cells[i].Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(cell_qty))
                    {
                        qty += decimal.Parse(cell_qty);
                    }
                }
                dataGridView4.Rows[k].Cells[col_count - 1].Value = qty.ToString();
            }
            //dgv_list1.Columns.RemoveAt(1);
            //dgv_list1.Columns.RemoveAt(0);
            dataGridView4.Columns[0].Visible = false;
            // dataGridView4.Columns[2].Visible = false;
            // dataGridView4.Columns[3].Visible = false;
            txt_article.Text = "";
            dataGridView4.Columns[0].HeaderCell.Style.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        }



        private void SetDgvView1()
        {
            int col_count = dataGridView4.Columns.Count;
            for (int k = 0; k < dataGridView4.Rows.Count; k++)
            {
                string row_type = dataGridView4.Rows[k].Cells["ROW_TYPE"].Value == null ? "" : dataGridView4.Rows[k].Cells["ROW_TYPE"].Value.ToString().Trim();
                string art_no = dataGridView4.Rows[k].Cells["ARTICLE"].Value == null ? "" : dataGridView4.Rows[k].Cells["ARTICLE"].Value.ToString().Trim();

                if ("Z".Equals(row_type))
                {
                    dataGridView4.Rows[k].DefaultCellStyle.BackColor = Color.MediumSeaGreen;
                }

                decimal qty = 0;
                for (int i = 6; i < col_count - 1; i++)
                {
                    string cell_qty = dataGridView4.Rows[k].Cells[i].Value == null ? "" : dataGridView4.Rows[k].Cells[i].Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(cell_qty))
                    {
                        qty += decimal.Parse(cell_qty);
                    }
                }
                dataGridView4.Rows[k].Cells[col_count - 1].Value = qty.ToString();
            }
            //dgv_list1.Columns.RemoveAt(1);
            //dgv_list1.Columns.RemoveAt(0);
            dataGridView4.Columns[0].Visible = false;
            //dataGridView4.Columns[2].Visible = false;
            //dataGridView4.Columns[3].Visible = false;
            txt_article.Text = "";
            txt_po.Text = "";
            //dataGridView4.Columns[0].HeaderCell.Style.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        }


        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                // Set focus to TextBox in TabPage1
                text_qr_code.Focus();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                // Set focus to TextBox in TabPage2
                txt_out_qr_code.Focus();
            }
            //else if (tabControl1.SelectedTab == tabPage3)
            //{
            //    // Set focus to TextBox in TabPage3
            //    txt_out_qr_code.Focus();
            //}
            else if (tabControl1.SelectedTab == tabPage4)
            {
                // Set focus to TextBox in TabPage4
                txt_article.Focus();
            }
        }

        private void Txt_article_TextChanged(object sender, EventArgs e)
        {
            var items1 = new List<AutocompleteItem>();
            var columnWidth = new int[] { 350 };
            int n = 0;
            DataTable dtJson;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("article", this.txt_article.Text);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetArtListByLikeQuery", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i]["ART_NO"].ToString() + "  |  " + dtJson.Rows[i]["ART_NAME"].ToString() }, "") { ColumnWidth = columnWidth, ImageIndex = n });
                    n++;
                }
            }
            AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
            autocompleteMenu2.MaximumSize = new Size(250, 350);
            autocompleteMenu2.SetAutocompleteMenu(txt_article, autocompleteMenu2);
            autocompleteMenu2.SetAutocompleteItems(items1);

        }

        private void Button3_Click(object sender, EventArgs e)
        {

            if (dataGridView4.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Article Inventory Information";
                ExportExcels.Export(a, dataGridView4);
            }
        }

        private void CheckBox1_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to Proceed ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void Btn_Matching_Search_Click(object sender, EventArgs e)
        {
            advancedDataGridView1.DataSource = null;

            string salesorder = txt_Matching_SO.Text.Trim();
            string PO = txt_Matching_PO.Text;
            if ((string.IsNullOrEmpty(salesorder)) && (string.IsNullOrEmpty(PO)))
            {
                MessageBox.Show("Please Enter SalesOrder or PO !", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dictionary<string, Object> d = new Dictionary<string, object>();
            d.Add("SALESORDER", salesorder);
            d.Add("PO", PO);
            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Get_Component_Matching_Reports", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Object>>(ret)["RetData"].ToString();
                sortfilterJson = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                // DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Cursor.Current = Cursors.Default;
                if (sortfilterJson == null || sortfilterJson.Rows.Count <= 0)
                {

                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "! No Data Found");
                    advancedDataGridView1.DataSource = null;

                }
                else
                {
                    advancedDataGridView1.DataSource = sortfilterJson.DefaultView;
                    advancedDataGridView1.Update();

                    // Color Settings
                    #region
                    //for (int i = 0; i < sortfilterJson.Rows.Count; i++)
                    //{
                    //    //  In_Qty Color Settings

                    //    int columnIndex = 6; // Column index is 0-based
                    //    int columnIndex1 = 7;
                    //    int columnIndex2 = 8;
                    //    int columnIndex3 = 9;

                    //    advancedDataGridView1.Rows[i].Cells[columnIndex].Style.BackColor = Color.FromArgb(51, 204, 255);


                    //   // advancedDataGridView1.Rows[i].Cells["Column7"].Style.BackColor = Color.FromArgb(51, 204, 255);

                    //    //dataGridView5.Rows[i].Cells["SIZE_QTY"].Style.BackColor = Color.LightSkyBlue;

                    //    decimal size_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()));

                    //    decimal in_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["IN_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["IN_QTY"].ToString()));
                    //    decimal out_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["OUT_QTY"].ToString()));

                    //    string status = sortfilterJson.Rows[i]["STATUS"].ToString();

                    //    if (size_qty == in_qty)
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex1"].Style.BackColor = Color.LimeGreen;
                    //        // dataGridView5.Rows[i].Cells["IN_QTY"].Style.BackColor = Color.LightCoral;
                    //    }
                    //    else if (size_qty < in_qty)
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex1"].Style.BackColor = Color.LightCoral;
                    //    }
                    //    else
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex1"].Style.BackColor = Color.Yellow;
                    //    }

                    //    if (size_qty == out_qty)
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex2"].Style.BackColor = Color.LimeGreen;
                    //        // dataGridView5.Rows[i].Cells["IN_QTY"].Style.BackColor = Color.LightCoral;
                    //    }
                    //    else if (size_qty < out_qty)
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex2"].Style.BackColor = Color.LightCoral;
                    //    }
                    //    else
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex2"].Style.BackColor = Color.Yellow;
                    //    }

                    //    if (status == "Matched")
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex3"].Style.BackColor = Color.LimeGreen;
                    //    }
                    //    else if (status == "Not Matched")
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex3"].Style.BackColor = Color.Yellow;
                    //    }
                    //    else
                    //    {
                    //        advancedDataGridView1.Rows[i].Cells["columnIndex3"].Style.BackColor = Color.LightCoral;
                    //    }
                    //}



                    #endregion

                    setbackGroundColors();
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                dataGridView4.DataSource = null;
            }

        }


        private void setbackGroundColors()
        {
            for (int i = 0; i < sortfilterJson.Rows.Count; i++)
            {
                // In_Qty Color Settings
                int columnIndex = 7; // Column index is 0-based
                int columnIndex1 = 8;
                int columnIndex2 = 9;
                int columnIndex3 = 10;

                


                // Set background color for Column7
                advancedDataGridView1.Rows[i].Cells[columnIndex].Style.BackColor = Color.FromArgb(51, 204, 255);

                // Retrieve values for comparison
                decimal size_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()));
                decimal in_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["IN_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["IN_QTY"].ToString()));
                decimal out_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["OUT_QTY"].ToString()));
                string status = sortfilterJson.Rows[i]["STATUS"].ToString();

                // Set background color for Column8 (IN_QTY)
                if (size_qty == in_qty)
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex1].Style.BackColor = Color.LimeGreen;
                }
                else if (size_qty < in_qty)
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex1].Style.BackColor = Color.LightCoral;
                }
                else
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex1].Style.BackColor = Color.Yellow;
                }

                // Set background color for Column9 (OUT_QTY)
                if (size_qty == out_qty)
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex2].Style.BackColor = Color.LimeGreen;
                }
                else if (size_qty < out_qty)
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex2].Style.BackColor = Color.LightCoral;
                }
                else
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex2].Style.BackColor = Color.Yellow;
                }

                // Set background color for Column10 (STATUS)
                if (status == "Matched")
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex3].Style.BackColor = Color.LimeGreen;
                }
                else if (status == "Not Matched")
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex3].Style.BackColor = Color.Yellow;
                }
                else
                {
                    advancedDataGridView1.Rows[i].Cells[columnIndex3].Style.BackColor = Color.LightCoral;
                }
            }
        }

        #region
        //private void setbackGroundColorsForFilters()
        //{



        //    // Check if the data source is null or empty
        //    if (sortfilterJson == null || sortfilterJson.Rows.Count == 0)
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
        //        return;
        //    }


        //    // Loop through each row in the data source
        //    for (int i = 0; i < sortfilterJson.Rows.Count; i++)
        //    {
        //        // Check if the current row index is valid
        //        if (i >= advancedDataGridView1.Rows.Count)
        //            return;

        //        // Set background colors based on conditions
        //        // Adjust column indices as needed
        //        advancedDataGridView1.Rows[i].Cells[7].Style.BackColor = Color.FromArgb(51, 204, 255); // Column7

        //        decimal size_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["SIZE_QTY"].ToString()));
        //        decimal in_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["IN_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["IN_QTY"].ToString()));
        //        decimal out_qty = string.IsNullOrEmpty(sortfilterJson.Rows[i]["OUT_QTY"].ToString()) ? 0 : Math.Round(decimal.Parse(sortfilterJson.Rows[i]["OUT_QTY"].ToString()));
        //        string status = sortfilterJson.Rows[i]["STATUS"].ToString();

        //        if (i >= advancedDataGridView1.Rows.Count)
        //            return;

        //        if (size_qty == in_qty)
        //        {
        //            advancedDataGridView1.Rows[i].Cells[8].Style.BackColor = Color.LimeGreen; // Column8
        //        }
        //        else if (size_qty < in_qty)
        //        {
        //            advancedDataGridView1.Rows[i].Cells[8].Style.BackColor = Color.LightCoral; // Column8
        //        }
        //        else
        //        {
        //            advancedDataGridView1.Rows[i].Cells[8].Style.BackColor = Color.Yellow; // Column8
        //        }

        //        if (size_qty == out_qty)
        //        {
        //            advancedDataGridView1.Rows[i].Cells[9].Style.BackColor = Color.LimeGreen; // Column9
        //        }
        //        else if (size_qty < out_qty)
        //        {
        //            advancedDataGridView1.Rows[i].Cells[9].Style.BackColor = Color.LightCoral; // Column9
        //        }
        //        else
        //        {
        //            advancedDataGridView1.Rows[i].Cells[9].Style.BackColor = Color.Yellow; // Column9
        //        }

        //        if (status == "Matched")
        //        {
        //            advancedDataGridView1.Rows[i].Cells[10].Style.BackColor = Color.LimeGreen; // Column10
        //        }
        //        else if (status == "Not Matched")
        //        {
        //            advancedDataGridView1.Rows[i].Cells[10].Style.BackColor = Color.Yellow; // Column10
        //        }
        //        else
        //        {
        //            advancedDataGridView1.Rows[i].Cells[10].Style.BackColor = Color.LightCoral; // Column10
        //        }
        //    }


        //}

        #endregion



        private void Btn_Matching_Export_Click(object sender, EventArgs e)
        {
            if (advancedDataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Treatment Matching Report";
                ExportExcels.Export(a, advancedDataGridView1);
            }
        }

        private void Panel5_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.Yellow, ButtonBorderStyle.Inset);
        }

        private void Panel7_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, p.DisplayRectangle, Color.PaleVioletRed, ButtonBorderStyle.Inset);
        }

        private void DataGridView4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdvancedDataGridView1_SortStringChanged(object sender, EventArgs e)
        {
            //this.advancedDataGridView1.DataSource = sortfilterJson;
            sortfilterJson.DefaultView.Sort = advancedDataGridView1.SortString; ;
            //setbackGroundColorsForFilters();

        }

        private void AdvancedDataGridView1_FilterStringChanged(object sender, EventArgs e)
        {
            //this.advancedDataGridView1.DataSource = sortfilterJson;

            sortfilterJson.DefaultView.RowFilter = advancedDataGridView1.FilterString;
            // setbackGroundColorsForFilters();


        }

        private void Btn_Matching_Clear_Click(object sender, EventArgs e)
        {
            txt_Matching_SO.Text = null;
            txt_Matching_PO.Text = null;
            advancedDataGridView1.DataSource = null;
        }






        #region

        //private void Stitching_Line_TextChanged(object sender, EventArgs e)
        //{
        //    AutocompleteMenuNS.AutocompleteMenu autocompleteMenu2 = new AutocompleteMenuNS.AutocompleteMenu();
        //    autocompleteMenu2.Items = null;
        //    autocompleteMenu2.MaximumSize = new Size(200, 350);
        //    var columnWidth = new[] { 50, 350 };
        //    DataTable dt = GetAllDepts();
        //    int n = 1;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        //autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"] + " " + dt.Rows[i]["DEPARTMENT_NAME"] }, dt.Rows[i]["DEPARTMENT_CODE"] + "|" + dt.Rows[i]["DEPARTMENT_NAME"]) { ColumnWidth = columnWidth, ImageIndex = n });
        //        n++;
        //    }
        //    autocompleteMenu2.SetAutocompleteMenu(Stitching_Line, autocompleteMenu2);
        //}


        //private DataTable GetAllDepts()
        //{
        //    DataTable dt = new DataTable();
        //    string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, JsonConvert.SerializeObject(string.Empty));
        //    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        dt = JsonHelper.GetDataTableByJson(json);
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }

        //    return dt;
        //}

        //private void Stitching_Line_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        string dept = Stitching_Line.Text.Trim().ToUpper().Split('|')[0];
        //        Stitching_Line.Text = dept;
        //    }
        //}


        #endregion
        #region  Dummy Code

        //private void AdvancedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
        //    if (e.RowIndex < 1 || e.ColumnIndex < 0)
        //        return;
        //    if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
        //    {
        //        e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;

        //        // Set the alignment of the text to center
        //        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    }
        //    else
        //    {
        //        e.AdvancedBorderStyle.Top = advancedDataGridView1.AdvancedCellBorderStyle.Top;
        //    }
        //}

        #endregion
        #region  Another Code For  IsTheSameCellValue Method

        //bool IsTheSameCellValue(int column, int row)
        //{

        //    // Specify the column indices for cell 4 and cell 7
        //    int cell3ColumnIndex = 3; // Assuming cell 4 is at column index 2 (zero-based index)
        //    int cell6ColumnIndex = 6; // Assuming cell 7 is at column index 5 (zero-based index)

        //    // Check if the current column is cell 4 or cell 7
        //    if (column != cell3ColumnIndex && column != cell6ColumnIndex)
        //    {
        //        // If not, return false
        //        return false;
        //    }

        //    DataGridViewCell cell1 = advancedDataGridView1[column, row];
        //    DataGridViewCell cell2 = advancedDataGridView1[column, row - 1];

        //    if (cell1.Value == null || cell2.Value == null)
        //    {
        //        return false;
        //    }
        //    return cell1.Value.ToString() == cell2.Value.ToString();

        //}

        #endregion

        private void AdvancedDataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.FormattingApplied = true;
            }

        }
        bool IsTheSameCellValue(int column, int row)
        {
            // Specify the column indices for cell 4 and cell 7 and cell 8

            int cell4ColumnIndex = 3; // Assuming cell 4 is at column index 3 (zero-based index)
            int cell7ColumnIndex = 5; 
            int cell8ColumnIndex = 7;
            int cell12ColumnIndex = 11;

            // Check if the current column is either cell 4 or cell 7
            if (column == cell4ColumnIndex || column == cell7ColumnIndex || column == cell8ColumnIndex || column == cell12ColumnIndex)
            {
                // Get the current and previous cells for comparison
                DataGridViewCell cell1 = advancedDataGridView1[column, row];
                DataGridViewCell cell2 = advancedDataGridView1[column, row - 1];

                // Check if either cell is null
                if (cell1.Value == null || cell2.Value == null)
                {
                    return false;
                }

                // Compare the values of the current and previous cells
                return cell1.Value.ToString() == cell2.Value.ToString();
            }

            // If the current column is neither cell 4 nor cell 7 and 8, return false
            return false;
        }



        private void AdvancedDataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                e.AdvancedBorderStyle.Top = advancedDataGridView1.AdvancedCellBorderStyle.Top;
            }
        }

        private void Txt_status_report_SelectedValueChanged(object sender, EventArgs e)
        {
            if(txt_status_report.Text == "INVENTORY")
            {

                cb_org.Text = null;
                From_Date.Text = null;
                To_Date.Text = null;
                comboBox1.Text = null;
                textBox1.Text = null;

                textBox1.Enabled = false;
                TxtFromDate.Enabled = false;
                Txt_To_Date.Enabled = false;
                From_Date.Enabled = false;
                To_Date.Enabled = false;
                comboBox1.Enabled = false;
                cb_org.Enabled = false;
                

            }
            else
            {
                textBox1.Enabled = true;
                TxtFromDate.Enabled = true;
                Txt_To_Date.Enabled = true;
                From_Date.Enabled = true;
                To_Date.Enabled = true;
                comboBox1.Enabled = true;
                cb_org.Enabled = true;
            }
        }






    }
}