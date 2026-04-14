using System;
using log4net;

namespace ProcessAllocationService
{
    class LogHelper
    {
        //这里的 loginfo 和 log4net.config 里的名字要一样
        private static readonly ILog loginfo = LogManager.GetLogger("loginfo");

        //这里的 logerror 和 log4net.config 里的名字要一样
        private static readonly ILog logerror = LogManager.GetLogger("logerror");

        public static void Info(string message)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(message);
            }
        }

        public static void Error(string message, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(message, ex);
            }
        }

        public static void Error(string message)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(message);
            }
        }
    }
}