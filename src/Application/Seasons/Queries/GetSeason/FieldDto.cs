using Application.Common.Mappings;

namespace Application.Seasons.Queries.GetSeason
{
    public class FieldDto : IMapFrom<Domain.Entities.Field>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
