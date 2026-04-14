
using AutocompleteMenuNS;
using NewExportExcels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML;
using ClosedXML.Excel;
using System.IO;

namespace Efficiency_Tracking_Report
{
    public partial class Efficiency_Tracking_Report : Form
    {

        DataTable dtJson;
        public Efficiency_Tracking_Report()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Efficiency_Tracking_Report_Load(object sender, EventArgs e)
        {
            LoadOrgId();
            LoadPlant();
            LoadRoutNo();
            LoadDepts();

            LoadEfficiencyReport();

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

                CB_Org.DataSource = WMSorgEntries;
                CB_Org.DisplayMember = "Name";
                CB_Org.ValueMember = "Code";


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
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

        private DataTable GetDepts()
        {
            DataTable dt = null;
            Dictionary<string, Object> p = new Dictionary<string, object>();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
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

        private void LoadPlant()
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
            CB_Plants.DataSource = items1;
            //combobox_plant.DataSource = items1;
            //comboBox3.DataSource = items1;

        }

        private void LoadRoutNo()
        {
            var items4 = new List<AutocompleteItem>();
            string ret4 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.MES_Hourly_Management_Server", "LoadRoutNo", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject("noParam"));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret4)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                items4.Add(new MulticolumnAutocompleteItem(new[] { "" }, ""));
                for (int i = 1; i <= dtJson.Rows.Count; i++)
                {
                    items4.Add(new MulticolumnAutocompleteItem(new[] { dtJson.Rows[i - 1]["rout_no"].ToString(), dtJson.Rows[i - 1]["rout_name_z"].ToString() }, dtJson.Rows[i - 1]["rout_no"].ToString() + "|" + dtJson.Rows[i - 1]["rout_name_z"].ToString()));
                }
            }
            CB_Process.DataSource = items4;
            //combobox_process.DataSource = items4;
            //comboBox1.DataSource = items4;
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            LoadEfficiencyReport();
        }

        public void LoadEfficiencyReport()
        {
            this.dataGridView1.DataSource = null;
            try
            {
                GetEfficiencyReport();
            }
            catch (Exception ex)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, ex.Message);
            }
        }

        private void GetEfficiencyReport()
        {
            Dictionary<string, Object> dic = new Dictionary<string, Object>();

            dic.Add("Date", dateTimePicker1.Text);
            dic.Add("vCompany", CB_Org.SelectedValue.ToString());
            dic.Add("vPlant", string.IsNullOrWhiteSpace(CB_Plants.Text) ? CB_Plants.Text : CB_Plants.Text.Split('|')[0]);
            dic.Add("vProcess", string.IsNullOrWhiteSpace(CB_Process.Text) ? CB_Process.Text : CB_Process.Text.Split('|')[0]);
            dic.Add("vDept", Txt_Lines.Text);
            dic.Add("vModelName", Txt_Model.Text.ToUpper());
            Cursor.Current = Cursors.WaitCursor;
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL, "KZ_RTLAPI", "KZ_RTLAPI.Controllers.GeneralServer", "GetEfficiencyReport", Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dic));
            Cursor.Current = Cursors.Default;
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if(dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson;
                    adjustColumnWidth();
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this,"No Data Found");
                }


            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }

        }

        private void adjustColumnWidth()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dataGridView1.Columns.Contains("MODELNAME"))
            {
                dataGridView1.Columns["MODELNAME"].Width = 250;  // Set the desired width
            }

        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            AllClear();
        }

        private void AllClear()
        {
            CB_Org.ResetText();
            CB_Plants.ResetText();
            CB_Process.ResetText();
            Txt_Lines.Clear();
        }

        private void Btn_ExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                sfd.FileName = "Efficiency Report";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    //string a = "Efficiency Tracking Report";
                    //ExportExcels.Export(a, dataGridView1);

                    DataSet ds = new DataSet();
                    byte[] excelbytes = null;

                    ds.Tables.Add(dtJson);
                    ds.Tables[0].TableName = "Efficiency Report";

                    XLWorkbook ExcelData = new XLWorkbook();

                    foreach (DataTable dt in ds.Tables)
                    {
                        #region Add Serial No Code

                        //if (dt.Rows.Count > 0)
                        //{
                        //    dt.Columns.Add("S.No", typeof(int)).SetOrdinal(0);
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        for (int i = 0; i < dt.Rows.Count; i++)
                        //        {
                        //            dt.Rows[i]["S.No"] = i + 1;
                        //        }
                        //    }

                        //}

                        #endregion


                        var adjustcolumns = ExcelData.Worksheets.Add(dt);
                        adjustcolumns.Columns().AdjustToContents();


                        //var headerrow = adjustcolumns.Row(1);
                        //headerrow.Style.Font.FontName = "Calisto MT";
                        //headerrow.Style.Font.Bold = true;

                        adjustcolumns.Style.Font.FontName = "Thaoma";
                        adjustcolumns.Style.Font.FontSize = 10;


                    }

                    MemoryStream iMemoryStream = new MemoryStream();
                    ExcelData.SaveAs(iMemoryStream);
                    excelbytes = iMemoryStream.ToArray();
                    iMemoryStream.Close();


                    string filePath = sfd.FileName;

                    // Save the Excel file to the selected location
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        fileStream.Write(excelbytes, 0, excelbytes.Length);
                    }
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, " Excel Saved Successfully !");
                }
               


            }
        }
















    }
}
