using FluentValidation;
using MediatR;

namespace CurrencyExchange.Domain.Messages
{
    public class GetExchangeRequest : IRequest<GetExchangeResponse>
    {
        public int FromCurrency { get; set; }
        public int ToCurrency { get; set; }
    }

    public class GetExchangeRequestValidator : AbstractValidator<GetExchangeRequest>
    {
        public GetExchangeRequestValidator()
        {
            RuleFor(req => req)
                .Must(req => req.FromCurrency != req.ToCurrency)
                .WithMessage("'FromCurrenct' must be different from 'ToCurrency'");
            RuleFor(req => req.FromCurrency)
                .GreaterThan(0);
            RuleFor(req => req.ToCurrency)
                .GreaterThan(0);
        }
    }

    public class GetExchangeResponse : BaseConversionResponse
    {
    }
}
