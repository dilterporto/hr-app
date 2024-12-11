using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HR.Abstractions.Logging;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
  : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
{
  private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

  public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
  {
    Log.ForContext<TRequest>();
    Log.Information("[Application] Processing {Commmand}", request);
    return await next();
  }
}
