using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("user")]
    public class ExportUserDto
    {
        [XmlAttribute("firs-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public ExportSoldProductDto[] SoldProducts { get; set; }
    }
}