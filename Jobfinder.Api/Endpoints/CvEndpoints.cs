using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
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


        root.MapGet("cvs/myCv", async (HttpContext context, ICvRepository repository, CancellationToken cancellationToken) =>
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid id))
                return Results.Unauthorized();
            var cv = await repository.GetCvByUserId(id, cancellationToken);
            if (cv is null)
                return Results.NotFound();
            CvDto cvDto = cv;
            return Results.Ok(cvDto);

        });
    }
}