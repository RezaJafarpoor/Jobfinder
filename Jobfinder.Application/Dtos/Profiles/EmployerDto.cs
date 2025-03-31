using Jobfinder.Domain.Entities;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Profiles;

public record EmployerDto(
    [property:JsonPropertyName("company")] Domain.Entities.Company? Company
) : IProfile
{
    public static implicit operator EmployerDto(EmployerProfile employerProfile)
        => new EmployerDto(Company: employerProfile.Company ?? null);
}