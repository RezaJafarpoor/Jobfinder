using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
{
    //TODO: Configuration for value object needed

    public void Configure(EntityTypeBuilder<JobOffer> builder)
    {
        builder.HasKey(jo => jo.Id);
        builder.Property(jo => jo.JobName).HasMaxLength(100).IsRequired();
        builder.Property(jo => jo.JobDescription).HasMaxLength(1500).IsRequired();
        builder.Property(jo => jo.CompanyName).HasMaxLength(100).IsRequired();
        builder.Property(jo => jo.JobName).HasMaxLength(100).IsRequired();
        builder.Property(jo => jo.JobDetails.Location.Address).HasMaxLength(1500).IsRequired();
        builder.Property(jo => jo.JobDetails.Location.City).HasMaxLength(200).IsRequired();
        builder.Property(jo => jo.JobDetails.Location.Province).HasMaxLength(50).IsRequired();
        
    }
}