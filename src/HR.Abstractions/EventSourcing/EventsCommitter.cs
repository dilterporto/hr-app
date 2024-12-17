using HR.Abstractions.DDD;
using Microsoft.Extensions.Logging;

namespace HR.Abstractions.EventSourcing;

public class EventsCommitter(ILogger<EventsCommitter> logger) : IEventsCommiter
{
  private readonly IDictionary<Type, Func<IDomainEvent, Task>> _eventCommiters
    = new Dictionary<Type, Func<IDomainEvent, Task>>();

  private static Func<IDomainEvent, Task> RunAsync<TEvent>(IEventCommitter<TEvent> accountEventCommiter)
    where TEvent : IDomainEvent =>
    async (@event) => await accountEventCommiter
      .CommitAsync((TEvent)@event);

  public void AddAsync<TEvent>(IEventCommitter<TEvent> eventCommitter) where TEvent : IDomainEvent 
    => _eventCommiters.Add(typeof(TEvent), RunAsync(eventCommitter));

  public async Task CommitAllAsync(IEnumerable<IDomainEvent> events)
  {
    foreach (var @event in events)
    {
      if (!_eventCommiters.ContainsKey(@event.GetType()))
      {
        logger.LogWarning("[Persistence] No committer found for {EventType} {@Event}", @event.GetType(), @event);
        continue;
      }

      var committer = _eventCommiters[@event.GetType()];
      await committer.Invoke(@event);
      logger.LogInformation("[Persistence] Committed {EventType} {@Event}", @event.GetType(), @event);
    }
  }
}
