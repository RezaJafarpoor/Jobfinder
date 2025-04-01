using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;



internal class CompanyRepository(ApplicationDbContext dbContext) : ICompanyRepository
{
    public  Task CreateCompany(Company userCompany)
    {
        dbContext.Add(userCompany);
        return Task.CompletedTask;

    }

    public Task UpdateCompany( Company company)
    {
        dbContext.Update(company);
        return Task.CompletedTask;
    }

    public async  Task<List<Company>> GetCompanies(CancellationToken cancellationToken)
    {
        var companies = await dbContext.Companies.AsNoTracking().ToListAsync(cancellationToken);
        return companies;
    }

    public async Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId, cancellationToken);
        return company ?? null;
    }

    public async Task<Company?> GetCompanyByEmployerId(Guid employerId, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.OwnerId == employerId, cancellationToken);
        return company;
    }

    public async Task<string?> GetCompanyNameByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var companyName = await dbContext.Companies
            .Where(c => c.OwnerId == userId)
            .Select(c => c.CompanyName).FirstOrDefaultAsync(cancellationToken);
        return companyName;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}