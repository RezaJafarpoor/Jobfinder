using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.JobApplication;

public record CancelJobApplicationDto
    (
        [property:JsonPropertyName("jobSeekerProfileId")]Guid JobSeekerProfileId,
        [property:JsonPropertyName("jobOfferId")]Guid JobOfferId
        );