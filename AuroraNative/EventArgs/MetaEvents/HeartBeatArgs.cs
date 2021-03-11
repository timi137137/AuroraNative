using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述元事件中心跳事件参数的类
    /// </summary>
    public sealed class HeartBeatArgs : MetaEventArgs
    {
        #region --属性--

        /// <summary>
        /// 状态信息
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public object Status { get; private set; }

        /// <summary>
        /// 到下次心跳的间隔，单位毫秒
        /// </summary>
        [JsonProperty(PropertyName = "interval")]
        public long Interval { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="MetaEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="MetaEventType">元事件类型</param>
        /// <param name="Status">状态信息</param>
        /// <param name="Interval">到下次心跳的间隔，单位毫秒</param>
        public HeartBeatArgs(long TimeStamp, long SelfID, string PostType, string MetaEventType, object Status, long Interval) : base(TimeStamp, SelfID, PostType, MetaEventType)
        {
            this.Status = Status;
            this.Interval = Interval;
        }

        #endregion
    }
}
