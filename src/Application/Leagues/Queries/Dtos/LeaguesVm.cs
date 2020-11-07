using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Leagues.Queries.Dtos
{
    public class LeaguesVm
    {
        public IList<LeagueDto> Leagues { get; set; }
    }
}
