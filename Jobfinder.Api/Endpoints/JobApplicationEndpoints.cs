using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace Jobfinder.Api.Endpoints;

public static class JobApplicationEndpoints
{
    public static void AddJobApplicationEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        root.MapGet("jobOffer/{jobOfferId}/applications", async ([FromRoute]string jobOfferId,IJobOfferApplicationsUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out Guid id);
            var result = await unitOfWork.GetApplicationsForJob(id, cancellationToken);
            return result.IsSuccess ? 
                Results.Ok(result.Data) 
                : Results.NotFound();
        });


        root.MapPost("jobOffer/{jobOfferId}", async ([FromRoute] string jobOfferId, [FromBody]CreateJobApplicationDto dto,
            IJobOfferApplicationsUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
        {
            Guid.TryParse(jobOfferId, out Guid id);
            if (id != dto.JobOfferId)
                return Results.BadRequest();
            var result = await unitOfWork.ApplyToJob(dto, cancellationToken);
            return result.IsSuccess ? 
                Results.NoContent() : 
                Results.BadRequest(result.Errors);
        });

    }
}