using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class TeamSeasonConfiguration : IEntityTypeConfiguration<TeamSeason>
    {
        public void Configure(EntityTypeBuilder<TeamSeason> builder)
        {
            builder.HasKey(ts => new { ts.TeamId, ts.SeasonId });

            builder.HasOne(ts => ts.Team)
                .WithMany(t => t.TeamSeasons)
                .HasForeignKey(ts => ts.TeamId);

            builder.HasOne(ts => ts.Season)
                .WithMany(s => s.TeamSeasons)
                .HasForeignKey(ts => ts.SeasonId);

            builder.Property(x => x.TeamId)
                .IsRequired();

            builder.Property(x => x.SeasonId)
                .IsRequired();
        }
    }
}
