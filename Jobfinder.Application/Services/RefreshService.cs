using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;

namespace Jobfinder.Application.Services;

public class RefreshService
(IRefreshTokenRepository repository,
    ITokenProvider tokenProvider)
{

    public async Task<Response<IdentityResponse>> CheckRefreshToken(string oldToken, CancellationToken cancellationToken)
    {
        var refreshToken = await repository.FindTokenAsync(oldToken, cancellationToken);
        if (refreshToken is null)
            return Response<IdentityResponse>.Failure("token is not valid");
        if (refreshToken.IsExpired())
            return Response<IdentityResponse>.Failure("token is not valid");
        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        if (!await repository.SaveChanges(cancellationToken))
            return Response<IdentityResponse>.Failure("something went wrong");
        var accessToken = tokenProvider.GenerateJwtToken(refreshToken.UserId);
        return Response<IdentityResponse>.Success
            (new IdentityResponse(accessToken, refreshToken.Token));


    }
}