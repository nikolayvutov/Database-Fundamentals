using System;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeController _employeeController;
        
        public EmployeeInfoCommand(IEmployeeController employeeController)
        {
            this._employeeController = employeeController;
        }
        
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            var employeeDto = this._employeeController.GetEmployeeInfo(id);

            return $"ID: {employeeDto.Id} {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:F2}";
        }
    }
}