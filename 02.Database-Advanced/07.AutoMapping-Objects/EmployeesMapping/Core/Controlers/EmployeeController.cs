using System;
using System.Linq;
using EmployeesMapping.Core.Contracts;
using EmployeesMapping.Core.Dtos;
using EmployeesMapping.Data;
using EmployeeDto = EmployeesMapping.Data.EmployeeDto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesMapping.Models;

namespace EmployeesMapping.Core.Controlers
{
    public class EmployeeController : IEmployeeController
    {
        private readonly EmployeeContext context;
        private readonly IMapper mapper;
        
        public EmployeeController(EmployeeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<Employee>(employeeDto);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();
        }

        public void SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Birthday = date;

            this.context.SaveChanges();
        }

        public void SetAddress(int employeeId, string address)
        {
            var employee = context.Employees.Find(employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            employee.Address = address;

            this.context.SaveChanges();
        }

        public EmployeeDto GetEmployeeInfo(int employeeId)
        {
         
            var employee = context.Employees.Find(employeeId);

            var employeeDto = Mapper.Map<EmployeeDto>(employee); 
                
                
            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }

        public EmployeePersonalInfoDto GetEmployeePersonalInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeePersonalInfoDto>(employee);

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employeeDto;
        }
    }
}