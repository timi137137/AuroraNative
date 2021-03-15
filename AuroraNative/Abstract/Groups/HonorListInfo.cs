using Newtonsoft.Json;

namespace AuroraNative
{
    /// <summary>
    /// 群荣誉详细信息 抽象类
    /// </summary>
    public sealed class HonorListInfo
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
        /// 头像URL
        /// </summary>
        [JsonProperty(PropertyName = "avatar")]
        public string Avater;

        /// <summary>
        /// 荣誉描述
        /// </summary>
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description;

        /// <summary>
        /// 持续天数
        /// </summary>
        [JsonProperty(PropertyName = "day_count", NullValueHandling = NullValueHandling.Ignore)]
        public string DayCount;

        #endregion
    }
}
