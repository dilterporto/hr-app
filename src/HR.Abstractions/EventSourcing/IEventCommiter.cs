using HR.Abstractions.DDD;

namespace HR.Abstractions.EventSourcing;

public interface IEventCommitter<in TEvent> where TEvent : IDomainEvent
{
  Task CommitAsync(TEvent @event);
}

public interface IEventsCommiter
{
  Task CommitAllAsync(IEnumerable<IDomainEvent> events);
}
