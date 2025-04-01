using Jobfinder.Domain.Entities;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.JobOffer;

public record JobOfferDto
 (
     
 [property:JsonPropertyName("identifier")] string Id,    
 [property:JsonPropertyName("jobName")] string JobName, 
 [property:JsonPropertyName("jobDescription")] string JobDescription, 
 [property:JsonPropertyName("jobDetails")] JobDetails JobDetails, 
 [property:JsonPropertyName("salary")] Salary Salary, 
 [property:JsonPropertyName("companyName")]string CompanyName,
 [property:JsonPropertyName("category")]JobCategory Category)

{
    public static implicit operator JobOfferDto(Domain.Entities.JobOffer jobOffer)
        => new JobOfferDto(jobOffer.Id.ToString(),jobOffer.JobName, jobOffer.JobDescription, jobOffer.JobDetails, jobOffer.Salary,
            jobOffer.CompanyName,jobOffer.Category);
}