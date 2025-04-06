using Jobfinder.Domain.Enums;

namespace Jobfinder.Application.Dtos.JobApplication;

// change it
public record UpdateJobApplicationStatus
    (
        JobApplicationStatus Status
        );