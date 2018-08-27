using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("user")]
    public class ExportChildUserDto
    {
        [XmlAttribute("firs-name")]
        public string FirstName { get; set; }
        
        [XmlAttribute("last-name")]
        public string LastName { get; set; }
        
        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public SoldProductExportDto SoldProducts { get; set; }
    }
}