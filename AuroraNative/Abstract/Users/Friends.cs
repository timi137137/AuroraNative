using Newtonsoft.Json;

namespace AuroraNative.Type.Users
{
    /// <summary>
    /// 好友 抽象类
    /// </summary>
    public sealed class Friends
    {
        #region --属性--

        /// <summary>
        /// QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID;

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string NickName;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark;

        #endregion
    }
}
