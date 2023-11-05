using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderBasketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderBaskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Items = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBaskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderBaskets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBaskets_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBaskets_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 16, 18, 45, 251, DateTimeKind.Local).AddTicks(8588));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 16, 18, 45, 251, DateTimeKind.Local).AddTicks(8631));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 16, 18, 45, 251, DateTimeKind.Local).AddTicks(8634));

            migrationBuilder.CreateIndex(
                name: "IX_OrderBaskets_BookId",
                table: "OrderBaskets",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBaskets_InventoryItemId",
                table: "OrderBaskets",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderBaskets_UserId",
                table: "OrderBaskets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBaskets");

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 1, 57, 54, 457, DateTimeKind.Local).AddTicks(4081));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 1, 57, 54, 457, DateTimeKind.Local).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "InventoryItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 11, 4, 1, 57, 54, 457, DateTimeKind.Local).AddTicks(4125));
        }
    }
}
