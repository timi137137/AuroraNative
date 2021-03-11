using System;

namespace AuroraNative
{
    /// <summary>
    /// 元事件 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [PostType(PostType.MetaEvent)]
    public class MetaEventTypeAttribute : BaseAttribute, IEquatable<MetaEventTypeAttribute>
    {
        #region --属性--

        /// <summary>
        /// 元事件 枚举
        /// </summary>
        public MetaEventType MetaEventType { get => Utils.GetEnumByDescription<MetaEventType>(Type); set => Type = value.ToString(); }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 元事件类型构造函数，初始化 <see cref="BaseAttribute"/> 类的实例
        /// </summary>
        /// <param name="MetaEventType">元事件类型</param>
        public MetaEventTypeAttribute(MetaEventType MetaEventType)
        {
            this.MetaEventType = MetaEventType;
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
            return Equals(obj as MetaEventTypeAttribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MetaEventTypeAttribute other)
        {
            if (other != null)
            {
                return MetaEventType == other.MetaEventType;
            }
            return false;
        }

        #endregion
    }

    /// <summary>
    /// 元事件 枚举
    /// </summary>
    public enum MetaEventType
    {
        /// <summary>
        /// 生命周期事件
        /// </summary>
        lifecycle = 1,
        /// <summary>
        /// 心跳事件
        /// </summary>
        heartbeat = 2
    }
}
