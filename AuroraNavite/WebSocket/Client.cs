using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public string host
        {
            private get { return Host; }
            set { Host = value; }
        }
        /// <summary>
        /// 事件钩子
        /// </summary>
        public Event EventHook;

        private ClientWebSocket WebSocketClient;
        private JObject Json;
        private Task WaitFeedback;
        private static readonly Type[] AttributeTypes;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 创建并连接到WebSocket服务器
        /// </summary>
        /// <returns>连接成功返回true，反而异之</returns>
        public bool Create()
        {
            WebSocketClient = new ClientWebSocket();
            Task Connect = WebSocketClient.ConnectAsync(new Uri("ws://" + Host + "/"), CancellationToken.None);
            Connect.Wait();
            if (WebSocketClient.State == WebSocketState.Open)
            {
                WaitFeedback = Task.Run(Feedback);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 立刻中断并释放连接<para>注意！断开后需要重新Create</para>
        /// </summary>
        public void Dispose()
        {
            WebSocketClient.Dispose();
            WebSocketClient.Abort();
        }

        #endregion

        #region --构造函数--

        static Client()
        {
            AttributeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsAbstract == false && p.IsInterface == false && typeof(Attribute).IsAssignableFrom(p)).ToArray();
        }

        #endregion

        #region --私有函数--

        private async void Feedback()
        {
            while (true)
            {
                ArraySegment<byte> BytesReceived = new ArraySegment<byte>(new byte[5120]);
                WebSocketReceiveResult Result = await WebSocketClient.ReceiveAsync(BytesReceived, CancellationToken.None);
                Json = JObject.Parse(Encoding.UTF8.GetString(BytesReceived.Array, 0, Result.Count));

                if (Json.TryGetValue("echo", out JToken Token))
                {
                    //TODO API
                }
                else
                {
                    #region ==处理事件分发==

                    foreach (Type Type in AttributeTypes.Where(p => p.GetCustomAttribute<PostTypeAttribute>() != null))
                    {
                        PostTypeAttribute PostTypeAttribute = Type.GetCustomAttribute<PostTypeAttribute>();

                        if (PostTypeAttribute != null && PostTypeAttribute.PostType == Utils.GetEnumByDescription<PostType>((string)Json.GetValue("post_type")))
                        {
                            foreach (MethodInfo Method in EventHook.GetType().GetMethods().Where(p => p.GetCustomAttribute<PostTypeAttribute>() != null))
                            {
                                if (Method.GetCustomAttribute(Type) is BaseAttribute attribute && attribute.Type == (string)Json.GetValue(Utils.GetChildTypeByPostType(Json)))
                                {
                                    ParameterInfo Parameter = Method.GetParameters().SingleOrDefault();

                                    if (Parameter != null)
                                    {
                                        Method.Invoke(EventHook, new object[] { Json.ToObject(Parameter.ParameterType) });
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
        }

        #endregion
    }
}
