using System;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeController _employeeController;
        
        public EmployeePersonalInfoCommand(IEmployeeController employeeController)
        {
            this._employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);

            var employeeDto = this._employeeController.GetEmployeePersonalInfo(id);

            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:F2}"
                   + Environment.NewLine + $"Birthday: {employeeDto.Birthday.Value.ToString("dd-MM-yyyy")}"
                   + Environment.NewLine + $"Address: {employeeDto.Address}";
        }
    }
}