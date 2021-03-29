using Newtonsoft.Json;

namespace AuroraNative.Type.Groups.SystemMessages
{
    /// <summary>
    /// 群系统消息 - 消息列表 基抽象类
    /// </summary>
    public abstract class BaseRequest
    {
        #region --属性--

        /// <summary>
        /// 请求ID
        /// </summary>
        [JsonProperty(PropertyName = "request_id")]
        public long RequestID;

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID;

        /// <summary>
        /// 群名称
        /// </summary>
        [JsonProperty(PropertyName = "group_name")]
        public string GroupName;

        /// <summary>
        /// 是否已处理
        /// </summary>
        [JsonProperty(PropertyName = "checked")]
        public bool Checked;

        /// <summary>
        /// 处理者,未处理是0
        /// </summary>
        [JsonProperty(PropertyName = "actor")]
        public long Actor;

        #endregion
    }
}
