using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Matches.Queries.GetMatchStats
{
    public class MatchStatsVm : IMapFrom<Match>
    {
        public TeamDto HomeTeam { get; set; }
        public int HomePoints { get; set; }
        public int HomeAssists { get; set; }
        public int HomeFouls { get; set; }

        public TeamDto AwayTeam { get; set; }
        public int AwayPoints { get; set; }
        public int AwayAssists { get; set; }
        public int AwayFouls { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Match, MatchStatsVm>()
                .ForMember(d => d.HomeAssists, opt => opt.MapFrom(s => s.MatchPlayers.Where(x => x.Player.TeamId == s.HomeTeamId).Sum(x => x.Assists)))
                .ForMember(d => d.HomeFouls, opt => opt.MapFrom(s => s.MatchPlayers.Where(x => x.Player.TeamId == s.HomeTeamId).Sum(x => x.Fouls)))
                .ForMember(d => d.AwayAssists, opt => opt.MapFrom(s => s.MatchPlayers.Where(x => x.Player.TeamId == s.AwayTeamId).Sum(x => x.Assists)))
                .ForMember(d => d.AwayFouls, opt => opt.MapFrom(s => s.MatchPlayers.Where(x => x.Player.TeamId == s.AwayTeamId).Sum(x => x.Fouls)));
        }
    }
}