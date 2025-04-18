﻿
using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Company;

public record CreateCompanyDto
    (
        [property:JsonPropertyName("companyName")] string CompanyName,
        [property:JsonPropertyName("websiteAddress")]string WebsiteAddress,
        [property:JsonPropertyName("location")]Location Location,
        [property:JsonPropertyName("sizeOfCompany")]int SizeOfCompany,
        [property:JsonPropertyName("description")]string Description
        );