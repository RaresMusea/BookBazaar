using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteredOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 23, 20, 8, 562, DateTimeKind.Local).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 23, 20, 8, 562, DateTimeKind.Local).AddTicks(4527));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 23, 20, 8, 562, DateTimeKind.Local).AddTicks(4529));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 22, 49, 51, 147, DateTimeKind.Local).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 22, 49, 51, 147, DateTimeKind.Local).AddTicks(264));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 11, 5, 22, 49, 51, 147, DateTimeKind.Local).AddTicks(266));
        }
    }
}
