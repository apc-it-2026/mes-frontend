using AutocompleteMenuNS;
using MaterialSkin.Controls;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace Outsole_Buffing_Entry
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }
        public void OrgData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            string On = "1";
            retData.Add("Org", On);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetOrgData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                Org.Text = "";
                Org1.Text = "";
                Org2.Text = "";
                Org.Items.Clear();
                Org1.Items.Clear();
                Org2.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string PlantName = dtJson.Rows[i]["FACTORY_SAP"].ToString();
                    Org.Items.Add(PlantName);
                    Org1.Items.Add(PlantName);
                    Org2.Items.Add(PlantName);
                }
                if (Org.Items.Count > 0)
                    Org1.SelectedIndex = -1;
                Org.SelectedIndex = -1;
                Org2.SelectedIndex = -1;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Org not found");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Po();
            OrgData();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void Load_Po()
        {
            Po.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Po.AutoCompleteSource = AutoCompleteSource.CustomSource;
            PoNum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PoNum.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection Autodata = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            Dictionary<string, string> p = new Dictionary<string, string>();
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer", "Get_Po", Program.client.UserToken, JsonConvert.SerializeObject(p));
            ResultObject retObject = JsonConvert.DeserializeObject<ResultObject>(ret);
            if (retObject.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(retObject.RetData);
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson.Rows.Count > 0)
                {
                    autocompleteMenu1.MaximumSize = new Size(250, 350);
                    var columnWidth = new[] { 50, 200 };
                    int n = 1;
                    for (int i = 0; i < dtJson.Rows.Count; i++)
                    {
                        autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dtJson.Rows[i]["CUSTOMER_PO"].ToString() }, dtJson.Rows[i]["CUSTOMER_PO"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                        n++;
                    }

                }

            }
        }
        public void ProdlineData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(comboBox2.Text))
            {
                retData.Add("Plant", comboBox2.Text);
                retData.Add("Org", Org.Text);
            }
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetProdlinetData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox3.Text = "";
                comboBox3.Items.Clear();

                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string Prodline = dtJson.Rows[i]["DEPARTMENT_CODE"].ToString();
                    comboBox3.Items.Add(Prodline);
                }
                if (comboBox3.Items.Count > 0)
                    comboBox3.SelectedIndex = -1;
                else
                {
                    comboBox3.Text = "";
                    comboBox3.Items.Clear();
                }
            }
        }
        public void ProdlineView()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Plant", comboBox1.Text);
            retData.Add("Org", Org1.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetProdlinetData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox4.Text = "";
                comboBox4.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string Prodline = dtJson.Rows[i]["DEPARTMENT_CODE"].ToString();
                    comboBox4.Items.Add(Prodline);
                }
                if (comboBox4.Items.Count > 0)
                    comboBox4.SelectedIndex = -1;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Org not found");
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProdlineData();
        }

        private void Po_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Po.Text))
            {
                textBox1.Text = "";
                textBox3.Text = "";
            }
        }

        private void Po_Enter(object sender, EventArgs e)
        {

        }

        private void Po_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Dictionary<string, object> retData = new Dictionary<string, object>();

                retData.Add("DELAY_PO", Po.Text);
                string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetModel",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );

                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
                {
                    string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                    DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                    if (dtJson.Rows.Count > 0)
                    {
                        textBox1.Text = dtJson.Rows[0]["PROD_NO"].ToString();// Replace "YourColumnName" with the actual column name
                        textBox3.Text = dtJson.Rows[0]["NAME_E"].ToString();
                    }

                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProdlineView();
        }
        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"
                try
                {
                    // Get the input string from the TextBox
                    string input = textBox4.Text;
                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;
                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }
                    // Show the result back in the same TextBox
                    textBox4.Text = sum.ToString();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox4.Text = "";
                }
            }
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }
        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox5.Text;
                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;
                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }
                    // Show the result back in the same TextBox
                    textBox5.Text = sum.ToString();
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox5.Text = "";
                }
            }
        }

        private void TextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox6.Text;

                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;

                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }
                    // Show the result back in the same TextBox
                    textBox6.Text = sum.ToString();
                    textBox6.SelectionStart = textBox6.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox6.Text = "";
                }
            }

        }
        private void TextBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }
        private void TextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox8.Text;

                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;

                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }

                    // Show the result back in the same TextBox
                    textBox8.Text = sum.ToString();
                    textBox8.SelectionStart = textBox8.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox8.Text = "";
                }
            }
        }
        private void TextBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }
        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"
                try
                {
                    // Get the input string from the TextBox
                    string input = textBox7.Text;
                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;
                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }
                    // Show the result back in the same TextBox
                    textBox7.Text = sum.ToString();
                    textBox7.SelectionStart = textBox7.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox7.Text = "";
                }
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox4.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox5.Text, out var result12) ? result12 : 0;
            textBox7.Text = (value11 - value12).ToString();
            if (value12 > value11)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "You have entered Total passed outsole is More than the total Outsole Received");
                if (textBox5.Text.Length > 0)
                {
                    textBox5.Text = textBox5.Text.Substring(0, textBox5.Text.Length - 1);
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
                return;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox7.Text = "";
                textBox5.Text = "";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Org.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Organization");
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Production Plant");
                return;
            }

            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Assembly Line");
                return;
            }
            if (string.IsNullOrEmpty(Po.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Po Number and Press Enter");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Po Number and Press Enter");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Po Number and Press Enter");
                return;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Outsole Quantity");
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Passed Outsole Quantity");
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Right Outsole Bgrade Quantity");
                return;
            }
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Left Outsole Bgrade Quantity");
                return;
            }
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Total Outsole Bgrade Quantity");
                return;
            }
            // Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Proddate", Date.Text);
            retData.Add("Org", Org.Text);
            retData.Add("Plant", comboBox2.Text);
            retData.Add("Assemblyline", comboBox3.Text);
            retData.Add("Po", Po.Text);
            retData.Add("Article", textBox1.Text);
            retData.Add("Model", textBox3.Text);
            retData.Add("Totaloutsole", textBox4.Text);
            retData.Add("Totalpass", textBox5.Text);
            retData.Add("Rightoutsolebgrade", textBox6.Text);
            retData.Add("Leftoutsolebgrade", textBox8.Text);
            retData.Add("Totaloutsolebgrade", textBox7.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "Insertoutsoledata",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData)
);
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data Saved Successfully");
                Todayinputdata();
                Clear();
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["ErrMsg"].ToString());
            }
        }
        public void Todayinputdata()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", Date.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetTodayoutsoledata",
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
        public void Clear()
        {
            comboBox3.SelectedIndex = -1;
            Org.SelectedIndex = -1;
            Po.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
            textBox4.Text = "";
            comboBox2.SelectedIndex = -1;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            double value11 = double.TryParse(textBox7.Text, out var result11) ? result11 : 0;
            double value12 = double.TryParse(textBox6.Text, out var result12) ? result12 : 0;
            double value15 = value11 - value12;
            textBox8.Text = value15.ToString();
            double value13 = double.TryParse(textBox8.Text, out var result13) ? result13 : 0;
            double value14 = value12 + value13;
            if (value12 > value11 || value14 > value11)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "You have entered Lef outsole Bgrade Quantity is More than the total Bgrade Quantity");
                if (textBox6.Text.Length > 0)
                {
                    textBox6.Text = textBox6.Text.Substring(0, textBox6.Text.Length - 1);
                    textBox6.SelectionStart = textBox6.Text.Length;
                }
                return;
            }
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                textBox8.Text = "";
                textBox8.Text = "";
            }
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }
        public void LoadTodayData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", From.Text);
            retData.Add("Todate", To.Text);
            retData.Add("Plant", comboBox1.Text);
            retData.Add("Assemblyline", comboBox4.Text);
            retData.Add("Po", PoNum.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "Getoutsoledata",
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
                comboBox4.Text = "";
                PoNum.Text = "";
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", From.Text);
            retData.Add("Todate", To.Text);
            retData.Add("Org", Org1.Text);
            retData.Add("Plant", comboBox1.Text);
            retData.Add("Assemblyline", comboBox4.Text);
            retData.Add("Po", PoNum.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "Getoutsoledata",
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
                comboBox4.Text = "";
                PoNum.Text = "";
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
            }
        }
        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }
        private void Label2_Click(object sender, EventArgs e)
        {

        }
        public void PlantData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Org", Org.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetPlantData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox2.Text = "";
                comboBox2.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string PlantName = dtJson.Rows[i]["UDF05"].ToString();
                    comboBox2.Items.Add(PlantName);
                }
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = -1;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Org not found");
                }
            }
        }
        public void PlantViewData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Org", Org1.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetPlantData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox1.Text = "";
                comboBox1.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string PlantName = dtJson.Rows[i]["UDF05"].ToString();
                    comboBox1.Items.Add(PlantName);
                }
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = -1;
            }
        }
        private void Org_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlantData();
        }
        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlantViewData();
        }
        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Org2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlantData2();
        }

        public void PlantData2()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Org", Org2.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetPlantData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox7.Text = "";
                comboBox7.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string PlantName = dtJson.Rows[i]["UDF05"].ToString();
                    comboBox7.Items.Add(PlantName);
                }
                if (comboBox7.Items.Count > 0)
                {
                    comboBox7.SelectedIndex = -1;
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Org not found");
                }
            }
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProdlineView2();
        }
        public void ProdlineView2()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Plant", comboBox7.Text);
            retData.Add("Org", Org2.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                     "GetProdlinetData",
    Program.client.UserToken,
    Newtonsoft.Json.JsonConvert.SerializeObject(retData)
    );
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                comboBox6.Text = "";
                comboBox6.Items.Clear();
                for (int i = 0; i < dtJson.Rows.Count; i++)
                {
                    string Prodline = dtJson.Rows[i]["DEPARTMENT_CODE"].ToString();
                    comboBox6.Items.Add(Prodline);
                }
                if (comboBox6.Items.Count > 0)
                    comboBox6.SelectedIndex = -1;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Org not found");
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("fromDate", dateTimePicker1.Text);
            p.Add("toDate", dateTimePicker2.Text);
            p.Add("Org", Org2.Text);
            p.Add("Plant", comboBox7.Text);
            p.Add("Assemblyline", comboBox6.Text);
            string ret = WebAPIHelper.Post(
                Program.client.APIURL,
                "KZ_RTDMAPI",
                "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                "Get_Dashboard_reports",
                Program.client.UserToken,
                JsonConvert.SerializeObject(p));
            try
            {
                var retDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (retDict == null || !retDict.ContainsKey("IsSuccess") || !Convert.ToBoolean(retDict["IsSuccess"]))
                {
                    MessageBox.Show("API call failed or returned unexpected data.");
                    return;
                }

                string json = retDict["RetData"]?.ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                // Clear previous chart content
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.Legends.Clear();
                chart1.Titles.Clear();
                chart1.Annotations.Clear();

                if (dtJson == null || dtJson.Rows.Count == 0)
                {
                    chart1.Titles.Add(new Title("No data found",
                        Docking.Top,
                        new Font("Tahoma", 14, FontStyle.Bold),
                        Color.Red));
                    return;
                }

                // Detect count-like column
                DataColumn countColumn =
                    dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("TOTAL_BGRADE", StringComparison.OrdinalIgnoreCase))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("COUNT(R_OUTSOLE_BGRADE_NO)", StringComparison.OrdinalIgnoreCase))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("COUNT", StringComparison.OrdinalIgnoreCase))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.ToUpper().Contains("COUNT("));

                if (countColumn == null)
                {
                    MessageBox.Show("Expected TOTAL_BGRADE or COUNT column in the data.");
                    return;
                }
                string countColName = countColumn.ColumnName;

                // Detect category column (prefer PLANT)
                DataColumn categoryColumn =
                    dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("PLANT", StringComparison.OrdinalIgnoreCase))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.ToUpper().Contains("PLANT"))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.Equals("R_OUTSOLE_BGRADE_NO", StringComparison.OrdinalIgnoreCase))
                    ?? dtJson.Columns.Cast<DataColumn>()
                        .FirstOrDefault(c => c.ColumnName.ToUpper().Contains("BGRADE"));

                // --- Title: add BEFORE adjusting chart area so there's space ---
                // Main centered title (keeps it visually above the entire chart)
                Title mainTitle = new Title("BGrade Distribution",
                    Docking.Top,
                    new Font("Tahoma", 18, FontStyle.Bold),
                    Color.Black);
                // ensure title is docked at top of the chart control area (not inside chart area)
                mainTitle.DockedToChartArea = string.Empty;
                mainTitle.Alignment = ContentAlignment.TopCenter;
                chart1.Titles.Add(mainTitle);

                // Chart area setup - leave extra top space so title doesn't overlap the pie
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.BackColor = Color.Transparent;
                chartArea.AxisX.MajorGrid.LineWidth = 0;
                chartArea.AxisY.MajorGrid.LineWidth = 0;
                chartArea.AxisX.LabelStyle.Enabled = false;
                chartArea.AxisY.LabelStyle.Enabled = false;

                // Reserve 60% width for pie (left) and leave top margin (Y = 8%) for the title
                chartArea.Position = new ElementPosition(0f, 8f, 60f, 90f);      // moved down by 8%
                                                                                 // Make inner plot large and nicely centered within that left area
                chartArea.InnerPlotPosition = new ElementPosition(6f, 8f, 85f, 82f);
                chart1.ChartAreas.Add(chartArea);

                // Create pie series (clean appearance)
                Series series = new Series("BGradePie");
                series.ChartType = SeriesChartType.Pie;
                series.IsValueShownAsLabel = false;              // disable labels on slices
                series["PieLabelStyle"] = "Disabled";
                series["PieDrawingStyle"] = "Default";
                series.BorderColor = Color.Transparent;
                series.BorderWidth = 0;
                series["CollectedThreshold"] = "0";
                chart1.Series.Add(series);

                // Legend setup - positioned under the total on the right
                Legend legend = new Legend("PlantsLegend");
                legend.Docking = Docking.Right;
                legend.Alignment = StringAlignment.Near;
                legend.Title = "Plants";
                legend.TitleFont = new Font("Tahoma", 14, FontStyle.Bold);
                legend.Font = new Font("Tahoma", 9, FontStyle.Regular);
                legend.BackColor = Color.Transparent;
                legend.IsDockedInsideChartArea = false;
                legend.LegendStyle = LegendStyle.Table;
                legend.TableStyle = LegendTableStyle.Auto;
                // Place legend block on right area under the total text
                legend.Position = new ElementPosition(62f, 28f, 36f, 64f); // X,Y,Width,Height (percent of chart)
                chart1.Legends.Add(legend);

                // Color map for consistent coloring
                Dictionary<string, Color> colorMap = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
        {
            { "AP2", Color.FromArgb(0, 123, 167) },
            { "AP8", Color.FromArgb(242, 153, 0) },
        };
                Color[] fallbackPalette = new Color[]
                {
            Color.MediumSeaGreen,
            Color.MediumVioletRed,
            Color.Goldenrod,
            Color.CadetBlue,
            Color.Coral,
            Color.DarkKhaki
                };
                int fallbackIndex = 0;

                // Gather valid rows for slices (exclude any named "TOTAL")
                List<DataRow> validRows = new List<DataRow>();
                if (categoryColumn != null && !string.Equals(categoryColumn.ColumnName, countColName, StringComparison.OrdinalIgnoreCase))
                {
                    validRows = dtJson.AsEnumerable()
                        .Where(r =>
                            r[countColName] != DBNull.Value &&
                            double.TryParse(r[countColName].ToString(), out double cnt) &&
                            cnt > 0 &&
                            r[categoryColumn.ColumnName] != DBNull.Value &&
                            !string.IsNullOrWhiteSpace(r[categoryColumn.ColumnName].ToString()) &&
                            !r[categoryColumn.ColumnName].ToString().Trim().Equals("TOTAL", StringComparison.OrdinalIgnoreCase)
                        )
                        .ToList();
                }

                if (validRows.Count == 0 && categoryColumn != null)
                {
                    chart1.Titles.Add(new Title("No plant data found",
                        Docking.Top,
                        new Font("Tahoma", 14, FontStyle.Bold),
                        Color.Red));
                    return;
                }

                // Determine grand total: prefer explicit TOTAL row if present, otherwise sum slices
                double grandTotal = 0;
                if (categoryColumn != null)
                {
                    var totalRow = dtJson.AsEnumerable()
                        .FirstOrDefault(r =>
                            r[categoryColumn.ColumnName] != DBNull.Value &&
                            r[categoryColumn.ColumnName].ToString().Trim().Equals("TOTAL", StringComparison.OrdinalIgnoreCase) &&
                            r[countColName] != DBNull.Value &&
                            double.TryParse(r[countColName].ToString(), out _)
                        );

                    if (totalRow != null)
                    {
                        grandTotal = Convert.ToDouble(totalRow[countColName]);
                    }
                    else
                    {
                        grandTotal = validRows.Sum(r => Convert.ToDouble(r[countColName]));
                    }
                }
                else
                {
                    grandTotal = dtJson.AsEnumerable()
                        .Where(r => r[countColName] != DBNull.Value && double.TryParse(r[countColName].ToString(), out _))
                        .Sum(r => Convert.ToDouble(r[countColName]));
                }

                // Add slices and put clean text into legend entries (full count and percent)
                foreach (var row in validRows)
                {
                    string category = row[categoryColumn.ColumnName].ToString().Trim();
                    double count = Convert.ToDouble(row[countColName]);
                    double percent = (grandTotal > 0) ? (count / grandTotal) * 100.0 : 0.0;

                    int idx = series.Points.AddXY(category, count);
                    series.Points[idx].LegendText = $"{category}: {count} ({percent:F1}%)";

                    if (colorMap.ContainsKey(category))
                        series.Points[idx].Color = colorMap[category];
                    else
                        series.Points[idx].Color = fallbackPalette[(fallbackIndex++) % fallbackPalette.Length];

                    series.Points[idx].BorderColor = Color.FromArgb(30, Color.Black);
                    series.Points[idx].BorderWidth = 1;
                }

                // Add a clear "Total: NNN" annotation on the RIGHT but vertically aligned with the top area
                TextAnnotation totalAnnotation = new TextAnnotation();
                totalAnnotation.Text = $"Total:\n{(long)Math.Round(grandTotal)}";
                totalAnnotation.Font = new Font("Tahoma", 16, FontStyle.Bold);
                totalAnnotation.ForeColor = Color.Black;
                totalAnnotation.Alignment = ContentAlignment.MiddleCenter;
                totalAnnotation.BackColor = Color.Transparent;
                totalAnnotation.IsSizeAlwaysRelative = false;
                // Place inside right reserved area (percent coordinates relative to chart)
                totalAnnotation.X = 80F;   // horizontally in the right side
                totalAnnotation.Y = 14F;   // keep above the legend block and below the main title
                totalAnnotation.Width = 18F;
                totalAnnotation.Height = 10F;
                totalAnnotation.ClipToChartArea = string.Empty;
                chart1.Annotations.Add(totalAnnotation);

                // (Title already added above; no extra title here)
                Dashboard();
                LoadSamplePieChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void LoadSamplePieChart()
        {
            try
            {
                // Use selected range (fromDate to toDate) 
                DateTime fromDate = dateTimePicker1.Value;
                DateTime toDate = dateTimePicker2.Value;

                Dictionary<string, object> p = new Dictionary<string, object>
        {
            { "fromDate", fromDate.ToString("yyyy/MM/dd") },
            { "toDate", toDate.ToString("yyyy/MM/dd") }
        };

                // Call API
                string ret = WebAPIHelper.Post(
                    Program.client.APIURL,
                    "KZ_RTDMAPI",
                    "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                    "Get_Month_Wise_Bgrade",
                    Program.client.UserToken,
                    JsonConvert.SerializeObject(p));

                var apiResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                bool isSuccess = Convert.ToBoolean(apiResponse["IsSuccess"]);
                if (!isSuccess)
                {
                    MessageBox.Show("API call failed: " + apiResponse["ErrMsg"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parse result
                string json = apiResponse["RetData"].ToString();
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);

                // Build dictionary from API (only months returned)
                Dictionary<string, int> monthData = new Dictionary<string, int>();
                foreach (DataRow row in dt.Rows)
                {
                    string monthYear = row["MONTH_YEAR"].ToString();
                    int count = Convert.ToInt32(row["BGRADE_COUNT"]);
                    if (!monthData.ContainsKey(monthYear))
                        monthData.Add(monthYear, count);
                }

                // Build all months between fromDate and toDate
                List<string> monthsInRange = new List<string>();
                DateTime iterDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                while (iterDate <= toDate)
                {
                    monthsInRange.Add(iterDate.ToString("MMM yyyy"));
                    iterDate = iterDate.AddMonths(1);
                }

                // Clear chart
                chart3.Series.Clear();
                chart3.ChartAreas.Clear();
                chart3.Titles.Clear();   // <-- IMPORTANT: remove previous titles

                // ---------------------------------------------
                // ⭐ ADD TITLE HERE ⭐
                // ---------------------------------------------
                chart3.Titles.Add(new Title(
                    "Month Wise Out Sole Buffing Key Process BGrades Count",
                    Docking.Top,
                    new Font("Segoe UI", 14, FontStyle.Bold),
                    Color.Black)
                );

                // Create series
                Series series = new Series("Bgrade Trend")
                {
                    ChartType = SeriesChartType.Spline,
                    BorderWidth = 3,
                    Color = Color.Teal,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 6
                };

                // Add all months (0 if missing)
                foreach (string month in monthsInRange)
                {
                    int count = monthData.ContainsKey(month) ? monthData[month] : 0;
                    DataPoint point = new DataPoint();
                    point.SetValueXY(month, count);

                    if (count > 0)
                    {
                        point.IsValueShownAsLabel = true;
                        point.LabelForeColor = Color.Black;
                        point.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    }

                    series.Points.Add(point);
                }

                chart3.Series.Add(series);

                // Configure chart area
                ChartArea chartArea = new ChartArea("MainArea");
                chartArea.BackColor = Color.White;
                chartArea.AxisX.Interval = 1;
                chartArea.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisX.LineColor = Color.LightGray;
                chartArea.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular); 
                chartArea.AxisY.MajorGrid.Enabled = false;
                chartArea.AxisY.LineColor = Color.LightGray;
                chart3.ChartAreas.Add(chartArea); 
                if (chart3.Legends.Count > 0) 
                    chart3.Legends[0].Enabled = false;

                chart3.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void Dashboard()
        {
            Dictionary<string, object> p = new Dictionary<string, object>
    {
        { "fromDate", dateTimePicker1.Text },
        { "toDate", dateTimePicker2.Text },
        { "Org", Org2.Text },
        { "Plant", comboBox7.Text },
        { "Assemblyline", comboBox6.Text }
    };

            string ret = WebAPIHelper.Post(
                Program.client.APIURL,
                "KZ_RTDMAPI",
                "KZ_RTDMAPI.Controllers.OutsoleBuffingServer",
                "Get_Bgrades_reports",
                Program.client.UserToken,
                JsonConvert.SerializeObject(p));

            bool isSuccess = false;
            try
            {
                var parsed = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (parsed != null && parsed.ContainsKey("IsSuccess"))
                    bool.TryParse(parsed["IsSuccess"].ToString(), out isSuccess);
            }
            catch { }

            if (!isSuccess)
            {
                MessageBox.Show("Failed to retrieve data from server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string json = null;
            try
            {
                json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
            }
            catch
            {
                MessageBox.Show("Invalid response from server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataTable dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected criteria.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // --- Clear previous chart ---
                chart2.Series.Clear();
                chart2.ChartAreas.Clear();
                chart2.Legends.Clear();
                chart2.Titles.Clear();

                // --- Fonts & visual tweaks ---
                Font axisFont = new Font("Lucida Bright", 12, FontStyle.Regular);
                Font titleFont = new Font("Lucida Bright", 12, FontStyle.Bold);
                Font pointFont = new Font("Lucida Bright", 10, FontStyle.Bold);

                chart2.AntiAliasing = AntiAliasingStyles.Graphics | AntiAliasingStyles.Text;
                chart2.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

                // --- Chart area ---
                ChartArea ca = new ChartArea("MainArea");
                ca.AxisY.MajorGrid.Enabled = false;
                ca.AxisX.MajorGrid.Enabled = false;
                ca.AxisX.Title = "Plant";
                ca.AxisY.Title = "B-Grade Count";
                ca.AxisX.LabelStyle.Font = axisFont;
                ca.AxisY.LabelStyle.Font = axisFont;
                ca.AxisX.TitleFont = titleFont;
                ca.AxisY.TitleFont = titleFont;
                ca.AxisX.LabelStyle.IsEndLabelVisible = true;
                ca.AxisX.IsLabelAutoFit = false; // we'll control auto-fit logic below

                // Scroll / zoom
                ca.AxisX.ScrollBar.Enabled = true;
                ca.AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
                ca.AxisX.ScrollBar.Size = 12;
                ca.AxisX.ScaleView.Zoomable = true;
                ca.AxisX.ScaleView.MinSize = 1;
                ca.AxisX.ScaleView.Size = 6;

                // default inner plot position (we will tweak later)
                ca.Position.Auto = true;
                ca.InnerPlotPosition.Auto = false;
                ca.InnerPlotPosition = new ElementPosition(10, 12, 80, 78); // leave room at bottom for labels

                chart2.ChartAreas.Add(ca);

                // --- Series ---
                Series series = new Series("Outsole B-Grades")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(70, 130, 180),
                    BorderColor = Color.FromArgb(45, 45, 45),
                    BorderWidth = 1,
                    IsValueShownAsLabel = true,
                    Font = pointFont,
                    IsXValueIndexed = true
                };
                series["PointWidth"] = "0.7";

                // --- Detect label & value columns robustly ---
                string labelCol = null;
                string[] labelCandidates = { "PLANT_NAME", "PLANT", "ASSEMBLY_LINE", "LINE", "ORG" };
                foreach (var c in labelCandidates)
                    if (dt.Columns.Contains(c)) { labelCol = c; break; }
                if (labelCol == null) labelCol = dt.Columns[0].ColumnName;

                string valueCol = null;
                string[] valueCandidates = { "TOTAL_BGRADE", "TOTAL_OUTSOLE_BGRADE_NO", "TOTAL_OUTSOLE_BGRADE" };
                foreach (var c in valueCandidates)
                    if (dt.Columns.Contains(c)) { valueCol = c; break; }
                if (valueCol == null)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.DataType == typeof(int) || dc.DataType == typeof(long) || dc.DataType == typeof(double) || dc.DataType == typeof(decimal))
                        {
                            valueCol = dc.ColumnName;
                            break;
                        }
                    }
                }
                if (valueCol == null && dt.Columns.Count > 1)
                    valueCol = dt.Columns[1].ColumnName;

                // --- Create safe row list and measure label lengths ---
                var rows = dt.AsEnumerable()
                    .Select(r =>
                    {
                        string rawLabel = r[labelCol] == DBNull.Value ? "" : r[labelCol].ToString().Trim();
                        double val = 0;
                        if (valueCol != null && r[valueCol] != DBNull.Value)
                            double.TryParse(r[valueCol].ToString().Trim(), out val);

                        return new
                        {
                            Label = string.IsNullOrWhiteSpace(rawLabel) ? "(unknown)" : rawLabel,
                            Value = val
                        };
                    })
                    .ToList();

                int maxLabelLen = rows.Any() ? rows.Max(x => x.Label.Length) : 0;

                // --- Decide label rotation & auto-fit based on label length & count ---
                // Short labels (<=4) -> horizontal (0°), Medium (5-10) -> -30°, Long (>10) -> -45°
                if (maxLabelLen <= 4 && rows.Count <= 10)
                {
                    ca.AxisX.LabelStyle.Angle = 0;
                    ca.AxisX.IsLabelAutoFit = false;
                    ca.InnerPlotPosition = new ElementPosition(10, 12, 80, 80); // more height for plot
                }
                else if (maxLabelLen <= 10)
                {
                    ca.AxisX.LabelStyle.Angle = -30;
                    ca.AxisX.IsLabelAutoFit = true;
                    ca.InnerPlotPosition = new ElementPosition(10, 10, 80, 76);
                }
                else
                {
                    ca.AxisX.LabelStyle.Angle = -45;
                    ca.AxisX.IsLabelAutoFit = true;
                    ca.InnerPlotPosition = new ElementPosition(10, 8, 80, 74);
                }

                // If many points, show every nth label to avoid crowding (keeps "perfect" look)
                int labelCount = rows.Count;
                if (labelCount > 20)
                {
                    int interval = (int)Math.Ceiling(labelCount / 20.0); // show ~20 labels max
                    ca.AxisX.LabelStyle.Interval = interval;
                }
                else
                {
                    ca.AxisX.LabelStyle.Interval = 1;
                }

                // --- Add points to the series ---
                foreach (var item in rows)
                {
                    int idx = series.Points.AddXY(item.Label, item.Value);
                    var pt = series.Points[idx];
                    pt.Label = item.Value.ToString("0");
                    pt.LabelForeColor = Color.Black;
                    pt.Font = pointFont;
                    pt.ToolTip = $"{item.Label}\nB-Grades: {item.Value:N0}";
                }

                chart2.Series.Add(series);

                // --- Legend ---
                Legend legend = new Legend("Legend");
                legend.Docking = Docking.Top;
                legend.Font = axisFont;
                legend.BackColor = Color.Transparent;
                legend.ForeColor = Color.FromArgb(45, 45, 45);
                chart2.Legends.Add(legend);
                series.Legend = "Legend";

                // --- Title ---
                chart2.Titles.Add(new Title("Plant Wise Outsole B-Grades", Docking.Top, titleFont, Color.Black));

                // --- Initial view (if many points) ---
                if (series.Points.Count > 6)
                {
                    ca.AxisX.ScaleView.Position = 0;
                    ca.AxisX.ScaleView.Size = Math.Min(6, series.Points.Count);
                }
                else
                {
                    ca.AxisX.ScaleView.ZoomReset();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}


