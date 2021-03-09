using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuroraNavite.WebSockets
{
    /// <summary>
    /// WebSocket 服务器 封装类
    /// <para>反向WebSocket</para>
    /// </summary>
    public class Server : BaseWebSocket
    {
        #region --变量--

        private string Port = "6700";
        /// <summary>
        /// WebSocket监听端口
        /// </summary>
        public string port
        {
            private get { return Port; }
            set { Port = value; }
        }

        private HttpListener Listener;
        private bool IsConnect = false;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 创建WebSocket服务器并监听端口
        /// </summary>
        public void Create()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add("http://*:" + Port + "/");
            Listener.Start();
            Task.Run(Feedback);
            while (!IsConnect) {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 立刻中断并释放连接<para>注意！断开后需要重新Create</para>
        /// </summary>
        public void Dispose()
        {
            Listener.Stop();
            WebSocket.Dispose();
            WebSocket.Abort();
        }

        #endregion

        #region --构造函数--

        static Server()
        {
            AttributeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsAbstract == false && p.IsInterface == false && typeof(Attribute).IsAssignableFrom(p)).ToArray();
        }

        #endregion

        #region --私有函数--

        private async void Feedback()
        {
            while (true)
            {
                HttpListenerContext Context = await Listener.GetContextAsync();
                if (Context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext SocketContext = await Context.AcceptWebSocketAsync(null);
                    WebSocket = SocketContext.WebSocket;
                    IsConnect = true;
                    while (WebSocket.State == WebSocketState.Open)
                    {
                        await GetEventAsync();
                    }
                }
            }
        }

        #endregion
    }
}
