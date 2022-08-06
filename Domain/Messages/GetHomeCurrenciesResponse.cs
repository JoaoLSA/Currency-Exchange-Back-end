namespace Domain.Messages
{
    public class GetHomeCurrenciesResponse
    {
        public CurrencyDetail FromCurrency { get; set; }
        public CurrencyDetail ToCurrency { get; set; }
    }
}