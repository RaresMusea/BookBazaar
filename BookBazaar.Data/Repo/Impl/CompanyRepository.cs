using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CompanyModels;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.Data.Repo.Impl;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    private readonly AppDataContext _context;

    public CompanyRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public Company Update(Company company)
    {
        return _context.Companies.Update(company).Entity;
    }

    public async Task<bool> Exists(Company company)
    {
        if (company is null)
        {
            return false;
        }

        return await _context.Companies.FirstOrDefaultAsync(comp =>
            comp.Name == company.Name && comp.Id == company.Id && comp.Email == company.Email) is not null;
    }
}