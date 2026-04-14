using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SJeMES_Framework.Common
{
    public class OtherPrograms
    {
        public static Dictionary<string, Assembly> AssemblyCache = new Dictionary<string, Assembly>();
        public static Hashtable InstanceCache = new Hashtable();

        public static void RunApp(string DllName, string ClassName, string Method, Dictionary<string, object> Parameters)
        {
            try
            {
                Assembly assembly = null;

                if (AssemblyCache.ContainsKey(DllName))
                {
                    assembly = AssemblyCache[DllName];
                }
                else
                {
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

                    if (!File.Exists(path + @"\" + DllName + ".dll"))
                    {
                        MessageBox.Show("找不到" + DllName + ".dll文件");
                        return;
                    }
                    assembly = Assembly.LoadFrom(path + @"\" + DllName + ".dll");

                    AssemblyCache.Add(DllName, assembly);
                }

                Type type = assembly.GetType(ClassName);

                object instance = null;

                if (InstanceCache.Contains(type))
                {
                    instance = InstanceCache[type];
                }
                else
                {
                    instance = Activator.CreateInstance(type);
                    InstanceCache.Add(type, instance);
                }

                MethodInfo mi = type.GetMethod(Method);


                object[] args = new object[1];

                args[0] = Parameters;

                object obj = mi.Invoke(instance, args);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
