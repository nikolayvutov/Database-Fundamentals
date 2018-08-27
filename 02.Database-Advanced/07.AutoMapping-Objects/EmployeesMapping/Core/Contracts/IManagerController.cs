using EmployeesMapping.Core.Dtos;

namespace EmployeesMapping.Core.Contracts
{
    public interface IManagerController
    {
        void SetManager(int employeeId, int managerId);

        ManagerDto ManagerInfo(int employeeId);
    }
}