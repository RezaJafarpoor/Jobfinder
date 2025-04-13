using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.Services;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Events;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.Services;

internal sealed class RegisterService
        (ITokenProvider tokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        UserManager<User> userManager) : IRegisterService
{

    public async Task<Response<IdentityResponse>> Register(RegisterDto register, CancellationToken cancellationToken)
    {
        Roles role = register.Roles switch
        {
            Roles.Admin => Roles.Admin,
            Roles.JobSeeker => Roles.JobSeeker,
            Roles.Employer => Roles.Employer,
            _ => throw new ArgumentOutOfRangeException()
        };
        var user = new User(register.Email, role);
        var createdUser = await userManager.CreateAsync(user, register.Password);
        if (!createdUser.Succeeded)
        {
            var error = createdUser.Errors.Select(e => e.Description).ToList();
            return Response<IdentityResponse>.Failure(error);
        }

        var refreshToken = new RefreshToken
            (tokenProvider.GenerateRefreshToken(), user);
        await refreshTokenRepository.AddToken(refreshToken);
        if (!await refreshTokenRepository.SaveChanges(cancellationToken))
            return Response<IdentityResponse>.Failure("Something went wrong!");
        user.AddDomainEvent(new UserRegisteredEvent(user));
        var accessToken = tokenProvider.GenerateJwtToken(user.Id, user.UserRole.ToString());
        var response = new IdentityResponse(AccessToken: accessToken, RefreshToken: refreshToken.Token);
         return Response<IdentityResponse>.Success(response); 
    }
    
}