using CurrencyExchange.Domain.Messages;

namespace CurrencyExchange.Infrastructure
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public Task<GetHomeCurrenciesResponse> GetHomeCurrencies()
        {
            throw new NotImplementedException();
        }
    }

    public interface ICurrencyRepository
    {
        Task<GetHomeCurrenciesResponse> GetHomeCurrencies();
    }
}