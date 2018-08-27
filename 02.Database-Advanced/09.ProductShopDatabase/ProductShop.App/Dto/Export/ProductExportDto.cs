﻿using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("product")]
    public class ProductExportDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    
        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}