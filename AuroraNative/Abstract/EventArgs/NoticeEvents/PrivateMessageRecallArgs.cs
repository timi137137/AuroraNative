using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中好友消息撤回参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class PrivateMessageRecallArgs : NoticeEventArgs
    {
        #region --属性--

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
        /// <param name="UserID">好友 QQ 号</param>
        /// <param name="MessageID">被撤回的消息 ID</param>
        public PrivateMessageRecallArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, long UserID, long MessageID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.MessageID = MessageID;
        }

        #endregion
    }
}
