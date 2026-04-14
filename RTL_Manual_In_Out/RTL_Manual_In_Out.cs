using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using SJeMES_Framework.WebAPI;
using System.Threading.Tasks;

namespace RTL_Manual_In_Out
{
    public partial class RTL_Manual_In_Out : MaterialForm
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

        public RTL_Manual_In_Out()
        {
            InitializeComponent();

            //this.WindowState = FormWindowState.Maximized;

            Timer timer1 = new Timer();
            timer1.Interval = 1000; // Set interval to 1 second
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {

            lbl_date_info.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt");  // tt shows 12 hr format time
            lbl_date_out.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss tt");  // tt shows 12 hr format time
        }
        private void Btn_Masterpo_Search_Click(object sender, EventArgs e)
        {

            if ((string.IsNullOrEmpty(txt_Master_Po.Text)) && (string.IsNullOrEmpty(txt_Master_Po.Text)))
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Enter Master PO ! ", Program.Client, "", Program.Client.Language);
                MessageBox.Show(msg, "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clearAllData();

            Cursor.Current = Cursors.WaitCursor;

            LoadMasterPOData();
            LoadComponentsInfo();

            Cursor.Current = Cursors.Default;
        }

        private DataTable GetDepts()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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
        private void LoadDepts()
        {
            var columnWidth = new int[] { 30, 250 };
            DataTable dt = GetDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + "   " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;

            }
        }

        private void LoadComponentsInfo()
        {
            DataTable dtJson = null;
            comboBox1.Items.Clear();
            List<string> partList1 = new List<string>();
            //var items1 = new List<AutocompleteItem>();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("MasterPO", txt_Master_Po.Text);
            partList1.Clear();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "LoadComponentsInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "LoadComponentsInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    partList1.Add("");
                    //items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                    if (dtJson.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtJson.Rows.Count - 1; i++)
                        {
                            partList1.Add(dtJson.Rows[i]["PART_NO"].ToString().Trim() + "   " + dtJson.Rows[i]["PART_NAME"].ToString().Trim());
                            // items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["PART_NO"].ToString(),"  ", dtJson.Rows[i - 1]["PART_NAME"].ToString() }, dtJson.Rows[i - 1]["PART_NAME"].ToString()));

                        }
                        comboBox1.Items.AddRange(partList1.ToArray());
                        txt_order_no.Text = dtJson.Rows[0]["ORDER_NO"].ToString();
                        txt_order_no.BackColor = System.Drawing.SystemColors.Window;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "");
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        private void LoadMasterPOData()
        {

            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("udf", txt_Master_Po.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkOrderOperationServer", "GetOrderInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);
                //if (dic["DataTable"].Rows.Count > 0)
                //    dataGridView1.DataSource = dic["DataTable"];

                DataTable dt = dic["DataTable_info"];
                if (dt.Rows.Count > 0)
                {
                    txt_article.Text = dt.Rows[0]["art"].ToString();//ART
                    txt_model.Text = dt.Rows[0]["鞋款名称"].ToString();//shoe name
                    txt_salesorder.Text = dt.Rows[0]["SALES_ORDER"].ToString();//sales order
                    txt_po.Text = dt.Rows[0]["po"].ToString();//PO
                    // textBox7.Text = dt.Rows[0]["DATE_FINISH_PLAN"].ToString();//Estimated Completion Date


                    txt_article.BackColor = System.Drawing.SystemColors.Window;
                    txt_model.BackColor = System.Drawing.SystemColors.Window;
                    txt_salesorder.BackColor = System.Drawing.SystemColors.Window;
                    txt_po.BackColor = System.Drawing.SystemColors.Window;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");             // New Code
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void clearAllData()
        {

            //dataGridView1.DataSource = null;
            //dataGridView1.Columns["Column6"].Visible = false;

            comboBox1.Items.Clear();
            txt_salesorder.Text = null;
            txt_po.Text = null;
            txt_article.Text = null;
            txt_model.Text = null;
            txt_order_no.Text = null;

        }

        private void Btn_receipt_save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Select Component ! ", Program.Client, "", Program.Client.Language);
                MessageHelper.ShowErr(this, msg);
                return;
            }

            DialogResult res = MessageBox.Show("Are you sure you want to Save ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {

                DataTable CheckCuttingLine = IsCuttingLineorNot(lblline_info.Text.Trim());
                if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
                {
                    if (CheckCuttingLine.Rows[0]["PROCESS"].ToString() == "C")
                    {
                        Dictionary<string, Object> p = new Dictionary<string, object>();
                        p.Add("SalesOrder", txt_salesorder.Text);
                        p.Add("PO", txt_po.Text);
                        p.Add("Article", txt_article.Text);
                        //p.Add("Part_No", part_no);
                        p.Add("Line", lblline_info.Text);
                        p.Add("User", lbl_user_info.Text);
                        p.Add("OrderNo", txt_order_no.Text);
                        p.Add("IN", "IN");
                        p.Add("QR_Code", "2222");   //2222 Indicates Manual Operation
                        MessageHelper.ShowSuccess(this, " Data Saved Successfully !");
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Only Cutting Lines Allowed to input the Data !");
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, "You are not Allowed to Scan , Please Contact Administrator! ");
                }

            }


        }
        private DataTable IsCuttingLineorNot(string Line)
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


        private void RTL_Manual_In_Out_Load(object sender, EventArgs e)
        {
            getLine_and_User();
            LoadDepts();
            //LoadOrgId();
            // GetPlant();
            LoadMasterPO();
            LoadStitchingDept();
            btn_receipt_save.Visible = false;
            button3.Visible = false;
            cb_stitching_Line.Visible = false;
            tabControl1.TabPages.Remove(tabPage3);
        }
        private DataTable GetMasterPO()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.GeneralServer", "GetMasterPO", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                 dt  = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }
        private void LoadMasterPO()
        {
            var columnWidth = new int[] { 30, 250 };
            DataTable dt = GetMasterPO();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["UDF01"].ToString() + "   " + dt.Rows[i]["SALES_ORDER"].ToString() }, dt.Rows[i]["UDF01"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;

            }
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
                    lblline_info.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    lbl_user_info.Text = dtJson.Rows[0]["STAFF_NO"].ToString();

                    lblline_info_out.Text = dtJson.Rows[0]["STAFF_DEPARTMENT"].ToString();
                    lbl_user_info_out.Text = dtJson.Rows[0]["STAFF_NO"].ToString();
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }


        //private void LoadOrgId()
        //{
        //    List<ComboBoxData> WMSorgEntries = new List<ComboBoxData> { };
        //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadOrgId", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
        //    if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //    {
        //        string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
        //        DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
        //        WMSorgEntries.Add(new ComboBoxData() { Code = "", Name = "" });
        //        for (int i = 0; i < dtJson.Rows.Count; i++)
        //        {
        //            WMSorgEntries.Add(new ComboBoxData() { Code = dtJson.Rows[i]["ORG_CODE"].ToString(), Name = dtJson.Rows[i]["ORG_NAME"].ToString() });
        //        }

        //        cb_org.DataSource = WMSorgEntries;
        //        cb_org.DisplayMember = "Name";
        //        cb_org.ValueMember = "Code";


        //    }
        //    else
        //    {
        //        SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //    }
        //}



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
                cb_stitching_Line.DataSource = items1;

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["ErrMsg"].ToString());
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            clearAllData();
            txt_Master_Po.Text = null;
        }

        private void ComboBox2_SelectedValueChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedItem == null || comboBox2.SelectedItem == "")
            {
                MessageHelper.ShowErr(this, "Please Select Component !");
                return;
            }

            string part_no = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("MasterPO", textBox5.Text);
            p.Add("SalesOrder", textBox4.Text);
            p.Add("PO", textBox3.Text);
            p.Add("Article", textBox2.Text);
            p.Add("Part_No", part_no);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetoutComponentRoutingInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                dataGridView2.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Parse the required columns to decimal
                    if (decimal.TryParse(dt.Rows[i]["SIZE_QTY"].ToString(), out decimal size_qty) &&
                        decimal.TryParse(dt.Rows[i]["IN_QTY"].ToString(), out decimal in_qty) &&
                        decimal.TryParse(dt.Rows[i]["OUT_QTY"].ToString(), out decimal out_qty))
                    {
                        if (out_qty < size_qty && out_qty < in_qty)
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = false;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = false;

                            dataGridView2.Rows[i].Cells["Confirm"].Style.BackColor = Color.FromArgb(96, 233, 27);
                            dataGridView2.Rows[i].Cells["Confirm"].Style.ForeColor = Color.DarkBlue;

                        }
                        else if (size_qty == out_qty)
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Column18"].Value = out_qty;
                            dataGridView2.Rows[i].Cells["Column18"].Style.BackColor = Color.FromArgb(22, 199, 68);
                            // dataGridView2.Rows[i].Cells["Column18"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView2.Rows[i].Cells["Column18"].Style.ForeColor = Color.White;
                            dataGridView2.Rows[i].Cells["Confirm"].Style.BackColor = Color.DarkGray;
                            dataGridView2.Rows[i].Cells["Confirm"].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        // Handle parsing errors if any, such as invalid data in SIZE_QTY or IN_QTY columns
                        // You might want to log or handle this error appropriately
                        Console.WriteLine($"Error parsing row {i}: SIZE_QTY or IN_QTY is not a valid Value.");
                    }
                }
                dataGridView2.Refresh();
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                //return null;
            }

        }



        #region Old Code for Combobox Event
        //private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (comboBox1.SelectedItem == null || comboBox1.SelectedItem == "")
        //    {
        //        MessageHelper.ShowErr(this, "Please Select Component !");
        //        return;
        //    }


        //    string part_no = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
        //    Dictionary<string, Object> p = new Dictionary<string, object>();
        //    p.Add("MasterPO", txt_Master_Po.Text);
        //    p.Add("SalesOrder", txt_salesorder.Text);
        //    p.Add("PO", txt_po.Text);
        //    p.Add("Article", txt_article.Text);
        //    p.Add("Part_No", part_no);
        //    //p.Add("Line", lblline_info.Text);
        //    //p.Add("User", lbl_user_info.Text);
        //    //p.Add("OrderNo", txt_order_no.Text);
        //    //p.Add("IN","IN");

        //    Cursor.Current = Cursors.WaitCursor;

        //    string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetComponentRoutingInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
        //    var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
        //    if (Convert.ToBoolean(retJson["IsSuccess"]))
        //    {
        //        Cursor.Current = Cursors.Default;
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
        //        dataGridView1.DataSource = dt;


        //        #region Old Code

        //        //for (int i = 0; i < dt.Rows.Count; i++)
        //        //{
        //        //    decimal size_qty = decimal.Parse(dt.Rows[i]["SIZE_QTY"].ToString());
        //        //    decimal in_qty = decimal.Parse(dt.Rows[i]["IN_QTY"].ToString());


        //        //    if (in_qty < size_qty)
        //        //    {
        //        //        dataGridView1.Rows[i].Cells[columnIndex].ReadOnly = false;
        //        //    }
        //        //    else
        //        //    {
        //        //        dataGridView1.Rows[i].Cells[columnIndex].ReadOnly = true;
        //        //    }
        //        //}

        //        #endregion  


        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            // Parse the required columns to decimal
        //            if (decimal.TryParse(dt.Rows[i]["SIZE_QTY"].ToString(), out decimal size_qty) &&
        //                decimal.TryParse(dt.Rows[i]["IN_QTY"].ToString(), out decimal in_qty))
        //            {
        //                if (in_qty < size_qty)
        //                {
        //                    dataGridView1.Rows[i].Cells["Column6"].ReadOnly = false;
        //                    dataGridView1.Rows[i].Cells["Save"].ReadOnly = false;
        //                    dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.FromArgb(96, 233, 27);
        //                    //dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.DarkGreen;
        //                    dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.DarkBlue;
        //                    //dataGridView1.Columns["Save"].ReadOnly = false;
        //                }
        //                else
        //                {
        //                    dataGridView1.Rows[i].Cells["Column6"].ReadOnly = true;
        //                    dataGridView1.Rows[i].Cells["Save"].ReadOnly = true;
        //                    dataGridView1.Rows[i].Cells["Save"].Value = DBNull.Value; ;
        //                    dataGridView1.Rows[i].Cells["Column6"].Value = in_qty;
        //                    dataGridView1.Rows[i].Cells["Column6"].Style.BackColor = Color.FromArgb(22, 199, 68);
        //                    //dataGridView1.Rows[i].Cells["Column6"].Style.BackColor = Color.PaleVioletRed;
        //                    dataGridView1.Rows[i].Cells["Column6"].Style.ForeColor = Color.White;
        //                    dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.PaleVioletRed;
        //                    dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.White;
        //                }
        //            }
        //            else
        //            {
        //                // Handle parsing errors if any, such as invalid data in SIZE_QTY or IN_QTY columns
        //                // You might want to log or handle this error appropriately
        //                Console.WriteLine($"Error parsing row {i}: SIZE_QTY or IN_QTY is not a valid Value.");
        //            }
        //        }
        //        dataGridView1.Refresh();
        //    }
        //    else
        //    {
        //        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //        //return null;
        //    }

        //}

        #endregion


        #region New Code For ComboBox Event
        private async void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox1.SelectedItem.ToString() == "")
            {
                MessageHelper.ShowErr(this, "Please Select Component !");
                return;
            }

            string part_no = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
            Dictionary<string, object> parameters = new Dictionary<string, object>
    {
        { "MasterPO", txt_Master_Po.Text },
        { "SalesOrder", txt_salesorder.Text },
        { "PO", txt_po.Text },
        { "Article", txt_article.Text },
        { "Part_No", part_no }
        // Uncomment and add additional parameters if necessary
        // { "Line", lblline_info.Text },
        // { "User", lbl_user_info.Text },
        // { "OrderNo", txt_order_no.Text },
        // { "IN", "IN" }
    };

            // Change cursor to indicate loading
            Cursor.Current = Cursors.WaitCursor;

            // Make the API call asynchronously
            string response = await Task.Run(() =>
            {
                return SJeMES_Framework.WebAPI.WebAPIHelper.Post(
                    Program.Client.APIURL,
                    "KZ_WOOAPI",
                    "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server",
                    "GetComponentRoutingInfo",
                    Program.Client.UserToken,
                    Newtonsoft.Json.JsonConvert.SerializeObject(parameters)
                );
            });

            Cursor.Current = Cursors.Default;

            // Deserialize the response into a dictionary
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            if (Convert.ToBoolean(responseJson["IsSuccess"]))
            {
                // Deserialize the returned data into a DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(responseJson["RetData"].ToString());
                dataGridView1.DataSource = dt;

                // Iterate through the rows to modify the read-only state and style of cells based on conditions
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Parse SIZE_QTY and IN_QTY columns to decimals
                    if (decimal.TryParse(dt.Rows[i]["SIZE_QTY"].ToString(), out decimal size_qty) &&
                        decimal.TryParse(dt.Rows[i]["IN_QTY"].ToString(), out decimal in_qty))
                    {
                        if (in_qty < size_qty)
                        {
                            // Make cells editable and change their style if in_qty is less than size_qty
                            dataGridView1.Rows[i].Cells["Column6"].ReadOnly = false;
                            dataGridView1.Rows[i].Cells["Save"].ReadOnly = false;
                            dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.FromArgb(96, 233, 27);
                            dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.DarkBlue;
                        }
                        else
                        {
                            // Make cells read-only and change their style if in_qty is greater than or equal to size_qty
                            dataGridView1.Rows[i].Cells["Column6"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["Save"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["Save"].Value = DBNull.Value;
                            dataGridView1.Rows[i].Cells["Column6"].Value = in_qty;
                            dataGridView1.Rows[i].Cells["Column6"].Style.BackColor = Color.FromArgb(22, 199, 68);
                            dataGridView1.Rows[i].Cells["Column6"].Style.ForeColor = Color.White;
                            dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        // Handle parsing errors
                        Console.WriteLine($"Error parsing row {i}: SIZE_QTY or IN_QTY is not a valid value.");
                    }
                }

                // Refresh the DataGridView to apply changes
                dataGridView1.Refresh();
            }
            else
            {
                // Display an error message if the API call was not successful
                MessageHelper.ShowErr(this, responseJson["ErrMsg"].ToString());
            }
        }

        #endregion


        private void Txt_Master_Po_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Mark the event as handled, meaning the character input will be ignored
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(textBox5.Text)) && (string.IsNullOrEmpty(textBox5.Text)))
            {

                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Enter Master PO ! ", Program.Client, "", Program.Client.Language);
                MessageBox.Show(msg, "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clearAlloutwardData();

            Cursor.Current = Cursors.WaitCursor;

            LoadOutMasterPOData();
            LoadOutComponentsInfo();

            Cursor.Current = Cursors.Default;
        }

        private void LoadOutComponentsInfo()
        {
            DataTable dtJson = null;
            comboBox2.Items.Clear();
            List<string> partList1 = new List<string>();
            //var items1 = new List<AutocompleteItem>();
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("MasterPO", textBox5.Text);
            partList1.Clear();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "LoadComponentsInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            //string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "LoadComponentsInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    partList1.Add("");
                    //items1.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                    if (dtJson.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtJson.Rows.Count - 1; i++)
                        {
                            partList1.Add(dtJson.Rows[i]["PART_NO"].ToString().Trim() + "   " + dtJson.Rows[i]["PART_NAME"].ToString().Trim());
                            // items1.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["PART_NO"].ToString(),"  ", dtJson.Rows[i - 1]["PART_NAME"].ToString() }, dtJson.Rows[i - 1]["PART_NAME"].ToString()));

                        }
                        comboBox2.Items.AddRange(partList1.ToArray());
                        txt_out_order.Text = dtJson.Rows[0]["ORDER_NO"].ToString();
                        txt_out_order.BackColor = System.Drawing.SystemColors.Window;

                        //comboBox2.BackColor = Color.FromArgb(255, 255, 255);
                        //comboBox2.ForeColor = Color.Black;
                    }
                    else
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "");
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void LoadOutMasterPOData()
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("udf", textBox5.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.WorkOrderOperationServer", "GetOrderInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, DataTable> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, DataTable>>(json);
                //if (dic["DataTable"].Rows.Count > 0)
                //    dataGridView1.DataSource = dic["DataTable"];

                DataTable dt = dic["DataTable_info"];
                if (dt.Rows.Count > 0)
                {
                    textBox2.Text = dt.Rows[0]["art"].ToString();//ART
                    textBox1.Text = dt.Rows[0]["鞋款名称"].ToString();//shoe name
                    textBox4.Text = dt.Rows[0]["SALES_ORDER"].ToString();//sales order
                    textBox3.Text = dt.Rows[0]["po"].ToString();//PO

                    textBox3.BackColor = System.Drawing.SystemColors.Window;
                    textBox2.BackColor = System.Drawing.SystemColors.Window;
                    textBox4.BackColor = System.Drawing.SystemColors.Window;
                    textBox1.BackColor = System.Drawing.SystemColors.Window;

                    // textBox1.ForeColor = Color.DarkBlue;

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");             // New Code
                }
            }
        }





        private bool IsNumeric(string value)
        {
            decimal number;
            return decimal.TryParse(value, out number);
        }


        #region
        //private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0 || e.RowIndex > dataGridView1.Rows.Count - 1) { return; }


        //    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
        //    {
        //        if (row.Cells["Save"].Value == null)
        //        {
        //            MessageHelper.ShowErr(this, "Please Enter Qty");
        //            return;
        //        }
        //        else
        //        {
        //            string Master_PO = row.Cells["Column1"].Value.ToString();
        //            string Vendor_Name = row.Cells["Column9"].Value.ToString();
        //            string Process = row.Cells["Column8"].Value.ToString();
        //            string Part_Name = row.Cells["Column3"].Value.ToString();
        //            string Size = row.Cells["Column2"].Value.ToString();
        //            string Size_Qty = row.Cells["Column4"].Value.ToString();
        //            string Present_In_Qty = row.Cells["Column6"].Value.ToString();

        //            MessageHelper.ShowSuccess(this, "Save Successfully");

        //        }

        //        // Extract data from the row


        //    }

        //}
        #endregion

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();

            if (comboBox1.SelectedIndex == -1)
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Select Component ! ", Program.Client, "", Program.Client.Language);
                MessageHelper.ShowErr(this, msg);
                return;
            }

            DataTable CheckCuttingLine = IsCuttingLineorNot(lblline_info.Text.Trim());
            if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
            {
                if (CheckCuttingLine.Rows[0]["PROCESS"].ToString() == "C")
                {
                    if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                        e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Save")
                    {
                        // Extract data from the clicked row
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        string part_no = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                        string Line = lblline_info.Text;
                        string org_id = Line.Substring(0, 4);
                        string plant = string.Empty;
                        if (org_id == "5041")
                            plant = "APEX";
                        else
                            plant = Line.Substring(4, 3);
                        string User = lbl_user_info.Text;
                        string Sales_Order = txt_salesorder.Text;
                        string PO = txt_po.Text;
                        string Article = txt_article.Text;
                        string Art_Name = txt_po.Text;
                        //string Component = comboBox1.SelectedValue.ToString();
                        string Order_no = txt_order_no.Text;
                        string Master_PO = row.Cells["Column1"].Value.ToString();
                        string Vendor_Name = row.Cells["Column9"].Value.ToString();
                        string Process = row.Cells["Column8"].Value.ToString();
                        string Part_Name = row.Cells["Column3"].Value.ToString();
                        string Size = row.Cells["Column2"].Value.ToString();
                        int Size_Qty = int.Parse(row.Cells["Column4"].Value.ToString());
                        int Total_In_Qty = int.Parse(row.Cells["Column5"].Value.ToString());
                        int Balance_Qty = int.Parse(row.Cells["Column7"].Value.ToString());
                        // int Present_In_Qty = int.Parse(row.Cells["Column6"].Value.ToString());
                        string vend_code = string.Empty;
                        if (Vendor_Name == "HONGYANG INDUSTRY (INDIA) PRIV")
                        {
                            vend_code = "0000030170";
                        }
                        else
                        {
                            vend_code = "GY01";
                        }


                        int? Present_In_Qty = 0;
                        if (int.TryParse(row.Cells["Column6"].Value?.ToString(), out int parsedValue))
                        {
                            Present_In_Qty = parsedValue;
                        }

                        if (Size_Qty == Total_In_Qty || Size_Qty < Total_In_Qty)
                        {
                            MessageHelper.ShowSuccess(this, "This Size Qty is Fully Confirmed !");
                        }
                        else if (Present_In_Qty > Balance_Qty)
                        {
                            MessageHelper.ShowErr(this, "Entered Qty is Greater than Balance Qty !");
                        }
                        else if (!IsNumeric(Convert.ToString(Present_In_Qty)))
                        {
                            MessageHelper.ShowErr(this, "Please Enter Number Only.");
                        }
                        else if (string.IsNullOrEmpty(Convert.ToString(Present_In_Qty)))
                        {
                            MessageHelper.ShowErr(this, "Please Enter Received Qty");
                        }
                        else if (Present_In_Qty <= 0)
                        {
                            MessageHelper.ShowErr(this, "Please Enter Qty  Greater than 0 only !");
                        }
                        else
                        {

                            DialogResult res = MessageHelper.ShowOK(this, "Are you Sure Want to Save! ");
                            if (res == DialogResult.OK)
                            {
                                p.Add("QR_Code", "2222");   // 2222 indicates Manual Operation
                                p.Add("Line", Line);
                                p.Add("UserCode", User);
                                p.Add("Sales_Order", Sales_Order);
                                p.Add("PO", PO);
                                p.Add("Article", Article);
                                p.Add("Art_Name", Art_Name);
                                p.Add("PartName", Part_Name);
                                p.Add("part_no", part_no);
                                p.Add("Order_no", Order_no);
                                p.Add("Master_PO", Master_PO);
                                p.Add("Vendor_Name", Vendor_Name);
                                p.Add("Vend_Code", vend_code);
                                p.Add("Process", Process);
                                p.Add("Size", Size);
                                p.Add("Size_Qty", Size_Qty);
                                p.Add("Present_In_Qty", Present_In_Qty);
                                p.Add("org_id", org_id);
                                p.Add("Plant", plant);
                                p.Add("IP_Address", scan_ip);
                                // p.Add("Status", "IN");

                                //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Manual_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                                if (Convert.ToBoolean(retJson["IsSuccess"]))
                                {

                                    int count = int.Parse(retJson["RetData"].ToString());
                                    if (count > 0)
                                    {
                                        GetComponentRoutingInfo(Master_PO, part_no, Sales_Order, Article, PO);
                                        MessageHelper.ShowSuccess(this, "Save Successfully");
                                        Present_In_Qty = null;
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

                        }
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Only Cutting Lines Allowed to input the Data !");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "You are not Allowed to Scan , Please Contact Administrator! ");
            }


        }

        private void GetComponentRoutingInfo(string master_PO, string part_no, string sales_Order, string article, string pO)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("MasterPO", master_PO);
            p.Add("Part_No", part_no);
            p.Add("SalesOrder", sales_Order);
            p.Add("Article", article);
            p.Add("PO", pO);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetComponentRoutingInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                dataGridView1.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Parse the required columns to decimal
                    if (decimal.TryParse(dt.Rows[i]["SIZE_QTY"].ToString(), out decimal size_qty) &&
                        decimal.TryParse(dt.Rows[i]["IN_QTY"].ToString(), out decimal in_qty))
                    {
                        if (in_qty < size_qty)
                        {
                            dataGridView1.Rows[i].Cells["Column6"].ReadOnly = false;
                            dataGridView1.Rows[i].Cells["Save"].ReadOnly = false;
                            dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.FromArgb(96, 233, 27);
                            dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.DarkBlue;
                            //dataGridView1.Columns["Save"].ReadOnly = false;
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells["Column6"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["Save"].ReadOnly = true;
                            dataGridView1.Rows[i].Cells["Save"].Value = DBNull.Value; ;
                            dataGridView1.Rows[i].Cells["Column6"].Value = in_qty;
                            dataGridView1.Rows[i].Cells["Column6"].Style.BackColor = Color.FromArgb(22, 199, 68);
                            //dataGridView1.Rows[i].Cells["Column6"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[i].Cells["Column6"].Style.ForeColor = Color.White;
                            dataGridView1.Rows[i].Cells["Save"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView1.Rows[i].Cells["Save"].Style.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error parsing row {i}: SIZE_QTY or IN_QTY is not a valid Value.");
                    }
                }




                dataGridView1.Refresh();
            }
        }

        private void Txt_Master_Po_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if ((string.IsNullOrEmpty(txt_Master_Po.Text)) && (string.IsNullOrEmpty(txt_Master_Po.Text)))
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Enter Master PO ! ", Program.Client, "", Program.Client.Language);
                    MessageBox.Show(msg, "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                clearAllData();

                Cursor.Current = Cursors.WaitCursor;

                LoadMasterPOData();
                LoadComponentsInfo();

                Cursor.Current = Cursors.Default;

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            clearAlloutwardData();
            textBox5.Text = null;
        }

        private void clearAlloutwardData()
        {

            textBox4.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox1.Text = null;
            txt_out_order.Text = null;
            comboBox2.Items.Clear();
        }



        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.ColumnIndex;
            if (dataGridView2.RowCount <= 1) { return; }
            Regex qty_check = new Regex("^(0\\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\\.[0-9]*[0-9]$|[1-9]+[0-9]*$)");
            if (index == 9)
            {
                if (dataGridView2.Rows[e.RowIndex].Cells["Column18"].Value == null) { return; }
                string qty = dataGridView2.Rows[e.RowIndex].Cells["Column18"].Value.ToString();
                if (qty_check.IsMatch(qty))
                {
                    dataGridView2.Rows[e.RowIndex].Cells["Column18"].Value = qty;
                }
                else
                {
                    dataGridView2.Rows[e.RowIndex].Cells["Column18"].Value = string.Empty;
                    // dataGridView2.Rows[e.RowIndex].Cells["Column6"].Value = string.Empty;
                }
                return;
            }
        }

        private void DataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            Dictionary<string, object> p = new Dictionary<string, object>();

            if (comboBox2.SelectedIndex == -1 || comboBox2.Text == "")
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Select Component ! ", Program.Client, "", Program.Client.Language);
                MessageHelper.ShowErr(this, msg);
                return;
            }

            if (cb_stitching_Line.SelectedIndex == -1 || cb_stitching_Line.Text == "")
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Select Delivery Stitching Line ! ", Program.Client, "", Program.Client.Language);
                MessageHelper.ShowErr(this, msg);
                return;
            }

            DataTable CheckCuttingLine = IsCuttingLineorNot(lblline_info_out.Text.Trim());
            if (CheckCuttingLine.Rows.Count > 0 && CheckCuttingLine != null)
            {
                if (CheckCuttingLine.Rows[0]["PROCESS"].ToString() == "C")
                {
                    if (e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                        e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == "Confirm")
                    {
                        // Extract data from the clicked row
                        DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                        string deliveryLine = cb_stitching_Line.Text;
                        string part_no = Regex.Split(comboBox2.Text, "\\s+", RegexOptions.IgnoreCase)[0];
                        string Line = lblline_info_out.Text;
                        string org_id = Line.Substring(0, 4);
                        string plant = string.Empty;
                        if (org_id == "5041")
                            plant = "APEX";
                        else
                            plant = Line.Substring(4, 3);
                        string User = lbl_user_info.Text;
                        string Sales_Order = textBox4.Text;
                        string PO = textBox3.Text;
                        string Article = textBox2.Text;
                        string Art_Name = textBox1.Text;
                        //string Component = comboBox1.SelectedValue.ToString();
                        string Order_no = txt_out_order.Text;
                        string Master_PO = row.Cells["Column10"].Value.ToString();
                        string Vendor_Name = row.Cells["Column11"].Value.ToString();
                        string Process = row.Cells["Column12"].Value.ToString();
                        string Part_Name = row.Cells["Column13"].Value.ToString();
                        string Size = row.Cells["Column14"].Value.ToString();
                        int Size_Qty = int.Parse(row.Cells["Column15"].Value.ToString());
                        int Total_In_Qty = int.Parse(row.Cells["Column19"].Value.ToString());
                        int Balance_Qty = int.Parse(row.Cells["Column17"].Value.ToString());
                        int Total_Issue_Qty = int.Parse(row.Cells["Column16"].Value.ToString());
                        string vend_code = string.Empty;
                        if (Vendor_Name == "HONGYANG INDUSTRY (INDIA) PRIV")
                        {
                            vend_code = "0000030170";
                        }
                        else
                        {
                            vend_code = "GY01";
                        }


                        int? Present_Issue_Qty = 0;
                        if (int.TryParse(row.Cells["Column18"].Value?.ToString(), out int parsedValue))
                        {
                            Present_Issue_Qty = parsedValue;
                        }

                        if (Size_Qty == Total_Issue_Qty || Size_Qty < Total_Issue_Qty)
                        {
                            MessageHelper.ShowSuccess(this, "This Size Qty is Fully Confirmed !");
                        }
                        else if (Present_Issue_Qty > Balance_Qty)
                        {
                            MessageHelper.ShowErr(this, "Entered Qty is Greater than Inventory  !");
                        }
                        else if (!IsNumeric(Convert.ToString(Present_Issue_Qty)))
                        {
                            MessageHelper.ShowErr(this, "Please Enter Number Only.");
                        }
                        else if (string.IsNullOrEmpty(Convert.ToString(Present_Issue_Qty)))
                        {
                            MessageHelper.ShowErr(this, "Please Enter Received Qty");
                        }
                        else if (Present_Issue_Qty <= 0)
                        {
                            MessageHelper.ShowErr(this, "Please Enter Qty  Greater than 0 only !");
                        }
                        else
                        {
                            DialogResult res = MessageHelper.ShowOK(this, "Are you sure you want to Save ");
                            if (res == DialogResult.OK)
                            {
                                p.Add("DeliveryLine", deliveryLine);
                                p.Add("QR_Code", "2222");   // 2222 indicates Manual Operation
                                p.Add("Line", Line);
                                p.Add("UserCode", User);
                                p.Add("Sales_Order", Sales_Order);
                                p.Add("PO", PO);
                                p.Add("Article", Article);
                                p.Add("Art_Name", Art_Name);
                                p.Add("PartName", Part_Name);
                                p.Add("part_no", part_no);
                                p.Add("Order_no", Order_no);
                                p.Add("Master_PO", Master_PO);
                                p.Add("Vendor_Name", Vendor_Name);
                                p.Add("Vend_Code", vend_code);
                                p.Add("Process", Process);
                                p.Add("Size", Size);
                                p.Add("Size_Qty", Size_Qty);
                                p.Add("Present_out_Qty", Present_Issue_Qty);
                                p.Add("org_id", org_id);
                                p.Add("Plant", plant);
                                p.Add("IP_Address", scan_ip);
                                // p.Add("Status", "IN");

                                //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_ManualOutward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
                                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                                if (Convert.ToBoolean(retJson["IsSuccess"]))
                                {

                                    int count = int.Parse(retJson["RetData"].ToString());
                                    if (count > 0)
                                    {
                                        GetoutComponentRoutingInfo(Master_PO, part_no, Sales_Order, Article, PO);
                                        MessageHelper.ShowSuccess(this, "Save Successfully");
                                        Present_Issue_Qty = null;
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
                        }
                    }

                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Only Cutting Lines Allowed to input the Data !");
                }
            }
            else
            {
                MessageHelper.ShowErr(this, "You are not Allowed to Scan , Please Check your Account! ");
            }



        }

        private void GetoutComponentRoutingInfo(string master_PO, string part_no, string sales_Order, string article, string pO)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("MasterPO", master_PO);
            p.Add("SalesOrder", sales_Order);
            p.Add("PO", pO);
            p.Add("Article", article);
            p.Add("Part_No", part_no);

            Cursor.Current = Cursors.WaitCursor;

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "GetoutComponentRoutingInfo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                Cursor.Current = Cursors.Default;
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(retJson["RetData"].ToString());
                dataGridView2.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Parse the required columns to decimal
                    if (decimal.TryParse(dt.Rows[i]["SIZE_QTY"].ToString(), out decimal size_qty) &&
                        decimal.TryParse(dt.Rows[i]["IN_QTY"].ToString(), out decimal in_qty) &&
                        decimal.TryParse(dt.Rows[i]["OUT_QTY"].ToString(), out decimal out_qty))
                    {
                        if (out_qty < size_qty && out_qty < in_qty)
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = false;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = false;

                            dataGridView2.Rows[i].Cells["Confirm"].Style.BackColor = Color.FromArgb(96, 233, 27); ;
                            dataGridView2.Rows[i].Cells["Confirm"].Style.ForeColor = Color.DarkBlue;

                        }
                        else if (size_qty == out_qty)
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Column18"].Value = out_qty;
                            dataGridView2.Rows[i].Cells["Column18"].Style.BackColor = Color.FromArgb(22, 199, 68);
                            //dataGridView2.Rows[i].Cells["Column18"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView2.Rows[i].Cells["Column18"].Style.ForeColor = Color.White;
                            dataGridView2.Rows[i].Cells["Confirm"].Style.BackColor = Color.PaleVioletRed;
                            dataGridView2.Rows[i].Cells["Confirm"].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dataGridView2.Rows[i].Cells["Column18"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].ReadOnly = true;
                            dataGridView2.Rows[i].Cells["Confirm"].Value = DBNull.Value;
                        }
                    }
                    else
                    {
                        // Handle parsing errors if any, such as invalid data in SIZE_QTY or IN_QTY columns
                        // You might want to log or handle this error appropriately
                        Console.WriteLine($"Error parsing row {i}: SIZE_QTY or IN_QTY is not a valid Value.");
                    }
                }
                dataGridView2.Refresh();
            }
            else
            {
                MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
                //return null;
            }

        }


        #region  Cell Content Click Inward Process

        //private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    Dictionary<string, object> p = new Dictionary<string, object>();

        //    if (comboBox1.SelectedIndex == -1)
        //    {
        //        string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Select Component ! ", Program.Client, "", Program.Client.Language);
        //        MessageHelper.ShowErr(this, msg);
        //        return;
        //    }

        //    DialogResult res = MessageBox.Show("Are you sure you want to Save ", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        //    if (res == DialogResult.OK)
        //    {
        //        if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
        //        e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Save")
        //        {
        //            // Extract data from the clicked row
        //            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];


        //            string part_no = Regex.Split(comboBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0];
        //            string Line = lblline_info.Text;
        //            string org_id = Line.Substring(0, 4);
        //            string plant = string.Empty;
        //            if (org_id == "5041")
        //                plant = "APEX";
        //            else
        //                plant = Line.Substring(4, 3);
        //            string User = lbl_user_info.Text;
        //            string Sales_Order = txt_salesorder.Text;
        //            string PO = txt_po.Text;
        //            string Article = txt_article.Text;
        //            string Art_Name = txt_po.Text;
        //            //string Component = comboBox1.SelectedValue.ToString();
        //            string Order_no = txt_order_no.Text;
        //            string Master_PO = row.Cells["Column1"].Value.ToString();
        //            string Vendor_Name = row.Cells["Column9"].Value.ToString();
        //            string Process = row.Cells["Column8"].Value.ToString();
        //            string Part_Name = row.Cells["Column3"].Value.ToString();
        //            string Size = row.Cells["Column2"].Value.ToString();
        //            int Size_Qty = int.Parse(row.Cells["Column4"].Value.ToString());
        //            int Total_In_Qty = int.Parse(row.Cells["Column5"].Value.ToString());
        //            int Balance_Qty = int.Parse(row.Cells["Column7"].Value.ToString());
        //            // int Present_In_Qty = int.Parse(row.Cells["Column6"].Value.ToString());
        //            string vend_code = string.Empty;
        //            if (Vendor_Name == "HONGYANG INDUSTRY (INDIA) PRIV")
        //            {
        //                vend_code = "0000030170";
        //            }
        //            else
        //            {
        //                vend_code = "GY01";
        //            }


        //            int? Present_In_Qty = 0;
        //            if (int.TryParse(row.Cells["Column6"].Value?.ToString(), out int parsedValue))
        //            {
        //                Present_In_Qty = parsedValue;
        //            }

        //            if (Size_Qty == Total_In_Qty || Size_Qty < Total_In_Qty)
        //            {
        //                MessageHelper.ShowSuccess(this, "This Size Qty is Fully Confirmed !");
        //            }
        //            else if (Present_In_Qty > Balance_Qty)
        //            {
        //                MessageHelper.ShowErr(this, "Entered Qty is Greater than Balance Qty !");
        //            }
        //            else if (!IsNumeric(Convert.ToString(Present_In_Qty)))
        //            {
        //                MessageHelper.ShowErr(this, "Please Enter Number Only.");
        //            }
        //            else if (string.IsNullOrEmpty(Convert.ToString(Present_In_Qty)))
        //            {
        //                MessageHelper.ShowErr(this, "Please Enter Received Qty");
        //            }
        //            else if (Present_In_Qty <= 0)
        //            {
        //                MessageHelper.ShowErr(this, "Please Enter Qty  Greater than 0 only !");
        //            }
        //            else
        //            {

        //                p.Add("QR_Code", "2222");   // 2222 indicates Manual Operation
        //                p.Add("Line", Line);
        //                p.Add("UserCode", User);
        //                p.Add("Sales_Order", Sales_Order);
        //                p.Add("PO", PO);
        //                p.Add("Article", Article);
        //                p.Add("Art_Name", Art_Name);
        //                p.Add("PartName", Part_Name);
        //                p.Add("part_no", part_no);
        //                p.Add("Order_no", Order_no);
        //                p.Add("Master_PO", Master_PO);
        //                p.Add("Vendor_Name", Vendor_Name);
        //                p.Add("Vend_Code", vend_code);
        //                p.Add("Process", Process);
        //                p.Add("Size", Size);
        //                p.Add("Size_Qty", Size_Qty);
        //                p.Add("Present_In_Qty", Present_In_Qty);
        //                p.Add("org_id", org_id);
        //                p.Add("Plant", plant);
        //                p.Add("IP_Address", scan_ip);
        //                // p.Add("Status", "IN");

        //                //string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
        //                string ret = WebAPIHelper.Post(Program.Client.APIURL, "KZ_WOOAPI", "KZ_WOOAPI.Controllers.RTL_BarcodePrint_Server", "Insert_RTL_Manual_Inward_Data", Program.Client.UserToken, JsonConvert.SerializeObject(p));
        //                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
        //                if (Convert.ToBoolean(retJson["IsSuccess"]))
        //                {

        //                    int count = int.Parse(retJson["RetData"].ToString());
        //                    if (count > 0)
        //                    {
        //                        GetComponentRoutingInfo(Master_PO, part_no, Sales_Order, Article, PO);
        //                        MessageHelper.ShowSuccess(this, "Save Successfully");
        //                        Present_In_Qty = null;
        //                    }
        //                    else
        //                    {
        //                        MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //                    }
        //                }
        //                else
        //                {
        //                    MessageHelper.ShowErr(this, retJson["ErrMsg"].ToString());
        //                }


        //            }
        //        }
        //    }
        //}


        #endregion



        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((string.IsNullOrEmpty(textBox5.Text)) && (string.IsNullOrEmpty(textBox5.Text)))
                {

                    string msg = SJeMES_Framework.Common.UIHelper.UImsg("Please Enter Master PO ! ", Program.Client, "", Program.Client.Language);
                    MessageBox.Show(msg, "Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                clearAlloutwardData();

                Cursor.Current = Cursors.WaitCursor;

                LoadOutMasterPOData();
                LoadOutComponentsInfo();

                Cursor.Current = Cursors.Default;
            }

        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control characters like backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Mark the event as handled, meaning the character input will be ignored
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.ColumnIndex;
            if (dataGridView1.RowCount <= 1) { return; }
            Regex qty_check = new Regex("^(0\\.0*[1-9]+[0-9]*$|[1-9]+[0-9]*\\.[0-9]*[0-9]$|[1-9]+[0-9]*$)");
            if (index == 9)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value == null) { return; }
                string qty = dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value.ToString();
                if (qty_check.IsMatch(qty))
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value = qty;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Column6"].Value = string.Empty;
                    // dataGridView2.Rows[e.RowIndex].Cells["Column6"].Value = string.Empty;
                }
                return;
            }
        }
    }
}
