using System;

namespace Viventium.Assignment.Models;

public class Employee : EmployeeHeader
{
    /// <summary>EmployeeEmail</summary>
    public string Email { get; set; }
    
    /// <summary>EmployeeDepartment</summary>
    public string Department { get; set; }
    
    /// <summary>HireDate</summary>
    public DateTime HireDate { get; set; }
    
    /// <summary>
    /// List of EmployeeHeaders of the managers, ordered ascending by seniority (i.e. the immediate manager first)
    /// </summary>
    public EmployeeHeader[] Managers { get; set; }
}