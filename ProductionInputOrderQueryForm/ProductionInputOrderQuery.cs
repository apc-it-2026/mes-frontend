using AutocompleteMenuNS;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewExportExcels;

namespace ProductionInputOrderQuery
{
    public partial class ProductionInputOrderQuery : MaterialForm
    {
        public Boolean isTitle = false;
        public class StatusEntry
        {
            public string Code { get; set; }

            public string Name { get; set; }
        }

        public class RoutEntry
        {
            public string Code { get; set; }

            public string Name { get; set; }
        }

        public ProductionInputOrderQuery()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            this.WindowState = FormWindowState.Maximized;

            tabControl1.TabPages.Remove(tabPage3);

            
        }

        private void ProductionInputOrderQueryForm_Load(object sender, EventArgs e)
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            //Departmental information reminder
            LoadDepts();
            //LoadPOs();
            //Multilingual for drop down boxes
            GetComboBoxUI();
            //load factory
            GetAllOrgInfo();
            //Load process
            GetAllRoutNo();
            //Multilingual update
           
            this.dataGridView1.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };
            this.dataGridView2.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };
            this.dataGridView3.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };

        }

        private void LoadDepts()
        {
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private DataTable GetDepts()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private DataTable GetAllOrgInfo()   //Get all factory information
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetAllOrgInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    orderSource.Add(dt.Rows[i]["ORG_CODE"].ToString() + " | " + dt.Rows[i]["ORG_NAME"].ToString());
                }
                //Planned output variance query [factory code] binding data source
                textBox10.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox10.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox10.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

                //Query [factory code] binding data source according to daily production instructions
                textOrgId.AutoCompleteCustomSource = orderSource;   //bind data source
                textOrgId.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textOrgId.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

                //Query the [factory code] binding data source according to the investment time
                textBox12.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox12.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private DataTable GetAllRoutNo()    //Get all processes
        {
            List<StatusEntry> statusEntries = new List<StatusEntry> { };
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralOrderServer", "GetAllRoutNo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    statusEntries.Add(new StatusEntry() { Code = dt.Rows[i]["ROUT_NO"].ToString(), Name = dt.Rows[i]["ROUT_NO"].ToString() });
                }

                this.comboBox1.DataSource = statusEntries;
                this.comboBox1.DisplayMember = "Name";
                this.comboBox1.ValueMember = "Code";

                this.comboBoxRoutNo.DataSource = statusEntries;
                this.comboBoxRoutNo.DisplayMember = "Name";
                this.comboBoxRoutNo.ValueMember = "Code";

                this.comboBox2.DataSource = statusEntries;
                this.comboBox2.DisplayMember = "Name";
                this.comboBox2.ValueMember = "Code";
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void GetComboBoxUI()
        {
            List<StatusEntry> statusEntries = new List<StatusEntry> { };
            Dictionary<string, Object> p = new Dictionary<string, object>();
         
            p.Add("enmu_type", "statusEntries");
            string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ComboBoxUIServer", "GetComboBoxUI", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    statusEntries.Add(new StatusEntry() { Code = dtJson.Rows[i]["ENUM_CODE"].ToString(), Name = dtJson.Rows[i]["ENUM_VALUE"].ToString() });
                }
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["ErrMsg"].ToString());
            }
       
            this.comboBoxStatus.DataSource = statusEntries;
            this.comboBoxStatus.DisplayMember = "Name";
            this.comboBoxStatus.ValueMember = "Code";

            this.cbStatus.DataSource = statusEntries;
            this.cbStatus.DisplayMember = "Name";
            this.cbStatus.ValueMember = "Code";

        }

        private bool Vail_01()
        {
            bool resultValue = false;
            if (!(string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrWhiteSpace(textBox10.Text)))
            {
                resultValue = true;
            }
            return resultValue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Vail_01())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered！");
                return;
            }

            //if (textBox10.Text != "5001" || textBox10.Text != "5011" || textBox10.Text != "5041")
            //{
            //    SJeMES_Control_Library.MessageHelper.ShowErr(this,"Please Enter Correct Org Code");
            //}


            Cursor.Current = Cursors.WaitCursor;

            this.dataGridView3.DataSource = null;
            this.dataGridView3.AutoGenerateColumns = false;
            Dictionary<string, Object> p = new Dictionary<string, object>();

            string vSeId = textBox5.Text.ToString();
            string vPO = textBox1.Text.ToString();
            string vDept = textBox6.Text.ToString().ToUpper();
            string vArt = textBox4.Text.ToString();
            string vRoutNo = comboBox1.Text.ToString();
            string vMainProdOrder = textBox9.Text.ToString();
            string vOrgId = textBox10.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0];

            p.Add("vSeId", vSeId);
            p.Add("vPO", vPO);
            p.Add("vDept", vDept);
            p.Add("vArt", vArt);
            p.Add("vRoutNo", vRoutNo);
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vOrgId", vOrgId);
            p.Add("vInOut", "IN");
            p.Add("title", this.Text);
            p.Add("art_name", textBox15.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetWorkDaySizeReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Cursor.Current = Cursors.Default;
                if (dtJson.Rows.Count > 0)
                {   //Generate "total" row
                    DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow1[dtJson.Columns["PO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["WORKORDER_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["MAIN_PROD_ORDER"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["d_dept"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ART_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["art_name"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["size_no"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ROUT_NO"]] = "Total";
                    dtJson.Rows.Add(TotaldgvRow1);
                }

                else
                {
                    //MessageBox.Show("No Data Found");               // Newly Added by Shyam
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");                // Newly Added by Shyam
                }
                dataGridView3.DataSource = dtJson.DefaultView;
                dataGridView3.Update();

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void Export01_Click(object sender, EventArgs e)
        {

            //string a = "Production Input Summary Report.xls";
            //ExportExcels.Export(a, dataGridView3);                                       // Old Code

            if (dataGridView3.Rows.Count <= 0)                                            // New Code
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Input by Summary Report.xls";
                ExportExcels.Export(a, dataGridView3);
            }
        }

        private void Export02_Click(object sender, EventArgs e)
        {
            //string a = "Input by Day Wise Report.xls";
            //ExportExcels.Export(a, dataGridView2);


            if (dataGridView2.Rows.Count <= 0)                                            // New Code
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Input by Summary Report.xls";
                ExportExcels.Export(a, dataGridView2);
            }

        }

        private void Export03_Click(object sender, EventArgs e)
        {
            //string a = "Input by Time Report.xls";
            //ExportExcels.Export(a, dataGridView1);

            if (dataGridView3.Rows.Count <= 0)                                            // New Code
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Input by Summary Report.xls";
                ExportExcels.Export(a, dataGridView1);
            }

        }

        private bool Vail_02()
        {
            bool resultValue = false;
            if (!(string.IsNullOrEmpty(textOrgId.Text) || string.IsNullOrWhiteSpace(textOrgId.Text)))
            {
                resultValue = true;
            }
            return resultValue;
        }

        private void btn_query_Click(object sender, EventArgs e)
        {
            if (!Vail_02())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered!");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;                        // Newly Added by Shyam

            this.dataGridView2.DataSource = null;
            this.dataGridView2.AutoGenerateColumns = false;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSeId", textSeId.Text.ToString());
            p.Add("vWrokDayStart", dtpWorkDayStart.Value.ToString("yyyy/MM/dd"));
            p.Add("vWrokDayEnd", dtpWorkDayEnd.Value.ToString("yyyy/MM/dd"));
            p.Add("vDept", textDDept.Text.ToString().ToUpper());
            p.Add("vPO", textPO.Text.ToString());
            p.Add("vArt", textArt.Text.ToString());
            p.Add("vRoutNo", comboBoxRoutNo.Text.ToString());
            p.Add("vStatus", comboBoxStatus.SelectedValue.ToString());
            p.Add("vMainProdOrder", textMainProdOrder.Text.ToString());
            p.Add("vOrgId", textOrgId.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0]);
            p.Add("vInOut", "IN");
            p.Add("title", this.Text);
            p.Add("art_name", textBox14.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetWorkDayReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Cursor.Current = Cursors.Default;                                            // Newly Added by Shyam
                if (dtJson.Rows.Count > 0)
                {   //Generate "total" row
                    DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow2[dtJson.Columns["PO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["WORKORDER_NO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["MAIN_PROD_ORDER"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["d_dept"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["work_day"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["ART_NO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["art_name"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["size_no"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["ROUT_NO"]] = "Total";
                    dtJson.Rows.Add(TotaldgvRow2);
                }

                else
                {
                    //MessageBox.Show("No Data Found");               // Newly Added by Shyam
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");                // Newly Added by Shyam

                }
                dataGridView2.DataSource = dtJson.DefaultView;
                dataGridView2.Update();

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private bool Vail_03()
        {
            bool resultValue = false;
            if (!(string.IsNullOrEmpty(textBox12.Text) || string.IsNullOrWhiteSpace(textBox12.Text)))
            {
                resultValue = true;
            }
            return resultValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Vail_03())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered！");
                return;
            }

            Cursor.Current = Cursors.Default;                                                // New Code

            this.dataGridView1.DataSource = null;
            this.dataGridView1.AutoGenerateColumns = false;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            string vSeId = textBox3.Text.ToString();
            string vPO = textBox8.Text.ToString();
            string vWrokDayStart = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string vWrokDayEnd = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            string vDept = textBox7.Text.ToString().ToUpper();
            string vArt = textBox2.Text.ToString();
            string vProcessNo = comboBox2.Text.ToString();
            string vProdOrder = textBox11.Text.ToString();
            string vOrgId = textBox12.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0];

            p.Add("vSeId", vSeId);
            p.Add("vPO", vPO);
            p.Add("vWrokDayStart", vWrokDayStart);
            p.Add("vWrokDayEnd", vWrokDayEnd);
            p.Add("vDept", vDept);
            p.Add("vArt", vArt);
            p.Add("vProcessNo", vProcessNo);
            p.Add("vProdOrder", vProdOrder);
            p.Add("vOrgId", vOrgId);
            p.Add("title", this.Text);
            p.Add("art_name", textBox13.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetMesLableDReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                Cursor.Current = Cursors.Default;                                           // New Code

                if (dtJson.Rows.Count > 0)
                {   //Generate "total" row
                    DataRow TotaldgvRow3 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow3[dtJson.Columns["PO_NO"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["WORKORDER_NO_3"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["MAIN_PROD_ORDER"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["SCAN_DETPT"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["SCAN_DATE"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["ART_NO"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["ART_NAME"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["SIZE_NO"]] = string.Empty;
                    TotaldgvRow3[dtJson.Columns["PROCESS_NO"]] = "Total";
                    dtJson.Rows.Add(TotaldgvRow3);
                }
                else
                {
                    //MessageBox.Show("No Data Found");               // Newly Added by Shyam
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");                // Newly Added by Shyam
                }

                dataGridView1.DataSource = dtJson.DefaultView;
                dataGridView1.Update();

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        
    }
}
