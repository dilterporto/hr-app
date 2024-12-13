﻿using HR.Abstractions.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Persistence.Writing.Events;

public class EventModelConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.ToTable("events");
    builder.Property(x => x.Id).HasColumnName("id");
    builder.HasKey(e => e.Id);
    builder.Property(x => x.AggregateId).HasColumnName("aggregate_id");
    builder.Property(x => x.Version).HasColumnName("version");
    builder.Property(x => x.CreatedAt).HasColumnName("created_at");
    builder.Property(x => x.Type).HasColumnName("type");
    builder.Property(x => x.Data)
      .HasColumnName("data")
      .HasColumnType("jsonb");
  }
}
