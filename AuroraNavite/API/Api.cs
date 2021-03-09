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
            JObject Params = new JObject() {
                { "message", Message},
                { "auto_escape", AutoEscape}
            };

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
        /// <param name="QID">QQ号</param>
        /// <param name="Enable">是否设置为管理员<para>默认:true</para></param>
        public void SetGroupAdmin(long GroupID, long QID, bool Enable = true)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
                { "group_id", GroupID },
                { "enable", Enable }
            };

            SendCallVoid(new BaseAPI("set_group_admin", Params, "SetGroupAdmin:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 设置群名片
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QID">QQ号</param>
        /// <param name="Card">群名片内容<para>默认:null（删除群名片）</para></param>
        public void SetGroupCard(long GroupID, long QID, string Card = null)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
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
        /// 设置群组专属头衔
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QID">QQ号</param>
        /// <param name="SpecialTitle">群名片内容<para>默认:null（删除群名片）</para></param>
        /// <param name="Duration">专属头衔有效期, 单位秒, 不过此项似乎没有效果, 可能是只有某些特殊的时间长度有效, 有待测试<para>默认:-1(永久)</para></param>
        public void SetGroupSpecialTitle(long GroupID, long QID, string SpecialTitle = null, int Duration = -1)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
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
        public async Task<JObject> GetLoginInfo()
        {
            return await SendCallObject(new BaseAPI("get_login_info", null, "GetLoginInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取陌生人信息
        /// </summary>
        /// <param name="QID">QQ号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetStrangerInfo(long QID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "user_id", QID },
                { "no_cache ", Cache }
            };

            return await SendCallObject(new BaseAPI("get_stranger_info", Params, "GetStrangerInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetFriendList()
        {
            return await SendCallObject(new BaseAPI("get_friend_list", null, "GetFriendList:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupInfo(long GroupID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "no_cache ", Cache }
            };

            return await SendCallObject(new BaseAPI("get_group_info", Params, "GetGroupInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupList()
        {
            return await SendCallObject(new BaseAPI("get_group_list", null, "GetGroupList:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="QID">QQ号</param>
        /// <param name="Cache">是否使用缓存，使用缓存响应快但是可能更新不及时<para>默认:false</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupMemberInfo(long GroupID, long QID, bool Cache = false)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "user_id", QID },
                { "no_cache ", Cache }
            };

            return await SendCallObject(new BaseAPI("get_group_member_info", Params, "GetGroupMemberInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupMemberList(long GroupID)
        {
            return await SendCallObject(new BaseAPI("get_group_member_info", new JObject { { "group_id", GroupID } }, "GetGroupMemberInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群荣耀信息
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="Type">要获取的群荣誉类型<para>talkative/performer/legend/strong_newbie/emotion/all</para></param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupHonorInfo(long GroupID, string Type)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "type ", Type }
            };

            return await SendCallObject(new BaseAPI("get_group_honor_info", Params, "GetGroupHonorInfo:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> GetVersionInfo()
        {
            return await SendCallObject(new BaseAPI("get_version_info", null, "GetVersionInfo:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> OCRImage(string Image)
        {
            return await SendCallObject(new BaseAPI("ocr_image", new JObject { { "image", Image } }, "OCRImage:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群系统消息
        /// </summary>
        /// <returns>错误或不存在任何消息返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupSystemMsg()
        {
            return await SendCallObject(new BaseAPI("get_group_system_msg", null, "GetGroupSystemMsg:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> GetGroupFileSystemInfo(long GroupID)
        {
            return await SendCallObject(new BaseAPI("get_group_file_system_info", new JObject { { "group_id", GroupID } }, "GetGroupFileSystemInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群根目录文件列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupRootFiles(long GroupID)
        {
            return await SendCallObject(new BaseAPI("get_group_root_files", new JObject { { "group_id", GroupID } }, "GetGroupRootFiles:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群子目录文件列表
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <param name="FolderID">文件夹ID</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupFilesByFolder(long GroupID, string FolderID)
        {
            JObject Params = new JObject
            {
                { "group_id", GroupID },
                { "folder_id ", FolderID }
            };

            return await SendCallObject(new BaseAPI("get_group_files_by_folder", Params, "GetGroupFilesByFolder:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> GetStatus()
        {
            return await SendCallObject(new BaseAPI("get_group_info", null, "GetGroupInfo:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群 @全体成员 剩余次数
        /// </summary>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupAtAllRemain(long GroupID)
        {
            return await SendCallObject(new BaseAPI("get_group_at_all_remain", new JObject { { "group_id", GroupID } }, "GetGroupAtAllRemain:" + Utils.NowTimeSteamp()));
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
        /// <param name="QID">QQ号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetVIPInfo(long QID)
        {
            return await SendCallObject(new BaseAPI("_get_vip_info", new JObject { { "user_id", QID } }, "GetGroupAtAllRemain:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> GetOnlineClients(bool Cache)
        {
            return await SendCallObject(new BaseAPI("get_online_clients", new JObject { { "no_cache ", Cache } }, "GetOnlineClients:" + Utils.NowTimeSteamp()));
        }

        /// <summary>
        /// 获取群消息历史记录
        /// </summary>
        /// <param name="MessageSeq">起始消息序号</param>
        /// <param name="GroupID">群号</param>
        /// <returns>错误返回null,成功返回JObject</returns>
        public async Task<JObject> GetGroupMsgHistory(long MessageSeq, long GroupID)
        {
            JObject Params = new JObject
            {
                { "message_seq", MessageSeq },
                { "group_id ", GroupID }
            };

            return await SendCallObject(new BaseAPI("get_group_msg_history", Params, "GetGroupMsgHistory:" + Utils.NowTimeSteamp()));
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
        public async Task<JObject> GetEssenceMsgList(long GroupID)
        {
            return await SendCallObject(new BaseAPI("get_essence_msg_list", new JObject { { "group_id", GroupID } }, "GetEssenceMsgList:" + Utils.NowTimeSteamp()));
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
