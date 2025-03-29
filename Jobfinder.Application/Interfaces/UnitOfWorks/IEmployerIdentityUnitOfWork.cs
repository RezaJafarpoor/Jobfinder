using Jobfinder.Application.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IEmployerIdentityUnitOfWork
{
    Task<Response<User>> RegisterEmployer(User user, string password);
}