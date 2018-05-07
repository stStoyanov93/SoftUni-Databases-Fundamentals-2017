using System;
using Microsoft.Extensions.DependencyInjection;
using Employees.Data;
using Microsoft.EntityFrameworkCore;
using Employees.Services;
using AutoMapper;

namespace Employees.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options => options.UseSqlServer(Configuration.ConfigurationString));

            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<AutoMaappperProfile>());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
