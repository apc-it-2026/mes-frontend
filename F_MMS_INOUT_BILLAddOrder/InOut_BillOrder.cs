using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.Common;
using SJeMES_Framework.WebAPI;
//using WinFormLib;

namespace F_MMS_InOut_BillAddOrder
{
    public partial class InOut_BillOrder : MaterialForm
    {
        DataTable TailorDt = new DataTable();
        DataTable ProcessDt = new DataTable();
        //详细
        DataTable TailorDt1 = new DataTable();
        DataTable ProcessDt1 = new DataTable();

        //部门数组
        DataTable deptData = new DataTable();
      
        public InOut_BillOrder()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void InOut_BillOrder_Load(object sender, EventArgs e)
        {
            LoadDept();
            LoadProcess();
            GetComboBoxUI();
            LoadReceivingDepartment();

            //tabPage3.Parent = null;
            Font a = new Font("宋体", 12);
            dataGridView1.Font = a;//字体
            dataGridView2.Font = a;
            dataGridView3.Font = a;
            dataGridView4.Font = a;
            dataGridView5.Font = a;
            dataGridView6.Font = a;
            dataGridView7.Font = a;
            dataGridView8.Font = a;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView3.AutoGenerateColumns = false;
            dataGridView4.AutoGenerateColumns = false;
            dataGridView5.AutoGenerateColumns = false;
            dataGridView6.AutoGenerateColumns = false;
            dataGridView7.AutoGenerateColumns = false;
            dataGridView8.AutoGenerateColumns = false;

            label28.Visible = this.label31.Visible= this.label33.Visible = this.label35.Visible = false;
        }

        private void LoadDept()
        {
            var source = new AutoCompleteStringCollection();   //存放数据库查询结果
            Dictionary<string, string> p = new Dictionary<string, string>();  
            p.Add("s", "C");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "OutgoingQuery", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                deptData = dtJson;
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["DEPARTMENT_CODE"] + "  " + dtJson.Rows[i - 1]["DEPARTMENT_NAME"]);
                }
                textDept.AutoCompleteCustomSource = source;
                textDept.AutoCompleteMode = AutoCompleteMode.Suggest;
                textDept.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textDept2.AutoCompleteCustomSource = source;
                textDept2.AutoCompleteMode = AutoCompleteMode.Suggest;
                textDept2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void LoadProcess()
        {
            var source = new AutoCompleteStringCollection();   //存放数据库查询结果
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("s", "A");
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "GetProcessVend", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["VEND_NO"] + "  " + dtJson.Rows[i - 1]["VEND_NAME"]);
                }
                textBox1.AutoCompleteCustomSource = source;
                textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtVendor.AutoCompleteCustomSource = source;
                txtVendor.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtVendor.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void GetComboBoxUI()
        {
            List<ComboboxEntry> TailorEntries = new List<ComboboxEntry>();
            TailorEntries.Add(new ComboboxEntry() { Code = "", Name = "ALL" });

            List<ComboboxEntry> VendEntries = new List<ComboboxEntry>();
            VendEntries.Add(new ComboboxEntry() { Code = "", Name = "ALL" });

            List<ComboboxEntry> AuditStatus = new List<ComboboxEntry>();
            AuditStatus.Add(new ComboboxEntry() { Code = "", Name = "ALL" });
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("enmu_type", "TailorEntries");

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "GetComboBoxUI", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    TailorEntries.Add(new ComboboxEntry { Code = dtJson.Rows[i]["ENUM_CODE"].ToString(), Name = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

            p.Remove("enmu_type");
            p.Add("enmu_type", "VendEntries");
            string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "GetComboBoxUI", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    VendEntries.Add(new ComboboxEntry { Code = dtJson.Rows[i]["ENUM_CODE"].ToString(), Name = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
            }

            p.Remove("enmu_type");
            p.Add("enmu_type", "AuditStatus");
            string ret3 = WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ComboBoxUIServer", "GetComboBoxUI", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    AuditStatus.Add(new ComboboxEntry { Code = dtJson.Rows[i]["ENUM_CODE"].ToString(), Name = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret3)["ErrMsg"].ToString());
            }

            cbTailor.DataSource = TailorEntries;
            cbTailor.DisplayMember = "Name";
            cbTailor.ValueMember = "Code";

            comboBox2.DataSource = VendEntries;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Code";

            cbTailor2.DataSource = TailorEntries;
            cbTailor2.DisplayMember = "Name";
            cbTailor2.ValueMember = "Code";

            cbTailor3.DataSource = VendEntries;
            cbTailor3.DisplayMember = "Name";
            cbTailor3.ValueMember = "Code";

            Column3.DataSource = TailorEntries;
            Column3.DisplayMember = "Name";
            Column3.ValueMember = "Code";

            vend_type.DataSource = VendEntries;
            vend_type.DisplayMember = "Name";
            vend_type.ValueMember = "Code";

            dvVend_Type.DataSource = VendEntries;
            dvVend_Type.DisplayMember = "Name";
            dvVend_Type.ValueMember = "Code";

            dataGridViewComboBoxColumn1.DataSource = TailorEntries;
            dataGridViewComboBoxColumn1.DisplayMember = "Name";
            dataGridViewComboBoxColumn1.ValueMember = "Code";

            vend_type.DataSource = VendEntries;
            vend_type.DisplayMember = "Name";
            vend_type.ValueMember = "Code";

            cbStatus.DataSource = AuditStatus;
            cbStatus.DisplayMember = "Name";
            cbStatus.ValueMember = "Code";
            comboBox1.DataSource = AuditStatus;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Code";

            cbStatus2.DataSource = AuditStatus;
            cbStatus2.DisplayMember = "Name";
            cbStatus2.ValueMember = "Code";

            cbStatus3.DataSource = AuditStatus;
            cbStatus3.DisplayMember = "Name";
            cbStatus3.ValueMember = "Code";

            Column4.DataSource = AuditStatus;
            Column4.DisplayMember = "Name";
            Column4.ValueMember = "Code";

            vend_status.DataSource = AuditStatus;
            vend_status.DisplayMember = "Name";
            vend_status.ValueMember = "Code";

            dvVend_Status.DataSource = AuditStatus;
            dvVend_Status.DisplayMember = "Name";
            dvVend_Status.ValueMember = "Code";

            dataGridViewComboBoxColumn2.DataSource = AuditStatus;
            dataGridViewComboBoxColumn2.DisplayMember = "Name";
            dataGridViewComboBoxColumn2.ValueMember = "Code";
            vend_status.DataSource = AuditStatus;
            vend_status.DisplayMember = "Name";
            vend_status.ValueMember = "Code";
        }

        private void btnTailorQuery_Click(object sender, EventArgs e)
        {
            //string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_WMSAPI", "KZ_WMSAPI.Controllers.F_WMS_MaterielOutputServer", "getReason", Program.client.UserToken, JsonConvert.SerializeObject(string.Empty));

            Dictionary<string, string> p = new Dictionary<string, string>();
            //裁剪
            p.Add("vDept", Regex.Split(textDept.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vWorkDate", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            p.Add("vType", cbTailor.SelectedValue.ToString());
            p.Add("vStatus", cbStatus.SelectedValue.ToString());
            p.Add("vBILL_NO", txtOrderID.Text);
            //p.Add("vVend_No", txtFeedingGroup.Text);//comboxVend_No   //p.Add("vFeedingGroup",txtFeedingGroup.Text);
            p.Add("vVend_No", comboxVend_No.Text);
            p.Add("vWareHouse", Regex.Split(comboxVend_No.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vCreateUser", this.txtCreateUser1.Text);
            string cbstatusValue = cbStatus.SelectedValue.ToString();
            
            btnCheck1.Enabled = cbstatusValue != "Y";//如果不是审核就是禁用状态

            if (string.IsNullOrEmpty(cbstatusValue))
            {
                btnCheck1.Enabled = false;
            }

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetTailorBill", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView2.Update();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int index = dataGridView1.CurrentRow.Index;
                    string type = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    string UNIT = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    DataTable dataTablePoint = TailorDt.Copy();

                    DataRow[] dataRows = deptData.Select("DEPARTMENT_CODE='"+UNIT+"'");
                    string DEPARTMENT_NAME = "";
                    if (dataRows.Length == 0) 
                        DEPARTMENT_NAME ="";
                    else
                        DEPARTMENT_NAME = dataRows[0].ItemArray[1].ToString();

                    dataTablePoint.Columns.Add(new DataColumn("DEPARTMENT_NAME", typeof(string)));
                    for (int i = 0; i < dataTablePoint.Rows.Count; i++)
                    {
                       // dataTablePoint.Rows[i]["UNIT"] = UNIT;
                        dataTablePoint.Rows[i]["DEPARTMENT_NAME"] = DEPARTMENT_NAME;
                    }

                    var reportType = type.Equals("A") ? "\\裁剪收货.frx" : "\\裁剪发货.frx";
                    string path = Application.StartupPath + @"\报表" + reportType;
                    InOutBillAddOrder_MMSPrint frm = new InOutBillAddOrder_MMSPrint(dataTablePoint, path);
                    frm.Show();
                }
            }
        }

        private void btnVendExport_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 0)
            {
                if (dataGridView3.CurrentRow != null)
                {
                    int index = dataGridView3.CurrentRow.Index;
                    string type = dataGridView3.Rows[index].Cells[4].Value.ToString();
                    string BEFOREUNIT = dataGridView3.Rows[index].Cells[1].Value.ToString();
                    DataTable dataTablePoint = ProcessDt.Copy();
                    // DataRow TotaldgvRow2 = TotalRow.GetTotalRow(dataTablePoint) ?? throw new ArgumentNullException("TotalRow.GetTotalRow(dataTablePoint)");        //返回 DataRow
                    // TotaldgvRow2[dataTablePoint.Columns["SIZE_NO"]] = "合计";
                    //TotaldgvRow2[dataTablePoint.Columns["order_no"]] = string.Empty;
                    // dataTablePoint.Rows.Add(TotaldgvRow2);
                    dataTablePoint.Columns.Add(new DataColumn("BEFOREUNIT", typeof(string)));
                    for (int i = 0; i < dataTablePoint.Rows.Count; i++)
                    {
                        // dataTablePoint.Rows[i]["UNIT"] = UNIT;
                        dataTablePoint.Rows[i]["BEFOREUNIT"] = BEFOREUNIT;
                    }


                    var reportType = type.Equals("B") ? "\\中仓出厂商.frx" : "\\厂商入中仓.frx";
                    string path = Application.StartupPath + @"\报表" + reportType;
                    InOutBillAddOrder_MMSPrint frm = new InOutBillAddOrder_MMSPrint(dataTablePoint, path);
                    frm.Show();
                }
            }
        }

        private void btnVendQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vDept", Regex.Split(textBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vWorkDate", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
            p.Add("vType", comboBox2.SelectedValue.ToString());
            p.Add("vStatus", comboBox1.SelectedValue.ToString());
            p.Add("vBILL_NO", textBox2.Text);
            string comboBox1Value = cbStatus.SelectedValue.ToString();
            string comboBox2Value = comboBox2.SelectedValue.ToString();
            p.Add("vWareHouse", Regex.Split(textBox3.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vCreateUser",this.txtCreateUser2.Text);
            button1.Enabled = comboBox1Value != "Y" && comboBox2Value != "";
            if (string.IsNullOrEmpty(comboBox1Value))
            {
                button1.Enabled = false;
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetProcessBill", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView3.DataSource = dtJson.DefaultView;
                dataGridView4.Update();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1 && dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.CurrentRow.Index;
                var vBillNo = dataGridView1.Rows[index].Cells[0].Value == null ? "" : dataGridView1.Rows[index].Cells[0].Value.ToString();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("BillNo", vBillNo); 
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetTailorBillDetail", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString(); //5
                    
                    TailorDt = JsonHelper.GetDataTableByJson(json);
                    //TailorDt.Columns["QUANTITY"].DataType = System.Type.GetType("System.Int32");
                    dataGridView2.DataSource = TailorDt.DefaultView;
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;
            if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Index > -1 && dataGridView3.SelectedRows.Count > 0)
            {
                int index = dataGridView3.CurrentRow.Index;
                var vBillNo = dataGridView3.Rows[index].Cells[0].Value == null ? "" : dataGridView3.Rows[index].Cells[0].Value.ToString();
                Dictionary<string, object> p = new Dictionary<string, object>();
                Dictionary<string, object> hasmap = new Dictionary<string, object>();
                p.Add("BillNo", vBillNo);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetProcessBillDetail", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    ProcessDt = JsonHelper.GetDataTableByJson(json);
                    dataGridView4.DataSource = ProcessDt.DefaultView;

                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    int row = dataGridView4.Rows.Count;
                   
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }
     

        //裁剪审核
        private void btnCheck1_Click(object sender, EventArgs e)
        {
            btnCheck1.Enabled = false;
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index > -1 && dataGridView1.SelectedRows.Count > 0)
            {
                int a = dataGridView1.CurrentRow.Index;
                string bill_no = dataGridView1.Rows[a].Cells["Column1"].Value.ToString();
                DialogResult dr = MessageBox.Show("是否确认审核？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    DataTable TailorCheckDt = GetDgvToTable(dataGridView2);//打包
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    p.Add("bill_no", bill_no);
                    p.Add("data", TailorCheckDt);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "TailorBillCheck", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        TailorRefresh();
                        MessageHelper.ShowSuccess(this,"提交成功!");
                        btnCheck1.Enabled = true;
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                        btnCheck1.Enabled = true;
                    }
                }
              
            }

        }

        //工艺的审核
        private void btnCheck2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Index > -1 && dataGridView3.SelectedRows.Count > 0)
            {
                int a = dataGridView3.CurrentRow.Index;
                string bill_no = dataGridView3.Rows[a].Cells["V_bill_no"].Value.ToString();
                string ORG = dataGridView4.Rows[0].Cells["V_RECEIVING_ORGID"].Value.ToString();
                DialogResult dr = MessageBox.Show("是否确认审核？", "提示", MessageBoxButtons.OKCancel);
               //  Boolean flag = true;
               // if(dr == DialogResult.OK)
               // {
               //     if (comboBox2.Text == "收料")
               //     {
               //         DataTable Dt = GetDgvToTable(dataGridView4);
               //         if (Dt.Rows.Count > 0)
               //         {
               //             Dictionary<string, object> p = new Dictionary<string, object>();
               //             p.Add("ORG", ORG);
               //             p.Add("data", Dt);
               //             string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "QueryData", Program.client.UserToken, JsonConvert.SerializeObject(p));
               //             if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
               //             {
               //                 MessageHelper.ShowSuccess(this, "入库成功!");
               //             }
               //             else
               //             {
               //                 flag = false;
               //                 MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
               //             }
               //         }
               //     }
               // }

                if (dr == DialogResult.OK)
                {
                    Boolean flag = false;
                    if (comboBox2.Text == "Receipt")
                    {
                        flag = true;
                    }
                      
                    Dictionary<string, object> p = new Dictionary<string, object>();
                    DataTable Dt = GetDgvToTable(dataGridView4);
                    DataTable Dt1 = GetDgvToTable1(dataGridView4);
                    var query = from g in Dt.AsEnumerable()
                                group g by new
                                {
                                    V_order_no = g.Field<string>("V_order_no"),
                                    order_no = g.Field<string>("Column13"),
                                    order_seq = g.Field<string>("Column16"),
                                    V_SIZE_NO = g.Field<string>("V_SIZE_NO")                                  
                                } into products

                                select new
                                {
                                    V_order_no = products.Key.V_order_no,
                                    order_no = products.Key.order_no,
                                    order_seq = products.Key.order_seq,
                                    V_SIZE_NO = products.Key.V_SIZE_NO,
                                    qty = products.Sum(n => Convert.ToDecimal(n.Field<string>("qty")))
                                };

                    p.Add("bill_no", bill_no); 
                    p.Add("data", Dt);
                    p.Add("flag", flag); 
                    p.Add("data1", query);
                    p.Add("ORG", ORG);
                    string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "ProcessBillCheck", Program.client.UserToken, JsonConvert.SerializeObject(p));
                    if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                    {
                        VendRefresh();
                        MessageHelper.ShowSuccess(this, "提交成功!");
                    }
                    else
                    {
                        MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                    }
                }
           
            }
            button1.Enabled = true;
        }

        private DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name);
                dt.Columns.Add(dc);
            }

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

        private DataTable GetDgvToTable1(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name);
                dt.Columns.Add(dc);
            }

            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                if (string.IsNullOrEmpty(Convert.ToString(dgv.Rows[count].Cells[0].Value)))
                {
                    continue;
                }
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public void TailorRefresh()
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vDept", Regex.Split(textDept.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vWorkDate", dateTimePicker1.Value.ToString("yyyy/MM/dd"));
            p.Add("vType", cbTailor.SelectedValue.ToString());
            p.Add("vStatus", cbStatus.SelectedValue.ToString());
            p.Add("vBILL_NO", txtOrderID.Text);
            //p.Add("vVend_No", txtFeedingGroup.Text);//comboxVend_No   //p.Add("vFeedingGroup",txtFeedingGroup.Text);
            p.Add("vVend_No", comboxVend_No.Text);
            p.Add("vWareHouse", Regex.Split(comboxVend_No.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vCreateUser", this.txtCreateUser1.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "GetTailorBill", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView2.Update();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        public void VendRefresh()
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vDept", Regex.Split(textBox1.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vWorkDate", dateTimePicker2.Value.ToString("yyyy/MM/dd"));
            p.Add("vType", comboBox2.SelectedValue.ToString());
            p.Add("vStatus", comboBox1.SelectedValue.ToString());
            p.Add("vBILL_NO", textBox2.Text);
            p.Add("vWareHouse", Regex.Split(textBox3.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vCreateUser", this.txtCreateUser2.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetProcessBill", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView3.DataSource = dtJson.DefaultView;
                dataGridView4.Update();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnDetailedQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vDept", Regex.Split(textDept2.Text, "\\s+", RegexOptions.IgnoreCase)[0]);//部门
            p.Add("vWorkDate", dateTimePicker3.Value.ToString("yyyy/MM/dd"));//工作日期
            p.Add("vType", cbTailor2.SelectedValue.ToString());//类型
            p.Add("vStatus", cbStatus2.SelectedValue.ToString());//审核状态
            p.Add("vWareHouse", Regex.Split(textBox5.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vBILL_NO", textBox4.Text);
            p.Add("vCreateUser", this.txtCreateUser3.Text);
            string cbstatusValue = cbStatus2.SelectedValue.ToString();
            
            if (cbstatusValue == "Y"|| string.IsNullOrEmpty(cbstatusValue))
            {
                btnDetailedAudit.Enabled = false;//审核按钮为灰色
            }
            else
            {
                btnDetailedAudit.Enabled = true;
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetDetailedQueryList", Program.client.UserToken, JsonConvert.SerializeObject(p));
           
            var dic= JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"];
            if (Convert.ToBoolean(dic))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView5.DataSource = dtJson.DefaultView;
                dataGridView6.Update();
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                string ss = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
            }
        }

        private void btnExportOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView5.Rows.Count > 0)
            {
                if (dataGridView5.CurrentRow != null)
                {
                    int index = dataGridView5.CurrentRow.Index;
                    string type = dataGridView5.Rows[index].Cells[2].Value.ToString();
                    DataTable dataTablePoint = TailorDt1.Copy();
                    for (int i = 0; i < dataTablePoint.Rows.Count; i++)
                    {
                        string str = dataTablePoint.Rows[i]["T_ORDER_NO"].ToString();
                        string str1 = dataTablePoint.Rows[i]["sales_order"].ToString();
                        string temp_order = "";
                        string temp_sales = "";
                        string[] vs = str.Split(',');
                        string[] vs1 = str1.Split(',');
                        foreach (var s in vs)
                        {
                            string t1 = s.Remove(0, 3);
                            Console.WriteLine(t1);
                            int kk = int.Parse(t1);
                            temp_order += kk + ",";
                        }
                        foreach (var s in vs1)
                        {
                            string t1 = s.Remove(0, 1);
                            Console.WriteLine(t1);
                            int kk = int.Parse(t1);
                            temp_sales += kk + ",";
                        }

                        temp_order = temp_order.Remove(temp_order.Length - 1, 1);
                        temp_sales = temp_sales.Remove(temp_sales.Length - 1, 1);
                        dataTablePoint.Rows[i]["T_ORDER_NO"] = temp_order;
                        dataTablePoint.Rows[i]["sales_order"] = temp_sales;
                    }

                    DataRow dataRow = WinFormLib.TotalRow.GetTotalRow(dataTablePoint);
                    dataRow[dataTablePoint.Columns["SALES_ORDER"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["T_ORDER_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["SIZE_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["TRAIN_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["T_QUANTITY"]] = "合计";
                    dataTablePoint.Rows.Add(dataRow);
                    var reportType = type.Equals("A") ? "\\裁剪收货1_工单版.frx" : "\\裁剪发货1_工单版.frx";
                    string path = Application.StartupPath + @"\报表" + reportType;
                    InOutBillAddOrder_MMSPrint frm = new InOutBillAddOrder_MMSPrint(dataTablePoint, path);
                    frm.Show();
                }
            }
        }


        //详细的裁剪审核
        //private void btnDetailedAudit_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView5.CurrentRow != null && dataGridView5.CurrentRow.Index > -1 && dataGridView5.SelectedRows.Count > 0)
        //    {
        //        int a = dataGridView5.CurrentRow.Index;
        //        string bill_no = dataGridView5.Rows[a].Cells["BILL_NO"].Value.ToString();
        //        DialogResult dr = MessageBox.Show("是否确认审核？", "提示", MessageBoxButtons.OKCancel);
        //        if (dr == DialogResult.OK)
        //        {
        //            DataTable TailorCheckDt = GetDgvToTable(dataGridView2);
        //            Dictionary<string, object> p = new Dictionary<string, object>
        //            {
        //                {"bill_no", bill_no}, {"data", TailorCheckDt}
        //            };
        //            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "TailorBillCheck", Program.client.UserToken, JsonConvert.SerializeObject(p));
        //            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //            {
        //                TailorRefresh();
        //                MessageHelper.ShowSuccess(this, "提交成功!");
        //            }
        //            else
        //            {
        //                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //            }
        //        }
        //    }
        //}

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView6.DataSource = null;
            if (dataGridView5.CurrentRow != null && dataGridView5.CurrentRow.Index > -1 && dataGridView5.SelectedRows.Count > 0)
            {
                int index = dataGridView5.CurrentRow.Index;
                var vBillNo = dataGridView5.Rows[index].Cells[0].Value == null ? "" : dataGridView5.Rows[index].Cells[0].Value.ToString();
                var vOPERATION_TYPE= dataGridView5.Rows[index].Cells[2].Value == null ? "" : dataGridView5.Rows[index].Cells[2].Value.ToString();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("BillNo", vBillNo);
                p.Add("OPERATION_TYPE", vOPERATION_TYPE);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetTailorBillDetail2", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString(); //5

                    TailorDt1 = JsonHelper.GetDataTableByJson(json);
                    dataGridView6.DataSource = TailorDt1.DefaultView;

                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);//表
                    int row = dataGridView6.Rows.Count;//行数
                    for (int i = 0; i < row; i++)
                    {
                        string train_no = dt.Rows[0]["train_no"].ToString();//趟数
                        string[] sArray = dt.Rows[0]["purchase_order_number"].ToString().Split(',');
                        for (int j = 0; j < sArray.Length; j++)
                        {
                            if (sArray.Length > 0)
                            {
                                //string[] arr = sArray[j].Split('-');
                                //if (train_no == arr[0])
                                //{
                                //    dataGridView6.Rows[i].Cells[6].Value = arr[1];
                                //}
                                for (int k = 0; k < sArray.Length; k++)
                                {
                                    string[] arr = sArray[k].Split('-');
                                    if (train_no == arr[0])
                                    {
                                        dataGridView6.Rows[i].Cells[5].Value =  arr[1];
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void dataGridView7_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView8.DataSource = null;
            if (dataGridView7.CurrentRow != null && dataGridView7.CurrentRow.Index > -1 && dataGridView7.SelectedRows.Count > 0)
            {
                int index = dataGridView7.CurrentRow.Index;
                var vBillNo = dataGridView7.Rows[index].Cells[0].Value == null ? "" : dataGridView7.Rows[index].Cells[0].Value.ToString();
                var vOPERATION_TYPE = dataGridView7.Rows[index].Cells[2].Value == null ? "" : dataGridView7.Rows[index].Cells[2].Value.ToString();
                Dictionary<string, object> p = new Dictionary<string, object>();
                p.Add("BillNo", vBillNo);
                p.Add("OPERATION_TYPE", vOPERATION_TYPE);
                string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetProcessBillDetail3", Program.client.UserToken, JsonConvert.SerializeObject(p));
                if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    ProcessDt1 = JsonHelper.GetDataTableByJson(json);
                    dataGridView8.DataSource = ProcessDt1.DefaultView;

                    DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    int row = dataGridView8.Rows.Count;
                    for (int i = 0; i < row; i++)
                    {
                        string train_no = dt.Rows[0]["train_no"].ToString();
                        string[] sArray = dt.Rows[0]["purchase_order_number"].ToString().Split(',');
                        for (int j = 0; j < sArray.Length; j++)
                        {
                            if (sArray.Length > 0)
                            {
                                //string[] arr = sArray[j].Split('-');
                                //if (train_no == arr[0])
                                //{
                                //    dataGridView8.Rows[i].Cells[6].Value = arr[1];
                                //}
                                for (int k = 0; k < sArray.Length; k++)
                                {
                                    string[] arr = sArray[k].Split('-');
                                    if (train_no == arr[0])
                                    {
                                        dataGridView8.Rows[i].Cells[6].Value = arr[1];
                                    }
                                }
                            }
                        }
                    }

                    //DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    //int row = dataGridView8.Rows.Count;
                    //for (int i = 0; i < row; i++)
                    //{
                    //    int train_no = (int)decimal.Parse(dt.Rows[0]["train_no"].ToString());
                    //    string[] sArray = dt.Rows[0]["purchase_order_number"].ToString().Split(',');

                    //    for (int j = 0; j < sArray.Length; j++)
                    //    {
                    //        if (train_no > 0)
                    //        {
                    //            string PON = dt.Rows[0]["purchase_order_number"].ToString().Split(',')[train_no - 1];
                    //            string purchase_order_number = PON.ToString().Split('-')[1];
                    //            dataGridView8.Rows[i].Cells[6].Value = purchase_order_number;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void btnProcessDetailedQuery_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("vDept", Regex.Split(txtVendor.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vWorkDate", dateTimePicker4.Value.ToString("yyyy/MM/dd"));
            p.Add("vType", cbTailor3.SelectedValue.ToString());
            p.Add("vStatus", cbStatus3.SelectedValue.ToString());
            p.Add("vBILL_NO", textBox6.Text);
            p.Add("vWareHouse", Regex.Split(textBox7.Text, "\\s+", RegexOptions.IgnoreCase)[0]);
            p.Add("vCreateUser", this.txtCreateUser4.Text);
            string comboBox1Value = cbStatus3.SelectedValue.ToString();
            
            btnProcessAudit.Enabled = comboBox1Value != "Y";

            if (string.IsNullOrEmpty(comboBox1Value))
            {
                btnProcessAudit.Enabled = false;
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_NewBillOrderServer", "GetProcessBill", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                dataGridView7.DataSource = dtJson.DefaultView;
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }
        //详细的审核
        //private void btnProcessAudit_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView7.CurrentRow != null && dataGridView7.CurrentRow.Index > -1 && dataGridView7.SelectedRows.Count > 0)
        //    {
        //        int a = dataGridView7.CurrentRow.Index;
        //        string bill_no = dataGridView7.Rows[a].Cells["V_bill_no"].Value.ToString();
        //        DialogResult dr = MessageBox.Show("是否确认审核？", "提示", MessageBoxButtons.OKCancel);
        //        if (dr == DialogResult.OK)
        //        {
        //            DataTable ProcessDt = GetDgvToTable(dataGridView4);
        //            Dictionary<string, object> p = new Dictionary<string, object>();
        //            p.Add("bill_no", bill_no);
        //            p.Add("data", ProcessDt);
        //            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "ProcessBillCheck", Program.client.UserToken, JsonConvert.SerializeObject(p));
        //            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
        //            {
        //                VendRefresh();
        //                MessageHelper.ShowSuccess(this, "提交成功!");
        //            }
        //            else
        //            {
        //                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
        //            }
        //        }
        //    }
        //}

        private void btnExportProcess_Click(object sender, EventArgs e)
        {
            if (dataGridView7.Rows.Count > 0)
            {
                if (dataGridView7.CurrentRow != null)
                {
                    int index = dataGridView7.CurrentRow.Index;
                    string type = dataGridView7.Rows[index].Cells[2].Value.ToString();
                    DataTable dataTablePoint = ProcessDt1.Copy();
                    for (int i = 0; i < dataTablePoint.Rows.Count; i++)
                    {
                        string str = dataTablePoint.Rows[i]["T_ORDER_NO"].ToString();
                        string str1 = dataTablePoint.Rows[i]["sales_order"].ToString();
                        string temp_order = "";
                        string temp_sales = "";
                        string[] vs = str.Split(',');
                        string[] vs1 = str1.Split(',');
                        foreach (var s in vs)
                        {
                            string t1 = s.Remove(0, 3);
                            Console.WriteLine(t1);
                            int kk = int.Parse(t1);
                            temp_order += kk + ",";
                        }
                        foreach (var s in vs1)
                        {
                            string t1 = s.Remove(0, 1);
                            Console.WriteLine(t1);
                            int kk = int.Parse(t1);
                            temp_sales += kk + ",";
                        }

                        temp_order = temp_order.Remove(temp_order.Length - 1, 1);
                        temp_sales = temp_sales.Remove(temp_sales.Length - 1, 1);
                        dataTablePoint.Rows[i]["T_ORDER_NO"] = temp_order;
                        dataTablePoint.Rows[i]["sales_order"] = temp_sales;
                    }

                    DataRow dataRow = WinFormLib.TotalRow.GetTotalRow(dataTablePoint);
                    dataRow[dataTablePoint.Columns["SALES_ORDER"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["T_ORDER_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["SIZE_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["TRAIN_NO"]] = string.Empty;
                    dataRow[dataTablePoint.Columns["T_QUANTITY"]] = "合计";
                    dataTablePoint.Rows.Add(dataRow);
                    var reportType = type.Equals("B") ? "\\中仓出厂商1_工单版.frx" : "\\厂商入中仓1_工单版.frx";
                    string path = Application.StartupPath + @"\报表" + reportType;
                    InOutBillAddOrder_MMSPrint frm = new InOutBillAddOrder_MMSPrint(dataTablePoint, path);
                    frm.Show();
                }
            }
        }


        //加载送外厂商
        private void LoadReceivingDepartment()
        {
            var source = new AutoCompleteStringCollection();   //存放数据库查询结果
            Dictionary<string, string> p = new Dictionary<string, string>();
         
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_MMSAPI", "KZ_MMSAPI.Controllers.MMS_InOut_Bill_OrderServer", "LoadReceivingDepartment", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = JsonHelper.GetDataTableByJson(json);
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    source.Add(dtJson.Rows[i - 1]["MANAGEMENTAREA_NO"] + "  " + dtJson.Rows[i - 1]["MANAGEMENTAREA_MEMO"]);
                }
                comboxVend_No.AutoCompleteCustomSource = source;
                comboxVend_No.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboxVend_No.AutoCompleteSource = AutoCompleteSource.CustomSource;

                textBox3.AutoCompleteCustomSource = source;
                textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;

                textBox5.AutoCompleteCustomSource = source;
                textBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox5.AutoCompleteSource = AutoCompleteSource.CustomSource;

                textBox7.AutoCompleteCustomSource = source;
                textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource; 
            }
            else
            {
                MessageHelper.ShowErr(this, JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void dataGridView4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void dataGridView6_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void dataGridView8_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView2.SelectedCells)
                {
                    if (dgvCell.ColumnIndex == 10)
                    {
                        if (dgvCell.Value == null)
                        {
                            break;
                        }
                        if (IsNumeric(dgvCell.Value.ToString()))
                        {
                            pp += Convert.ToDecimal(dgvCell.Value.ToString());
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                this.label28.Text = pp.ToString();
                this.label28.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception("选中合计报错了"+ex.Message);
            }
        }
        private bool IsNumeric(string number)
        {
            try
            {
                decimal.Parse(number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView4.SelectedCells)
                {
                    if (dgvCell.ColumnIndex == 13)
                    {
                        if (dgvCell.Value == null)
                        {
                            break;
                        }
                        if (IsNumeric(dgvCell.Value.ToString()))
                        {
                            pp += Convert.ToDecimal(dgvCell.Value.ToString());
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                this.label31.Text = pp.ToString();
                this.label31.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception("选中合计报错了" + ex.Message);
            }
        }

        private void dataGridView6_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView6.SelectedCells)
                {
                    if (dgvCell.ColumnIndex == 9)
                    {
                        if (dgvCell.Value == null)
                        {
                            break;
                        }
                        if (IsNumeric(dgvCell.Value.ToString()))
                        {
                            pp += Convert.ToDecimal(dgvCell.Value.ToString());
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                this.label33.Text = pp.ToString();
                this.label33.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception("选中合计报错了" + ex.Message);
            }
        }

        private void dataGridView8_SelectionChanged(object sender, EventArgs e)
        {
            decimal pp = 0;
            try
            {
                foreach (DataGridViewCell dgvCell in dataGridView8.SelectedCells)
                {
                    if (dgvCell.ColumnIndex == 10)
                    {
                        if (dgvCell.Value == null)
                        {
                            break;
                        }
                        if (IsNumeric(dgvCell.Value.ToString()))
                        {
                            pp += Convert.ToDecimal(dgvCell.Value.ToString());
                        }
                        else
                        {
                            continue;
                        }
                    }
                }

                this.label35.Text = pp.ToString();
                this.label35.Visible = true;
            }
            catch (Exception ex)
            {
                throw new Exception("选中合计报错了" + ex.Message);
            }
        }
    }

    public class ComboboxEntry
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
