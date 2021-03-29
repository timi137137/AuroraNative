namespace AuroraNative.Enum
{
    /// <summary>
    /// 通知事件 枚举
    /// </summary>
    public enum NoticeType
    {
        /// <summary>
        /// 群文件上传
        /// </summary>
        group_upload = 1,
        /// <summary>
        /// 群管理员变动
        /// </summary>
        group_admin = 2,
        /// <summary>
        /// 群成员减少
        /// </summary>
        group_decrease = 3,
        /// <summary>
        /// 群成员增加
        /// </summary>
        group_increase = 4,
        /// <summary>
        /// 群禁言
        /// </summary>
        group_ban = 5,
        /// <summary>
        /// 好友添加
        /// </summary>
        friend_add = 6,
        /// <summary>
        /// 群消息撤回
        /// </summary>
        group_recall = 7,
        /// <summary>
        /// 好友消息撤回
        /// </summary>
        friend_recall = 8,
        /// <summary>
        /// 通知
        /// </summary>
        notify = 9,
        /// <summary>
        /// 群成员名片更新
        /// </summary>
        group_card = 10,
        /// <summary>
        /// 接收到离线文件
        /// </summary>
        offline_file = 11,
        /// <summary>
        /// 其他客户端在线状态变更
        /// </summary>
        client_status = 12,
        /// <summary>
        /// 精华消息
        /// </summary>
        essence = 13
    }
}
