using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TeamSeason
    {
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
    }
}
