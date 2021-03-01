using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述元事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class MetaEventArgs : Base
    {
        #region --属性--

        /// <summary>
        /// 元事件类型
        /// </summary>
        [JsonProperty(PropertyName = "meta_event_type")]
        public string MetaEventType { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Base"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="MetaEventType">元事件类型</param>
        public MetaEventArgs(long TimeStamp, long SelfID, string PostType, string MetaEventType) : base(TimeStamp, SelfID, PostType)
        {
            this.MetaEventType = MetaEventType;
        }

        #endregion
    }
}
