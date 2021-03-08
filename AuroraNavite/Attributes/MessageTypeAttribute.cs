using System;

namespace AuroraNavite
{
    /// <summary>
    /// 消息事件 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [PostType(PostType.Message)]
    public class MessageTypeAttribute : BaseAttribute, IEquatable<MessageTypeAttribute>
    {
        #region --属性--

        /// <summary>
        /// 消息事件 枚举
        /// </summary>
        public MessageType MessageType { get => Utils.GetEnumByDescription<MessageType>(Type); set => Type = value.ToString(); }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 消息事件类型构造函数，初始化 <see cref="BaseAttribute"/> 类的实例
        /// </summary>
        /// <param name="MessageType">消息事件类型</param>
        public MessageTypeAttribute(MessageType MessageType)
        {
            this.MessageType = MessageType;
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
        public bool Equals(MessageTypeAttribute other)
        {
            if (other != null)
            {
                return MessageType == other.MessageType;
            }
            return false;
        }

        #endregion
    }

    /// <summary>
    /// 消息事件 枚举
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 私聊消息
        /// </summary>
        @private = 1,
        /// <summary>
        /// 群聊消息
        /// </summary>
        group = 2
    }
}
