using System;
using System.Collections.Generic;
using Domain.Base;

namespace BLL.App.DTO
{
    public class Game : DomainEntityId
    {
        public Guid StadiumId { get; set; }
        public Stadium Stadium { get; set; } = default!;

        public int GameLength { get; set; }

        public ICollection<GameEvent>? GameEvents { get; set; }
        public int MatchRound { get; set; }
    }
}