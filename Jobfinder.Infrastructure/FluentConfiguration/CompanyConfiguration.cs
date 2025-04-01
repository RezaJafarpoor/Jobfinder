using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CompanyName).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(1500).IsRequired();
        builder.Property(c => c.WebsiteAddress).HasMaxLength(200).IsRequired();
        builder.Property(c => c.SizeOfCompany).IsRequired();
        
        builder.OwnsOne(c => c.Location, loc =>
        {
            loc.Property(c => c.Address).HasMaxLength(1500).IsRequired();
            loc.Property(c => c.City).HasMaxLength(200).IsRequired();
            loc.Property(c => c.Province).HasMaxLength(50).IsRequired();
        });

        builder.HasOne<EmployerProfile>(c => c.Owner)
            .WithOne(ep => ep.Company)
            .HasForeignKey<Company>(c => c.OwnerId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}