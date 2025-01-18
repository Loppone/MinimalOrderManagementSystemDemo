namespace ImageService.Api.Application.Handlers;

public static class ErrorHandlingHelper
{
    public static IResult HandleValidationErrors(IEnumerable<IError> errors)
    {
        var errorDetails = errors
            .Select((e, index) => new KeyValuePair<string, object?>($"{index + 1}", e.Message))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        return Results.Problem(
            detail: "Validation Errors",
            statusCode: StatusCodes.Status400BadRequest,
            extensions: new Dictionary<string, object?>
            {
                { "errors", errorDetails }
            }
        );
    }
}
