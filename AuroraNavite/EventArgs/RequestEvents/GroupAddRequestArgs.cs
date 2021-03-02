using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述消息事件中群请求事件参数的类
    /// </summary>
    public sealed class GroupAddRequestArgs : RequestEventArgs
    {
        #region --属性--

        /// <summary>
        /// 请求子类型
        /// </summary>
        [JsonProperty(PropertyName = "sub_type")]
        public string SubType { get; private set; }

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="RequestEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="RequestType">	请求类型</param>
        /// <param name="SubType">请求子类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="Comment">验证信息</param>
        /// <param name="Flag">请求 flag, 在调用处理请求的 API 时需要传入</param>
        public GroupAddRequestArgs(long TimeStamp, long SelfID, string PostType, string RequestType, string SubType, long GroupID, long UserID, string Comment, string Flag) : base(TimeStamp, SelfID, PostType, RequestType, UserID, Comment, Flag)
        {
            this.SubType = SubType;
            this.GroupID = GroupID;
        }

        #endregion
    }
}
