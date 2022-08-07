using MediatR;

namespace CurrencyExchange.Domain.Messages
{
    public class GetHomeCurrenciesRequest : IRequest<GetHomeCurrenciesResponse>
    {

    }
    public class GetHomeCurrenciesResponse
    {
        public CurrencyDetail? FromCurrency { get; set; }
        public CurrencyDetail? ToCurrency { get; set; }
    }
}