using Newtonsoft.Json;

namespace AuroraNative.Type.Files
{
    /// <summary>
    /// 群文件 抽象类
    /// </summary>
    public sealed class File
    {
        #region --属性--

        /// <summary>
        /// 文件ID
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public string FileID;

        /// <summary>
        /// 文件名称
        /// </summary>
        [JsonProperty(PropertyName = "file_name")]
        public string FileName;

        /// <summary>
        /// 文件类型
        /// </summary>
        [JsonProperty(PropertyName = "busid")]
        public long BusID;

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonProperty(PropertyName = "file_size")]
        public long FileSize;

        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonProperty(PropertyName = "upload_time")]
        public string UploadTime;

        /// <summary>
        /// 过期时间，永久为零
        /// </summary>
        [JsonProperty(PropertyName = "dead_time")]
        public int DeadTime;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty(PropertyName = "modify_time")]
        public int ModifyTime;

        /// <summary>
        /// 下载次数
        /// </summary>
        [JsonProperty(PropertyName = "download_times")]
        public int DownloadsCount;

        /// <summary>
        /// 上传者ID
        /// </summary>
        [JsonProperty(PropertyName = "uploader")]
        public int Uploader;

        /// <summary>
        /// 上传者名字
        /// </summary>
        [JsonProperty(PropertyName = "uploader_name")]
        public int UploaderName;

        #endregion
    }
}
