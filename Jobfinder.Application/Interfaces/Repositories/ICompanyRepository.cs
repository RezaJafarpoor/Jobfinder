using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface ICompanyRepository
{
    Task CreateCompany(Company userCompany);
    Task UpdateCompany(Company company);
    Task<List<Company>> GetCompanies(CancellationToken cancellationToken);
    Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken);
    Task<Company?> GetCompanyByEmployerId(Guid employerId, CancellationToken cancellationToken);

    Task<string?> GetCompanyNameByUserId(Guid userId, CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

}