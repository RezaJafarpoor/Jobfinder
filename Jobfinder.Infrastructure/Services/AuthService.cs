using Jobfinder.Application.Common.Models;
using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Services;

internal class AuthService(ApplicationDbContext dbContext,
    UserManager<User> userManager,
    TokenProvider tokenProvider
    ) : IAuthService
{

    public async Task<Response<TokenResponse>> RegisterJobSeeker(RegisterDto register, CancellationToken cancellationToken)
    {
        var user = new User { UserName = register.Email, Email = register.Email };
        var createdUser = await userManager.CreateAsync(user, register.Password);
        if (!createdUser.Succeeded)
        {
            var errors = IdentityResult.Failed().Errors.Select(d => d.Description).ToList();
            return Response<TokenResponse>.Failure(errors);
        }

        var profile = new JobSeekerProfile { User = user};
        dbContext.JobSeekerProfiles.Add(profile);
        var accessToken = tokenProvider.GenerateJwtToken(user);
        var refreshToken = tokenProvider.GenerateRefreshToken();
        var token = new RefreshToken
        {
            User = user,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            Token = refreshToken
        };
         dbContext.RefreshTokens.Add(token);
         if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
         {
             var tokenResponse = new TokenResponse(AccessToken:accessToken, RefreshToken: refreshToken );
             return Response<TokenResponse>.Success(tokenResponse);
         }
         
         return Response<TokenResponse>.Failure("Something Went Wrong");

    }
    
}