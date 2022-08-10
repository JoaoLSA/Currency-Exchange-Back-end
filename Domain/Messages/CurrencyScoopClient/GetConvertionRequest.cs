using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Domain.Messages.CurrencyScoopClient
{
    public class GetConvertionRequest
    {
        public string? From { get; set; }
        public string? To { get; set; }
        public decimal Amount { get; set; }
    }
    public class GetConvertionResponse
    {
        public Response? Response { get; set; }
    }

    public class Response
    {
        public int Timespan { get; set; }
        public DateTime Date { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
    }
}
