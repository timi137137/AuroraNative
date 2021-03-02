using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述请求事件参数的基础类, 该类是抽象的
    /// </summary>
    public abstract class RequestEventArgs : Base
    {
        #region --属性--

        /// <summary>
        /// 请求类型
        /// </summary>
        [JsonProperty(PropertyName = "request_type")]
        public string RequestType { get; private set; }

        /// <summary>
        /// 发送者QQ号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID { get; private set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; private set; }

        /// <summary>
        /// 请求 flag ,在调用处理请求的 API 时需要传入
        /// </summary>
        [JsonProperty(PropertyName = "flag")]
        public string Flag { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Base"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="RequestType">	请求类型</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="Comment">验证信息</param>
        /// <param name="Flag">请求 flag, 在调用处理请求的 API 时需要传入</param>
        public RequestEventArgs(long TimeStamp, long SelfID, string PostType, string RequestType, long UserID, string Comment, string Flag) : base(TimeStamp, SelfID, PostType)
        {
            this.RequestType = RequestType;
            this.UserID = UserID;
            this.Comment = Comment;
            this.Flag = Flag;
        }

        #endregion
    }
}
