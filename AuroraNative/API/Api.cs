using AuroraNative.Exceptions;
using AuroraNative.Type;
using AuroraNative.Type.Files;
using AuroraNative.Type.Groups;
using AuroraNative.Type.Groups.SystemMessages;
using AuroraNative.Type.Users;
using AuroraNative.WebSockets;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AuroraNative
{
    /// <summary>
    /// API 类
    /// </summary>
    public sealed class API
    {
        #region --变量--

        internal static JObject TaskList = new JObject();
        internal static MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        private readonly WebSocket WebSockets;

        #endregion

        #region --属性--

        /// <summary>
        /// 获取API实例
        /// </summary>
        public static API CurrentApi => (API)Cache.Get($"API{AppDomain.CurrentDomain.Id}");

        #endregion

        #region --构造函数--

        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="WebSockets">WebSocket句柄</param>
        private API(WebSocket WebSockets)
        {
            if (WebSockets != null)
            {
                this.WebSockets = WebSockets;
            }
            else
            {
                throw new WebSocketException(-1, "传入的WebSocket不可为空");
            }
        }

        #endregion

        #region --公开函数--

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="UserID">接受者QQ号</param>
        /// <param name="Message">信息内容</param>
        /// <param name="AutoEscape">是否转义<para>默认：false</para></param>
        /// <returns>返回消息ID，错误返回-1</returns>
        public async Task<string> SendPrivateMessage(long UserID, string Message, bool AutoEscape = false)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
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
            //FIXME 等昵昵修复获取合并转发的BUG才能继续
            SendCallVoid(new BaseAPI("send_group_forward_msg", Params, "SendGroupForwardMessage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message">信息内容</param>
        /// <param name="MessageType">信息类型<para>私聊：private</para><para>群聊：group</para></param>
        /// <param name="UserID">QQ号</param>
        /// <param name="GroupID">群号</param>
        /// <param name="AutoEscape">是否转义<para>默认：false</para></param>
        /// <returns>错误返回-1，成功返回信息ID</returns>
        public async Task<string> SendMsg(string Message, string MessageType = null, long UserID = 0, long GroupID = 0, bool AutoEscape = false)
        {
            JObject Params = new JObject() {
                { "message", Message},
                { "auto_escape", AutoEscape}
            };

            switch (MessageType)
            {
                case "private":
                    Params.Add("user_id", UserID);
                    break;
                case "group":
                    Params.Add("group_id", GroupID);
                    break;
                case null:
                    if (UserID != 0)
                    {
                        Params.Add("user_id", UserID);
                    }
                    else if (GroupID != 0)
                    {
                        Params.Add("group_id", GroupID);
                    }
                    break;
                default:
                    throw new Exceptions.JsonException(-1, "传入的参数不符合预期");
            }

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
        public async Task<Dictionary<string, object>> GetMsg(string MessageID)
        {
            JObject Json = await SendCallObject(new BaseAPI("get_msg", new JObject() { { "message_id", MessageID } }, "GetMsg:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                {"MessageID",Json.Value<int>("message_id")},
                {"RealID",Json.Value<int>("real_id")},
                {"Sender",Json["sender"].ToObject<Sender>()},
                {"Time",Json.Value<int>("time")},
                {"Message",Json.Value<string>("message")},
                {"RawMessage",Json.Value<string>("raw_message")}
            };
        }

        /// <summary>
        /// 获取合并转发内容
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetForwardMsg(string MessageID)
        {
            //TODO 等转发合并消息做完后需要修改这个方法的返回类型
            return await SendCallObject(new BaseAPI("get_forward_msg", new JObject() { { "message_id", MessageID } }, "GetForwardMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="Filename">图片缓存文件名,带不带后缀你喜欢就好</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetImage(string Filename)
        {
            if (!Filename.Contains(".image"))
            {
                Filename += ".image";
            }

            JObject Json = await SendCallObject(new BaseAPI("get_image", new JObject() { { "file", Filename } }, "GetImage:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                {"Size",Json.Value<int>("size")},
                {"FileName",Json.Value<string>("filename")},
                {"Url",Json.Value<string>("url")}
            };
        }

        /// <summary>
        /// 群组踢人
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="RejectAddRequest">是否自动拒绝此人加群申请<para>默认:false</para></param>
        public void SetGroupKick(long GroupID, long UserID, bool RejectAddRequest = false)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
                { "group_id", GroupID },
                { "reject_add_request", RejectAddRequest }
            };

            SendCallVoid(new BaseAPI("set_group_kick", Params, "SetGroupKick:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 群组单人禁言
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="Duration">禁言时间，单位秒<para>默认:30分钟(1800秒)</para></param>
        public void SetGroupBan(long GroupID, long UserID, int Duration = 1800)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
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

        /// <summary>
        /// 群组全员禁言
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Enable">是否禁言<para>默认:true</para></param>
        public void SetGroupWholeBan(long GroupID, bool Enable = true)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "enable", Enable }
            };

            SendCallVoid(new BaseAPI("set_group_whole_ban", Params, "SetGroupWholeBan:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群管理员
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="Enable">是否设置为管理员<para>默认:true</para></param>
        public void SetGroupAdmin(long GroupID, long UserID, bool Enable = true)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
                { "group_id", GroupID },
                { "enable", Enable }
            };

            SendCallVoid(new BaseAPI("set_group_admin", Params, "SetGroupAdmin:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群名片
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="Card">群名片内容<para>默认:null（删除群名片）</para></param>
        public void SetGroupCard(long GroupID, long UserID, string Card = null)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
                { "group_id", GroupID },
                { "card", Card }
            };

            SendCallVoid(new BaseAPI("set_group_card", Params, "SetGroupCard:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群名
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="GroupName">新群名</param>
        public void SetGroupName(long GroupID, string GroupName = null)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "group_name", GroupName }
            };

            SendCallVoid(new BaseAPI("set_group_ban", Params, "SetGroupBan:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 退出群组
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="IsDismiss">是否解散</param>
        public void SetGroupLeave(long GroupID, bool IsDismiss = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "is_dismiss", IsDismiss }
            };

            SendCallVoid(new BaseAPI("set_group_leave", Params, "SetGroupLeave:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群组成员专属头衔
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="SpecialTitle">群名片内容<para>默认:null（删除群名片）</para></param>
        /// <param name="Duration">专属头衔有效期, 单位秒, 不过此项似乎没有效果, 可能是只有某些特殊的时间长度有效, 有待测试<para>默认:-1(永久)</para></param>
        public void SetGroupSpecialTitle(long GroupID, long UserID, string SpecialTitle = null, int Duration = -1)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
                { "group_id", GroupID },
                { "duration", Duration },
                { "special_title", SpecialTitle }
            };

            SendCallVoid(new BaseAPI("set_group_special_title", Params, "SetGroupSpecialTitle:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 处理加好友请求
        /// </summary>
        /// <param name="Flag">加好友请求事件的Flag</param>
        /// <param name="Approve">是否同意请求<para>默认:true</para></param>
        /// <param name="Remark">添加好友后的备注（仅在同意时有效)</param>
        public void SetFriendAddRequest(string Flag, bool Approve = true, string Remark = null)
        {
            JObject Params = new JObject
            {
                { "flag", Flag },
                { "approve", Approve },
                { "remark", Remark }
            };

            SendCallVoid(new BaseAPI("set_friend_add_request", Params, "SetFriendAddRequest:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 处理加群请求/邀请
        /// </summary>
        /// <param name="Flag">加群请求事件的Flag</param>
        /// <param name="SubType">子类型，需与加群请求事件上的 sub_type 一致</param>
        /// <param name="Approve">是否同意请求/邀请<para>默认:true</para></param>
        /// <param name="Reason">拒绝理由（仅在拒绝时有效)</param>
        public void SetGroupAddRequest(string Flag, string SubType, bool Approve = true, string Reason = null)
        {
            JObject Params = new JObject
            {
                { "flag", Flag },
                { "sub_type ", SubType },
                { "approve", Approve },
                { "reason", Reason }
            };

            SendCallVoid(new BaseAPI("set_group_add_request", Params, "SetGroupAddRequest:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取登录号信息
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetLoginInfo()
        {
            JObject Json = await SendCallObject(new BaseAPI("get_login_info", null, "GetLoginInfo:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                {"UserID",Json.Value<long>("user_id")},
                {"NickName",Json.Value<string>("nickname")}
            };
        }

        /// <summary>
        /// 获取陌生人信息
        /// </summary>
        /// <param name="UserID">QQ号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetStrangerInfo(long UserID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "user_id", UserID },
                { "no_cache ", Cache }
            };

            JObject Json = await SendCallObject(new BaseAPI("get_stranger_info", Params, "GetStrangerInfo:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                {"UserID",Json.Value<int>("user_id")},
                {"NickName",Json.Value<string>("nickname")},
                {"Sex",Json.Value<string>("sex")},
                {"Age",Json.Value<string>("age")},
                {"QID",Json.Value<string>("qid")}
            };
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<Friends>> GetFriendList()
        {
            return (await SendCallArray(new BaseAPI("get_friend_list", null, "GetFriendList:" + Utils.NowTimeSteamp()))).ToObject<List<Friends>>();
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="UserID">好友QQ号</param>
        public void DeleteFriend(long UserID) {
            SendCallVoid(new BaseAPI("delete_friend", new JObject{{ "friend_id", UserID }}, "DeleteFriend:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Groups> GetGroupInfo(long GroupID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "no_cache ", Cache }
            };

            return (await SendCallObject(new BaseAPI("get_group_info", Params, "GetGroupInfo:" + Utils.NowTimeSteamp()))).ToObject<Groups>();
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<Groups>> GetGroupList()
        {
            return (await SendCallObject(new BaseAPI("get_group_list", null, "GetGroupList:" + Utils.NowTimeSteamp()))).ToObject<List<Groups>>();
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="UserID">QQ号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<GroupMember> GetGroupMemberInfo(long GroupID, long UserID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "user_id", UserID },
                { "no_cache ", Cache }
            };

            return (await SendCallObject(new BaseAPI("get_group_member_info", Params, "GetGroupMemberInfo:" + Utils.NowTimeSteamp()))).ToObject<GroupMember>();
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<GroupMember>> GetGroupMemberList(long GroupID)
        {
            return (await SendCallObject(new BaseAPI("get_group_member_info", new JObject { { "group_id", GroupID } }, "GetGroupMemberInfo:" + Utils.NowTimeSteamp()))).ToObject<List<GroupMember>>();
        }

        /// <summary>
        /// 获取群荣耀信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Type">要获取的群荣誉类型<para>talkative/performer/legend/strong_newbie/emotion/all</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<HonorListInfo>> GetGroupHonorInfo(long GroupID, string Type)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "type ", Type }
            };

            JObject Json = await SendCallObject(new BaseAPI("get_group_honor_info", Params, "GetGroupHonorInfo:" + Utils.NowTimeSteamp()));
            List<HonorListInfo> Result = new List<HonorListInfo>();

            foreach (string i in new string[] { "talkative", "performer", "legend", "strong_newbie", "emotion" })
            {
                if (Json.TryGetValue("current_talkative", out JToken Cache))
                {
                    Result.Add(Cache.ToObject<HonorListInfo>());
                }
                if (Json.TryGetValue(i + "_list", out Cache))
                {
                    Result.Add(Cache.ToObject<HonorListInfo>());
                }
            }

            return Result;
        }

        /// <summary>
        /// 检查是否可以发送图片
        /// </summary>
        /// <returns>错误返回false</returns>
        public async Task<bool> CanSendImage()
        {
            return (await SendCallObject(new BaseAPI("can_send_image", null, "CanSendImage:" + Utils.NowTimeSteamp()))).Value<bool>("yes");
        }

        /// <summary>
        /// 检查是否可以发送语音
        /// </summary>
        /// <returns>错误返回false</returns>
        public async Task<bool> CanSendRecord()
        {
            return (await SendCallObject(new BaseAPI("can_send_record", null, "CanSendRecord:" + Utils.NowTimeSteamp()))).Value<bool>("yes");
        }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetVersionInfo()
        {
            JObject Json = await SendCallObject(new BaseAPI("get_version_info", null, "GetVersionInfo:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                {"AppFullName",Json.Value<string>("app_full_name")},
                {"AppName",Json.Value<string>("app_name")},
                {"AppVersion",Json.Value<string>("app_version")},
                {"CQDirectory",Json.Value<string>("coolq_directory")},
                {"CQEdition",Json.Value<string>("coolq_edition")},
                {"IsGoCqhttp",Json.Value<bool>("go-cqhttp")},
                {"PluginBuildConfiguration",Json.Value<string>("plugin_build_configuration")},
                {"PluginBuildNumber",Json.Value<string>("plugin_build_number")},
                {"PluginVersion",Json.Value<string>("plugin_version")},
                {"Protocol",Json.Value<string>("protocol")},
                {"ProtocolVersion",Json.Value<string>("protocol_version")},
                {"RuntimeOS",Json.Value<string>("runtime_os")},
                {"RuntimeVersion",Json.Value<string>("runtime_version")},
                {"Version",Json.Value<string>("version")}
            };
        }

        /// <summary>
        /// 重启 go-cqhttp (谨慎使用！)
        /// </summary>
        /// <param name="Delay">延迟重启时间，单位毫秒<para>默认:0 (立刻重启)</para></param>
        public void SetRestart(int Delay = 0)
        {
            SendCallVoid(new BaseAPI("set_restart", new JObject() { { "delay", Delay } }, "SetRestart:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群头像
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="File">图片</param>
        /// <param name="Cache">是否使用已缓存的文件<para>默认:1(使用)</para></param>
        public void SetGroupPortrait(long GroupID, string File, int Cache = 1)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "file", File },
                { "cache ", Cache }
            };

            SendCallVoid(new BaseAPI("set_group_portrait", Params, "SetGroupPortrait:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取中文分词(谨慎使用!)
        /// </summary>
        /// <param name="Content">内容</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<string[]> GetWordSlices(string Content)
        {
            return (await SendCallObject(new BaseAPI(".get_word_slices", new JObject { { "content", Content } }, "GetWordSlices:" + Utils.NowTimeSteamp()))).GetValue("slices").ToObject<string[]>();
        }

        /// <summary>
        /// 图片OCR
        /// </summary>
        /// <param name="Image">图片ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> OCRImage(string Image)
        {
            JObject Json = await SendCallObject(new BaseAPI("ocr_image", new JObject { { "image", Image } }, "OCRImage:" + Utils.NowTimeSteamp()));
            return new Dictionary<string, object>() {
                { "Texts",Json.Value<JToken>("texts").ToObject<List<OCRTextDetection>>()},
                { "Language",Json.Value<string>("language")}
            };
        }

        /// <summary>
        /// 获取群系统消息
        /// </summary>
        /// <returns>错误或不存在任何消息返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetGroupSystemMsg()
        {
            JObject Json = await SendCallObject(new BaseAPI("get_group_system_msg", null, "GetGroupSystemMsg:" + Utils.NowTimeSteamp()));
            return new Dictionary<string, object>() {
                { "InvitedRequest",Json.Value<JToken>("invited_requests").ToObject<List<InvitedRequest>>()},
                { "JoinRequest",Json.Value<JToken>("join_requests").ToObject<List<JoinRequest>>()}
            };
        }

        /// <summary>
        /// 上传本地文件到群文件
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="FilePath">本地文件路径</param>
        /// <param name="SaveName">群文件保存的文件名称</param>
        /// <param name="Folder">上传到群文件的父目录ID<para>默认:null (保存到根目录下)</para></param>
        public void UploadGroupFile(long GroupID, string FilePath, string SaveName, string Folder = null)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "file", FilePath },
                { "name", SaveName },
                { "folder ", Folder }
            };

            SendCallVoid(new BaseAPI("upload_group_file", Params, "UploadGroupFile:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群文件系统信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetGroupFileSystemInfo(long GroupID)
        {
            JObject Json = await SendCallObject(new BaseAPI("get_group_file_system_info", new JObject { { "group_id", GroupID } }, "GetGroupFileSystemInfo:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object> {
                { "FileCount",Json.Value<int>("file_count")},
                { "LimitCount",Json.Value<int>("limit_count")},
                { "UsedSpace",Json.Value<long>("used_space")},
                { "TotalSpace",Json.Value<long>("total_space")}
            };
        }

        /// <summary>
        /// 获取群根目录文件列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetGroupRootFiles(long GroupID)
        {
            JObject Json = await SendCallObject(new BaseAPI("get_group_root_files", new JObject { { "group_id", GroupID } }, "GetGroupRootFiles:" + Utils.NowTimeSteamp()));
            return new Dictionary<string, object>() {
                { "Files",Json.Value<JToken>("files").ToObject<List<File>>()},
                { "Folder",Json.Value<JToken>("folders").ToObject<List<Folder>>()}
            };
        }

        /// <summary>
        /// 获取群子目录文件列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="FolderID">文件夹ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetGroupFilesByFolder(long GroupID, string FolderID)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "folder_id ", FolderID }
            };

            JObject Json = await SendCallObject(new BaseAPI("get_group_files_by_folder", Params, "GetGroupFilesByFolder:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                { "Files",Json.Value<JToken>("files").ToObject<List<File>>()},
                { "Folder",Json.Value<JToken>("folders").ToObject<List<Folder>>()}
            };
        }

        /// <summary>
        /// 获取群文件资源链接
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="FileID">文件ID</param>
        /// <param name="BusID">文件类型</param>
        /// <returns>错误返回null,成功返回下载链接</returns>
        public async Task<string> GetGroupFilesURL(long GroupID, string FileID, int BusID)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "file_id", FileID },
                { "busid ", BusID }
            };

            return (await SendCallObject(new BaseAPI("get_group_file_url", Params, "GetGroupFilesURL:" + Utils.NowTimeSteamp()))).Value<string>("url");
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetStatus()
        {
            JObject Json = await SendCallObject(new BaseAPI("get_group_info", null, "GetGroupInfo:" + Utils.NowTimeSteamp()));
            return new Dictionary<string, object>() {
                { "AppInitialized",Json.Value<bool>("app_initialized")},
                { "AppEnabled",Json.Value<bool>("app_enabled")},
                { "PluginsGood",Json.Value<bool>("plugins_good")},
                { "AppGood",Json.Value<bool>("app_good")},
                { "Online",Json.Value<bool>("online")},
                { "Good",Json.Value<bool>("good")},
                { "Statistics",Json.Value<RunningStatistics>("stat")}
            };
        }

        /// <summary>
        /// 获取群 @全体成员 剩余次数
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetGroupAtAllRemain(long GroupID)
        {
            JObject Json = await SendCallObject(new BaseAPI("get_group_at_all_remain", new JObject { { "group_id", GroupID } }, "GetGroupAtAllRemain:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                { "CanAtAll",Json.Value<bool>("can_at_all")},
                { "GroupAdminAtAllCount",Json.Value<short>("remain_at_all_count_for_group")},
                { "BotAtAllCount",Json.Value<short>("remain_at_all_count_for_uin")}
            };
        }

        /// <summary>
        /// 对事件执行快速操作 (隐藏 API )
        /// </summary>
        /// <param name="Context">事件数据对象</param>
        /// <param name="Operation">快速操作对象</param>
        public void HandleQuickOperation(object Context, object Operation)
        {
            JObject Params = new JObject
            {
                { "context", JsonConvert.SerializeObject(Context) },
                { "operation ", JsonConvert.SerializeObject(Operation) }
            };

            SendCallVoid(new BaseAPI(".handle_quick_operation", Params, "HandleQuickOperation:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取VIP信息
        /// </summary>
        /// <param name="UserID">QQ号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<Dictionary<string, object>> GetVIPInfo(long UserID)
        {
            JObject Json = await SendCallObject(new BaseAPI("_get_vip_info", new JObject { { "user_id", UserID } }, "GetGroupAtAllRemain:" + Utils.NowTimeSteamp()));

            return new Dictionary<string, object>() {
                { "UserID",Json.Value<long>("user_id")},
                { "NickName",Json.Value<string>("nickname")},
                { "Level",Json.Value<long>("level")},
                { "LevelSpeed",Json.Value<double>("level_speed")},
                { "VipLevel",Json.Value<string>("vip_level")},
                { "VipGrowthSpeed",Json.Value<long>("vip_growth_speed")},
                { "VipGrowthTotal",Json.Value<long>("vip_growth_total")}
            };
        }

        /// <summary>
        /// 发送群公告
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Content">公告内容</param>
        public void SendGroupNotice(long GroupID, string Content)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "content ", Content }
            };

            SendCallVoid(new BaseAPI("_send_group_notice", Params, "SendGroupNotice:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 重载事件过滤器
        /// </summary>
        public void ReloadEventFilter()
        {
            SendCallVoid(new BaseAPI("reload_event_filter", null, "ReloadEventFilter:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 下载文件到缓存目录
        /// </summary>
        /// <param name="URL">下载链接</param>
        /// <param name="ThreadCount">线程数</param>
        /// <param name="Headers">自定义请求头</param>
        /// <returns>错误返回null,成功返回本地绝对路径</returns>
        public async Task<string> DownloadFile(string URL, int ThreadCount, string[] Headers)
        {
            JObject Params = new JObject
            {
                { "url", URL },
                { "thread_count", ThreadCount },
                { "headers ", JArray.FromObject(Headers) }
            };

            return (await SendCallObject(new BaseAPI("download_file", Params, "DownloadFile:" + Utils.NowTimeSteamp()))).Value<string>("file");
        }

        /// <summary>
        /// 获取当前账号在线客户端列表
        /// </summary>
        /// <param name="Cache">是否无视缓存</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<Device>> GetOnlineClients(bool Cache)
        {
            return (await SendCallObject(new BaseAPI("get_online_clients", new JObject { { "no_cache ", Cache } }, "GetOnlineClients:" + Utils.NowTimeSteamp()))).Value<JToken>("clients").ToObject<List<Device>>();
        }

        /// <summary>
        /// 获取群消息历史记录
        /// </summary>
        /// <param name="MessageSeq">起始消息序号</param>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<Messages>> GetGroupMsgHistory(long GroupID, long MessageSeq = 0)
        {
            JObject Params = new JObject
            {
                { "message_seq", MessageSeq },
                { "group_id", GroupID }
            };

            if (MessageSeq == 0)
            {
                Params.Remove("message_seq");
            }

            JObject Json = await SendCallObject(new BaseAPI("get_group_msg_history", Params, "GetGroupMsgHistory:" + Utils.NowTimeSteamp()));
            return Json.Value<JArray>("messages").ToObject<List<Messages>>();
        }

        /// <summary>
        /// 设置精华消息
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        public void SetEssenceMsg(string MessageID)
        {
            SendCallVoid(new BaseAPI("set_essence_msg", new JObject { { "message_id", MessageID } }, "SetEssenceMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 移除精华消息
        /// </summary>
        /// <param name="MessageID">消息ID</param>
        public void DelEssenceMsg(string MessageID)
        {
            SendCallVoid(new BaseAPI("delete_essence_msg", new JObject { { "message_id", MessageID } }, "DelEssenceMsg:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取精华消息列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<List<Essences>> GetEssenceMsgList(long GroupID)
        {
            return (await SendCallObject(new BaseAPI("get_essence_msg_list", new JObject { { "group_id", GroupID } }, "GetEssenceMsgList:" + Utils.NowTimeSteamp()))).ToObject<List<Essences>>();
        }

        /// <summary>
        /// 检查链接安全性
        /// </summary>
        /// <param name="URL">需要检测的链接</param>
        /// <returns>错误返回null,成功返回int</returns>
        public async Task<int> CheckURLSafely(string URL)
        {
            return (await SendCallObject(new BaseAPI("check_url_safely", new JObject { { "url", URL } }, "CheckURLSafely:" + Utils.NowTimeSteamp()))).Value<int>("level");
        }

        /// <summary>
        /// 获取在线机型
        /// </summary>
        /// <param name="ModelName">机型名字</param>
        /// <returns>在线机型列表</returns>
        public async Task<List<Model>> GetModels(string ModelName) {
            return (await SendCallObject(new BaseAPI("_get_model_show", new JObject { { "model", ModelName } }, "GetModelShow:" + Utils.NowTimeSteamp()))).ToObject<List<Model>>();
        }

        /// <summary>
        /// 设置在线机型
        /// </summary>
        /// <param name="ModelName">机型名字</param>
        /// <param name="ShowModelName">在线机型名字</param>
        public void SetModel(string ModelName, string ShowModelName)
        {
            JObject Params = new JObject
            {
                { "model", ModelName },
                { "model_show ", ShowModelName }
            };

            SendCallVoid(new BaseAPI("_set_model_show", Params, "SetModelShow:" + Utils.NowTimeSteamp()));
        }

        #region ==额外API==

        /// <summary>
        /// 获取指定群头像
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="SaveTo">保存路径-包括文件名(通常后缀名是jpg)</param>
        /// <returns>是否成功</returns>
        public bool DownloadGroupImage(long GroupID,string SaveTo) {
            try
            {
                new WebClient().DownloadFile($"https://p.qlogo.cn/gh/{GroupID}/{GroupID}/100", SaveTo);
                return true;
            }
            catch (Exception _) {
                return false;
            }
        }

        #endregion

        #endregion

        #region --私有函数--

        #region ==调用函数==

        private async Task<string> SendCallMessageID(BaseAPI Params)
        {
            SendCall(Params);

            string Result = "-1";
            await Task.Run(() => { Result = FeedbackMessageID(Params.UniqueCode); });
            return Result;
        }

        private async Task<JObject> SendCallObject(BaseAPI Params)
        {
            SendCall(Params);

            JObject Result = null;
            await Task.Run(() => { Result = FeedbackObject(Params.UniqueCode); });
            return Result;
        }

        private async Task<JArray> SendCallArray(BaseAPI Params)
        {
            SendCall(Params);

            JArray Result = null;
            await Task.Run(() => { Result = FeedbackArray(Params.UniqueCode); });
            return Result;
        }

        private void SendCall(BaseAPI Params)
        {
            Logger.Debug($"API调用:\n请求的接口:{Params.Action}\n请求的唯一码:{Params.UniqueCode}\n请求的参数:\n{Params.Params}");
            WebSockets.Send(Params);
            lock (TaskList) {
                if (!TaskList.TryGetValue(Params.UniqueCode, out _))
                {
                    TaskList.Add(Params.UniqueCode, "Sended");
                }
            }
        }

        private void SendCallVoid(BaseAPI Params)
        {
            WebSockets.Send(Params);
        }

        #endregion

        private static string FeedbackMessageID(string UniqueCode)
        {
            JToken FBJson = GetFeedback(UniqueCode);

            //判断返回
            if (FBJson["status"].ToString() == "ok")
            {
                return FBJson["data"]["message_id"].ToString();
            }
            return "-1";
        }

        private static JObject FeedbackObject(string UniqueCode)
        {
            JToken FBJson = GetFeedback(UniqueCode);

            //判断返回
            if (FBJson["status"].ToString() == "ok")
            {
                return JObject.Parse(FBJson["data"].ToString());
            }
            return null;
        }

        private static JArray FeedbackArray(string UniqueCode)
        {
            JToken FBJson = GetFeedback(UniqueCode);

            //判断返回
            if (FBJson["status"].ToString() == "ok")
            {
                return JArray.Parse(FBJson["data"].ToString());
            }
            return null;
        }

        private static JToken GetFeedback(string UniqueCode)
        {
            JToken FBJson = null;

            do
            {
                lock (TaskList)
                {
                    if (TaskList[UniqueCode].ToString() != "Sended")
                    {
                        FBJson = TaskList[UniqueCode];
                        TaskList.Remove(UniqueCode);
                        break;
                    }
                }
                Thread.Sleep(10);
            } while (FBJson == null);
            return FBJson;
        }

        internal static API Create(WebSocket WebSocket)
        {
            try
            {
                API Api = new API(WebSocket);
                Cache.Set($"API{AppDomain.CurrentDomain.Id}", Api);
                Task.Run(() =>
                {
                    Thread.Sleep(5000);
                    if (!WebSocket.IsCheckVersion)
                    {
                        Event.CheckVersion();
                    }
                });
                return Api;
            }
            catch (WebSocketException e)
            {
                Logger.Warning("警告，传入的WebSocket有误。错误代码: " + e.ErrorCode);
            }
            return null;
        }

        internal static void Destroy()
        {
            Cache.Remove($"API{AppDomain.CurrentDomain.Id}");
        }

        #endregion
    }
}
