using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketingSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentIntentToTicketOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "TicketOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "TicketOrders");
        }
    }
}
