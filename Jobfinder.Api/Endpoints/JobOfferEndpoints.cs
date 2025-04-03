using Jobfinder.Application.Dtos.Category;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Jobfinder.Api.Endpoints;

public static class JobOfferEndpoints
{
    public static void AddJobOfferEndpoints(this IEndpointRouteBuilder builder)
    {
        var root = builder.MapGroup("api");

        // root.MapPost("jobOffer", async ([FromBody]CreateJobOfferDto job, IJobOfferRepository jobOfferRepository, HttpContext context, ICompanyRepository companyRepository, CancellationToken cancellationToken) =>
        // {
        //     var employer = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //     if (employer is null)
        //         return Results.Unauthorized();
        //     Guid.TryParse(employer, out Guid profileId);
        //     var category = new JobCategory(job.JobCategory);
        //     var company = await companyRepository.GetCompanyByEmployerId(profileId, cancellationToken);
        //     if (company is null)
        //         return Results.BadRequest("Register your company first");
        //     var jobOffer = new JobOffer(job.JobName, job.JobDescription, job.JobDetails, job.Salary,company.CompanyName,
        //         category, profileId);
        //     await jobOfferRepository.CreateJobOffer(jobOffer);
        //     return await jobOfferRepository.SaveChangesAsync(cancellationToken)
        //         ? Results.BadRequest()
        //         : Results.NoContent();
        // });
        
        root.MapGet("jobOffer", async (IJobOfferRepository repository, CancellationToken cancellationToken) =>
        {
            var jobOffers = await repository.GetJobOffers(cancellationToken);
            if (jobOffers is null)
                return Results.NotFound();

            var dtos = jobOffers.Select(jo => (JobOfferDto) jo);
           return Results.Ok(dtos);
        });

        root.MapGet("jobOffer/{id}",
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