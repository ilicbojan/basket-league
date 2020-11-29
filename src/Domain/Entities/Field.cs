using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
