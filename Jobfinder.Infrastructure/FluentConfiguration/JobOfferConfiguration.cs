using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
{
   

    public void Configure(EntityTypeBuilder<JobOffer> builder)
    {
        builder.HasKey(jo => jo.Id);
        builder.Property(jo => jo.JobName).HasMaxLength(100).IsRequired();
        builder.Property(jo => jo.JobDescription).HasMaxLength(1500).IsRequired();
        builder.Property(jo => jo.CompanyName).HasMaxLength(100).IsRequired();
        builder.OwnsOne(jo => jo.JobDetails, jd =>
        {
            jd.OwnsOne(j => j.WorkingDatsAndHours);
            jd.OwnsOne(lc => lc.Location, loc =>
            {
                loc.Property(l => l.Address).HasMaxLength(1500).IsRequired();
                loc.Property(l => l.City).HasMaxLength(50).IsRequired();
                loc.Property(l => l.Province).HasMaxLength(50).IsRequired();

            });
        });
        builder.OwnsOne(jo => jo.Salary);
        

        builder.HasOne(jo => jo.Category)
            .WithMany(c => c.JobOffers)
            .HasForeignKey(jo => jo.CategoryId);

        builder.HasOne<EmployerProfile>()
            .WithMany(ep => ep.JobOffers)
            .HasForeignKey(jo => jo.EmployerProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(jo => jo.JobApplications)
            .WithOne(ja => ja.JobOffer)
            .HasForeignKey(ja => ja.JobOfferId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}