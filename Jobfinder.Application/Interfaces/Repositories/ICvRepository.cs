using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface ICvRepository : IScopedService
{
    Task CreateCv(Cv cv);
    Task UpdateCv(Cv cv);
    Task<List<Cv>?> GetCvs(CancellationToken cancellationToken);
    Task<Cv?> GetCvById(Guid cvId, CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    Task<Cv?> GetCvByUserId(Guid userId, CancellationToken cancellationToken);
}