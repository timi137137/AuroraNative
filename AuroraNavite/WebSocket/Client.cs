﻿using AuroraNavite.EventArgs;
using Newtonsoft.Json.Linq;
using System;
using System.Net.WebSockets;
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

        #region --私有函数--

        private async void Feedback()
        {
            while (true)
            {
                ArraySegment<byte> BytesReceived = new ArraySegment<byte>(new byte[5120]);
                WebSocketReceiveResult Result = await WebSocketClient.ReceiveAsync(BytesReceived, CancellationToken.None);
                Json = JObject.Parse(Encoding.UTF8.GetString(BytesReceived.Array, 0, Result.Count));
                switch ((string)Json.GetValue("post_type"))
                {
                    case "meta_event":
                        if ((string)Json.GetValue("meta_event_type") == "lifecycle")
                        {
                            EventHook.LifeCycle(Json.ToObject<LifeCycleArgs>());
                        }
                        else if ((string)Json.GetValue("meta_event_type") == "heartbeat")
                        {
                            EventHook.HeartBeat(Json.ToObject<HeartBeatArgs>());
                        }
                        break;
                    case "message":
                        if ((string)Json.GetValue("message_type") == "private")
                        {
                            EventHook.PrivateMessage(Json.ToObject<PrivateMessageArgs>());
                        }
                        else if ((string)Json.GetValue("message_type") == "group")
                        {
                            EventHook.GroupMessage(Json.ToObject<GroupMessageArgs>());
                        }
                        break;
                    case "request":
                        break;
                    case "notice":
                        break;
                    case "message_sent":
                        break;
                    default:
                        if (Json.TryGetValue("echo", out JToken JsonResult))
                        {
                            //TODO 跳给API
                        }
                        else
                        {
                            throw new Exceptions.JsonException(-1, "错误！无法解析推送的事件！\n" + Json);
                        }
                        break;
                }
            }
        }

        #endregion
    }
}
