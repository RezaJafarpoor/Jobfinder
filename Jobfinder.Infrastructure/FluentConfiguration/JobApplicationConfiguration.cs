using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.HasKey(ja => ja.Id);
        builder.HasOne(ja => ja.JobSeekerProfile)
            .WithMany(jsp => jsp.JobApplications)
            .HasForeignKey(ja => ja.JobSeekerProfileId);
        builder.HasOne(ja => ja.JobOffer)
            .WithMany(jo => jo.JobApplications)
            .HasForeignKey(ja => ja.JobOfferId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}