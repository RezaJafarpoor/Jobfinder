using  Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;



internal class CvRepository
    (ApplicationDbContext dbContext) : ICvRepository
{
    public Task CreateCv(Cv cv)
    {
        dbContext.Add(cv);
        return Task.CompletedTask;
    }

    public  Task UpdateCv( Cv cv)
    {
        dbContext.Cvs.Update(cv);
        return Task.CompletedTask;
    }

    public async Task<List<Cv>?> GetCvs(CancellationToken cancellationToken)
    {
        var cvs = await dbContext.Cvs
            .Include(c => c.JobSeeker)
            .AsNoTracking().ToListAsync(cancellationToken);
        return cvs;
    }

    

    public async Task<Cv?> GetCvById(Guid cvId, CancellationToken cancellationToken)
    {
        var cv = await dbContext.Cvs
            .FirstOrDefaultAsync(c=> c.Id == cvId, cancellationToken);
        return cv;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken) > 0;

    public async Task<Cv?> GetCvByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var cv = await dbContext.Cvs.Include(c => c.JobSeeker)
            .Where(u => u.JobSeeker.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken);
        return cv;
    }
}