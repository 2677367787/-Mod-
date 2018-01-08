using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class AppConfig
    {
        /// <summary>
        /// 创建时选择的路径
        /// </summary>
        [XmlElement]
        public string CreatePath { get; set; }

        /// <summary>
        /// 选择时选择的路径
        /// </summary>
        [XmlElement]
        public string SelectPath { get; set; }

        /// <summary>
        /// 游戏安装路径
        /// </summary>
        [XmlElement]
        public string GameInstallPath { get; set; }

    
    }
}
