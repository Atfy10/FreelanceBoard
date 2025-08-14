using FluentValidation;
using FreelanceBoard.Core.Helpers;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Any())
        {
            var errorMessages = failures.Select(e => e.ErrorMessage).ToList();
            var combinedMessage = string.Join(";", errorMessages);

            // Handle Result<T> case
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var dataType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(dataType)
                    .GetMethod("Failure", new[] { typeof(string), typeof(string), typeof(int) });

                var result = failureMethod.Invoke(null, new object[] { "Validation", combinedMessage, 400 });
                return (TResponse)result;
            }
            // Handle non-generic Result case
            else if (typeof(TResponse) == typeof(Result))
            {
                var result = Result.Failure("Validation", combinedMessage);
                return (TResponse)(object)result;
            }

            // If the response type isn't Result or Result<T>, fall back to throwing
            throw new ValidationException(failures);
        }

        return await next();
    }
}