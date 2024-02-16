using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Viventium.Assignment.Abstractions;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Controllers;

[Route("Companies")]
public class CompanyController : Controller
{
    private readonly ICompanyRepo _companyRepo;

    public CompanyController(ICompanyRepo companyRepo)
    {
        _companyRepo = companyRepo;
    }

    [HttpGet("")]
    public async Task<IEnumerable<CompanyHeader>> GetCompaniesAsync()
    {
        return await _companyRepo.GetCompaniesAsync();
    }

    [HttpGet("{companyId}")]
    public async Task<Company> GetCompanyAsync(int companyId)
    {
        return await _companyRepo.GetCompanyAsync(companyId);
    }

    [HttpGet("{companyId}/Employees/{employeeNumber}")]
    public async Task<Employee> GetEmployeeAsync(int companyId, string employeeNumber)
    {
        return await _companyRepo.GetEmployeeAsync(companyId, employeeNumber);
    }
}