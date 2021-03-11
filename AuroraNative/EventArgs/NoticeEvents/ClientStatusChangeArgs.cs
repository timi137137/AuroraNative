using Newtonsoft.Json;

namespace AuroraNative.EventArgs
{
    /// <summary>
    /// 提供用于描述通知事件中其他客户端在线状态变更事件参数的基础类, 该类是抽象的
    /// </summary>
    public sealed class ClientStatusChangeArgs : NoticeEventArgs
    {
        #region --属性--

        /// <summary>
        /// 客户端信息
        /// </summary>
        [JsonProperty(PropertyName = "client")]
        public Device Client { get; private set; }

        /// <summary>
        /// 当前是否在线
        /// </summary>
        [JsonProperty(PropertyName = "online")]
        public bool Online { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="NoticeEventArgs"/> 类的新实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        /// <param name="NoticeType">通知类型</param>
        /// <param name="Client">客户端信息</param>
        /// <param name="Online">当前是否在线</param>
        public ClientStatusChangeArgs(string PostType, string NoticeType, Device Client, bool Online) : base(0, 0, PostType, NoticeType, 0)
        {
            this.Client = Client;
            this.Online = Online;
        }

        #endregion
    }
}
