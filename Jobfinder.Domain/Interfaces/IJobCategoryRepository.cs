using Jobfinder.Domain.Entities;

namespace Jobfinder.Domain.Interfaces;

public interface IJobCategoryRepository
{
    Task<bool> AddCategory();
    Task<bool> DeleteCategoryById(Guid id);
    Task<bool> UpdateCategoryById(Guid id);
    Task<JobCategory> GetCategoryById(Guid id);
    Task<List<JobCategory>> GetCategories();

}