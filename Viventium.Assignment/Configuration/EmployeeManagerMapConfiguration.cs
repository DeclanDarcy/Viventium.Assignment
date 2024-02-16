using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viventium.Assignment.Entities;

namespace Viventium.Assignment.Configuration;

public class EmployeeManagerMapConfiguration : IEntityTypeConfiguration<EmployeeManagerMap>
{
    public void Configure(EntityTypeBuilder<EmployeeManagerMap> map)
    {
        map.HasKey(x => x.Id);
        map.Property(x => x.Id).IsRequired();
        map.HasOne(x => x.Employee).WithMany(x => x.Managers).HasForeignKey(x => x.EmployeeId);
        map.HasOne(x => x.Manager).WithMany().HasForeignKey(x => x.ManagerId);
    }
}