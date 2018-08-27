﻿using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("category")]
    public class ExportCategoryByProductDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        [XmlElement("products-count")]
        public int ProductsCount { get; set; }
        
        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }
        
        [XmlElement("total-revenue")]
        public decimal TotalRevenue { get; set; }
    }
}