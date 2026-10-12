using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketingSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_FirstName",
                table: "TicketOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_LastName",
                table: "TicketOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress_FirstName",
                table: "TicketOrders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_LastName",
                table: "TicketOrders");
        }
    }
}
