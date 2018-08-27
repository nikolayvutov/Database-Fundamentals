using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("product")]
    public class ExportSoldProductDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}