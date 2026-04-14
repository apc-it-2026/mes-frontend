using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AutoSendEmail
{
     public class excel
    {
        public void ExportExcel(System.Data.DataTable dt,string filename,string worksheetName)
        {
            if (dt != null)
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                if (excel == null)
                {
                    return;
                }

                //设置为不可见，操作在后台执行，为 true 的话会打开 Excel
                excel.Visible = false;

                //打开时设置为全屏显式
                //excel.DisplayFullScreen = true;

                //初始化工作簿
                Microsoft.Office.Interop.Excel.Workbooks workbooks = excel.Workbooks;

                //新增加一个工作簿，Add（）方法也可以直接传入参数 true
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                //同样是新增一个工作簿，但是会弹出保存对话框
                //Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

                //新增加一个 Excel 表(sheet)
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                //设置表的名称
                worksheet.Name = worksheetName;
                try
                {
                    //创建一个单元格
                    Microsoft.Office.Interop.Excel.Range range;

                    int rowIndex = 1;       //行的起始下标为 1
                    int colIndex = 1;       //列的起始下标为 1

                    //设置列名
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //设置第一行，即列名
                        worksheet.Cells[rowIndex, colIndex + i] = dt.Columns[i].ColumnName;

                        //获取第一行的每个单元格
                        range = worksheet.Cells[rowIndex, colIndex + i];

                        //设置单元格的内部颜色
                        range.Interior.ColorIndex = 33;

                        //字体加粗
                        range.Font.Bold = true;

                        //设置为黑色
                        range.Font.Color = 0;

                        //设置为宋体
                        range.Font.Name = "Arial";

                        //设置字体大小
                        range.Font.Size = 12;

                        //水平居中
                        range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                        //垂直居中
                        range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }

                    //跳过第一行，第一行写入了列名
                    rowIndex++;

                    //写入数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[rowIndex + i, colIndex + j] = dt.Rows[i][j].ToString();
                        }
                    }

                    //设置所有列宽为自动列宽
                    //worksheet.Columns.AutoFit();

                    //设置所有单元格列宽为自动列宽
                    worksheet.Cells.Columns.AutoFit();
                    //worksheet.Cells.EntireColumn.AutoFit();

                    //是否提示，如果想删除某个sheet页，首先要将此项设为fasle。
                    excel.DisplayAlerts = false;

                    //保存写入的数据，这里还没有保存到磁盘
                    workbook.Saved = true;

                    //设置导出文件路径
                    //string path = HttpContext.Current.Server.MapPath("/");

                    //设置新建文件路径及名称
                    string savePath = filename + ".xlsx";

                    //创建文件
                    FileStream file = new FileStream(savePath, FileMode.CreateNew);

                    //关闭释放流，不然没办法写入数据
                    file.Close();
                    file.Dispose();

                    //保存到指定的路径
                    workbook.SaveCopyAs(savePath);

                    ////还可以加入以下方法输出到浏览器下载
                    //FileInfo fileInfo = new FileInfo(savePath);
                    //OutputClient(fileInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    workbook.Close(false, Type.Missing, Type.Missing);
                    workbooks.Close();

                    //关闭退出
                    excel.Quit();

                    //释放 COM 对象
                    Marshal.ReleaseComObject(worksheet);
                    Marshal.ReleaseComObject(workbook);
                    Marshal.ReleaseComObject(workbooks);
                    Marshal.ReleaseComObject(excel);

                    worksheet = null;
                    workbook = null;
                    workbooks = null;
                    excel = null;

                    GC.Collect();
                }
            }
        }
        public void OutputClient(byte[] bytes)
        {
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();

            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm")));

            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("GB2312");

            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.Close();
        }
    }
}
