using System.Collections.Generic;
using System.Threading.Tasks;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Abstractions;

public interface ICompanyRepo
{
    Task<IEnumerable<CompanyHeader>> GetCompaniesAsync();
    Task<Company> GetCompanyAsync(int companyId);
    Task<Employee> GetEmployeeAsync(int companyId, string employeeNumber);
    Task ImportAsync(IEnumerable<CompanyRecord> companies);
}