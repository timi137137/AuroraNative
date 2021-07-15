using Newtonsoft.Json;

namespace AuroraNative.Type
{
    /// <summary>
    /// 机型 抽象类
    /// </summary>
    public sealed class Model
    {
        #region --属性--

        /// <summary>
        /// 机型名称
        /// </summary>
        [JsonProperty(PropertyName = "model_show")]
        public string Name;

        /// <summary>
        /// 是否付费
        /// </summary>
        [JsonProperty(PropertyName = "need_pay")]
        public bool IsPay;

        #endregion
    }
}
