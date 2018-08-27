using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("users")]
    public class ExportMainUserDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
        
        [XmlElement("user")]
        public ExportChildUserDto[] User { get; set; }
    }
}