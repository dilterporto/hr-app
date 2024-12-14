using MediatR;

namespace HR.Abstractions.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IRequest<TResponse>
{

}
