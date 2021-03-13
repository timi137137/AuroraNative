using AuroraNative.EventArgs;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AuroraNative
{
    /// <summary>
    /// Enum和Description特性互转 转换器
    /// </summary>
    internal class EnumDescriptionConverter : JsonConverter
    {
        /// <summary>
        /// 当属性的值为枚举类型时才使用转换器
        /// </summary>
        /// <param name="objectType">目标类型</param>
        /// <returns>返回布尔值</returns>
        public override bool CanConvert(Type objectType) => objectType == typeof(Enum);

        /// <summary>
        /// 获取枚举的描述值
        /// </summary>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                writer.WriteValue("");
                return;
            }

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                writer.WriteValue("");
                return;
            }

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            writer.WriteValue(attributes.Length > 0 ? attributes[0].Description : "");
        }

        /// <summary>
        /// 通过Description获取枚举值
        /// </summary>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            foreach (FieldInfo field in objectType.GetFields())
            {
                object[] objects = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objects.Any(item => (item as DescriptionAttribute)?.Description == reader.Value?.ToString()))
                {
                    return Convert.ChangeType(field.GetValue(-1), objectType);
                }
            }

            return CQCodeType.Unknown;
        }
    }
}
