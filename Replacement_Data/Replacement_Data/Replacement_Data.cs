using AutocompleteMenuNS;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;
using System;
using System.Collections.Generic;
using SJeMES_Control_Library;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewExportExcels;
using MaterialSkin.Controls;

namespace Replacement_Data
{
    public partial class Replacement_Data : MaterialForm
    {
        

        public Replacement_Data()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        public void LoadProd_Line()
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                  "KZ_PRODKPIAPI.Controllers.ReplacementsServer", "GetLines", Program.client.UserToken, JsonConvert.SerializeObject(p));
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
        public void ViewData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", dateTimePicker1.Text);
            retData.Add("Todate", dateTimePicker2.Text);
            retData.Add("ProdLine", textBox1.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.ReplacementsServer",
                     "ViewReplacementsData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );


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
                dataGridView1.IsEmpty();
                dataGridView1.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }
        public void FinalData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", dateTimePicker5.Text);
            retData.Add("Todate", dateTimePicker6.Text);
            retData.Add("ProdLine", textBox4.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.ReplacementsServer",
                     "FinalReplacementsData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView3.DataSource = dtJson;

                }

            }
            else
            {
                dataGridView3.IsEmpty();
                dataGridView3.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }
        public void EditData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", dateTimePicker3.Text);
            retData.Add("Todate", dateTimePicker4.Text);
            retData.Add("ProdLine", textBox2.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_PRODKPIAPI",
                     "KZ_PRODKPIAPI.Controllers.ReplacementsServer",
                     "EditReplacementsData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );


            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    dataGridView2.DataSource = dtJson;

                }

            }
            else
            {
                dataGridView2.IsEmpty();
                dataGridView2.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            ViewData();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

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
                string a = "ReplacementsReport.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Replacement_Data_Load(object sender, EventArgs e)
        {
            tabPage2 = tabControl1.TabPages[1];
            tabPage3 = tabControl1.TabPages[2];
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
            LoadProd_Line();
           
        }


        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "FinalReplacementsReport.xls";
                ExportExcels.Export(a, dataGridView3);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            EditData();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FinalData();
        }

        private void DataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {

                if (dataGridView2.Columns[e.ColumnIndex].Name == "Edit")
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells["LOCK_STATUS"].Value.ToString() == "Locked")
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Data Locked");
                        return;
                    }
                    string replaceDate = dataGridView2.Rows[e.RowIndex].Cells["Column3"].Value.ToString();
                    string salesOrder = dataGridView2.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                    string replaceWorkcentre = dataGridView2.Rows[e.RowIndex].Cells["Column6"].Value.ToString();
                    string responsibleDept = dataGridView2.Rows[e.RowIndex].Cells["Column8"].Value.ToString();
                    string textBox5Value = dataGridView2.Rows[e.RowIndex].Cells["Column14"].Value.ToString();
                    string textBox6Value = dataGridView2.Rows[e.RowIndex].Cells["Column15"].Value.ToString();
                    string Code_Replaceworkcentre = dataGridView2.Rows[e.RowIndex].Cells["Column5"].Value.ToString();
                    string component = dataGridView2.Rows[e.RowIndex].Cells["Column13"].Value.ToString();
                    ////Replace_Date.Enabled = false;
                    //Sales_Order.Enabled = false;
                    //Replace_Workcentre.ReadOnly = false;
                    //Responsible_Dept.ReadOnly = false;
                    //textBox1.Visible = true;
                    //button2.Visible = true;
                    //button1.Visible = false;
                    Replacements_Update ru = new Replacements_Update(replaceDate, salesOrder, replaceWorkcentre, responsibleDept, textBox5Value, textBox6Value, Code_Replaceworkcentre, component);
                    ru.UpdateSuccess += (message) =>
                    {
                        // When the data is updated successfully, display the message
                        SJeMES_Control_Library.MessageHelper.ShowSuccess(this, message);
                        EditData();
                        ViewData();

                    };
                    ru.Show();
                    

                }
            }


        }
    }
}
