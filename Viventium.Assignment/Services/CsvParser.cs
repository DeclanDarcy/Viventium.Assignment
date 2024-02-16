using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Csv;
using Viventium.Assignment.Abstractions;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models.Parsing;

namespace Viventium.Assignment.Services;

public class CsvParser : ICsvParser
{
    public async Task<IReadOnlyCollection<CompanyRecord>> ParseCompaniesAsync(MemoryStream csvStream)
    {
        var companies = new List<CompanyRecord>();
        var companyMaps = new Dictionary<int, CompanyMap>();
        IAsyncEnumerable<ICsvLine> lines = CsvReader.ReadFromStreamAsync(csvStream);

        await foreach (ICsvLine line in lines)
        {
            int companyId = int.Parse(line[0]);
            string companyCode = line[1];
            string companyDescription = line[2];

            CompanyRecord company = companies.FirstOrDefault(x => x.Id == companyId);
            CompanyMap map;

            if (company == null)
            {
                company = new CompanyRecord
                {
                    Id = companyId,
                    Code = companyCode,
                    Description = companyDescription,
                    Employees = new List<EmployeeRecord>()
                };
                
                companies.Add(company);

                map = new CompanyMap();
                companyMaps.Add(companyId, map);
            }
            else
            {
                map = companyMaps[companyId];
            }
            
            string employeeNumber = line[3];
            string employeeFirstName = line[4];
            string employeeLastName = line[5];
            string employeeEmail = line[6];
            string employeeDepartment = line[7];
            DateTime hireDate = DateTime.TryParse(line[8], out DateTime dateTime) ? dateTime : default;

            var employee = new EmployeeRecord
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId,
                EmployeeNumber = employeeNumber,
                FullName = $"{employeeFirstName} {employeeLastName}",
                Email = employeeEmail,
                Department = employeeDepartment,
                HireDate = hireDate,
                Managers = new List<EmployeeManagerMap>()
            };

            company.Employees.Add(employee);
            
            string managerEmployeeNumber = line[9];
            map.Employees.Add(employeeNumber, employee);
            map.Managers.Add(employeeNumber, managerEmployeeNumber);
        }

        foreach (CompanyRecord company in companies)
        {
            CompanyMap map = companyMaps[company.Id];
            
            foreach (EmployeeRecord employee in company.Employees)
            {
                map.Managers.TryGetValue(employee.EmployeeNumber, out string managerEmployeeNumber);

                while (!string.IsNullOrEmpty(managerEmployeeNumber))
                {
                    EmployeeRecord manager = map.Employees[managerEmployeeNumber];
                    
                    employee.Managers.Add(new EmployeeManagerMap
                    {
                        Id = Guid.NewGuid(),
                        Employee = employee,
                        EmployeeId = employee.Id,
                        Manager = manager,
                        ManagerId = manager.Id
                    });

                    map.Managers.TryGetValue(manager.EmployeeNumber, out managerEmployeeNumber);
                }
            }

            company.EmployeeCount = company.Employees.Count;
        }

        return companies;
    }
}