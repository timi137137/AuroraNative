using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class NoticeEventArgs : Base
    {
        #region --属性--

        /// <summary>
        /// 通知类型
        /// </summary>
        [JsonProperty(PropertyName = "notice_type")]
        public string NoticeType { get; private set; }

        /// <summary>
        /// 发送者QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Base"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">	通知类型</param>
        /// <param name="UserID">发送者QQ号</param>
        public NoticeEventArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, long UserID) : base(TimeStamp, SelfID, PostType)
        {
            this.NoticeType = NoticeType;
            this.UserID = UserID;
        }

        #endregion
    }
}
