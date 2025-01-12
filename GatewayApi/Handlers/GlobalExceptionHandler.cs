namespace GatewayApi.Handlers;

/// <summary>
/// Gestisce le eccezioni globali
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Gestione delle eccezioni personalizzate
        // GP: Dovrebbe gestire solo i casi NON PREVISTI
        var message = exception switch
        {
            // Esempio:
            NotFoundException ex => $"The item has not been found: {ex.Message}",
            _ => exception.Message
        };

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "Unexpected Error!",
                Detail = message,
                Type=exception.GetType().Name,
                Status = StatusCodes.Status500InternalServerError
            }
        });
    }
}
