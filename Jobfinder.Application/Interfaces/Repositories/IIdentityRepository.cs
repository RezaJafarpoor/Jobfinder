using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IIdentityRepository : IScopedService
{
    Task<Response<User>> RegisterUser(User user, string password);
    Task <Response<User>> LoginUser(User user, string password);

}