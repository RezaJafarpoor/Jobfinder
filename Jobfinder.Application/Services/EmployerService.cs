using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public class EmployerService

    (IEmployerProfileRepository profileRepository)
{
    public async Task<Response<string>> CreateCompany(Guid employerId, CreateCompanyDto dto, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetProfileById(employerId, cancellationToken);
        if (profile is null)
            return Response<string>.Failure("Employer does not exist");
        var company = new Company(profile, dto.CompanyName, dto.WebsiteAddress, dto.Location, dto.SizeOfCompany,
            dto.Description);
        if (profile.Company is not null)
            return Response<string>.Failure("User has a company ");
        
        profile.Company = company;
        if (!await profileRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Failure("Something went wrong");
        return Response<string>.Success();
    }
};