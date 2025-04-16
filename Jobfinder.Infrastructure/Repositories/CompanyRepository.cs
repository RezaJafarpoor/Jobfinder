using Jobfinder.Application.Interfaces.Repositories;
using Jobfinder.Domain.Entities;
using Jobfinder.Infrastructure.Persistence.SqlServer;
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
        var companies = await dbContext.Companies.ToListAsync(cancellationToken);
        return companies;
    }

    public async Task<Company?> GetCompanyById(Guid companyId, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId, cancellationToken);
        return company ?? null;
    }

    public async Task<Company?> GetCompanyByEmployerId(Guid employerId, CancellationToken cancellationToken)
    {
        var company = await dbContext.Companies.
            FirstOrDefaultAsync( cancellationToken);
        return company;
    }

   

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;
    }
}