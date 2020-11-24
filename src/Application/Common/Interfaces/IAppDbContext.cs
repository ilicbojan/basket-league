using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<League> Leagues { get; set; }
        DbSet<Season> Seasons { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<TeamSeason> TeamSeasons { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<MatchPlayer> MatchPlayers { get; set; }


        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
