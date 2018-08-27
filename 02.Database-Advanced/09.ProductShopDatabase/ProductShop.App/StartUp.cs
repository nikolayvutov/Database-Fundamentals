using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using ProductShop.App.Dto;
using ProductShop.App.Dto.Export;
using ProductShop.Data;
using ProductShop.Models;

namespace ProductShop.App
{
    public class StartUp
    {
        private static ProductShopContext _context = new ProductShopContext();
        
        static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
// Import From XML

            ImportUsers(mapper);
            ImportProducts(mapper);
            ImportCategories(mapper);
            GenerateCategoryForProducts();
            
//Export to XML
            ProductsInRange();
            UsersSoldProducts();     
            CategoriesByProductCount();
            UsersAndProducts();
            
        }

        private static void GenerateCategoryForProducts()
        {
            var categoryProductList = new List<CategoryProduct>();
            
            for (int productId = 1; productId <= 200; productId++)
            {
                var categoryId = new Random().Next(1, 12);

                var categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId,
                };
                
                categoryProductList.Add(categoryProduct);
            }
            
            _context.CategoryProducts.AddRange(categoryProductList);
            _context.SaveChanges();
        }
        
        private static void ImportUsers(IMapper mapper)
        {
            var xmlString = File.ReadAllText("XML/ImportXML/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), new XmlRootAttribute("users"));
            var deserializedUser = (UserDto[])serializer.Deserialize(new StringReader(xmlString));
            
            var validUsers = new List<User>();
            
            foreach (var userDto in deserializedUser)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                var user = mapper.Map<User>(userDto);
                validUsers.Add(user);
            }
            
            _context.Users.AddRange(validUsers);
            _context.SaveChanges();

        }

        private static void ImportProducts(IMapper mapper)
        {
            var xmlString = File.ReadAllText("XML/ImportXML/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), new XmlRootAttribute("products"));
            var deserializedProducts = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            var validProducts = new List<Product>();
            var counter = 0;
            foreach (var productDto in deserializedProducts)
            {

                if (!IsValid(productDto))
                {
                    continue;
                }
                var product = mapper.Map<Product>(productDto);
                
                var buyerId = new Random().Next(1, 30);
                var sellerId = new Random().Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                validProducts.Add(product);

                counter++;
            }

            _context.Products.AddRange(validProducts);
            _context.SaveChanges();
        }

        private static void ImportCategories(IMapper mapper)
        {
            var xmlString = File.ReadAllText("XML/ImportXML/categories.xml");

            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("categories"));
            var deserializedCategories = (CategoryDto[])serializer.Deserialize(new StringReader(xmlString));

            var validCategories = new List<Category>();
            
            foreach (var categoryDto in deserializedCategories)
            {                
                if (!IsValid(categoryDto))
                {
                    continue;    
                }
                
                var category = mapper.Map<Category>(categoryDto);
                validCategories.Add(category);
            }
            
            _context.Categories.AddRange(validCategories);
            _context.SaveChanges();
        }

        private static void ProductsInRange()
        {
            var products = _context.Products.Where(p => p.Price >= 1_000 && p.Price <= 2_000 && p.Buyer != null)
                .OrderBy(p => p.Price)
                .Select(p => new ExportProductDto {
                    Name = p.Name,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName ?? p.Buyer.LastName,
                    Price = p.Price,
                }).ToArray();
 
            var sb = new StringBuilder();
            
            var xmlNameSpaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            
            var serializer = new XmlSerializer(typeof(ExportProductDto[]), new XmlRootAttribute("products"));
            serializer.Serialize(new StringWriter(sb), products, xmlNameSpaces);
            
            File.WriteAllText("XML/ExportXML/products-in-range.xml", sb.ToString(), Encoding.UTF8);
        }

        private static void UsersSoldProducts()
        {
            var users = _context.Users.Where(u => u.SoldProducts.Count > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new ExportUserDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.SoldProducts.Select(p => new ExportSoldProductDto
                    {
                        Name = p.Name,
                        Price = p.Price
                    }).ToArray()
                }).ToArray();
            
            
            var sb = new StringBuilder();
            
            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            var serializer = new XmlSerializer(typeof(ExportUserDto[]), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);
            
            File.WriteAllText("XML/ExportXML/users-sold-products.xml", sb.ToString(), Encoding.UTF8);
        }

        private static void CategoriesByProductCount()
        {
            var categories = _context.Categories
                .Select(c => new ExportCategoryByProductDto
                {
                    Name = c.Name,
                    ProductsCount = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Select(p => p.Product.Price).DefaultIfEmpty(0).Average(),
                    TotalRevenue = c.CategoryProducts.Sum(p => p.Product.Price),
                })
                .OrderByDescending(p => p.ProductsCount)
                .ToArray();
            
            var sb = new StringBuilder();
            
            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            var serializer = new XmlSerializer(typeof(ExportCategoryByProductDto[]), new XmlRootAttribute("categories"));
            serializer.Serialize(new StringWriter(sb), categories, xmlNamespaces);
            
            File.WriteAllText("XML/ExportXML/categories-by-products.xml", sb.ToString(), Encoding.UTF8);
            
        }

        private static void UsersAndProducts()
        {
            var users = new ExportMainUserDto
            {
                Count = _context.Users.Count(),
                User = _context.Users
                        .Where(u => u.SoldProducts.Count >= 1)
                    .Select(u => new ExportChildUserDto
                    {
                        Age = u.Age,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        SoldProducts =new SoldProductExportDto
                            {
                                Count = u.SoldProducts.Count(),
                                Products = u.SoldProducts.Select(p => new ProductExportDto
                                    {
                                        Name = p.Name,
                                        Price = p.Price,
                                    })
                                    .ToArray()
                            }
                    })
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .ThenBy(u => u.LastName)
                    .ToArray()
            };
            
            var sb = new StringBuilder();
            
            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            var serializer = new XmlSerializer(typeof(ExportMainUserDto), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(sb), users, xmlNamespaces);
            
            File.WriteAllText("XML/ExportXML/users-and-products.xml", sb.ToString(), Encoding.UTF8);   
        }
        
        private static bool IsValid(object obj)
        {
            
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationresult = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationresult, true);
            return isValid;
        }
    }
}