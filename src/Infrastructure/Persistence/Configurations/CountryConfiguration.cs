﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
