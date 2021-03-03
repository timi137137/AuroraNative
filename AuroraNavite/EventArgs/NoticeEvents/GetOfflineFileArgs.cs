using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中接收到离线文件事件参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class GetOfflineFileArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 文件数据
        /// </summary>
        [JsonProperty(PropertyName = "file")]
        public File File { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="UserID">成员id</param>
        /// <param name="File">文件数据</param>
        public GetOfflineFileArgs(string PostType, string NoticeType, long UserID, File File) : base(0, 0, PostType, NoticeType, UserID)
        {
            this.File = File;
        }

        #endregion
    }
}
