using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Xml;
using Newtonsoft.Json;
using SJeMES_Framework.WebAPI;

namespace WorkingHoursService
{
    public partial class WorkingHoursService : ServiceBase
    {
        DateTime scheduleTime;

        private readonly Timer timer1 = new Timer();

        //是否读取App.config里面的key=minute的值
        private bool isConfigCustomTime;//是否有设置自定义时间

        public WorkingHoursService()
        {
            InitializeComponent();
            scheduleTime = DateTime.Today.AddHours(21); // 任务在一天中晚上9点运行一次。
        }

        protected override void OnStart(string[] args)
        {
            isConfigCustomTime = bool.Parse(ConfigurationManager.AppSettings["isConfigCustomTime"]); //设置读取App.config里面的key=minute的值
            if (isConfigCustomTime)
            {
                int minuteNum = int.Parse(ConfigurationManager.AppSettings["minute"]); //数据是可以读取的到的。
                timer1.Enabled = true; //开启定时器
                timer1.Interval = 60000 * minuteNum; //执行间隔时间,单位为毫秒;60000此时时间间隔为1分钟,现在是30分钟执行一次。
            }
            else
            {
                //Windows服务第一次才会进来这里，之后都去执行timer1_Tick方法
                //对于第一次，设置计划时间减去当前时间的秒数  
                double tillNextInterval = scheduleTime.Subtract(DateTime.Now).TotalSeconds * 1000;
                if (tillNextInterval < 0) tillNextInterval += new TimeSpan(24, 0, 0).TotalSeconds * 1000; //设置24小时候的时间
                //假如是晚上19:00，那么间隔时间是tillNextInterval=21-19=2,也就是第一次执行是2个小时之后执行一次
                //假如是晚上22:00，那么间隔时间是2+21=23个小时。tillNextInterval=21-22+24=23个小时之后执行一次
                //假如是第二天2:00,那么间隔时间是21-2=19小时。tillNextInterval=19个小时之后执行一次
                timer1.Enabled = true; //开启定时器
                timer1.Interval = tillNextInterval; //执行间隔....执行
            }

            timer1.Elapsed += timer1_Tick; //利用委托执行方法
            timer1.AutoReset = true; //设置是执行一次（false）还是一直执行(true)；
            WriteLog("工时记录定时服务运行成功");
        }

        //为什么要设置成只读的呢？这时因为如果在lock代码段中改变logHelperLock的值，其它线程就畅通无阻了，因为互斥锁的对象变了，object.ReferenceEquals必然返回false。
        private static readonly object logHelperLock = new object();
        public static void WriteLog(string strLog)
        { 
            object obj = new object();
            lock (obj)
            {
                //提供对指定驱动器上的信息的访问。  
                bool isHasDerive_D = true;
                bool isHasDeive_E = true;
                #region 这行代码可以不要
                DriveInfo driverInfoIsD = new DriveInfo("D");//提供对指定驱动器上的信息的访问。
                if (!driverInfoIsD.IsReady) //如没有D盘，没有DriveInfo这些代码，会报错，提示：设备未就绪
                {
                    isHasDerive_D = false;
                }

                DriveInfo driverInfoIsE = new DriveInfo("E");
                if (!isHasDerive_D && !driverInfoIsE.IsReady)
                {
                    isHasDeive_E = false;
                }
                #endregion
                //string path = Environment.CurrentDirectory;//当前项目目录下
                //string path = ConfigurationManager.AppSettings["path"];
                //string path = "D:"; //这里写死，那么API那边就不需要同步更新web.config文件。
                //string path = System.Environment.CurrentDirectory;//这个是当前项目下的目录。
                #region 类库里面写法
                //string webconfig = AppDomain.CurrentDomain.BaseDirectory + "web.config";
                //string webconfigXml = ReadToEnd(webconfig);
                //XmlDocument xmlDoc = new XmlDocument();
                //if (webconfigXml != null) xmlDoc.LoadXml(webconfigXml);

                //XmlNodeList studentNodeList = xmlDoc.SelectNodes("configuration/appSettings/add");
                //foreach (XmlNode item in studentNodeList)
                //{
                //    var key = item.Attributes.GetNamedItem("key");
                //    if (key.Value == "path")
                //    {
                //        path = item.Attributes["value"].Value;
                //    }
                //    break;
                //}
                #endregion

                bool isCreateOK = false;//默认未创建文件.
                string path = ConfigurationManager.AppSettings["path"];//这个没用必须在当前启动项目下面的config能读取的到,现在在类库项目中无法读取到值。即使添加了App.config，设置了始终复制属性，但是还是读取不到。
                if (string.IsNullOrWhiteSpace(path)) //如果没有配置默认为D盘。
                {
                    path = "D:";
                }
                string sFilePath = $@"{path}\" + DateTime.Now.ToString("yyyy-MM") + "日志"; //不要放在C:\Windows\System32下面不然找不到.可以查看C:\Windows下面System32文件夹下的System32-xxx的目录
                string sFileName = DateTime.Now.ToString("MM-dd") + ".log";
                sFileName = sFilePath + @"\" + sFileName; //文件的绝对路径
                try
                {
                    if (isHasDerive_D)
                    {
                        isCreateOK = IsCreatedLogFile(strLog, sFilePath, sFileName);
                    }
                    if (!isCreateOK && isHasDeive_E)
                    {
                        //如果E盘不存在会提示错误，提示如下：设备未就绪
                        sFilePath = @"E:\NET5_API\" + DateTime.Now.ToString("yyyy-MM") + "日志";
                        sFileName = DateTime.Now.ToString("MM-dd") + ".log";
                        sFileName = sFilePath + @"\" + sFileName; //文件的绝对路径
                        isCreateOK = IsCreatedLogFile(strLog, sFilePath, sFileName);
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = ex.Message;
                    //Access to the path is denied 报拒绝访问该路径错误，这个是因为对应盘符没有设置权限问题！给权限之后即可解决。
                    //或者是服务器上的杀毒软件造成的。跟管理运维的人沟通关闭服务器的杀毒或加入白名单即可。
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 是否创建日志文件成功
        /// </summary>
        /// <param name="strLog"></param>
        /// <param name="sFilePath"></param>
        /// <param name="sFileName"></param>
        /// <returns></returns>
        private static bool IsCreatedLogFile(string strLog, string sFilePath, string sFileName)
        {
            if (!string.IsNullOrEmpty(sFilePath) && !Directory.Exists(sFilePath)) //验证路径是否存在
            {
                //不存在则创建
                Directory.CreateDirectory(sFilePath);
            }
            if (!string.IsNullOrEmpty(sFileName))
            {
                //FileStream fs = new FileStream(sFileName,FileMode.OpenOrCreate,FileAccess.ReadWrite);//这样写没用，因为没有文件只是打开，每次都会被清空掉。
                FileStream fs;
                if (File.Exists(sFileName))
                {
                    //验证文件是否存在，有则追加，无则创建
                    fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
                    //FileMode.Append只能与FileAccess.Write一起使用不然报错。
                }
                else
                {
                    fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
                }

                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   ---   " + strLog);
                sw.Flush();  //清空缓存,Flush方法可以及时将缓冲区的内容写入文件，以刷新文件的内容。这样，共享此文件的其它使用者，不用一直等到你的程序结束，就能看到最新的内容。
                sw.Close();
                fs.Close();
                return true;
            }
            return false;
        }

        public static void WriteStream(string path, string strLog)
        {
            if (path == null) throw new ArgumentNullException(paramName: nameof(path));
            try
            {
                lock (logHelperLock)
                {
                    using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   ---   " + strLog);
                            sw.Flush();// 清空缓存,Flush方法可以及时将缓冲区的内容写入文件，以刷新文件的内容。
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 读取指定路径的文本信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadToEnd(string path)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            result = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        ///     同步工时记录
        /// </summary>
        private void SyncWorkHour()
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            parm.Add("status", 0); //在线记录。

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetOnlineAndOfflineDepts", Program.client.UserToken, JsonConvert.SerializeObject(parm));//未能找到文件“C:\\WINDOWS\\system32\\Config.json”。":"C:\\WINDOWS\\system32\\Config.json,因为这个API传输需要这个Config.json文件信息。

            if (ret != null)
            {
                var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);
                if (Convert.ToBoolean(retJson["IsSuccess"]))
                {
                    string json = retJson["RetData"].ToString();
                    DataTable dtDeptNos = JsonConvert.DeserializeObject<DataTable>(json);
                    if (dtDeptNos != null)
                    {
                        WriteLog($"当前在线部门人数是{dtDeptNos.Rows.Count}");
                        if (dtDeptNos.Rows.Count > 0)
                        {
                            int totalCount = 0;
                            foreach (DataRow row in dtDeptNos.Rows)
                            {
                                if (row != null)
                                {
                                    DataTable workTimeDt = GetWorkTimeSyncWorkHour(row["shiftCode"].ToString());

                                    if (workTimeDt != null && workTimeDt.Rows.Count == 0)
                                    {
                                        WriteLog($"这个工时代号是{row["shiftCode"]}没有工时设置记录");
                                    }

                                    if (workTimeDt != null && workTimeDt.Rows.Count > 0)
                                    {
                                        Dictionary<string, object> parm2 = new Dictionary<string, object>();
                                        parm2.Add("morningOnTime", workTimeDt.Rows[0]["am_from"].ToString());
                                        parm2.Add("morningOffTime", workTimeDt.Rows[0]["am_to"].ToString());
                                        parm2.Add("afternoonOnTime", workTimeDt.Rows[0]["pm_from"].ToString());
                                        parm2.Add("afternoonEndTime", workTimeDt.Rows[0]["pm_to"].ToString());
                                        parm2.Add("shiftCode", row["shiftCode"].ToString());
                                        parm2.Add("status", 0);
                                        parm2.Add("isAutoUpate", true);
                                        parm2.Add("deptNo", row["deptno"].ToString());
                                        parm2.Add("startTime", row["startTime"].ToString());
                                        parm2.Add("empNo", row["empNo"].ToString());
                                        parm2.Add("onlinetime", row["onlinetime"].ToString());

                                        WriteLog($"工时代号是{row["shiftCode"]}");

                                        string ret2 = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "OverTimeCount", Program.client.UserToken, JsonConvert.SerializeObject(parm2));
                                        var retJson2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret2);
                                        if (Convert.ToBoolean(retJson2["IsSuccess"]))
                                        {
                                            string count = retJson2["RetData"].ToString();
                                            totalCount += int.Parse(count);
                                        }
                                        else
                                        {
                                            WriteLog($"出错信息是定时服务超时统计OverTimeCount方法：{retJson2["ErrMsg"]}");
                                            return;
                                        }
                                    }
                                }
                            }

                            if (totalCount > 0)
                            {
                                WriteLog($"同步成功，一共{totalCount}记录");
                            }
                        }
                        else
                        {
                            WriteLog("没有需要同步的数据");
                        }
                    }
                }
                else
                {
                    WriteLog($"出错信息是定时服务同步工时记录GetOnlineAndOfflineDepts方法：{retJson["ErrMsg"]}");
                }
            }
        }

        /// <summary>
        ///     获取定时服务获取工时
        /// </summary>
        private DataTable GetWorkTimeSyncWorkHour(string shift_code)
        {
            Dictionary<string, object> parm = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(shift_code))
            {
                parm.Add("shift_code", shift_code);
            }

            string ret = WebAPIHelper.Post(Program.client.APIURL, "KZ_SFCAPI_WorkOrder", "KZ_SFCAPI_WorkOrder.Controllers.GeneralServer", "GetWorkTimeSyncWorkHour", Program.client.UserToken, JsonConvert.SerializeObject(parm));
            var retJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(ret);

            if (Convert.ToBoolean(retJson["IsSuccess"]))
            {
                string json = retJson["RetData"].ToString();
                DataTable dtJson = JsonConvert.DeserializeObject<DataTable>(json);
                return dtJson;
            }
            else
            {
                WriteLog($"出错信息是定时服务获取工时{nameof(GetWorkTimeSyncWorkHour)}方法：{retJson["ErrMsg"]}");
                return null;
            }
        }

        //结束关闭定时器
        protected override void OnStop()
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                WriteLog($"{DateTime.Now},同步开始！");
                SyncWorkHour();
                WriteLog("");//换行打印
            }
            catch (Exception ex)
            {
                WriteLog("错误信息是同步工时记录定时服务：" + ex.Message);
            }

            //如果第一次运行之后，则重置为这个点数每24小时运行一次  
            if (!isConfigCustomTime)
            {
                if (timer1.Interval != 24 * 60 * 60 * 1000)
                {
                    timer1.Interval = 24 * 60 * 60 * 1000;
                }
            }
        }
    }
}