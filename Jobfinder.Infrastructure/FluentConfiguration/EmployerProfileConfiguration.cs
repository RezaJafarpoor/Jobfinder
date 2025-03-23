using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

public class EmployerProfileConfiguration : IEntityTypeConfiguration<EmployerProfile>
{
    public void Configure(EntityTypeBuilder<EmployerProfile> builder)
    {
        builder.HasKey(ep => ep.Id);

        builder.HasOne(ep => ep.Company)
            .WithOne()
            .HasForeignKey<EmployerProfile>(ep => ep.CompanyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ep => ep.User)
            .WithOne()
            .HasForeignKey<EmployerProfile>(ep => ep.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ep => ep.JobOffers)
            .WithOne()
            .HasForeignKey(jo =>jo.EmployerProfileId);


    }
}