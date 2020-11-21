using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Teams.Queries.GetTeam
{
    public class TeamVm : IMapFrom<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
