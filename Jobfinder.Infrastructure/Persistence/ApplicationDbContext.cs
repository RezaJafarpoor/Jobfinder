using Jobfinder.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jobfinder.Infrastructure.Persistence;

internal class ApplicationDbContext
    (DbContextOptions<ApplicationDbContext> options) :IdentityDbContext<User,Role,Guid> (options)
{   
    internal DbSet<User> ApplicationUsers { get; set; }
    internal DbSet<JobSeekerProfile> JobSeekerProfiles { get; set; }    
    internal DbSet<EmployerProfile> EmployerProfiles { get; set; }
    internal DbSet<Cv> Cvs { get; set; }
    internal DbSet<RefreshToken> RefreshTokens { get; set; }
    internal DbSet<Company> Companies { get; set; }
    internal DbSet<JobCategory> JobCategories { get; set; }
    internal DbSet<JobOffer> JobOffers { get; set; }
    internal DbSet<JobApplication> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}