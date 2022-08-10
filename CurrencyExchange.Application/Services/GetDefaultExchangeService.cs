using CurrencyExchange.Domain.Messages;
using CurrencyExchange.Infrastructure.Database;
using MediatR;

namespace CurrencyExchange.Application.Services
{
    public class GetDefaultExchangeService : IRequestHandler<GetHomeCurrenciesRequest, GetHomeCurrenciesResponse>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetDefaultExchangeService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public async Task<GetHomeCurrenciesResponse> Handle(GetHomeCurrenciesRequest request, CancellationToken cancellationToken)
        {
            return await _currencyRepository.GetHomeCurrencies();
        }
    }
}