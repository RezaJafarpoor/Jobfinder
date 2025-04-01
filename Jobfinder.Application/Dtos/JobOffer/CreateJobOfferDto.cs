using Jobfinder.Domain.Entities;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.JobOffer;

public record CreateJobOfferDto
    (
        [property:JsonPropertyName("jobName")] string JobName,
        [property:JsonPropertyName("jobDescription")] string JobDescription,
        [property:JsonPropertyName("jobDetails")] JobDetails JobDetails,
        [property:JsonPropertyName("salary")] Salary Salary,
        [property:JsonPropertyName("category")] string JobCategory
        );
        