using AuroraNavite.EventArgs;

namespace AuroraNavite
{
    /// <summary>
    /// 事件类
    /// </summary>
    public class Event
    {
        #region --公开函数--

        #region --元事件--

        /// <summary>
        /// 元事件 - 生命周期
        /// </summary>
        /// <param name="e">生命周期事件参数</param>
        public virtual void LifeCycle(LifeCycleArgs e) { }

        /// <summary>
        /// 元事件 - 心跳
        /// </summary>
        /// <param name="e">心跳事件参数</param>
        public virtual void HeartBeat(HeartBeatArgs e) { }

        #endregion

        #region --消息事件--

        /// <summary>
        /// 消息事件 - 私聊消息
        /// </summary>
        /// <param name="e">私聊消息参数</param>
        public virtual void PrivateMessage(PrivateMessageArgs e) { }

        /// <summary>
        /// 消息事件 - 群消息
        /// </summary>
        /// <param name="e">群消息参数</param>
        public virtual void GroupMessage(GroupMessageArgs e) { }

        #endregion

        #region --请求事件--

        /// <summary>
        /// 请求事件 - 好友请求
        /// </summary>
        /// <param name="e">好友请求参数</param>
        public virtual void FriendAddRequest(FriendAddRequsetArgs e) { }

        /// <summary>
        /// 请求事件 - 群请求
        /// </summary>
        /// <param name="e">群请求参数</param>
        public virtual void GroupAddRequest(GroupAddRequestArgs e) { }

        #endregion

        #endregion
    }
}
