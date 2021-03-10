using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AuroraNative
{
    /// <summary>
    /// API基础 抽象类
    /// </summary>
    public sealed class BaseAPI
    {
        #region --属性--

        /// <summary>
        /// API节点
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public string Action { get; private set; }

        /// <summary>
        /// 参数
        /// </summary>
        [JsonProperty(PropertyName = "params", NullValueHandling = NullValueHandling.Ignore)]
        public JObject Params { get; private set; }

        /// <summary>
        /// 唯一识别码
        /// </summary>
        [JsonProperty(PropertyName = "echo")]
        public string UniqueCode { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="BaseAPI"/> 类的新实例
        /// </summary>
        /// <param name="Action">API节点</param>
        /// <param name="Params">参数</param>
        /// <param name="UniqueCode">唯一识别码</param>
        public BaseAPI(string Action, JObject Params, string UniqueCode)
        {
            this.Action = Action;
            this.Params = Params;
            this.UniqueCode = UniqueCode;
        }

        #endregion
    }
}
