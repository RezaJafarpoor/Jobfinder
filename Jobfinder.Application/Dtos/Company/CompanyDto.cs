using Jobfinder.Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Jobfinder.Application.Dtos.Company;

public record CompanyDto(string CompanyName,
    [property:JsonPropertyName("websiteAddress")]string WebsiteAddress,
    [property:JsonPropertyName("location")]Location Location,
    [property:JsonPropertyName("sizeOfCompany")]int SizeOfCompany,
    [property:JsonPropertyName("Description")]string Description
    )

{

    public static implicit operator CompanyDto(Domain.Entities.Company company)
        => new(company.CompanyName, company.WebsiteAddress, company.Location, company.SizeOfCompany,
            company.Description);
}