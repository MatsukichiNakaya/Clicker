using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Clicker
{
    [Serializable]
    [XmlRoot("MacroSet")]
    public class MacroSet
    {
        [XmlElement(ElementName="Macros")]
        public List<Macro> Macros { get; set; }
    }

    [Serializable]
    public class Macro
    {
        [XmlElement(ElementName = "Name", IsNullable = false)]
        public String Name { get; set; }

        [XmlElement(ElementName="KeyCode", IsNullable=false)]
        public Int32 KeyCode { get; set; }

        [XmlElement(ElementName="MacroCommands", IsNullable=false)]
        public List<Command> MacroCommands { get; set; }
    }

    [Serializable]
    public class Command
    {
        [XmlAttribute(AttributeName = "Type")]
        public Int32 CommandType { get; set; }

        [XmlElement(ElementName="X")]
        public Int32 X { get; set; }

        [XmlElement(ElementName="Y")]
        public Int32 Y { get; set; }

        [XmlElement(ElementName = "Delay")]
        public Int32 Delay { get; set; }
    }
}
