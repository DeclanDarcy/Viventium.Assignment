namespace Viventium.Assignment.Models;

public class CompanyHeader
{
    /// <summary>CompanyId</summary>
    public int Id { get; set; }
    
    /// <summary>CompanyCode</summary>
    public string Code { get; set; }
    
    /// <summary>CompanyDescription</summary>
    public string Description { get; set; }
    
    /// <summary>Number of Employees in the given company</summary>
    public int EmployeeCount { get; set; }
}

