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

namespace Quality_Bonus
{
    public partial class QualityBonus : MaterialForm
    {
        public QualityBonus()
        {
            InitializeComponent();
        }

        private void QualityBonus_Load(object sender, EventArgs e)
        {
            LoadProd_Line();
            LoadProd_Plant();
        }
        public void LoadProd_Line()
        {
            txt_line.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_line.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.RepairsDataserver", "GetMESDepts", Program.client.UserToken, JsonConvert.SerializeObject(p));
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

        public void LoadProd_Plant()
        {
            txt_line.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_line.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                 "KZ_PRODKPIAPI.Controllers.IE_DataServer", "GetAllPlants", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                //DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                dt = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                // Add empty row before binding
                DataRow newRow = dt.NewRow();
                newRow["udf05"] = "";   // set empty value
                dt.Rows.InsertAt(newRow, 0);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "udf05";
                comboBox1.ValueMember = "udf05";
            }
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("Startdate", s_date.Text);
            p.Add("Enddate", e_date.Text);
            p.Add("ProdLine", txt_line.Text);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                 "KZ_RTDMAPI.Controllers.QualityBonusServer", "GetBonus", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if(dtJson.Rows.Count>0)
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
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }

        private void Btn_export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "QualityBonusData.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> p = new Dictionary<string, string>();
            p.Add("Startdate", dateTimePicker1.Text);
            p.Add("Enddate", dateTimePicker2.Text);
            p.Add("ProdPlant", comboBox1.Text);

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                 "KZ_RTDMAPI.Controllers.QualityBonusServer", "GetPlantBonus", Program.client.UserToken, JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
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
                dataGridView2.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }
    }
}
