using AuroraNative.Type.Users;
using Newtonsoft.Json;

namespace AuroraNative.Type
{
    /// <summary>
    /// 消息 抽象类
    /// </summary>
    public sealed class Messages
    {
        #region --属性--

        /// <summary>
        /// 匿名信息
        /// </summary>
        [JsonProperty(PropertyName = "anonymous")]
        public Anonymous Anonymous;

        /// <summary>
        /// 字体
        /// </summary>
        [JsonProperty(PropertyName = "font")]
        public int Font;

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID;

        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message;

        /// <summary>
        /// 原始消息内容
        /// </summary>
        [JsonProperty(PropertyName = "raw_message")]
        public string RawMessage;

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public int MessageID;

        /// <summary>
        /// 消息序号
        /// </summary>
        [JsonProperty(PropertyName = "message_seq")]
        public long MessageSeq;

        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty(PropertyName = "message_type")]
        public string MessageType;

        /// <summary>
        /// 消息事件主类型
        /// </summary>
        [JsonProperty(PropertyName = "post_type")]
        public string PostType;

        /// <summary>
        /// 消息事件子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType;

        /// <summary>
        /// 收到事件的机器人QQ号
        /// </summary>
        [JsonProperty(PropertyName = "self_id")]
        public int SelfID;

        /// <summary>
        /// 发送者
        /// </summary>
        [JsonProperty(PropertyName = "sender")]
        public Sender Sender;

        /// <summary>
        /// 消息时间戳
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public long TimeStamp;

        /// <summary>
        /// 发送者QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID;

        #endregion
    }
}
