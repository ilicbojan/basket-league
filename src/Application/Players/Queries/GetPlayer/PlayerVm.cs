using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Players.Queries.GetPlayer
{
    public class PlayerVm : IMapFrom<Player>
    {
        public int Id { get; set; }
        public int JerseyNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerVm>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName));
        }
    }
}