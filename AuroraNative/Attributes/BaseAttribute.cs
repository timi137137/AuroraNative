using System;

namespace AuroraNative
{
    /// <summary>
    /// 特性的基类
    /// </summary>
    public abstract class BaseAttribute : Attribute
    {
        /// <summary>
        /// 子类型，是抽象的
        /// </summary>
        public string Type { get; set; }
    }
}
