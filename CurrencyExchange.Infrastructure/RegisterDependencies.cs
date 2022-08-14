using CurrencyExchange.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Infrastructure
{
    public static class RegisterDependencies
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddSingleton<ICurrencyScoopClient, CurrencyScoopClient>();
        }
    }
}
