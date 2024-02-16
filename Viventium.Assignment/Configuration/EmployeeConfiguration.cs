using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeRecord>
{
    public void Configure(EntityTypeBuilder<EmployeeRecord> employee)
    {
        employee.HasKey(x => x.Id);
        
        // TODO: Set max lengths based on requirements for storage and query efficiency
        employee.Property(x => x.Id).IsRequired();
        employee.Property(x => x.EmployeeNumber).IsRequired();
        employee.Property(x => x.FullName).IsRequired();
        employee.Property(x => x.Email).IsRequired();
        employee.Property(x => x.Department).IsRequired();
        employee.Property(x => x.HireDate).IsRequired();
        
        employee.HasIndex(x => new
        {
            x.CompanyId,
            x.EmployeeNumber
        }).IsUnique();
    }
}