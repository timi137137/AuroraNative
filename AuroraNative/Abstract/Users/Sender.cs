using Newtonsoft.Json;

namespace AuroraNative.Type.Users
{
    /// <summary>
    /// 提供用于描述发送者信息的基础类, 该类是抽象的
    /// </summary>
    public sealed class Sender
    {
        #region --属性--

        /// <summary>
        /// 发送者 QQ 号
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserID { get; private set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; private set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public string Sex { get; private set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [JsonProperty(PropertyName = "age")]
        public int Age { get; private set; }

        /// <summary>
        /// 群名片／备注
        /// </summary>
        [JsonProperty(PropertyName = "card")]
        public string Card { get; private set; }

        /// <summary>
        /// 地区
        /// </summary>
        [JsonProperty(PropertyName = "area")]
        public string Area { get; private set; }

        /// <summary>
        /// 成员等级
        /// </summary>
        [JsonProperty(PropertyName = "level")]
        public string Level { get; private set; }

        /// <summary>
        /// 角色
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; private set; }

        /// <summary>
        /// 专属头衔
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; private set; }

        #endregion

        #region --构造函数--

        /// <summary>
        /// 初始化 <see cref="Sender"/> 类的新实例
        /// </summary>
        /// <param name="UserID">发送者 QQ 号</param>
        /// <param name="NickName">昵称</param>
        /// <param name="Card">群名片／备注</param>
        /// <param name="Sex">性别</param>
        /// <param name="Age">年龄</param>
        /// <param name="Area">地区</param>
        /// <param name="Level">成员等级</param>
        /// <param name="Role">角色</param>
        /// <param name="Title">专属头衔</param>
        public Sender(long UserID, string NickName, string Card, string Sex, int Age, string Area, string Level, string Role, string Title)
        {
            this.UserID = UserID;
            this.NickName = NickName;
            this.Sex = Sex;
            this.Age = Age;
            this.Card = Card;
            this.Area = Area;
            this.Level = Level;
            this.Role = Role;
            this.Title = Title;
        }

        #endregion
    }
}
