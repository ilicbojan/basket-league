using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Seasons.Queries.GetSeasons
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
