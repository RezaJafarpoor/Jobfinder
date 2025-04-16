using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.JobApplication;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobOfferApplicationsUnitOfWork(
    IJobApplicationRepository jobApplicationRepository,
    IJobSeekerProfileRepository jobSeekerProfileRepository,
    ApplicationDbContext dbContext,
    IJobOfferRepository jobOfferRepository)
    : IJobOfferApplicationsUnitOfWork
{


    public IJobApplicationRepository JobApplicationRepository { get; set; } = jobApplicationRepository;
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; } = jobSeekerProfileRepository;
    public IJobOfferRepository JobOfferRepository { get; set; } = jobOfferRepository;



    public async Task<bool> SaveChangesAsync()
        => await dbContext.SaveChangesAsync() > 0;
}
 