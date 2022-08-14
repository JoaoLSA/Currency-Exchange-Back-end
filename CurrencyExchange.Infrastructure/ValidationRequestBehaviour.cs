using FluentValidation;
using MediatR;

namespace CurrencyExchange.Infrastructure
{
    public class ValidationRequestBehavior<TRequest, Unit> : IPipelineBehavior<TRequest, Unit>
        where TRequest : IRequest<Unit>
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidationRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            var tasks = _validators.Select(async v => await v.ValidateAsync(new ValidationContext<TRequest>(request)));
            var result = await Task.WhenAll(tasks);
            var failures = result.SelectMany(x => x.Errors)
               .Where(f => f != null)
               .ToList();

            return failures.Any() ? throw new RequestValidationException(failures) : await next();
        }

    }
}
