using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IUserProfileUnitOfWork
{
    Task<Response<JobSeekerProfile>> RegisterAsJobSeeker(string  userEmail, string password);    
    Task<Response<EmployerProfile>> RegisterAsEmployer(string  userEmail, string password);

    Task<Response<@EmployerProfile>> LoginAsEmployer(string userEmail,  string password, CancellationToken cancellationToken);
    Task<Response<@JobSeekerProfile>> LoginAsJobSeeker(string userEmail,  string password, CancellationToken cancellationToken);

}