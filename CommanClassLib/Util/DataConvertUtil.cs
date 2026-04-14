using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CommanClassLib.Util
{
    public class DataConvertUtil<T> where T : new()
    {
        /// <summary>
        ///     把DataTable转换成指定类型的List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertDataTableToList(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            string tempName = "";

            foreach (DataRow row in dt.Rows)
            {
                T type = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = type.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name; // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = row[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(type, value, null);
                    }
                }

                ts.Add(type);
            }

            return ts;
        }

        /// <summary>
        ///     DataTable转List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> DataTableToList(DataTable dt)
        {
            if (dt == null) return null;
            List<T> entityList = new List<T>();
            List<string> columnList = new List<string>();
            if (dt.Columns.Count > 0)
            {
                columnList.AddRange(from DataColumn column in dt.Columns select column.ColumnName);
            }

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            foreach (DataRow row in dt.Rows)
            {
                var entity = new T();
                foreach (var column in columnList)
                {
                    var proper = properties.Find(column, true);
                    if (proper != null)
                    {
                        var properType = proper.PropertyType;
                        if (proper.Converter != null)
                        {
                            object value = null;
                            var type = proper.PropertyType;
                            if (type == typeof(string))
                            {
                                value = row[column].ToString();
                            }
                            else if (type == typeof(decimal))
                            {
                                value = decimal.Parse(row[column].ToString());
                            }
                            else if (type == typeof(decimal?))
                            {
                                if (row[column] != null)
                                {
                                    value = decimal.Parse(row[column].ToString());
                                }
                            }
                            else
                            {
                                value = proper.Converter.ConvertTo(row[column], properType);
                            }

                            if (value != null)
                                proper.SetValue(entity, value);
                        }
                    }
                }

                entityList.Add(entity);
            }

            return entityList;
        }

        /// <summary>
        ///     把泛型List转换成DataTable
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