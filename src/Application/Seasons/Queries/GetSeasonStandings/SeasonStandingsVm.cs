using System.Collections.Generic;

namespace Application.Seasons.Queries.GetSeasonStandings
{
    public class SeasonStandingsVm
    {
        public int SeasonId { get; set; }
        public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }
}
