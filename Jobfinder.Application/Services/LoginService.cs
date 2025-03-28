using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Dtos.Identity;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Application.Services;

public class LoginService
    (IIdentityRepository identityRepository,
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository) : ILoginService
{
    public async Task<Response<TokenResponse>> LoginWithPassword(LoginDto loginDto)
    {
        var result  = await identityRepository.LoginUser(loginDto.Email, loginDto.Password);
        if (!result.IsSuccess)
            return Response<TokenResponse>.Failure(result.Errors);
        var accessToken = tokenProvider.GenerateJwtToken(result.Data!);
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!);
        if (await refreshTokenRepository.AddTokenForUser(refreshToken))
            return Response<TokenResponse>.Success(new TokenResponse(accessToken, refreshToken.Token));
        return Response<TokenResponse>.Failure("Something went wrong");
    }
}