using Newtonsoft.Json;

namespace AuroraNavite.EventArgs {
    /// <summary>
    /// 提供用于描述通知事件中群通知事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class GroupNoticeArgs : NoticeEventArgs
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

        /// <summary>
        /// 	操作者 QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "operator_id")]
        public long OperatorID { get; private set; }

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
        /// <param name="OperatorID">操作者 QQ 号</param>
        /// <param name="UserID">被操作者 QQ 号</param>
        public GroupNoticeArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long GroupID, long OperatorID, long UserID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.SubType = SubType;
            this.OperatorID = OperatorID;
        }

        #endregion
    }
}
