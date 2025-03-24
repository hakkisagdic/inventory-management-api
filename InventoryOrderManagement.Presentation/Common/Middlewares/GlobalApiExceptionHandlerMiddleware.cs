using Microsoft.AspNetCore.Diagnostics;

namespace InventoryOrderManagement.Presentation.Common.Middlewares;

public class GlobalApiExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalApiExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IExceptionHandler customExceptionHandler)
    {
        try
        {
            await _next(httpContext);

            switch (httpContext.Response.StatusCode)
            {
            case StatusCodes.Status401Unauthorized:
                await customExceptionHandler.TryHandleAsync(
                    httpContext,
                    new UnauthorizedAccessException("Unauthorized - Token missing or invalid"),
                    CancellationToken.None);
                break;
            case StatusCodes.Status403Forbidden:
                await customExceptionHandler.TryHandleAsync(
                    httpContext,
                    new Exception("Forbidden - Access denied"),
                    CancellationToken.None);
                break;
            }
        }
        catch (Exception e)
        {
            await customExceptionHandler.TryHandleAsync(httpContext, e, CancellationToken.None);
        }
    }
}