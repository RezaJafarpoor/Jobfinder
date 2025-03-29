using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface ICvRepository
{
    Task CreateCv(Cv cv);
    Task UpdateCv(Cv cv);
    Task<List<Cv>?> GetCvs(CancellationToken cancellationToken);
    Task<Cv?> GetCvById(Guid cvId, CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}