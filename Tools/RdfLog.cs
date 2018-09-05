using System;
using System.Text;

namespace Tools
{
    /// <summary>
    /// 日志
    /// </summary>
    public class RdfLog
    {
        private static object logObject = new object();
        private static object errorObject = new object();
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        public static void WriteLog(string title, string className = "", string methodName = "")
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            sb.AppendLine("类名:" + className);
            if (!string.IsNullOrWhiteSpace(className))
            {
                sb.AppendLine("类名:" + className);
            }
            if (!string.IsNullOrWhiteSpace(className))
            {
                sb.AppendLine("方法:" + methodName);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                sb.AppendLine("标题:" + title);
            }
            sb.AppendLine("----------------------------------------------------------------------------");
            lock (logObject)
            {
                RdfFile.WriteAppend(fileName, RdfFile.AppDir + "Log", sb.ToString());
            }
        }
        /// <summary>
        /// 写入异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="title">标题</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法名</param>
        public static void WriteException(Exception ex, string title = "", string className = "", string methodName = "")
        {
            if (ex.InnerException == null)
            {
                string fileName = DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                if (!string.IsNullOrWhiteSpace(className))
                {
                    sb.AppendLine("类名:" + className);
                }
                if (!string.IsNullOrWhiteSpace(methodName))
                {
                    sb.AppendLine("方法:" + methodName);
                }
                if (!string.IsNullOrWhiteSpace(title))
                {
                    sb.AppendLine("标题:" + title);
                }

                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine("----------------------------------------------------------------------------");
                lock (errorObject)
                {
                    RdfFile.WriteAppend(fileName, RdfFile.AppDir + "Error", sb.ToString());
                }
            }
            else
                WriteException(ex.InnerException, title, className, methodName);
        }
    }
}
