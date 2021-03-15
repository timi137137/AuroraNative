using Newtonsoft.Json;

namespace AuroraNative
{
    /// <summary>
    /// 群文件夹 抽象类
    /// </summary>
    public sealed class Folder
    {
        #region --属性--

        /// <summary>
        /// 文件夹ID
        /// </summary>
        [JsonProperty(PropertyName = "folder_id")]
        public string FolderID;

        /// <summary>
        /// 文件夹名称
        /// </summary>
        [JsonProperty(PropertyName = "folder_name")]
        public string FolderName;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "create_time")]
        public long CreateTime;

        /// <summary>
        /// 创建者
        /// </summary>
        [JsonProperty(PropertyName = "creator")]
        public long Creator;

        /// <summary>
        /// 创建者名称
        /// </summary>
        [JsonProperty(PropertyName = "creator_name")]
        public string CreatoName;

        /// <summary>
        /// 子文件数量
        /// </summary>
        [JsonProperty(PropertyName = "total_file_count")]
        public int TotalFilesCount;

        #endregion
    }
}
