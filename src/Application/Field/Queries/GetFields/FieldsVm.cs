using System.Collections.Generic;

namespace Application.Field.Queries.GetFields
{
    public class FieldsVm
    {
        public IList<FieldDto> Fields { get; set; } = new List<FieldDto>();
    }
}