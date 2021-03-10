using System;

namespace AuroraNative.Exceptions
{
    /// <summary>
    /// 异常 类 -- 基类
    /// </summary>
    class Base : Exception
    {
        #region --变量--

        public virtual int errorCode { get; set; }
        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
        }

        #endregion

        #region --构造函数--

        public Base(int Code, string Messgae) : base(Messgae)
        {
            errorCode = Code;
        }

        #endregion
    }
}
