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

        #endregion
    }
}
