using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Events;
using Jobfinder.Domain.Interfaces;

namespace Jobfinder.Application.DomainEventHandlers;

public class UserRegisteredEventHandler
    (IEmployerProfileRepository employerProfileRepository,
        IJobSeekerProfileRepository jobSeekerProfileRepository): IDomainEventHandler<UserRegisteredEvent>
{
    public async Task HandleAsync(UserRegisteredEvent domainEvent)
    {
        switch (domainEvent.User.UserRole)
        {
            case Roles.Employer:
                await employerProfileRepository.CreateProfile(new EmployerProfile(domainEvent.User));
                await employerProfileRepository.SaveChangesAsync(CancellationToken.None);
                break;
            case Roles.JobSeeker:
                await jobSeekerProfileRepository.CreateProfile(new JobSeekerProfile(domainEvent.User, null,null));
                await jobSeekerProfileRepository.SaveChangesAsync(CancellationToken.None);
                break;
            case Roles.Admin:
                //TODO: implement later
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}