using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class LoginService
    (IIdentityRepository identityRepository,
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository) 
{
    public async Task<Response<TokenResponse>> LoginWithPassword(User user, string password)
    {
        var result  = await identityRepository.LoginUser(user, password);
        if (!result.IsSuccess)
            return Response<TokenResponse>.Failure(result.Errors);
        var accessToken = tokenProvider.GenerateJwtToken(result.Data!);
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!);
        if (await refreshTokenRepository.AddTokenForUser(refreshToken))
            return Response<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken.Token));
        return Response<TokenResponse>.Failure("Something went wrong");
    }
}