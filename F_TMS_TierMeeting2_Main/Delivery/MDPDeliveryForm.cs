using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelApp = Microsoft.Office.Interop.Excel;

namespace F_TMS_TierMeeting2_Main
{
    public partial class MDPDeliveryForm : MaterialForm
    {
        private string strPath = Parameters.strPath; 
        public MDPDeliveryForm()
        {
            InitializeComponent();
            InitUI();
        }
        private void InitUI()
        {
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.WindowState = FormWindowState.Maximized;
        }
        private void GetData() {
            gridData.Rows.Clear();
            gridData.Columns.Clear();
            ExcelApp.Application excelApp = new ExcelApp.Application();
            if (excelApp == null)
            {
                Console.WriteLine("Excel is not installed!!");
                return;
            }
            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(strPath);
            ExcelApp._Worksheet excelSheet = excelBook.Sheets["Delay summery "];
            ExcelApp.Range rangeTitle = excelSheet.get_Range("A1:M1", Missing.Value);
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
                
            ExcelApp.Range range = excelSheet.get_Range("A2:M21", Missing.Value);
            if (range != null)
            {
                foreach (ExcelApp.Range r in range)
                {
                    string str = r.Text;
                    int tmpCol = r.Column;
                    int tmpRow = r.Row;
                    if (tmpCol == 1)
                        gridData.Rows.Add();
                    gridData.Rows[tmpRow - 2].Cells[tmpCol - 1].Value = str;
                }
            }
            excelBook.Close(false, Missing.Value, Missing.Value);
            excelApp.Quit();
        }

        private void MDPDeliveryForm_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
