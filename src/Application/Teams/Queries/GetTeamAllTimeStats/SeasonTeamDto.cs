namespace Application.Teams.Queries.GetTeamAllTimeStats
{
    public class SeasonTeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int MatchesPlayed { get; set; }
        public double ScoredPointsAvg { get; set; }
        public double ReceivedPointsAvg { get; set; }
        public double AssistsAvg { get; set; }
        public double FoulsAvg { get; set; }
        public int ScoredPoints { get; set; }
        public int ReceivedPoints { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int PointsDiff { get; set; }
    }
}