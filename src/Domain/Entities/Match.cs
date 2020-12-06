using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Round { get; set; }
        public int HomePoints { get; set; }
        public int AwayPoints { get; set; }

        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }
        public string RefereeId { get; set; }
        public virtual AppUser Referee { get; set; }
        public string DelegateId { get; set; }
        public virtual AppUser Delegate { get; set; }
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }

        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}
