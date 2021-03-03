using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群红包运气王提示事件参数的类
    /// </summary>
    public sealed class GroupRedPoketLuckyKingArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        /// <summary>
        /// 提示类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 运气王 QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "target_id")]
        public long TargetID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="SubType">提示类型</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="TargetID">运气王 QQ 号</param>
        public GroupRedPoketLuckyKingArgs(string PostType, string NoticeType, long GroupID, string SubType, long UserID, long TargetID) : base(0, 0, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.SubType = SubType;
            this.TargetID = TargetID;
        }

        #endregion
    }
}
