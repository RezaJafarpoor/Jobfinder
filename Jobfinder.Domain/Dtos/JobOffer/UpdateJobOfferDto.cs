using Jobfinder.Domain.Entities;
using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Dtos.JobOffer;

public record UpdateJobOfferDto
(
    string? JobDescription,
    JobDetails? JobDetails
);