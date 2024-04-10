using BankExchangeRateAggregator.BLL.Models;
using BankExchangeRateAggregator.DAL.Entities;
using BankExchangeRateAggregator.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BankExchangeRateAggregator.BLL.Services.Implementations
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly BankExchangeRateSettings _settings = new BankExchangeRateSettings();
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBankExchangeRateRepository _rateRepository;

        public ExchangeRateService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IBankExchangeRateRepository rateRepository)
        {
            _httpClientFactory = httpClientFactory;
            configuration.GetSection("ExchangeRateAPISettings").Bind(_settings);
            _rateRepository = rateRepository;
        }

        public async Task UpdateExchangeRatesByApi()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_settings.ApiUrl}{_settings.ApiKey}/latest/AMD");

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<ExchangeRateApiResponse>(contentStream);

                if (response != null)
                {
                    var currentTimeUtc = DateTime.UtcNow;

                    var existingRates = await _rateRepository.Get();
                    var ratesToUpdate = new List<BankExchangeRate>();
                    var ratesToCreate = new List<BankExchangeRate>();

                    foreach (var exchangeRate in response.ConversionRates)
                    {
                        var currency = exchangeRate.Key;
                        var rate = 1M / exchangeRate.Value;

                        var existingRate = existingRates.FirstOrDefault(x => x.Currency == currency);

                        if (existingRate != null)
                        {
                            existingRate.Rate = rate;
                            existingRate.TimeLastUpdateUtc = currentTimeUtc;
                            ratesToUpdate.Add(existingRate);
                        }
                        else
                        {
                            var newRate = new BankExchangeRate
                            {
                                Currency = currency,
                                Rate = rate,
                                TimeLastUpdateUtc = currentTimeUtc
                            };
                            ratesToCreate.Add(newRate);
                        }
                    }

                    await _rateRepository.Update(ratesToUpdate);
                    await _rateRepository.Create(ratesToCreate);
                }
            }
            else
            {
                throw new Exception($"HTTP request failed with status code: {httpResponseMessage.StatusCode}");
            }
        }


        public async Task UpdateExchangeRate(BankExchangeRate bankExchangeRate)
        {
            if (bankExchangeRate == null)
            {
                throw new ArgumentNullException(nameof(bankExchangeRate));
            }

            var existingRate = await _rateRepository.GetExchangeRateById(bankExchangeRate.Id);
            if (existingRate == null)
            {
                throw new InvalidOperationException($"Bank exchange rate with ID {bankExchangeRate.Id} not found.");
            }

            existingRate.Currency = bankExchangeRate.Currency;
            existingRate.Rate = bankExchangeRate.Rate;
            existingRate.TimeLastUpdateUtc = bankExchangeRate.TimeLastUpdateUtc;

            await _rateRepository.UpdateExchangeRate(existingRate);
        }

        public async Task<IEnumerable<BankExchangeRate>> GetExchangeRates()
        {
            return await _rateRepository.Get();
        }

        public async Task<IEnumerable<BankExchangeRate>> GetExchangeRatesOrderedByCurrency()
        {
            return await _rateRepository.GetExchangeRatesOrderedByCurrency();
        }

        public async Task<BankExchangeRate?> GetExchangeRateById(int? id)
        {
            if (id.HasValue)
            {
                return await _rateRepository.GetExchangeRateById(id.Value);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<BankExchangeRate>> GetExchangeRatesByCurrency(string currency)
        {
            return await _rateRepository.GetExchangeRatesByCurrency(currency);
        }

        public async Task<bool> ExchangeRateExists(int id)
        {
            return await _rateRepository.ExchangeRateExists(id);
        }
    }
}
