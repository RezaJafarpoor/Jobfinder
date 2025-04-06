
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Domain.Entities;

public sealed class User : IdentityUser<Guid>  
{

    
    public User(){}

    public User(string email)
    {
        Email = email;
        UserName = email;
    }
}