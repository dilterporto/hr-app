using HR.Persistence.Reading.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.Reading.ModelConfiguration;

public class EmployeeModelConfiguration : IEntityTypeConfiguration<EmployeeProjection>
{
  public void Configure(EntityTypeBuilder<EmployeeProjection> builder)
  {
    builder.ToTable("employees");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedNever().HasColumnName("id");
    builder.Property(x => x.FirstName).HasColumnName("first_name");
    builder.Property(x => x.LastName).HasColumnName("last_name");
    builder.Property(x => x.HireDate).HasColumnName("hire_date");
    builder.Property(x => x.DepartmentId).HasColumnName("department_id");
    builder.Property(x => x.PhoneNumber).HasColumnName("phone_number");
    builder.Property(x => x.AddressLine1).HasColumnName("address_line_1");
    builder.Property(x => x.AddressLine2).HasColumnName("address_line_2");
    builder.Property(x => x.AddressCity).HasColumnName("address_city");
    builder.Property(x => x.AddressState).HasColumnName("address_state");
    builder.Property(x => x.AddressZipCode).HasColumnName("address_zip_code");
  }
}
