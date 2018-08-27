using System;
using System.Text;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly IManagerController _managerController;
        
        public ManagerInfoCommand(IManagerController managerController)
        {
            this._managerController = managerController;
        }
        
        public string Execute(string[] args)
        {
            var sb = new StringBuilder();
            
            int employeeId = int.Parse(args[0]);

            var managerDto = _managerController.ManagerInfo(employeeId);

            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesCount}");

            foreach (var employee in managerDto.EmployeesDto)
            {
                sb.AppendLine($"     - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd(); 
        }
    }
}