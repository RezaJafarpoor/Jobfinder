using Jobfinder.Application.Common.Models;
using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Dtos.Identity;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Identity;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Services;

internal class LoginService(SignInManager<User> signInManager
    , UserManager<User> userManager,
    TokenProvider tokenProvider,
    ApplicationDbContext dbContext) : ILoginService
{
    public async Task<Response<TokenResponse>> Login(LoginDto login, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
            return Response<TokenResponse>.Failure("Email or password is wrong");
        var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded)
            return Response<TokenResponse>.Failure("Email or password is wrong");

        var refreshToken = new RefreshToken
        {
            Token = tokenProvider.GenerateRefreshToken(),
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            User = user,
        };
        dbContext.RefreshTokens.Add(refreshToken);
        if (await dbContext.SaveChangesAsync(cancellationToken) < 0)
            return Response<TokenResponse>.Failure("Something went wrong!");
        var accessToken = tokenProvider.GenerateJwtToken(user);
        return Response<TokenResponse>.Success(new TokenResponse(AccessToken:accessToken, RefreshToken:refreshToken.Token));
    }
    
}