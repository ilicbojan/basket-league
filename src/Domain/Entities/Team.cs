﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TeamSeason> TeamSeasons { get; set; }
    }
}
