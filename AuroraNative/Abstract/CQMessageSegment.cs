using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuroraNative.Type
{
    /// <summary>
    /// CQ消息段 类
    /// </summary>
    public sealed class CQMessageSegment
    {
        #region --属性--

        /// <summary>
        /// CQ 码功能名
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// CQ码参数
        /// </summary>
        [JsonProperty(PropertyName = "data",NullValueHandling = NullValueHandling.Include)]
        [JsonExtensionData]
        public Dictionary<string, string> Data { get; set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 创建一个新的<see cref="CQMessageSegment"/>变量
        /// </summary>
        /// <param name="Type">CQ码功能名</param>
        /// <param name="Params">CQ码参数</param>
        public CQMessageSegment(string Type,Dictionary<string,string> Params = null) {
            this.Type = Type;
            Data = Params;
        }

        #endregion
    }
}
