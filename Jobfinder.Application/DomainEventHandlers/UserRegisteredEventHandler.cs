using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Notification;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Events;

namespace Jobfinder.Application.DomainEventHandlers;

public class UserRegisteredEventHandler
(IJobSeekerProfileRepository jobSeekerProfileRepository,
     IEmployerProfileRepository employerProfileRepository) : INotificationHandler<UserRegisteredEvent>
{
     public async Task Handle(UserRegisteredEvent domainEvent)
     {
          
          switch (domainEvent.User.UserRole)
          {
               case Roles.Admin:
                    break;
               case Roles.JobSeeker:
                    await jobSeekerProfileRepository.CreateProfile(new JobSeekerProfile(domainEvent.User,null,null));
                    await jobSeekerProfileRepository.SaveChangesAsync(CancellationToken.None);
                    break;
               case Roles.Employer:
                    await employerProfileRepository.CreateProfile(new EmployerProfile(domainEvent.User));
                    await employerProfileRepository.SaveChangesAsync(CancellationToken.None);
                    break;
               default:
                    throw new ArgumentOutOfRangeException();
          }
     }

    
}