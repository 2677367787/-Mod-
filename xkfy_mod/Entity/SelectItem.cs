using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace xkfy_mod.Entity
{
    public class SelectItem
    {
        [XmlElement]
        public string Type;

        [XmlElement]
        public string ChildType;

        [XmlElement]
        public string Text;

        [XmlElement]
        public string Value;
    }
}
