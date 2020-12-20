using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Seasons.Queries.GetSeasons
{
    public class SeasonDto : IMapFrom<Season>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public LeagueDto League { get; set; }
    }
}
