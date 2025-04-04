using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Services;

public class EmployerService

    (IEmployerProfileRepository profileRepository,
        ICompanyRepository companyRepository,
        IJobOfferRepository jobOfferRepository,
        IJobApplicationRepository applicationRepository)
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


    public async Task<Response<string>> CreateJobOffer(Guid employerId, CreateJobOfferDto dto,
        CancellationToken cancellationToken)
    {
        var employer = await profileRepository.GetProfileById(employerId, cancellationToken);
        if (employer is null)
            return Response<string>.Failure("employer does not exist");
        var company = await companyRepository.GetCompanyByEmployerId(employerId, cancellationToken);
        if (company is null)
            return Response<string>.Failure("Register company first");
        var category = new JobCategory(dto.JobCategory);
        var jobOffer = new JobOffer(dto.JobName, dto.JobDescription, dto.JobDetails, dto.Salary, company.CompanyName, category, employer);
        employer.AddJobOffer(jobOffer);
        if (!await profileRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Failure("something went wrong");
        return Response<string>.Success();
    }

    public async Task<Response<List<Cv?>?>> GetApplicationForJobByJobId(Guid jobId, CancellationToken cancellationToken)
    {
        var job = await applicationRepository.GetCvsForJobOffer(jobId, cancellationToken);
        return job is null ?
            Response<List<Cv?>?>.Failure("No application for job ") :
            Response<List<Cv?>?>.Success(job);
    }
};