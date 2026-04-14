using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class NotNull
    {
        /// <summary>
        /// 随机一项为空就返回true(仅支持String类型)
        /// </summary>
        public static bool Trues(params string[] arr)
        {
            bool flag = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(arr[i].ToString()))
                {
                    flag = true;
                    break;
                }
            }
            return flag;

        }
    }
}
