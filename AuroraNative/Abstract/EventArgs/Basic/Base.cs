using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class Base
    {
        #region --属性--

        /// <summary>
        /// 事件发生的时间戳
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public long TimeStamp { get; private set; }

        /// <summary>
        /// 收到事件的机器人QQ号
        /// </summary>
        [JsonProperty(PropertyName = "self_id")]
        public long SelfID { get; private set; }

        /// <summary>
        /// 上报类型
        /// </summary>
        [JsonProperty(PropertyName = "post_type")]
        public string PostType { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Base"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发送的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        public Base(long TimeStamp, long SelfID, string PostType)
        {
            this.TimeStamp = TimeStamp;
            this.SelfID = SelfID;
            this.PostType = PostType;
        }

        #endregion
    }
}
