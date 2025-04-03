using Jobfinder.Application.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IEmployerCompanyUnitOfWork
{
    Task<Response<string>> CreateCompany(Guid employerId, Company company, CancellationToken cancellationToken);
}