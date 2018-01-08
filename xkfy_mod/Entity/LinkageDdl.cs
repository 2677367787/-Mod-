/***************************************************** 
** 命名空间：xkfy_mod.Entity
** 文件名称：LinkageDdl
** 内容简述： 
** 版　　本：V1.0 
** 作　　者： 
** 创建日期：2018/1/1 15:57:51
** 修改记录： 
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    [Serializable]
    public class LinkageDdl
    {
        /// <summary>
        /// key值
        /// </summary>
        [XmlElement]
        public string Key { get; set; }
        /// <summary>
        /// key值
        /// </summary>
        [XmlElement]
        public string DataKey { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        [XmlElement]
        public string Value { get; set; }
    }
}
