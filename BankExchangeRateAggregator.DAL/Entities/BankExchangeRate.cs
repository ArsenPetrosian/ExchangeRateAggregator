using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BankExchangeRateAggregator.DAL.Entities
{
    [Index(nameof(Currency), IsUnique = true)]
    public class BankExchangeRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotNull]
        public required string Currency { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than zero.")]
        public decimal Rate { get; set; }

        [NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TimeLastUpdateUtc { get; set; }
    }
}
