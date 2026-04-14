using MaterialSkin.Controls;
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

namespace ImportInnerBox
{
    public partial class ImportInnerBoxForm :MaterialForm
    {
        IList<object[]> data = null;
        int count = 1;

        public ImportInnerBoxForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name,this,Program.client,"",Program.client.Language);
            String RET = SJeMES_Framework.Common.UIHelper.UIVisiable(this, "内盒标资料导入", Program.client);
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dataGridView != null)
            {
                this.dataGridView.Rows.Clear();
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (dataGridView1 != null)
                {
                    data = new List<object[]>();
                    this.dataGridView1.Rows.Clear();
                }
                foreach (string filename in ofd.FileNames)
                {
                    try
                    {
                        this.dataGridView1.Rows.Add(filename);
                        dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
                        this.getTextData(Path.GetFullPath(filename));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void getTextData(string fileName)
        {
            try
            {
                string extension = Path.GetExtension(fileName);
                switch (extension)
                {
                    case ".txt":
                        break;
                    default:
                        string mes = SJeMES_Framework.Common.UIHelper.UImsg("ImportInnerBox-0001", Program.client,"", Program.client.Language);
                        throw new Exception(mes+$"*{extension}");
                }
                //dataGridView.Name = "内盒标资料导入";
                //dataGridView.AllowUserToAddRows = false;
                StreamReader sr = new StreamReader(fileName, Encoding.Default);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Char[] separatpr = { '\t' };
                    string[] str = line.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Object[] row = new object[str.Length + 1];
                    if (count == 1)
                    {
                        for (int i = 1; i < str.Length + 1; i++)
                        {
                            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                            col.HeaderText = "column" + i;
                            col.Name = "column" + i;
                            col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                            this.dataGridView.Columns.Add(col);
                        }
                    }
                    for (int i = 0; i < str.Length; i++)
                    {
                        row[i] = str[i];
                    }
                    if (row[0] != null && !row[0].ToString().Equals(""))
                    {
                        this.dataGridView.Rows.Add(row);
                        row[str.Length] = line.ToString();
                        data.Add(row);
                        count++;
                    }

                }
                dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);

        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            import_db();
        }
        private void import_db()
        {
            DataTable tab = new DataTable();
            tab.Columns.Add("column1");
            tab.Columns.Add("column2");
            if (data != null)
            {
                foreach (var o in data)
                {
                    DataRow dr = tab.NewRow();
                    dr[0] = o[0];
                    dr[1] = o[o.Length - 1];
                    tab.Rows.Add(dr);
                }
                Dictionary<string, Object> d = new Dictionary<string, object>();
                d.Add("data", tab);
                string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ImportInnerBoxServer", "UpLoad", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(d));
                if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
                else
                {
                    SJeMES_Control_Library.MessageHelper.ShowErr(this, Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["ErrMsg"].ToString());
                }
            }
        }

        private void butqueryNo_Click(object sender, EventArgs e)
        {
            Dictionary<string, Object> p = new Dictionary<string, object>();
            p.Add("date", dateTimePicker1.Text.ToString());
            string ret = SJeMES_Framework.WebAPI.WebAPIHelper.Post(Program.client.APIURL, "KZ_MESAPI", "KZ_MESAPI.Controllers.ImportInnerBoxServer", "Query", Program.client.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(p));
            if (Convert.ToBoolean(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["IsSuccess"]))
            {
                string json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret)["RetData"].ToString();
                DataTable dtJson = SJeMES_Framework.Common.JsonHelper.GetDataTableByJson(json);
                dataGridView2.DataSource = dtJson.DefaultView;
                dataGridView2.Update();
            }
            dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Long;
            this.dateTimePicker1.CustomFormat = null;
            this.dateTimePicker1.Checked = true;
        }


        


        private void butExport_Click(object sender, EventArgs e)
        {
            string saveFileName = SJeMES_Framework.Common.UIHelper.UImsg("ImportInnerBox-savefilename", Program.client, "",Program.client.Language);
            ExportExcels(saveFileName, dataGridView2);
        }

        private void ExportExcels(string fileName, DataGridView myDGV)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = fileName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                //ImportInnerBox-0002
                string mes = SJeMES_Framework.Common.UIHelper.UImsg("ImportInnerBox-0002", Program.client,"", Program.client.Language);
                MessageBox.Show(mes);
                return;
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写入标题
            for (int i = 0; i < myDGV.ColumnCount; i++)
            {
                worksheet.Cells[1, i + 1] = myDGV.Columns[i].HeaderText;
            }
            //写入数值
            for (int r = 0; r < myDGV.Rows.Count; r++)
            {
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    worksheet.Cells[r + 2, i + 1].NumberFormatLocal = "@";
                    worksheet.Cells[r + 2, i + 1] = myDGV.Rows[r].Cells[i].Value;
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                }
                catch (Exception ex)
                {
                    string mes = SJeMES_Framework.Common.UIHelper.UImsg("ImportInnerBox-0003", Program.client,"", Program.client.Language);
                    MessageBox.Show(mes + ex.Message);
                }
            }
            xlApp.Quit();
            GC.Collect();//强行销毁
            string saveInfo = SJeMES_Framework.Common.UIHelper.UImsg("ImportInnerBox-success", Program.client, "",Program.client.Language);
            string tips = SJeMES_Framework.Common.UIHelper.UImsg("msg-00002", Program.client,"", Program.client.Language);
            MessageBox.Show(saveInfo,tips, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void clear_Click(object sender, EventArgs e)
        {
            this.dateTimePicker1.Format = DateTimePickerFormat.Custom;
            this.dateTimePicker1.CustomFormat = " ";
            this.dateTimePicker1.Checked = false;
        }
    }
}
