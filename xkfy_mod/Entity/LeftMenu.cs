using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class LeftMenu
    {
        /// <summary>
        /// 菜单的显示名称
        /// </summary>
        [XmlElement]
        public string MenuText;

        /// <summary>
        /// 菜单Tag
        /// </summary>
        [XmlElement]
        public string MenuTag;

        /// <summary>
        /// 菜单分类名称
        /// </summary>
        [XmlElement]
        public string MenuName;
    }
}
