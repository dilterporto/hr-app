using MediatR;

namespace HR.Abstractions.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>;
