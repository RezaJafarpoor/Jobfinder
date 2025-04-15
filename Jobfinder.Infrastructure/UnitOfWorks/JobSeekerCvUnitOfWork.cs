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

internal class JobSeekerCvUnitOfWork : IJobSeekerCvUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private  IDbContextTransaction _transaction;

    public JobSeekerCvUnitOfWork(IJobSeekerProfileRepository jobSeekerProfileRepository, ICvRepository cvRepository,  ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        JobSeekerProfileRepository = jobSeekerProfileRepository;
        CvRepository = cvRepository;
    }

    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; }
    public ICvRepository CvRepository { get; set; }
    
       

    public async Task BeginTransaction()
    {
         _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync() 
        => await _transaction.CommitAsync();

    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public async Task<bool> SaveChangesAsync()
        => await _dbContext.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _transaction.Dispose();
    }
    
}