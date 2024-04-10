namespace BankExchangeRateAggregator.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }

        public string? Currency { get; set; }

        public decimal Rate { get; set; }

        public DateTime TimeLastUpdateUtc { get; set; }

        public bool IsMain => Currency == "AMD";
    }
}
