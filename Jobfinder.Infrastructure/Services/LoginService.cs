using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.Services;
using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Services;

public sealed class LoginService(
    ITokenProvider tokenProvider,
    IRefreshTokenRepository refreshTokenRepository,
    UserManager<User> userManager,
    SignInManager<User> signInManager) : ILoginService
{
    public async Task<Response<IdentityResponse>> LoginWithPassword(LoginDto login, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
            return Response<IdentityResponse>.Failure("User or password is wrong");
        var loggedUser = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!loggedUser.Succeeded)
            return Response<IdentityResponse>.Failure("User or password is wrong");
        var refreshToken = new RefreshToken
            (tokenProvider.GenerateRefreshToken(), user);
        await refreshTokenRepository.AddToken(refreshToken);
        if (!await refreshTokenRepository.SaveChanges(cancellationToken))
            return Response<IdentityResponse>.Failure("Something went wrong!");
        var accessToken = tokenProvider.GenerateJwtToken(user.Id, user.UserRole.ToString());
        var response = new IdentityResponse(AccessToken: accessToken, RefreshToken: refreshToken.Token);
        return Response<IdentityResponse>.Success(response); 
    }
}