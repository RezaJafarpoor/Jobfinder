using Jobfinder.Domain.Dtos.Cv;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface ICvRepository
{
    Task<bool> CreateCv(Guid userId, CreateCvDto createCvDto, CancellationToken cancellationToken);
    Task<bool> UpdateCv(Guid userId,CreateCvDto createCvDto, CancellationToken cancellationToken);
    Task<List<Cv>?> GetCvs(CancellationToken cancellationToken);
    Task<Cv?> GetCvByUserId(Guid userId, CancellationToken cancellationToken);
}