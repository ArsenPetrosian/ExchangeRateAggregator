using BankExchangeRateAggregator.DAL.Entities;

namespace BankExchangeRateAggregator.DAL.Repositories
{
    public interface IBankExchangeRateRepository
    {
        Task<List<BankExchangeRate>> Get();

        Task<BankExchangeRate> GetExchangeRateById(int id);

        Task<BankExchangeRate> GetByCurrency(string currency);

        Task<IEnumerable<BankExchangeRate>> GetExchangeRatesOrderedByCurrency();

        Task<IEnumerable<BankExchangeRate>> GetExchangeRatesByCurrency(string currency);

        Task Create(IEnumerable<BankExchangeRate> exchangeRates);

        Task Create(BankExchangeRate rate);

        Task<bool> ExchangeRateExists(int id);

        Task UpdateExchangeRate(BankExchangeRate rate);

        Task Update(BankExchangeRate rate);

        Task Update(IEnumerable<BankExchangeRate> rates);
    }
}
