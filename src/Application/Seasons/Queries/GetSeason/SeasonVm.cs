using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Seasons.Queries.GetSeason
{
    public class SeasonVm : IMapFrom<Season>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public LeagueDto League { get; set; }
        public FieldDto Field { get; set; }
    }
}
