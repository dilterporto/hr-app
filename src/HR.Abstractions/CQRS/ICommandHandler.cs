using MediatR;

namespace HR.Abstractions.CQRS;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : IRequest<TResponse>
{

}
