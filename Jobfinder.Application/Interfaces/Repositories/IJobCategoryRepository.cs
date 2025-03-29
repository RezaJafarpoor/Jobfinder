using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobCategoryRepository
{
    Task<bool> AddCategory(JobCategory jobCategory, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryById(Guid id);
    Task UpdateCategory(JobCategory jobCategory);
    Task<JobCategory?> GetCategoryById(Guid categoryId, CancellationToken cancellationToken);
    Task<List<JobCategory>?> GetCategories(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

}