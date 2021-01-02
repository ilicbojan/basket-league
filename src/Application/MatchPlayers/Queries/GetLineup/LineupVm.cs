using System.Collections.Generic;

namespace Application.MatchPlayers.Queries.GetLineup
{
    public class LineupVm
    {
        public IList<MatchPlayerDto> Players { get; set; } = new List<MatchPlayerDto>();
    }
}