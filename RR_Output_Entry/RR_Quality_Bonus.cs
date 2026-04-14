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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RR_Quality_Bonus
{
    public partial class RR_Quality_Bonus : MaterialForm
    {
        private ExcelProcessor _currentExcelProcessor = null;
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private void GetExcelData(string fileName)
        {
            try
            {
                this._currentExcelProcessor = new ExcelProcessor(fileName);
                IList<object[]> list = this._currentExcelProcessor.GetSheetData(0);
                if (data != null && data.Count > 0)
                {
                    for (int i = 1; i < list.Count; i++)
                    {
                        data.Add(list[i]);
                    }
                }
                else
                {
                    data = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }
        public RR_Quality_Bonus()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", dateTimePicker1.Text);
            retData.Add("Todate", dateTimePicker2.Text);
            retData.Add("Team", comboBox4.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.QualityBonusServer",
                     "ViewRRQualityBonus",
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
        public void RefreshData()
        {
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Fromdate", Date.Text);
            retData.Add("Team", comboBox1.Text);
            retData.Add("RubberType", comboBox3.Text);
            string retdata = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.QualityBonusServer",
                     "RRGetData",
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
        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Team");
                return;
            }
            if (string.IsNullOrEmpty(comboBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Select Rubber Type");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Output");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Bgrades");
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Qualified Quantity");
                return;
            }
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "Please Enter Defect Quantity");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> retData = new Dictionary<string, object>();
            retData.Add("Date", Date.Text);
            retData.Add("Team", comboBox1.Text);
            retData.Add("Rubbertype", comboBox3.Text);
            retData.Add("Output", textBox1.Text);
            retData.Add("Bgrades", textBox2.Text);
            retData.Add("InspectedQty", textBox3.Text);
            retData.Add("PassQty", textBox4.Text);
            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.QualityBonusServer",
                     "RROutputData",
Program.client.UserToken,
Newtonsoft.Json.JsonConvert.SerializeObject(retData));

            if (Convert.ToBoolean(JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))

            {
                string json = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                if (json == "Failed")
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data");
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Data inserted Successfully");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";


                }

            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

        }

        private void TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }

        }

        private void TableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {

        }


        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox1.Text;

                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;

                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }

                    // Show the result back in the same TextBox
                    textBox1.Text = sum.ToString();
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox1.Text = "";

                }
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox2.Text;

                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;

                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }

                    // Show the result back in the same TextBox
                    textBox2.Text = sum.ToString();
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox2.Text = "";
                }
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent Enter key from "ding"

                try
                {
                    // Get the input string from the TextBox
                    string input = textBox3.Text;

                    // Split by '+' and parse each number
                    string[] parts = input.Split('+');
                    int sum = 0;

                    foreach (string part in parts)
                    {
                        sum += int.Parse(part.Trim());
                    }

                    // Show the result back in the same TextBox
                    textBox3.Text = sum.ToString();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
                catch
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "Invalid Input");
                    textBox3.Text = "";
                }
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

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "RRBonusData.xls";
                ExportExcels.Export(a, dataGridView2);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RR_Output_Entry_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Insert(0, "");
            comboBox3.Items.Insert(0, "");
            comboBox4.Items.Insert(0, "");
        }


        private void Button7_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("MONTH", Month.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.QualityBonusServer",
                     "GetRRBonusTeamsData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);

            if (ret.IsSuccess)
            {

                Dictionary<string, object> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data"].ToString());



                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView3.DataSource = dtJson1;
                }
                else
                {
                    dataGridView3.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");
                }
            }
            else
            {
                dataGridView3.DataSource = null;
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No data found");

            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data ?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                try
                {
                    DataTable dt = new DataTable();
                    isTitle = true;
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = true;
                    ofd.Filter = "EXCEL|*.xls*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        data = new List<object[]>();
                        foreach (string filename in ofd.FileNames)
                        {
                            try
                            {
                                this.GetExcelData(Path.GetFullPath(filename));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, ex.Message);
                            }
                        }
                        if (data != null && data.Count > 0)
                        {
                            int colNum = data[0].Length;
                            for (int i = 0; i < colNum; i++)
                            {
                                string columnName = data[0][i].ToString();
                                dt.Columns.Add(columnName);
                            }
                            for (int i = 1; i < data.Count; i++)
                            {
                                DataRow row = dt.NewRow();
                                for (int j = 0; j < colNum; j++)
                                {
                                    row[j] = data[i][j];
                                }
                                dt.Rows.Add(row);
                            }

                        }
                    }
                    if (dt.Columns.Count != 2)
                    {
                        MessageBox.Show("Import template error, please refer to");
                        return;
                    }
                    if (dt != null)
                    {

                        SJeMES_Control_Library.Forms.FrmWare ru = new SJeMES_Control_Library.Forms.FrmWare(dt);
                        ru.StartPosition = FormStartPosition.CenterScreen;
                        ru.ShowDialog();
                        bool is_sure = ru.is_sure;
                        if (is_sure)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("MONTH", Month.Text);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_RTDMAPI",
                     "KZ_RTDMAPI.Controllers.QualityBonusServer",
                     "RRBonusTeamsImport",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {

                                // WarehouseData();
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Imported successfully");
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.client, Program.client.WebServiceUrl, Program.client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {

            string path = Application.StartupPath + @"\Download Template" + "\\RRTeamsUpload.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "RRTeamsUpload.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "RR_Bonus_Team_Details_DB_Data.xls";
                ExportExcels.Export(a, dataGridView1);
                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Successfully downloaded");
            }
        }
    }
}
