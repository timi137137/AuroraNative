using AuroraNative.Enum;
using System;

namespace AuroraNative.Attributes
{
    /// <summary>
    /// 请求事件 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [PostType(PostType.Request)]
    public class RequestTypeAttribute : Base, IEquatable<RequestTypeAttribute>
    {
        #region --属性--

        /// <summary>
        /// 请求事件 枚举
        /// </summary>
        public RequestType RequestType { get => Utils.GetEnumByDescription<RequestType>(Type); set => Type = value.ToString(); }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 请求事件类型构造函数，初始化 <see cref="Base"/> 类的实例
        /// </summary>
        /// <param name="RequestType">请求事件类型</param>
        public RequestTypeAttribute(RequestType RequestType)
        {
            this.RequestType = RequestType;
        }

        #endregion

        #region --公开函数--

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as MessageTypeAttribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(RequestTypeAttribute other)
        {
            if (other != null)
            {
                return RequestType == other.RequestType;
            }
            return false;
        }

        #endregion
    }
}
