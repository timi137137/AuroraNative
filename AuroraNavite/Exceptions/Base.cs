using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraNavite.Exceptions
{
    /// <summary>
    /// 异常 类 -- 基类
    /// </summary>
    class Base : Exception
    {
        public virtual int errorCode { get; set; }
        public int ErrorCode
        {
            get
            {
                return errorCode;
            }
        }

        public Base(int Code, string Messgae) : base(Messgae)
        {
            errorCode = Code;
        }
    }
}
