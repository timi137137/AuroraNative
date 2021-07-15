using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群成员增加事件参数的类
    /// </summary>
    public sealed class GroupBanSpeakArgs : GroupNoticeArgs
    {
        #region --属性--

        /// <summary>
        /// 禁言时长，单位秒
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public long Duration { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="SubType">事件子类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="OperatorID">操作者 QQ 号</param>
        /// <param name="UserID">被禁言 QQ 号</param>
        /// <param name="Duration">禁言时长,单位秒</param>
        public GroupBanSpeakArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, string SubType, long GroupID, long OperatorID, long UserID, long Duration) : base(TimeStamp, SelfID, PostType, NoticeType, SubType, GroupID, OperatorID, UserID)
        {
            this.Duration = Duration;
        }

        #endregion
    }
}
