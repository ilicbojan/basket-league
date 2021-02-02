using Application.Common.Mappings;

namespace Application.Field.Queries.GetFields
{
    public class FieldDto : IMapFrom<Domain.Entities.Field>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual CityDto City { get; set; }
    }
}