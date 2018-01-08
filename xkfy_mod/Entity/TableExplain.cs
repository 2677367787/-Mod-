using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class TableExplain
    {
        /// <summary>
        /// 列名
        /// </summary>
        [XmlElement]
        public string ToolColumn { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [XmlElement]
        public string Column { get; set; }

        /// <summary>
        /// 显示的文字
        /// </summary>
        [XmlElement]
        public string Text { get; set; }

        /// <summary>
        /// 移动到Label显示的详细注释
        /// </summary>
        [XmlElement]
        public string Explain { get; set; }

        /// <summary>
        /// 是否作为查询列
        /// </summary>
        [XmlElement]
        public int IsSelColumn { get; set; }

        /// <summary>
        /// 自动生成控件时,控件宽度
        /// </summary>
        [XmlElement]
        public int Width { get; set; }

        /// <summary>
        /// 自动生成控件时,控件高度
        /// </summary>
        [XmlElement]
        public int Height { get; set; }

        /// <summary>
        /// 是否是下拉框,1是, 0否
        /// </summary>
        [XmlElement]
        public int IsDropDownList { get; set; }

        /// <summary>
        /// 加载时唯一键值名称
        /// </summary>
        [XmlElement]
        public string DataKey { get; set; }

        /// <summary>
        /// 下拉框的数据源文件路径
        /// </summary>
        [XmlElement]
        public string DataSourcePath { get; set; }　
    }
}
