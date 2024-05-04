using FluentValidation;
using MediatR;
using ErrorOr;

// The below code has been replicated from https://github.com/amantinband/clean-architecture/blob/main/src/CleanArchitecture.Application/Common/Behaviors/ValidationBehavior.cs
// The ValidationBehaviour class is injected into the collection of Application Services and will be executed when an method matches the IRequest format, if matched, the relevant validator(s) for the matching request will
// be executed against the command or query method. If several validations exist, and fail, all errors will be compiled and returned in the response.
namespace SensorFlow.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator = validator;

        public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errors = validationResult.Errors
                .ConvertAll(error => Error.Validation(
                    code: error.PropertyName,
                    description: error.ErrorMessage));

            return (dynamic)errors;
        }
    }
}
