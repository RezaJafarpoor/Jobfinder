﻿using Jobfinder.Domain.Entities;
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
            loc.Property(c => c.Address).HasMaxLength(1500);
            loc.Property(c => c.City).HasMaxLength(200);
            loc.Property(c => c.Province).HasMaxLength(50);
        });
        builder.Property(c => c.ServiceStatus).HasDefaultValue(MilitaryServiceStatus.NotServedYet);

    }
}