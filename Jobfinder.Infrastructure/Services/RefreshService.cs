﻿using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.Services;

namespace Jobfinder.Infrastructure.Services;

internal class RefreshService
(IRefreshTokenRepository repository,
    ITokenProvider tokenProvider) : IRefreshService
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