using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MatchPlayer
    {
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int Points { get; set; }
        public int Assists { get; set; }
        public int Fouls { get; set; }
    }
}
