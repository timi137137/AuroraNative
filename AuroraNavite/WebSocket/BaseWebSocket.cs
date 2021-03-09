using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace AuroraNavite.WebSocket
{
    /// <summary>
    /// WebSocket 基础类
    /// </summary>
    public abstract class BaseWebSocket
    {
        #region --变量--

        /// <summary>
        /// Websocket句柄
        /// </summary>
        public ClientWebSocket WebSocket;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 发送数据到服务端/客户端
        /// </summary>
        /// <param name="Json">传输Json格式的文本</param>
        public void Send(BaseAPI Json)
        {
            WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Json, Formatting.None))), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        #endregion
    }
}
