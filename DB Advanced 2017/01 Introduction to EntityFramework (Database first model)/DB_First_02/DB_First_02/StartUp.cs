using System;
using System.Linq;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using P02_DatabaseFirst.Data;
using P02_DatabaseFirst.Data.Models;


namespace P02_DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new SoftUniContext())
            {
                //03
                //var employees = db.Employees
                //    .Select(e => new
                //    {
                //        Name = $"{e.FirstName} {e.LastName} {e.MiddleName}",
                //        e.JobTitle,
                //        e.Salary
                //    });

                //foreach (var e in employees)
                //{
                //    Console.WriteLine($"{e.Name} {e.JobTitle} {e.Salary:F2}");
                //}

                //04
                //var employees = db.Employees
                //    .Where(e => e.Salary > 50000)
                //    .Select(e => new
                //    {
                //        e.FirstName
                //    })
                //    .OrderBy(e => e.FirstName);

                //foreach (var e in employees)
                //{
                //    Console.WriteLine($"{e.FirstName}");
                //}

                //05
                //var employees = db.Employees
                //    .Where(e => e.Department.Name == "Research and Development")
                //    .OrderBy(e => e.Salary)
                //    .ThenByDescending(e => e.FirstName)
                //    .Select(e => new
                //    {
                //        Name = $"{e.FirstName} {e.LastName}",
                //        DPName = e.Department.Name,
                //        e.Salary
                //    });

                //foreach (var e in employees)
                //{
                //    Console.WriteLine($"{e.Name} from {e.DPName} - {e.Salary:F2}");
                //}

                //06
                //var address = new Address()
                //{
                //    AddressText = "Vitoshka 15",
                //    TownId = 4
                //};

                //var nakov = db.Employees
                //    .Where(e => e.LastName == "Nakov")
                //    .FirstOrDefault();

                //nakov.Address = address;
                //db.SaveChanges();

                //var result = db.Employees                   
                //    .OrderByDescending(a => a.AddressId)
                //    .Take(10)
                //    .Select(a => a.Address.AddressText);

                //foreach (var r in result)
                //{
                //    Console.WriteLine($"{r}");
                //}

                //07
                //var data = db.Employees
                //    .Where(e =>
                //    e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 &&
                //    ep.Project.StartDate.Year <= 2003))
                //    .Take(30)
                //    .Select(e => new
                //    {
                //        Name = $"{e.FirstName} {e.LastName}",
                //        MName = $"{e.Manager.FirstName} {e.Manager.LastName}",
                //        Projects = e.EmployeesProjects.Select(ep => new
                //        {
                //            ep.Project.Name,
                //            ep.Project.StartDate,
                //            ep.Project.EndDate
                //        })
                //    });

                //foreach (var employee in data)
                //{
                //    Console.WriteLine($"{employee.Name} - Manager: {employee.MName}");

                //    foreach (var project in employee.Projects)
                //    {
                //        Console.Write($"--{project.Name} - {project.StartDate} - ");

                //        if (project.EndDate == null)
                //        {
                //            Console.WriteLine("not finished");
                //        }
                //        else
                //        {
                //            Console.WriteLine($"{project.EndDate}");
                //        }
                //    }
                //}

                //08
                //var data = db.Addresses.OrderByDescending(x => x.Employees.Count)
                //    .ThenBy(x => x.Town.Name)
                //    .ThenBy(x => x.AddressText)
                //    .Select(x => new
                //    {
                //        x.AddressText,
                //        x.Town.Name,
                //        x.Employees.Count
                //    })
                //    .Take(10);

                //foreach (var item in data)
                //{
                //    Console.WriteLine($"{item.AddressText}, {item.Name} - {item.Count} employees");
                //}

                //09
                //var employee = db.Employees
                //    .Where(e => e.EmployeeId == 147)
                //    .Select(e => new
                //    {
                //        Name = $"{e.FirstName} {e.LastName}",
                //        e.JobTitle,
                //        Projects = e.EmployeesProjects.Select(ep => new
                //        {
                //            ep.Project.Name
                //        })
                //    });

                //foreach (var e in employee)
                //{
                //    Console.WriteLine($"{e.Name} - {e.JobTitle}");

                //    var orderedProjects = e.Projects.OrderBy(x => x.Name);

                //    foreach (var p in orderedProjects)
                //    {
                //        Console.WriteLine($"{p.Name}");
                //    }
                //}

                //10
                //var data = db.Departments
                //    .Where(x => x.Employees.Count() > 5)
                //    .Select(x => new
                //    {
                //        EmpCount = x.Employees.Count(),
                //        DepName = x.Name,
                //        MName = $"{x.Manager.FirstName} {x.Manager.LastName}",
                //        EmpS = x.Employees.Select(y => new
                //        {
                //            y.FirstName,
                //            y.LastName,
                //            y.JobTitle
                //        }
                //      )
                //      .OrderBy(y => y.FirstName)
                //      .ThenBy(y => y.LastName)
                //    })
                //    .OrderBy(x => x.EmpCount)
                //    .ThenBy(x => x.DepName);

                //foreach (var dep in data)
                //{
                //    Console.WriteLine($"{dep.DepName} - {dep.MName}");

                //    foreach (var e in dep.EmpS)
                //    {
                //        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                //    }

                //    Console.WriteLine(new string('-', 10));
                //}

                //11
                //var data = db.Projects
                //    .OrderByDescending(x => x.StartDate)
                //    .Take(10)
                //    .Select(x => new
                //    {
                //        x.Name,
                //        x.Description,
                //        StartDate = x.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                //    })
                //    .OrderBy(x => x.Name);

                //foreach (var item in data)
                //{
                //    Console.WriteLine(item.Name);
                //    Console.WriteLine(item.Description);
                //    Console.WriteLine(item.StartDate);
                //}

                //12
                //var selectedEmployees = db.Employees
                //    .Where(e => e.Department.Name == "Engineering"
                //    || e.Department.Name == "Tool Design"
                //    || e.Department.Name == "Marketing "
                //    || e.Department.Name == "Information Services");

                //foreach (var e in selectedEmployees)
                //{
                //    e.Salary *= 1.12M;
                //}

                //db.SaveChanges();

                //var orderedEmps = selectedEmployees
                //    .Select(e => new
                //    {
                //        e.FirstName,
                //        e.LastName,
                //        e.Salary
                //    })
                //.OrderBy(e => e.FirstName)
                //.ThenBy(e => e.LastName);

                //foreach (var e in orderedEmps)
                //{
                //    Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
                //}

                //13
                //var selectedEmployees = db.Employees
                //    .Where(e => e.FirstName.StartsWith("Sa"))
                //    .Select(e => new
                //    {
                //        e.FirstName,
                //        e.LastName,
                //        e.JobTitle,
                //        e.Salary
                //    })
                //    .OrderBy(e => e.FirstName)
                //    .ThenBy(e => e.LastName);

                //foreach (var e in selectedEmployees)
                //{
                //    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
                //}

                //14
                //int idOfProjectToDelete = 2;

                //var employeesProjectsToDelete = db.EmployeesProjects.Where(x => x.ProjectId == idOfProjectToDelete);
                //var projectToDelete = db.Projects.Where(x => x.ProjectId == idOfProjectToDelete).SingleOrDefault();

                //db.EmployeesProjects.RemoveRange(employeesProjectsToDelete);
                //db.Projects.Remove(projectToDelete);
                //db.SaveChanges();

                //var updatedProjects = db.Projects.Take(10);

                //foreach (var pr in updatedProjects)
                //{
                //    Console.WriteLine(pr.Name);
                //}

                //15
                //var townToRemove = Console.ReadLine();
                //var town = db.Towns
                //    .Include(t => t.Addresses)
                //    .SingleOrDefault(t => t.Name == townToRemove);

                //var adressCount = 0;

                //if (town != null)
                //{
                //    adressCount = town.Addresses.Count;

                //    db.Employees
                //        .Where(e => e.AddressId != null && town.Addresses.Any(a => a.AddressId == e.AddressId))
                //        .ToList()
                //        .ForEach(e => e.Address = null);

                //    db.SaveChanges();

                //    db.Addresses.RemoveRange(town.Addresses);
                //    db.Towns.Remove(town);
                //    db.SaveChanges();
                //}

                //Console.WriteLine($"{adressCount} address in {townToRemove} was deleted");
            }
        }
    }
}
