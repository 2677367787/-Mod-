using System;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    [Serializable]
    public class DicConfig
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
 
    }
}
