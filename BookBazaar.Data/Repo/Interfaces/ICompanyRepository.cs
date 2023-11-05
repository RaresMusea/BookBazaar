using BookBazaar.Models.CompanyModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface ICompanyRepository : IRepository<Company>
{
    Company Update(Company company);
    Task<bool> Exists(Company book);
}