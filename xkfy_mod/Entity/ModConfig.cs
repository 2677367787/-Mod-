using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class ModConfig
    {
        /// <summary>
        /// 名称名称
        /// </summary>
        [XmlElement]
        public string Name;

        /// <summary>
        /// 路径
        /// </summary>
        [XmlElement]
        public string Path;
    }
}
