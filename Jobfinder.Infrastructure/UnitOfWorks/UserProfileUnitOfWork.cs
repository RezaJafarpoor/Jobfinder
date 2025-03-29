using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class UserProfileUnitOfWork
   (IEmployerProfileRepository employerProfileRepository,
      IJobSeekerProfileRepository jobSeekerProfileRepository,
      UserManager<User> userManager, ApplicationDbContext dbContext) : IUserProfileUnitOfWork
{
   public async Task<Response<User>> RegisterAndCreateProfile(User user, UserType userType, string password)
   {
      await using var transaction = await dbContext.Database.BeginTransactionAsync(); 
      try
      {
         var createdUser = await userManager.CreateAsync(user, password);
         if (!createdUser.Succeeded)
         {
            var errors = createdUser.Errors.Select(err => err.Description).ToList();
            return Response<User>.Failure(errors);
         }

         switch (userType)
         {
            case UserType.Employer:
               var employerProfile = new EmployerProfile(user);
               await employerProfileRepository.CreateProfile(employerProfile);
               break;
            case UserType.JobSeeker:
               var jobSeekerProfile = new JobSeekerProfile(user, null,null);
               await jobSeekerProfileRepository.CreateProfile(jobSeekerProfile);
               break;
            default:
               return Response<User>.Failure("User Type is invalid");
         }

         await dbContext.SaveChangesAsync();
         await transaction.CommitAsync();
         return Response<User>.Success(user);
      }
      catch (Exception e)
      {
         await transaction.RollbackAsync();
         return Response<User>.Failure("Something went wrong");
      }
      
   }
}