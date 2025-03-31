using  Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Services;
using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api/identity");

        root.MapPost("register",
            async ([FromBody] RegisterDto register, RegisterService registerService, CancellationToken cancellationToken) =>
            {
                var result = await registerService.Register(register, cancellationToken);
                return result.Errors.Count == 0 ? 
                    Results.Ok(result.Data) 
                    : Results.BadRequest(result.Errors);
            });
        

        root.MapPost("login", async ([FromBody]LoginDto loginDto, LoginService loginService) =>
        {
            var result = await loginService.LoginWithPassword(loginDto);
           return  result.Errors.Count == 0 ?
               Results.Ok(result.Data) :
               Results.BadRequest(result.Errors);
        });
    }
}


        
