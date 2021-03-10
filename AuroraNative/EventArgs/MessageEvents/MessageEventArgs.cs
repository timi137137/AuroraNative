using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述消息事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class MessageEventArgs : Base
    {
        #region --属性--

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty(PropertyName = "message_type")]
        public string MessageType { get; private set; }

        /// <summary>
        /// 消息子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public int MessageID { get; private set; }

        /// <summary>
        /// 发送者QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID { get; private set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; private set; }

        /// <summary>
        /// 原始消息内容
        /// </summary>
        [JsonProperty(PropertyName = "raw_message")]
        public string RawMessage { get; private set; }

        /// <summary>
        /// 字体
        /// </summary>
        [JsonProperty(PropertyName = "font")]
        public int Font { get; private set; }

        /// <summary>
        /// 发送者信息
        /// </summary>
        [JsonProperty(PropertyName = "sender")]
        public Sender Sender { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Base"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="MessageType">消息类型</param>
        /// <param name="SubType">消息子类型</param>
        /// <param name="MessageID">消息ID</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="Message">消息内容</param>
        /// <param name="RawMessage">原始消息内容</param>
        /// <param name="Font">字体</param>
        /// <param name="Sender">发送者信息</param>
        public MessageEventArgs(long TimeStamp, long SelfID, string PostType, string MessageType, string SubType, int MessageID, long UserID, string Message, string RawMessage, int Font, Sender Sender) : base(TimeStamp, SelfID, PostType)
        {
            this.MessageType = MessageType;
            this.SubType = SubType;
            this.MessageID = MessageID;
            this.UserID = UserID;
            this.Message = Message;
            this.RawMessage = RawMessage;
            this.Font = Font;
            this.Sender = Sender;
        }

        #endregion
    }
}
