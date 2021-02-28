namespace AuroraNavite.Exceptions
{
    /// <summary>
    /// 异常 类 -- 关于WebSocket异常
    /// </summary>
    class WebSocketException : Base
    {
        public WebSocketException(int Code, string Messgae) : base(Code,Messgae) {
            errorCode = Code;
        }
    }
}
