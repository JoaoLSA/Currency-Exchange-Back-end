using CurrencyExchange.Domain.Messages.CurrencyScoopClient;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Infrastructure
{
    public class CurrencyScoopClient : ICurrencyScoopClient
    {
        private readonly RestClient _client;
        private readonly string _apiKey;

        public CurrencyScoopClient(IConfiguration configuration)
        {
            var currencyScoopEndpoint = configuration!.GetSection("CurrencyScoop:BaseUrl").Value;
            var options = new RestClientOptions(currencyScoopEndpoint)
            {
                ThrowOnAnyError = true,
                MaxTimeout = 10000
            };
            _client = new RestClient(options);
            _apiKey = "";
        }
        public async Task<GetConvertionResponse?> GetConvertionAsync(GetConvertionRequest request)
        {
            var clientRequest = new RestRequest("convert");
            clientRequest.AddParameter("api_key", _apiKey);
            clientRequest.AddObject(request);

            return await _client.GetAsync<GetConvertionResponse>(clientRequest);
        }
    }

    public interface ICurrencyScoopClient
    {
        Task<GetConvertionResponse?> GetConvertionAsync(GetConvertionRequest request);
    }
}
