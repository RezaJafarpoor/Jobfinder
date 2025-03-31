using Jobfinder.Domain.Entities;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Profiles;

public record JobSeekerDto
    (
        [property:JsonPropertyName("firstName")] string Firstname,
        [property:JsonPropertyName("lastName")] string LastName) : IProfile
{
    public static implicit operator JobSeekerDto(JobSeekerProfile jobSeeker) 
        => new JobSeekerDto(jobSeeker.Firstname, jobSeeker.Lastname);
}