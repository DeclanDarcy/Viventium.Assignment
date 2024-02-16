using System;
using System.Collections.Generic;

namespace Viventium.Assignment.Entities;

public class EmployeeRecord
{
    public Guid Id { get; set; }
    public int CompanyId { get; set; }
    public string Email { get; set; }
    public string Department { get; set; }
    public DateTime HireDate { get; set; }
    public List<EmployeeManagerMap> Managers { get; set; }
    public string EmployeeNumber { get; set; }
    public string FullName { get; set; }
}