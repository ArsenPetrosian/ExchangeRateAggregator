using BankExchangeRateAggregator.DAL.Entities;

namespace BankExchangeRateAggregator.BLL.Services
{
    public interface IExchangeRateService
    {
        public Task UpdateExchangeRatesByApi();

        public Task UpdateExchangeRate(BankExchangeRate bankExchangeRate);

        public Task<IEnumerable<BankExchangeRate>> GetExchangeRates();

        public Task<IEnumerable<BankExchangeRate>> GetExchangeRatesOrderedByCurrency();

        public Task<BankExchangeRate> GetExchangeRateById(int? id);

        public Task<IEnumerable<BankExchangeRate>> GetExchangeRatesByCurrency(string currency);

        public Task<bool> ExchangeRateExists(int id);
    }
}
