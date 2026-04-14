using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace F_BarcodePrinting_WakeOrder
{
   public static class FastReportHelper
    {

        /// <summary>
        /// 加载FastReport报表
        /// </summary>
        /// <param name="ctr">在哪个控件上显示报表</param>
        /// <param name="fileName">文件路径包含文件名后缀名</param>
        /// <param name="dic">报表的数据源名称和SQL，键是数据源名称值是SQL</param>
        /// <param name="dicParameter">报表的参数，key参数名，value参数值</param>
        public static void LoadFastReport(Control ctr,string fileName, Dictionary<string, string> dic, Dictionary<string, string> dicParameter)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    MessageBox.Show("找不到报表文件："+ fileName,"报表提示");
                    return;
                }
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件
             
                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览
                
                report.Load(fileName);//加载报表

                foreach (var item in dic)
                {
                    FastReport.Data.TableDataSource tds = report.GetDataSource(item.Key) as FastReport.Data.TableDataSource;//设置表格数据源名称
                    tds.SelectCommand = item.Value;//设置表格数据源SQL
                    tds.Init();//初始化
                }
                foreach (var item in dicParameter)
                {
                    report.SetParameterValue(item.Key, item.Value); // item.Key参数名 item.Value值
                }
                
                report.Prepare();
                report.ShowPrepared(true);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           

        }

        public static void LoadFastReport(string fastReportXML,string connectionString, Control ctr, Dictionary<string, string> dic, Dictionary<string, string> dicParameter)
        {
            try
            {
        
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件

                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览

                report.LoadFromString(fastReportXML);//加载报表
                report.Dictionary.Connections[0].ConnectionString = connectionString;//设置连接字符串
                foreach (var item in dic)
                {
                    FastReport.Data.TableDataSource tds = report.GetDataSource(item.Key) as FastReport.Data.TableDataSource;//设置表格数据源名称
                    tds.SelectCommand = item.Value;//设置表格数据源SQL
                    tds.Init();//初始化
                }
                foreach (var item in dicParameter)
                {
                    report.SetParameterValue(item.Key, item.Value); // item.Key参数名 item.Value值
                }

                report.Prepare();
                report.ShowPrepared(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }
        public static void LoadFastReport(GDSJ_Framework.Class.OrgClass org,string reportCode,string WebServiceUrl, Control ctr, Dictionary<string, string> dic)
        {
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '"+ reportCode+"'");
                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                var tab = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                string fastReportXml = string.Empty;
                string connectionString = string.Empty;
                if (tab.Rows.Count > 0)
                {

                    fastReportXml = tab.Rows[0]["ReportString"].ToString();
                    connectionString = @"Data Source=" + org.DBServer + @";AttachDbFilename=;Initial Catalog=" + org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + org.DBUser + ";Password=" + org.DBPassword;

                }else
                {
                    MessageBox.Show("请维护报表配置！");
                    return;
                }

                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件

                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览

                report.LoadFromString(fastReportXml);//加载报表
                report.Dictionary.Connections[0].ConnectionString = connectionString;//设置连接字符串
                foreach (var item in dic)
                {
                    FastReport.Data.TableDataSource tds = report.GetDataSource(item.Key) as FastReport.Data.TableDataSource;//设置表格数据源名称
                    tds.SelectCommand = item.Value;//设置表格数据源SQL
                    tds.Init();//初始化
                }
                //foreach (var item in dicParameter)
                //{
                //    report.SetParameterValue(item.Key, item.Value); // item.Key参数名 item.Value值
                //}

                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        public static void LoadFastReport(GDSJ_Framework.Class.OrgClass org, string reportCode, string WebServiceUrl, Control ctr, System.Data.DataTable dataTable,string dataSourceName)
        {
            try
            {
                Dictionary<string, string> p = new Dictionary<string, string>();
                p.Add("sql", "SELECT ReportPath,ReportXML,ReportString FROM[SJEMSSYS].[dbo].[SYSREPORT01M] where ReportCode = '" + reportCode + "'");
                string XML = GDSJ_Framework.Common.WebServiceHelper.RunService(WebServiceUrl, "SJEMS_API", "SJEMS_API.DataBase", "GetDataTable", p);
                string dtXML = GDSJ_Framework.Common.StringHelper.GetDataFromFirstTag(XML, "<RetData>", "</RetData>");
                var tab = GDSJ_Framework.Common.StringHelper.GetDataTableFromXML(dtXML);
                string fastReportXml = string.Empty;
                string connectionString = string.Empty;
                if (tab.Rows.Count > 0)
                {

                    fastReportXml = tab.Rows[0]["ReportString"].ToString();
                    connectionString = @"Data Source=" + org.DBServer + @";AttachDbFilename=;Initial Catalog=" + org.DBName + @";
                    Integrated Security=False;Persist Security Info=False;User ID=" + org.DBUser + ";Password=" + org.DBPassword;

                }
                else
                {
                    MessageBox.Show("请维护报表配置！");
                    return;
                }

                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件

                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览

                report.LoadFromString(fastReportXml);//加载报表
                report.Dictionary.Connections[0].ConnectionString = connectionString;//设置连接字符串

                FastReport.Data.TableDataSource tds = report.GetDataSource(dataSourceName) as FastReport.Data.TableDataSource;//设置表格数据源名称
                tds.Table = dataTable;


                tds.Init();
                
                report.Prepare();
                report.ShowPrepared(true);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return;
            }
        }

        public static void LoadFastReport(Control ctr, string fileName, Dictionary<string, string> dicParameter,DataTable dt,string tablename)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    MessageBox.Show("Report file not found：" + fileName, "report prompt");
                    return;
                }
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件

                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览

                report.Load(fileName);//加载报表
                                      //report.Dictionary.Connections[0].ConnectionString = @"Data Source=" + Program.Org.DBServer + @";AttachDbFilename=;Initial Catalog=" + Program.Org.DBName + @";
                                      //    Integrated Security=False;Persist Security Info=False;User ID=" + Program.Org.DBUser + ";Password=" + Program.Org.DBPassword;
                                      //report.Dictionary.Connections[0].CommandTimeout = 0;
                                      //foreach (var item in dicParameter)
                                      //{
                                      //    report.SetParameterValue(item.Key, item.Value); // item.Key参数名 item.Value值
                                      //}
                FastReport.Data.TableDataSource tds1 = report.GetDataSource(tablename) as FastReport.Data.TableDataSource;//设置表格数据源名称
                int dd = dt.Rows.Count;
                tds1.Table = dt;
                report.Prepare();
                report.ShowPrepared(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }
        /// <summary>
        /// 加载FastReport报表
        /// </summary>
        /// <param name="ctr">在哪个控件上显示报表</param>
        /// <param name="fileName">文件路径包含文件名后缀名</param>
        /// <param name="dic">报表的数据源名称和SQL，键是数据源名称值是SQL</param>
        /// <param name="dicParameter">报表的参数，key参数名，value参数值</param>
        public static void LoadFastReport(Control ctr, string fileName, Dictionary<string, System.Data.DataTable> dataTable, Dictionary<string, string> dicParameter)
        {
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    MessageBox.Show("Report file not found：" + fileName, "report prompt");
                    return;
                }
                ctr.Controls.Clear();
                FastReport.Report report = new FastReport.Report();
                FastReport.Preview.PreviewControl previewControl = new FastReport.Preview.PreviewControl();//创建报表控件

                previewControl.Dock = System.Windows.Forms.DockStyle.Fill;//填充整个控件
                ctr.Controls.Add(previewControl);//添加控件

                report.Preview = previewControl;//指定在这个控件预览，如果没有这行，会弹出一个窗口预览

                report.Load(fileName);//加载报表

                foreach (var item in dicParameter)
                {
                    report.SetParameterValue(item.Key, item.Value); // item.Key参数名 item.Value值
                }
                foreach (var item in dataTable)
                {
                    // item.Key参数名 item.Value值
                    report.RegisterData(item.Value, item.Key);
                }

                report.Prepare();
                report.PrintSettings.ShowDialog = false;
                report.ShowPrepared(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }


        }

       

    }
}
