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
    ApplicationDbContext dbContext) : IJobSeekerCvUnitOfWork
{
    private  IDbContextTransaction _transaction;
    public async Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid userId,CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var profile = await jobSeekerProfileRepository.GetProfileByUserId(userId, cancellationToken);
            if (profile is null)
                return Response<string>.Failure("User does not exist");

            var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MaximumSalary, cvDto.MaximumSalary, cvDto.Status,
                profile);
            profile.Firstname = cvDto.Firstname;
            profile.Lastname = cvDto.Lastname;
            await cvRepository.CreateCv(cv);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Response<string>.Success("Cv added");        //WARNING: remove the message in the Success()
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Response<string>.Failure(e.Message);
        }
    }

    public async Task BeginTransaction()
    {
         _transaction = await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync() 
        => await _transaction.CommitAsync();

    public async Task RollbackAsync()
        => await _transaction.RollbackAsync();

    public async Task SaveChangesAsync()
        => await dbContext.SaveChangesAsync();

    public void Dispose()
    {
        _transaction.Dispose();
        dbContext.Dispose();
    }
    
}