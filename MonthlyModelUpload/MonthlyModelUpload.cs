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
using AutocompleteMenuNS;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace MonthlyModelUpload
{
    public partial class MonthlyModelUpload : MaterialForm
    {
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public MonthlyModelUpload()
        {
            InitializeComponent();
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            GetLineWiseModelData();
        }

        private void btn_template_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_Monthly_Model_Data.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_Monthly_Model_Data.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }
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
        private void btn_upload_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data for {dateTimePicker2.Text}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("MONTH", dateTimePicker2.Text);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "ModelImport",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("Imported successfully");
                                GetLineWiseModelData();
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

        public void GetLineWiseModelData()
        {

            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Month", dateTimePicker2.Text.Replace("/", ""));
            Data.Add("ProdLine", txtprodline.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer",
                "GetLineWiseModelData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
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

        private void MonthlyModelUpload_Load(object sender, EventArgs e)
        {
            //dateTimePicker2.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM");
            //dateTimePicker2.Enabled = false;
            LoadSeDept();
            autocompleteMenu1.SetAutocompleteMenu(textBox1, autocompleteMenu2);
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
        private void GetAllModels()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Month", dateTimePicker1.Text.Replace("/", ""));
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "GetAllModels", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                dt1 = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView1.DataSource = dt1;
                textBox1.TextChanged += textBox1_TextChanged;
                dataGridView2.DataSource = null;
            }
            else
            {
                string msg = SJeMES_Framework.Common.UIHelper.UImsg("err-00003", Program.client, "", Program.client.Language);
                string exceptionMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString();
                SJeMES_Control_Library.MessageHelper.ShowErr(this, msg + exceptionMsg);
                dataGridView1.DataSource = null;
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data for {dateTimePicker1.Text}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                    if (dt.Columns.Count != 6)
                    {
                        MessageBox.Show("Import template error, please refer to");
                        return;
                    }
                    if (dt != null)
                    {
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("Month", dateTimePicker1.Text.Replace("/", ""));
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "MEStandardImport",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("Imported successfully");
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            GetAllModels();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if(checkBox1.Checked)
            //{
            //    dateTimePicker2.Enabled = true;
            //}
            //else
            //{
            //    dateTimePicker2.Text = DateTime.Now.AddMonths(1).ToString("yyyy/MM");
            //    dateTimePicker2.Enabled = false;
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_ME_Head_Count.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_ME_Head_Count.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {

                string filterExpression = $"MODEL_NAME LIKE '%{textBox1.Text}%'";
                DataView dataView = new DataView(dt1)
                {
                    RowFilter = filterExpression
                };
                dataGridView1.DataSource = dataView;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Available to filter");
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            string modelname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["model_name"].Value.ToString();
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("modelname", modelname);
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "GetModel_ME_Count", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                //dt = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dt = JsonConvert.DeserializeObject<DataTable>(json);
                if (dt.Rows.Count>0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    dataGridView2.DataSource = null;
                }
               
            }
           
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_EOLR_Data.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_EOLR_Data.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data for {dateTimePicker3.Text}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("MONTH", dateTimePicker3.Text);
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer", "Monthly_EOLR_Upload",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                MessageBox.Show("Imported successfully");
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

        private void button5_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Month", dateTimePicker3.Text);
            Data.Add("ProdLine", textBox2.Text);
            Cursor.Current = Cursors.WaitCursor;
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.DayTargetsServer",
                "GetLineWiseEOLRData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
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
    }
}
