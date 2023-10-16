using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;

namespace BookBazaar.Data.Repo.Impl;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly AppDataContext _context;

    public BookRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public Book Update(Book book)
    {
        return _context.Books.Update(book).Entity;
    }
}