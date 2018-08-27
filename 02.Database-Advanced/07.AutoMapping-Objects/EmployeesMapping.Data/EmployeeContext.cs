using EmployeesMapping.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesMapping.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {   
        }

        public EmployeeContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(x => x.Manager)
                    .WithMany(x => x.ManagerEmployees)
                    .HasForeignKey(x => x.ManagerId);
            });

        }
    }
}