using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventTicketingSystem.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Billing_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ServiceFees = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event_Id = table.Column<int>(type: "int", nullable: false),
                    Event_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Event_Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TicketOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketOrderItems_TicketOrders_TicketOrderId",
                        column: x => x.TicketOrderId,
                        principalTable: "TicketOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketOrderItems_TicketOrderId",
                table: "TicketOrderItems",
                column: "TicketOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketOrderItems");

            migrationBuilder.DropTable(
                name: "TicketOrders");
        }
    }
}
