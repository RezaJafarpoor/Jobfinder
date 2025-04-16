using Jobfinder.Application.Commons.Identity;
using Jobfinder.Application.Dtos.Cvs;
using Microsoft.AspNetCore.Antiforgery;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class CvEndpoints
{
    public static void AddCvEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

       

    root.MapPost("cvs",
            async ([FromBody] CreateCvDto createCvDto, JobSeekerService service, HttpContext context,
                    CancellationToken cancellationToken)
                =>
            {
                var subClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(subClaim, out Guid userId))
                {
                    var result = await service.CreateCvAndUpdateUsername(createCvDto, userId, cancellationToken);
                    return result.IsSuccess ? Results.Ok(result.Data) : Results.BadRequest();
                }

                return Results.Unauthorized();
            })
        .RequireAuthorization(AuthPolicies.JobSeekerOnly.ToString());

        root.MapGet("cvs", async (ICvRepository cvRepository, CancellationToken cancellationToken ) =>
        {
            var cvs = await cvRepository.GetCvs(cancellationToken);
            return cvs is null ?Results.NotFound()
                :Results.Ok(cvs.Select(cv => (CvDto)cv));
        });

        
        root.MapGet("cvs/{userId}", async ([FromRoute]string userId, ICvRepository cvRepository, CancellationToken cancellationToken) =>
        {
            if (Guid.TryParse(userId, out Guid id))
            {
                var cv = await cvRepository.GetCvByUserId(id,cancellationToken);
                return cv is null ? Results.NotFound() : Results.Ok(cv);
            }

            return Results.BadRequest();
        });
        
    }
}