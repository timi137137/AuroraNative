using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述元事件中生命周期事件参数的类
    /// </summary>
    public sealed class LifeCycleArgs : MetaEventArgs
    {
        #region --属性--

        /// <summary>
        /// 事件子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="MetaEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="MetaEventType">元事件类型</param>
        /// <param name="SubType">事件子类型</param>
        public LifeCycleArgs(long TimeStamp, long SelfID, string PostType, string MetaEventType, string SubType) : base(TimeStamp, SelfID, PostType, MetaEventType)
        {
            this.SubType = SubType;
        }

        #endregion
    }
}
