using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群成员名片更新（核验）事件参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class GroupCardVerifyArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 新名片
        /// </summary>
        [JsonProperty(PropertyName = "card_new")]
        public string CardNew { get; private set; }

        /// <summary>
        /// 旧名片
        /// </summary>
        [JsonProperty(PropertyName = "card_old")]
        public string CardOld { get; private set; }

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
        /// <param name="UserID">成员id</param>
        /// <param name="CardNew">新名片</param>
        /// <param name="CardOld">旧名片</param>
        public GroupCardVerifyArgs(string PostType, string NoticeType, long GroupID, long UserID, string CardNew, string CardOld) : base(0, 0, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.CardNew = CardNew;
            this.CardOld = CardOld;
        }

        #endregion
    }
}
