using MediatR;

namespace CurrencyExchange.Domain.Messages
{
    public class GetHomeCurrenciesRequest : IRequest<GetHomeCurrenciesResponse>
    {

    }
    public class GetHomeCurrenciesResponse : BaseConversionResponse
    {
    }
}