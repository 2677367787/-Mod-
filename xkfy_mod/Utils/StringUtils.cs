using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace xkfy_mod.Utils
{
    public class StringUtils
    {
        #region 返回中文字符数量
        /// <summary>
        /// 返回中文数量
        /// </summary>
        /// <param name="textboxTextStr">输入的字符串</param>
        /// <returns></returns>
        public static int GetCnLength(string textboxTextStr)
        {
            return textboxTextStr.Count(t => t >= 0x3000 && t <= 0x9FFF);
        }

        #endregion

        public static bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsIgnoreCase(string source, string toCheck)
        {
            return Contains(source, toCheck, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 格式化处理工具列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string FormatToolColumn(string columnName)
        {
            return columnName.Replace("#", "");
        }

        #region 使用正则表达式截取##中的各类ID
        /// <summary>
        /// 使用正则表达式截取##中的各类ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetId(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\#)[^\[\]]+(?=\#)");//正则
            return m.Count > 0 ? m[0].Value : "";
        }

        /// <summary>
        /// 使用正则表达式截取[]中的各类ID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetRdoValue(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\[)[^\[\]]+(?=\])");//正则
            return m.Count > 0 ? m[0].Value : "";
        }

        /// <summary>
        /// 使用正则表达式截取{}中的值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetRegexValue(string str)
        {
            MatchCollection m = Regex.Matches(str, @"(?<=\{)[^\[\]]+(?=\})");//正则
            return m.Count > 0 ? m[0].Value : "";
        }
        #endregion
    }
}
