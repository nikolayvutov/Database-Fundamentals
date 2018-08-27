using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("sold-products")]
    public class SoldProductExportDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
        
        [XmlElement("product")]
        public ProductExportDto[] Products { get; set; }
    }
}