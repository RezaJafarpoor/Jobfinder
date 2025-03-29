using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Application.Interfaces.UnitOfWorks;
using Jobfinder.Infrastructure.Persistence;

namespace Jobfinder.Infrastructure.UnitOfWorks;

internal class JobSeekerCvUnitOfWork 
    (IJobSeekerProfileRepository jobSeekerProfileRepository,
        ICvRepository cvRepository,
        ApplicationDbContext dbContext) : IJobSeekerCvUnitOfWork;