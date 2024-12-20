﻿using CSharpFunctionalExtensions;
using HR.Abstractions.DDD;
using HR.Abstractions.EventSourcing;
using HR.Persistence.Writing.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HR.Persistence.Writing;

public class Repository<TAggregate, TRepository>(
  EventsDbContext dbContext, 
  IEventsCommiter eventsCommitter, ILogger<TRepository> logger) where TAggregate : AggregateRoot
{
  public async Task<Maybe<TAggregate>> LoadByIdAsync(Guid id)
  {
    var events = await dbContext.Set<Event>()
      .Where(e => e.AggregateId == id)
      .OrderBy(e => e.Version)
      .ToListAsync();

    if (events.Count == 0)
    {
      return Maybe<TAggregate>.None;
    }

    var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true)!;
    aggregate.Id = id;

    foreach (DomainEvent? eventData in events.Select(@event => JsonConvert
               .DeserializeObject(@event.Data!, Type.GetType(@event.Type!)!)))
      aggregate.ApplyChange(eventData!, isNew: false);

    return aggregate;
  }

  public async Task<Result> SaveAsync(TAggregate aggregate)
  {
    try
    {
      PersistAggregateEvents(aggregate);

      await CommitEvents(aggregate);

      aggregate.MarkChangesAsCommitted();
      
      return Result.Success();
    }
    catch (Exception e)
    {
      return Result.Failure(e.Message);
    }
  }

  private async Task CommitEvents(TAggregate aggregate) 
    => await eventsCommitter.CommitAllAsync(aggregate.UncommittedEvents);

  private void PersistAggregateEvents(TAggregate aggregate)
  {
    var events = aggregate.UncommittedEvents
      .Select(@event => new Event
      {
        Id = @event.Id,
        AggregateId = aggregate.Id,
        Version = @event.Version,
        CreatedAt = @event.CreatedAt,
        Type = $"{@event.GetType().FullName}, {@event.GetType().Assembly.GetName().Name}",
        Data = JsonConvert.SerializeObject(@event)
      });

    foreach (var @event in events)
    {
      logger.LogInformation("[Persistence] Saving {EventType} {@Event}", @event.GetType(), @event);
      dbContext.Add(@event);
    }
      
  }
}
