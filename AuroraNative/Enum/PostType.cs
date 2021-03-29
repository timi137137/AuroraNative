using System.ComponentModel;

namespace AuroraNative.Enum
{
    /// <summary>
    /// 上报类型 枚举
    /// </summary>
    public enum PostType
    {
        /// <summary>
        /// 元事件
        /// </summary>
        [Description("meta_event")]
        MetaEvent = 1,
        /// <summary>
        /// 消息事件
        /// </summary>
        [Description("message")]
        Message = 2,
        /// <summary>
        /// 通知事件
        /// </summary>
        [Description("notice")]
        Notice = 3,
        /// <summary>
        /// 请求事件
        /// </summary>
        [Description("request")]
        Request = 4
    }
}
