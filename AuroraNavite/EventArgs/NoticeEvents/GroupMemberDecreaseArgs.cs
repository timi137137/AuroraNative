namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群成员减少事件参数的类
    /// </summary>
    public sealed class GroupMemberDecreaseArgs : GroupNoticeArgs
    {
        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="GroupNoticeArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">事件子类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="OperatorID">操作者 QQ 号（如果是主动退群，则和 user_id 相同）</param>
        /// <param name="UserID">离开者 QQ 号</param>
        public GroupMemberDecreaseArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long GroupID, long OperatorID, long UserID) : base(TimeStamp, SelfID, PostType, NoticeType, SubType, GroupID, OperatorID, UserID) { }

        #endregion
    }
}
