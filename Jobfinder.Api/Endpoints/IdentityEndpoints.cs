using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Dtos;
using Jobfinder.Domain.Dtos.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api/identity");
        var registration = root.MapGroup("register");

        registration.MapPost("jobSeeker",
            async ([FromBody] RegisterDto register, IRegisterService registerService, CancellationToken cancellationToken) =>
            {
                var result = await registerService.RegisterJobSeekerProfile(register, cancellationToken);
                return result.Errors.Count == 0 ? 
                    Results.Ok(result.Data) 
                    : Results.BadRequest(result.Errors);
            });

        registration.MapPost("employer", async ([FromBody] RegisterDto register,IRegisterService registerService, CancellationToken cancellationToken ) =>
        {
            var result =  await registerService.RegisterEmployerProfile(register, cancellationToken);
            return result.Errors.Count == 0 ?
                Results.Ok(result.Data) :
                Results.BadRequest(result.Errors);
        });
        
        root.MapPost("login", async ([FromBody]LoginDto login, ILoginService loginService, CancellationToken cancellationToken) =>
        {
            var result = await loginService.Login(login, cancellationToken);
            return result.Errors.Count == 0 ?
                Results.Ok(result.Data) :
                Results.BadRequest(result.Errors);
        });

    }
}


        
