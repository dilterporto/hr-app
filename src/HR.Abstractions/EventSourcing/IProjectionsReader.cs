using CSharpFunctionalExtensions;

namespace HR.Abstractions.EventSourcing;

public interface IProjectionsReader 
{
  /// <summary>
  /// Gets a Projection by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<Maybe<TProjection>> GetByIdAsync<TProjection>(Guid id) where TProjection : Projection;

  /// <summary>
  /// Gets all Projections. 
  /// </summary>
  Task<IQueryable<TProjection>> GetAllAsync<TProjection>() where TProjection : Projection;
}
