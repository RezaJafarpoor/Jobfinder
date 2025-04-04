using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IEmployerCompanyUnitOfWork : IScopedService

{
    Task<Response<string>> CreateCompany(Guid employerId, Company company, CancellationToken cancellationToken);
}