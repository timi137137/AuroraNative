using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuroraNative.WebSockets
{
    /// <summary>
    /// WebSocket 基础类
    /// </summary>
    public abstract class BaseWebSocket
    {
        #region --变量--

        internal int Port = 6700;
        internal WebSocket WebSocket;
        internal Event EventHook;
        internal JObject Json;
        internal static readonly Type[] AttributeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsAbstract == false && p.IsInterface == false && typeof(Attribute).IsAssignableFrom(p)).ToArray();
        internal static readonly Version DependencyVersion = new Version("0.9.40");
        internal static bool IsCheckVersion = false;

        #endregion

        #region --公开函数--

        /// <summary>
        /// 客户端创建 抽象方法
        /// </summary>
        public abstract void Create();
        /// <summary>
        /// 客户端销毁 抽象方法
        /// </summary>
        public abstract void Dispose();

        internal abstract void Feedback();

        /// <summary>
        /// 发送数据到服务端/客户端
        /// </summary>
        /// <param name="Json">传输Json格式的文本</param>
        internal void Send(BaseAPI Json)
        {
            try
            {
                WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Json, Formatting.None))), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Logger.Error("调用API出现未知错误！\n" + e.Message, $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        internal async Task GetEventAsync()
        {
            ArraySegment<byte> BytesReceived = new ArraySegment<byte>(new byte[5120]);
            WebSocketReceiveResult Result = await WebSocket.ReceiveAsync(BytesReceived, CancellationToken.None);
            Json = JObject.Parse(Encoding.UTF8.GetString(BytesReceived.Array, 0, Result.Count));

            if (Json.TryGetValue("echo", out JToken Token))
            {
                Api.TaskList[Json["echo"].ToString()] = Json;
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
                            if (Method.GetCustomAttribute(Type) is BaseAttribute Attribute && Attribute.Type == (string)Json.GetValue(Utils.GetChildTypeByPostType(Json)))
                            {
                                ParameterInfo Parameter = Method.GetParameters().SingleOrDefault();

                                if (Parameter != null)
                                {
                                    Method.Invoke(EventHook, new object[] { Json.ToObject(Parameter.ParameterType) });
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion
            }
        }

        #endregion
    }
}
