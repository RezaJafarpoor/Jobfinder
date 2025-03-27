using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Domain.Dtos.Company;

public record UpdateCompanyDto
    (string? WebsiteAddress,
        Location? Location,
        int? SizeOfCompany,
        string? Description);