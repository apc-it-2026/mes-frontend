using AutocompleteMenuNS;
using MaterialSkin.Controls;
using NewExportExcels;
using Newtonsoft.Json;
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

namespace Final_IE
{
    public partial class Final_IE : MaterialForm
    {
        public Final_IE()
        {
            InitializeComponent();
        }
        Efficiency ef = new Efficiency();
           
        private void btn_export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "DayWise_IE.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetDayWise_IE();
        }

        public void GetDayWise_IE()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("FromDate", proddate_s.Text);
            Data.Add("ToDate", proddate_e.Text);
            Data.Add("ProdLine", txtpline.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "GetDayWise_IE",
                               Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetAccumulated_IE();
        }

        public void GetAccumulated_IE()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("FromDate", fromdate.Text);
            Data.Add("ToDate", todate.Text);
            Data.Add("ProdLine", txtprodline.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "GetAccumulated_IE",
                               Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView3.DataSource = dtJson1;
                }
                else
                {
                    dataGridView3.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Accumulated_IE.xls";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void Final_IE_Load(object sender, EventArgs e)
        {
            LoadSeDept();
            GetAllPlants();
        }
        private void LoadSeDept()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }
        private DataTable GetAllDepts()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.GeneralServer", "GetAllDepts", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
            return dt;
        }

        private void GetAllPlants()
        {
            DataTable dt = new DataTable();
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "GetAllPlants", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                //DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "udf05"; 
                comboBox1.ValueMember = "udf05";

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "udf05";
                comboBox2.ValueMember = "udf05";
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("ProdDate", dateTimePicker1.Text);
            Data.Add("Plant", comboBox1.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "GetC2B_C2S_IE",
                               Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView4.DataSource = dtJson1;
                }
                else
                {
                    dataGridView4.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView4.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "C2B_C2S_IE.xls";
                ExportExcels.Export(a, dataGridView4);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text) && string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please select Plant or Line");
                return;
            }
            Efficienct_Target(comboBox2.Text, textBox1.Text);
        }
        public void Efficienct_Target(string Plant , string Line)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime selectedDate = DateTime.Parse(dateTimePicker2.Text);
            var result = ef.Efficienct_Target(selectedDate, Plant, Line);
            if (result.Success)
            {
                dataGridView6.DataSource = result.Data;
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, result.Message);
            }
            else
            {
                dataGridView6.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, result.Message);
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (dataGridView6.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Efficiency_Target_Report.xls";
                ExportExcels.Export(a, dataGridView6);
            }
        }
    }
}
