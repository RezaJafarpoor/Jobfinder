using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Application.Dtos.JobOffer;

public record UpdateJobOfferDto
(
    string? JobDescription,
    JobDetails? JobDetails
);