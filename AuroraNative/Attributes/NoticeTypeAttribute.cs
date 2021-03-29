using AuroraNative.Enum;
using System;

namespace AuroraNative
{
    /// <summary>
    /// 通知事件 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [PostType(PostType.Notice)]
    public class NoticeTypeAttribute : Base, IEquatable<NoticeTypeAttribute>
    {
        #region --属性--

        /// <summary>
        /// 通知事件 枚举
        /// </summary>
        public NoticeType NoticeType { get => Utils.GetEnumByDescription<NoticeType>(Type); set => Type = value.ToString(); }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 通知事件类型构造函数，初始化 <see cref="Base"/> 类的实例
        /// </summary>
        /// <param name="NoticeType">通知事件类型</param>
        public NoticeTypeAttribute(NoticeType NoticeType)
        {
            this.NoticeType = NoticeType;
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
            return Equals(obj as NoticeTypeAttribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NoticeTypeAttribute other)
        {
            if (other != null)
            {
                return NoticeType == other.NoticeType;
            }
            return false;
        }

        #endregion
    }
}
