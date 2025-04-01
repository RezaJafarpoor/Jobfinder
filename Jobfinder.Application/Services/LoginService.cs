using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Dtos.Profiles;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public sealed class LoginService(
    IUserProfileUnitOfWork unitOfWork,
    ITokenProvider tokenProvider,
    IRefreshTokenRepository refreshTokenRepository)
{
    public async Task<Response<IdentityResponse>> LoginWithPassword(LoginDto login, CancellationToken cancellationToken)
    {
        switch (login.UserType)
        {
            case UserType.JobSeeker when
                await unitOfWork.LoginAsJobSeeker(login.Email, login.Password, cancellationToken) is { } jobSeeker:
                if (!jobSeeker.IsSuccess)
                    return Response<IdentityResponse>.Failure("JobSeeker Does not exist");
                var jobSeekerRefreshToken =
                    new RefreshToken(tokenProvider.GenerateRefreshToken(), jobSeeker.Data!.User);
                if (!await refreshTokenRepository.AddTokenForUser(jobSeekerRefreshToken))
                    return Response<IdentityResponse>.Failure("Something wrong with token service");
                var jobSeekerAccessToken = tokenProvider.GenerateJwtToken(jobSeeker.Data.Id);
                JobSeekerDto jobSeekerDto = jobSeeker.Data;
                return Response<IdentityResponse>.Success(new IdentityResponse(jobSeekerAccessToken,
                    jobSeekerRefreshToken.Token, jobSeekerDto));
            case UserType.Employer when
                await unitOfWork.LoginAsEmployer(login.Email, login.Password, cancellationToken) is { } employer:
                if (!employer.IsSuccess)
                    return Response<IdentityResponse>.Failure("Employer Does Not Exist");
                var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), employer.Data!.User);
                if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
                    return Response<IdentityResponse>.Failure("Something wrong with Token Service");
                var accessToken = tokenProvider.GenerateJwtToken(employer.Data!.Id);
                EmployerDto employerDto = employer.Data;
                return Response<IdentityResponse>.Success(new IdentityResponse(accessToken, refreshToken.Token,
                    employerDto));

            default:
                return Response<IdentityResponse>.Failure("User type does not exist");
        }
    }
}