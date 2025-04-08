using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;

internal class JobCategoryRepository
    (ApplicationDbContext dbContext)
    : IJobCategoryRepository
{
    public async Task<bool> AddCategory(JobCategory jobCategory, CancellationToken cancellationToken)
    {
        var category =
            await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Category == jobCategory.Category,
                cancellationToken);
        if (category is not null)
            return false;
        dbContext.Add(jobCategory);
        return true;
    }

    public async Task<bool> DeleteCategoryById(Guid id)
    {
        var category = await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Id == id);
        if (category is null)
            return false;
        dbContext.JobCategories.Remove(category);
        return true;
    }
    
    public Task UpdateCategory(JobCategory jobCategory)
    {
        dbContext.Update(jobCategory);
        return Task.CompletedTask;
    }

    public async Task<JobCategory?> GetCategoryById(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await dbContext.JobCategories.FirstOrDefaultAsync(jc => jc.Id == categoryId, cancellationToken);
        return category;
    }

    public async Task<List<JobCategory>?> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await dbContext.JobCategories.AsNoTracking().ToListAsync(cancellationToken);
        return categories;
    }
    
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    => await dbContext.SaveChangesAsync(cancellationToken) > 0;

}