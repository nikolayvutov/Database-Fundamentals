using System;
using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeController _employeeController;
        
        public SetBirthdayCommand(IEmployeeController employeeController)
        {
            this._employeeController = employeeController;
        }
        
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            
            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);
            
            this._employeeController.SetBirthday(id, date);

            return "Command accomplished successfully!";
        }
    }
}