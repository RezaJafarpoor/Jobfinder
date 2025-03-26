using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<bool> CreateCompany();
    Task<bool> UpdateCompany();
    Task<List<Company>> GetCompanies();
    Task<Company> GetCompanyById(Guid id);
}