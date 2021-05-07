using Newtonsoft.Json;

namespace AuroraNative.Type
{
    /// <summary>
    /// 提供用于描述登录设备的基础类, 该类是抽象的
    /// </summary>
    public abstract class Device
    {
        #region --属性--

        /// <summary>
        /// 客户端ID
        /// </summary>
        [JsonProperty(PropertyName = "app_id")]
        public long AppID { get; private set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [JsonProperty(PropertyName = "device_name")]
        public string Name { get; private set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonProperty(PropertyName = "device_kind")]
        public string Type { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Device"/> 类的新实例
        /// </summary>
        /// <param name="AppID">客户端ID</param>
        /// <param name="Name">设备名称</param>
        /// <param name="Type">设备类型</param>
        public Device(long AppID, string Name, string Type)
        {
            this.AppID = AppID;
            this.Name = Name;
            this.Type = Type;
        }

        #endregion
    }
}
