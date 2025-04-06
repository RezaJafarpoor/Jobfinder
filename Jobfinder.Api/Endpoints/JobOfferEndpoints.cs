using Jobfinder.Application.Commons.Identity;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class JobOfferEndpoints
{
    public static void AddJobOfferEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapPost("jobOffers", async ([FromBody]CreateJobOfferDto job, EmployerService service, HttpContext context, CancellationToken cancellationToken) =>
        {
            var employer = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (employer is null)
                return Results.Unauthorized();
            Guid.TryParse(employer, out Guid profileId);
            var result = await service.CreateJobOffer(profileId, job, cancellationToken);
            return result.IsSuccess ? 
                Results.NoContent() :
                Results.BadRequest(result.Errors);
        })
        .RequireAuthorization(AuthPolicies.EmployerOnly.ToString());
        
        root.MapGet("jobOffers", async (IJobOfferRepository repository, CancellationToken cancellationToken) =>
        {
            var jobOffers = await repository.GetJobOffers(cancellationToken);
            if (jobOffers is null)
                return Results.NotFound();

            var dtos = jobOffers.Select(jo => (JobOfferDto) jo);
           return Results.Ok(dtos);
        });

        root.MapGet("jobOffers/{id}",
            async (string id, IJobOfferRepository jobOfferRepository, CancellationToken cancellationToken) =>
            {
                Guid.TryParse(id, out Guid jobOfferId);
                var jobOffer = await jobOfferRepository.GetJobOfferById(jobOfferId, cancellationToken);
                if (jobOffer is null)
                    return Results.NotFound();
                JobOfferDto dto = jobOffer;
                return Results.Ok(dto);
            });
        
        
        
    }
}