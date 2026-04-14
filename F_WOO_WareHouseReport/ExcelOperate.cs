using System;
using System.Web;

namespace F_WOO_WareHouseReport
{
    internal class ExcelOperate
    {

        private object mValue = System.Reflection.Missing.Value;

        public ExcelOperate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Merge Cells
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void Merge(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Merge(mValue);
        }
        /// <summary>
        /// Sets the font size for contiguous areas
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="strStartCell">开始单元格</param>
        /// <param name="strEndCell">结束单元格</param>
        /// <param name="intFontSize">字体大小</param>
        public void SetFontSize(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, int intFontSize)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Size = intFontSize.ToString();
        }

        /// <summary>
        /// Landscape printing
        /// </summary>
        /// <param name="CurSheet"></param>
        public void xlLandscape(Microsoft.Office.Interop.Excel._Worksheet CurSheet)
        {
            CurSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

        }
        /// <summary>
        /// Portrait print
        /// </summary>
        /// <param name="CurSheet"></param>
        public void xlPortrait(Microsoft.Office.Interop.Excel._Worksheet CurSheet)
        {
            CurSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
        }
        /// <summary>
        /// Insert the specified value in the specified cell
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="Cell">单元格 如Cells[1,1]</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteCell(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objCell, object objValue)
        {
            CurSheet.get_Range(objCell, mValue).Value2 = objValue;

        }

        /// <summary>
        /// Insert the specified value in the specified Range
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="StartCell">开始单元格</param>
        /// <param name="EndCell">结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteRange(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, object objValue)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Value2 = objValue;
        }

        /// <summary>
        /// Merge cells and insert the specified value in the merged cell
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="objValue">文本、数字等值</param>
        public void WriteAfterMerge(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, object objValue)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Merge(mValue);
            CurSheet.get_Range(objStartCell, mValue).Value2 = objValue;

        }

        /// <summary>
        /// Set formulas for cells
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objCell">单元格</param>
        /// <param name="strFormula">公式</param>
        public void SetFormula(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objCell, string strFormula)
        {
            CurSheet.get_Range(objCell, mValue).Formula = strFormula;
        }


        /// <summary>
        /// cell wrapping
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void AutoWrapText(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).WrapText = true;
        }

        /// <summary>
        /// Set the font color for the entire contiguous area
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="clrColor">颜色</param>
        public void SetColor(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, System.Drawing.Color clrColor)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /// <summary>
        /// Set the cell background color for the entire contiguous area
        /// </summary>
        /// <param name="CurSheet"></param>
        /// <param name="objStartCell"></param>
        /// <param name="objEndCell"></param>
        /// <param name="clrColor"></param>
        public void SetBgColor(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, System.Drawing.Color clrColor)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Interior.Color = System.Drawing.ColorTranslator.ToOle(clrColor);
        }

        /// <summary>
        /// Set the font name of the contiguous area
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="fontname">字体名称 隶书、仿宋_GB2312等</param>
        public void SetFontName(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, string fontname)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Font.Name = fontname;
        }

        /// <summary>
        /// Set the font of the continuous area to bold
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetBold(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            //CurSheet.get_Range(objStartCell, objEndCell).Font.Bold = true;
            CurSheet.get_Range(objStartCell, objEndCell).Font.Size = 12;
        }


        /// <summary>
        /// Set the border of the continuous area: the top, bottom, left and right are black continuous borders
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetBorderAll(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            CurSheet.get_Range(objStartCell, objEndCell).Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

        }

        /// <summary>
        /// Set continuous area horizontally centered
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignCenter(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }

        /// <summary>
        /// Set the continuous area to the left horizontally
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignLeft(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
        }

        /// <summary>
        /// Set the continuous area horizontally to the right
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        public void SetHAlignRight(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell)
        {
            CurSheet.get_Range(objStartCell, objEndCell).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
        }


        /// <summary>
        /// Sets the display format for contiguous areas
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="strNF">如"#,##0.00"的显示格式</param>
        public void SetNumberFormat(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, string strNF)
        {
            CurSheet.get_Range(objStartCell, objEndCell).NumberFormat = strNF;

        }
        public void border(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object aa, object bb)
        {
            CurSheet.get_Range(aa, bb).Borders.LineStyle = 1;
        }

        /// <summary>
        /// set column width
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="strColID">列标识，如A代表第一列</param>
        /// <param name="dblWidth">宽度</param>
        public void SetColumnWidth(Microsoft.Office.Interop.Excel._Worksheet CurSheet, string strColID, double dblWidth)
        {
            ((Microsoft.Office.Interop.Excel.Range)CurSheet.Columns.GetType().InvokeMember("Item", System.Reflection.BindingFlags.GetProperty, null, CurSheet.Columns, new object[] { (strColID + ":" + strColID).ToString() })).ColumnWidth = dblWidth;
        }

        /// <summary>
        /// set column width
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="dblWidth">宽度</param>
        public void SetColumnWidth(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, double dblWidth)
        {
            CurSheet.get_Range(objStartCell, objEndCell).ColumnWidth = dblWidth;
        }


        /// <summary>
        /// set row height
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objStartCell">开始单元格</param>
        /// <param name="objEndCell">结束单元格</param>
        /// <param name="dblHeight">行高</param>
        public void SetRowHeight(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objStartCell, object objEndCell, double dblHeight)
        {
            CurSheet.get_Range(objStartCell, objEndCell).RowHeight = dblHeight;

        }


        /// <summary>
        /// Add hyperlinks to cells
        /// </summary>
        /// <param name="CurSheet">Worksheet</param>
        /// <param name="objCell">单元格</param>
        /// <param name="strAddress">链接地址</param>
        /// <param name="strTip">屏幕提示</param>
        /// <param name="strText">链接文本</param>
        public void AddHyperLink(Microsoft.Office.Interop.Excel._Worksheet CurSheet, object objCell, string strAddress, string strTip, string strText)
        {
            CurSheet.Hyperlinks.Add(CurSheet.get_Range(objCell, objCell), strAddress, mValue, strTip, strText);
        }

        /// <summary>
        /// save as xls file
        /// </summary>
        /// <param name="CurBook">Workbook</param>
        /// <param name="strFilePath">文件路径</param>
        public void Save(Microsoft.Office.Interop.Excel._Workbook CurBook, string strFilePath)
        {
            CurBook.SaveCopyAs(strFilePath);
        }

        /// <summary>
        /// save document
        /// </summary>
        /// <param name="CurBook">Workbook</param>
        /// <param name="strFilePath">文件路径</param>
        public void SaveAs(Microsoft.Office.Interop.Excel._Workbook CurBook, string strFilePath)
        {
            CurBook.SaveAs(strFilePath, mValue, mValue, mValue, mValue, mValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlShared, mValue, mValue, mValue, mValue, mValue);
        }

        /// <summary>
        /// save as html file
        /// </summary>
        /// <param name="CurBook">Workbook</param>
        /// <param name="strFilePath">文件路径</param>
        public void SaveHtml(Microsoft.Office.Interop.Excel._Workbook CurBook, string strFilePath)
        {
            CurBook.SaveAs(strFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml, mValue, mValue, mValue, mValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, mValue, mValue, mValue, mValue, mValue);
        }


        /// <summary>
        /// free memory
        /// </summary>
        public void Dispose(Microsoft.Office.Interop.Excel._Worksheet CurSheet, Microsoft.Office.Interop.Excel._Workbook CurBook, Microsoft.Office.Interop.Excel._Application CurExcel)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurSheet);
                CurSheet = null;
                CurBook.Close(false, mValue, mValue);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurBook);
                CurBook = null;

                CurExcel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(CurExcel);
                CurExcel = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (System.Exception ex)
            {
                HttpContext.Current.Response.Write("An error occurred while freeing Excel memory:" + ex);
            }
            finally
            {
                foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("Excel"))
                    //if (pro.StartTime < DateTime.Now)
                    pro.Kill();
            }
            System.GC.SuppressFinalize(this);

        }
    }
}