/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：Explain
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2017/12/25 22:40:33
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class RowFormat
    {
        /// <summary>
        /// key值
        /// </summary>
        [XmlElement]
        public string Key { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [XmlElement]
        public string Value { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [XmlElement]
        public string Format { get; set; }
    }
}
