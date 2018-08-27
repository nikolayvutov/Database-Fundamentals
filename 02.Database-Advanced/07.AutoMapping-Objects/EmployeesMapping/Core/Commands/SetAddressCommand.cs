using EmployeesMapping.Core.Contracts;

namespace EmployeesMapping.Core.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly IEmployeeController _employeeController;
        
        public SetAddressCommand(IEmployeeController employeeController)
        {
            this._employeeController = employeeController;
        }
        
        public string Execute(string[] args)
        {
            int id = int.Parse(args[0]);
            string address = args[1];
            
            _employeeController.SetAddress(id, address);

            return "Command accomplished syccessfully!";
        }
    }
}