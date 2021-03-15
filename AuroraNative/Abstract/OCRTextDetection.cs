using Newtonsoft.Json;
using System.Numerics;

namespace AuroraNative
{
    /// <summary>
    /// OCR结果信息 抽象类
    /// </summary>
    public sealed class OCRTextDetection
    {
        #region --属性--

        /// <summary>
        /// 文本
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text;

        /// <summary>
        /// 置信度
        /// </summary>
        [JsonProperty(PropertyName = "confidence")]
        public int Confidence;

        /// <summary>
        /// 坐标
        /// </summary>
        [JsonProperty(PropertyName = "coordinates")]
        public Vector2 Coordinates;

        #endregion
    }
}
