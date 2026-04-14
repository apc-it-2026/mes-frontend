using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class ExeclHelper : Form
    {

        /// <summary>
        /// 导出DataTable数据到Excel
        /// </summary>
        /// <param name="DataTable">数据源（要导出的数据源，类型应是DataTable）</param>
        /// <param name="fileName">要保存的文件名，需要加扩展有.xls 例：'Test.xls'</param>
        ///  <param name="headDic">列标题字典</param>
        public static void ExportToTrueExcel(DataTable DT, Dictionary<string, string> headDic = null, string FileNames = "")
        {
            var Url = string.Empty;
            string sheetName = FileNames;
            FileNames = FileNames + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);

            //列标题
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            int index = 0;
            foreach (DataColumn column in DT.Columns)
            {
                if (headDic != null && headDic.Keys.Contains(column.ColumnName))
                {
                    headerRow.CreateCell(index).SetCellValue(headDic[column.ColumnName]);
                }
                else
                {
                    headerRow.CreateCell(index).SetCellValue(column.ColumnName);
                }
                index++;
            }
            //Excel 内容
            int rowIndex = 1;
            foreach (DataRow row in DT.Rows)
            {
                HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in DT.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            try
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.Description = "Please choose a save path";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        MessageBox.Show("The save path cannot be empty", "Prompt");
                        return;
                    }
                    string path = dialog.SelectedPath;
                    string savePath = $"{path}\\{FileNames}";
                    using (FileStream fileStream = File.OpenWrite(savePath))
                    {
                        workbook.Write(fileStream);
                    }
                    MessageBox.Show("Export succeeded", "prompt");
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// DQA、MQA导出DataTable数据到Excel
        /// </summary>
        /// <param name="DataTable">数据源（要导出的数据源，类型应是DataTable）</param>
        /// <param name="fileName">要保存的文件名，需要加扩展有.xls 例：'Test.xls'</param>
        ///  <param name="headDic">列标题字典</param>
        public static void ExportToTrueExcelEx(DataTable DT, Dictionary<string, string> headDicEx = null,Dictionary<string, string> headDic = null, string FileNames = "")
        {
            var Url = string.Empty;
            string sheetName = FileNames;
            FileNames = FileNames + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sheetName);

            //Excle头部数据
            int ColIndex = 0;//列序号
            int RowsIndex = 0;//行序号
            HSSFRow headerRowEx = (HSSFRow)sheet.CreateRow(RowsIndex);
            foreach (string item in headDicEx.Keys)
            {
                headerRowEx.CreateCell(2 * ColIndex).SetCellValue(item);
                headerRowEx.CreateCell((2 * ColIndex) + 1).SetCellValue(headDicEx[item]);
                ColIndex++;

                if (ColIndex == 2)
                {
                    ColIndex = 0;
                    RowsIndex++;
                    headerRowEx = (HSSFRow)sheet.CreateRow(RowsIndex);
                }
            }

            //列标题
            int index = 0;
            RowsIndex++;
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(RowsIndex);
            foreach (DataColumn column in DT.Columns)
            {
                if (headDic != null && headDic.Keys.Contains(column.ColumnName.ToLower()))
                {
                    headerRow.CreateCell(index).SetCellValue(headDic[column.ColumnName.ToLower()]);
                }
                else
                {
                    headerRow.CreateCell(index).SetCellValue(column.ColumnName);
                }
                index++;
            }
            RowsIndex++;
            //Excel 内容
            int rowIndex = RowsIndex;
            foreach (DataRow row in DT.Rows)
            {
                HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in DT.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            try
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.Description = "Please choose a save path";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(dialog.SelectedPath))
                    {
                        MessageBox.Show("The save path cannot be empty", "Prompt");
                        return;
                    }
                    string path = dialog.SelectedPath;
                    string savePath = $"{path}\\{FileNames}";
                    using (FileStream fileStream = File.OpenWrite(savePath))
                    {
                        workbook.Write(fileStream);
                    }
                    MessageBox.Show("Export succeeded", "prompt");
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
