using System.Collections.Generic;

namespace Application.Seasons.Queries.GetSeasonResults
{
    public class ResultsVm
    {
        public IList<MatchDto> Matches { get; set; } = new List<MatchDto>();
    }
}
