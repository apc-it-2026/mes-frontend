using MaterialSkin.Controls;
using Newtonsoft.Json;
using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace F_TMS_TierMeeting3_Main
{
    public partial class KPIDeliveryForm : MaterialForm
    {
        //private string strError = "Error when upload file!";
        private string strPath = Program.strPath;
        public KPIDeliveryForm()
        {
            InitializeComponent();
            
        }
        private void InitUI() {
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //UploadFile();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xls)|*.xlsx";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileNames.FirstOrDefault();
                try
                {
                    File.Copy(fileName, strPath, true);
                    GetData();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }
        private void UploadFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Excel files (*.xls)|*.xlsx";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog.FileNames.FirstOrDefault();
                try
                {
                    File.Copy(fileName, strPath, true);
                    WebClient client = new WebClient();
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.UploadFile(@"http://localhost:60626/uploads/", "POST", fileName);
                    client.Dispose();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowErr(this, ex.Message);
                }
            }
        }
        private void GetData()
        {
            gridData.Rows.Clear();
            gridData.Columns.Clear();
            ExcelApp.Application excelApp = new ExcelApp.Application();
            if (excelApp == null)
            {
                Console.WriteLine("Excel is not installed!!");
                return;
            }
            //ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "delivery.xlsx");
            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(strPath);
            ExcelApp._Worksheet excelSheet = excelBook.Sheets["KPI"];
            ExcelApp.Range rangeTitle = excelSheet.get_Range("D2:Q2", Missing.Value);
            if (rangeTitle != null) {
                int i = 0;
                foreach (ExcelApp.Range r in rangeTitle)
                {
                    string str = r.Text;
                    gridData.Columns.Add(str, str);
                    if (i == 0)
                    {
                        gridData.Columns[i].Width = 300;
                    }
                    else
                    {
                        gridData.Columns[i].Width = 200;
                    }
                    i++;
                }
            }
            ExcelApp.Range range = excelSheet.get_Range("D12:Q20", Missing.Value);
            if (range != null) {
                foreach (ExcelApp.Range r in range)
                {
                    string str = r.Text;
                    int tmpCol = r.Column;
                    int tmpRow = r.Row;
                    if (tmpCol == 4)
                        gridData.Rows.Add();
                    gridData.Rows[tmpRow - 12].Cells[tmpCol - 4].Value = str;
                }
            }
            //excelBook.Close(0);
            excelBook.Close(false, Missing.Value, Missing.Value); 
            excelApp.Quit();
            //var process = System.Diagnostics.Process.GetProcessesByName("Excel");
            //foreach (var p in process)
            //{
            //    if (!string.IsNullOrEmpty(p.ProcessName))
            //    {
            //        try
            //        {
            //            p.Kill();
            //        }
            //        catch { }
            //    }
            //}
        }

        private void DetailDeliveryForm_Load(object sender, EventArgs e)
        {
            GetData();
            InitUI();
        }

        private void btnMDP_Click(object sender, EventArgs e)
        {
            MDPDeliveryForm frm = new MDPDeliveryForm();
            frm.Show();
        }

        private void btnPDP_Click(object sender, EventArgs e)
        {
            PDPDeliveryForm frm = new PDPDeliveryForm();
            frm.Show();
        }

        private void btnSDP_Click(object sender, EventArgs e)
        {
            SDPDeliveryForm frm = new SDPDeliveryForm();
            frm.Show();
        }
        
        ////////////////////////////////test upload file
        ///
    }
}
