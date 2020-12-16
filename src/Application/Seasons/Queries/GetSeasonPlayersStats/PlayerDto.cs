namespace Application.Seasons.Queries.GetSeasonPlayersStats
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MatchesPlayed { get; set; }
        public double PointsAvg { get; set; }
        public double AssistsAvg { get; set; }
        public double FoulsAvg { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
    }
}
