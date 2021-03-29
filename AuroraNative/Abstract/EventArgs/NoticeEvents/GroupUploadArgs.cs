using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中群文件上传事件参数的类
    /// </summary>
    public sealed class GroupUploadArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 群号
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public long GroupID { get; private set; }

        /// <summary>
        /// 匿名消息
        /// </summary>
        [JsonProperty(PropertyName = "file")]
        public File File { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="TimeStamp">事件发生的时间戳</param>
        /// <param name="SelfID">收到事件的机器人QQ号</param>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">发送者QQ号</param>
        /// <param name="File">文件信息</param>
        public GroupUploadArgs(long TimeStamp, long SelfID, string PostType, string NoticeType, long GroupID, long UserID, File File) : base(TimeStamp, SelfID, PostType, NoticeType, UserID)
        {
            this.GroupID = GroupID;
            this.File = File;
        }

        #endregion
    }
}
