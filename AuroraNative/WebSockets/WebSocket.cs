using AuroraNative.Enum;
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
    /// WebSocket基类
    /// </summary>
    public abstract class WebSocket
    {
        #region --变量--

        internal System.Net.WebSockets.WebSocket WebSockets;
        internal Event EventHook;
        internal JObject Json;
        internal int Port = 6700;
        internal static readonly System.Type[] AttributeTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.IsAbstract == false && p.IsInterface == false && typeof(Attribute).IsAssignableFrom(p)).ToArray();
        internal static readonly Version DependencyVersion = new Version("1.0.0");
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

        /// <summary>
        /// 客户端事件处理 抽象方法
        /// </summary>
        internal abstract void Feedback();

        /// <summary>
        /// 发送数据到服务端/客户端
        /// </summary>
        /// <param name="Json">传输Json格式的文本</param>
        internal void Send(BaseAPI Json)
        {
            try
            {
                WebSockets.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Json, Formatting.None))), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Logger.Error("调用API出现未知错误！\n" + e.Message, $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
            }
        }

        internal async Task GetEventAsync()
        {
            ArraySegment<byte> BytesReceived = new ArraySegment<byte>(new byte[10240]);
            WebSocketReceiveResult Result = await WebSockets.ReceiveAsync(BytesReceived, CancellationToken.None);
            if (Result.Count == 0) { return; }
            Json = JsonConvert.DeserializeObject<JObject>(Encoding.UTF8.GetString(BytesReceived.Array, 0, Result.Count));

            if (Json.TryGetValue("echo", out JToken Token))
            {
                if (Json.TryGetValue("status", out JToken Cache) && Cache.ToString() != "ok")
                {
                    Logger.Warning(Json.ToString(), $"{MethodBase.GetCurrentMethod().DeclaringType.Name}.{MethodBase.GetCurrentMethod().Name}");
                }

                lock (API.TaskList) {
                    API.TaskList[Token.ToString()] = Json;
                }
            }
            else
            {
                #region ==处理事件分发==

                foreach (System.Type Type in AttributeTypes.Where(p => p.GetCustomAttribute<PostTypeAttribute>() != null))
                {
                    PostTypeAttribute PostTypeAttribute = Type.GetCustomAttribute<PostTypeAttribute>();

                    if (PostTypeAttribute != null && PostTypeAttribute.PostType == Utils.GetEnumByDescription<PostType>((string)Json.GetValue("post_type")))
                    {
                        foreach (MethodInfo Method in EventHook.GetType().GetMethods().Where(p => p.GetCustomAttribute<PostTypeAttribute>() != null))
                        {
                            if (Method.GetCustomAttribute(Type) is Base Attribute && Attribute.Type == (string)Json.GetValue(Utils.GetChildTypeByPostType(Json)))
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
