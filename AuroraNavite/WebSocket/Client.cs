using AuroraNavite.EventArgs;
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
                        MetaEvents(Json);
                        break;
                    case "message":
                        MessageEvents(Json);
                        break;
                    case "request":
                        RequestEvents(Json);
                        break;
                    case "notice":
                        NoticeEvents(Json);
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
                            throw new Exceptions.JsonException(-1, "接到无法解析的事件！\n" + Json);
                        }
                        break;
                }
            }
        }

        private void MetaEvents(JObject Json)
        {
            if ((string)Json.GetValue("meta_event_type") == "lifecycle")
            {
                EventHook.LifeCycle(Json.ToObject<LifeCycleArgs>());
            }
            else if ((string)Json.GetValue("meta_event_type") == "heartbeat")
            {
                EventHook.HeartBeat(Json.ToObject<HeartBeatArgs>());
            }
        }

        private void MessageEvents(JObject Json)
        {
            if ((string)Json.GetValue("message_type") == "private")
            {
                EventHook.PrivateMessage(Json.ToObject<PrivateMessageArgs>());
            }
            else if ((string)Json.GetValue("message_type") == "group")
            {
                EventHook.GroupMessage(Json.ToObject<GroupMessageArgs>());
            }
        }

        private void RequestEvents(JObject Json)
        {
            if ((string)Json.GetValue("request_type") == "friend")
            {
                EventHook.FriendAddRequest(Json.ToObject<FriendAddRequsetArgs>());
            }
            else if ((string)Json.GetValue("request_type") == "group")
            {
                EventHook.GroupAddRequest(Json.ToObject<GroupAddRequestArgs>());
            }
        }

        private void NoticeEvents(JObject Json)
        {
            switch ((string)Json.GetValue("notice_type"))
            {
                case "group_upload":
                    EventHook.GroupUpload(Json.ToObject<GroupUploadArgs>());
                    break;
                case "group_admin":
                    EventHook.GroupManageChange(Json.ToObject<GroupManageChangeArgs>());
                    break;
                case "group_decrease":
                    EventHook.GroupMemberDecrease(Json.ToObject<GroupMemberDecreaseArgs>());
                    break;
                case "group_increase":
                    EventHook.GroupMemberIncrease(Json.ToObject<GroupMemberIncreaseArgs>());
                    break;
                case "group_ban":
                    EventHook.GroupBanSpeak(Json.ToObject<GroupBanSpeakArgs>());
                    break;
                case "friend_add":
                    EventHook.FriendAdd(Json.ToObject<FriendAddArgs>());
                    break;
                case "group_recall":
                    EventHook.GroupMessageRecall(Json.ToObject<GroupMessageRecallArgs>());
                    break;
                case "friend_recall":
                    EventHook.PrivateMessageRecall(Json.ToObject<PrivateMessageRecallArgs>());
                    break;
                case "notify":
                    NotifyEvents(Json);
                    break;
                case "group_card":
                    EventHook.GroupCardVerify(Json.ToObject<GroupCardVerifyArgs>());
                    break;
                case "offline_file":
                    EventHook.GetOfflineFile(Json.ToObject<GetOfflineFileArgs>());
                    break;
                case "client_status":
                    EventHook.ClientStatusChange(Json.ToObject<ClientStatusChangeArgs>());
                    break;
                case "essence":
                    EventHook.EssenceMessageChange(Json.ToObject<EssenceMessageChangeArgs>());
                    break;
                default:
                    break;
            }
        }

        private void NotifyEvents(JObject Json)
        {
            switch ((string)Json.GetValue("sub_type"))
            {
                case "poke":
                    if (Json.TryGetValue("group_id", out _))
                    {
                        EventHook.GroupPoke(Json.ToObject<GroupPokeArgs>());
                    }
                    else
                    {
                        EventHook.PrivatePoke(Json.ToObject<PrivatePokeArgs>());
                    }
                    break;
                case "lucky_king":
                    EventHook.GroupRedPoketLuckyKing(Json.ToObject<GroupRedPoketLuckyKingArgs>());
                    break;
                case "honor":
                    EventHook.GroupMemberHonorChange(Json.ToObject<GroupMemberHonorChangeArgs>());
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
