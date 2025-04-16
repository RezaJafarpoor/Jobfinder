using Jobfinder.Application.Interfaces.Common;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Application.Interfaces.Repositories;

public interface IJobCategoryRepository : IScopedService
{
    Task AddCategory(JobCategory jobCategory);
    Task DeleteCategoryById(JobCategory category);
    Task UpdateCategory(JobCategory jobCategory);
    Task<JobCategory?> GetCategoryById(Guid categoryId, CancellationToken cancellationToken);
    Task<List<JobCategory>?> GetCategories(CancellationToken cancellationToken);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);

}