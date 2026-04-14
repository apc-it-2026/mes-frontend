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

namespace ProductionOutputOrderQuery
{
    public partial class ProductionOutputOrderQuery : MaterialForm
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

        public ProductionOutputOrderQuery()
        {
            InitializeComponent();

            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);

            this.WindowState = FormWindowState.Maximized;
        }

        private void ProductionOutputOrderQueryForm_Load(object sender, EventArgs e)
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            //Departmental information reminder
            LoadDepts();
            //LoadPOs();
            //Multilingual for drop down boxes
            GetComboBoxUI();
            //Get factory information
            GetAllOrgInfo();
            //Get the process
            GetAllRoutNo();
            //Multilingual update
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            this.dataGridView2.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };
            this.dataGridView3.DataError += delegate (object obj, DataGridViewDataErrorEventArgs eve) { };

            dtpWorkDayStart.Text = DateTime.Now.ToShortDateString();
            dtpWorkDayEnd.Text = DateTime.Now.ToShortDateString();

            GetPlantInfo(); //Add by Venkat 2024/05/07
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

        //Get order list
        private DataTable GetPOs()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.StitchingInOutServer", "GetOrdM", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                int count = dt.Rows.Count;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
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
                //Query [factory code] binding data source according to daily production instructions
                textOrgId.AutoCompleteCustomSource = orderSource;   //bind data source
                textOrgId.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textOrgId.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

                //Planned output variance query [factory code] binding data source
                textOrgId_plan.AutoCompleteCustomSource = orderSource;   //bind data source
                textOrgId_plan.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textOrgId_plan.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties

                //Planned output detail query [factory code] binding data source //add by venkat 2024/05/17
                t3_txtOrgId.AutoCompleteCustomSource = orderSource;   //bind data source
                t3_txtOrgId.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                t3_txtOrgId.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private DataTable GetAllRoutNo()    //Get all the processes that have been reported
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
                //Query [Process] Binding data source according to daily production instructions
                this.comboBoxRoutNo.DataSource = statusEntries;
                this.comboBoxRoutNo.DisplayMember = "Name";
                this.comboBoxRoutNo.ValueMember = "Code";

                //Planned output variance query【Process】Binding data source
                this.comboBoxRoutNo_plan.DataSource = statusEntries;
                this.comboBoxRoutNo_plan.DisplayMember = "Name";
                this.comboBoxRoutNo_plan.ValueMember = "Code";


                //Planned output Detailed report add by venkat 2024/05/16
                this.comboBox3.DataSource = statusEntries;
                this.comboBox3.DisplayMember = "Name";
                this.comboBox3.ValueMember = "Code";
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

        private void LoadPOs()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetPOs();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(
                    new[] { n + "", dt.Rows[i]["SE_ID"].ToString() + " " + dt.Rows[i]["MER_PO"].ToString() + " " + dt.Rows[i]["PROD_NO"].ToString() }, dt.Rows[i]["SE_ID"].ToString() + "|" + dt.Rows[i]["MER_PO"].ToString() + "|" + dt.Rows[i]["PROD_NO"].ToString())
                { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
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

            this.comboBox4.DataSource = statusEntries;
            this.comboBox4.DisplayMember = "Name";
            this.comboBox4.ValueMember = "Code";

        }

        private bool Vail_01()
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
            if (!Vail_01())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered！");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;                            // Newly Added by Shyam

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
            p.Add("vInOut", "OUT");
            p.Add("title", this.Text);
            p.Add("art_name", textBox3.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetWorkDayReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                Cursor.Current = Cursors.Default;                            // Newly Added by Shyam

                if (dtJson.Rows.Count > 0)
                {
                    DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow1[dtJson.Columns["po"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["WORKORDER_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["MAIN_PROD_ORDER"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["d_dept"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["work_day"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["art_no"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["art_name"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["size_no"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ROUT_NO"]] = "Total";
                    dtJson.Rows.Add(TotaldgvRow1);
                }
                else
                {                                                      // Newly Added by Shyam
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

        private bool Vail_02()
        {
            bool resultValue = false;
            if (!(string.IsNullOrEmpty(textOrgId_plan.Text) || string.IsNullOrWhiteSpace(textOrgId_plan.Text)))
            {
                resultValue = true;
            }
            return resultValue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Vail_02())
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered！");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;                            // Newly Added by Shyam

            this.dataGridView3.DataSource = null;
            this.dataGridView3.AutoGenerateColumns = false;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            string vSeId = textBox5.Text.ToString();
            string vPO = textBox1.Text.ToString();
            string vDept = textBox6.Text.ToString().ToUpper();
            string vArt = textBox4.Text.ToString();
            string vRoutNo = comboBoxRoutNo_plan.Text.ToString();
            string vMainProdOrder = textMainProdOrder_plan.Text.ToString();
            string vOrgId = textOrgId_plan.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0];

            p.Add("vSeId", vSeId);
            p.Add("vPO", vPO);
            p.Add("vDept", vDept);
            p.Add("vArt", vArt);
            p.Add("vRoutNo", vRoutNo);
            p.Add("vMainProdOrder", vMainProdOrder);
            p.Add("vOrgId", vOrgId);
            p.Add("vInOut", "OUT");
            p.Add("title", this.Text);
            p.Add("art_name", textBox2.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetWorkDaySizeReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                Cursor.Current = Cursors.Default;                            // Newly Added by Shyam

                if (dtJson.Rows.Count > 0)
                {
                    DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dtJson);        //return DataRow
                    TotaldgvRow2[dtJson.Columns["PO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["SE_ID"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["WORKORDER_NO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["MAIN_PROD_ORDER"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["d_dept"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["ART_NO"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["art_name"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["size_no"]] = string.Empty;
                    TotaldgvRow2[dtJson.Columns["ROUT_NO"]] = "Total";
                    dtJson.Rows.Add(TotaldgvRow2);
                }

                else
                {                                                      // Newly Added by Shyam
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
            //string a = "Day Wise Output Query.xls";
            //ExportExcels.Export(a, dataGridView2);

            if (dataGridView2.Rows.Count <= 0)                                            // New Code by Shyam
            {
                MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Total Summary Report.xls";
                ExportExcels.Export(a, dataGridView2);
            }

        }

        private void Export02_Click(object sender, EventArgs e)
        {
            //string a = "Production Summary.xls";
            //ExportExcels.Export(a, dataGridView3);

            if (dataGridView3.Rows.Count <= 0)                                            // New Code by Shyam
            {
                //MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Total Summary Report.xls";
                ExportExcels.Export(a, dataGridView3);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //Begin Add By Venkat 2024/05/16
        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(t3_txtOrgId.Text) || string.IsNullOrWhiteSpace(t3_txtOrgId.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Factory code not entered！");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            this.dataGridView4.DataSource = null;
            this.dataGridView4.AutoGenerateColumns = false;

            Dictionary<string, Object> p = new Dictionary<string, object>();

            p.Add("vSeId", textBox19.Text.ToString());
            p.Add("vWrokDayStart", dateTimePicker4.Value.ToString("yyyy/MM/dd HH:mm"));
            p.Add("vWrokDayEnd", dateTimePicker3.Value.ToString("yyyy/MM/dd HH:mm"));
            p.Add("vPlant", textBox15.Text.ToString().ToUpper());
            p.Add("vDept", textBox20.Text.ToString().ToUpper());
            p.Add("vPO", textBox18.Text.ToString());
            p.Add("vArt", textBox17.Text.ToString());
            p.Add("vRoutNo", comboBox3.Text.ToString());
            p.Add("vStatus", comboBox4.SelectedValue.ToString());
            p.Add("vMainProdOrder", textBox16.Text.ToString());
            p.Add("vOrgId", t3_txtOrgId.Text.Trim().ToString().ToUpper().Split(new Char[] { '|' })[0]);
            p.Add("vInOut", "OUT");
            //p.Add("title", this.Text);
            //p.Add("art_name", textBox3.Text);

            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.ProductionReportOrderServer", "GetWorkDetailReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                Cursor.Current = Cursors.Default;

                if (dtJson.Rows.Count > 0)
                {
                    DataRow TotaldgvRow1 = WinFormLib.TotalRow.GetTotalRow(dtJson);
                    TotaldgvRow1[dtJson.Columns["ORG_ID"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SCAN_DATE"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["PLANT"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["DEPARTMENT_NAME"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["PO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SO_SEQ"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["MAIN_WORK_ORDER"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SUB_WORK_ORDER"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["SIZE_NO"]] = "Total";
                    TotaldgvRow1[dtJson.Columns["PROCESS_NO"]] = string.Empty;
                    TotaldgvRow1[dtJson.Columns["ART_NO"]] = string.Empty;
                    dtJson.Rows.Add(TotaldgvRow1);
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found！");
                }
                dataGridView4.DataSource = dtJson.DefaultView;
                dataGridView4.Update();

            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count <= 0)                                            // New Code by Shyam
            {
                MessageBox.Show("No Data Found");
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                string a = "Production Output Report by Detail.xls";
                ExportExcels.Export(a, dataGridView4);
            }

        }



        private DataTable GetPlantInfo()   //Get all plant information
        {
            var orderSource = new AutoCompleteStringCollection();   //Store database query results
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOrgInfo", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    orderSource.Add(dt.Rows[i]["CODE"].ToString());
                }

                //Pquery [Plant code] binding data source //add by venkat 2024/05/17
                textBox15.AutoCompleteCustomSource = orderSource;   //bind data source
                textBox15.AutoCompleteMode = AutoCompleteMode.Suggest;    //Show related dropdown
                textBox15.AutoCompleteSource = AutoCompleteSource.CustomSource;   //set properties
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
            return dt;
        }

    }
}
