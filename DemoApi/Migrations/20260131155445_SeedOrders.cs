using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "UserId", "TotalAmount", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 120.50m, DateTime.UtcNow },
                    { 1, 75.00m, DateTime.UtcNow },
                    { 2, 300.00m, DateTime.UtcNow }
                }
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
