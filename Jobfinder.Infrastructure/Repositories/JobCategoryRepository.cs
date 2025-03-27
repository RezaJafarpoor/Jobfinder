using Jobfinder.Domain.Dtos.Category;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

//TODO: Add Result pattern to this class

internal class JobCategoryRepository
    (ApplicationDbContext dbContext)
    : IJobCategoryRepository
{
    public async Task<bool> AddCategory(CategoryDto categoryDto, CancellationToken cancellationToken)
    {
        var category =
            await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Category == categoryDto.CategoryName,
                cancellationToken);
        if (category is null)
            return false;
        var newCategory = new JobCategory
        {
            Category = categoryDto.CategoryName
        };
        dbContext.Add(newCategory);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteCategoryById(Guid id)
    {
        var category = await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Id == id);
        if (category is null)
            return false;
        dbContext.JobCategories.Remove(category);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCategoryById(CategoryDto categoryDto, Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Id == categoryId, cancellationToken);
        if (category is null)
            return false;
        category.Category = categoryDto.CategoryName;

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<JobCategory?> GetCategoryById(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Id == categoryId, cancellationToken);
        return category;
    }

    public async Task<List<JobCategory>?> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await dbContext.JobCategories.ToListAsync(cancellationToken);
        return categories;
    }
}