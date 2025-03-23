using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

public class CvConfiguration : IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Location.Address).HasMaxLength(1500).IsRequired();
        builder.Property(c => c.Location.City).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Location.Province).HasMaxLength(50).IsRequired();
        builder.Property(c => c.ServiceStatus).HasDefaultValue(MilitaryServiceStatus.NotServedYet);

        builder.HasOne(c => c.JobSeeker)
            .WithOne(jsp => jsp.JobSeekerCv)
            .HasForeignKey<Cv>(c => c.JobSeekerId);
    }
}