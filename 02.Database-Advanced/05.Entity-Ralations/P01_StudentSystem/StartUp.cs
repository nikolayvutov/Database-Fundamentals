using System;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemContext())
            {
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
            }
        }
    }
}