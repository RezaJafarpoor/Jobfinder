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
    IRefreshTokenRepository refreshTokenRepository,
    CancellationToken cancellationToken)
{
    public async Task<Response<IdentityResponse>> LoginWithPassword(LoginDto login)
    {
        string accessToken, refreshToken;
        switch (login.UserType)
        {
            case UserType.JobSeeker when 
                await unitOfWork.LoginAsJobSeeker(login.Email, login.Password, cancellationToken) is {} jobSeeker :
                if (!jobSeeker.IsSuccess)
                    return Response<IdentityResponse>.Failure("JobSeeker Does not exist");
                GetTokens(jobSeeker.Data!.User, out accessToken, out refreshToken);
                JobSeekerDto jobSeekerDto= jobSeeker.Data;
                return Response<IdentityResponse>.Success(new IdentityResponse(accessToken, refreshToken, jobSeekerDto));
            case UserType.Employer when
                await unitOfWork.LoginAsEmployer(login.Email, login.Password, cancellationToken) is {} employer:
                if (!employer.IsSuccess)
                    return Response<IdentityResponse>.Failure("Employer Does Not Exist");
                GetTokens(employer.Data!.User, out accessToken, out refreshToken);
                EmployerDto employerDto= employer.Data;
                return Response<IdentityResponse>.Success(new IdentityResponse(accessToken, refreshToken, employerDto));

            default:
                return Response<IdentityResponse>.Failure("User Type does not exist");
        }
        
    }



    private void GetTokens(User user, out string accessToken, out string refreshToken)
    {
        var refToken = new RefreshToken
            (tokenProvider.GenerateRefreshToken(), user);
        refreshTokenRepository.AddTokenForUser(refToken);
        refreshToken = refToken.Token;
        accessToken = tokenProvider.GenerateJwtToken(user.Id);
    }
}