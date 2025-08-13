using FluentValidation;
using FreelanceBoard.Core.Domain.Enums;
using FreelanceBoard.Core.Helpers;
using MediatR;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	private readonly OperationExecutor _execute;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
		OperationExecutor operationExecutor)
	{
		_validators = validators;
		_execute = operationExecutor;
	}

	public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
		RequestHandlerDelegate<TResponse> next)
	{
		if (_validators.Any())
		{
			var context = new ValidationContext<TRequest>(request);

			var validationResults = await Task.WhenAll(
				_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

			var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}
		}
		return await next();
	}


}