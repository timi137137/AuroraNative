namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中好友戳一戳事件参数的类
    /// </summary>
    public sealed class PrivatePokeArgs : PokeEventArgs
    {
        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="PokeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">时间戳</param>
        /// <param name="SelfID">机器人 QQ 号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">提示类型</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="TargetID">被戳者 QQ 号</param>
        public PrivatePokeArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long UserID, long TargetID) : base(TimeStamp, SelfID, PostType, NoticeType, SubType, UserID, TargetID) { }

        #endregion
    }
}
