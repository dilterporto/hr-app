using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Writing.Events;

public class EventsDbContext(DbContextOptions<EventsDbContext> options) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new EventModelConfiguration());
  }
}
