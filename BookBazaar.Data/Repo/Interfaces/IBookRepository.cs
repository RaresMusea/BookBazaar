using BookBazaar.Models.BookModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Book Update(Book book);
}