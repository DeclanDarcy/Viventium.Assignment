using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viventium.Assignment.Abstractions;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Controllers;

public class DataStoreController : Controller
{
    private readonly ICompanyRepo _companyRepo;
    private readonly ICsvParser _csvParser;

    public DataStoreController(ICompanyRepo companyRepo, ICsvParser csvParser)
    {
        _companyRepo = companyRepo;
        _csvParser = csvParser;
    }

    [HttpPost("")]
    public async Task<IActionResult> ImportCsvFileAsync(IFormFile csvFile)
    {
        if (csvFile == null || csvFile.Length == 0)
        {
            return BadRequest("No file");
        }

        IEnumerable<CompanyRecord> companies;
        
        await using (var csvStream = new MemoryStream())
        {
            await csvFile.CopyToAsync(csvStream);
            csvStream.Seek(0, SeekOrigin.Begin);
            companies = await _csvParser.ParseCompaniesAsync(csvStream);
        }

        foreach (CompanyRecord company in companies)
        {
            if (company.Employees.Count != company.Employees.DistinctBy(x => x.EmployeeNumber).Count())
            {
                return BadRequest("The employeeNumber should be unique within a given company");
            }

            foreach (EmployeeRecord employee in company.Employees)
            {
                foreach (EmployeeRecord manager in employee.Managers.Select(x => x.Manager))
                {
                    if (employee.CompanyId != manager.CompanyId)
                    {
                        return BadRequest("The manager of the given employee should exist in the same company.");
                    }
                }
            }
        }

        await _companyRepo.ImportAsync(companies);
        return Ok();
    }
}