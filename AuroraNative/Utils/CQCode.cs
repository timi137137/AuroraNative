using AuroraNative.Exceptions;
using AuroraNative.Type;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AuroraNative
{
    /// <summary>
    /// CQ码处理 基类
    /// </summary>
    public static class CQCode
    {
        /// <summary>
        /// 将纯文本字符串转义成CQ码原本的样子
        /// </summary>
        /// <param name="String">待转义字符串</param>
        /// <returns>转义后字符串</returns>
        public static string ToCQCodeString(string String) {
            return String.Replace("&amp;", "&").Replace("&#91;", "[").Replace("&#93;", "]").Replace("&#44;", ",");
        }

        /// <summary>
        /// 将CQ码转义成纯文本
        /// </summary>
        /// <param name="String">待转义字符串</param>
        /// <returns>转义后字符串</returns>
        public static string ToPlainTextString(string String) {
            return String.Replace("&", "&amp;").Replace("[", "&#91;").Replace("]", "&#93;").Replace(",", "&#44;");
        }

        /// <summary>
        /// 把传入的消息段类型转成CQ码字符串
        /// </summary>
        /// <param name="MessageSegment">消息段类型</param>
        /// <returns>CQ码字符串</returns>
        public static string ToCQCode(CQMessageSegment MessageSegment) {
            if (MessageSegment == null) {
                throw new CQCodeException(-3,"消息段类型不可为空");
            }

            try {
                StringBuilder Builder = new StringBuilder();
                Builder.Append("[CQ:");
                Builder.Append(MessageSegment.Type);
                if (MessageSegment.Data != null) {
                    foreach (KeyValuePair<string, string> i in MessageSegment.Data)
                    {
                        Builder.Append($",{i.Key}={i.Value}");
                    }
                }
                Builder.Append("]");
                return Builder.ToString();
            } catch(Exception) {
                throw new CQCodeException(-4, "消息段转CQ码出现未知错误");
            }
        }

        /// <summary>
        /// 把传入的CQ码字符串转成消息段类型
        /// </summary>
        /// <param name="CQCodeString">CQ码字符串</param>
        /// <returns>返回一个消息段类型</returns>
        public static CQMessageSegment Parse(string CQCodeString) {
            if (!CQCodeString.Contains("[") || !CQCodeString.Contains("]")) {
                throw new CQCodeException(-1,"传入的CQ码字符串有误");
            }

            if (new Regex(@"(?<=\[)(.*?)(?=])").Match(CQCodeString).Success) {
                string AllCQCodeContent = new Regex(@"(?<=\[)(.*?)(?=])").Match(CQCodeString).Value;
                string Type = new Regex(@":(\w+)").Match(AllCQCodeContent).Value.Replace(":","");

                if (Type == null) {
                    throw new CQCodeException(-2, "传入的CQ码字符串有误");
                }

                Dictionary<string, string> Params = new Dictionary<string, string>();
                MatchCollection Matches = new Regex(@",([\w\-.]+?)=([^,\]]+)").Matches(AllCQCodeContent);

                for (int i = 0; i < Matches.Count; i++)
                {
                    string[] Cache = Matches[i].Value.Replace(",","").Split('=');
                    Params.Add(Cache[0],Cache[1]);
                }

                return new CQMessageSegment(Type,Params);
            }
            return null;
        }
    }
}
