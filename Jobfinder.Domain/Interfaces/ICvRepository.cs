namespace Jobfinder.Domain.Interfaces;

public interface ICvRepository
{
    Task<bool> CreateCv();
    Task<bool> UpdateCv(Guid id);
}