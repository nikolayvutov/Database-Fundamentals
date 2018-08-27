using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductShop.Models
{
    public class User
    {
        public User()
        {
            this.SoldProducts = new HashSet<Product>();
            this.BoughtProducts = new HashSet<Product>();
        }
        
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }
        
        [InverseProperty("Buyer")]
        public ICollection<Product> SoldProducts { get; set; }
        
        [InverseProperty("Seller")]
        public ICollection<Product> BoughtProducts { get; set; }
    }
}