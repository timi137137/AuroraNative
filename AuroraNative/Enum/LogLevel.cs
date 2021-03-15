namespace AuroraNative
{
    /// <summary>
    /// 表示日志信息等级的枚举
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
		/// 表示输出日志的等级是 "调试" 级别
		/// </summary>
		Debug = 0,
        /// <summary>
        /// 表示输出日志的等级是 "信息" 级别
        /// </summary>
        Info = 1,
        /// <summary>
        /// 表示输出日志的等级是 "警告" 级别
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 表示输出日志的等级是 "错误" 级别
        /// </summary>
        Error = 3,
        /// <summary>
        /// 表示不输出日志
        /// </summary>
        Off = 4
    }
}
