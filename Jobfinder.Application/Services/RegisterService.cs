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

    public async Task<Response<IdentityResponse>> Register(RegisterDto register, CancellationToken cancellationToken)
    {
       var result =  await unitOfWork.RegisterAndCreateProfile(register.Email, register.UserType,register.Password);
       if (!result.IsSuccess)
           return Response<IdentityResponse>.Failure(result.Errors);
       var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!);
       if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
           return Response<IdentityResponse>.Failure("Something Went wrong with token service");
       
       var accessToken = tokenProvider.GenerateJwtToken(result.Data!.Id);
       return Response<IdentityResponse>.Success(new IdentityResponse(AccessToken:accessToken, RefreshToken:refreshToken.Token,null));
       // add profile in to response
       
    }
    
}