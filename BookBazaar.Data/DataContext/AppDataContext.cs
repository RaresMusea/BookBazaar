using BookBazaar.Models.BookModels;
using BookBazaar.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.Data.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Genre = "Adventure", Priority = 1 },
            new Category { Id = 2, Genre = "Science-Fiction", Priority = 1 },
            new Category { Id = 3, Genre = "Psychology", Priority = 1 }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1, Title = "How to Win Friends and Influence People", Author = "Dale Carnegie", CategoryId = 5,
                Description =
                    "Millions of people from all around the world have improved and continue to improve their lives" +
                    " with the help of Dale Carnegie's ideas and techniques. In 'The Secrets of Success: How to Win" +
                    " Friends and Influence People',Carnegie uses an exuberant and voluble style to convey ways to break" +
                    " free from mental routines and live a more satisfying life. His ideas have stood the test of time" +
                    " and serve as tools to help you make friends quickly and easily, increase your popularity," +
                    " communicate your way of thinking to others clearly, acquire new clients, become a better speaker," +
                    " and become more adept in conversation. You will be able to ignite enthusiasm among your peers." +
                    " The book promises to transform your relationships with all the people in your life.",
                DatePublished = new DateTime(2004, 4, 15),
                CoverImageUrl = "",
                Isbn = "9780091906351",
                Price = 10.5,
                Language = "English",
                Publisher = "Apres"
            },
            new Book
            {
                Id = 2, Title = "Good to Great", Author = "Jim Collins", CategoryId = 3, CoverImageUrl = "",
                Description =
                    "Following on from 'Built To Last' which was about how to establish a successful business," +
                    " this book looks at how to develop a good company into a great one.",
                DatePublished = new DateTime(2005, 2, 13),
                Isbn = "9780712676090",
                Price = 20.24,
                Language = "English",
                Publisher = "Random House"
            },
            new Book
            {
                Id = 3, Title = "Comunicarea eficienta. Editia a IV-a, revazuta si adaugita", CategoryId = 1,
                Author = "Ion-Ovidiu Panisoara", CoverImageUrl = "",
                Description = "„Comunicarea se afla peste tot in jurul nostru. Sintem inconjurati de comunicare," +
                              " de la comunicarea cu propria persoana (care poate sa ne conduca spre o gindire pozitiva," +
                              " spre succes sau poate sa ne arunce in haosul esecului) la comunicarea cu ceilalti. Noi" +
                              " sintem, fiecare, o suma a tuturor interactiunilor pe care le-am avut in trecut si pe" +
                              " care le vom avea in viitor, sintem o parte din toti aceia cu care ne-am intilnit in" +
                              " viata si care si-au pus amprenta asupra modului nostru de a intelege lumea: parinti," +
                              " dascali, prieteni, necunoscuti... Iata, pe scurt, dar revelator, rolul comunicarii." +
                              " Avem cu totii o vasta experienta de comunicare, practic comunicam dintotdeauna. Totusi," +
                              " comunicarea se invata, este un proces prin care poti trece de la simpla comunicare la comunicarea" +
                              " eficienta. Am incercat sa surprindem acest proces in cartea noastra pe care am" +
                              " intitulat-o Comunicarea eficienta, aflata acum la a patra editie, ce include aspecte" +
                              " pentru fiecare treapta de dezvoltare: personala si profesionala.” (Ion-Ovidiu Panisoara)",
                DatePublished = new DateTime(2015, 6, 23),
                Isbn = "9789734654796",
                Price = 5.78,
                Language = "Romanian",
                Publisher = "Polirom",
            }
        );
    }
}