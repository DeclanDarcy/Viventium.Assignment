using System.Collections.Generic;
using Viventium.Assignment.Entities;

namespace Viventium.Assignment.Models.Parsing;

public class CompanyMap
{
    public CompanyMap()
    {
        Employees = new Dictionary<string, EmployeeRecord>();
        Managers = new Dictionary<string, string>();
    }
    
    public IDictionary<string, EmployeeRecord> Employees { get; }
    public IDictionary<string, string> Managers { get; }
}