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

namespace TierMeeting
{
    public partial class PDPDeliveryForm : MaterialForm
    {
        private string strPath = Parameters.strPath;
        public PDPDeliveryForm()
        {
            InitializeComponent();
            InitUI();
        }
        private void InitUI()
        {
            SJeMES_Framework.Common.UIHelper.UIUpdate(this.Name, this, Program.client, "", Program.client.Language);
            gridData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.WindowState = FormWindowState.Maximized;
            int height = base.Height;
            gridData.ColumnHeadersHeight = Convert.ToInt32(height / 25);
            gridData.RowTemplate.Height = Convert.ToInt32(height / 25);
            gridData.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", (float)height / 60f, FontStyle.Regular, GraphicsUnit.Point, 134);
            gridData.DefaultCellStyle.Font = new Font("宋体", (float)height / 55f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.WindowState = FormWindowState.Maximized;
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
            ExcelApp.Workbook excelBook = excelApp.Workbooks.Open(strPath);
            ExcelApp._Worksheet excelSheet = excelBook.Sheets["KPI"];
            ExcelApp.Range rangeTitle = excelSheet.get_Range("D38:P38", Missing.Value);
            if (rangeTitle != null) {
                int width = base.Width;
                int i = 0;
                foreach (ExcelApp.Range r in rangeTitle)
                {
                    string str = r.Text;
                    gridData.Columns.Add(str, str);
                    if (i == 0)
                    {
                        gridData.Columns[i].Width = width / 7;
                    }
                    else
                    {
                        gridData.Columns[i].Width = width / 12;
                    }
                    i++;
                }
            }
            ExcelApp.Range range = excelSheet.get_Range("D39:P59", Missing.Value);
            if (range != null)
            {
                foreach (ExcelApp.Range r in range)
                {
                    string str = r.Text;
                    int tmpCol = r.Column;
                    int tmpRow = r.Row;
                    if (tmpCol == 4)
                        gridData.Rows.Add();
                    gridData.Rows[tmpRow - 39].Cells[tmpCol - 4].Value = str;
                }
            }
            gridData.DefaultCellStyle.Font = new Font("宋体", 20f, FontStyle.Regular, GraphicsUnit.Point, 134);
            excelBook.Close(false, Missing.Value, Missing.Value);
            excelApp.Quit();
        }

        private void PDPDeliveryForm_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
