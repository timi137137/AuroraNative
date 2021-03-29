using Newtonsoft.Json;

namespace AuroraNative.Type.Groups.SystemMessages
{
    /// <summary>
    /// 群系统消息 - 进群消息列表 抽象类
    /// </summary>
    public sealed class JoinRequest : BaseRequest
    {
        #region --属性--

        /// <summary>
        /// 请求者
        /// </summary>
        [JsonProperty(PropertyName = "requester_uin")]
        public long RequesterUserID;

        /// <summary>
        /// 请求者昵称
        /// </summary>
        [JsonProperty(PropertyName = "requester_nick")]
        public string RequesterNickName;

        /// <summary>
        /// 验证信息
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public long Message;

        #endregion
    }
}
