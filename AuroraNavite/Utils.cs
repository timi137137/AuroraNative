using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace AuroraNavite
{
    /// <summary>
    /// 通用方法 类
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 通过 枚举Description 转为枚举
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="description">需要转换的Description</param>
        /// <returns>返回该枚举</returns>
        public static T GetEnumByDescription<T>(string description) where T : Enum
        {
            System.Reflection.FieldInfo[] fields = typeof(T).GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs.Length > 0 && (objs[0] as DescriptionAttribute).Description == description)
                {
                    return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException(string.Format("{0} 未能找到对应的枚举.", description), "Description");
        }

        /// <summary>
        /// 通过上报类型返回子类型键名
        /// </summary>
        /// <param name="Json">接到的事件</param>
        /// <returns>返回子类型的键名</returns>
        public static string GetChildTypeByPostType(JObject Json)
        {
            if (Json.TryGetValue("post_type", out JToken Token))
            {
                switch ((string)Token)
                {
                    case "meta_event":
                        return "meta_event_type";
                    case "message":
                        return "message_type";
                    case "request":
                        return "request_type";
                    case "notice":
                        return "notice_type";
                    default:
                        break;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取现在时间戳
        /// </summary>
        /// <returns>返回字符串</returns>
        public static string NowTimeSteamp()
        {
            return ((DateTime.Now.Ticks - TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local).Ticks) / 10000).ToString();
        }
    }
}
