
using Jobfinder.Domain.Commons.Identity;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Domain.Entities;

public sealed class User : IdentityUser<Guid>
{

    public Roles UserRole { get; set; }
    public User()
    {
        
    }

    public User(string email, Roles userRole)
    {
        UserRole = userRole;
        Email = email;
        UserName = email;
    }
}