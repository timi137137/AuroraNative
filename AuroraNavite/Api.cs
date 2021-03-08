using AuroraNavite.WebSocket;
using Newtonsoft.Json.Linq;

namespace AuroraNavite
{
    /// <summary>
    /// API 类
    /// </summary>
    public class Api
    {
        #region --变量--

        private readonly BaseWebSocket WebSocket;

        #endregion

        #region --构造函数--

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="client">Client实例</param>
        public Api(BaseWebSocket WebSocket) 
        {
            this.WebSocket = WebSocket;
        }

        #endregion

        public void a() {
            WebSocket.Send(JObject.Parse("{\"action\":\"send_private_msg\",\"params\":{\"user_id\":3220419645,\"message\":\"你好\"},\"echo\":\"123\"}"));
        }
    }
}
