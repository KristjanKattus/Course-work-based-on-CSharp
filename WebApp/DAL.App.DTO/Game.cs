﻿using System;
using System.Collections.Generic;
using Domain.App;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Game : DomainEntityId
    {
        public Guid StadiumId { get; set; }
        public Stadium? Stadium { get; set; }

        public Guid LeagueId { get; set; }
        
        public int GameLength { get; set; } = 90;

        public ICollection<GameEvent>? GameEvents { get; set; }
        
        public ICollection<GameTeam>? GameTeams { get; set; }
        public int MatchRound { get; set; }
    }
}