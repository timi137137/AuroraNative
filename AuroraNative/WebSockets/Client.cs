using System;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AuroraNative.WebSockets
{
    /// <summary>
    /// WebSocket 客户端 封装类
    /// <para>正向 WebSocket</para>
    /// </summary>
    public class Client : BaseWebSocket
    {
        #region --变量--

        private string Host = "127.0.0.1:6700";
        /// <summary>
        /// WebSocket服务端地址<para>请记得带端口号</para>
        /// </summary>
        public string host
        {
            private get { return Host; }
            set { Host = value; }
        }

        #endregion

        #region --构造函数--

        static Client()
        {
            AttributeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsAbstract == false && p.IsInterface == false && typeof(Attribute).IsAssignableFrom(p)).ToArray();
        }

        #endregion

        #region --公开函数--

        /// <summary>
        /// 创建并连接到WebSocket服务器
        /// </summary>
        /// <returns>连接成功返回true，反而异之</returns>
        public bool Create()
        {
            if (WebSocket is ClientWebSocket socket) {
                Task Connect = socket.ConnectAsync(new Uri("ws://" + Host + "/"), CancellationToken.None);
                Connect.Wait();
                if (WebSocket.State == WebSocketState.Open)
                {
                    Task.Run(Feedback);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 立刻中断并释放连接<para>注意！断开后需要重新Create</para>
        /// </summary>
        public void Dispose()
        {
            WebSocket.Dispose();
            WebSocket.Abort();
        }

        #endregion

        #region --私有函数--

        private async void Feedback()
        {
            while (true)
            {
                await GetEventAsync();
            }
        }

        #endregion
    }
}
