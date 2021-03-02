using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群管理员变动事件参数的类
    /// </summary>
    public sealed class GroupManageChangeArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        /// <summary>
        /// 事件子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">事件子类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">管理员 QQ 号</param>
        public GroupManageChangeArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long GroupID, long UserID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.SubType = SubType;
        }

        #endregion
    }
}
