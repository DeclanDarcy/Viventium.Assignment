using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viventium.Assignment.Entities;
using Viventium.Assignment.Models;

namespace Viventium.Assignment.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<CompanyRecord>
{
    public void Configure(EntityTypeBuilder<CompanyRecord> company)
    {
        company.HasKey(x => x.Id);
        
        // TODO: Set max lengths based on requirements for storage and query efficiency
        company.Property(x => x.Id).IsRequired();
        company.Property(x => x.Code).IsRequired();
        company.Property(x => x.Description).IsRequired();
        company.Property(x => x.EmployeeCount).IsRequired();
        
        company.HasMany(x => x.Employees).WithOne().HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Cascade);
    }
}