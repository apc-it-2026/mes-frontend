using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierMeeting
{
    public static class Parameters
    {
        public static string dateFormat = "yyyy/MM/dd";
        public enum QueryDeptType : int
        {
            All = 0,
            Plant = 1,
            Section = 2,
            Line = 3
        }
        //public static string strPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\delivery.xlsx";
        public static string strPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\delivery.xlsx";
        //public static string strPath = @"E:\data\\delivery.xlsx";
        public static string urlQuality = @"http://10.2.1.46";
        //public static string urlQuality = @"http://10.30.3.232:83/";
        public static double GreenRate = 100;
        public static double YellowRate = 90;
        public static string headerFont = "Microsoft YaHei";
        public static string textFont = "宋体";
    }
}
