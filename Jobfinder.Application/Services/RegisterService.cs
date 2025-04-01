using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Dtos.Profiles;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class RegisterService
        (ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUserProfileUnitOfWork unitOfWork)
{

    public async Task<Response<IdentityResponse>> Register(RegisterDto register, CancellationToken cancellationToken)
    {
        if (register.UserType == UserType.Employer)
        {
            var result =  await unitOfWork.RegisterAsEmployer(register.Email, register.Password);
            if (!result.IsSuccess)
                return Response<IdentityResponse>.Failure(result.Errors);
            var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!.User);
            if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
                return Response<IdentityResponse>.Failure("Something Went wrong with token service");
            var accessToken = tokenProvider.GenerateJwtToken(result.Data!.Id);
            EmployerDto dto = result.Data;
            return Response<IdentityResponse>.Success(new IdentityResponse(AccessToken: accessToken,
                RefreshToken: refreshToken.Token, dto));
        }
        if (register.UserType == UserType.JobSeeker)
        {
            var result =  await unitOfWork.RegisterAsJobSeeker(register.Email, register.Password);
            if (!result.IsSuccess)
                return Response<IdentityResponse>.Failure(result.Errors);
            var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), result.Data!.User);
            if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
                return Response<IdentityResponse>.Failure("Something Went wrong with token service");
            var accessToken = tokenProvider.GenerateJwtToken(result.Data!.Id);
            JobSeekerDto dto = result.Data;
            return Response<IdentityResponse>.Success(new IdentityResponse(AccessToken: accessToken,
                RefreshToken: refreshToken.Token, dto));
        }
        return Response<IdentityResponse>.Failure("User type is not valid");
    }
    
}