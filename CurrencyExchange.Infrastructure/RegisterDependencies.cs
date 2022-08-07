using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Infrastructure
{
    public static class RegisterDependencies
    {
        public static void RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        }
    }
}
