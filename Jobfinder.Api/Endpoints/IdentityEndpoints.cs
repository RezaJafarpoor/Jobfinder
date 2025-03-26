using Jobfinder.Application.Interfaces;
using Jobfinder.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api/identity");

        root.MapPost("register",
            async ([FromBody] RegisterDto register, IAuthService authService, CancellationToken cancellationToken) =>
            {
                var result = await authService.RegisterJobSeeker(register, cancellationToken);
                return result.Errors.Count == 0 ? Results.Ok(result.Data) : Results.BadRequest(result.Errors);
            });
    }
}


        
