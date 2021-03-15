using System;

namespace AuroraNative
{
    /// <summary>
    /// 彩色日志输出类
    /// </summary>
    public static class Logger
    {
        #region --属性--

        /// <summary>
        /// 日志级别设定<para>默认 Info</para>
        /// </summary>
        public static LogLevel LogLevel = LogLevel.Info;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 输出一个等级为 调试 的信息
        /// </summary>
        /// <param name="Message">要输出的信息</param>
        /// <param name="MethodName">输出的方法名，可选传入</param>
        public static void Debug(string Message, string MethodName = null)
        {
            if (LogLevel <= LogLevel.Debug)
            {
                Output(Message, ConsoleColor.Gray, LogLevel.Debug, MethodName, true);
            }
        }

        /// <summary>
        /// 输出一个等级为 信息 的信息
        /// </summary>
        /// <param name="Message">要输出的信息</param>
        /// <param name="MethodName">输出的方法名，可选传入</param>
        public static void Info(string Message, string MethodName = null)
        {
            if (LogLevel <= LogLevel.Info)
            {
                Output(Message, ConsoleColor.White, LogLevel.Info, MethodName);
            }
        }

        /// <summary>
        /// 输出一个等级为 警告 的信息
        /// </summary>
        /// <param name="Message">要输出的信息</param>
        /// <param name="MethodName">输出的方法名，可选传入</param>
        public static void Warning(string Message, string MethodName = null)
        {
            if (LogLevel <= LogLevel.Warning)
            {
                Output(Message, ConsoleColor.Yellow, LogLevel.Warning, MethodName);
            }
        }

        /// <summary>
        /// 输出一个等级为 错误 的信息
        /// </summary>
        /// <param name="Message">要输出的信息</param>
        /// <param name="MethodName">输出的方法名，可选传入</param>
        public static void Error(string Message, string MethodName = null)
        {
            if (LogLevel <= LogLevel.Error)
            {
                Output(Message, ConsoleColor.Red, LogLevel.Error, MethodName);
            }
        }

        #endregion

        #region --私有函数--

        internal static void Output(string Message, ConsoleColor Color, LogLevel Level, string MethodName, bool IsFine = false)
        {
            Console.ForegroundColor = Color;
            string NowTime;

            if (!IsFine)
            {
                NowTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            }
            else
            {
                NowTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
            }

            if (MethodName != null)
            {
                Console.WriteLine($"[{NowTime}]" + $" [{Level}]" + $" [{MethodName}]: " + Message);
            }
            else
            {
                Console.WriteLine($"[{NowTime}]" + $" [{Level}]: " + Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
