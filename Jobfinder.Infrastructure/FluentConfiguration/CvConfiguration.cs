using Jobfinder.Domain.Entities;
using Jobfinder.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class CvConfiguration : IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Location, loc =>
        {
            loc.Property(c => c.Address).HasMaxLength(1500).IsRequired();
            loc.Property(c => c.City).HasMaxLength(200).IsRequired();
            loc.Property(c => c.Province).HasMaxLength(50).IsRequired();
        });
        builder.Property(c => c.ServiceStatus).HasDefaultValue(MilitaryServiceStatus.NotServedYet);

        builder.HasOne(c => c.JobSeeker)
            .WithOne(jsp => jsp.JobSeekerCv)
            .HasForeignKey<Cv>(c => c.JobSeekerId);
    }
}