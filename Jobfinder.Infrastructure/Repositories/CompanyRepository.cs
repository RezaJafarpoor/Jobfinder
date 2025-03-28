using Azure.Core;
using Jobfinder.Domain.Dtos.Company;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Repositories;


//TODO: Add Result pattern to this class

internal class CompanyRepository(ApplicationDbContext dbContext) : ICompanyRepository
{
    public async Task<bool> CreateCompany(Guid employerId,CreateCompanyDto companyDto, CancellationToken cancellationToken)
    {

        var company = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Owner == employerId || c.CompanyName == companyDto.CompanyName, cancellationToken );
        if (company is null)
            return false;
        
        var newCompany = new Company
        {
            CompanyName = companyDto.CompanyName,
            WebsiteAddress = companyDto.WebsiteAddress,
            Location = companyDto.Location,
            SizeOfCompany = companyDto.SizeOfCompany,
            Description = companyDto.Description,
            Owner  = employerId
        };

        dbContext.Add(company);
        return await dbContext.SaveChangesAsync(cancellationToken) > 0;

    }

    public async Task<bool> UpdateCompany(Guid employerId, UpdateCompanyDto companyDto, CancellationToken cancellationToken)
    {
        var userCompany = await dbContext.Companies
            .FirstOrDefaultAsync(c => c.Owner == employerId, cancellationToken);
        if (userCompany is null)
            return false;
        userCompany.WebsiteAddress = companyDto.WebsiteAddress ?? userCompany.WebsiteAddress;
        userCompany.Location = companyDto.Location ?? userCompany.Location;
        userCompany.SizeOfCompany = companyDto.SizeOfCompany ?? userCompany.SizeOfCompany;
        userCompany.Description = companyDto.Description ?? userCompany.Description;

        return await dbContext.SaveChangesAsync(cancellationToken) > 0;

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
}