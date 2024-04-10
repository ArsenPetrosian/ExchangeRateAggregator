using Microsoft.EntityFrameworkCore;
using BankExchangeRateAggregator.DAL.Data;
using BankExchangeRateAggregator.DAL.Repositories;
using BankExchangeRateAggregator.BLL.Services;
using BankExchangeRateAggregator.BLL.Services.Implementations;

namespace BankExchangeRateAggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankExchangeRateAggregatorContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("BankExchangeRateAggregatorContext")));

            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
            builder.Services.AddScoped<IBankExchangeRateRepository, BankExchangeRateRepository>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
