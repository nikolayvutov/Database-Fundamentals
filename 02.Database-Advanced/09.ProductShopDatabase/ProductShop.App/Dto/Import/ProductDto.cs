using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop.App.Dto
{
    [XmlType("product")]
    public class ProductDto
    {
        [XmlElement("name")]
        [MinLength(3)]
        public string Name { get; set; }
        
        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}