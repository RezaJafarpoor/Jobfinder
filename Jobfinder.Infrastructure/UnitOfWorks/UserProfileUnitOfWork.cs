using Jobfinder.Application.Commons;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class UserProfileUnitOfWork
   (IEmployerProfileRepository employerProfileRepository,
      IJobSeekerProfileRepository jobSeekerProfileRepository,
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      ApplicationDbContext dbContext) : IUserProfileUnitOfWork
{
   

   public async Task<Response<JobSeekerProfile>> RegisterAsJobSeeker(string userEmail, string password)
   {
      var user = new User(userEmail);
      await using var transaction = await dbContext.Database.BeginTransactionAsync(); 
      try
      {
         var createdUser = await userManager.CreateAsync(user, password);
         if (!createdUser.Succeeded)
         {
            var errors = createdUser.Errors.Select(err => err.Description).ToList();
            return Response<JobSeekerProfile>.Failure(errors);
         }
         var jobSeekerProfile = new JobSeekerProfile(user, null,null);
         await jobSeekerProfileRepository.CreateProfile(jobSeekerProfile);

         

         await dbContext.SaveChangesAsync();
         await transaction.CommitAsync();
         return Response<JobSeekerProfile>.Success(jobSeekerProfile);
      }
      catch (Exception e)
      {
         await transaction.RollbackAsync();
         return Response<JobSeekerProfile>.Failure(e.Message);
      }
   }

   public async Task<Response<EmployerProfile>> RegisterAsEmployer(string userEmail, string password)
   {
      var user = new User(userEmail);
      await using var transaction = await dbContext.Database.BeginTransactionAsync(); 
      try
      {
         var createdUser = await userManager.CreateAsync(user, password);
         if (!createdUser.Succeeded)
         {
            var errors = createdUser.Errors.Select(err => err.Description).ToList();
            return Response<EmployerProfile>.Failure(errors);
         }
         var employerProfile = new EmployerProfile(user);
         await employerProfileRepository.CreateProfile(employerProfile);
         await dbContext.SaveChangesAsync();
         await transaction.CommitAsync();
         return Response<EmployerProfile>.Success(employerProfile);
      }
      catch (Exception e)
      {
         await transaction.RollbackAsync();
         return Response<EmployerProfile>.Failure(e.Message);
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
      return profile is null ?
         Response<EmployerProfile>.Failure("Profile does not exist") :
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
      var profile = await jobSeekerProfileRepository.GetProfileById(currentUser.Id, cancellationToken);
      return profile is null
         ? Response<JobSeekerProfile>.Failure("Profile does not exist") :
         Response<JobSeekerProfile>.Success(profile);
   }
}