using Newtonsoft.Json;

namespace AuroraNative.Type.Users
{
    /// <summary>
    /// 提供用于描述匿名信息的基础类, 该类是抽象的
    /// </summary>
    public sealed class Anonymous
    {
        #region --属性--

        /// <summary>
        /// 匿名用户 ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long ID { get; private set; }

        /// <summary>
        /// 匿名用户名称
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// 匿名用户 flag, 在调用禁言 API 时需要传入
        /// </summary>
        [JsonProperty(PropertyName = "flag")]
        public string Flag { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Anonymous"/> 类的新实例
        /// </summary>
        /// <param name="ID">匿名用户 ID</param>
        /// <param name="Name">匿名用户名称</param>
        /// <param name="Flag">匿名用户 flag, 在调用禁言 API 时需要传入</param>
        public Anonymous(long ID, string Name, string Flag)
        {
            this.ID = ID;
            this.Name = Name;
            this.Flag = Flag;
        }

        #endregion
    }
}
