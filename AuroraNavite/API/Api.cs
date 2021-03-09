using AuroraNavite.EventArgs;
using AuroraNavite.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuroraNavite
{
    /// <summary>
    /// API 类
    /// </summary>
    public class Api
    {
        #region --变量--

        /// <summary>
        /// 任务队列
        /// </summary>
        internal static JObject TaskList = new JObject();

        private readonly BaseWebSocket WebSocket;

        #endregion

        #region --构造函数--

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="WebSocket">WebSocket句柄</param>
        public Api(BaseWebSocket WebSocket)
        {
            this.WebSocket = WebSocket;
        }

        #endregion

        #region --公开函数--

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="QID">接受者QQ号</param>
        /// <param name="Message">信息内容</param>
        /// <param name="AutoEscape">是否转义<para>默认：false</para></param>
        /// <returns>返回消息ID，错误返回-1</returns>
        public async Task<string> SendPrivateMessage(long QID, string Message, bool AutoEscape = false)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
                { "message", Message },
                { "auto_escape", AutoEscape }
            };

            return await SendCallMessageID(new BaseAPI("send_private_msg", Params, "SendPrivateMessage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 发送群聊消息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Message">信息内容</param>
        /// <param name="AutoEscape">是否转义<para>默认：false</para></param>
        /// <returns>返回消息ID，错误返回-1</returns>
        public async Task<string> SendGroupMessage(long GroupID, string Message, bool AutoEscape = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "message", Message },
                { "auto_escape", AutoEscape }
            };

            return await SendCallMessageID(new BaseAPI("send_group_msg", Params, "SendGroupMessage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 转发合并消息 - 群
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Message">信息内容</param>
        /// <returns>返回消息ID，错误返回-1</returns>
        public void SendGroupForwardMessage(long GroupID, JArray Message)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "messages", Message }
            };

            SendCallVoid(new BaseAPI("send_group_forward_msg", Params, "SendGroupForwardMessage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message">信息内容</param>
        /// <param name="MessageType">信息类型<para>私聊：private</para><para>群聊：group</para></param>
        /// <param name="QID">QQ号</param>
        /// <param name="GroupID">群号</param>
        /// <param name="AutoEscape">是否转义<para>默认：false</para></param>
        /// <returns>错误返回-1，成功返回信息ID</returns>
        public async Task<string> SendMsg(string Message, string MessageType = null, long QID = 0, long GroupID = 0, bool AutoEscape = false)
        {
            JObject Params = new JObject();

            switch (MessageType)
            {
                case "private":
                    Params.Add("user_id", QID);
                    break;
                case "group":
                    Params.Add("group_id", GroupID);
                    break;
                default:
                    if (QID != 0)
                    {
                        Params.Add("user_id", QID);
                    }
                    else if (GroupID != 0)
                    {
                        Params.Add("group_id", GroupID);
                    }
                    break;
            }

            Params.Add("message", Message);
            Params.Add("auto_escape", AutoEscape);

            return await SendCallMessageID(new BaseAPI("send_msg", Params, "SendMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        public void DeleteMessage(int MessageID)
        {
            SendCallVoid(new BaseAPI("delete_msg", new JObject() { { "message_id", MessageID } }, "DeleteMessage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetMsg(string MessageID)
        {
            return await SendCallObject(new BaseAPI("get_msg", new JObject() { { "message_id", MessageID } }, "GetMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取合并转发内容
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetForwardMsg(string MessageID)
        {
            return await SendCallObject(new BaseAPI("get_forward_msg", new JObject() { { "message_id", MessageID } }, "GetForwardMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="Filename">图片缓存文件名,带不带后缀你喜欢就好</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetImage(string Filename)
        {
            if (!Filename.Contains(".image"))
            {
                Filename += ".image";
            }
            return await SendCallObject(new BaseAPI("get_image", new JObject() { { "file", Filename } }, "GetImage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 群组踢人
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QID">QQ号</param>
        /// <param name="RejectAddRequest">是否自动拒绝此人加群申请<para>默认:false</para></param>
        public void SetGroupKick(long GroupID, long QID, bool RejectAddRequest = false)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
                { "group_id", GroupID },
                { "reject_add_request", RejectAddRequest }
            };

            SendCallVoid(new BaseAPI("set_group_kick", Params, "SetGroupKick:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 群组单人禁言
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QID">QQ号</param>
        /// <param name="Duration">禁言时间，单位秒<para>默认:30分钟(1800秒)</para></param>
        public void SetGroupBan(long GroupID, long QID, int Duration = 1800)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
                { "group_id", GroupID },
                { "duration", Duration }
            };

            SendCallVoid(new BaseAPI("set_group_ban", Params, "SetGroupBan:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 群组匿名用户禁言
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Duration">禁言时间，单位秒。注意无法解除匿名用户禁言！<para>默认:30分钟(1800秒)</para></param>
        /// <param name="AnonymousFlag">匿名用户的Flag</param>
        /// <param name="Anonymous">群消息事件中完整的 anonymous </param>
        public void SetGroupAnonymousBan(long GroupID, int Duration = 1800, string AnonymousFlag = null, Anonymous Anonymous = null)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "duration", Duration }
            };

            if (Anonymous == null)
            {
                Params.Add("anonymous_flag", AnonymousFlag);
            }
            else
            {
                Params.Add("anonymous", JsonConvert.SerializeObject(Anonymous));
            }

            SendCallVoid(new BaseAPI("set_group_anonymous_ban", Params, "SetGroupAnonymousBan:" + Utils.NowTimeSteamp()));
        }

        #endregion

        #region --私有函数--

        #region ==调用函数==

        private async Task<string> SendCallMessageID(BaseAPI Params)
        {
            WebSocket.Send(Params);
            TaskList.Add(Params.UniqueCode, "Sended");

            string Result = "-1";
            await Task.Run(() => { Result = FeedbackMessageID(Params.UniqueCode); });
            return Result;
        }

        private async Task<JObject> SendCallObject(BaseAPI Params)
        {
            WebSocket.Send(Params);
            TaskList.Add(Params.UniqueCode, "Sended");

            JObject Result = null;
            await Task.Run(() => { Result = FeedbackObject(Params.UniqueCode); });
            return Result;
        }

        private void SendCallVoid(BaseAPI Params)
        {
            WebSocket.Send(Params);
        }

        #endregion

        private static string FeedbackMessageID(string UniqueCode)
        {
            JObject FBJson = new JObject();
            while (FBJson["status"] == null)
            {
                if (TaskList[UniqueCode].ToString() != "Sended")
                {
                    FBJson = JObject.Parse(TaskList[UniqueCode].ToString());
                    TaskList.Remove(UniqueCode);
                }
                Thread.Sleep(10);
            }
            //判断返回
            if (FBJson["status"].ToString() == "ok")
            {
                return FBJson["data"]["message_id"].ToString();
            }
            return "-1";
        }

        private static JObject FeedbackObject(string UniqueCode)
        {
            JObject FBJson = new JObject();
            while (FBJson["status"] == null)
            {
                if (TaskList[UniqueCode].ToString() != "Sended")
                {
                    FBJson = JObject.Parse(TaskList[UniqueCode].ToString());
                    TaskList.Remove(UniqueCode);
                }
                Thread.Sleep(10);
            }
            //判断返回
            if (FBJson["status"].ToString() == "ok")
            {
                return JObject.Parse(FBJson["data"].ToString());
            }
            return null;
        }

        #endregion
    }
}
