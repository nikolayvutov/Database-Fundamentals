﻿using EmployeesMapping.Core.Contracts;
using EmployeesMapping.Data;

namespace EmployeesMapping.Core.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeController employeeController;
        
        public AddEmployeeCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }


        public string Execute(string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);
            
            EmployeeDto employeeDto = new EmployeeDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };
            
            this.employeeController.AddEmployee(employeeDto);

            return $"Employee {firstName} {lastName} was added successfully";
        }
    }
}