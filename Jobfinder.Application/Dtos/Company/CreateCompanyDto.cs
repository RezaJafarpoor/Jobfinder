using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Application.Dtos.Company;

public record CreateCompanyDto
    (string CompanyName,
        string WebsiteAddress,
        Location Location,
        int SizeOfCompany,
        string Description
        );