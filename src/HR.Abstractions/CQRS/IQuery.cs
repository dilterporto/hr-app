using MediatR;

namespace HR.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>;

