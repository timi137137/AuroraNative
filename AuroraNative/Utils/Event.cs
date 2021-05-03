using AuroraNative.Attributes;
using AuroraNative.Enum;
using AuroraNative.EventArgs;
using AuroraNative.WebSockets;
using Newtonsoft.Json.Linq;
using System;

namespace AuroraNative
{
    /// <summary>
    /// 事件类
    /// </summary>
    public abstract class Event
    {
        #region --公开函数--

        #region --元事件--

        /// <summary>
        /// 元事件 - 生命周期
        /// </summary>
        /// <param name="e">生命周期事件参数</param>
        [PostType(PostType.MetaEvent)]
        [MetaEventType(MetaEventType.lifecycle)]
        public virtual void LifeCycle(LifeCycleArgs e) { }

        /// <summary>
        /// 元事件 - 心跳
        /// </summary>
        /// <param name="e">心跳事件参数</param>
        [PostType(PostType.MetaEvent)]
        [MetaEventType(MetaEventType.heartbeat)]
        public virtual void HeartBeat(HeartBeatArgs e) { }

        #endregion

        #region --消息事件--

        /// <summary>
        /// 消息事件 - 私聊消息
        /// </summary>
        /// <param name="e">私聊消息参数</param>
        [PostType(PostType.Message)]
        [MessageType(MessageType.@private)]
        public virtual void PrivateMessage(PrivateMessageArgs e) { }

        /// <summary>
        /// 消息事件 - 群消息
        /// </summary>
        /// <param name="e">群消息参数</param>
        [PostType(PostType.Message)]
        [MessageType(MessageType.group)]
        public virtual void GroupMessage(GroupMessageArgs e) { }

        #endregion

        #region --请求事件--

        /// <summary>
        /// 请求事件 - 好友请求
        /// </summary>
        /// <param name="e">好友请求参数</param>
        [PostType(PostType.Request)]
        [RequestType(RequestType.friend)]
        public virtual void FriendAddRequest(FriendAddRequsetArgs e) { }

        /// <summary>
        /// 请求事件 - 群请求
        /// </summary>
        /// <param name="e">群请求参数</param>
        [PostType(PostType.Request)]
        [RequestType(RequestType.group)]
        public virtual void GroupAddRequest(GroupAddRequestArgs e) { }

        #endregion

        #region --通知事件--

        /// <summary>
        /// 通知事件 - 群文件上传
        /// </summary>
        /// <param name="e">群文件上传参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_upload)]
        public virtual void GroupUpload(GroupUploadArgs e) { }

        /// <summary>
        /// 通知事件 - 群管理员变动
        /// </summary>
        /// <param name="e">群管理员变动参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_admin)]
        public virtual void GroupManageChange(GroupManageChangeArgs e) { }

        /// <summary>
        /// 通知事件 - 群成员减少
        /// </summary>
        /// <param name="e">群成员减少参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_decrease)]
        public virtual void GroupMemberDecrease(GroupMemberDecreaseArgs e) { }

        /// <summary>
        /// 通知事件 - 群成员增加
        /// </summary>
        /// <param name="e">群成员增加参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_increase)]
        public virtual void GroupMemberIncrease(GroupMemberIncreaseArgs e) { }

        /// <summary>
        /// 通知事件 - 群禁言
        /// </summary>
        /// <param name="e">群禁言参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_ban)]
        public virtual void GroupBanSpeak(GroupBanSpeakArgs e) { }

        /// <summary>
        /// 通知事件 - 好友添加
        /// </summary>
        /// <param name="e">好友添加参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.friend_add)]
        public virtual void FriendAdd(FriendAddArgs e) { }

        /// <summary>
        /// 通知事件 - 群消息撤回
        /// </summary>
        /// <param name="e">群消息撤回参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_recall)]
        public virtual void GroupMessageRecall(GroupMessageRecallArgs e) { }

        /// <summary>
        /// 通知事件 - 好友消息撤回
        /// </summary>
        /// <param name="e">好友消息撤回参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.friend_recall)]
        public virtual void PrivateMessageRecall(PrivateMessageRecallArgs e) { }

        /// <summary>
        /// 通知事件 - 群内戳一戳
        /// </summary>
        /// <param name="e">群内戳一戳参数</param>
        [PostType(PostType.Notice)]
        public virtual void GroupPoke(GroupPokeArgs e) { }

        /// <summary>
        /// 通知事件 - 好友戳一戳
        /// </summary>
        /// <param name="e">群内戳一戳参数</param>
        [PostType(PostType.Notice)]
        public virtual void PrivatePoke(PrivatePokeArgs e) { }

        /// <summary>
        /// 通知事件 - 群红包运气王提示
        /// </summary>
        /// <param name="e">群红包运气王提示参数</param>
        [PostType(PostType.Notice)]
        public virtual void GroupRedPoketLuckyKing(GroupRedPoketLuckyKingArgs e) { }

        /// <summary>
        /// 通知事件 - 群成员荣誉变更提示
        /// </summary>
        /// <param name="e">群成员荣誉变更提示参数</param>
        [PostType(PostType.Notice)]
        public virtual void GroupMemberHonorChange(GroupMemberHonorChangeArgs e) { }

        /// <summary>
        /// 通知事件 - 群成员名片更新（核验）
        /// </summary>
        /// <param name="e">群成员名片更新（核验）参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.group_card)]
        public virtual void GroupCardVerify(GroupCardVerifyArgs e) { }

        /// <summary>
        /// 通知事件 - 接收到离线文件
        /// </summary>
        /// <param name="e">接收到离线文件参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.offline_file)]
        public virtual void GetOfflineFile(GetOfflineFileArgs e) { }

        /// <summary>
        /// 通知事件 - 其他客户端在线状态变更
        /// </summary>
        /// <param name="e">其他客户端在线状态变更参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.client_status)]
        public virtual void ClientStatusChange(ClientStatusChangeArgs e) { }

        /// <summary>
        /// 通知事件 - 精华消息变更
        /// </summary>
        /// <param name="e">精华消息变更参数</param>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.essence)]
        public virtual void EssenceMessageChange(EssenceMessageChangeArgs e) { }

        /// <summary>
        /// 请勿使用，用于子事件分发
        /// </summary>
        [PostType(PostType.Notice)]
        [NoticeType(NoticeType.notify)]
        public void RouterNotify(JObject Json)
        {
            if (Json.TryGetValue("sub_type", out JToken Token))
            {
                switch ((string)Token)
                {
                    case "poke":
                        if (Json.TryGetValue("group_id", out _))
                        {
                            GroupPoke(Json.ToObject<GroupPokeArgs>());
                        }
                        else
                        {
                            PrivatePoke(Json.ToObject<PrivatePokeArgs>());
                        }
                        break;
                    case "lucky_king":
                        GroupRedPoketLuckyKing(Json.ToObject<GroupRedPoketLuckyKingArgs>());
                        break;
                    case "honor":
                        GroupMemberHonorChange(Json.ToObject<GroupMemberHonorChangeArgs>());
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region --私有函数--

        internal static async void CheckVersion()
        {
            Logger.Debug("开始检查 go-cqhttp 版本是否符合最低版本...");
            if ((await API.CurrentApi.GetVersionInfo()).TryGetValue("AppVersion", out object Version) && new Version(Version.ToString().Substring(1, Version.ToString().IndexOf('-') - 1)) < WebSocket.DependencyVersion)
            {
                Logger.Warning($"框架最低依赖版本与 go-cqhttp 不符！请检查是否为最新的框架或符合的 go-cqhttp\ngo-cqhttp版本:{Version}\n框架最低依赖版本:v{WebSocket.DependencyVersion}");
            }
            else
            {
                Logger.Debug("go-cqhttp 版本符合最低版本!");
            }
            WebSocket.IsCheckVersion = true;
        }

        #endregion
    }
}
