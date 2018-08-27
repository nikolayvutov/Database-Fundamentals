using System;
using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (SaleContext saleContext = new SaleContext())
            {
                saleContext.Database.Migrate();
            }
        }
    }
}