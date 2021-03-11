using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群成员增加事件参数的类
    /// </summary>
    public sealed class EssenceMessageChangeArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 消息发送者ID
        /// </summary>
        [JsonProperty(PropertyName = "sender_id")]
        public new long UserID { get; private set; }

        /// <summary>
        /// 操作者ID
        /// </summary>
        [JsonProperty(PropertyName = "operator_id")]
        public long OperatorID { get; private set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public int MessageID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">子类型</param>
        /// <param name="UserID">消息发送者ID</param>
        /// <param name="OperatorID">操作者ID</param>
        /// <param name="MessageID">消息ID</param>
        public EssenceMessageChangeArgs(string PostType, string NoticeType, string SubType, long UserID, long OperatorID, int MessageID) : base(0, 0, PostType, NoticeType, UserID)
        {
            this.SubType = SubType;
            this.OperatorID = OperatorID;
            this.UserID = UserID;
            this.MessageID = MessageID;
        }

        #endregion
    }
}
