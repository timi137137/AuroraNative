namespace AuroraNative.Exceptions
{
    /// <summary>
    /// 异常 类 -- 关于CQ码异常
    /// </summary>
    class CQCodeException : Base
    {
        #region --构造函数--

        public CQCodeException(int Code, string Messgae) : base(Code, Messgae)
        {
            errorCode = Code;
        }

        #endregion
    }
}
