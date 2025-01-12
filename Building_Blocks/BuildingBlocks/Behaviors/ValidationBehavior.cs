namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        
        // Eredito da ResultBase perchè TResponse è generalmente un Result<T>, quindi la constraint su Result è restrittiva
        // La classe dovrà esporre un costruttore senza parametri
        where TResponse : ResultBase, new()
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .Select(x => new Error(x.ErrorMessage))
            .Distinct()
            .ToList();

        if (!errors.Any())
        {
            return await next();
        }

        var result = new TResponse();

        foreach (var error in errors)
            result.Reasons.Add(error);

        return result;
    }
}