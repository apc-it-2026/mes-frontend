using MaterialSkin.Controls;
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

namespace Domestic_Schedule
{
    public partial class Domestic_Schedule : MaterialForm
    {
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        public Boolean isTitle = false;
        IList<object[]> data = null;
        private ExcelProcessor _currentExcelProcessor = null;
        public Domestic_Schedule()
        {
            InitializeComponent();
        }

        private void Btn_search_Click(object sender, EventArgs e)
        {
            GetSO_Schedule();
        }

        public void GetSO_Schedule()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("SalesOrder", txtso.Text);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Transactions_Server", "GetSO_Schedule",
                Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
            if (ret.IsSuccess)
            {
                DataTable dtJson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ret.RetData);
                if (dtJson1.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dtJson1;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    MessageHelper.ShowErr(this, "No data found");
                }

            }
            else
            {
                MessageHelper.ShowErr(this, ret.ErrMsg);
            }
        }

        private void Btn_template_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Download Template" + "\\Import_Template_for_Planning_Schedule.xlsx";
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xlsx";
            saveDialog.Filter = "Excel文件|*.xlsx";
            saveDialog.FileName = "Import_Template_for_Planning_Schedule.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                saveFileName = saveDialog.FileName;
                System.IO.File.Copy(path, saveFileName, true);
                System.Diagnostics.Process.Start(saveFileName);
            }
        }


        private void Btn_import_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data?", "Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                        MessageHelper.ShowErr(this, "Import template error, please refer to");
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
                            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.Client.APIURL,
                                "KZ_FAST_REPORT_API", "KZ_FAST_REPORT_API.Controllers.Domestic_Transactions_Server", "ImportPlanningData",
                                Program.Client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
                            ResultObject ret = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultObject>(retdata);
                            if (ret.IsSuccess)
                            {
                                MessageHelper.ShowSuccess(this, "Imported successfully");
                            }
                            else
                            {
                                MessageBox.Show(ret.ErrMsg);
                               // MessageHelper.ShowErr(this, ret.ErrMsg);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = SJeMES_Framework.Common.UIHelper.UImsg(ex.Message, Program.Client, Program.Client.WebServiceUrl, Program.Client.Language);
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, msg);
                }
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
                MessageHelper.ShowErr(this, ex.Message);
            }
        }
    }
}
