using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Identity;
using Jobfinder.Application.Dtos.Profiles;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System.Net.NetworkInformation;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class UserProfileUnitOfWork
   (IEmployerProfileRepository employerProfileRepository,
      IJobSeekerProfileRepository jobSeekerProfileRepository,
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      ApplicationDbContext dbContext) : IUserProfileUnitOfWork
{
   public async Task<Response<User>> RegisterAndCreateProfile(string userEmail, UserType userType, string password)
   {
      var user = new User(userEmail);
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

   public async Task<Response<EmployerProfile>> LoginAsEmployer(string userEmail, string password, CancellationToken cancellationToken)
   {


      var currentUser = await userManager.FindByEmailAsync(userEmail);
      if (currentUser is null)
         return Response<EmployerProfile>.Failure("User name or password is wrong");

      var signInResult = await signInManager.CheckPasswordSignInAsync(currentUser, password, false);
      if (!signInResult.Succeeded)
         return Response<EmployerProfile>.Failure("User name or password is wrong");
      var profile = await employerProfileRepository.GetProfileByUserId(currentUser.Id, cancellationToken);
      return profile is null
         ? Response<EmployerProfile>.Failure("Profile does not exist") :
         Response<EmployerProfile>.Success(profile);
   }

   public async Task<Response<JobSeekerProfile>> LoginAsJobSeeker(string userEmail, string password, CancellationToken cancellationToken)
   {
      var currentUser = await userManager.FindByEmailAsync(userEmail);
      if (currentUser is null)
         return Response<JobSeekerProfile>.Failure("User name or password is wrong");

      var signInResult = await signInManager.CheckPasswordSignInAsync(currentUser, password, false);
      if (!signInResult.Succeeded)
         return Response<JobSeekerProfile>.Failure("User name or password is wrong");
      var profile = await jobSeekerProfileRepository.GetProfileByUserId(currentUser.Id, cancellationToken);
      return profile is null
         ? Response<JobSeekerProfile>.Failure("Profile does not exist") :
         Response<JobSeekerProfile>.Success(profile);
   }
}