using System.Collections.Generic;

namespace Application.Players.Queries.GetPlayers
{
    public class PlayersVm
    {
        public IList<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}