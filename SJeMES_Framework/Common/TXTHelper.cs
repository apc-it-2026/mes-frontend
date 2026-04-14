using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class TXTHelper
    {
        #region Create(string TXTPath) 创建一个文件
        public static bool Create(string TXTPath)
        {
            bool  ret = true;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Create))
                {
                    
                }
            }
            catch (Exception ex) { ret = false; };
            return ret;
        }
        #endregion

        #region ReadLines(string TXTPath) 读取每行数据
        public static List<string> ReadLines(string TXTPath)
        {
            List<string> ret = new List<string>();
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                            ret.Add(sr.ReadLine());
                    }
                }
            }
            catch (Exception ex) { };
            return ret;
        } 
        #endregion

        #region ReadToEnd(string TXTPath) 读取所有数据
        public static string ReadToEnd(string TXTPath)
        {
            string ret = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        ret = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex) { };
            return ret;
        } 
        #endregion

        #region ReadTopLine(string TXTPath) 读取第一行数据
        public static string ReadTopLine(string TXTPath)
        {
            string ret = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        ret = sr.ReadLine();
                    }
                }
            }
            catch (Exception ex) { };
            return ret;
        } 
        #endregion

        #region ReadLine(string TXTPath, int Index) 读取Index那行数据
        public static string ReadLine(string TXTPath, int Index)
        {
            string ret = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        int i = 0;
                        while (i < Index + 1)
                        {
                            ret = sr.ReadLine();
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return ret;
        } 
        #endregion

        #region Write(string TXTPath, string Msg) 新建文本并写Msg
        public static bool Write(string TXTPath, string Msg)
        {
            bool ret = true;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(Msg);
                    }
                }
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }
        #endregion

        #region WriteLine(string TXTPath, string Msg) 新建文本并写一行Msg
        public static bool WriteLine(string TXTPath, string Msg)
        {
            bool ret = true;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Msg);
                    }
                }
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }
        #endregion

        #region WriteLineToEnd(string TXTPath, string Msg) 新建文本并写一行Msg
        public static bool WriteLineToEnd(string TXTPath, string Msg)
        {
            bool ret = true;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(Msg);
                    }
                }
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }
        #endregion

        #region WriteToEnd(string TXTPath, string Msg) 新建文本并写一行Msg
        public static bool WriteToEnd(string TXTPath, string Msg)
        {
            bool ret = true;
            try
            {
                using (FileStream fs = new FileStream(TXTPath, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(Msg);
                    }
                }
            }
            catch (Exception ex) { ret = false; }
            return ret;
        }
        #endregion

    }
}
