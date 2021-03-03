using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中戳一戳事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class PokeEventArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 提示类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 被戳者 QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "target_id")]
        public long TargetID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">提示类型</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="TargetID">被戳者 QQ 号</param>
        public PokeEventArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long UserID, long TargetID) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.TargetID = TargetID;
            this.SubType = SubType;
        }

        #endregion
    }
}
