﻿// <auto-generated />
using System;
using BookBazaar.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookBazaar.Data.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20231022205716_AddedCoverImagePropertyToBookEntity")]
    partial class AddedCoverImagePropertyToBookEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookBazaar.Models.BookModels.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CoverImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Dale Carnegie",
                            CategoryId = 5,
                            CoverImageUrl = "",
                            DatePublished = new DateTime(2004, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Millions of people from all around the world have improved and continue to improve their lives with the help of Dale Carnegie's ideas and techniques. In 'The Secrets of Success: How to Win Friends and Influence People',Carnegie uses an exuberant and voluble style to convey ways to break free from mental routines and live a more satisfying life. His ideas have stood the test of time and serve as tools to help you make friends quickly and easily, increase your popularity, communicate your way of thinking to others clearly, acquire new clients, become a better speaker, and become more adept in conversation. You will be able to ignite enthusiasm among your peers. The book promises to transform your relationships with all the people in your life.",
                            Isbn = "9780091906351",
                            Language = "English",
                            Price = 10.5,
                            Publisher = "Apres",
                            Title = "How to Win Friends and Influence People"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Jim Collins",
                            CategoryId = 3,
                            CoverImageUrl = "",
                            DatePublished = new DateTime(2005, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Following on from 'Built To Last' which was about how to establish a successful business, this book looks at how to develop a good company into a great one.",
                            Isbn = "9780712676090",
                            Language = "English",
                            Price = 20.239999999999998,
                            Publisher = "Random House",
                            Title = "Good to Great"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Ion-Ovidiu Panisoara",
                            CategoryId = 1,
                            CoverImageUrl = "",
                            DatePublished = new DateTime(2015, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "„Comunicarea se afla peste tot in jurul nostru. Sintem inconjurati de comunicare, de la comunicarea cu propria persoana (care poate sa ne conduca spre o gindire pozitiva, spre succes sau poate sa ne arunce in haosul esecului) la comunicarea cu ceilalti. Noi sintem, fiecare, o suma a tuturor interactiunilor pe care le-am avut in trecut si pe care le vom avea in viitor, sintem o parte din toti aceia cu care ne-am intilnit in viata si care si-au pus amprenta asupra modului nostru de a intelege lumea: parinti, dascali, prieteni, necunoscuti... Iata, pe scurt, dar revelator, rolul comunicarii. Avem cu totii o vasta experienta de comunicare, practic comunicam dintotdeauna. Totusi, comunicarea se invata, este un proces prin care poti trece de la simpla comunicare la comunicarea eficienta. Am incercat sa surprindem acest proces in cartea noastra pe care am intitulat-o Comunicarea eficienta, aflata acum la a patra editie, ce include aspecte pentru fiecare treapta de dezvoltare: personala si profesionala.” (Ion-Ovidiu Panisoara)",
                            Isbn = "9789734654796",
                            Language = "Romanian",
                            Price = 5.7800000000000002,
                            Publisher = "Polirom",
                            Title = "Comunicarea eficienta. Editia a IV-a, revazuta si adaugita"
                        });
                });

            modelBuilder.Entity("BookBazaar.Models.CategoryModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genre = "Adventure",
                            Priority = 1
                        },
                        new
                        {
                            Id = 2,
                            Genre = "Science-Fiction",
                            Priority = 1
                        },
                        new
                        {
                            Id = 3,
                            Genre = "Psychology",
                            Priority = 1
                        });
                });

            modelBuilder.Entity("BookBazaar.Models.BookModels.Book", b =>
                {
                    b.HasOne("BookBazaar.Models.CategoryModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
