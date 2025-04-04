using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class JobApplicationEndpoints
{
    public static void AddJobApplicationEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapGet("jobOffer/{jobOfferId}/applications", async ([FromRoute]string jobOfferId,EmployerService service, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out Guid id);
            var result = await service.GetApplicationForJobByJobId(id, cancellationToken);
            return result.IsSuccess ? 
                Results.Ok(result.Data) 
                : Results.NotFound();
        });


        root.MapPost("jobOffer/{jobOfferId}/applications", async ([FromRoute] string jobOfferId ,
            JobSeekerService service, CancellationToken cancellationToken, HttpContext context) =>
        {
            var jobSeekerId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(jobSeekerId, out var id);
            Guid.TryParse(jobOfferId, out var jobId);
            var result = await service.ApplyToJob(new CreateJobApplicationDto(id, jobId), cancellationToken);
            return result.IsSuccess ?
                Results.NoContent() :
                Results.BadRequest();
        });

    }
}