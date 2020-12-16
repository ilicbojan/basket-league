﻿using Application.Common.Mappings;
using Domain.Entities;
using System;

namespace Application.Matches.Queries.GetMatch
{
    public class MatchVm : IMapFrom<Match>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Round { get; set; }
        public int HomePoints { get; set; }
        public int AwayPoints { get; set; }
        public bool IsPlayed { get; set; }
    }
}