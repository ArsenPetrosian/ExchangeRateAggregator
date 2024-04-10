using BankExchangeRateAggregator.DAL.Data;
using BankExchangeRateAggregator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankExchangeRateAggregator.DAL.Repositories
{
    public class BankExchangeRateRepository : IBankExchangeRateRepository
    {
        private readonly BankExchangeRateAggregatorContext _context;

        public BankExchangeRateRepository(BankExchangeRateAggregatorContext context)
        {
            _context = context;
        }

        public async Task<List<BankExchangeRate>> Get()
        {
            return await _context.BankExchangeRate.ToListAsync();
        }

        public async Task<BankExchangeRate> GetExchangeRateById(int id)
        {
            return await _context.BankExchangeRate.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<BankExchangeRate> GetByCurrency(string currency)
        {
            return await _context.BankExchangeRate
                .FirstOrDefaultAsync(rate => rate.Currency == currency);
        }

        public async Task<IEnumerable<BankExchangeRate>> GetExchangeRatesOrderedByCurrency()
        {
            return await _context.BankExchangeRate
                .OrderBy(rate => rate.Currency != "AMD")
                .ThenBy(rate => rate.Currency)
                .ToListAsync();
        }

        public async Task<IEnumerable<BankExchangeRate>> GetExchangeRatesByCurrency(string currency)
        {
            return await Task.FromResult(_context.BankExchangeRate
                .Where(rate => rate.Currency.ToLower().Contains(currency.ToLower()))
                .ToList());
        }

        public async Task Create(IEnumerable<BankExchangeRate> exchangeRates)
        {
            _context.BankExchangeRate.AddRange(exchangeRates);
            await _context.SaveChangesAsync();
        }

        public async Task Create(BankExchangeRate rate)
        {
            _context.BankExchangeRate.Add(rate);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExchangeRateExists(int id)
        {
            return await _context.BankExchangeRate.AnyAsync(e => e.Id == id);
        }

        public async Task UpdateExchangeRate(BankExchangeRate rate)
        {
            _context.Entry(rate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Update(BankExchangeRate rate)
        {
            _context.Update(rate);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IEnumerable<BankExchangeRate> rates)
        {
            foreach (var rate in rates)
            {
                _context.Entry(rate).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }
    }
}
