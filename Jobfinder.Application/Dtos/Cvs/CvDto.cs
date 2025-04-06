using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Cvs;

public record CvDto
    (
        [property:JsonPropertyName("location")] string Id,
        [property:JsonPropertyName("location")] Location Location,
        [property:JsonPropertyName("birthDay")] DateOnly? BirthDay,
        [property:JsonPropertyName("maxmimumSalary")] int? MaximumSalary,
        [property:JsonPropertyName("minimumSalary")] int? MinimumSalary,
        [property:JsonPropertyName("status")] MilitaryServiceStatus? Status)
{
    public static implicit operator CvDto(Cv cv) 
        => new CvDto(cv.Id.ToString(),cv.Location,cv.BirthDay,cv.MaximumExpectedSalary,cv.MinimumExpectedSalary,cv.ServiceStatus);
    

    
    
}
