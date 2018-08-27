using System.Xml.Serialization;
using ProductShop.Models;

namespace ProductShop.App.Dto.Export
{
    [XmlType("product")]
    public class ExportProductDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("buyer")]
        public string Buyer { get; set; }
    }
}