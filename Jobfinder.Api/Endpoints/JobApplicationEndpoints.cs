using Jobfinder.Application.Commons.Identity;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Interfaces.Repositories;
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

        root.MapGet("jobOffers/{jobOfferId}/applications", async ([FromRoute]string jobOfferId,EmployerService service, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out Guid id);
            var result = await service.GetApplicationForJobByJobId(id, cancellationToken);
            return result.IsSuccess ? 
                Results.Ok(result.Data) 
                : Results.NotFound();
        })
        .RequireAuthorization(AuthPolicies.EmployerOnly.ToString());


        root.MapPost("jobOffers/{jobOfferId}/applications", async ([FromRoute] string jobOfferId ,
            JobSeekerService service, CancellationToken cancellationToken, HttpContext context) =>
        {
            var jobSeekerId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (jobSeekerId is null)
                return Results.Unauthorized();
            Guid.TryParse(jobSeekerId, out var id);
            Guid.TryParse(jobOfferId, out var jobId);
            var result = await service.ApplyToJob(new CreateJobApplicationDto(id, jobId), cancellationToken);
            return result.IsSuccess ?
                Results.NoContent() :
                Results.BadRequest();
        })
        .RequireAuthorization(AuthPolicies.JobSeekerOnly.ToString());
        

        root.MapPost("jobOffers/{jobOfferId}/applications/{applicationId}", async ([FromRoute] string jobOfferId, 
            [FromRoute] string applicationId, [FromBody] UpdateJobApplicationStatus status
            , EmployerService service, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out var jobId);
            Guid.TryParse(applicationId, out var appId);
            var result = await service.ChangeApplicationStatusForJobApplication(appId, jobId, status, cancellationToken);
            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Errors);
        })
        .RequireAuthorization(AuthPolicies.EmployerOnly.ToString());

        root.MapDelete("jobOffers/{jobOfferId}/applications/{applicationId}", async ([FromRoute] string jobOfferId, 
                [FromRoute] string applicationId, HttpContext context
                , JobSeekerService service, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out var jobId);
            Guid.TryParse(applicationId, out var appId);
            var jobSeeker = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(jobSeeker, out var userId);
            var result = await service.CancelApplication(jobId, appId, userId);
            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Errors);
        })
        .RequireAuthorization(AuthPolicies.JobSeekerOnly.ToString());

    }
}