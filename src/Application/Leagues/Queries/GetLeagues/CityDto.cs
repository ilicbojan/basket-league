using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Leagues.Queries.GetLeagues
{
    public class CityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}