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

        internal WebSocket WebSocket;
        internal Event EventHook;
        internal JObject Json;
        internal static Type[] AttributeTypes;

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
                            if (Method.GetCustomAttribute(Type) is BaseAttribute attribute && attribute.Type == (string)Json.GetValue(Utils.GetChildTypeByPostType(Json)))
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
