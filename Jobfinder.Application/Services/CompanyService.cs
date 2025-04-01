using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public class CompanyService(ICompanyRepository companyRepository)
{

    public async Task<Response<string>> AddCompanyForEmployer(Guid employerId, CreateCompanyDto companyDto, CancellationToken cancellationToken)
    {
        var currentCompany = await companyRepository.GetCompanyByEmployerId(employerId, cancellationToken);
        if (currentCompany is not null)
            return Response<string>.Failure("You Already have a company");

        var company = new Company(employerId, companyDto.WebsiteAddress, companyDto.Location, companyDto.SizeOfCompany,
            companyDto.Description);
        await companyRepository.CreateCompany(company);
        if (!await companyRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Failure("Something went wrong");
        return Response<string>.Success();
    }
    public async Task<Response<string>> UpdateCompanyForEmployer(Guid employerId, UpdateCompanyDto companyDto, CancellationToken cancellationToken)
    {
        var currentCompany = await companyRepository.GetCompanyByEmployerId(employerId, cancellationToken);
        if (currentCompany is  null)
            return Response<string>.Failure("Company Does not exist");

        currentCompany.UpdateCompany( companyDto.WebsiteAddress, companyDto.Location, companyDto.SizeOfCompany,
            companyDto.Description);
        await companyRepository.UpdateCompany(currentCompany);
        if (!await companyRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Failure("Something went wrong");
        return Response<string>.Success();
    }
}