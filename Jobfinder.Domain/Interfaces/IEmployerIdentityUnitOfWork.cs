using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IEmployerIdentityUnitOfWork
{
    Task<Response<User>> RegisterEmployer(User user, string password);
}