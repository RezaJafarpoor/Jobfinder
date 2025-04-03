using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class EmployerProfileConfiguration : IEntityTypeConfiguration<EmployerProfile>
{
    public void Configure(EntityTypeBuilder<EmployerProfile> builder)
    {
        builder.HasKey(ep => ep.Id);

        builder.HasOne(ep => ep.Company)
            .WithOne(c => c.Owner)
            .HasForeignKey<Company>(c => c.OwnerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(ep => ep.User)
            .WithOne()
            .HasForeignKey<EmployerProfile>(ep => ep.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ep => ep.JobOffers)
            .WithOne(jo => jo.EmployerProfile)
            .HasForeignKey(jo =>jo.EmployerProfileId);


    }
}