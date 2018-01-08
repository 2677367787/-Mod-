using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class EventData
    {
        /// <summary>
        /// 事件ID
        /// </summary>
        [XmlElement]
        public string EventId { get; set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        [XmlElement]
        public string EventName { get; set; }

        /// <summary>
        /// 前置事件
        /// </summary>
        [XmlElement]
        public string ParantId { get; set; }

        /// <summary>
        /// 事件回合
        /// </summary>
        [XmlElement]
        public string Round { get; set; }
    }
}
