﻿// <auto-generated />
using System;
using BookBazaar.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookBazaar.Data.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

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
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Dale Carnegie",
                            CategoryId = 1,
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
                            CategoryId = 2,
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

            modelBuilder.Entity("BookBazaar.Models.CompanyModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadquartersAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IncorporationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<int>("NumberOfEmployees")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Brasov",
                            Country = "Romania",
                            Email = "contact@libris_bookstore.ro",
                            HeadquartersAddress = "123 Strada Florilor",
                            IncorporationDate = new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Libris Romania",
                            NumberOfEmployees = 45,
                            Phone = "+40 783 214 903"
                        },
                        new
                        {
                            Id = 2,
                            City = "Frankfurt",
                            Country = "Germany",
                            Email = "kontact@bibliophilic-heaven.de",
                            HeadquartersAddress = "45 Musterstraße",
                            IncorporationDate = new DateTime(1999, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Bibliophilic Haven",
                            NumberOfEmployees = 200,
                            Phone = "+49 789 214 981"
                        },
                        new
                        {
                            Id = 3,
                            City = "Belfast",
                            Country = "UK",
                            Email = "wordsmiths_retreat_library@gmail.com",
                            HeadquartersAddress = "42 Larkspur Crescent",
                            IncorporationDate = new DateTime(2010, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Wordsmith's Retreat Library",
                            NumberOfEmployees = 10,
                            Phone = "+44 20 1234 5678"
                        });
                });

            modelBuilder.Entity("BookBazaar.Models.InventoryModels.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<int>("QuantitySold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            DateAdded = new DateTime(2023, 11, 2, 22, 31, 34, 968, DateTimeKind.Local).AddTicks(4445),
                            QuantityInStock = 40,
                            QuantitySold = 0
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            DateAdded = new DateTime(2023, 11, 2, 22, 31, 34, 968, DateTimeKind.Local).AddTicks(4481),
                            QuantityInStock = 60,
                            QuantitySold = 0
                        },
                        new
                        {
                            Id = 3,
                            BookId = 3,
                            DateAdded = new DateTime(2023, 11, 2, 22, 31, 34, 968, DateTimeKind.Local).AddTicks(4484),
                            QuantityInStock = 20,
                            QuantitySold = 0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BookBazaar.Models.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
