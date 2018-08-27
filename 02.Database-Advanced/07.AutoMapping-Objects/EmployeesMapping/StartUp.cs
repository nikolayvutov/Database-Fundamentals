using System;
using AutoMapper;
using EmployeesMapping.Core;
using EmployeesMapping.Core.Contracts;
using EmployeesMapping.Core.Controlers;
using EmployeesMapping.Data;
using EmployeesMapping.Services;
using EmployeesMapping.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesMapping
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var service = ConfigureService();
            IEngine engine = new Engine(service);
            
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeeContext>(opts => opts.UseSqlServer
                (Configuration.ConnectionString));

            serviceCollection.AddAutoMapper(conf => conf.AddProfile<EmployeeProfile>());
            
            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<IEmployeeController, EmployeeController>();

            serviceCollection.AddTransient<IManagerController, ManagerController>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}