using System;
using System.Collections.Generic;
using System.Text;

using AutoMapper;
using Employees.Models;
using Employees.DtoModels;

namespace Employees.App
{
    class AutoMaappperProfile : Profile
    {
        public AutoMaappperProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }
    }
}
