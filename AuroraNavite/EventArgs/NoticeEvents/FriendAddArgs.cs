namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中添加好友通知事件参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class FriendAddArgs : NoticeEventArgs
    {
        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="UserID">新添加好友 QQ 号</param>
        public FriendAddArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, long UserID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID) { }

        #endregion
    }
}
