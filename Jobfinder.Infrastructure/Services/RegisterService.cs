using Jobfinder.Application.Common.Models;
using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Dtos.Identity;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Services;

internal class RegisterService(ApplicationDbContext dbContext,
    UserManager<User> userManager,
    TokenProvider tokenProvider
    ) : IRegisterService
{

    public async Task<Response<TokenResponse>> RegisterJobSeekerProfile(RegisterDto register, CancellationToken cancellationToken)
    {
        var newUser = new User { UserName = register.Email, Email = register.Email };
        var createdUser = await userManager.CreateAsync(newUser, register.Password);
        if (!createdUser.Succeeded)
        {
            var errors = createdUser.Errors.Select(error => error.Description).ToList();
            return Response<TokenResponse>.Failure(errors);
        }

        var profile = new JobSeekerProfile { User = newUser };
        dbContext.JobSeekerProfiles.Add(profile);
        // var refreshToken = new RefreshToken
        // {
        //     User = newUser,
        //     ExpirationDate = DateTime.UtcNow.AddDays(7),
        //     Token = tokenProvider.GenerateRefreshToken()
        // };
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), newUser, DateTime.UtcNow.AddDays(7));
         dbContext.RefreshTokens.Add(refreshToken);
         if (await dbContext.SaveChangesAsync(cancellationToken) <= 0)
             return Response<TokenResponse>.Failure("Something Went Wrong");
         
         var accessToken = tokenProvider.GenerateJwtToken(newUser);
         var tokenResponse = new TokenResponse(AccessToken:accessToken, RefreshToken: refreshToken.Token );
         return Response<TokenResponse>.Success(tokenResponse);

    }


    public async Task<Response<TokenResponse>> RegisterEmployerProfile(RegisterDto register, CancellationToken cancellationToken)
    {

        var newUser = new User { UserName = register.Email, Email = register.Email };
        var createdUser = await userManager.CreateAsync(newUser, register.Password);
        if (!createdUser.Succeeded)
        {
            var errors = createdUser.Errors.Select(error => error.Description).ToList();
            return Response<TokenResponse>.Failure(errors);
        }

        var profile = new EmployerProfile { User = newUser };
        dbContext.EmployerProfiles.Add(profile);
        // var refreshToken = new RefreshToken
        // {
        //     Token = tokenProvider.GenerateRefreshToken(),
        //     ExpirationDate = DateTime.UtcNow.AddDays(7),
        //     User = newUser,
        // };
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), newUser, DateTime.UtcNow.AddDays(7));

        dbContext.RefreshTokens.Add(refreshToken);
        if (await dbContext.SaveChangesAsync(cancellationToken)< 0)
        {
            return Response<TokenResponse>.Failure("Something Went wrong during user registration.");            
        }
        var accessToken = tokenProvider.GenerateJwtToken(newUser);
        return Response<TokenResponse>.Success(new TokenResponse(AccessToken: accessToken, RefreshToken: refreshToken.Token));


    }
}