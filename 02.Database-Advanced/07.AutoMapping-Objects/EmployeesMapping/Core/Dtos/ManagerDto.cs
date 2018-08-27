using System.Collections.Generic;

namespace EmployeesMapping.Core.Dtos
{
    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> EmployeesDto { get; set; }

        public int EmployeesCount => EmployeesDto.Count;
    }
}