using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitTest.Infrastructure.Persistence.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.EmployeeForename);
        builder.Property(e => e.EmployeeSurename);

        builder.HasData(
            new Permission
            {
                Id = 1,
                EmployeeForename = "nombreTest",
                EmployeeSurename = "apellidoTest"
            },
            new Permission
            {
                Id = 2,
                EmployeeForename = "nombreTest2",
                EmployeeSurename = "apellidoTest2"
            },
            new Permission
            {
                Id = 3,
                EmployeeForename = "nombreTest3",
                EmployeeSurename = "apellidoTest3"
            }
            );
    }
}
