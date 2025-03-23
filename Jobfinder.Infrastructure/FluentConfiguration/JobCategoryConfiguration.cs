using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

public class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
{
    public void Configure(EntityTypeBuilder<JobCategory> builder)
    {
        builder.HasKey(jc => jc.Id);
        builder.Property(jc => jc.Category).HasMaxLength(200).IsRequired();
        builder.HasMany(jc => jc.JobOffers)
            .WithOne(jo => jo.Category)
            .HasForeignKey(jo => jo.CategoryId);
    }
}