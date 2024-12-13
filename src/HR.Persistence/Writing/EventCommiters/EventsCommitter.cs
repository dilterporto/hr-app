using HR.Abstractions.DDD;
using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class EventsCommitter : IEventsCommiter
{
  private readonly ILogger<EventsCommitter> _logger;

  private readonly IDictionary<Type, Func<IDomainEvent, Task>> _eventCommiters
    = new Dictionary<Type, Func<IDomainEvent, Task>>();

  public EventsCommitter(
    IEventCommitter<EmployeeCreatedEvent> employeeCreatedEventCommitter,
    ILogger<EventsCommitter> logger)
  {
    _logger = logger;
    _eventCommiters.Add(typeof(EmployeeCreatedEvent), RunAsync(employeeCreatedEventCommitter));
  }
  
  private static Func<IDomainEvent, Task> RunAsync<TEvent>(IEventCommitter<TEvent> accountEventCommiter)
    where TEvent : IDomainEvent =>
    async (@event) => await accountEventCommiter
      .CommitAsync((TEvent)@event);
  
  public async Task CommitAllAsync(IEnumerable<IDomainEvent> events)
  {
    foreach (var @event in events)
    {
      if (!_eventCommiters.ContainsKey(@event.GetType()))
      {
        _logger.LogWarning("[Persistence] No committer found for {EventType} {@Event}", @event.GetType(), @event);
        continue;
      }

      var committer = _eventCommiters[@event.GetType()];
      await committer.Invoke(@event);
      _logger.LogInformation("[Persistence] Committed {EventType} {@Event}", @event.GetType(), @event);
    }
  }
}
