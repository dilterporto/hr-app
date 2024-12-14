using CSharpFunctionalExtensions;
using HR.Abstractions.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace HR.Persistence.Reading;

public class ProjectionsReader(ProjectionsDbContext projectionsDbContext) : IProjectionsReader
{
  public async Task<Maybe<TProjection>> GetByIdAsync<TProjection>(Guid id) where TProjection : Projection
  {
    var projection = await projectionsDbContext
      .Set<TProjection>()
      .TagWith($"GetByIdAsync - {typeof(TProjection).Name}")
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == id);

    return projection == null ? Maybe<TProjection>.None : Maybe<TProjection>.From(projection);
  }

  public Task<IQueryable<TProjection>> GetAllAsync<TProjection>() where TProjection : Projection 
    => Task.FromResult(projectionsDbContext.Set<TProjection>()
      .TagWith($"GetAllAsync - {typeof(TProjection).Name}")
      .AsNoTracking()
      .AsQueryable());
}
