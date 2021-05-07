using Newtonsoft.Json;

namespace AuroraNative.Type
{
    /// <summary>
    /// 运行统计 抽象类
    /// </summary>
    public sealed class RunningStatistics
    {
        #region --属性--

        /// <summary>
        /// 收到的数据包总数
        /// </summary>
        [JsonProperty(PropertyName = "packet_received")]
        public ulong PacketReceived;

        /// <summary>
        /// 发送的数据包总数
        /// </summary>
        [JsonProperty(PropertyName = "packet_sent")]
        public ulong PacketSent;

        /// <summary>
        /// 数据包丢失总数
        /// </summary>
        [JsonProperty(PropertyName = "packet_lost")]
        public uint PacketLost;

        /// <summary>
        /// 接受信息总数
        /// </summary>
        [JsonProperty(PropertyName = "message_received")]
        public ulong MessageReceived;

        /// <summary>
        /// 	发送信息总数
        /// </summary>
        [JsonProperty(PropertyName = "message_sent")]
        public ulong MessageSent;

        /// <summary>
        /// TCP 链接断开次数
        /// </summary>
        [JsonProperty(PropertyName = "disconnect_times")]
        public uint DisconnectsCount;

        /// <summary>
        /// 账号掉线次数
        /// </summary>
        [JsonProperty(PropertyName = "lost_times")]
        public uint LostsCount;

        #endregion
    }
}
