using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankExchangeRateAggregator.DAL.Entities;
using BankExchangeRateAggregator.BLL.Services;
using BankExchangeRateAggregator.Models;

namespace BankExchangeRateAggregator.Controllers
{
    public class BankExchangeRatesController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;

        public BankExchangeRatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        // GET: BankExchangeRates
        public async Task<IActionResult> Index()
        {
            var bankExchangeRates = await _exchangeRateService.GetExchangeRatesOrderedByCurrency();
            var exchangeRates = bankExchangeRates.Select(rate => new ExchangeRate
            {
                Id = rate.Id,
                Currency = rate.Currency,
                Rate = rate.Rate,
                TimeLastUpdateUtc = rate.TimeLastUpdateUtc
            });
            return View(exchangeRates);
        }

        // GET: BankExchangeRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankExchangeRate = await _exchangeRateService.GetExchangeRateById(id.Value);
            if (bankExchangeRate == null)
            {
                return NotFound();
            }

            var exchangeRate = new ExchangeRate
            {
                Id = bankExchangeRate.Id,
                Currency = bankExchangeRate.Currency,
                Rate = bankExchangeRate.Rate,
                TimeLastUpdateUtc = bankExchangeRate.TimeLastUpdateUtc
            };

            return View(exchangeRate);
        }

        // GET: BankExchangeRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bankExchangeRate = await _exchangeRateService.GetExchangeRateById(id.Value);
            if (bankExchangeRate == null)
            {
                return NotFound();
            }

            var exchangeRate = new ExchangeRate
            {
                Id = bankExchangeRate.Id,
                Currency = bankExchangeRate.Currency,
                Rate = bankExchangeRate.Rate,
                TimeLastUpdateUtc = bankExchangeRate.TimeLastUpdateUtc
            };

            return View(exchangeRate);
        }


        // POST: BankExchangeRates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Currency,Rate")] ExchangeRate exchangeRate)
        {
            if (id == null || id != exchangeRate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bankExchangeRate = new BankExchangeRate
                    {
                        Id = id.Value,
                        Currency = exchangeRate.Currency,
                        Rate = exchangeRate.Rate,
                        TimeLastUpdateUtc = DateTime.UtcNow
                    };

                    await _exchangeRateService.UpdateExchangeRate(bankExchangeRate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _exchangeRateService.ExchangeRateExists(exchangeRate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exchangeRate);
        }


        // GET: BankExchangeRates/Update
        public async Task<IActionResult> Update()
        {
            await _exchangeRateService.UpdateExchangeRatesByApi();
            return RedirectToAction("Index");
        }

        // GET: BankExchangeRates
        [HttpGet("Search")]
        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<ExchangeRate> exchangeRates;

            if (!string.IsNullOrEmpty(search))
            {
                var bankExchangeRates = await _exchangeRateService.GetExchangeRatesByCurrency(search);
                exchangeRates = bankExchangeRates.Select(rate => new ExchangeRate
                {
                    Id = rate.Id,
                    Currency = rate.Currency,
                    Rate = rate.Rate,
                    TimeLastUpdateUtc = rate.TimeLastUpdateUtc
                });
            }
            else
            {
                var bankExchangeRates = await _exchangeRateService.GetExchangeRatesOrderedByCurrency();
                exchangeRates = bankExchangeRates.Select(rate => new ExchangeRate
                {
                    Id = rate.Id,
                    Currency = rate.Currency,
                    Rate = rate.Rate,
                    TimeLastUpdateUtc = rate.TimeLastUpdateUtc
                });
            }

            return View(exchangeRates);
        }
    }
}
