using  Jobfinder.Domain.Commons;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Repositories;

internal class IdentityRepository
    (UserManager<User> userManager,
        SignInManager<User> signInManager) : IIdentityRepository
{
    public async Task<Response<User>> RegisterUser(User user, string password)
    {
        var createdUser = await userManager.CreateAsync(user, password);
        if (createdUser.Succeeded)
            return Response<User>.Success(user);
        
        var errors = createdUser.Errors.Select(e => e.Description).ToList();
        return Response<User>.Failure(errors);

    }

    public async Task<Response<User>> LoginUser(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return Response<User>.Failure("user or password is wrong.");
        var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
        
        return signInResult.Succeeded 
            ? Response<User>.Success(user)
            : Response<User>.Failure("user or password is wrong.");
    }
}