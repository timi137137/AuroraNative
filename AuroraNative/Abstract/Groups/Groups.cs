using Newtonsoft.Json;

namespace AuroraNative.Type.Groups
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
        /// 群备注
        /// </summary>
        [JsonProperty(PropertyName = "group_memo")]
        public string GroupRemark;

        /// <summary>
        /// 群创建时间
        /// </summary>
        [JsonProperty(PropertyName = "group_create_time", NullValueHandling = NullValueHandling.Ignore)]
        public uint GroupCreateTime;

        /// <summary>
        /// 群等级
        /// </summary>
        [JsonProperty(PropertyName = "group_level", NullValueHandling = NullValueHandling.Ignore)]
        public uint GroupLevel;

        /// <summary>
        /// 现在人数
        /// </summary>
        [JsonProperty(PropertyName = "member_count", NullValueHandling = NullValueHandling.Ignore)]
        public int MemberCount;

        /// <summary>
        /// 最大人数
        /// </summary>
        [JsonProperty(PropertyName = "max_member_count", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxMemberCount;

        #endregion
    }
}
