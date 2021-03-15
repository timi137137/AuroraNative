using Newtonsoft.Json;

namespace AuroraNative
{
    /// <summary>
    /// 精华消息 抽象类
    /// </summary>
    public sealed class Essences
    {
        #region --属性--

        /// <summary>
        /// 发送者QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "sender_id")]
        public long SenderID { get; private set; }

        /// <summary>
        /// 发送者昵称
        /// </summary>
        [JsonProperty(PropertyName = "sender_nick")]
        public string SenderNickName { get; private set; }

        /// <summary>
        /// 消息发送时间
        /// </summary>
        [JsonProperty(PropertyName = "sender_time")]
        public string SenderTime { get; private set; }

        /// <summary>
        /// 操作者QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "operator_id")]
        public string OperatorID { get; private set; }

        /// <summary>
        /// 操作者昵称
        /// </summary>
        [JsonProperty(PropertyName = "operator_nick")]
        public string OperatorNickName { get; private set; }

        /// <summary>
        /// 精华设置时间
        /// </summary>
        [JsonProperty(PropertyName = "operator_time")]
        public string OperatorTime { get; private set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public string MessageID { get; private set; }

        #endregion
    }
}
