using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SampleNG_Report
{
    public partial class F_SampleNG_Report : MaterialForm
    {
        DataTable partDt;
        public class ComboboxEntry
        {
            public string ENUM_CODE { get; set; }
            public string ENUM_VALUE { get; set; }
        }
        public F_SampleNG_Report()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView4.AutoGenerateColumns = false;
            tabControl1.TabPages[1].Parent = null;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView2.ColumnHeadersHeight = 50;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView3.ColumnHeadersHeight = 50;
            dataGridView2.RowTemplate.Height = 50;
            dataGridView3.RowTemplate.Height = 25;

            List<ComboboxEntry> pp = new List<ComboboxEntry>();
            pp.Add(new ComboboxEntry() { ENUM_CODE = "Y", ENUM_VALUE = "样品室" });
            pp.Add(new ComboboxEntry() { ENUM_CODE = "N", ENUM_VALUE = "工艺部" });
            comboBox1.DataSource = pp;
            comboBox1.DisplayMember = "ENUM_VALUE";
            comboBox1.ValueMember = "ENUM_CODE";
            comboBox2.DataSource = pp;
            comboBox2.DisplayMember = "ENUM_VALUE";
            comboBox2.ValueMember = "ENUM_CODE";
            comboBox3.DataSource = pp;
            comboBox3.DisplayMember = "ENUM_VALUE";
            comboBox3.ValueMember = "ENUM_CODE";

            GetPartList();
        }
        public void GetPartList()
        {
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetPartList", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);
                partDt = dataTable;
            }
            else
            {
                SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            string sample_no = txtSampleOrder1.Text.Trim().ToString();
            string art_no = txtArtNo1.Text.Trim().ToString();
            string suppliers_code = txtVendor1.Text.ToString();
            string part_no = txtPartNo1.Text.ToString();
            string startDate = txtStartDate1.Value.ToString("yyyyMMdd");
            string endDate = txtEndDate1.Value.ToString("yyyyMMdd");
            string sendUnit = txtSendUnit1.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("art_no", art_no);
            dictionary.Add("suppliers_code", suppliers_code);
            dictionary.Add("part_no", part_no);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            dictionary.Add("sendUnit", sendUnit);
            dictionary.Add("COL", comboBox2.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetReport", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dataTable);        //返回 DataRow
                    TotaldgvRow2[12] = "合计";
                   // TotaldgvRow2[11] = DBNull.Value;
                    TotaldgvRow2[13] = DBNull.Value;
                
                    string v = TotaldgvRow2["received_quantity"].ToString();
                    string v1 = TotaldgvRow2["pass_qty"].ToString();
                    TotaldgvRow2["rft"] =Math.Round(double.Parse(v1)/double.Parse(v)*100,2) +"%";
                    dataTable.Rows.Add(TotaldgvRow2);
                }

                this.dataGridView1.DataSource = dataTable;
               
            }
            else
            {
                MessageBox.Show("查无数据!");
                this.dataGridView1.DataSource = null;
            }
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("不良报表",dataGridView1);
        }

        private void btnResetSelect1_Click(object sender, EventArgs e)
        {
            txtSampleOrder1.Text = "";
            txtArtNo1.Text = "";
            txtVendor1.Text = "";
            txtPartNo1.Text = "";
            txtSendUnit1.Text = "";
            textBox3.Text = "";
            dataGridView1.DataSource = null;
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            string startDate = dateTimePicker2.Value.ToString("yyyyMMdd");
            string endDate = dateTimePicker1.Value.ToString("yyyyMMdd");
            string suppliers_code = textBox2.Text.ToString();
            string sendUnit = textBox1.Text.ToString();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            //dictionary.Add("sample_no", sample_no);
            // dictionary.Add("art_no", art_no);
            // dictionary.Add("suppliers_code", suppliers_code);
            // dictionary.Add("part_no", part_no);
            dictionary.Add("suppliers_code", suppliers_code);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            dictionary.Add("sendUnit", sendUnit);
            dictionary.Add("COL", comboBox1.SelectedValue.ToString());
            try
            {
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetColums", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                    DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                    CreateColum(dataTable);
                    foreach (DataGridViewColumn column in dataGridView3.Columns)
                    { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
                }
                else { throw new Exception(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString()); }

                string ret1 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetLeftData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["IsSuccess"]))
                {
                    string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret1)["RetData"].ToString();
                    DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                    // CreateColum(dataTable);
                    dataGridView2.DataSource = dataTable;
                    foreach (DataGridViewColumn column in dataGridView2.Columns)
                    { column.SortMode = DataGridViewColumnSortMode.NotSortable; }
                }
                else { throw new Exception(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString()); }

                string ret2 = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetRightData", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["IsSuccess"]))
                {
                    dataGridView3.Rows.Clear();
                    string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2)["RetData"].ToString();
                    DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                    MarryDatatable(dataTable);
                }
                else { throw new Exception(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString()); }

            }
            catch (Exception ception)
            {
                MessageBox.Show(ception.Message);
                dataGridView2.DataSource = null;
                dataGridView3.Rows.Clear();
                dataGridView3.Columns.Clear();
                //throw;
            }
            
        }

        public void CreateColum(DataTable dt) 
        {
            dataGridView3.Columns.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView3.Columns.Add(dt.Rows[i][0].ToString(),dt.Rows[i][0].ToString());
            }
        }

        //匹配左边数据
        public void MarryDatatable(DataTable dt) 
        {
            DataTable dataTable = dataGridView2.DataSource as DataTable;
            int doubleRow = 0;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string suppliers_code = dataTable.Rows[i]["suppliers_code"].ToString();
                //string  = dataTable.Rows[i]["suppliers_code"].ToString();
                DataRow[] dataRows = dt.Select("suppliers_code ='" + suppliers_code+"'");
                dataGridView3.Rows.Add();
                dataGridView3.Rows.Add();
                for (int j = 0; j < dataRows.Length; j++)
                {
                    string headTXT = dataRows[j]["CAUSESNAME"].ToString();
                    string ng_qty = dataRows[j]["QTY"].ToString();
                    string received_qty = dataTable.Rows[i]["received_qty"].ToString();
                    double result = Convert.ToDouble(ng_qty) / Convert.ToDouble(received_qty) *100;
                    result = Math.Round(result, 2);
                    dataGridView3.Rows[doubleRow].Cells[headTXT].Value = dataRows[j]["QTY"].ToString();
                    dataGridView3.Rows[doubleRow+1].Cells[headTXT].Value = result+"%";
                }
                doubleRow += 2;
            } 
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView2.FirstDisplayedScrollingRowIndex;
            dataGridView3.HorizontalScrollingOffset = dataGridView2.HorizontalScrollingOffset;
        }

        private void dataGridView3_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView3.FirstDisplayedScrollingRowIndex;
            dataGridView2.HorizontalScrollingOffset = dataGridView3.HorizontalScrollingOffset;
        }
        private void btnResetSelect2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            partList partList = new partList(partDt);
            partList.sengMessage += new partList.sengPartList(GetInfo);
            partList.ShowDialog();
        }

        void GetInfo(string name,string value)
        {
            this.txtPartNo1.Text = name;
            this.textBox3.Text = value;
            this.textBox4.Text = value;
            this.textBox5.Text = name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sample_no = txtSampleOrder1.Text.Trim().ToString();
            string art_no = txtArtNo1.Text.Trim().ToString();
            string suppliers_code = txtVendor1.Text.ToString();
            string part_no = txtPartNo1.Text.ToString();
            string startDate = txtStartDate1.Value.ToString("yyyyMMdd");
            string endDate = txtEndDate1.Value.ToString("yyyyMMdd");
            string sendUnit = txtSendUnit1.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("art_no", art_no);
            dictionary.Add("suppliers_code", suppliers_code);
            dictionary.Add("part_no", part_no);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            dictionary.Add("sendUnit", sendUnit);
            dictionary.Add("COL", comboBox2.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "getTatolNumReposrt", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);

                DataTable dataTable1 = new DataTable();
                
                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable1.Columns.Add(dataTable.Rows[i]["VEND_NAME"].ToString());
                }
                DataRow dataRow = dataTable1.NewRow();
                DataRow dataRow1 = dataTable1.NewRow();
                DataRow dataRow2 = dataTable1.NewRow();
                DataRow dataRow3 = dataTable1.NewRow();
                dataTable1.Rows.Add(dataRow);
                dataTable1.Rows.Add(dataRow1);
                dataTable1.Rows.Add(dataRow2);
                dataTable1.Rows.Add(dataRow3);
                
                for (int i = 0; i < dataTable1.Columns.Count; i++)
                {
                    dataTable1.Rows[0][dataTable1.Columns[i].ColumnName] = dataTable.Rows[i]["RECE_QTY"].ToString();
                    dataTable1.Rows[1][dataTable1.Columns[i].ColumnName] = dataTable.Rows[i]["NG_QTY"].ToString();
                    dataTable1.Rows[2][dataTable1.Columns[i].ColumnName] = Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString())- Convert.ToDecimal(dataTable.Rows[i]["NG_QTY"].ToString());
                    if (Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString()) == 0)
                    {
                        dataTable1.Rows[3][dataTable1.Columns[i].ColumnName] = "0%";
                    }
                    else
                    {
                        dataTable1.Rows[3][dataTable1.Columns[i].ColumnName] = Convert.ToDecimal(dataTable.Rows[i]["NG_QTY"].ToString()) / Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString())*100+"%";
                    }
                }

                NewExportExcels.ExportExcels.Export("不良报表汇总", dataTable1);


            }
            else
            {
                MessageBox.Show("查无数据!");
                this.dataGridView1.DataSource = null;
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            partList partList = new partList(partDt);
            partList.sengMessage += new partList.sengPartList(GetInfo);
            partList.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
    
            dataGridView4.DataSource = null;
            string sample_no = textBox9.Text.Trim().ToString();
            string art_no = textBox8.Text.Trim().ToString();
            string suppliers_code = textBox7.Text.ToString();
            string part_no = textBox5.Text.ToString();
            string startDate = dateTimePicker4.Value.ToString("yyyyMMdd");
            string endDate = dateTimePicker3.Value.ToString("yyyyMMdd");
            string sendUnit = textBox6.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("art_no", art_no);
            dictionary.Add("suppliers_code", suppliers_code);
            dictionary.Add("part_no", part_no);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            dictionary.Add("sendUnit", sendUnit);
            dictionary.Add("COL", comboBox3.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "GetNGRepart", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);
                dataTable.Columns.Add("pass_qty");
                dataTable.Columns.Add("RFT");
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["pass_qty"] = Convert.ToDouble(dataTable.Rows[i]["rece_qty"]) - Convert.ToDouble(dataTable.Rows[i]["ng_qty"]);
                    if (Convert.ToDouble(dataTable.Rows[i]["rece_qty"]) == 0)
                    {
                        dataTable.Rows[i]["RFT"] = "0%";
                    }
                    else
                    {
                        dataTable.Rows[i]["RFT"] = Math.Round(Convert.ToDouble(dataTable.Rows[i]["ng_qty"]) / Convert.ToDouble(dataTable.Rows[i]["rece_qty"]) * 100,2)  + "%";
                    }
                }
                if (dataTable.Rows.Count > 0)
                {
                    DataRow TotaldgvRow2 = WinFormLib.TotalRow.GetTotalRow(dataTable);        //返回 DataRow
                    TotaldgvRow2["Size_no"] = "合计";
               
                    TotaldgvRow2[13] = DBNull.Value;

                    string v = TotaldgvRow2["rece_qty"].ToString();
                    string v1 = TotaldgvRow2["pass_qty"].ToString();
                    TotaldgvRow2["rft"] = Math.Round(double.Parse(v1) / double.Parse(v) * 100, 2) + "%";
                    dataTable.Rows.Add(TotaldgvRow2);
                }


                this.dataGridView4.DataSource = dataTable;

            }
            else
            {
                MessageBox.Show("查无数据!");
                this.dataGridView1.DataSource = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sample_no = textBox9.Text.Trim().ToString();
            string art_no = textBox8.Text.Trim().ToString();
            string suppliers_code = textBox7.Text.ToString();
            string part_no = textBox5.Text.ToString();
            string startDate = dateTimePicker4.Value.ToString("yyyyMMdd");
            string endDate = dateTimePicker3.Value.ToString("yyyyMMdd");
            string sendUnit = textBox6.Text.ToString();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("sample_no", sample_no);
            dictionary.Add("art_no", art_no);
            dictionary.Add("suppliers_code", suppliers_code);
            dictionary.Add("part_no", part_no);
            dictionary.Add("startDate", startDate);
            dictionary.Add("endDate", endDate);
            dictionary.Add("sendUnit", sendUnit);
            dictionary.Add("COL", comboBox2.SelectedValue.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.Sample_NG_ReportService", "getTatolNumReposrt", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string retData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dataTable = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(retData);

                DataTable dataTable1 = new DataTable();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable1.Columns.Add(dataTable.Rows[i]["VEND_NAME"].ToString());
                }
                DataRow dataRow = dataTable1.NewRow();
                DataRow dataRow1 = dataTable1.NewRow();
                DataRow dataRow2 = dataTable1.NewRow();
                DataRow dataRow3 = dataTable1.NewRow();
                dataTable1.Rows.Add(dataRow);
                dataTable1.Rows.Add(dataRow1);
                dataTable1.Rows.Add(dataRow2);
                dataTable1.Rows.Add(dataRow3);

                for (int i = 0; i < dataTable1.Columns.Count; i++)
                {
                    dataTable1.Rows[0][dataTable1.Columns[i].ColumnName] = dataTable.Rows[i]["RECE_QTY"].ToString();
                    dataTable1.Rows[1][dataTable1.Columns[i].ColumnName] = dataTable.Rows[i]["NG_QTY"].ToString();
                    dataTable1.Rows[2][dataTable1.Columns[i].ColumnName] = Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString()) - Convert.ToDecimal(dataTable.Rows[i]["NG_QTY"].ToString());
                    if (Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString()) == 0)
                    {
                        dataTable1.Rows[3][dataTable1.Columns[i].ColumnName] = "0%";
                    }
                    else
                    {
                        dataTable1.Rows[3][dataTable1.Columns[i].ColumnName] = Math.Round( Convert.ToDecimal(dataTable.Rows[i]["NG_QTY"].ToString()) / Convert.ToDecimal(dataTable.Rows[i]["RECE_QTY"].ToString()),2) * 100 + "%";
                    }
                }

              //  NewExportExcels.ExportExcels.Export("不良报表汇总", dataTable1);
                excelUtil("不良报表汇总", dataTable1);

            }
            else
            {
                MessageBox.Show("查无数据!");
                this.dataGridView1.DataSource = null;
            }

        }


        public void excelUtil(string fileName, DataTable myDGV)
        {
            string text = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = fileName;
            saveFileDialog.ShowDialog();
            text = saveFileDialog.FileName;
            if (text.IndexOf(":") < 0)
            {
                return;
            }

            Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
            if (application == null)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }

            Microsoft.Office.Interop.Excel.Workbooks workbooks = application.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)(dynamic)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Worksheet worksheet2 = null;
            worksheet2 = (Microsoft.Office.Interop.Excel.Worksheet)(dynamic)workbook.ActiveSheet;
            
           
            for (int i = 1; i < myDGV.Columns.Count+1; i++)
            {
                worksheet2.Rows[1].Cells[i+1] = "'" + myDGV.Columns[i-1].ColumnName.ToString();
            }
        
          
            try
            {
                object[,] array = new object[myDGV.Rows.Count, myDGV.Columns.Count];
                for (int j = 0; j < myDGV.Rows.Count; j++)
                {
                    for (int k = 0; k < myDGV.Columns.Count; k++)
                    {
                        array[j, k] = myDGV.Rows[j][k].ToString();
                    }
                }
                worksheet2.Rows[2].Cells[1] = "总生产数（双）";
                worksheet2.Rows[3].Cells[1] = "不良数（双）";
                worksheet2.Rows[4].Cells[1] = "合格数（双）";
                worksheet2.Rows[5].Cells[1] = "RFT";
                Microsoft.Office.Interop.Excel.Range range = worksheet2.Range[(dynamic)worksheet.Cells[2, 2], (dynamic)worksheet.Cells[myDGV.Rows.Count+1, myDGV.Columns.Count+1]];
                range.Value2 = array;
               
                worksheet2 = range.Worksheet;
                //  worksheet2.Columns.EntireColumn.AutoFit();
                workbook.Saved = true;
                workbook.SaveCopyAs(text);
                application.Quit();
                GC.Collect();
                MessageBox.Show("保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NewExportExcels.ExportExcels.Export("不良报表", dataGridView4);
        }
    }
}
