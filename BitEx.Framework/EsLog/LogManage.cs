using System;
using NLog.Config;

namespace BitEx.Framework.EsLog
{
    public class LogManage
    {
        static string appName;
        static LogManage()
        {
            NLog.LogManager.Configuration = new XmlLoggingConfiguration(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\NLog.config");
        }
        public static void Init(string source, LogLevel lv)
        {
            appName = source;
            level = lv;
        }
        static string AppName
        {
            get
            {
                return appName;
            }
        }
        static LogLevel level = LogLevel.Info;
        static LogLevel Level
        {
            get
            {
                return level;
            }
        }
        public static string Exchange
        {
            get
            {
                return "SystemLog";
            }
        }
        public static ILogger GetLogger(string source, string appName = null)
        {
            if (string.IsNullOrEmpty(appName))
            {
                appName = AppName;
            }
            return new MQLogger(Exchange, appName, source, Level);
        }
        public static ILogger GetLogger(Type source, string appName = null)
        {
            if (string.IsNullOrEmpty(appName))
            {
                appName = AppName;
            }
            return new MQLogger(Exchange, appName, source.FullName, Level);
        }
    }
}
