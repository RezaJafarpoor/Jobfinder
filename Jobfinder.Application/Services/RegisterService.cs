using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Application.Services;

public sealed class RegisterService
        (ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUserProfileUnitOfWork unitOfWork)
{

    public async Task<Response<TokenResponse>> Register(RegisterDto register, CancellationToken cancellationToken)
    {
        var user = new User(register.Email);
       var result =  await unitOfWork.RegisterAndCreateProfile(user, register.UserType,register.Password);
       if (!result.IsSuccess)
           return Response<TokenResponse>.Failure(result.Errors);
       var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!);
       if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
           return Response<TokenResponse>.Failure("Something Went wrong with token service");
       
       var accessToken = tokenProvider.GenerateJwtToken(result.Data!);
       return Response<TokenResponse>.Success(new TokenResponse(AccessToken:accessToken, RefreshToken:refreshToken.Token));
    }
    
}