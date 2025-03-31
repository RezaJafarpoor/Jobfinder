using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Dtos.Profiles;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IUserProfileUnitOfWork
{
    Task<Response<User>> RegisterAndCreateProfile(string  userEmail, UserType userType, string password);
    Task<Response<@EmployerProfile>> LoginAsEmployer(string userEmail,  string password, CancellationToken cancellationToken);
    Task<Response<@JobSeekerProfile>> LoginAsJobSeeker(string userEmail,  string password, CancellationToken cancellationToken);

}