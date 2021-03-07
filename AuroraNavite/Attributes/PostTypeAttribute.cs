using System;
using System.ComponentModel;

namespace AuroraNavite
{
    /// <summary>
    /// 上报类型 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PostTypeAttribute : BaseAttribute
    {
        #region --变量--

        /// <summary>
        /// 上报类型
        /// </summary>
        public PostType PostType { get; set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 上报类型构造函数，初始化 <see cref="BaseAttribute"/> 类实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        public PostTypeAttribute(PostType PostType)
        {
            this.PostType = PostType;
        }

        #endregion
    }

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
