using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IIdentityRepository
{
    Task<Response<User>> RegisterUser(User user, string password);
    Task <Response<User>> LoginUser(string email, string password);

}