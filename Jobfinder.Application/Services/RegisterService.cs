using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Dtos.Identity;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Application.Services;

public class RegisterService
    (IJobSeekerIdentityUnitOfWork jobSeekerUnitOfWork,
        ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IEmployerIdentityUnitOfWork employerUnitOfWork) : IRegisterService
{

    public async Task<Response<TokenResponse>> RegisterJobSeekerProfile(RegisterDto register, CancellationToken cancellationToken)
    {
        var user = await jobSeekerUnitOfWork.RegisterJobSeeker(new User(register.Email), register.Password);
        if (!user.IsSuccess)
            return Response<TokenResponse>.Failure(user.Errors);
        
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), user.Data!);
         if (! await refreshTokenRepository.AddTokenForUser(refreshToken))
             return Response<TokenResponse>.Failure("Something went wrong!");
         
         var accessToken = tokenProvider.GenerateJwtToken(user.Data!);
         var tokenResponse = new TokenResponse(AccessToken:accessToken, RefreshToken: refreshToken.Token );
         return Response<TokenResponse>.Success(tokenResponse);

    }


    public async Task<Response<TokenResponse>> RegisterEmployerProfile(RegisterDto register, CancellationToken cancellationToken)
    {

        var newUser = new User(register.Email);
        var createdUser = await employerUnitOfWork.RegisterEmployer(newUser, register.Password);
        if (!createdUser.IsSuccess)
        {
            return Response<TokenResponse>.Failure(createdUser.Errors);
        }
        
        var refreshToken = new RefreshToken(tokenProvider.GenerateRefreshToken(), createdUser.Data!);

        
        if (!await refreshTokenRepository.AddTokenForUser(refreshToken))
            return Response<TokenResponse>.Failure("Something Went wrong during user registration.");            
        
        var accessToken = tokenProvider.GenerateJwtToken(newUser);
        return Response<TokenResponse>.Success(new TokenResponse(AccessToken: accessToken, RefreshToken: refreshToken.Token));
    }
}