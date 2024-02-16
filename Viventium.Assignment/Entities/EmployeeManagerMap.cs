using System;

namespace Viventium.Assignment.Entities;

public class EmployeeManagerMap
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid ManagerId { get; set; }
    public EmployeeRecord Employee { get; set; }
    public EmployeeRecord Manager { get; set; }
}