using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群内戳一戳事件参数的类
    /// </summary>
    public sealed class GroupPokeArgs : PokeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="PokeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">提示类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="TargetID">被戳者 QQ 号</param>
        public GroupPokeArgs(string PostType, string NoticeType, string SubType, long GroupID, long UserID, long TargetID) : base(0, 0, PostType, NoticeType, SubType, UserID, TargetID)
        {
            this.GroupID = GroupID;
        }

        #endregion
    }
}
