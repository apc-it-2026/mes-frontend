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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Upload_TPPH
{
    public partial class Upload_TPPH : MaterialForm
    {

        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public Upload_TPPH()
        {
            InitializeComponent();
        }

        private void btn_template_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_TPPH.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_TPPH.xlsx";
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
            DialogResult result = MessageBox.Show($@"Are you sure you want to save {tabPage1.Text.ToUpper()} data for the Month  {Month_Tpph.Text.Replace("/", "")} ?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                    if (dt.Columns.Count != 7)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Import template error, please refer to");
                        return;
                    }
                    if (dt != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("Month", Month_Tpph.Text.Replace("/", ""));
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "TargetPPH_Upload",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Imported successfully");
                                Get_Model_TPPH(txt_Model.Text, Month_Tpph.Text.Replace("/", ""));
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);
                                
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

        private void Upload_TPPH_Load(object sender, EventArgs e)
        {
            Load_Model_Names();
            LoadSeDept();
            autocompleteMenu2.SetAutocompleteMenu(textBox2, autocompleteMenu2);

        }

        private DataTable Get_Model_Names()
        {
            DataTable dtJson1 = new DataTable();
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "Get_Model_Names",
                               Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
               
            }
            return dtJson1;
        }

        private void Load_Model_Names()
        {
            autocompleteMenu1.Items = null;
            autocompleteMenu1.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = Get_Model_Names();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu1.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["ARTICLE"].ToString() }, dt.Rows[i]["ARTICLE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
                n++;
            }
        }

        private void LoadSeDept()
        {
            autocompleteMenu2.Items = null;
            autocompleteMenu2.MaximumSize = new System.Drawing.Size(350, 350);
            var columnWidth = new int[] { 50, 250 };
            DataTable dt = GetAllDepts();
            int n = 1;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                autocompleteMenu2.AddItem(new MulticolumnAutocompleteItem(new[] { n + "", dt.Rows[i]["DEPARTMENT_CODE"].ToString() + " " + dt.Rows[i]["DEPARTMENT_NAME"].ToString() }, dt.Rows[i]["DEPARTMENT_CODE"].ToString()) { ColumnWidth = columnWidth, ImageIndex = n });
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
        private void button1_Click(object sender, EventArgs e)
        {
            Get_Model_TPPH(txt_Model.Text, Month_Tpph.Text.Replace("/", ""));
        }

        private void Get_Model_TPPH(string Article, string Month_Tpph)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Article", Article);
            Data.Add("Month", Month_Tpph);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "Get_Model_TPPH",
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

        private void btn_export_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "ModelWise_TPPH.xlsx";
                ExportExcels.Export(a, dataGridView1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_Model_Complexity.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_Model_Complexity.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show($@"Are you sure you want to save {tabPage2.Text.ToUpper()} data for the Month {Month.Text.Replace("/", "")} ?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                    if (dt.Columns.Count != 4)
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Import template error, please refer to");
                        return;
                    }
                    if (!(dt.Columns.Contains("EOLR") && dt.Columns.Contains("MODEL_NAME") && dt.Columns.Contains("ARTICLE") && dt.Columns.Contains("PROCESS")))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Import template error, please refer to");
                        return;
                    }
                    if (dt != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("Month", Month.Text.Replace("/", ""));
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "ModelComplexity_Upload",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Imported successfully");
                                Get_ModelComplexity(textBox1.Text, Month.Text.Replace("/", ""));
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);

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

        private void Get_ModelComplexity(string Article, string Month )
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Article", Article);
            Data.Add("Month", Month);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "Get_ModelComplexity",
                               Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret.RetData);
                DataTable dtJson1 = JsonConvert.DeserializeObject<DataTable>(dic["Data"].ToString());
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dtJson1;
                }
                else
                {
                    dataGridView2.DataSource = null;
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_ModelComplexity(textBox1.Text,Month.Text.Replace("/", ""));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Get_LineEOLR(textBox2.Text, dateTimePicker1.Text.Replace("/", ""));
        }

        private void Get_LineEOLR(string ProdLine,string Month)
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("ProdLine", ProdLine);
            Data.Add("Month", Month);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                               "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "Get_LineEOLR",
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

        private void button8_Click(object sender, EventArgs e)
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

        private void button9_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show($@"Are you sure you want to save {tabPage3.Text.ToUpper()} data for the Month {dateTimePicker1.Text.Replace("/", "")}?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Import template error, please refer to");
                        return;
                    }
                    if (!(dt.Columns.Contains("EOLR") && dt.Columns.Contains("DEPT_CODE")))
                    {
                        SJeMES_Control_Library.MessageHelper.ShowErr(this, "Import template error, please refer to");
                        return;
                    }
                    if (dt != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        SJeMES_Control_Library.Forms.FrmImport frm = new SJeMES_Control_Library.Forms.FrmImport(dt);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        bool is_sure = frm.is_sure;
                        if (is_sure)
                        {
                            Dictionary<string, object> p = new Dictionary<string, object>();
                            p.Add("SOURCE", dt);
                            p.Add("Month", dateTimePicker1.Text.Replace("/", ""));
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL,
                                "KZ_PRODKPIAPI", "KZ_PRODKPIAPI.Controllers.IE_DataServer", "LineEOLR_Upload",
                                Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));

                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this, "Imported successfully");
                                Get_LineEOLR(textBox2.Text, dateTimePicker1.Text.Replace("/", ""));
                            }
                            else
                            {
                                SJeMES_Control_Library.MessageHelper.ShowErr(this, ret.ErrMsg);

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

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "Line_EOLR.xlsx";
                ExportExcels.Export(a, dataGridView3);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, "No Data Found");
            }
            else
            {
                string a = "ModelComplexity.xlsx";
                ExportExcels.Export(a, dataGridView2);
            }
        }

        private void Upload_TPPH_Load_1(object sender, EventArgs e)
        {

        }
    }
    
}
