using System;
using EmployeesMapping.Core.Dtos;
using EmployeeDto = EmployeesMapping.Data.EmployeeDto;

namespace EmployeesMapping.Core.Contracts
{
    public interface IEmployeeController
    {
        void AddEmployee(EmployeeDto employeeDto);

        void SetBirthday(int employeeId, DateTime date);

        void SetAddress(int employeeId, string address);

        EmployeeDto GetEmployeeInfo(int employeeId);

        EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId);
    }
}