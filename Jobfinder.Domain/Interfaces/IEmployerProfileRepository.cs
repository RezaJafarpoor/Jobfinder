using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IEmployerProfileRepository
{
    Task CreateProfile(User user);
    

}