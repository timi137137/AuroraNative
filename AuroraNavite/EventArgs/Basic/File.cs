using Newtonsoft.Json;

namespace AuroraNavite.EventArgs
{
    /// <summary>
    /// 提供用于描述匿名信息的基础类, 该类是抽象的
    /// </summary>
    public sealed class File
    {
        #region --属性--

        /// <summary>
        /// 文件 ID
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; private set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// 文件大小 ( 字节数 )
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; private set; }

        /// <summary>
        /// 下载链接
        /// </summary>
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; private set; }

        /// <summary>
        /// busid ( 目前不清楚有什么作用 )
        /// </summary>
        [JsonProperty(PropertyName = "busid", NullValueHandling = NullValueHandling.Ignore)]
        public long BusID { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="File"/> 类的新实例
        /// </summary>
        /// <param name="ID">文件 ID</param>
        /// <param name="Name">文件名</param>
        /// <param name="Size">文件大小 ( 字节数 )</param>
        /// <param name="Url">下载链接</param>
        /// <param name="BusID">busid ( 目前不清楚有什么作用 )</param>
        public File(string ID, string Name, long Size, string Url, long BusID)
        {
            this.ID = ID;
            this.Name = Name;
            this.Size = Size;
            this.BusID = BusID;
            this.Url = Url;
        }

        #endregion
    }
}
