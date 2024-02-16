namespace Viventium.Assignment.Models;

public class Company : CompanyHeader
{
    /// <summary>List of EmployeeHeader objects in the given company</summary>
    public EmployeeHeader[] Employees { get; set; }
}