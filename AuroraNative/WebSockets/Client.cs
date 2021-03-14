using System;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
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

        private string Host = "127.0.0.1";

        /// <summary>
        /// WebSocket服务端地址
        /// </summary>
        public string host
        {
            private get { return Host; }
            set
            {
                if (value.Contains(":"))
                {
                    string[] Cache = value.Split(':');
                    Host = Cache[0];
                    Port = int.Parse(Cache[1]);
                }
                else
                {
                    Host = value;
                }
            }
        }
        /// <summary>
        /// WebSocket服务端端口号
        /// </summary>
        public int port
        {
            private get { return Port; }
            set { Port = value; }
        }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 创建一个 <see cref="Client"/> 实例
        /// </summary>
        /// <param name="Event">重写后的事件类实例</param>
        public Client(Event Event) => EventHook = Event;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 创建并连接到WebSocket服务器
        /// </summary>
        public override void Create()
        {
            StringBuilder Cache = new StringBuilder();
            Cache.Append(Host);
            Cache.Append(':');
            Cache.Append(Port);

            Logger.Debug("正向WebSocket已创建，准备连接...", $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
            for (int i = 1; i < 4; i++)
            {
                try
                {
                    WebSocket = new ClientWebSocket();
                    if (WebSocket is ClientWebSocket socket)
                    {
                        Logger.Debug($"准备连接至IP:{Cache}", $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
                        Task Connect = socket.ConnectAsync(new Uri("ws://" + Cache.ToString() + "/"), CancellationToken.None);
                        Connect.Wait();
                        if (WebSocket.State == WebSocketState.Open)
                        {
                            Logger.Info("已连接至 go-cqhttp 服务器！");
                            Logger.Debug("防止由于go-cqhttp未初始化异常，连接后需等待2秒...");
                            Thread.Sleep(2000);
                            Logger.Debug("go-cqhttp 初始化完毕！");
                            Task.Run(Feedback);
                            Api.Create(this);
                            return;
                        }
                    }
                }
                catch (AggregateException)
                {
                    Logger.Warning($"连接到 go-cqhttp 服务器失败！五秒后重试(重试次数:{i})...", $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
                    Thread.Sleep(5000);
                }
            }
            Logger.Error("连接到 go-cqhttp 服务器失败！请检查IP是否正确(需要携带端口号)或确认服务器是否启动和初始化完毕！", $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
        }

        /// <summary>
        /// 立刻中断并释放连接<para>注意！断开后需要重新Create</para>
        /// </summary>
        public override void Dispose()
        {
            Logger.Debug($"准备销毁正向WebSocket...", $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
            try
            {
                WebSocket.Dispose();
                WebSocket.Abort();
                Api.Destroy();
                Logger.Info("已销毁正向WebSocket");
            }
            catch (Exception e)
            {
                Logger.Error("销毁正向WebSocket失败！\n" + e.Message, $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        #endregion

        #region --私有函数--

        internal override async void Feedback()
        {
            while (true)
            {
                await GetEventAsync();
            }
        }

        #endregion
    }
}
