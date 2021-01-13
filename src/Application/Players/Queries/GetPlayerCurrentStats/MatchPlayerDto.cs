namespace Application.Players.Queries.GetPlayerCurrentStats
{
    public class MatchPlayerDto
    {
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
        public TeamDto Team { get; set; }
    }
}