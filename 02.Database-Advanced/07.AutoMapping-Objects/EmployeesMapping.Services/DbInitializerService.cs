using System;
using EmployeesMapping.Data;
using EmployeesMapping.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeesMapping.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly EmployeeContext context;
        
        public DbInitializerService(EmployeeContext context)
        {
            this.context = context;
        }
        
        public void InitializeDatabase()
        {
            this.context.Database.Migrate();
        }
    }
}