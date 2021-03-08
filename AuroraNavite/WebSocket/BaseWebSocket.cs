using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace AuroraNavite.WebSocket
{
    public abstract class BaseWebSocket
    {
        public ClientWebSocket WebSocketClient;

        public void Send(JObject Json)
        {
            WebSocketClient.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(Json.ToString())), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
