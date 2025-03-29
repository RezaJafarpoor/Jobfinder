using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cv;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobSeekerCvUnitOfWork(
    IJobSeekerProfileRepository jobSeekerProfileRepository,
    ICvRepository cvRepository,
    ApplicationDbContext dbContext) : IJobSeekerCvUnitOfWork
{
    public async Task<Response<string>> CreateCvAndUpdateUsername(CreateCvDto cvDto, Guid jobSeekerId,CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var profile = await jobSeekerProfileRepository.GetUserById(jobSeekerId, cancellationToken);
            if (profile is null)
                return Response<string>.Failure("User does not exist");
            
            var cv = new Cv(cvDto.Location, cvDto.BirthDay, cvDto.MaximumSalary, cvDto.MaximumSalary, cvDto.Status,
                jobSeekerId);
            await cvRepository.CreateCv(cv);
            profile.Firstname = cvDto.Firstname;
            profile.Lastname = cvDto.Lastname;
            await jobSeekerProfileRepository.Update(profile);
            await transaction.CommitAsync(cancellationToken);
            return Response<string>.Success("Cv added");        //WARNING: remove the message in the Success()
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Response<string>.Failure(e.Message);
        }
    }
}