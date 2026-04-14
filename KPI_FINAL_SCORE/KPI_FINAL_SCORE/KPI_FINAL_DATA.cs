using AutocompleteMenuNS;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPI_FINAL_SCORE
{
    public partial class KPI_FINAL_DATA : MaterialForm
    {
        Emp_KPI ter_emp = new Emp_KPI();
        public KPI_FINAL_DATA()
        {
            InitializeComponent();
        }
        public void KPIFinalScore(string Criteria,string Plant,string Type)
        {

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Month", Month.Text.Replace("/", ""));
            Data.Add("ProdLine", ProdLine.Text);
            Data.Add("Criteria", Criteria);
            Data.Add("Plant", Plant);
            Data.Add("Type", Type);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "KPIFinalScore", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dtJson;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }

        }
        public void LoadProdLine()
        {
            ProdLine.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ProdLine.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "KPIProdLine", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };

                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["DEPARTMENT_CODE"].ToString() }, dtJson.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }
                }
            }
        }

        public void LoadPlantList()
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "LoadPlantList", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    // Insert an empty row at the top
                    DataRow emptyRow = dtJson.NewRow();
                    emptyRow["udf05"] = ""; // Replace with your actual display column name
                    emptyRow["udf05"] = DBNull.Value; // Optional: set to DBNull or 0
                    dtJson.Rows.InsertAt(emptyRow, 0);

                    // ComboBox4 binding
                    comboBox8.DataSource = dtJson.Copy();
                    comboBox8.DisplayMember = "udf05"; // replace with actual column name
                    comboBox8.ValueMember = "udf05";     // replace with actual column name
                    comboBox8.SelectedIndex = 0;

                    // ComboBox5 binding
                    comboBox6.DataSource = dtJson.Copy();
                    comboBox6.DisplayMember = "udf05";
                    comboBox6.ValueMember = "udf05";
                    comboBox6.SelectedIndex = 0;
                }
            }
        }

        public void LoadCriteria()
        {
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "LoadCriteria", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if(dtJson.Rows.Count>0)
                {
                    // Insert an empty row at the top
                    DataRow emptyRow = dtJson.NewRow();
                    emptyRow["criteria_name"] = ""; // Replace with your actual display column name
                    emptyRow["criteria_name"] = DBNull.Value; // Optional: set to DBNull or 0
                    dtJson.Rows.InsertAt(emptyRow, 0);

                    // ComboBox4 binding
                    comboBox4.DataSource = dtJson.Copy();
                    comboBox4.DisplayMember = "criteria_name"; // replace with actual column name
                    comboBox4.ValueMember = "criteria_name";     // replace with actual column name
                    comboBox4.SelectedIndex = 0;

                    // ComboBox5 binding
                    comboBox5.DataSource = dtJson.Copy();
                    comboBox5.DisplayMember = "criteria_name";
                    comboBox5.ValueMember = "criteria_name";
                    comboBox5.SelectedIndex = 0;
                }
               
            }
        }

        public void LoadPosition(string Month)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("Month", Month);  
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "LoadPosition", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    DataRow emptyRow = dtJson.NewRow();
                    emptyRow["EMP_ROLE"] = "";
                    dtJson.Rows.InsertAt(emptyRow, 0);
                    comboBox1.DataSource = dtJson;
                    comboBox1.DisplayMember = "EMP_ROLE";  
                    comboBox1.ValueMember = "EMP_ROLE";     
                }
                else
                {
                    comboBox1.DataSource = null;
                    comboBox1.Items.Clear(); 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, $@"Employee Data of Month {Month} Not yet uploaded");
                }

            }
        }

        public void LoadPlant(string Month)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("Month", Month);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "LoadPlant", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson != null && dtJson.Rows.Count > 0)
                {
                    DataRow emptyRow = dtJson.NewRow();
                    emptyRow["PLANT"] = "";  
                    dtJson.Rows.InsertAt(emptyRow, 0); 
                    comboBox2.DataSource = dtJson;
                    comboBox2.DisplayMember = "PLANT";  
                    comboBox2.ValueMember = "PLANT";     
                }
                else
                {
                    comboBox2.DataSource = null;
                    comboBox2.Items.Clear(); 
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, $@"Employee Data of Month {Month} Not yet uploaded");
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            KPIFinalScore(comboBox4.Text, comboBox6.Text, comboBox7.Text);
        }

        private void KPI_FINAL_DATA_Load(object sender, EventArgs e)
        {
            LoadProdLine();
            LoadCriteria();
            LoadPlantList();
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "MonthWise_KPI_Final_Data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DayWiseKPI(comboBox5.Text,comboBox8.Text,comboBox9.Text);
        }

        public void DayWiseKPI(string Criteria,string Plant,string Type)
        {

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("FromDate", dt_start.Text);
            Data.Add("ToDate", dt_end.Text);
            Data.Add("ProdLine", txtline.Text);
            Data.Add("Criteria", Criteria);
            Data.Add("Plant", Plant);
            Data.Add("Type", Type);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "DayWiseKPI", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dtJson.Rows.Count > 0)
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = dtJson;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
                 
            }
            else
            {
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "DayWise_KPI_Final_Data.xls";
                ExportExcels.Export(a, dataGridView2);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Please select position");
                return;
            }
            Calculate_Emp_KPI(comboBox1.Text);
        }

        public void Calculate_Emp_KPI( string position)
        {
           Cursor.Current = Cursors.WaitCursor;
            var result = ter_emp.Calculate_Emp_KPI(dateTimePicker1.Text, position);
            if(result.Success)
           {
                dataGridView3.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, result.Message);
            }
           else
           {
                dataGridView3.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, result.Message);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (tabControl1.SelectedIndex == 2)
            {
                //LoadPosition(dateTimePicker1.Text);
                //LoadPlant(dateTimePicker1.Text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Position");
                return;
            }
            Get_Emp_KPI(comboBox1.Text,comboBox2.Text, comboBox3.Text);
        }
        public void Get_Emp_KPI(string position,string Plant,string Type)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dt = ter_emp.Get_Emp_KPI(dateTimePicker1.Text, position,Plant,Type);
            if (dt.Rows.Count > 0)
            {
                dataGridView3.DataSource = null;
                dataGridView3.DataSource = dt;
            }
            else
            {
                dataGridView3.DataSource=null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, $@"No Data Found");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "KPI_Data.xls";
                ExportExcels.Export(a, dataGridView3);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Label10_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }

        private void Label13_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var targetColumns = new List<string> { "OUTPUT_TARGET_PERCENT", "PO_FINISH_PERCENT", "REPAIRS", "B_GRADES", "RFT", "REPACKING_QTY", "SIZE_LABEL_COUNT", "REPLACEMENT_AMOUNT", "HAULTING", "IE_PERCENT" };
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (targetColumns.Contains(colName))
            {
                e.CellStyle.BackColor = Color.LightSkyBlue; // For target columns
            }
            else
            {
                e.CellStyle.BackColor = Color.Honeydew; // For all other columns
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var Criteria = dataGridView1.Columns[e.ColumnIndex].Name;
                var ProdLine = dataGridView1.Rows[e.RowIndex].Cells["PROD_LINE"].Value?.ToString();
                var month = Month.Text.Replace("/", "");
                var targetColumns = new List<string> { "OUTPUT_TARGET_PERCENT", "PO_FINISH_PERCENT", "REPAIRS", "B_GRADES", "RFT", "REPACKING_QTY", "SIZE_LABEL_COUNT", "REPLACEMENT_AMOUNT", "HAULTING", "IE_PERCENT" };
                string colName = dataGridView1.Columns[e.ColumnIndex].Name;

                if (targetColumns.Contains(colName))
                {
                    RawData rd = new RawData();
                    DataTable dt = rd.GetRawData(Criteria, ProdLine, month);
                    View_RawData rawdata = new View_RawData(dt);
                    rawdata.Show();
                }



            }
        }
    }
}
