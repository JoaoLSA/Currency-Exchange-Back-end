namespace CurrencyExchange.Domain.Messages
{
    public abstract class BaseConversionResponse
    {
        public CurrencyDetail? FromCurrency { get; set; }
        public CurrencyDetail? ToCurrency { get; set; }
    }
}
