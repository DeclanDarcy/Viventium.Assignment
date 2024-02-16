using System.Collections.Generic;

namespace Viventium.Assignment.Entities;

public class CompanyRecord
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int EmployeeCount { get; set; }
    public List<EmployeeRecord> Employees { get; set; }
}