using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.MatchPlayers.Queries.GetLineup
{
    public class MatchPlayerDto : IMapFrom<MatchPlayer>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }

        public TeamDto Team { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MatchPlayer, MatchPlayerDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PlayerId))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Player.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Player.User.LastName))
                .ForMember(d => d.Team, opt => opt.MapFrom(s => s.Player.Team));
        }
    }
}
