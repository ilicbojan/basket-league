using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Teams.Queries.GetTeam
{
    public class TeamVm : IMapFrom<Team>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentSeasonId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Team, TeamVm>()
                .ForMember(d => d.CurrentSeasonId, opt => opt.MapFrom(s => s.TeamSeasons.SingleOrDefault(x => x.Season.IsCurrent).SeasonId));
        }
    }
}
