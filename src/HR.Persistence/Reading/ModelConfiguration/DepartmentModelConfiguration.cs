using HR.Persistence.Reading.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.Reading.ModelConfiguration;

public class DepartmentModelConfiguration : IEntityTypeConfiguration<DepartmentProjection>
{
  public void Configure(EntityTypeBuilder<DepartmentProjection> builder)
  {
    builder.ToTable("departments");
    builder.HasKey(x => x.Id);
    builder.Property(x => x.Id).ValueGeneratedNever().HasColumnName("id");
    builder.Property(x => x.Name).HasColumnName("name");
  }
}
