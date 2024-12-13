using HR.Persistence.Reading.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Reading;

public class ProjectionsDbContext : DbContext
{
  public ProjectionsDbContext(DbContextOptions<ProjectionsDbContext> options) 
    : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new EmployeeModelConfiguration());
    modelBuilder.ApplyConfiguration(new DepartmentModelConfiguration());
  }
}
