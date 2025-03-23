using Jobfinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobfinder.Infrastructure.FluentConfiguration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CompanyName).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Description).HasMaxLength(1500).IsRequired();
        builder.Property(c => c.WebsiteAddress).HasMaxLength(200).IsRequired();
        builder.Property(c => c.SizeOfCompany).IsRequired();
        builder.Property(c => c.Location.Address).HasMaxLength(1500).IsRequired();
        builder.Property(c => c.Location.City).HasMaxLength(200).IsRequired();
        builder.Property(c => c.Location.Province).HasMaxLength(50).IsRequired();

        builder.HasOne<EmployerProfile>()
            .WithOne(ep => ep.Company)
            .HasForeignKey<Company>(c => c.Owner);

    }
}