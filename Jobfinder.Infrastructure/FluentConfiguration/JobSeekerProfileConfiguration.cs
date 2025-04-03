using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class JobSeekerProfileConfiguration : IEntityTypeConfiguration<JobSeekerProfile>
{
    public void Configure(EntityTypeBuilder<JobSeekerProfile> builder)
    {
        builder.HasKey(jsp => jsp.Id);
        
        builder.Property(jsp => jsp.Firstname).HasMaxLength(50)
            .IsRequired();
        builder.Property(jsp => jsp.Lastname).HasMaxLength(100)
            .IsRequired();
        
        builder.HasOne(jsp => jsp.JobSeekerCv) 
            .WithOne(c =>c.JobSeeker)
            .HasForeignKey<Cv>(c => c.JobSeekerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(jsp => jsp.User)
            .WithOne()
            .HasForeignKey<JobSeekerProfile>(jsp => jsp.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(jsp => jsp.JobApplications)
            .WithOne(ja => ja.JobSeekerProfile)
            .HasForeignKey(ja => ja.JobSeekerProfileId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();



    }
}