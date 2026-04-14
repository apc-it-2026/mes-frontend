
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class PrintHelper
    {

        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机

        /// <summary>
        /// 条码打印
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="Txt">数据文本.txt</param>
        /// <param name="Bat">可执行文件.bat</param>
        public static void PrintBarCode(DataTable dt,string Txt,string Bat)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {//Path.GetFileName(fullPath);
                    string printType = "Microsoft Print to PDF";

                    WriteTxt(dt, $"{Path.GetFileNameWithoutExtension(Txt)}", Application.StartupPath + $"/Printer/BarCodeModel/{Txt}", 1);
                    //Program.DefaultPrinter = printType;
                    //SetDefaultPrinter(Program.DefaultPrinter);
                    SetDefaultPrinter(printType);

                    Thread.Sleep(1000);

                    #region 启动答应程序
                    Process p = new Process();
                    p.StartInfo.FileName = $"{Bat}";
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.UseShellExecute = false;
                    //p.Start();//启动 
                    if (p.Start())
                    {
                        MessageBox.Show("打印成功！");
                    }
                    else
                    {
                        MessageBox.Show("打印失败！");
                    }

                    p.WaitForExit(5 * 1000);//等待上述进程执行完毕
                                            //p.WaitForExit();//这个会一直等待
                    if (p.HasExited == false)
                    {
                        p.Kill();
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }
        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path;

                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dc.ColumnName + "￥";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Data += dr[dc].ToString() + "￥";
                        }

                        Data = Data.Remove(Data.Length - 1) + "\r\n";
                    }

                }
                WriteText(Data, FilePath);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        private static void WriteText(string str, string FilePath)
        {
            string fileName = FilePath;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.AppendAllText(fileName, str, Encoding.UTF8);
        }
    }
}
