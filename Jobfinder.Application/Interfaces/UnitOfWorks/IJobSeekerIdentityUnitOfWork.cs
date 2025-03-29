using Jobfinder.Application.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobSeekerIdentityUnitOfWork
{
    Task<Response<User>> RegisterJobSeeker(User user, string password);
}