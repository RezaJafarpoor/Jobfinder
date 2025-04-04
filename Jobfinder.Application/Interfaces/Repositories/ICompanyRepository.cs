using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface ICompanyRepository : IScopedService
{
    Task CreateCompany(Company userCompany);
    Task UpdateCompany(Company company);
    Task<List<Company>> GetCompanies(CancellationToken cancellationToken);
    Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken);
    Task<Company?> GetCompanyByEmployerId(Guid employerId, CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

}