using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Dtos.Company;

public record CreateCompanyDto
    (string CompanyName,
        string WebsiteAddress,
        Location Location,
        int SizeOfCompany,
        string Description
        );