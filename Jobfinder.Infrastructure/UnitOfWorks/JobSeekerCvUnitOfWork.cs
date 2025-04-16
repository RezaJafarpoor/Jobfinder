using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobSeekerCvUnitOfWork(
    IJobSeekerProfileRepository jobSeekerProfileRepository,
    ICvRepository cvRepository,
    ApplicationDbContext dbContext)
    : IJobSeekerCvUnitOfWork
{
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; } = jobSeekerProfileRepository;
    public ICvRepository CvRepository { get; set; } = cvRepository;


    public async Task<bool> SaveChangesAsync()
        => await dbContext.SaveChangesAsync() > 0;

   
    
}