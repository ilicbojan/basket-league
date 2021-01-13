using System.Collections.Generic;

namespace Application.Players.Queries.GetPlayerCurrentStats
{
    public class PlayerCurrentStatsVm
    {
        public int MatchesPlayed { get; set; }
        public double PointsAvg { get; set; }
        public double AssistsAvg { get; set; }
        public double FoulsAvg { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
        public IList<MatchPlayerDto> Matches { get; set; } = new List<MatchPlayerDto>();
    }
}