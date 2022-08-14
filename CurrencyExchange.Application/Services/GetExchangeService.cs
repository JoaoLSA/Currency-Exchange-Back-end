using CurrencyExchange.Domain.Messages;
using CurrencyExchange.Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Services
{
    public class GetExchangeService : IRequestHandler<GetExchangeRequest, GetExchangeResponse>
    {
        private readonly ICurrencyRepository _currencyRepository;

        public GetExchangeService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public Task<GetExchangeResponse> Handle(GetExchangeRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
