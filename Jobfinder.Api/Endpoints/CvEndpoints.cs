using Jobfinder.Application.Dtos.Cv;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class CvEndpoints
{
    public static void AddCvEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapPost("cvs",
            async ([FromBody] CreateCvDto createCvDto, IJobSeekerCvUnitOfWork unitOfWork, HttpContext context,
                    CancellationToken cancellationToken)
                =>
            {
                var subClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(subClaim, out Guid userId))
                {
                    var result = await unitOfWork.CreateCvAndUpdateUsername(createCvDto, userId, cancellationToken);
                    return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest();
                }

                return Results.Unauthorized();
            });
        
    }
}