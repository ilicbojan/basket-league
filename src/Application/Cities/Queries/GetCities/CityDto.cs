using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Cities.Queries.GetCities
{
    public class CityDto : IMapFrom<City>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual CountryDto Country { get; set; }
    }
}