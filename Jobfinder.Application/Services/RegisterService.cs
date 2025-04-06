using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Dtos.Profiles;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Commons.Identity;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class RegisterService
        (ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUserProfileUnitOfWork unitOfWork)
{

    public async Task<Response<IdentityResponse>> Register(RegisterDto register, CancellationToken cancellationToken)
    {
        switch (register.UserType)
        {
            case UserType.Employer:
            {
                var result =  await unitOfWork.RegisterAsEmployer(register.Email, register.Password);
                if (!result.IsSuccess)
                    return Response<IdentityResponse>.Failure(result.Errors);
                var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!.User);
                if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
                    return Response<IdentityResponse>.Failure("Something Went wrong with token service");
                var accessToken = tokenProvider.GenerateJwtToken(result.Data!.UserId, Roles.Employer.ToString());
               
                return Response<IdentityResponse>.Success(new IdentityResponse(AccessToken: accessToken,
                    RefreshToken: refreshToken.Token));
            }
            case UserType.JobSeeker:
            {
                var result =  await unitOfWork.RegisterAsJobSeeker(register.Email, register.Password);
                if (!result.IsSuccess)
                    return Response<IdentityResponse>.Failure(result.Errors);
                var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!.User);
                if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
                    return Response<IdentityResponse>.Failure("Something Went wrong with token service");
                var accessToken = tokenProvider.GenerateJwtToken(result.Data!.UserId, Roles.JobSeeker.ToString());
                return Response<IdentityResponse>.Success(new IdentityResponse(AccessToken: accessToken,
                    RefreshToken: refreshToken.Token));
            }
            default:
                return Response<IdentityResponse>.Failure("User type is not valid");
        }
    }
    
}