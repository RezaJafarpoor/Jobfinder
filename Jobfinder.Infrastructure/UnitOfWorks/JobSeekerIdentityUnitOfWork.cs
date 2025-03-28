using Jobfinder.Domain.Commons;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobSeekerIdentityUnitOfWork(
    IIdentityRepository identityRepository,
    IJobSeekerProfileRepository jobSeekerProfileRepository,
    ApplicationDbContext dbContext) : IJobSeekerIdentityUnitOfWork
{
    
    public async Task<Response<User>> RegisterJobSeeker(User user, string password)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var createdUser = await identityRepository.RegisterUser(user, password);
            switch (createdUser.IsSuccess)
            {
                case false:
                    return Response<User>.Failure(createdUser.Errors);
                case true:
                    await jobSeekerProfileRepository.CreateProfile(user);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Response<User>.Success(createdUser.Data!);
            }
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Response<User>.Failure($"Transaction failed: {e.Message}");
        }
    }
}