using Newtonsoft.Json;

namespace AuroraNative
{
    /// <summary>
    /// 群成员信息 抽象类
    /// </summary>
    public sealed class GroupMember
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id", NullValueHandling = NullValueHandling.Include)]
        public long GroupID;

        /// <summary>
        /// QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id", NullValueHandling = NullValueHandling.Include)]
        public long UserID;

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname", NullValueHandling = NullValueHandling.Include)]
        public string NickName;

        /// <summary>
        /// 群名片
        /// </summary>
        [JsonProperty(PropertyName = "card", NullValueHandling = NullValueHandling.Include)]
        public string Card;

        /// <summary>
        /// 现在人数
        /// </summary>
        [JsonProperty(PropertyName = "sex", NullValueHandling = NullValueHandling.Include)]
        public string Sex;

        /// <summary>
        /// 最大人数
        /// </summary>
        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Include)]
        public int MaxMemberCount;

        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty(PropertyName = "area", NullValueHandling = NullValueHandling.Include)]
        public string Area;

        /// <summary>
        /// 加群时间戳
        /// </summary>
        [JsonProperty(PropertyName = "join_time", NullValueHandling = NullValueHandling.Include)]
        public int JoinTime;

        /// <summary>
        /// 最后发言时间戳
        /// </summary>
        [JsonProperty(PropertyName = "last_sent_time", NullValueHandling = NullValueHandling.Include)]
        public int LastSentTime;

        /// <summary>
        /// 成员等级
        /// </summary>
        [JsonProperty(PropertyName = "level", NullValueHandling = NullValueHandling.Include)]
        public int Level;

        /// <summary>
        /// 角色
        /// </summary>
        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Include)]
        public int Role;

        /// <summary>
        /// 是否不良记录成员
        /// </summary>
        [JsonProperty(PropertyName = "unfriendly", NullValueHandling = NullValueHandling.Include)]
        public bool UnFriendly;

        /// <summary>
        /// 专属头衔
        /// </summary>
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Include)]
        public string ExclusiveTitle;

        /// <summary>
        /// 专属头衔过期时间戳
        /// </summary>
        [JsonProperty(PropertyName = "title_expire_time", NullValueHandling = NullValueHandling.Include)]
        public long ExclusiveTitleTime;

        /// <summary>
        /// 是否允许修改群名片
        /// </summary>
        [JsonProperty(PropertyName = "card_changeable", NullValueHandling = NullValueHandling.Include)]
        public bool IsChangeCard;

        #endregion
    }
}
