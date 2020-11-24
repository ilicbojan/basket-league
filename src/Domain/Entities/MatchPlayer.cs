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
    }
}
