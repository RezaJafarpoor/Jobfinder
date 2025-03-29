using Jobfinder.Domain.ValueObjects;

namespace Jobfinder.Application.Dtos.Company;

public record UpdateCompanyDto
    (string? WebsiteAddress,
        Location? Location,
        int? SizeOfCompany,
        string? Description);