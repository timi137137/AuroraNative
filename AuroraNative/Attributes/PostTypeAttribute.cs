using AuroraNative.Enum;
using System;

namespace AuroraNative
{
    /// <summary>
    /// 上报类型 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PostTypeAttribute : Base
    {
        #region --变量--

        /// <summary>
        /// 上报类型
        /// </summary>
        public PostType PostType { get; set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 上报类型构造函数，初始化 <see cref="Base"/> 类实例
        /// </summary>
        /// <param name="PostType">上报类型</param>
        public PostTypeAttribute(PostType PostType)
        {
            this.PostType = PostType;
        }

        #endregion
    }
}
