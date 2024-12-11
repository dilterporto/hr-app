namespace HR.Abstractions.DDD;

public interface IAggregateRoot : IEntity
{
  public long Version { get; set; }
  void MarkChangesAsCommitted();
  List<DomainEvent> UncommittedEvents { get; }
}
