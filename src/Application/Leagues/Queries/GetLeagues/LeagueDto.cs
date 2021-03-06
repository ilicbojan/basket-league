﻿using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Leagues.Queries.GetLeagues
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityDto City { get; set; }
    }
}
