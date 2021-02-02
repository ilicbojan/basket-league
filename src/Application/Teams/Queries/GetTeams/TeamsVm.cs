using System.Collections.Generic;

namespace Application.Teams.Queries.GetTeams
{
    public class TeamsVm
    {
        public IList<TeamDto> Teams { get; set; } = new List<TeamDto>();
    }
}