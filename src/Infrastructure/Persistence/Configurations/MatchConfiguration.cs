using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Referee)
                .WithMany(r => r.RefereeMatches)
                .HasForeignKey(m => m.RefereeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Delegate)
                .WithMany(d => d.DelegateMatches)
                .HasForeignKey(m => m.DelegateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();

            builder.Property(x => x.Round)
                .IsRequired();

            builder.Property(x => x.HomePoints)
                .IsRequired();

            builder.Property(x => x.AwayPoints)
                .IsRequired();

            builder.Property(x => x.IsPlayed)
                .IsRequired();

            builder.Property(x => x.HomeTeamId)
                .IsRequired();

            builder.Property(x => x.AwayTeamId)
                .IsRequired();

            builder.Property(x => x.RefereeId)
                .IsRequired();

            builder.Property(x => x.DelegateId)
                .IsRequired();

            builder.Property(x => x.SeasonId)
                .IsRequired();
        }
    }
}
