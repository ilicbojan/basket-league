using System.Collections.Generic;

namespace Application.Matches.Queries.GetH2HMatches
{
    public class H2HMatchesVm
    {
        public IList<MatchDto> Matches { get; set; } = new List<MatchDto>();
    }
}