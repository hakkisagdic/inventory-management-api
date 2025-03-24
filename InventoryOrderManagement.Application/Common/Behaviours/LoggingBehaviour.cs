using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryOrderManagement.Core.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        try
        {
            _logger.LogInformation($"Try executing {requestName} at {DateTime.UtcNow.ToString()}");

            return await next();
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, $"Error executing {requestName} at {DateTime.UtcNow.ToString()}");

            throw;
        }
    }
}