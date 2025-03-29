using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task CreateCompany(Company userCompany);
    Task UpdateCompany(Company company);
    Task<List<Company>> GetCompanies(CancellationToken cancellationToken);
    Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken);
}