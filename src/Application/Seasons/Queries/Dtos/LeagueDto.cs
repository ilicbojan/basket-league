using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Seasons.Queries.Dtos
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
