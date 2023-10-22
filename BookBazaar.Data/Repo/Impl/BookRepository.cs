using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> Exists(Book book)
    {
        if (book is null)
        {
            return false;
        }

        return await _context.Books.FirstOrDefaultAsync(b =>
            b.Title == book.Title && b.Author == book.Author && b.Publisher == book.Publisher) is not null;
    }
}