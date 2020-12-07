namespace Application.Seasons.Queries.GetSeasonStandings
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int ScoredPoints { get; set; }
        public int ReceivedPoints { get; set; }
        public int PointsDiff { get; set; }
        public int Points { get; set; }
    }
}
