using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Matches.Queries.GetMatchStats
{
    public class TeamDto : IMapFrom<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
