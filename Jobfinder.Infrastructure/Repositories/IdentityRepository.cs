using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
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

    public async Task<Response<User>> LoginUser(User user, string password)
    {
        if (user.Email is null)
            return Response<User>.Failure("Email is not provided");
        var currentUser = await userManager.FindByEmailAsync(user.Email);
        if (currentUser is null)
            return Response<User>.Failure("user or password is wrong.");
        var signInResult = await signInManager.CheckPasswordSignInAsync(currentUser, password, false);
        
        return signInResult.Succeeded 
            ? Response<User>.Success(currentUser)
            : Response<User>.Failure("user or password is wrong.");
    }
}