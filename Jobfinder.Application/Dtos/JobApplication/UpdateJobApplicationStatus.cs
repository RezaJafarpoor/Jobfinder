using Jobfinder.Domain.Enums;

namespace Jobfinder.Application.Dtos.JobApplication;

public record UpdateJobApplicationStatus
    (
        JobApplicationStatus Status
        );