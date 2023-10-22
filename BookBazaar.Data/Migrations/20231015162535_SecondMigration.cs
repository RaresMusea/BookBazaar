using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBazaar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DatePublished = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "DatePublished", "Description", "Isbn", "Language", "Price", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "Dale Carnegie", new DateTime(2004, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Millions of people from all around the world have improved and continue to improve their lives with the help of Dale Carnegie's ideas and techniques. In 'The Secrets of Success: How to Win Friends and Influence People',Carnegie uses an exuberant and voluble style to convey ways to break free from mental routines and live a more satisfying life. His ideas have stood the test of time and serve as tools to help you make friends quickly and easily, increase your popularity, communicate your way of thinking to others clearly, acquire new clients, become a better speaker, and become more adept in conversation. You will be able to ignite enthusiasm among your peers. The book promises to transform your relationships with all the people in your life.", "9780091906351", "English", 10.5, "Apres", "How to Win Friends and Influence People" },
                    { 2, "Jim Collins", new DateTime(2005, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Following on from 'Built To Last' which was about how to establish a successful business, this book looks at how to develop a good company into a great one.", "9780712676090", "English", 20.239999999999998, "Random House", "Good to Great" },
                    { 3, "Ion-Ovidiu Panisoara", new DateTime(2015, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "„Comunicarea se afla peste tot in jurul nostru. Sintem inconjurati de comunicare, de la comunicarea cu propria persoana (care poate sa ne conduca spre o gindire pozitiva, spre succes sau poate sa ne arunce in haosul esecului) la comunicarea cu ceilalti. Noi sintem, fiecare, o suma a tuturor interactiunilor pe care le-am avut in trecut si pe care le vom avea in viitor, sintem o parte din toti aceia cu care ne-am intilnit in viata si care si-au pus amprenta asupra modului nostru de a intelege lumea: parinti, dascali, prieteni, necunoscuti... Iata, pe scurt, dar revelator, rolul comunicarii. Avem cu totii o vasta experienta de comunicare, practic comunicam dintotdeauna. Totusi, comunicarea se invata, este un proces prin care poti trece de la simpla comunicare la comunicarea eficienta. Am incercat sa surprindem acest proces in cartea noastra pe care am intitulat-o Comunicarea eficienta, aflata acum la a patra editie, ce include aspecte pentru fiecare treapta de dezvoltare: personala si profesionala.” (Ion-Ovidiu Panisoara)", "9789734654796", "Romanian", 5.7800000000000002, "Polirom", "Comunicarea eficienta. Editia a IV-a, revazuta si adaugita" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Genre", "Priority" },
                values: new object[,]
                {
                    { 1, "Adventure", 1 },
                    { 2, "Science-Fiction", 1 },
                    { 3, "Psychology", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
