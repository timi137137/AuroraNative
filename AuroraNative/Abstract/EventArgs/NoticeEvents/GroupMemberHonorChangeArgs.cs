using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群成员荣誉变更提示事件参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class GroupMemberHonorChangeArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 提示类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 荣誉类型
        /// </summary>
        [JsonProperty(PropertyName = "honor_type")]
        public string HonorType { get; private set; }

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="SubType">提示类型</param>
        /// <param name="UserID">成员id</param>
        /// <param name="HonorType">荣誉类型</param>
        public GroupMemberHonorChangeArgs(string PostType, string NoticeType, long GroupID, string SubType, long UserID, string HonorType) : base(0, 0, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.HonorType = HonorType;
            this.SubType = SubType;
        }

        #endregion
    }
}
