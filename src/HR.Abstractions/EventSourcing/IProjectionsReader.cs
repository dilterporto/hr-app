using CSharpFunctionalExtensions;

namespace HR.Abstractions.EventSourcing;

public interface IProjectionsReader<TProjection> where TProjection : Projection
{
  /// <summary>
  /// Gets a Projection by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<Maybe<TProjection>> GetByIdAsync(Guid id);

  /// <summary>
  /// Gets all Projections. 
  /// </summary>
  Task<IQueryable<TProjection>> GetAllAsync();
}
