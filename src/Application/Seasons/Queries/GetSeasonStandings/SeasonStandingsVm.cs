using System.Collections.Generic;

namespace Application.Seasons.Queries.GetSeasonStandings
{
    public class SeasonVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public List<TeamDto> Standings { get; set; } = new List<TeamDto>();
    }
}
