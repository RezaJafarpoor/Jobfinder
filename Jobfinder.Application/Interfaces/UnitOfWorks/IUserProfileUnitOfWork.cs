using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IUserProfileUnitOfWork
{
    Task<Response<User>> RegisterAndCreateProfile(User user, UserType userType, string password);
}