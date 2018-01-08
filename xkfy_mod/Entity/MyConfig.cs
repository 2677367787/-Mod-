using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class MyConfig
    {
        /// <summary>
        /// 主表名称
        /// </summary>
        [XmlElement]
        public string MainDtName { get; set; }

        /// <summary>
        /// 明细表名称
        /// </summary>
        [XmlElement]
        public string DetailDtName { get; set; }

        /// <summary>
        /// 文件类型,1，有主表，有明细表; 2，只有一个表; 3，只有一个表，但是列重复
        /// </summary>
        [XmlElement]
        public string DtType { get; set; }

        /// <summary>
        /// 文件全称
        /// </summary>
        [XmlElement]
        public string TxtName { get; set; }

        /// <summary>
        /// 类型,用于加载节点
        /// </summary>
        [XmlElement]
        public string Classify { get; set; }

        /// <summary>
        /// 备注说明,TXT文件的中文名称
        /// </summary>
        [XmlElement]
        public string Notes { get; set; }

        /// <summary>
        /// DtType = 1 时有效，表的基本信息列截止到哪列
        /// </summary>
        [XmlElement]
        public int BasicCritical { get; set; }

        /// <summary>
        /// DtType = 1 时有效，表的重复信息列从哪列开始
        /// </summary>
        [XmlElement]
        public int EffectCritical { get; set; } 

        /// <summary>
        /// 编辑窗口名称
        /// </summary>
        [XmlElement]
        public string EditFormName { get; set; }

        /// <summary>
        /// 是否缓存
        /// </summary>
        [XmlElement]
        public string IsCache { get; set; }
    }
}
