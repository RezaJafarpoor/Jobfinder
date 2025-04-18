﻿using Jobfinder.Domain.Enums;
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Cvs;

public record CreateCvDto
    (
        [property:JsonPropertyName("location")] Location Location,
        [property:JsonPropertyName("birthDay")] DateOnly BirthDay,
        [property:JsonPropertyName("maxmimumSalary")] int? MaximumSalary,
        [property:JsonPropertyName("minimumSalary")] int? MinimumSalary,
        [property:JsonPropertyName("status")] MilitaryServiceStatus? Status,
        [property:JsonPropertyName("firstName")] string Firstname,
        [property:JsonPropertyName("lastName")] string Lastname
        );