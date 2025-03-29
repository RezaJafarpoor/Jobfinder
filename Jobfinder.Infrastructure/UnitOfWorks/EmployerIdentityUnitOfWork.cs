using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class EmployerIdentityUnitOfWork
    (IEmployerProfileRepository employerProfileRepository,
        IIdentityRepository identityRepository,
        ApplicationDbContext dbContext) : IEmployerIdentityUnitOfWork
{
    public async Task<Response<User>> RegisterEmployer(User user, string password)
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
                    await employerProfileRepository.CreateProfile(user);
                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Response<User>.Success(createdUser.Data!);
            }
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Response<User>.Failure($"Transaction failed{e.Message}");
        }
    }
}