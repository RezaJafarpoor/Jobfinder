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

internal class JobOfferApplicationsUnitOfWork : IJobOfferApplicationsUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction _transaction;
    public JobOfferApplicationsUnitOfWork(IJobApplicationRepository jobApplicationRepository, IJobSeekerProfileRepository jobSeekerProfileRepository,
        ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        JobApplicationRepository = jobApplicationRepository;
        JobSeekerProfileRepository = jobSeekerProfileRepository;
    }

    public IJobApplicationRepository JobApplicationRepository { get; set; }
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; }


    public async Task BeginTransactionAsync()
        => _transaction = await _dbContext.Database.BeginTransactionAsync();


    public async Task CommitAsync()
        => await _transaction.CommitAsync();


    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
    public void Dispose()
    {
        _transaction.Dispose();
    }
}