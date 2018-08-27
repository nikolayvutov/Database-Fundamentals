using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using EmployeesMapping.Core.Contracts;
using EmployeesMapping.Core.Dtos;
using EmployeesMapping.Data;

namespace EmployeesMapping.Core.Controlers
{
    public class ManagerController : IManagerController
    {
        private readonly EmployeeContext _context;
        
        public ManagerController(EmployeeContext context)
        {
            this._context = context;
        }
        
        public void SetManager(int employeeId, int managerId)
        {
            var employee = _context.Employees.Find(employeeId);
            
            var manager = _context.Employees.Find(managerId);

            if (employee == null || manager == null)
            {
                throw new ArgumentException("Invalid id!");
            }

            employee.Manager = manager;

            _context.SaveChanges();
        }

        public ManagerDto ManagerInfo(int employeeId)
        {
            var employee = _context.Employees
                .Where(x => x.Id == employeeId)
                .ProjectTo<ManagerDto>()
                .SingleOrDefault();

            if (employee == null)
            {
                throw new ArgumentException("Invalid Id");
            }

            return employee;
        }
    }
}