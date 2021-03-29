using Newtonsoft.Json;

namespace AuroraNative.Type.Groups.SystemMessages
{
    /// <summary>
    /// 群系统消息 - 邀请消息列表 抽象类
    /// </summary>
    public sealed class InvitedRequest : BaseRequest
    {
        #region --属性--

        /// <summary>
        /// 邀请者
        /// </summary>
        [JsonProperty(PropertyName = "invitor_uin")]
        public long InvitorUserID;

        /// <summary>
        /// 邀请者昵称
        /// </summary>
        [JsonProperty(PropertyName = "invitor_nick")]
        public string InvitorNickName;

        #endregion
    }
}
