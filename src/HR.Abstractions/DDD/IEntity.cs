namespace HR.Abstractions.DDD;

public interface IEntity : IEntity<Guid>
{

}

public interface IEntity<TId>
{
  TId Id { get; set; }
}
