using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public bool IsCurrent { get; set; }

        public int LeagueId { get; set; }
        public virtual League League { get; set; }
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
