using Jobfinder.Application.Commons;
using Jobfinder.Application.Dtos.Cvs;
using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Application.Interfaces.UnitOfWorks;

public interface IJobSeekerCvUnitOfWork : IScopedService, IDisposable
{
    public IJobSeekerProfileRepository JobSeekerProfileRepository { get; set; }
    public ICvRepository CvRepository { get; set; }
    
 

    Task BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
    Task<bool> SaveChangesAsync();
}