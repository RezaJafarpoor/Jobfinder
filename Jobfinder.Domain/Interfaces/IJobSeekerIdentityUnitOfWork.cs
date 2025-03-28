using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobSeekerIdentityUnitOfWork
{
    Task<Response<User>> RegisterJobSeeker(User user, string password);
}