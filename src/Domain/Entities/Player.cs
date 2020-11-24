using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public int JerseyNumber { get; set; }
        public string JMBG { get; set; }

        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<MatchPlayer> PlayerMatches { get; set; }
    }
}
