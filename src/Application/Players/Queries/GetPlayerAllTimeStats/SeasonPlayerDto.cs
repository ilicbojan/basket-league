namespace Application.Players.Queries.GetPlayerAllTimeStats
{
    public class SeasonPlayerDto
    {
        public int MatchesPlayed { get; set; }
        public double PointsAvg { get; set; }
        public double AssistsAvg { get; set; }
        public double FoulsAvg { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
        public SeasonDto Season { get; set; }
    }
}