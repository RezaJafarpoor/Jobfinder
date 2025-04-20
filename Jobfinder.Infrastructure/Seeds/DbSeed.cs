using Bogus;
using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Jobfinder.Domain.Interfaces;
using Jobfinder.Domain.ValueObjects;
using Jobfinder.Infrastructure.Persistence.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Jobfinder.Infrastructure.Seeds;

internal class DbSeed(ApplicationDbContext dbContext, Logger<DbSeed> logger) : ISeeder
{
    public async Task Seed()
    {
        await dbContext.Database.MigrateAsync();
        if (!dbContext.EmployerProfiles.Any())
        { 
            logger.LogCritical(" -------> Seeding ");
            await SeedEmployer();
            await SeedJobSeeker();
            await dbContext.SaveChangesAsync();   
        }


    }

    private async Task SeedEmployer()
    {
        var passwordHasher = new PasswordHasher<User>();

        var faker = new Faker<User>();
        faker
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UserName, (_,u) => u.Email)
            .RuleFor(u => u.NormalizedEmail, (_,u) => u.Email!.ToUpper())
            .RuleFor(u => u.NormalizedUserName, (_,u) => u.Email!.ToUpper())
            .RuleFor(u => u.PasswordHash, (f, u) =>
            {
                var password = f.Internet.Password();
                return passwordHasher.HashPassword(u, password);
            })
            .RuleFor(u => u.UserRole, _ => Domain.Enums.Roles.Employer);
        var users = faker.Generate(500);
        List<EmployerProfile> employerProfiles = [];
        foreach (var user in users)
        {
            var profile = new EmployerProfile(user);
            employerProfiles.Add(profile);
        }

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.EmployerProfiles.AddRangeAsync(employerProfiles);
        await SeedCompanies(employerProfiles);
        await SeedJobOffers(employerProfiles);
    }

    private async Task SeedCompanies(List<EmployerProfile> employers)
    {
        var faker = new Faker<Company>();
        faker
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName().ToString())
            .RuleFor(c => c.WebsiteAddress, f => f.Internet.Url())
            .RuleFor(c => c.PictureUrl, f => f.Internet.Url())
            .RuleFor(c => c.Location, f => new Location
            {
                Address = f.Address.StreetAddress(),
                City = f.Address.City(),
                Province = f.Address.State()
            })
            .RuleFor(c => c.SizeOfCompany, f => f.Random.Int(1,10000));
        var companies = faker.Generate(500);
        for (var i = 0; i < 500; i++)
            employers[i].AddCompany(companies[i]);
        await dbContext.Companies.AddRangeAsync(companies);
    }

    private async Task SeedJobSeeker()
    {        
        var passwordHasher = new PasswordHasher<User>();
        var faker = new Faker<User>();
        faker
            .RuleFor(u => u.Id, _ => Guid.NewGuid())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UserName, (_,u) => u.Email)
            .RuleFor(u => u.NormalizedEmail, (_,u) => u.Email!.ToUpper())
            .RuleFor(u => u.NormalizedUserName, (_,u) => u.Email!.ToUpper())
            .RuleFor(u => u.PasswordHash, (f, u) =>
            {
                var password = f.Internet.Password();
                return passwordHasher.HashPassword(u, password);
            })
            .RuleFor(u => u.UserRole, _ => Domain.Enums.Roles.JobSeeker);
        var users = faker.Generate(500);
        List<JobSeekerProfile> jobSeekerProfiles = [];
        foreach (var user in users)
        {
            var profile = new JobSeekerProfile(user, null, null);
            jobSeekerProfiles.Add(profile);
        }

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.JobSeekerProfiles.AddRangeAsync(jobSeekerProfiles);
        await SeedCv(jobSeekerProfiles);
    }

    private async Task SeedCv(List<JobSeekerProfile> jobSeekerProfiles)
    {
        var faker = new Faker<Cv>();
        faker.RuleFor(c => c.Location, f => new Location
            {
                Address = f.Address.StreetAddress(),
                City = f.Address.City(),
                Province = f.Address.State()
            })
            .RuleFor(c => c.BirthDay, f => f.Date.PastDateOnly(20))
            .RuleFor(c => c.ServiceStatus, _ => MilitaryServiceStatus.NotServedYet)
            .RuleFor(c => c.MaximumExpectedSalary, f => f.Random.Int(1, 500))
            .RuleFor(c => c.MinimumExpectedSalary, f => f.Random.Int(1, 500));
        var cvs = faker.Generate(500);
        for (int i = 0; i < 500; i++)
        {
            cvs[i].JobSeeker = jobSeekerProfiles[i];
            cvs[i].JobSeekerId = jobSeekerProfiles[i].Id;
        }

        await dbContext.Cvs.AddRangeAsync(cvs);
    }

    private async Task SeedJobOffers(List<EmployerProfile> employerProfiles)
    {
        var faker = new Faker<JobOffer>();
        faker.RuleFor(j => j.CompanyName, f => f.Company.CompanyName())
            .RuleFor(j => j.Salary, f => new Salary
            {
                From = f.Random.Int(1, 500),
                To = f.Random.Int(1, 500)
            })
            .RuleFor(j => j.JobName, f => f.Name.JobTitle())
            .RuleFor(j => j.JobDescription, f => f.Name.JobDescriptor())
            .RuleFor(j => j.Category, f => new JobCategory
            {
                Id = default,
                Category = f.Name.JobType(),
            });
        int i = 0;
        var jobOffers = faker.Generate(50000);
        for (int j = 0; j < 500; j++)
        {
            for ( ; i < 100; i++)
            {
                employerProfiles[j].AddJobOffer(jobOffers[i]);
            }
        }
    }
}