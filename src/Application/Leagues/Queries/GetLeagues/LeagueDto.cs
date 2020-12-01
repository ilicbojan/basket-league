using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Leagues.Queries.GetLeagues
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}
