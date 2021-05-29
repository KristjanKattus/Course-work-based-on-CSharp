using System;
using System.Collections.Generic;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Game : DomainEntityId
    {
        public Guid StadiumId { get; set; }
        public Stadium? Stadium { get; set; }

        public Guid LeagueId { get; set; }
        
        public int GameLength { get; set; } = 90;

        public ICollection<GameEvent>? GameEvents { get; set; }

        public ICollection<GameEvent>? HomeTeamEvents { get; set; }
        public ICollection<GameEvent>? AwayTeamEvents { get; set; }
        public int MatchRound { get; set; }
    }
}