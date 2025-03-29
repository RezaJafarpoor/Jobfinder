﻿using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Cvs;

public record CvDto
    (
        [property:JsonPropertyName("location")] Location Location,
        [property:JsonPropertyName("birthDay")] DateOnly? BirthDay,
        [property:JsonPropertyName("maxmimumSalary")] int? MaximumSalary,
        [property:JsonPropertyName("minimumSalary")] int? MinimumSalary,
        [property:JsonPropertyName("status")] MilitaryServiceStatus? Status,
        [property:JsonPropertyName("fullName")] string FullName)
{
    public static implicit operator CvDto(Cv cv) 
        => new CvDto(cv.Location,cv.BirthDay,cv.MaximumExpectedSalary,cv.MinimumExpectedSalary,cv.ServiceStatus,cv.FullName);
    

    
    
}
