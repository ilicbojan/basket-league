using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Seasons.Queries.GetSeason
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
