using Newtonsoft.Json;

namespace AuroraNative
{
    /// <summary>
    /// 群组 抽象类
    /// </summary>
    public sealed class Groups
    {
        #region --属性--

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
        /// 现在人数
        /// </summary>
        [JsonProperty(PropertyName = "member_count")]
        public int MemberCount;

        /// <summary>
        /// 最大人数
        /// </summary>
        [JsonProperty(PropertyName = "max_member_count")]
        public int MaxMemberCount;

        #endregion
    }
}
