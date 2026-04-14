using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;

namespace SJeMES_Framework.Printer
{
   public  class PrintBarCodeHelper
    {

        #region PrintNow 打印
        public static bool PrintNow(DataTable dt, string ModelName,string Path)
        {
            bool ret = false;

            string Data = string.Empty;

            string FilePath = Path +@"\"+ ModelName+".txt";

            foreach (DataColumn dc in dt.Columns)
            {
                Data += dc.ColumnName + ",";
            }

            Data = Data.Remove(Data.Length - 1) + "\r\n";

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dr[dc].ToString() + ",";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";
            }


            WriteText(Data, FilePath);

            #region 启动答应程序
            Process p = new Process();

            p.StartInfo.FileName =  ModelName + "_print.bat";

            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit(1000);//等待上述进程执行完毕
            ret = true;
            if (p.HasExited == false)
            {
                p.Kill();
                //return "外部调用响应超时";
            }

            #endregion


            return ret;

        }
        public static bool WriteTxt(DataTable dt, string ModelName, string Path, int printQty)
        {
            try
            {
                string Data = string.Empty;

                string FilePath = Path + @"\" + ModelName + ".txt";

                foreach (DataColumn dc in dt.Columns)
                {
                    Data += dc.ColumnName + ",";
                }

                Data = Data.Remove(Data.Length - 1) + "\r\n";

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < printQty; i++)
                    {
                        foreach (DataColumn dc in dt.Columns)
                        {
                            Data += dr[dc].ToString() + ",";
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

        public static bool PrintNow(string ModelName)
        {
            bool ret = false;

          

            #region 启动答应程序
            Process p = new Process();

            p.StartInfo.FileName = ModelName + "_print.bat";

            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit(1000);//等待上述进程执行完毕
            ret = true;
            if (p.HasExited == false)
            {
                p.Kill();
                //return "外部调用响应超时";
            }

            #endregion


            return ret;

        }
        #endregion

        #region WriteText 写入数据
        private static void WriteText(string str, string FilePath)
        {
            string fileName = FilePath;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            System.IO.File.AppendAllText(fileName, str, Encoding.UTF8);
        }
        #endregion

    }
}
