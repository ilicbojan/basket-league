using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Leagues.Queries.GetLeagues
{
    public class LeagueDto : IMapFrom<League>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public IList<Season> Seasons { get; private set; } = new List<Season>();
    }
}
