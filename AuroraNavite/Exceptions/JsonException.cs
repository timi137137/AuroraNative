namespace AuroraNavite.Exceptions
{
    /// <summary>
    /// 异常 类 -- 关于Json异常
    /// </summary>
    class JsonException : Base
    {
        #region --构造函数--

        public JsonException(int Code, string Messgae) : base(Code, Messgae)
        {
            errorCode = Code;
        }

        #endregion
    }
}
