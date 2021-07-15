using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群消息撤回参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class GroupMessageRecallArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        /// <summary>
        /// 操作者 QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "operator_id")]
        public long OperatorID { get; private set; }

        /// <summary>
        /// 被撤回的消息 ID
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public long MessageID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="OperatorID">操作者 QQ 号</param>
        /// <param name="UserID">消息发送者 QQ 号</param>
        /// <param name="MessageID">被撤回的消息 ID</param>
        public GroupMessageRecallArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, long GroupID, long UserID, long OperatorID, long MessageID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.OperatorID = OperatorID;
            this.MessageID = MessageID;
        }

        #endregion
    }
}
