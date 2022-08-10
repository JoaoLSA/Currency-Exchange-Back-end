using CurrencyExchange.Domain.Messages;

namespace CurrencyExchange.Infrastructure.Database
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ICurrencyScoopClient _currencyScoopClient;

        public CurrencyRepository(ICurrencyScoopClient currencyScoopClient)
        {
            _currencyScoopClient = currencyScoopClient;
        }
        public async Task<GetHomeCurrenciesResponse> GetHomeCurrencies()
        {
            var clientResponse = await _currencyScoopClient.GetConvertionAsync(new()
            {
                From = "USD",
                To = "BRL",
                Amount = 1,
            });

            return new()
            {
                FromCurrency = new()
                {
                    Id = 1,
                    Value = clientResponse!.Response!.Amount
                },
                ToCurrency = new()
                {
                    Id = 2,
                    Value = clientResponse!.Response!.Value
                }
            };
        }
    }

    public interface ICurrencyRepository
    {
        Task<GetHomeCurrenciesResponse> GetHomeCurrencies();
    }
}