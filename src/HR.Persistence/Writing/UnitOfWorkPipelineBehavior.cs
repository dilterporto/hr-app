using HR.Abstractions.CQRS;
using HR.Persistence.Writing.Events;
using MediatR;

namespace HR.Persistence.Writing;

public class UnitOfWorkPipelineBehavior<TRequest, TResponse>(EventsDbContext unitOfWork) : CommandPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  public override async Task<TResponse> Handle(ICommand<TResponse> command, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    var response = await next();
    await unitOfWork.SaveChangesAsync(cancellationToken);
    return response;
  }
}
