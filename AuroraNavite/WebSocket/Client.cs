using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace AuroraNavite.WebSocket
{
    /// <summary>
    /// WebSocket 客户端 封装类
    /// <para>正向 WebSocket</para>
    /// </summary>
    public class Client
    {
        #region --变量--

        private string Host = "127.0.0.1:6700";
        /// <summary>
        /// WebSocket服务端地址<para>请记得带端口号</para>
        /// </summary>
        public string host {
            private get { return Host; }
            set { Host = value; }
        }

        #endregion

        public void aClient()
        {
            JObject Json = new JObject();
            JObject SecJson = new JObject();

            Json.Add("action", "send_private_msg");
            Json.Add("echo", "a");
            SecJson.Add("user_id", "3220419645");
            SecJson.Add("message", "test");
            SecJson.Add("auto_escape", false);
            Json.Add("params", SecJson);

            CancellationToken token = new CancellationToken();
            ClientWebSocket client = new ClientWebSocket();
            client.ConnectAsync(new Uri("ws://127.0.0.1:6700/"), token);
            client.SendAsync(new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes(Json.ToString())), WebSocketMessageType.Text, true, token);
            client.CloseAsync(WebSocketCloseStatus.NormalClosure, "-1", token);
        }
    }
}
