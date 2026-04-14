using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mes.Util
{
    public class DataConvertUtil<T> where T : new()
    {
        /// <summary>
        /// 把DataTable转换成指定类型的List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertDataTableToList(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 把泛型List转换成DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertListToDataTable(List<T> list)
        {
            DataTable dt = new DataTable();
            // 获得此模型的公共属性      
            PropertyInfo[] propertys = typeof(T).GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                // 判断此属性是否有Getter      
                if (!pi.CanRead) continue;
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            foreach (T item in list)
            {
                propertys = item.GetType().GetProperties();
                DataRow newRow = dt.NewRow();
                foreach (PropertyInfo pi in propertys)
                {
                    if (!pi.CanRead) continue;
                    newRow[pi.Name] = pi.GetValue(item);
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }


    }
}
