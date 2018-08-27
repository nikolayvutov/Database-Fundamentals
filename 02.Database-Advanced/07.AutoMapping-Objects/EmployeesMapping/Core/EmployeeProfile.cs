using AutoMapper;
using EmployeesMapping.Core.Dtos;
using EmployeesMapping.Models;
using EmployeeDto = EmployeesMapping.Data.EmployeeDto;

namespace EmployeesMapping.Core
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, ManagerDto>()
                .ForMember(dest => dest.EmployeesDto, 
                    from => from.MapFrom(x => x.ManagerEmployees))
                .ReverseMap();
            CreateMap<Employee, EmployeePersonalInfoDto>().ReverseMap();
        }
    }
}