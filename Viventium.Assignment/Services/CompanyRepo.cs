using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Viventium.Assignment.Abstractions;
using Viventium.Assignment.Context;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Services;

public class CompanyRepo : ICompanyRepo
{
    private readonly CompanyDbContext _db;

    public CompanyRepo(CompanyDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<CompanyHeader>> GetCompaniesAsync()
    {
        return await _db.Companies.Select(x => new CompanyHeader
        {
            Id = x.Id,
            Code = x.Code,
            Description = x.Description,
            EmployeeCount = x.EmployeeCount
        }).ToListAsync();
    }

    public async Task<Company> GetCompanyAsync(int companyId)
    {
        return await _db.Companies
            .Include(x => x.Employees)
            .Where(x => x.Id == companyId)
            .Select(x => new Company
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description,
                EmployeeCount = x.EmployeeCount,
                Employees = x.Employees.Select(employee => new EmployeeHeader
                {
                    EmployeeNumber = employee.EmployeeNumber,
                    FullName = employee.FullName
                }).ToArray()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Employee> GetEmployeeAsync(int companyId, string employeeNumber)
    {
        return await _db.Employees
            .Include(x => x.Managers)
            .ThenInclude(x => x.Manager)
            .Where(x => x.CompanyId == companyId && x.EmployeeNumber == employeeNumber)
            .Select(x => new Employee
            {
                EmployeeNumber = x.EmployeeNumber,
                FullName = x.FullName,
                Email = x.Email,
                Department = x.Department,
                HireDate = x.HireDate,
                Managers = x.Managers.Select(map => map.Manager).Select(manager => new EmployeeHeader
                {
                    EmployeeNumber = manager.EmployeeNumber,
                    FullName = manager.FullName
                }).ToArray()
            })
            .FirstOrDefaultAsync();
    }

    public async Task ImportAsync(IEnumerable<CompanyRecord> companies)
    {
        _db.Companies.RemoveRange(_db.Companies);
        await _db.SaveChangesAsync(true);
        _db.Companies.AddRange(companies);
        await _db.SaveChangesAsync(true);
    }
}