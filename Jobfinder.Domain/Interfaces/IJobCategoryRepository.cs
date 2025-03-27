using Jobfinder.Domain.Dtos.Category;
using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobCategoryRepository
{
    Task<bool> AddCategory(CategoryDto categoryDto, CancellationToken cancellationToken);
    Task<bool> DeleteCategoryById(Guid id);
    Task<bool> UpdateCategoryById(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken);
    Task<JobCategory?> GetCategoryById(Guid categoryId, CancellationToken cancellationToken);
    Task<List<JobCategory>?> GetCategories(CancellationToken cancellationToken);

}