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
    public  Task AddCategory(JobCategory jobCategory)
    {
        dbContext.Add(jobCategory);
        return Task.CompletedTask;
    }

    public  Task DeleteCategoryById(JobCategory category)
    {
        dbContext.JobCategories.Remove(category);
        return Task.CompletedTask;
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