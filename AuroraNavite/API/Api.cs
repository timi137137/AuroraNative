using AuroraNavite.WebSocket;
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

        private readonly string UniqueCode;
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
        /// <param name="AutoEscape">消息内容是否作为纯文本发送</param>
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
        /// <param name="AutoEscape">消息内容是否作为纯文本发送</param>
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

            if (MessageType == null)
            {
                if (QID != 0)
                {
                    Params.Add("user_id", QID);
                }
                else if (GroupID != 0)
                {
                    Params.Add("group_id", GroupID);
                }
            }
            else if (MessageType == "private")
            {
                Params.Add("user_id", QID);
            }
            else if (MessageType == "group")
            {
                Params.Add("group_id", GroupID);
            }

            Params.Add("message", Message);
            Params.Add("auto_escape", AutoEscape);

            return await SendCallMessageID(new BaseAPI("send_msg", Params, "SendMsg:" + Utils.NowTimeSteamp()));
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

        private void SendCallVoid(BaseAPI Params)
        {
            WebSocket.Send(Params);
        }

        #endregion

        /// <summary>
        /// 返回消息ID的回调
        /// </summary>
        /// <param name="UniqueCode">API唯一识别码</param>
        /// <returns>错误返回-1，成功返回信息ID</returns>
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

        #endregion
    }
}
