using NewExportExcels;
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

namespace RR_TEAMS_UPLOAD
{
    public partial class Form1 : Form
    {
        private ExcelProcessor _currentExcelProcessor = null;
        DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        public Boolean isTitle = false;
        IList<object[]> data = null;
        public Form1()
        {
            InitializeComponent();
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
                    dataGridView1.DataSource = dtJson1;
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
        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($@"Are you sure you want to save the data ?","Confirm Action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                                SJeMES_Control_Library.MessageHelper.ShowSuccess(this,"Imported successfully");
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
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

        private void Button6_Click(object sender, EventArgs e)
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
