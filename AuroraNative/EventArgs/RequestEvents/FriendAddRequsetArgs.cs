namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述消息事件中好友请求事件参数的类
    /// </summary>
    public sealed class FriendAddRequsetArgs : RequestEventArgs
    {
        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="RequestEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="RequestType">	请求类型</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="Comment">验证信息</param>
        /// <param name="Flag">请求 flag, 在调用处理请求的 API 时需要传入</param>
        public FriendAddRequsetArgs(long TimeStamp, long SelfID, string PostType, string RequestType, long UserID, string Comment, string Flag) : base(TimeStamp, SelfID, PostType, RequestType, UserID, Comment, Flag) { }

        #endregion
    }
}
