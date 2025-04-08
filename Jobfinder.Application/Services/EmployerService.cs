using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Company;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Dtos.JobOffer;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using System.Text;
using System.Threading.Channels;

namespace Jobfinder.Application.Services;

public class EmployerService

    (IEmployerProfileRepository profileRepository,
        ICompanyRepository companyRepository,
        IJobApplicationRepository applicationRepository,
        Channel<EmailContent> channel)
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

    public async Task<Response<List<CvDto>?>> GetApplicationForJobByJobId(Guid jobId, CancellationToken cancellationToken)
    {
        var job = await applicationRepository.GetCvsForJobOffer(jobId, cancellationToken);
        if (job is null)
            return Response<List<CvDto>?>.Failure("No application for job ");
        var dtos = job.Select(j => (CvDto)j).ToList();
        return Response<List<CvDto>?>.Success(dtos);

    }


    //TODO: Add Email support for notifying job Seeker about application
    public async Task<Response<string>> ChangeApplicationStatusForJobApplication(Guid applicationId,
        Guid jobId, UpdateJobApplicationStatus status,
        CancellationToken cancellationToken)
    {
        var job = await applicationRepository.GetJobApplication(jobId, applicationId, cancellationToken);
        if (job is null)
            return Response<string>.Failure("job or Application does not exist");
        job.Status = status.Status;
        if (!await applicationRepository.SaveChangesAsync(cancellationToken))
            return Response<string>.Failure("Something went wrong");

        if (status.Status is JobApplicationStatus.Accepted)
        {
            var emailContent = new EmailContent("Your Request Accepted",job.JobSeekerProfile.User.Email,$"Dear {job.JobSeekerProfile.Firstname}");
            await channel.Writer.WriteAsync(emailContent, cancellationToken);
        }

        return Response<string>.Success("changed");

    }
}