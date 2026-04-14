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

namespace KPI_FINAL_SCORE
{
    public partial class KPI_FINAL_DATA : MaterialForm
    {
        public KPI_FINAL_DATA()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        public void KPIFinalScore()
        {

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Month", Month.Text.Replace("/", ""));
            Data.Add("ProdLine", ProdLine.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.KPIFinalDataServer",
                     "KPIFinalScore", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView1.DataSource = dtJson;

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

        private void Button1_Click(object sender, EventArgs e)
        {
            KPIFinalScore();
        }

        private void KPI_FINAL_DATA_Load(object sender, EventArgs e)
        {
            LoadProdLine();
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
                string a = "KPI_Final_Data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }
    }
}
