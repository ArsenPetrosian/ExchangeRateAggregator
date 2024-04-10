using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankExchangeRateAggregator.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeLastUpdateUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeLastUpdateUtc",
                table: "BankExchangeRate",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()");

            migrationBuilder.CreateIndex(
                name: "IX_BankExchangeRate_Currency",
                table: "BankExchangeRate",
                column: "Currency",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BankExchangeRate_Currency",
                table: "BankExchangeRate");

            migrationBuilder.DropColumn(
                name: "TimeLastUpdateUtc",
                table: "BankExchangeRate");
        }
    }
}
