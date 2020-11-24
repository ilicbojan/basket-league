using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class MatchPlayerConfiguration : IEntityTypeConfiguration<MatchPlayer>
    {
        public void Configure(EntityTypeBuilder<MatchPlayer> builder)
        {
            builder.HasKey(mp => new { mp.MatchId, mp.PlayerId });

            builder.HasOne(mp => mp.Match)
                .WithMany(m => m.MatchPlayers)
                .HasForeignKey(mp => mp.MatchId);

            builder.HasOne(mp => mp.Player)
                .WithMany(p => p.PlayerMatches)
                .HasForeignKey(mp => mp.PlayerId);

            builder.Property(x => x.MatchId)
                .IsRequired();

            builder.Property(x => x.PlayerId)
                .IsRequired();
        }
    }
}
