﻿using System;
using System.Linq;
using System.Reflection;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;
        
        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public string Read(string[] input)
        {
            string commandName = input[0] + "Command";

            string[] args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);


            if (type == null)
            {
                throw new ArgumentException("Invalid command!");
            }
            
            var constructor = type.GetConstructors()
                .First();

            var constructorParameters = constructor.GetParameters()
                .Select(s => s.ParameterType)
                .ToArray();

            var service = constructorParameters.Select(serviceProvider.GetService).ToArray();

            var command = (ICommand)constructor.Invoke(service);

            string result = command.Execute(args);
            
            return result;
        }
    }
}