using Microsoft.EntityFrameworkCore;
using BankExchangeRateAggregator.DAL.Entities;

namespace BankExchangeRateAggregator.DAL.Data
{
    public class BankExchangeRateAggregatorContext : DbContext
    {
        public BankExchangeRateAggregatorContext(DbContextOptions<BankExchangeRateAggregatorContext> options)
            : base(options)
        {
        }

        public DbSet<BankExchangeRate> BankExchangeRate { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankExchangeRate>()
                .Property(b => b.TimeLastUpdateUtc)
                .HasDefaultValueSql("NOW()")
                .ValueGeneratedOnAdd();
        }
    }
}
