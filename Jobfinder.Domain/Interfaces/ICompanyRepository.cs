using Jobfinder.Domain.Dtos.Company;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface ICompanyRepository
{
    Task<bool> CreateCompany(Guid userId,CreateCompanyDto companyDto, CancellationToken cancellationToken);
    Task<bool> UpdateCompany(Guid userId,UpdateCompanyDto companyDto, CancellationToken cancellationToken);
    Task<List<Company>> GetCompanies(CancellationToken cancellationToken);
    Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken);
}