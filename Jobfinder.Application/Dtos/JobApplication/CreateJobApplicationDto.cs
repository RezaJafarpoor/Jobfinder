using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.JobApplication;

public record CreateJobApplicationDto
    (
        [property:JsonPropertyName("jobSeekerProfileId")]Guid JobSeekerProfileId,
        [property:JsonPropertyName("jobOfferId")]Guid JobOfferId
        );